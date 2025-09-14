using SR_DMG.Source.Example;
using SR_DMG.Source.Example.Json;
using SR_DMG.Source.UI.Model;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.Employ
{
    public partial class Mihomo
    {
        public const string Url_Wiki_Home = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/v1/";

        public static async void GetResources(int Target, List<int>? Pages = null)
        {
            Progress Prog = Simple.App_Progress;
            Prog.Title = Simple.Tip_Mihomo_PullList_Title;
            Prog.Text = Simple.Tip_Mihomo_PullList_Text;
            Prog.Initialize();
            List<FileCollection.UrlInfo> About = [];
            List<FileCollection.UrlInfo> Download = [];
            Pages ??= await GetWikiHome(Target);
            Prog.Maxmum = Pages.Count;
            static string GetFolderName(string Str)
            {
                List<string> Floders = [.. Str.Split(Path.DirectorySeparatorChar)];
                return Floders[Floders.FindIndex(S => S == Simple.Fold_Resources) + 2];
            }
            Prog.Title = Simple.Tip_Mihomo_Start_Download;
            for (int i = 0; i < Pages.Count + 1; i++)
            {
                if (Prog.Canceller.IsCancellationRequested) return;
                if (i >= Pages.Count)
                {
                    if (i == 0) break;
                    if (await Prog.Retry()) i = 0;
                    else return;
                }
                try
                {
                    List<FileCollection> Result = await GetWikiPage(Pages[i]);
                    Prog.Text = GetFolderName(Result[0].Basicpath);
                    for (int n = 0; n < Result.Count; n++)
                    {
                        Result[n].Initialize();
                        if (n == Result.Count - 1)
                        {
                            if (!Program.FileWrite(Result[n].Contents[0]))
                            {
                                About.Add(Result[n].Contents[0]);
                            }
                            continue;
                        }
                        foreach (FileCollection.UrlInfo Item in Result[n].Contents)
                        {
                            Internet Requst = new()
                            {
                                Url = Item.Url,
                                Canceller = Prog.Canceller.Token
                            };
                            if (await Requst.Download(Item.Path)) continue;
                            Download.Add(Item);
                        }
                    }
                    Pages.RemoveAt(i--);
                    Prog.Value++;
                }
                catch (Exception Excep) { Logger.Log(Excep); }
            }
            while (About.Count + Download.Count > 0)
            {
                Prog.Value = 0;
                Prog.Maxmum = About.Count + Download.Count;
                if (!await Prog.Retry()) return;
                for (int i = 0; i < About.Count; i++)
                {
                    if (Prog.Canceller.IsCancellationRequested) return;
                    Prog.Text = GetFolderName(About[i].Path);
                    if (Program.FileWrite(About[i]))
                    {
                        About.RemoveAt(i--);
                        Prog.Value++;
                    }
                }
                for (int i = 0; i < Download.Count; i++)
                {
                    if (Prog.Canceller.IsCancellationRequested) return;
                    Internet Request = new()
                    {
                        Url = Download[i].Url,
                        Canceller = Prog.Canceller.Token
                    };
                    Prog.Text = GetFolderName(Download[i].Path);
                    if (await Request.Download(Download[i].Path))
                    {
                        Download.RemoveAt(i--);
                        Prog.Value++;
                    }
                }
            }
            Prog.Completion();
        }

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
        public static async Task<List<FileCollection>> GetWikiPage(int Target)
        {
            PageInfo Model = await PageInfo.FormUrl(Target);
            List<FileCollection> Download = [];
            Download.Add(new()
            {
                Basicpath = Simple.Fold_Mihomo_Wiki_Page(Target, Model.Data.Content.Title),
                Contents = [new ()
                {
                    Path = Simple.File_Icon_Image,
                    Url = Model.Data.Content.Icon
                }]
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
            Download[^1].Basicpath = Download[0].Basicpath;
            Download[^1].Contents[0].Path = Simple.File_About;
            return Download;
        }

        [GeneratedRegex(@"【(\d+)】(.*)")]
        private static partial Regex RanksRegex();
        private static void GetRole(List<FileCollection> Download, PageInfo Model, List<string> Contents)
        {
            Role Role = new();
            string RanksContent = string.Empty;
            var Model_Role = Model.Data.Content.Rpg_Content;
            Download[0].Basicpath = Program.GetPath(Simple.Fold_Resources, Simple.Fold_Roles, Download[0].Basicpath);
            string ParseModules(string name)
            {
                return Model_Role.Modules.First(M => M.Name.Contains(name)).Components[0].Data;
            }
            if (Model_Role == null)
            {
                RanksContent = GetRole(Download, Contents, Role);
            }
            else
            {
                RanksContent = Program.FormJson<RoleInfo.Role_Rank>(ParseModules("星魂")).RichText;
                Role.Type = Simple.GetRoleType(Model_Role.Base.UserInfo.BaseType);
                Role.Element = Simple.GetElement(Model_Role.Base.UserInfo.ElementId);
                Role.Star = Model_Role.Base.UserInfo.Rarity;
                Role.Name = Model.Data.Content.Title;
                Dictionary<string, string> Dinary = [];
                RoleInfo.Role_Ascent Model_Ascent = Program.FormJson<RoleInfo.Role_Ascent>(ParseModules("晋阶"));
                foreach (var Item in Model_Ascent.List.First(M => M.TabName.StartsWith("80级")).Attr)
                {
                    Dinary[Item.Key] = Item.Value[0];
                }
                SetRole(Dinary, Role);
                RoleInfo.Role_Trace Model_Trace = Program.FormJson<RoleInfo.Role_Trace>(ParseModules("行迹"));
                int Index = 1;
                FileCollection File_Trace = new()
                {
                    Basicpath = Path.Combine(Download[0].Basicpath, Simple.Fold_Trace)
                };
                foreach (var PointItem in Model_Trace.Points)
                {
                    if (PointItem.Tag.StartsWith("属性")) continue;
                    string Icon = $"{Simple.Fold_Trace}-{Index++}";
                    File_Trace.Contents.Add(new() { Path = Icon, Url = PointItem.Icon });
                    int Target = 1;
                    foreach (var SubItem in PointItem.SubList)
                    {
                        Skill Skill = new()
                        {
                            Name = SubItem.SubTitle,
                            Text = Program.Get_Html_Text(SubItem.SubDesc),
                            Tags = [PointItem.Tag, .. SubItem.SubTag],
                            Icon = Icon
                        };
                        if (string.IsNullOrEmpty(Skill.Name)) Skill.Name = PointItem.Name;
                        SetRole(PointItem.TableData.Rows, Role, Skill, ref Target);
                    }
                }
                Download.Add(File_Trace);
            }
            FileCollection File_Rank = new()
            {
                Basicpath = Path.Combine(Download[0].Basicpath, Simple.Fold_Ranks)
            };
            string[] Ranks = Regex.Unescape(RanksContent).Split("</tr>");
            for (int i = 1; i < Ranks.Length; i += 2)
            {
                Match Match = RanksRegex().Match(Program.Get_Html_Text(Ranks[i - 1]));
                if (!Match.Success) continue;
                FileCollection.UrlInfo UrlInfo = new()
                {
                    Path = Simple.File_Mihomo_Rank_Image(Match.Groups[1].Value)
                };
                Role.Ranks.Add(new()
                {
                    Name = Program.Get_Html_Text(Match.Groups[2].Value),
                    Text = Program.Get_Html_Text(Ranks[i]),
                    Tags = [Simple.Tag_Mihomo_Rank_Image(Match.Groups[1].Value)]
                });
                Match = SourceRegex().Match(Ranks[i - 1]);
                UrlInfo.Url = Match.Groups[1].Value;
                File_Rank.Contents.Add(UrlInfo);
            }
            Download.Add(File_Rank);
            Download.Add(new()
            {
                Contents = [new() { Url = JsonSerializer.Serialize(Role, Simple.JsonOptions) }]
            });
        }

        private static string GetRole(List<FileCollection> Download, List<string> Contents, Role Role)
        {
            Dictionary<string, string> Dinary = [];
            RoleInfo.NewMain Model_NewMain = (RoleInfo.NewMain)RoleInfo.FromJson(Contents[0])[0];
            foreach (var Item in Model_NewMain.Data.MainFields)
            {
                (Dinary[Item.NameL], Dinary[Item.NameR]) = (Item.ValueL, Item.ValueR);
            }
            string[] Tags = Dinary.GetValueOrDefault("命途/属性")?.Split('/') ?? [];
            (Role.Name, Role.Type, Role.Element) = (Model_NewMain.Data.Name, Tags.First(), Tags.Last());
            RoleInfo.Breach Model_Breach = (RoleInfo.Breach)RoleInfo.FromJson(Contents[1])[0];
            foreach (var Item in Model_Breach.Data.Attr.First(M => M.Name.StartsWith("80级")).Attr)
            {
                (Dinary[Item.NameL], Dinary[Item.NameR]) = (Item.ValueL, Item.ValueR);
            }
            SetRole(Dinary, Role);
            int Index = 1;
            RoleInfo[] Model_Trace = RoleInfo.FromJson(Contents[2]);
            FileCollection File_Trace = new()
            {
                Basicpath = Path.Combine(Download[0].Basicpath, Simple.Fold_Trace)
            };
            foreach (var Item in ((RoleInfo.Trace)Model_Trace.First(M => M is RoleInfo.Trace)).Data.Attr.Points)
            {
                Tags = Item.Name.Split("</span>", 2);
                string Tag = Program.Get_Html_Text(Tags[0]);
                if (Tag.StartsWith("属性")) continue;
                string[] Names = Program.Get_Html_Text(Tags[Math.Min(1, Tags.Length - 1)]).Split('/');
                string[] Desc = Item.Desc.Split("<hr>");
                string Icon = $"{Simple.Fold_Trace}-{Index++}";
                File_Trace.Contents.Add(new()
                {
                    Path = Icon,
                    Url = Item.Icon
                });
                int Target = 1;
                for (int i = 0; i < Desc.Length; i++)
                {
                    Tags = Desc[i].Split("</em>", 2);
                    Skill Skill = new()
                    {
                        Name = Names[Math.Min(i, Names.Length - 1)],
                        Text = Program.Get_Html_Text(Tags.Last()),
                        Icon = Icon,
                        Tags = [Tag]
                    };
                    if (Tags.Length > 1)
                    {
                        Tags = Program.Get_Html_Text(Tags[0].Split("</strong>", 2).Last()).Split('、');
                        Skill.Tags.AddRange(Tags);
                    }
                    SetRole(Item.TableData.Rows, Role, Skill, ref Target);
                }
            }
            Download.Add(File_Trace);
            if (Model_Trace.FirstOrDefault(M => M is RoleInfo.Desc) is not RoleInfo.Desc Model_Desc)
            {
                Model_Desc = (RoleInfo.Desc)RoleInfo.FromJson(Contents[3])[0];
            }
            return Model_Desc.Data.Text;
        }

        [GeneratedRegex(@"【([\d%/.]+)】")]
        private static partial Regex TraceRegex();
        private static void SetRole(string[][] Content, Role Role, Skill Skill, ref int Index)
        {
            List<string> Parmas = [];
            MatchCollection Matchs = TraceRegex().Matches(Skill.Text);
            foreach (Match M in Matchs) Parmas.Add(M.Groups[1].Value);
            Parmas = [.. Parmas.Distinct()];
            for (int i = 0; i < Parmas.Count; i++)
            {
                List<string> Values = [];
                if (Index >= Content[0].Length) continue;
                foreach (string[] Item in Content) Values.Add(Item[Index]);
                if (Index < Content.Length - 1) Index++;
                Skill.Values.Add(Values);
            }
            Role.Trace.Add(Skill);
        }

        private static void SetRole(Dictionary<string, string> Dinary, Role Role)
        {
            Role.Attack = (int)Program.GetFloat(Dinary.GetValueOrDefault("攻击力"));
            Role.Health = (int)Program.GetFloat(Dinary.GetValueOrDefault("生命值"));
            Role.Denfense = (int)Program.GetFloat(Dinary.GetValueOrDefault("防御力"));
            Role.Speed = (int)Program.GetFloat(Dinary.GetValueOrDefault("速度"));
            Role.Energy = (int)Program.GetFloat(Dinary.GetValueOrDefault("终结技启动所需"));
            if (!Dinary.TryGetValue("行迹加成", out string? Result)) return;
            Role.Trace.Add(new Skill()
            {
                Name = Simple.Tag_Trace_Gain,
                Tags = [Simple.Tag_Trace],
                Values = [[.. Result.Split('、')]],
                Text = Result
            });
        }

        private static void GetEquip(List<FileCollection> Download, PageInfo Model, List<string> Contents)
        {
            EquipInfo[] Models = EquipInfo.FromJson(Contents[0]);
            EquipInfo.Main Model_Main = (EquipInfo.Main)Models.First(M => M is EquipInfo.Main);
            Equip Equip = new()
            {
                Name = Model.Data.Content.Title,
                Skills = [new() { Tags = [Simple.Tag_Equip] }],
                Type = Model_Main.Data.Career,
                Star = Convert.ToInt32(Model_Main.Data.Rate[0].ToString())
            };
            string[] Tags = Model_Main.Data.Skill.Split("</h3>", 2);
            Equip.Skills[0].Name = Program.Get_Html_Text(Tags[0]);
            Equip.Skills[0].Text = Program.Get_Html_Text(Tags[1]);
            MatchCollection Matches = TraceRegex().Matches(Equip.Skills[0].Text);
            foreach (Match M in Matches)
            {
                Equip.Skills[0].Values.Add([.. M.Groups[1].Value.Split('/')]);
            }
            EquipInfo.Value Model_Value = (EquipInfo.Value)Models.First(M => M is EquipInfo.Value);
            var Material = Model_Value.Data.Material.First(M => M.Name.StartsWith("80级"));
            (Equip.Health, Equip.Attack, Equip.Denfense) = (Material.Life, Material.Attack, Material.Defense);
            Download[0].Basicpath = Program.GetPath(Simple.Fold_Resources, Simple.Fold_Equips, Download[0].Basicpath);
            Download.Add(new()
            {
                Contents = [new() { Url = JsonSerializer.Serialize(Equip, Simple.JsonOptions) }]
            });
        }

        private static void GetRelic(List<FileCollection> Download, List<string> Contents)
        {
            RelicInfo[] Models = RelicInfo.FromJson(Contents[0]);
            Relic Relic = new() { Tpye = Models.Length > 4 ? Simple.Tag_Relic_Tunnel : Simple.Tag_Relic_Ornament };
            foreach (RelicInfo Model in Models)
            {
                if (Model is not RelicInfo.Main Model_Main) continue;
                int Relic_Id = Simple.GetRelicIndex(Model_Main.Data.Content[0].Name);
                string Relic_Name = Model_Main.Data.Content[0].Value;
                string Relic_Image = Model_Main.Data.Image;
                if (Relic_Id == 0)
                {
                    Relic.Name = Relic_Name;
                    string[] Tags = Model_Main.Data.Story.Split("</p>");
                    for (int i = 0; i < Tags.Length - 1; i++)
                    {
                        Skill Skill = new()
                        {
                            Name = Relic_Name,
                            Text = Program.Get_Html_Text(Tags[i]),
                            Tags = [Relic.Tpye]
                        };
                        Relic.Skills.Add(Skill);
                    }
                    Download[0].Contents[0].Url = Relic_Image;
                    continue;
                }
                Relic.Parts[Relic_Id.ToString()] = Relic_Name;
                Download[0].Contents.Add(new()
                {
                    Path = Simple.File_Mihomo_Relic_Image(Relic_Id),
                    Url = Relic_Image
                });
            }
            Download[0].Basicpath = Program.GetPath(Simple.Fold_Resources, Simple.Fold_Relics, Download[0].Basicpath);
            Download.Add(new()
            {
                Contents = [new() { Url = JsonSerializer.Serialize(Relic, Simple.JsonOptions) }]
            });
        }

        [GeneratedRegex(@"src=""(.*?)""")]
        private static partial Regex SourceRegex();
        private static async Task GetEnemy(List<FileCollection> Download, List<string> Contents)
        {
            EnemyInfo[] Models = EnemyInfo.FromJson(Contents[0]);
            EnemyInfo.Main Model_Main = (EnemyInfo.Main)Models.First(M => M is EnemyInfo.Main);
            Enemy Enemy = new() { Name = Model_Main.Data.Name, Type = Model_Main.Data.Kind };
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
            Download[0].Basicpath = Program.GetPath(Simple.Fold_Resources, Simple.Fold_Enemy, Download[0].Basicpath);
            Download[0].Contents[0].Url = Model_Main.Data.Image;
            Download.Add(new()
            {
                Contents = [new() { Url = JsonSerializer.Serialize(Enemy, Simple.JsonOptions) }]
            });
        }
    }
}