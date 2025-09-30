using SR_DMG.Source.Example;
using SR_DMG.Source.Example.Json;
using SR_DMG.Source.UI.Model;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.Employ
{
    public partial class Mihomo
    {
        public const string Url_Wiki_Home = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/";

        /// <summary>
        /// 更新来自 WIKI 的资源
        /// </summary>
        public static async void UpdataResources(int Target)
        {
            Progress Prog = Simple.App_Progress;
            Prog.Initialize(Simple.Tip_Mihomo_PullList_Title, Simple.Tip_Mihomo_PullList_Text);
            List<int> Pages = await GetWikiHome(Target);
            List<List<FileCollection>?> Retry = [];
            Prog.Title = Simple.Tip_Mihomo_Start_Download;
            Prog.Maxmum = Pages.Count;
            for (int i = 0; i < Pages.Count + 1; i++)
            {
                if (i == Pages.Count)
                {
                    if (i == 0) break;
                    if (await Prog.Retry()) i = 0;
                    else return;
                }
                try
                {
                    if (Prog.Canceller.IsCancellationRequested) return;
                    List<FileCollection>? Result;
                    if (Pages[i] > 0)
                    {
                        Result = await GetWikiPage(Pages[i]);
                    }
                    else
                    {
                        Result = Retry[-Pages[i]];
                    }
                    if (Result == null) continue;
                    bool _IsRetry = false;
                    for (int n = 0; n < Result.Count; n++)
                    {
                        if (Pages[i] > 0)
                        {
                            Result[n].Initialize(n > 0 ? Result[n - 1] : null);
                        }
                        bool IsRetry(bool Flag)
                        {
                            if (Flag) return _IsRetry = false;
                            if (Pages[i] <= 0) return _IsRetry = true;
                            Retry.Add(Result);
                            Pages[i] = 1 - Retry.Count;
                            return _IsRetry = true;
                        }
                        Prog.Text = Path.GetFileName(Result[0].Basicpath);
                        foreach (FileCollection.FileInfo Item in Result[n].Contents)
                        {
                            string FilePath = Result[n].GetPath(Item);
                            if (!File.Exists(FilePath))
                            {
                                Internet Requst = new()
                                {
                                    Url = Item.Url,
                                    Canceller = Prog.Canceller.Token
                                };
                                if (IsRetry(await Requst.Download(FilePath))) break;
                            }
                            Result[n].Initialize(Item);
                        }
                        if (n == Result.Count - 1)
                        {
                            string FilePath = Program.GetPath(Result[0].Basicpath, Simple.File_About);
                            if (!File.Exists(FilePath))
                            {
                                string Contents = Result[0].ToJson();
                                if (IsRetry(Program.FileWrite(FilePath, Contents))) continue;
                            }
                        }
                    }
                    if (_IsRetry) continue;
                    if (Pages[i] <= 0) Retry[-Pages[i]] = null;
                    Pages.RemoveAt(i--);
                    Prog.Value++;
                }
                catch (Exception Exception)
                {
                    Logger.Log(Exception);
                }
            }
            Prog.Completion();
        }

        /// <summary>
        /// 解析来自 WIKI 的页面目录
        /// </summary>
        /// <returns>标识页面 ID 的目录列表</returns>
        private static async Task<List<int>> GetWikiHome(int Target)
        {
            List<int> Result = [];
            HomeInfo Model = await HomeInfo.FormUrl();
            string[] Types = ["角色", "光锥", "遗器", "敌对物种"];
            for (int i = 0; i < Types.Length; i++)
            {
                if (Target > 0 && i != Target - 1) continue;
                foreach (var Item in Model.Data.List[0].Children.First(M => M.Name == Types[i]).List)
                {
                    Result.Add(Item.Content_Id);
                }
            }
            return Result;
        }

        [GeneratedRegex(@"data=""(.*?)""")]
        private static partial Regex ContentRegex();
        /// <summary>
        /// 解析来自 WIKI 的页面内容
        /// </summary>
        /// <returns>解析完成的结果，以及下载内容清单</returns>
        public static async Task<List<FileCollection>> GetWikiPage(int Target)
        {
            PageInfo Model = await PageInfo.FormUrl(Target);
            List<FileCollection> Download = [];
            Download.Add(new()
            {
                Basicpath = Simple.Fold_Mihomo_Wiki_Page(Target, Model.Data.Content.Title),
                Contents = [new() { File = Simple.File_Icon_Image, Url = Model.Data.Content.Icon }]
            });
            string Input = Model.Data.Content.Contents.FirstOrDefault()?.Text ?? string.Empty;
            if (Model.Data.Content.Summary.StartsWith("角色"))
            {
                Input = Model.Data.Content.Contents.FirstOrDefault(M => M.Name == "基础信息")?.Text ?? string.Empty;
            }
            List<string> Contents = [];
            MatchCollection Matches = ContentRegex().Matches(Input);
            foreach (Match M in Matches) Contents.Add(HttpUtility.UrlDecode(M.Groups[1].Value));
            switch (Model.Data.Content.Summary.Split('-')[0])
            {
                case "角色": GetRole(Download, Model, Contents); break;
                case "光锥": GetEquip(Download, Model, Contents); break;
                case "遗器" or "位面饰品": GetRelic(Download, Contents); break;
                case "敌人图鉴": await GetEnemy(Download, Contents); break;
                default: Logger.Log(Simple.Tip_Mihomo_Unfind_Page(Target, Model.Data.Content.Summary)); break;
            }
            return Download;
        }

        [GeneratedRegex(@"【(\d+)】(.*)")]
        private static partial Regex RanksRegex();
        /// <summary>
        /// 解析来自 WIKI 的角色信息
        /// </summary>
        private static void GetRole(List<FileCollection> Download, PageInfo Model, List<string> Contents)
        {
            string Ranks_Content = string.Empty;
            var Model_Role = Model.Data.Content.Rpg_Content;
            Role Role = new()
            {
                Basicpath = Path.Combine(Simple.Fold_Resources, Simple.Fold_Roles, Download[0].Basicpath),
                Contents = Download[0].Contents
            };
            string ParseModules(string name)
            {
                return Model_Role.Modules.First(M => M.Name.Contains(name)).Components[0].Data;
            }
            if (Model_Role == null)
            {
                Ranks_Content = SetRole(Contents, Role);
            }
            else
            {
                Ranks_Content = Program.FormJson<RoleInfo.Role_Rank>(ParseModules("星魂")).RichText;
                Role.Type = Simple.GetRoleType(Model_Role.Base.UserInfo.BaseType);
                Role.Element = Simple.GetElement(Model_Role.Base.UserInfo.ElementId);
                Role.Star = Model_Role.Base.UserInfo.Rarity;
                Role.Name = Model.Data.Content.Title;
                Dictionary<string, string> Dictionary = [];
                RoleInfo.Role_Ascent Model_Ascent = Program.FormJson<RoleInfo.Role_Ascent>(ParseModules("晋阶"));
                foreach (var Item in Model_Ascent.List.First(M => M.TabName.StartsWith("80级")).Attr)
                {
                    Dictionary[Item.Key] = Item.Value[0];
                }
                SetRole(Dictionary, Role);
                int Index = 1;
                RoleInfo.Role_Trace Model_Trace = Program.FormJson<RoleInfo.Role_Trace>(ParseModules("行迹"));
                foreach (var PointItem in Model_Trace.Points)
                {
                    if (PointItem.Tag.StartsWith("属性")) continue;
                    int Target = 1;
                    foreach (var SubItem in PointItem.SubList)
                    {
                        Skill Skill = new()
                        {
                            Name = SubItem.SubTitle,
                            Text = Program.Get_Html_Text(SubItem.SubDesc),
                            Tags = [PointItem.Tag, .. SubItem.SubTag],
                            Url = PointItem.Icon
                        };
                        if (string.IsNullOrWhiteSpace(Skill.Name))
                        {
                            Skill.Name = PointItem.Name;
                        }
                        SetRole(PointItem.TableData.Rows, Role, Skill, ref Target, ref Index);
                    }
                }
            }
            Role.Rank.Basicpath = Path.Combine(Role.Basicpath, Simple.Fold_Ranks);
            string[] Ranks = Regex.Unescape(Ranks_Content).Split("</tr>");
            for (int i = 1; i < Ranks.Length; i += 2)
            {
                Match Match = RanksRegex().Match(Program.Get_Html_Text(Ranks[i - 1]));
                if (!Match.Success) continue;
                Skill Skill = new()
                {
                    Name = Program.Get_Html_Text(Match.Groups[2].Value),
                    Text = Program.Get_Html_Text(Ranks[i]),
                    Tags = [Simple.Tag_Mihomo_Rank_Image(Match.Groups[1].Value)],
                    File = Simple.File_Mihomo_Rank_Image(Match.Groups[1].Value)
                };
                Match = SourceRegex().Match(Ranks[i - 1]);
                Skill.Url = Match.Groups[1].Value;
                Role.Rank.Contents.Add(Skill);
            }
            Download.AddRange([Role.Trace, Role.Rank]);
            Download[0] = Role;
        }

        /// <summary>
        /// 设置来自 WIKI 的角色信息（特殊版本）
        /// </summary>
        /// <returns>角色星魂的 HTML 片段</returns>
        private static string SetRole(List<string> Contents, Role Role)
        {
            Dictionary<string, string> Dictionary = [];
            RoleInfo.NewMain Model_NewMain = (RoleInfo.NewMain)RoleInfo.FromJson(Contents[0])[0];
            foreach (var Item in Model_NewMain.Data.MainFields)
            {
                (Dictionary[Item.NameL], Dictionary[Item.NameR]) = (Item.ValueL, Item.ValueR);
            }
            string[] Tags = Dictionary.GetValueOrDefault("命途/属性")?.Split('/') ?? [];
            (Role.Type, Role.Element) = (Tags.First(), Tags.Last());
            (Role.Name, Role.Star) = (Model_NewMain.Data.Name, Model_NewMain.Data.Star);
            RoleInfo.Breach Model_Breach = (RoleInfo.Breach)RoleInfo.FromJson(Contents[1])[0];
            foreach (var Item in Model_Breach.Data.Attr.First(M => M.Name.StartsWith("80级")).Attr)
            {
                (Dictionary[Item.NameL], Dictionary[Item.NameR]) = (Item.ValueL, Item.ValueR);
            }
            SetRole(Dictionary, Role);
            int Index = 1;
            RoleInfo[] Model_Trace = RoleInfo.FromJson(Contents[2]);
            foreach (var Item in ((RoleInfo.Trace)Model_Trace.First(M => M is RoleInfo.Trace)).Data.Attr.Points)
            {
                Tags = Item.Name.Split("</span>", 2);
                string Tag = Program.Get_Html_Text(Tags[0]);
                if (Tag.StartsWith("属性")) continue;
                string[] Names = Program.Get_Html_Text(Tags[Math.Min(1, Tags.Length - 1)]).Split('/');
                string[] Desc = Item.Desc.Split("<hr>");
                int Target = 1;
                for (int i = 0; i < Desc.Length; i++)
                {
                    Tags = Desc[i].Split("</em>", 2);
                    Skill Skill = new()
                    {
                        Name = Names[Math.Min(i, Names.Length - 1)].Trim(),
                        Text = Program.Get_Html_Text(Tags.Last()),
                        Url = Item.Icon
                    };
                    if (!string.IsNullOrWhiteSpace(Tag)) Skill.Tags.Add(Tag);
                    if (Tags.Length > 1)
                    {
                        Skill.Tags.AddRange(Program.Get_Html_Text(Tags[0].Split("</strong>", 2).Last()).Split('、'));
                    }
                    SetRole(Item.TableData.Rows, Role, Skill, ref Target, ref Index);
                }
            }
            if (Model_Trace.FirstOrDefault(M => M is RoleInfo.Desc) is not RoleInfo.Desc Model_Desc)
            {
                Model_Desc = (RoleInfo.Desc)RoleInfo.FromJson(Contents[3])[0];
            }
            return Model_Desc.Data.Text;
        }

        [GeneratedRegex(@"【([\d%/.]+)】")]
        private static partial Regex TraceRegex();
        /// <summary>
        /// 设置角色行迹不同等级的数值，并将技能添加到角色行迹中
        /// </summary>
        private static void SetRole(string[][] Content, Role Role, Skill Skill, ref int Target, ref int Index)
        {
            if (string.IsNullOrWhiteSpace(Skill.Text)) return;
            MatchCollection Matchs = TraceRegex().Matches(Skill.Text);
            List<string> Parmas = [];
            foreach (Match M in Matchs)
            {
                Parmas.Add(M.Groups[1].Value);
            }
            Parmas = [.. Parmas.Distinct()];
            for (int i = 0; i < Parmas.Count; i++)
            {
                List<string> Values = [];
                if (Target >= Content[0].Length) continue;
                foreach (string[] Item in Content) Values.Add(Item[Target]);
                if (Target < Content.Length - 1) Target++;
                Skill.Values.Add(Values);
            }
            Skill.File = $"{Simple.Fold_Trace}-{Index++}";
            Role.Trace.Contents.Add(Skill);
        }

        /// <summary>
        /// 设置角色基础面板数值，以及等级突破的行迹加成
        /// </summary>
        private static void SetRole(Dictionary<string, string> Dictionary, Role Role)
        {
            int GetValue(string name)
            {
                return (int)Program.GetFloat(Dictionary.GetValueOrDefault(name));
            }
            Role.Trace.Basicpath = Path.Combine(Role.Basicpath, Simple.Fold_Trace);
            Role.ATK = GetValue("攻击力");
            Role.HP = GetValue("生命值");
            Role.DEF = GetValue("防御力");
            Role.SPD = GetValue("速度");
            Role.ERG = GetValue("终结技启动所需");
            if (!Dictionary.TryGetValue("行迹加成", out string? Result)) return;
            Role.Trace.Contents.Add(new Skill()
            {
                Name = Simple.Tag_Trace_Gain,
                Tags = [Simple.Tag_Trace],
                Text = Result
            });
        }

        /// <summary>
        /// 解析来自 WIKI 的光锥信息
        /// </summary>
        private static void GetEquip(List<FileCollection> Download, PageInfo Model, List<string> Contents)
        {
            EquipInfo[] Models = EquipInfo.FromJson(Contents[0]);
            EquipInfo.Main Model_Main = (EquipInfo.Main)Models.First(M => M is EquipInfo.Main);
            Equip Equip = new()
            {
                Name = Model.Data.Content.Title,
                Type = Model_Main.Data.Career,
                Star = Convert.ToInt32(Model_Main.Data.Rate[0].ToString()),
                Basicpath = Path.Combine(Simple.Fold_Resources, Simple.Fold_Equips, Download[0].Basicpath),
                Contents = Download[0].Contents
            };
            Equip.Skill.Basicpath = Equip.Basicpath;
            string[] Tags = Model_Main.Data.Skill.Split("</h3>", 2);
            Skill Skill = new()
            {
                Tags = [Simple.Tag_Equip],
                Name = Program.Get_Html_Text(Tags[0]),
                Text = Program.Get_Html_Text(Tags[1]),
                File = Simple.File_Icon_Image,
                Url = Equip.Contents[0].Url
            };
            MatchCollection Matches = TraceRegex().Matches(Skill.Text);
            foreach (Match M in Matches)
            {
                Skill.Values.Add([.. M.Groups[1].Value.Split('/')]);
            }
            Equip.Skill.Contents.Add(Skill);
            EquipInfo.Value Model_Value = (EquipInfo.Value)Models.First(M => M is EquipInfo.Value);
            var Material = Model_Value.Data.Material.First(M => M.Name.StartsWith("80级"));
            (Equip.HP, Equip.ATK, Equip.DEF) = (Material.Life, Material.Attack, Material.Defense);
            Download.Add(Equip.Skill);
            Download[0] = Equip;
        }

        /// <summary>
        /// 解析来自 WIKI 的遗器信息
        /// </summary>
        private static void GetRelic(List<FileCollection> Download, List<string> Contents)
        {
            List<RelicInfo> Models = [];
            foreach (string Item in Contents)
            {
                Models.AddRange(RelicInfo.FromJson(Item));
            }
            Relic Relic = new()
            {
                Type = Models.Count > 4 ? Simple.Tag_Relic_Tunnel : Simple.Tag_Relic_Ornament,
                Basicpath = Path.Combine(Simple.Fold_Resources, Simple.Fold_Relics, Download[0].Basicpath),
                Contents = Download[0].Contents
            };
            Relic.Skill.Basicpath = Relic.Basicpath;
            foreach (RelicInfo Model in Models)
            {
                if (Model is not RelicInfo.Main Model_Main) continue;
                int Relic_Id = Simple.GetRelicIndex(Model_Main.Data.Content[0].Name);
                string Relic_Name = Model_Main.Data.Content[0].Value;
                string Relic_Image = Model_Main.Data.Image;
                if (Relic_Id > 0)
                {
                    Relic.Parts[Relic_Id.ToString()] = Relic_Name;
                    Relic.Contents.Add(new()
                    {
                        File = Simple.File_Mihomo_Relic_Image(Relic_Id),
                        Url = Relic_Image
                    });
                }
                else
                {
                    Relic.Name = Relic_Name;
                    string[] Tags = Model_Main.Data.Story.Split("</p>");
                    for (int i = 0; i < Tags.Length - 1; i++)
                    {
                        Skill Skill = new()
                        {
                            Name = Relic_Name,
                            Text = Program.Get_Html_Text(Tags[i]),
                            Tags = [Relic.Type],
                            File = Simple.File_Icon_Image,
                            Url = Relic.Contents[0].Url
                        };
                        Relic.Skill.Contents.Add(Skill);
                    }
                }
            }
            Download.Add(Relic.Skill);
            Download[0] = Relic;
        }

        [GeneratedRegex(@"src=""(.*?)""")]
        private static partial Regex SourceRegex();
        /// <summary>
        /// 解析来自 WIKI 的敌人信息
        /// </summary>
        private static async Task GetEnemy(List<FileCollection> Download, List<string> Contents)
        {
            EnemyInfo[] Models = EnemyInfo.FromJson(Contents[0]);
            EnemyInfo.Main Model_Main = (EnemyInfo.Main)Models.First(M => M is EnemyInfo.Main);
            Enemy Enemy = new()
            {
                Name = Model_Main.Data.Name,
                Type = Model_Main.Data.Kind,
                Basicpath = Path.Combine(Simple.Fold_Resources, Simple.Fold_Enemy, Download[0].Basicpath),
                Contents = Download[0].Contents
            };
            Enemy.Contents[0].Url = Model_Main.Data.Image;
            Enemy.Tag.AddRange(Program.Get_Html_Text(Model_Main.Data.Resistance).Split('、').Where(s => s != "无"));
            MatchCollection Matches = SourceRegex().Matches(Model_Main.Data.Weak);
            foreach (Match M in Matches)
            {
                Internet Neork = new()
                {
                    Url = M.Groups[1].Value
                };
                using HttpResponseMessage? Response = await Neork.Response();
                if (Response == null) continue;
                BitmapImage Bmp = new();
                Bmp.BeginInit();
                Bmp.CacheOption = BitmapCacheOption.OnLoad;
                Bmp.StreamSource = await Response.Content.ReadAsStreamAsync();
                Bmp.EndInit();
                CroppedBitmap Cmp = new(Bmp, new System.Windows.Int32Rect(Bmp.PixelWidth / 2, Bmp.PixelHeight / 2, 1, 1));
                byte[] Pixels = new byte[4];
                Cmp.CopyPixels(Pixels, 4, 0);
                int Index = 0;
                int[] Values = new int[Simple.Element_Colors.GetLength(0)];
                for (int i = 0; i < Values.Length; i++)
                {
                    int Abs_R = Math.Abs(Simple.Element_Colors[i, 0] - Pixels[2]);
                    int Abs_G = Math.Abs(Simple.Element_Colors[i, 1] - Pixels[1]);
                    int Abs_B = Math.Abs(Simple.Element_Colors[i, 2] - Pixels[0]);
                    Values[i] = Abs_R + Abs_G + Abs_B;
                    if (Values[i] < Values[Index]) Index = i;
                }
                Enemy.Weak.Add(Simple.GetElement(Index));
            }
            Download[0] = Enemy;
        }
    }
}