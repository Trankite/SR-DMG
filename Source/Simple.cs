using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace SR_DMG.Source
{
    interface Simple
    {
        static readonly string NewLine = Environment.NewLine;
        static readonly string RootFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App);
        const string App = "SR-DMG";
        const string Log = "logs.txt";
        const string About = "About.json"; // 描述文件
        const string Resources = "Resources"; // 资源目录
        static readonly string Roles = Path.Combine(Resources, "Roles"); // 角色
        static readonly string Equips = Path.Combine(Resources, "Equips"); // 光锥
        static readonly string Relics = Path.Combine(Resources, "Relics"); // 侵蚀隧洞
        static readonly string Enemy = Path.Combine(Resources, "Enemy"); // 敌对物种
        const string Ranks = "Ranks"; // 星魂
        const string Trace = "Trace"; // 行迹
        const string Url_Wiki_Home = "https://act-api-takumi-static.mihoyo.com/common/blackboard/sr_wiki/";
        static Exception Exp_Network_Error = new("网络请求失败！");
        static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        static readonly Color[] Element_Colors = [
            Color.FromRgb(205, 205, 205),
            Color.FromRgb(180, 20, 5),
            Color.FromRgb(75, 165, 220),
            Color.FromRgb(190, 90, 220),
            Color.FromRgb(100, 205, 150),
            Color.FromRgb(110, 100, 205),
            Color.FromRgb(240, 220, 90)];
        public static string Get_Html_Text(string html)
        {
            MatchCollection Mats = Regex.Matches(html, @"(?<=>|^)(.*?)(?=<|$)");
            html = string.Empty;
            foreach (Match M in Mats)
            {
                html += M.Groups[1].Value;
            }
            return html;
        }
        public static string Get_Role_Type(int id)
        {
            return Get_Role_Type(id.ToString());
        }
        public static string Get_Role_Type(string? name)
        {
            return name switch
            {
                "0" or "Destruction" => "毁灭",
                "1" or "TheHunt" => "巡猎",
                "2" or "Erudition" => "智识",
                "3" or "Harmony" => "同协",
                "4" or "Nihility" => "虚无",
                "5" or "Preservation" => "存护",
                "6" or "Abundance" => "丰饶",
                "7" or "Memory" => "记忆",
                _ => string.Empty
            };
        }
        public static string Get_Element(int id)
        {
            return Get_Element(id.ToString());
        }
        public static string Get_Element(string? name)
        {
            return name switch
            {
                "0" or "physical" => "物理",
                "1" or "fire" => "火",
                "2" or "ice" => "冰",
                "3" or "lightning" => "雷",
                "4" or "wind" => "风",
                "5" or "quantum" => "量子",
                "6" or "imaginary" => "虚数",
                _ => string.Empty
            };
        }
        public static string Get_Element(Color color)
        {
            int Index = 0;
            int[] Values = new int[Element_Colors.Length];
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = Math.Abs(Element_Colors[i].R - color.R)
                    + Math.Abs(Element_Colors[i].G - color.G)
                    + Math.Abs(Element_Colors[i].B - color.B);
                if (Values[i] < Values[Index]) Index = i;
            }
            return Get_Element(Index);
        }
        public static int Get_Relic_ID(string? name)
        {
            return name switch
            {
                "头部" => 1,
                "手部" => 2,
                "躯干" => 3,
                "脚部" => 4,
                "位面球" => 5,
                "连结绳" => 6,
                _ => 0
            };
        }
    }
}
