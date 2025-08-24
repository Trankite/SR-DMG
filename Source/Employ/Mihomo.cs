using SR_DMG.Source.Example;
using SR_DMG.Source.Material;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Media;
using static System.Text.Json.JsonElement;

namespace SR_DMG.Source.Employ
{
    public class Mihomo
    {
        public static async void GetResources(int Type)
        {
            List<string> Fails = [];
            (List<int> Roles, List<int> Equips, List<int> Relics, List<int> Enemy) = await GetWikiHome();
            List<int> Pages = Type switch
            {
                1 => Roles,
                2 => Equips,
                3 => Relics,
                4 => Enemy,
                _ => [.. Roles, .. Equips, .. Relics, .. Enemy]
            };
            async Task Download(List<string> Urls)
            {
                for (int i = 0; i < Urls.Count - 1; i += 2)
                {
                    if (string.IsNullOrEmpty(Urls[i + 1])) continue;
                    if (await Internet.Download(HttpMethod.Get, Urls[i + 1], Urls[i])) continue;
                    Fails.AddRange([Urls[i], Urls[i + 1]]);
                }
            }
            async Task Updata()
            {
                for (int i = 0; i < Pages.Count; i++)
                {
                    try
                    {
                        List<string> Urls = await GetWikiPage(Pages[i]);
                        if (Urls == null) continue;
                        File.WriteAllText(Path.Combine(Urls[0], Simple.About), Urls[^1]);
                        for (int n = 1; n < Urls.Count; n += 2)
                        {
                            Urls[n] = Path.Combine(Urls[0], Urls[n]);
                        }
                        Urls.RemoveAt(0);
                        Pages[i] = -1;
                        await Download(Urls);
                    }
                    catch (Exception Exp)
                    {
                        Logger.Log(Exp, $"尝试下载资源[{Pages[i]}]时发生意外错误。");
                    }
                }
            }
            int Expire = 0;
            int Total = Pages.Count;
            while (true)
            {
                if (Pages.Count > 0)
                {
                    await Updata();
                    Pages.RemoveAll(x => x < 0);
                }
                if (Pages.Count + Fails.Count == 0)
                {
                    break;
                }
                else if (true)
                {
                    if (Fails.Count > 0)
                    {
                        await Download(Fails);
                        Fails.RemoveRange(0, Expire);
                        Expire = Fails.Count;
                    }
                }
            }
        }

        private static async Task<(List<int> Roles, List<int> Equips, List<int> Relics, List<int> Enemy)> GetWikiHome()
        {
            string Url = $"{Simple.Url_Wiki_Home}v1/home/content/list?app_sn=sr_wiki&channel_id=17";
            string Page = await Internet.GetResult(HttpMethod.Get, Url) ?? throw Simple.Exp_Network_Error;
            JsonDocument Json = JsonDocument.Parse(Page);
            ArrayEnumerator Children = Json.RootElement
                .GetProperty("data")
                .GetProperty("list")
                .EnumerateArray().First()
                .GetProperty("children")
                .EnumerateArray();
            List<int> JsonParse(ArrayEnumerator Children, string name)
            {
                List<int> Content = [];
                JsonElement Jem = Children.First(E => E.GetProperty("name").GetString() == name);
                foreach (JsonElement E in Jem.GetProperty("list").EnumerateArray())
                {
                    Content.Add(E.GetProperty("content_id").GetInt32());
                }
                return Content;
            }
            List<int> Roles = JsonParse(Children, "角色");
            List<int> Equips = JsonParse(Children, "光锥");
            List<int> Relics = JsonParse(Children, "遗器");
            List<int> Enemy = JsonParse(Children, "敌对物种");
            return (Roles, Equips, Relics, Enemy);
        }

