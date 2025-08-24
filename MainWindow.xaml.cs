using SR_DMG.Source.Example;
using SR_DMG.Source.Material;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SR_DMG
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Initialize();
            InitializeComponent();
            Simple.Role_Skills = Role_Skills;
            Program.Debug();
            Test();
        }

        private void Test()
        {
            Span_Role.Images =
            [
                (BitmapImage)Application.Current.Resources["Icon_Role_Type_0"],
                (BitmapImage)Application.Current.Resources["Icon_Element_0"],
            ];
            Span_Enemy.Images =
            [
                (BitmapImage)Application.Current.Resources["Icon_Element_0"],
                (BitmapImage)Application.Current.Resources["Icon_Element_1"],
                (BitmapImage)Application.Current.Resources["Icon_Element_2"],
                (BitmapImage)Application.Current.Resources["Icon_Element_3"]
            ];
            Role_Skills.Items =
            [
                new Gain()
                {
                    MaxLevel = 15,
                    MaxLayer = 15,
                    Icon = (BitmapImage)Application.Current.Resources["Icon_RES_0"],
                    Title = "逐火救世，行则将至",
                    Text = "对指定敌方单体造成等同于白厄【50%/100%】攻击力的物理属性伤害。",
                    Tags = ["普攻", "战技点 +1", "单体", "削韧 10"],
                    Values = ["削韧 10", "攻击力 50%", "攻击力 100%"]
                },
                new Gain()
                {
                    MaxLevel = 15,
                    MaxLayer = 15,
                    Icon = (BitmapImage)Application.Current.Resources["Icon_RES_1"],
                    Title = "创生•血棘渡亡",
                    Text = "获得2点【毁伤】，对指定敌方单体造成等同于卡厄斯兰那【125%/250%】攻击力的物理属性伤害，对相邻目标造成等同于卡厄斯兰那【37.5%/75%】攻击力的物理属性伤害。",
                    Tags = ["普攻", "扩散", "削韧 30+20*2"],
                    Values = ["削韧 30+20*2", "攻击力 125%", "攻击力 250%", "攻击力 37.5%", "攻击力 75%"]
                },
                new Gain()
                {
                    MaxLevel = 15,
                    MaxLayer = 15,
                    Icon = (BitmapImage) Application.Current.Resources["Icon_RES_2"],
                    Title = "黎明创世，地辟天开",
                    Text = "获得2点【火种】，对指定敌方单体造成等同于白厄【150%/300%】攻击力的物理属性伤害，对相邻目标造成等同于白厄【60%/120%】攻击力的物理属性伤害。",
                    Tags = ["战技", "战技点 -1", "扩散", "削韧 20+10*2"],
                    Values = ["削韧 20+10*2", "攻击力 150%", "攻击力 300%", "攻击力 60%", "攻击力 120%"]
                }
            ];
            Role_Gain.Items = Role_Skills.Items;
            Role_Skills.Select = Role_Skills.Items.First();
            Role_Gain.Select = Role_Gain.Items.First();
        }

        private void Initialize()
        {
            Application.Current.Resources["Icon_RES"] = Application.Current.Resources["Icon_RES_0"];
            Application.Current.Resources["Icon_Element"] = Application.Current.Resources["Icon_Element_0"];
            Application.Current.Resources["Icon_DMG_Boost"] = Application.Current.Resources["Icon_DMG_Boost_0"];
            Application.Current.Resources["Icon_Role_Type"] = Application.Current.Resources["Icon_Role_Type_0"];
        }

    }
}