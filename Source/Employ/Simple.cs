using SR_DMG.Source.UI.Model;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SR_DMG.Source.Employ
{
    public class Simple
    {
        public const string App_Name = "SR-DMG";

        public const string File_Log = "logs.txt";
        public const string File_About = "About.json";
        public const string File_Icon_Image = "Icon";

        public const string Fold_Resources = "Resources";
        public const string Fold_Roles = "Roles";
        public const string Fold_Equips = "Equips";
        public const string Fold_Relics = "Relics";
        public const string Fold_Enemy = "Enemy";
        public const string Fold_Ranks = "Ranks";
        public const string Fold_Trace = "Trace";

        public const string Tag_Trace = "行迹";
        public const string Tag_Equip = "光锥";
        public const string Tag_Relic_Tunnel = "隧洞遗器";
        public const string Tag_Relic_Ornament = "位面饰品";
        public const string Tag_Trace_Gain = "行迹加成";

        public const string Tip_Input_Not_Number = "这不是一个有效的数值！";
        public const string Tip_Progress_Unfinished = "任务未完成，重试以继续";
        public const string Tip_Progress_Finished_Title = "任务完成";
        public const string Tip_Progress_Finished_Text = "任务已全部完成";
        public const string Tip_Mihomo_PullList_Title = "提取资源列表";
        public const string Tip_Mihomo_PullList_Text = "等待获取 WIKI 首页列表";
        public const string Tip_Mihomo_Start_Download = "开始下载资源";

        public const string Lay_DateTime = "yyyy-MM-dd HH:mm:ss";
        public const string Lay_PlusMinus = "+0.#%;-0.#%";

        public static string File_Mihomo_Rank_Image(string id) => $"{Fold_Ranks}-{id}";
        public static string File_Mihomo_Relic_Image(int id) => $"{Fold_Relics}-{id}";

        public static string Fold_Mihomo_Wiki_Page(int id, string name) => $"{id} {name}";

        public static string Tag_Mihomo_Rank_Image(string id) => $"星魂-{id}";

        public static string Tip_Mihomo_Unfind_Page(int id, string title) => $"无效的标签：[{id} {title}]";

        public static readonly string NewLine = Environment.NewLine;

        public static readonly BitmapImage Not_Find_Image = (BitmapImage)Application.Current.Resources["Icon_Not_Image"];

        public static readonly Progress App_Progress = new();

        public static readonly string App_Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App_Name);

        public static readonly Exception Exp_Network_Error = new("网络请求失败！");

        public static readonly Dictionary<string, BitmapImage> AppImages = [];
        public static readonly Dictionary<string, BitmapImage> RoleImages = [];

        public static readonly JsonSerializerOptions JsonOptions = new() { IncludeFields = true, WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        public static readonly int[,] Element_Colors = { { 205, 205, 205 }, { 180, 20, 5 }, { 75, 165, 220 }, { 190, 90, 220 }, { 100, 205, 150 }, { 110, 100, 205 }, { 240, 220, 90 } };

        public static readonly GainItem UnInit_GainItem = new()
        {
            Title = "技能 / 增益 名称",
            Text = "尝试添加一些数据吧！这是一个临时的默认值，当没有任何数据时才会显示这个，虽然你可以照常进行修改，但是此条数据不会被保存。",
            Tags = ["系统", "默认值", "自动移除", "不可修改"]
        };

        public static string GetElement(int id) => GetElement(id.ToString());

        public static string GetElement(string? name) => name switch
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

        public static int GetElementIndex(string? name) => name switch
        { "火" => 1, "冰" => 2, "雷" => 3, "风" => 4, "量子" => 5, "虚数" => 6, _ => 0 };

        public static string GetElementBreak(int id) => id switch
        { 1 => "灼烧", 2 => "冻结", 3 => "触电", 4 => "风化", 5 => "纠缠", 6 => "禁锢", _ => "裂伤" };

        public static string GetRoleType(int id) => GetRoleType(id.ToString());

        public static string GetRoleType(string? name) => name switch
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

        public static int GetRoleTypeIndex(string? name) => name switch
        { "毁灭" => 1, "巡猎" => 2, "智识" => 3, "同协" => 4, "虚无" => 5, "存护" => 6, "丰饶" => 7, "记忆" => 8, _ => 0 };

        public static int GetRelicIndex(string? name) => name switch
        { "头部" => 1, "手部" => 2, "躯干" => 3, "脚部" => 4, "位面球" => 5, "连结绳" => 6, _ => 0 };
    }
}