        public static async Task<List<string>> GetWikiPage(int Id)
        {
            string Url = $"{Simple.Url_Wiki_Home}v1/content/info?app_sn=sr_wiki&content_id={Id}";
            string Page = await Internet.GetResult(HttpMethod.Get, Url) ?? throw Simple.Exp_Network_Error;
            using JsonDocument Json = JsonDocument.Parse(Page);
            JsonElement Content = Json.RootElement.GetProperty("data").GetProperty("content");
            string Name = Content.GetProperty("title").GetString() ?? string.Empty;
            string Icon = Content.GetProperty("icon").GetString() ?? string.Empty;
            string Summary = Content.GetProperty("summary").GetString()?.Split('-')[0] ?? string.Empty;
            JsonElement RoleConten = Content.GetProperty("rpg_new_tmp_content");
            ArrayEnumerator Tags = Content.GetProperty("contents").EnumerateArray();
            string Data = RoleConten.ValueKind == JsonValueKind.Null ?
                (Summary == "角色" ? Tags.First(E => E.GetProperty("name").GetString()?
                .StartsWith("基础信息") ?? false) : Tags.First()).GetProperty("text")
                .GetString() ?? string.Empty : string.Empty;
            using JsonDocument Extra = JsonDocument.Parse(Content.GetProperty("ext").GetString() ?? string.Empty);
            JsonElement Table = Extra.RootElement.GetProperty(Extra.RootElement.EnumerateObject().First().Name);
            string Filter = Table.GetProperty("filter").GetProperty("text").GetString() ?? string.Empty;
            List<string> Contents = [];
            List<string> Download = [Name, "Icon", Icon];
            MatchCollection Mats = Regex.Matches(Data, @"data=""(.*?)""");
            foreach (Match M in Mats)
            {
                Contents.Add($"{{\"s\":{HttpUtility.UrlDecode(M.Groups[1].Value)}}}");
            }
            switch (Summary)
            {
                case "角色":
                    GetWikiRole(ref Download, RoleConten, Contents, Name);
                    break;
                case "光锥":
                    GetWikiEquip(ref Download, Contents, Name);
                    break;
                case "遗器":
                case "位面饰品":
                    GetWikiRelic(ref Download, Contents, Name);
                    break;
                case "敌人图鉴":
                    await Task.Run(() => GetWikiEnemy(ref Download, Contents, Name));
                    break;
                default:
                    Logger.Log($"无效的标签：[{Id}-{Name}]{Summary}");
                    break;
            }
            Directory.CreateDirectory(Download[0]);
            return Download;
        }
        private static string GetWikiRole(ref List<string> Download, List<string> Contents, ref Role role)
        {
            char[] Keys = ['L', 'R'];
            using (JsonDocument Json = JsonDocument.Parse(Contents[0]))
            {
                JsonElement Data = Json.RootElement.GetProperty("s").EnumerateArray().First(
                    E => E.GetProperty("partKey").GetString() == "newMain").GetProperty("data");
                role.Star = Data.GetProperty("star").GetInt32();
                ArrayEnumerator Fields = Data.GetProperty("mainFields").EnumerateArray();
                for (int i = 0; i < Keys.Length; i++)
                {
                    Data = Fields.FirstOrDefault(E => E.GetProperty("name" + Keys[i]).GetString() == "命途/属性");
                    if (Data.ValueKind == JsonValueKind.Undefined) continue;
                    string[] Tags = Data.GetProperty("value" + Keys[i]).GetString()?.Split('/', 2) ?? [];
                    role.Type = Tags[0];
                    role.Element = Tags[1];
                    break;
                }
            }
            using (JsonDocument Json = JsonDocument.Parse(Contents[1]))
            {
                foreach (JsonElement E in Json.RootElement.GetProperty("s").EnumerateArray()
                    .First(E => E.GetProperty("partKey").GetString() == "breach").GetProperty("data")
                    .GetProperty("attr").EnumerateArray().First(E => E.GetProperty("name_").GetString()?
                    .StartsWith("80级") ?? false).GetProperty("attr").EnumerateArray())
                {
                    for (int i = 0; i < Keys.Length; i++)
                    {
                        int GetValue()
                        {
                            return Convert.ToInt32(E.GetProperty("value" + Keys[i]).GetString());
                        }
                        switch (E.GetProperty("name" + Keys[i]).GetString())
                        {
                            case "生命值": role.HP = GetValue(); break;
                            case "攻击力": role.ATK = GetValue(); break;
                            case "防御力": role.DEF = GetValue(); break;
                            case "速度": role.SPD = GetValue(); break;
                        }
                    }
                }
            }
            JsonElement ParseModules(JsonDocument Json, string name)
            {
                return Json.RootElement.GetProperty("s").EnumerateArray().FirstOrDefault(
                    E => E.GetProperty("partKey").GetString() == name);
            }
            using (JsonDocument Json = JsonDocument.Parse(Contents[2]))
            {
                int Index = 1;
                foreach (JsonElement E in ParseModules(Json, "trace").GetProperty("data")
                    .GetProperty("attr").GetProperty("points").EnumerateArray())
                {
                    if (!E.TryGetProperty("name", out JsonElement Skill_Name)) continue;
                    string[] Tags = Skill_Name.GetString()?.Split("</span>", 2) ?? [];
                    string Tag = Simple.Get_Html_Text(Tags[0]);
                    if (Tag.StartsWith("属性")) continue;
                    string[] Names = Simple.Get_Html_Text(Tags[1]).Trim().Split('/');
                    string[] Desc = E.GetProperty("desc").GetString()?.Split("<hr>") ?? [];
                    Download.Add(Path.Combine(Simple.Trace, $"{Simple.Trace}-{Index}"));
                    Download.Add(E.GetProperty("icon").GetString() ?? string.Empty);
                    int Row = 1;
                    List<List<JsonElement>> Rows = [];
                    foreach (JsonElement Je in E.GetProperty("tableData").GetProperty("rows").EnumerateArray())
                    {
                        Rows.Add([.. Je.EnumerateArray()]);
                    }
                    for (int i = 0; i < Desc.Length; i++)
                    {
                        Skill skill = new()
                        {
                            Name = Names[Math.Min(i, Names.Length - 1)]
                        };
                        skill.Tag.Add(Tag);
                        Tags = Desc[i].Split("</em>", 2);
                        if (Tags.Length > 1)
                        {
                            skill.Tag.AddRange(Simple.Get_Html_Text(
                                Tags[0].Split("</strong>", 2).Last()).Split('、'));
                        }
                        skill.Text = Simple.Get_Html_Text(Tags.Last());
                        SetWikiRole(ref Rows, ref role, ref skill, ref Row);
                        Index++;
                    }
                }
                string RanksContent = GetRanksContent(Json);
                if (!string.IsNullOrEmpty(RanksContent))
                {
                    return RanksContent;
                }
            }
            string GetRanksContent(JsonDocument Json)
            {
                JsonElement Ranks = ParseModules(Json, "desc");
                if (Ranks.ValueKind != JsonValueKind.Undefined)
                {
                    return Ranks.GetProperty("data").GetProperty("text").GetString() ?? string.Empty;
                }
                else return string.Empty;
            }
            using (JsonDocument Json = JsonDocument.Parse(Contents[3]))
            {
                return GetRanksContent(Json);
            }
        }
        private static void GetWikiRole(ref List<string> Download, JsonElement RoleContent, List<string> Contents, string Name)
        {
            Role role = new()
            {
                Name = Name
            };
            string RanksContent = string.Empty;
            string ParseModules(ArrayEnumerator Modules, string name)
            {
                return Modules.First(E => E.GetProperty("name").GetString()?.StartsWith(name) ?? false)
                    .GetProperty("components").EnumerateArray().First()
                    .GetProperty("data").GetString() ?? string.Empty;
            }
            if (RoleContent.ValueKind == JsonValueKind.Null)
            {
                RanksContent = GetWikiRole(ref Download, Contents, ref role);
            }
            else
            {
                JsonElement UserInfo = RoleContent.GetProperty("base").GetProperty("userInfo");
                role.Type = Simple.Get_Role_Type(UserInfo.GetProperty("baseType").GetString());
                role.Element = Simple.Get_Element(UserInfo.GetProperty("elementId").GetString());
                role.Star = UserInfo.GetProperty("rarity").GetInt32();
                ArrayEnumerator Modules = RoleContent.GetProperty("modules").EnumerateArray();
                JsonDocument Json = JsonDocument.Parse(ParseModules(Modules, "角色晋阶"));
                RanksContent = ParseModules(Modules, "角色星魂");
                int GetValue(JsonElement E)
                {
                    return int.TryParse(E.GetProperty("value").EnumerateArray()
                        .First().GetString(), out int Value) ? Value : 0;
                }
                ArrayEnumerator Attrs = Json.RootElement.GetProperty("list").EnumerateArray();
                foreach (JsonElement E in Attrs.First().GetProperty("attr").EnumerateArray())
                {
                    if (E.GetProperty("key").GetString() == "速度") role.SPD = GetValue(E);
                }
                foreach (JsonElement E in Attrs.Last().GetProperty("attr").EnumerateArray())
                {
                    switch (E.GetProperty("key").GetString())
                    {
                        case "晋阶后生命值": role.HP = GetValue(E); break;
                        case "晋阶后攻击力": role.ATK = GetValue(E); break;
                        case "晋阶后防御力": role.DEF = GetValue(E); break;
                    }
                }
                int Index = 1;
                Json = JsonDocument.Parse(ParseModules(Modules, "角色行迹"));
                foreach (JsonElement E in Json.RootElement.GetProperty("points").EnumerateArray())
                {
                    if (!E.TryGetProperty("tag", out RoleContent)) continue;
                    string Tag = RoleContent.GetString() ?? string.Empty;
                    if (Tag.StartsWith("属性")) continue;
                    string SubName = E.GetProperty("name").GetString() ?? string.Empty;
                    Download.Add(Path.Combine(Simple.Trace, $"{Simple.Trace}-{Index}"));
                    Download.Add(E.GetProperty("icon").GetString() ?? string.Empty);
                    int Row = 1;
                    List<List<JsonElement>> Rows = [];
                    foreach (JsonElement Je in E.GetProperty("tableData").GetProperty("rows").EnumerateArray())
                    {
                        Rows.Add([.. Je.EnumerateArray()]);
                    }
                    foreach (JsonElement Je in E.GetProperty("subList").EnumerateArray())
                    {
                        Skill skill = new()
                        {
                            Tag = [Tag],
                            Name = Je.GetProperty("subTitle").GetString() ?? string.Empty
                        };
                        if (string.IsNullOrEmpty(skill.Name))
                        {
                            skill.Name = SubName;
                        }
                        skill.Text = Simple.Get_Html_Text(Je.GetProperty("subDesc").GetString() ?? string.Empty);
                        foreach (JsonElement Jem in Je.GetProperty("subTag").EnumerateArray())
                        {
                            skill.Tag.Add(Jem.GetString() ?? string.Empty);
                        }
                        SetWikiRole(ref Rows, ref role, ref skill, ref Row);
                        Index++;
                    }
                }
            }
            string[] Ranks = Regex.Unescape(RanksContent).Split("</tr>");
            for (int i = 1; i < Ranks.Length; i += 2)
            {
                Match Mat = Regex.Match(Simple.Get_Html_Text(Ranks[i - 1]), @"【(\d+)】(.*)");
                if (!Mat.Success) continue;
                Download.Add(Path.Combine(Simple.Ranks, $"{Simple.Ranks}-{Mat.Groups[1].Value}"));
                Skill skill = new()
                {
                    Name = Simple.Get_Html_Text(Mat.Groups[2].Value),
                    Text = Simple.Get_Html_Text(Ranks[i])
                };
                skill.Tag.Add($"星魂-{Mat.Groups[1].Value}");
                role.Ranks.Add(skill);
                Mat = Regex.Match(Ranks[i - 1], @"src=""(.*?)""");
                Download.Add(Mat.Groups[1].Value);
            }
            Download[0] = Program.GetPath(Simple.Roles, Download[0]);
            Download.Add(JsonSerializer.Serialize(role, Simple.JsonOptions));
            Directory.CreateDirectory(Path.Combine(Download[0], Simple.Ranks));
            Directory.CreateDirectory(Path.Combine(Download[0], Simple.Trace));
        }
        private static void SetWikiRole(ref List<List<JsonElement>> Rows, ref Role role, ref Skill skill, ref int Row)
        {
            List<string> Parmas = [];
            MatchCollection Mats = Regex.Matches(skill.Text, @"(【[\d%/.]+】)");
            foreach (Match M in Mats)
            {
                Parmas.Add(M.Groups[1].Value);
            }
            Parmas = [.. Parmas.Distinct()];
            for (int n = 0; n < Parmas.Count; n++)
            {
                List<string> Values = [];
                if (Row >= Rows[0].Count) continue;
                foreach (List<JsonElement> Jem in Rows)
                {
                    Values.Add(Jem[Row].GetString() ?? string.Empty);
                }
                if (Row < Rows.Count - 1) Row++;
                skill.Values.Add(Values);
            }
            role.Skills.Add(skill);
        }
        private static void GetWikiEquip(ref List<string> Download, List<string> Contents, string Name)
        {
            Equip equip = new()
            {
                Name = Name
            };
            using JsonDocument Json = JsonDocument.Parse(Contents[0]);
            ArrayEnumerator Content = Json.RootElement.GetProperty("s").EnumerateArray();
            JsonElement ParseModules(string name)
            {
                return Content.First(E => E.GetProperty("partKey").GetString() == name).GetProperty("data");
            }
            equip.Skills[0].Tag.Add("光锥");
            JsonElement MainContent = ParseModules("main");
            equip.Type = MainContent.GetProperty("career").GetString() ?? string.Empty;
            equip.Star = Convert.ToInt32(MainContent.GetProperty("rate").GetString()?.First().ToString());
            string[] Tags = MainContent.GetProperty("skill").GetString()?.Split("</h3>", 2) ?? [];
            equip.Skills[0].Name = Simple.Get_Html_Text(Tags[0]);
            equip.Skills[0].Text = Simple.Get_Html_Text(Tags[1]);
            MatchCollection Mats = Regex.Matches(equip.Skills[0].Text, @"【([\d%/.]+)】");
            foreach (Match M in Mats)
            {
                equip.Skills[0].Values.Add([.. M.Groups[1].Value.Split('/')]);
            }
            JsonElement Material = ParseModules("value").GetProperty("material").EnumerateArray()
                    .First(E => E.GetProperty("name").GetString()?.StartsWith("80级") ?? false);
            equip.HP = Material.GetProperty("life").GetInt32();
            equip.ATK = Material.GetProperty("attack").GetInt32();
            equip.DEF = Material.GetProperty("defense").GetInt32();
            Download[0] = Program.GetPath(Simple.Equips, Download[0]);
            Download.Add(JsonSerializer.Serialize(equip, Simple.JsonOptions));
        }
        private static void GetWikiRelic(ref List<string> Download, List<string> Contents, string Name)
        {
            Relic relic = new()
            {
                Name = Name
            };
            using JsonDocument Json = JsonDocument.Parse(Contents[0]);
            foreach (JsonElement E in Json.RootElement.GetProperty("s").EnumerateArray())
            {
                if (E.GetProperty("partKey").GetString() != "main") continue;
                JsonElement Data = E.GetProperty("data");
                JsonElement Content = Data.GetProperty("content").EnumerateArray().First();
                string Relic_Type = Content.GetProperty("name").GetString() ?? string.Empty;
                string Relic_Name = Content.GetProperty("value").GetString() ?? string.Empty;
                int Relic_ID = Simple.Get_Relic_ID(Relic_Type);
                relic.Tpye = Relic_ID > 4 ? "位面饰品" : "隧洞遗器";
                string Img_Url = Data.GetProperty("image").GetString() ?? string.Empty;
                if (Relic_ID == 0)
                {
                    relic.Name = Relic_Name;
                    string[] Tags = Data.GetProperty("story").GetString()?.Split("</p>", 2) ?? [];
                    for (int i = 1; i < Tags.Length; i++)
                    {
                        Skill skill = new()
                        {
                            Name = relic.Name,
                            Text = Simple.Get_Html_Text(Tags[i - 1])
                        };
                        skill.Tag.Add("遗器");
                        relic.Skills.Add(skill);
                    }
                    Download[2] = Img_Url;
                }
                else
                {
                    Download.Add($"{Relic_Type}-{Relic_Name}");
                    Download.Add(Img_Url);
                }
            }
            Download[0] = Program.GetPath(Simple.Relics, Download[0]);
            Download.Add(JsonSerializer.Serialize(relic, Simple.JsonOptions));
        }
        private static void GetWikiEnemy(ref List<string> Download, List<string> Contents, string Name)
        {
            Enemy enemy = new()
            {
                Name = Name
            };
            using JsonDocument Json = JsonDocument.Parse(Contents[0]);
            JsonElement Data = Json.RootElement.GetProperty("s").EnumerateArray()
                .First(E => E.GetProperty("partKey").GetString() == "main").GetProperty("data");
            enemy.Type = Data.GetProperty("kind").GetString() ?? string.Empty;
            enemy.Tag.AddRange(Simple.Get_Html_Text(Data.GetProperty("resistance")
                .GetString() ?? string.Empty).Split('、').Where(s => s != "无"));
            MatchCollection Mats = Regex.Matches(Data.GetProperty("weak")
                .GetString() ?? string.Empty, @"src=""(.*?)""");
            foreach (Match M in Mats)
            {
                Color? Weak_Color = Internet.GetColor(HttpMethod.Get, M.Groups[1].Value).Result;
                if (Weak_Color == null) continue;
                enemy.Weak.Add(Simple.Get_Element((Color)Weak_Color));
            }
            Download[0] = Program.GetPath(Simple.Enemy, Download[0]);
            Download[2] = Data.GetProperty("image").GetString() ?? string.Empty;
            Download.Add(JsonSerializer.Serialize(enemy, Simple.JsonOptions));
        }
    }
}