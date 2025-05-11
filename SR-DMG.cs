using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR_DMG
{
	public partial class SR_DMG : Form
	{
		private bool Saved;
		private string Group;
		private Role Roled = new();
		private float[] DMG = new float[6];
		private readonly bool[] Flags = new bool[10];
		private static readonly Dictionary<string, string> Cmd_Tip = [];
		private readonly List<Role> Roles = [];

		public SR_DMG()
		{
			DoInit();
			InitializeComponent();
			Cob_Simple_Clear();
			Cob_DMG_Equal_Clear();
			Cob_Transform_Clear();
			Cob_Gain_Clear();
		}

		// 初始化
		public void DoInit()
		{
			Group = "";
			Cmd_Tip["保存路径"] = "path";
			Cmd_Tip["登录米游社"] = "login";
			Cmd_Tip["我的角色"] = "uid me";
			Cmd_Tip["实时便筏"] = "note";
			Cmd_Tip["每日签到"] = "sign";
			Cmd_Tip["米游币任务"] = "coin";
			Cmd_Tip["开发文档"] = "about";
			Role.Start(this);
			Avatar.Init();
		}
		// 程序加载及关闭
		private void SR_DMG_Load(object sender, EventArgs e)
		{
			string FilePath = Program.GetPath(Program.AppPath.Config);
			try
			{
				if (File.Exists(FilePath))
				{
					string[] Arr = File.ReadAllLines(FilePath);
					Group = Arr[39][(Arr[39].IndexOf('：') + 1)..];
					if (Group != "")
					{
						Flags[2] = true;
						LoadDate();
					}
					else
					{
						Group_List();
					}
					int i = int.Parse(Arr[40][(Arr[40].IndexOf('：') + 1)..]);
					if (Flags[2] && i > 0 && i < Cob_Simple.Items.Count)
					{
						Cob_Simple.SelectedIndex = i;
					}
					else
					{
						string Tar = "";
						for (i = 0; i < 39; i++)
						{
							Tar += Arr[i][(Arr[i].IndexOf('：') + 1)..] + ',';
						}
						Roled = new Role(Tar);
					}
				}
				else Group_List();
			}
			catch
			{
				Program.TipForm($"文件读取失败\n{FilePath}");
			}
			finally
			{
				LoadRole(Roled);
			}
		}
		private void SR_DMG_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (SaveDate()) e.Cancel = !Program.Message("数据文件未成功保存，是否仍要退出？", "文件被占用");
			string FilePath = Program.GetPath(Program.AppPath.Config, IsCreate: true);
			try
			{
				using StreamWriter Wt = new(FilePath);
				Wt.WriteLine("名称：" + Roled.Name);
				foreach (Role.Property Item in Role.Properties)
				{
					Wt.WriteLine($"{Item.NickName}：{Roled.GetValue(Item.NickName)}");
				}
				Wt.WriteLine("增益：[" + string.Join("-", Roled.Gain) + ']');
				Wt.WriteLine("转化：[" + string.Join("-", Roled.Transform) + ']');
				Wt.WriteLine("当前组：" + Group);
				Wt.Write("历史记录：" + Cob_Simple.SelectedIndex);
			}
			catch
			{
				e.Cancel = !Program.Message("配置文件未成功保存，是否仍要退出？", "文件被占用");
			}
		}
		// 窗口透明
		private void SR_DMG_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) Opacity = 0.1f;
			Ceb_TopMost.Focus();
		}
		private void SR_DMG_MouseUp(object sender, MouseEventArgs e)
		{
			Opacity = 1;
		}
		private void SR_DMG_Activated(object sender, EventArgs e)
		{
			Opacity = 1;
		}
		// 置顶窗口
		private void Ceb_TopMost_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = Ceb_TopMost.Checked;
		}

		// 属性类型及特殊倍率类型
		private void Cob_Break_Type_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Roled.Break_Type != Cob_Break_Type.SelectedIndex)
			{
				Roled.Break_Type = Cob_Break_Type.SelectedIndex;
				DMG_Compute();
			}
		}
		private void Cob_DMG_Equal_Tpye_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Roled.DMG_Equal_Tpye != Cob_DMG_Equal_Tpye.SelectedIndex)
			{
				Roled.DMG_Equal_Tpye = Cob_DMG_Equal_Tpye.SelectedIndex;
				DMG_Compute();
			}
		}
		// 总值与基础值切换
		private void Lab_ATK_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_ATK.Hide();
				Pan_ATK_Base.Show();
			}
		}
		private void Lab_ATK_Base_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_ATK_Base.Hide();
				Pan_ATK.Show();
			}
		}
		private void Lab_HP_DEF_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_HP_DEF.Hide();
				Pan_HP_DEF_Base.Show();
			}
		}
		private void Lab_HP_DEF_Base_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_HP_DEF_Base.Hide();
				Pan_HP_DEF.Show();
			}
		}
		private void Lab_Effect_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_Effect.Hide();
				Pan_Heal_Rate.Show();
			}
		}
		private void Lab_Heal_Rate_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_Heal_Rate.Hide();
				Pan_Effect.Show();
			}
		}
		private void Lab_DMG_Equal_2_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_DMG_Equal_2.Hide();
				Pan_DMG_Equal_3.Show();
			}
		}
		private void Lab_DMG_Equal_3_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_DMG_Equal_3.Hide();
				Pan_DMG_Equal_2.Show();
			}
		}
		private void Lab_SPD_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_SPD.Hide();
				Pan_SPD_Base.Show();
			}
		}
		private void Lab_SPD_Base_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Pan_SPD_Base.Hide();
				Pan_SPD.Show();
			}
		}

		// 伤害计算参数
		private void Tex_DMG_TextChanged(object sender, EventArgs e)
		{
			TextBox Tex = sender as TextBox;
			Roled.SetValue(Tex.Name[4..], ToFloat(Tex.Text));
			if (!Flags[0])
			{
				DMG_Compute();
				string Tar = "[" + Role.GetName(Tex.Name[4..]) + "]";
				if (Tex_Transform.Text.LastIndexOf(Tar) > 0)
				{
					Tex_Transform_TextChanged(null, null);
				}
				if (Tex_Calculator.Text.Contains(Tar))
				{
					Tex_Calculator_TextChanged(null, null);
				}
			}
		}
		// 伤害计算
		private void DMG_Compute()
		{
			if (Flags[0]) return;
			float[][] Rel = Roled.DMG();
			Lab_DMG_1.Text = "不暴击：" + Rel[1][0].ToString("F0");
			Lab_DMG_2.Text = "暴 击 ：" + Rel[1][1].ToString("F0");
			Lab_DMG_3.Text = "期望值：" + Rel[1][2].ToString("F0");
			Lab_Break_1.Text = "超击破：" + Rel[1][3].ToString("F0");
			Lab_Break_2.Text = "击 破 ：" + Rel[1][4].ToString("F0");
			Lab_Break_3.Text = "裂伤纠缠禁锢灼烧冻结触电风化".Substring((int)Roled.Break_Type * 2, 2).Insert(1, " ") + " ：" +
				(Roled.Break_Type == 2 ? Rel[1][5].ToString("0.#") + " %" : Rel[1][5].ToString("F0"));
			Lab_DMG_Equal_Info.Text = Rel[0][1] > 0 ? "+" + Rel[0][1] : "";
			Lab_Area_1.Text = "攻击区：" + Rel[0][0].ToString("F0");
			Lab_Area_2.Text = "总倍率：" + (Roled.ATK > 0 ? Rel[0][0] / Roled.ATK * 100 : 0).ToString("0.#") + " %";
			Lab_Area_3.Text = "防御区：" + (Rel[0][2] * 100).ToString("0.#") + " %";
			Lab_Area_4.Text = "抗性区：" + (Rel[0][3] * 100).ToString("0.#") + " %";
			Lab_Area_5.Text = "暴击区：" + (Rel[0][4] * 100).ToString("0.#") + " %";
			Lab_Area_6.Text = "伤害区：" + (Rel[0][5] * 100).ToString("0.#") + " %";
			Lab_Area_7.Text = "击破区：" + Rel[0][6].ToString("F0");
			Lab_Area_8.Text = "韧性区：" + (Rel[0][7] * 100).ToString("0.#") + " %";
			float Crit = 2 * Roled.CRIT_Rate + Roled.CRIT_DMG;
			Crit = Crit > 400 ? Crit - 100 : 100 + Crit * Crit / 800;
			Lab_MaxCRIT.Text = $"期望极限：{Crit:0.#} %";
			if (Ceb_Note.Checked)
			{
				Improve(Lab_Vary_1, Rel[1][0], DMG[0]);
				Improve(Lab_Vary_2, Rel[1][1], DMG[1]);
				Improve(Lab_Vary_3, Rel[1][2], DMG[2]);
				Improve(Lab_Vary_4, Rel[1][3], DMG[3]);
				Improve(Lab_Vary_5, Rel[1][4], DMG[4]);
				Improve(Lab_Vary_6, Rel[1][5], DMG[5]);
			}
		}

		// 转化表达式
		private void Cob_Transform_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Flags[6]) return;
			if (Cob_Transform.SelectedIndex > 0)
			{
				Flags[6] = true;
				string Str = Cob_Transform.Items[Cob_Transform.SelectedIndex].ToString();
				if (Str.StartsWith('□'))
				{
					Str = "√" + Str[1..];
					Roled.AddTransform(Cob_Transform.SelectedIndex);
				}
				else
				{
					Str = "□" + Str[1..];
					Roled.Transform.Remove(Cob_Transform.SelectedIndex);
				}
				if (Str.Contains('：'))
				{
					SetTexFont(Tex_Name_4, Str[2..Str.IndexOf('：')]);
					Tex_Transform.Text = Str[(Str.IndexOf('：') + 1)..];
				}
				else
				{
					SetTexFont(Tex_Name_4, null);
					Tex_Transform.Text = Str[2..];
				}
				Cob_Transform.Items[Cob_Transform.SelectedIndex] = Str;
				Transform(Str, Roled, true);
				Cob_Transform.DroppedDown = true;
				Flags[6] = false;
			}
		}
		private void Cob_Transform_Clear()
		{
			Cob_Transform.Items.Clear();
			Cob_Transform.Items.Add("表达式示例：[暴击率]:[击破特攻] * 12.5% + 8% < 48%");
			Cob_Transform.SelectedIndex = 0;
		}
		private void Tex_Transform_TextChanged(object sender, EventArgs e)
		{
			if (Flags[4]) return;
			Flags[4] = true;
			if (Tex_Transform.Text.Contains(':'))
			{
				int index = Tex_Transform.SelectionStart;
				string[] Arr = Tex_Transform.Text.Split(':', '<');
				var (Equ, Val, Loc) = Compute(Arr[1], Roled, index - Arr[0].Length - 1);
				if (Arr.Length > 2)
				{
					Tex_Transform.Text = Arr[0] + ':' + Equ + '<' + Arr[2];
				}
				else Tex_Transform.Text = Arr[0] + ':' + Equ;
				Lab_Transform_Info.Text = Val == 0 ? "" : (Role.GetType(Arr[0].Trim('[', ']')) ?
					(Val * 100).ToString("0.#") + "%" : Val.ToString("F0"));
				Tex_Transform.SelectionStart = Arr[0].Length + Loc + 1;
				Tex_Transform.Refresh();
			}
			else
			{
				if (Tex_Transform.Text != "")
				{
					Tex_Transform.Text = "[]:";
					Tex_Transform.SelectionStart = 1;
				}
				Lab_Transform_Info.Text = "";
			}
			Flags[4] = false;
		}
		private void Btn_Save_4_Click(object sender, EventArgs e)
		{
			string[] Arr = Tex_Transform.Text.Split(':', '<');
			if (Arr.Length > 1)
			{
				if (Role.GetName(Arr[0].Trim('[', ']', ' ')) != null)
				{
					if (Lab_Transform_Info.Text != "NaN")
					{
						if (Arr[1].IndexOf('[') > -1)
						{
							string Str = "□ ";
							Arr[1] = Compute(Arr[1], Roled).Equ.TrimEnd('+', '-', '*', '/');
							if (Tex_Name_4.Font.Style == FontStyle.Regular)
							{
								Str += Tex_Name_4.Text + '：' + Arr[0] + ':' + Arr[1];
							}
							else
							{
								Str += Arr[0] + ':' + Arr[1];
							}
							if (Arr.Length > 2)
							{
								if (float.TryParse(Arr[2].TrimEnd('%'), out _))
								{
									Str += "<" + Arr[2].Replace(" ", "");
								}
								else
								{
									Tip("表达式错误：<" + Arr[2]);
									return;
								}
							}
							Save_Info = Flags[6] = true;
							Cob_Insert(Cob_Transform, Str);
							Flags[6] = false;
						}
						else
						{
							Tip("不包含引用数据：" + Arr[1]);
						}
					}
					else
					{
						Tip("表达式错误：" + Arr[1]);
					}
				}
				else
				{
					Tip("目标项错误：" + Arr[0]);
				}
			}
			else
			{
				Tip("表达式不完整");
			}
		}
		private void Btn_Del_4_Click(object sender, EventArgs e)
		{
			if (Cob_Transform.SelectedIndex > 0)
			{
				Save_Info = Flags[1] = Flags[6] = true;
				int Info = Cob_Transform.SelectedIndex;
				string Str = Cob_Transform.Items[Info].ToString();
				if (Str.StartsWith('√'))
				{
					Transform("□" + Str, Roled, true);
					Roled.Transform.Remove(Info);
				}
				Cob_Delete(Cob_Transform, Info);
				Flags[1] = Flags[6] = false;
			}
		}
		private void Transform(string Str, Role role, bool Info)
		{
			string[] Arr = Str[(Str.IndexOf('：') + 1)..].Split(':', '<');
			Arr[0] = Arr[0][(Arr[0].IndexOf('[') + 1)..].Trim(']');
			float Max = Arr.Length > 2 ? Arr[2].Contains('%') ?
				float.Parse(Arr[2][..Arr[2].IndexOf('%')]) * 0.01f
				: float.Parse(Arr[2]) : float.MaxValue;
			float Tar = (float)Compute(Arr[1], role).Val;
			if (Tar > 0)
			{
				if (Tar > Max) Tar = Max;
				if (Role.GetType(Arr[0])) Tar *= 100;
				if (Str.StartsWith('□')) Tar *= -1;
				Tar = (float)Math.Round(Tar, Role.GetType(Arr[0]) ? 1 : 0);
				role.SetValue(Arr[0], role.GetValue(Arr[0]) + Tar);
				if (Info)
				{
					GetControl<TextBox>("Tex_" + Role.GetName(Arr[0])).Text =
						role.GetValue(Arr[0]).ToString();
				}
			}
		}
		public void TranUpdate(string tar, Role role, bool Info)
		{
			if (Flags[0]) return;
			foreach (int id in role.Transform)
			{
				if (id >= Cob_Transform.Items.Count) continue;
				string str = Cob_Transform.Items[id].ToString();
				if (str.LastIndexOf($"[{tar}]") > str.LastIndexOf(':'))
				{
					Transform((Info ? '√' : '□') + str, role, Info && role == Roled);
				}
			}
		}
		// 数值增益
		private void Cob_Gain_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Flags[5]) return;
			if (Cob_Gain.SelectedIndex > 0)
			{
				Flags[5] = true;
				string Str = Cob_Gain.Items[Cob_Gain.SelectedIndex].ToString();
				if (Str.StartsWith('□'))
				{
					Str = "√" + Str[1..];
					Roled.AddGain(Cob_Gain.SelectedIndex);
				}
				else
				{
					Str = "□" + Str[1..];
					Roled.Gain.Remove(Cob_Gain.SelectedIndex);
				}
				if (Str.Contains('：'))
				{
					SetTexFont(Tex_Name_3, Str[2..Str.IndexOf('：')]);
					Tex_Gain.Text = Str[(Str.IndexOf('：') + 1)..];
				}
				else
				{
					SetTexFont(Tex_Name_3, null);
					Tex_Gain.Text = Str[2..];
				}
				Cob_Gain.Items[Cob_Gain.SelectedIndex] = Str;
				Gain(Cob_Gain.Text, Roled, true);
				Cob_Gain.DroppedDown = true;
				Flags[5] = false;
			}
		}
		private void Cob_Gain_Clear()
		{
			Cob_Gain.Items.Clear();
			Cob_Gain.Items.Add("数值增益   示例：[暴击率]:12.5%");
			Cob_Gain.SelectedIndex = 0;
		}
		private void Tex_Gain_TextChanged(object sender, EventArgs e)
		{
			string Str = Tex_Gain.Text;
			if (Str.EndsWith(':') && !Str.StartsWith('['))
			{
				Tex_Gain.Text = "[]:";
				Tex_Gain.SelectionStart = 1;
			}
		}
		private void Btn_Save_3_Click(object sender, EventArgs e)
		{
			string[] Arr = Tex_Gain.Text.Split(':');
			if (Arr.Length > 1)
			{
				if (Role.GetName(Arr[0].Trim('[', ']')) != null)
				{
					if (float.TryParse(Arr[1].TrimEnd('%'), out _))
					{
						Flags[5] = true;
						if (Tex_Name_3.Font.Style == FontStyle.Regular)
						{
							Cob_Insert(Cob_Gain, "□ " + Tex_Name_3.Text + '：' + Arr[0] + ':' + Arr[1].Replace(" ", ""));
						}
						else
						{
							Cob_Insert(Cob_Gain, "□ " + Arr[0] + ':' + Arr[1].Replace(" ", ""));
						}
						Flags[5] = false;
						Save_Info = true;
					}
					else
					{
						Tip("数值错误：" + Arr[1]);
					}
				}
				else
				{
					Tip("目标项错误：" + Arr[0]);
				}
			}
			else
			{
				Tip("表达式不完整");
			}
		}
		private void Btn_Del_3_Click(object sender, EventArgs e)
		{
			if (Cob_Gain.SelectedIndex > 0)
			{
				Save_Info = Flags[1] = Flags[5] = true;
				int Info = Cob_Gain.SelectedIndex;
				string Str = Cob_Gain.Items[Info].ToString();
				if (Str.StartsWith('√'))
				{
					Gain("□" + Str, Roled, true);
					Roled.Gain.Remove(Info);
				}
				Cob_Delete(Cob_Gain, Info);
				Flags[1] = Flags[5] = false;
			}
		}
		private void Gain(string Str, Role role, bool Info)
		{
			string[] Arr = Str[(Str.IndexOf('：') + 1)..].Split(':');
			Arr[0] = Arr[0][(Arr[0].IndexOf('[') + 1)..].Trim(']');
			float Tar = Arr[1].Contains('%') ?
				float.Parse(Arr[1].TrimEnd('%')) * 0.01f
				: float.Parse(Arr[1]);
			if (Role.GetType(Arr[0])) Tar *= 100;
			if (Str.StartsWith('□')) Tar *= -1;
			role.SetValue(Arr[0], role.GetValue(Arr[0]) + Tar);
			if (Info) GetControl<TextBox>("Tex_" + Role.GetName(Arr[0])).Text = role.GetValue(Arr[0]).ToString();
		}

		// 数据切换
		private void Cob_Simple_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Flags[1]) return;
			if (Cob_Simple.SelectedIndex > 0)
			{
				if (Flags[2]) LoadRole(Roles[Cob_Simple.SelectedIndex - 1]);
				else
				{
					Group = Cob_Simple.Text[2..];
					Flags[2] = true;
					LoadDate();
					Cob_Simple.DroppedDown = true;
				}
			}
		}
		private void Btn_Save_1_Click(object sender, EventArgs e)
		{
			if (Flags[2])
			{
				Roles.Add(new Role(Roled));
				Cob_Simple_Update();
				Save_Info = true;
			}
			else
			{
				if (Tex_Name_1.Font.Style == FontStyle.Regular)
				{
					Group = "G-0 " + Tex_Name_1.Text;
					Cob_Simple_Clear();
					Lab_Tip.Text = "";
					Roles.Clear();
					Flags[2] = true;
					Save_Info = true;
				}
				else Tip("组名称不可为空");
			}
		}
		private void Btn_Del_1_Click(object sender, EventArgs e)
		{
			if (Cob_Simple.SelectedIndex > 0)
			{
				Roles.RemoveAt(Cob_Simple.SelectedIndex - 1);
				Cob_Simple.Items.RemoveAt(Cob_Simple.SelectedIndex);
				Cob_Simple_Clear();
				for (int i = 0; i < Roles.Count; i++)
				{
					Cob_Simple.Items.Add(Roles[i].ToSimple(i));
				}
				Save_Info = true;
			}
			else if (Program.Message($"是否删除组：{Group}", "删除确认")) DeleteDate();
		}
		private void Cob_Simple_Clear()
		{
			Cob_Simple.Items.Clear();
			Cob_Simple.Items.Add("载入已保存的数据 （ 当前组：" +
				(Group == "" ? "G-None" : Group) + " ）");
			Cob_Simple.SelectedIndex = 0;
		}
		private void Cob_Simple_Update()
		{
			Role Info = Roles[^1];
			Roles.Sort();
			Flags[1] = true;
			Cob_Simple_Clear();
			for (int i = 0; i < Roles.Count; i++)
			{
				Cob_Simple.Items.Add(Roles[i].ToSimple(i));
			}
			Cob_Simple.SelectedIndex = Roles.IndexOf(Info) + 1;
			Flags[1] = false;
		}

		// 组切换
		private void Btn_Group_Click(object sender, EventArgs e)
		{
			if (Flags[2] || Group == "")
			{
				if (SaveDate())
				{
					Tip("保存数据失败，暂时无法切换"); return;
				}
				else Group_List();
			}
			else
			{
				Flags[2] = true;
				LoadDate();
			}
			Cob_Simple.DroppedDown = true;
		}
		private void Group_List()
		{
			try
			{
				Roles.Clear();
				Roled.Gain.Clear();
				Roled.Transform.Clear();
				Cob_Gain_Clear();
				Cob_Transform_Clear();
				Cob_DMG_Equal_Clear();
				Cob_Simple_Clear();
				Lab_Tip.Text = "未选择组";
				string FilePath = Program.GetPath(Program.AppPath.Save);
				string[] Arr = File.ReadAllLines(FilePath);
				Arr = Array.FindAll(Arr, str => str.StartsWith('G'));
				foreach (string str in Arr)
				{
					Cob_Simple.Items.Add("· " + str);
				}
				Flags[2] = false;
			}
			catch
			{
				Tip("读取组列表失败");
			}
		}

		// 倍率切换
		private void Cob_DMG_Equal_Info_SelectedIndexChanged(object sender, EventArgs e)
		{
			Roled.DMG_Equal_Info = Cob_DMG_Equal_Info.SelectedIndex;
			if (Flags[3]) return;
			if (Cob_DMG_Equal_Info.SelectedIndex > 0)
			{
				string[] Arr = Cob_DMG_Equal_Info.Text.Split('+');
				if (Arr[0].Contains('：'))
				{
					SetTexFont(Tex_Name_2, Arr[0][..Arr[0].IndexOf('：')]);
					Arr[0] = Arr[0][(Arr[0].IndexOf('：') + 1)..];
				}
				bool[] Info = new bool[3];
				for (int i = 0; i < Arr.Length; i++)
				{
					if (Arr[i].Contains('['))
					{
						Info[0] = true;
						Cob_DMG_Equal_Tpye.SelectedIndex = Arr[i][(Arr[i].IndexOf('[') + 1)..].Trim(' ', ']') switch
						{
							"生命值" => 1,
							"防御力" => 2,
							_ => 0,
						};
						Tex_DMG_Equal_2.Text = Arr[i][..Arr[i].IndexOf('%')].Trim(' ');
					}
					else if (Arr[i].Contains('%'))
					{
						Info[1] = true;
						Tex_DMG_Equal_1.Text = Arr[i].Trim(' ', '%');
					}
					else
					{
						Info[2] = true;
						Tex_DMG_Equal_3.Text = Arr[i].Trim(' ');
					}
				}
				if (!Info[0])
				{
					Cob_DMG_Equal_Tpye.SelectedIndex = 0;
					Tex_DMG_Equal_2.Text = "0";
				}
				if (!Info[1])
				{
					Tex_DMG_Equal_1.Text = "0";
				}
				if (!Info[2])
				{
					Tex_DMG_Equal_3.Text = "0";
				}
			}
		}
		private void Cob_DMG_Equal_Clear()
		{
			Cob_DMG_Equal_Info.Items.Clear();
			Cob_DMG_Equal_Info.Items.Add("切换技能倍率（ AEQ ）");
			Cob_DMG_Equal_Info.SelectedIndex = 0;
		}
		private void Btn_Save_2_Click(object sender, EventArgs e)
		{
			Flags[3] = true;
			string Equal = "";
			if (Tex_Name_2.Font.Style == FontStyle.Regular)
			{
				Equal = Tex_Name_2.Text + '：';
			}
			if (Roled.DMG_Equal_1 != 0)
			{
				Equal += Roled.DMG_Equal_1 + "%";
			}
			if (Roled.DMG_Equal_2 != 0 && Cob_DMG_Equal_Tpye.SelectedIndex > 0)
			{
				Equal += (Equal == "" || Equal.EndsWith('：') ? "" : " + ") + Roled.DMG_Equal_2 + "% [" + Cob_DMG_Equal_Tpye.Text + "]";
			}
			if (Roled.DMG_Equal_3 != 0)
			{
				Equal += (Equal == "" || Equal.EndsWith('：') ? "" : " + ") + Roled.DMG_Equal_3;
			}
			if (Equal == "" || Equal.EndsWith('：')) Tip("参数不能都为空");
			else
			{
				Save_Info = true;
				Cob_Insert(Cob_DMG_Equal_Info, Equal);
			}
			Flags[3] = false;
		}
		private void Btn_Del_2_Click(object sender, EventArgs e)
		{
			if (Cob_DMG_Equal_Info.SelectedIndex > 0)
			{
				Save_Info = true;
				Cob_DMG_Equal_Info.Items.RemoveAt(Cob_DMG_Equal_Info.SelectedIndex);
				Cob_DMG_Equal_Info.SelectedIndex = 0;
			}
		}

		// 取消保存
		private void Lab_Tip_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) Save_Info = !Program.Message("是否取消此次保存计划？", "取消保存");
		}
		// 参数相关文本框
		private void Tex_DMG_Enter(object sender, EventArgs e)
		{
			if (Flags[7]) return;
			TextBox Tex = sender as TextBox;
			TGUpdate(Role.GetName(Tex.Name[4..]), Roled, false);
			if (Tex.Text == "0") Tex.Text = "";
		}
		private void Tex_DMG_Leave(object sender, EventArgs e)
		{
			if (Flags[7]) return;
			TextBox Tex = sender as TextBox;
			TGUpdate(Role.GetName(Tex.Name[4..]), Roled, true);
			if (Tex.Text == "") Tex.Text = "0";
		}
		private void TGUpdate(string tar, Role role, bool Info)
		{
			for (int i = 0; i < 2; i++)
			{
				foreach (string str in (i == 0 ? Info : !Info) ? Cob_Gain.Items : Cob_Transform.Items)
				{
					if (str.StartsWith('√') && str.Contains($"[{tar}]:"))
					{
						if (i == 0 ? Info : !Info)
						{
							Gain(Info ? str : '□' + str, role, false);
						}
						else
						{
							Transform(Info ? str : '□' + str, role, false);
						}
					}
				}
			}
			if (role == Roled) LoadRole(role);
		}
		private void Tex_DMG_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) return;
			TextBox Tex = sender as TextBox ?? GetControl<TextBox>("Tex" + (sender as Label).Name[3..]);
			string Str = Tex.Name[4..];
			if (Ceb_Gain.Checked || Ceb_Transform.Checked)
			{
				TextBox Tar = Ceb_Gain.Checked ? Tex_Gain : Tex_Transform;
				int Info = Tar.SelectionStart;
				Flags[4] = true;
				if (Ceb_Transform.Checked && Tar.Text.Contains(':') && Tar.Text.IndexOf(':') < Info)
				{
					if (sender.GetType() == typeof(Label) && Role.GetType(Str))
					{
						Str = "[" + Role.GetName(Str) + "]%";
					}
					else
					{
						Str = "[" + Role.GetName(Str) + "]";
					}
					if (Info > Tar.Text.Length) Tar.Text += Str;
					else Tar.Text = Tar.Text.Insert(Info, Str);
				}
				else
				{
					if (Ceb_Gain.Checked && Role.GetType(Str))
					{
						Str = "[" + Role.GetName(Str) + "]:%";
						Info = -1;
					}
					else
					{
						Str = "[" + Role.GetName(Str) + "]:";
						Info = 0;
					}
					Tar.Text = Str;
				}
				Flags[4] = false;
				if (!Flags[8]) Ceb_Transform.Checked = Ceb_Gain.Checked = false;
				Tar.SelectionStart = Info + Str.Length;
				if (Tar == Tex_Transform) Tex_Transform_TextChanged(null, null);
				Tar.Focus();
			}
			else
			{
				if (Ceb_Cal_1.Checked || Ceb_Cal_2.Checked || Ceb_Cal_3.Checked)
				{
					float Val = ToFloat(Tex_Calculator.Text[(Tex_Calculator.Text.LastIndexOf('=') + 1)..]);
					if (sender.GetType() == typeof(Label))
					{
						if (Role.GetType(Str)) Val *= 100;
					}
					if (Ceb_Cal_1.Checked)
					{
						Roled.SetValue(Str, Roled.GetValue(Str) + Val);
					}
					else if (Ceb_Cal_2.Checked)
					{
						Roled.SetValue(Str, Val);
					}
					else
					{
						Roled.SetValue(Str, Roled.GetValue(Str) - Val);
					}
					if (!Flags[8]) Ceb_Cal_1.Checked = Ceb_Cal_2.Checked = Ceb_Cal_3.Checked = false;
					Tex.Text = Roled.GetValue(Str).ToString();
					Tex_Calculator.Focus();
				}
				else if (Ceb_Cal_4.Checked || Ceb_Cal_5.Checked)
				{
					int Info = Tex_Calculator.SelectionStart;
					bool Pam = sender.GetType() == typeof(Label) && Role.GetType(Str);
					if (Ceb_Cal_4.Checked)
					{
						Str = Tex.Text;
					}
					else
					{
						Str = "[" + Role.GetName(Str) + "]";
					}
					if (Pam) Str += '%';
					Flags[4] = true;
					if (Info > Tex_Calculator.Text.Length)
					{
						Tex_Calculator.Text += Str;
					}
					else
					{
						Tex_Calculator.Text = Tex_Calculator.Text.Insert(Info, Str);
					}
					Flags[4] = false;
					if (!Flags[8]) Ceb_Cal_4.Checked = Ceb_Cal_5.Checked = false;
					Tex_Calculator.SelectionStart = Info + Str.Length;
					Tex_Calculator_TextChanged(null, null);
					Tex_Calculator.Focus();
				}
			}
		}
		private void Lab_Area_MouseUp(object sender, MouseEventArgs e)
		{
			if (Ceb_Cal_4.Checked)
			{
				int Info = Tex_Calculator.SelectionStart;
				string Str = (sender as Label).Text;
				Str = ToFloat(Str) + (Str.EndsWith('%') ? "%" : "");
				Flags[4] = true;
				if (Info > Tex_Calculator.Text.Length)
				{
					Tex_Calculator.Text += Str;
				}
				else
				{
					Tex_Calculator.Text = Tex_Calculator.Text.Insert(Info, Str);
				}
				Flags[4] = false;
				if (!Flags[8]) Ceb_Cal_4.Checked = false;
				Tex_Calculator.SelectionStart = Info + Str.Length;
				Tex_Calculator_TextChanged(null, null);
				Tex_Calculator.Focus();
			}
		}
		// 名称相关文本框
		private void Tex_Name_Enter(object sender, EventArgs e)
		{
			TextBox Tex = sender as TextBox;
			if (Tex.Font.Style == FontStyle.Italic) Tex.Text = "";
			Tex.Font = new Font(Tex.Font, FontStyle.Regular);
		}
		private void Tex_Name_Leave(object sender, EventArgs e)
		{
			TextBox Tex = sender as TextBox;
			if (Tex == Tex_Name_1) Roled.Name = Tex.Text;
			if (Tex.Text == "") SetTexFont(Tex, null);
		}
		// 启用伤害比对
		private void Ceb_Note_CheckedChanged(object sender, EventArgs e)
		{
			if (Ceb_Note.Checked)
			{
				DMG = Roled.DMG()[1];
			}
			else
			{
				Lab_Vary_1.Text = "";
				Lab_Vary_2.Text = "";
				Lab_Vary_3.Text = "";
				Lab_Vary_4.Text = "";
				Lab_Vary_5.Text = "";
				Lab_Vary_6.Text = "";
			}
		}
		private static void Improve(Label Lab, float Dmg1, float Dmg2)
		{
			if ((int)Dmg1 == (int)Dmg2) Lab.Text = "";
			else
			{
				float Imp = (float)Math.Round((Dmg1 / Dmg2 - 1) * 100, 1);
				if (Imp < -0.1)
				{
					Lab.ForeColor = Color.DeepPink;
					Lab.Text = "- " + -Imp + " %";
				}
				else if (Imp > 0.1)
				{
					Lab.ForeColor = Color.LimeGreen;
					Lab.Text = "+ " + Imp + " %";
				}
				else
				{
					Lab.ForeColor = Color.RoyalBlue;
					Lab.Text = "< 0.1 %";
				}
			}
		}
		// 指令处理
		private void Tex_Cmd_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				Command(Tex_Cmd.Text);
				Ceb_TopMost.Focus();
			}
		}
		private void Lab_Cmd_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (Tex_Cmd.Text.Length > 0)
				{
					Command(Tex_Cmd.Text);
				}
				else
				{
					List<string> CmdName = [];
					foreach (KeyValuePair<string, string> Item in Cmd_Tip)
					{
						CmdName.Add(Item.Key);
					}
					int Index = Program.ListForm("指令提示", "指令列表", CmdName);
					if (Index > -1) Command(Cmd_Tip[CmdName[Index]]);
				}
			}
		}
		private async void Command(string Str)
		{
			if (Flags[9])
			{
				Tip("请等待指令执行完成"); return;
			}
			else
			{
				Flags[9] = true;
				string[] Tar = Str.Split(' ');
				Tip($"正在执行：{Str.ToUpper()}");
				switch (Tar[0].ToLower())
				{
					case "path": OpenPath(); break;
					case "login": await Mihomo.Login(); break;
					case "uid": await LoadUID(Tar); break;
					case "note": await Note(); break;
					case "sign": await Sign(); break;
					case "coin": await Coin(); break;
					case "about": Readme(); break;
					default: Tip($"无法识别：{Str.ToUpper()}"); break;
				}
				Flags[9] = false;
			}
		}
		// 获取角色信息
		private async Task LoadUID(string[] Tar)
		{
			if (Tar.Length > 1)
			{
				Avatars Avts;
				if (Flags[2] == false) { Tip("未创建组"); return; }
				if (int.TryParse(Tar[1], out int Uid) && Uid >= 100000000)
				{
					string FilePath = Program.GetPath(Program.AppPath.Data, Tar[1]);
					if (!File.Exists(FilePath) || Tar[0] == "UID")
					{
						Tar[0] = "云端";
						Avts = await Mihomo.Get_Roles(Tar[1]);
						if (Avts == null) return;
					}
					else
					{
						try
						{
							Tar[0] = "本地";
							Avts = JsonSerializer.Deserialize<Avatars>(File.ReadAllText(FilePath));
						}
						catch
						{
							Program.TipForm($"文件读取失败\n{FilePath}"); return;
						}
					}
				}
				else if (Tar[1].Equals("ME", StringComparison.CurrentCultureIgnoreCase))
				{
					Mihomo.Token Token = Mihomo.Get_Token();
					if (Token == null) return;
					string FilePath = Program.GetPath(Program.AppPath.Data, Token.Uid);
					if (!File.Exists(FilePath) || Tar[0] == "UID")
					{
						Tar[0] = "云端";
						string Rel = await Mihomo.Get_Roles(Token);
						if (Rel == null) return;
						Avts = JsonSerializer.Deserialize<Avatars>(Rel);
					}
					else
					{
						try
						{
							Tar[0] = "本地";
							Avts = JsonSerializer.Deserialize<Avatars>(File.ReadAllText(FilePath));
						}
						catch
						{
							Program.TipForm($"文件读取失败\n{FilePath}"); return;
						}
					}
				}
				else { Tip("参数错误：UID"); return; }
				List<string> ListText = [];
				foreach (Avatar Item in Avts.Avatar_List)
				{
					ListText.Add(Item.Name);
					if (Item.Servant.Name == "") continue;
					ListText.Add(Item.Servant.Name);
				}
				int Index = Program.ListForm($"{Tar[0]}数据", "角色列表", ListText);
				if (Index < 0) return;
				Role role = new() { Name = ListText[Index] };
				Avatar Avt = Avts.Avatar_List.Where(Avt => Avt.Name == role.Name || Avt.Servant.Name == role.Name).First();
				if (Avt.Properts.Count > 0)
				{
					role.Break_Type = Cob_Break_Type.Items.IndexOf(Avt.Element.PadLeft(2));
					foreach (Propert Prop in (Avt.Name == role.Name ? Avt.Properts : Avt.Servant.Properts))
					{
						role.SetValue(Prop.Name, float.Parse(Prop.Value.TrimEnd('%')));
					}
					Roles.Add(role);
					Tip($"已载入：{role.Name}");
					Cob_Simple_Update();
					Save_Info = true;
				}
				else Tip("未载入空数据");
			}
			else Tip("缺少参数：UID");
		}
		// 实时便筏
		private async Task Note()
		{
			string Str = await Mihomo.Get_Note();
			if (Str == null) { Tip("请求失败：实时便筏"); return; }
			Program.TipForm(Str, "实时便筏");
		}
		// 每日签到
		private async Task Sign()
		{
			string Str = await Mihomo.DoSign();
			if (Str == null) { Tip("请求失败：每日签到"); return; }
			Program.TipForm(Str, "每日签到");
		}
		// 米游币任务
		private async Task Coin()
		{
			string Str = await Mihomo.DoCoin();
			if (Str == null) { Tip("请求失败：米游币任务"); return; }
			Program.TipForm(Str, "米游币任务");
		}
		// 开发文档
		private static void Readme()
		{
			string FilePath = Program.GetPath(Program.AppPath.Readme);
			try
			{
				if (!File.Exists(FilePath))
				{
					Assembly Ase = Assembly.GetExecutingAssembly();
					using Stream stream = Ase.GetManifestResourceStream("SR_DMG.README.md");
					using FileStream Fs = File.Create(FilePath);
					stream.CopyTo(Fs);
				}
				OpenPath(FilePath);
			}
			catch
			{
				Program.TipForm($"文件保存失败\n{FilePath}");
			}
		}
		// 打开文件
		public static void OpenPath(string FilePath = null, bool Flag = false)
		{
			FilePath ??= Program.GetPath();
			if (Flag || Directory.Exists(FilePath))
			{
				Process.Start("explorer", $"\"{FilePath}\"");
			}
			else
			{
				Process.Start("explorer", $"/select,\"{FilePath}\"");
			}
		}

		// 载入数据
		private void LoadRole(Role role)
		{
			Flags[0] = Flags[5] = Flags[6] = true;
			SetTexFont(Tex_Name_1, role.Name);
			Roled.Name = role.Name;
			foreach (Role.Property Item in Role.Properties)
			{
				TextBox Tex = GetControl<TextBox>($"Tex_{Item.PropertyName}");
				if (Tex == null)
				{
					ComboBox Cob = GetControl<ComboBox>($"Cob_{Item.PropertyName}");
					int Index = (int)role.GetValue(Item.NickName);
					if (Index < Cob.Items.Count) Cob.SelectedIndex = Index;
				}
				else
				{
					Tex.Text = role.GetValue(Item.NickName).ToString();
				}
			}
			Roled.Gain = [.. role.Gain];
			TG_Set(Cob_Gain, role.Gain);
			Roled.Transform = [.. role.Transform];
			TG_Set(Cob_Transform, role.Transform);
			Tex_Calculator_TextChanged(null, null);
			Tex_Transform_TextChanged(null, null);
			Flags[0] = Flags[5] = Flags[6] = false;
			DMG_Compute();
		}
		private static void TG_Set(ComboBox Cob, List<int> list)
		{
			for (int i = 1; i < Cob.Items.Count; i++)
			{
				Cob.Items[i] = "□" + Cob.Items[i].ToString()[1..];
			}
			foreach (int i in list)
			{
				if (i < Cob.Items.Count)
				{
					Cob.Items[i] = "√" + Cob.Items[i].ToString()[1..];
				}
			}
		}
		// 读取数据文件
		private void LoadDate()
		{
			string FilePath = Program.GetPath(Program.AppPath.Save);
			if (File.Exists(FilePath))
			{
				try
				{
					string[] Arr = File.ReadAllLines(FilePath);
					int i = Array.IndexOf(Arr, Group) + 1;
					if (i > 0)
					{
						Lab_Tip.Text = "";
						Cob_Simple_Clear();
						if (i < Arr.Length)
						{
							int n = 0;
							do
							{
								if (Arr[i] == "") continue;
								switch (Arr[i][0])
								{
									case 'G':
										return;
									case 'D':
										Role role = new(Arr[i]);
										Cob_Simple.Items.Add(role.ToSimple(n++));
										Roles.Add(role);
										break;
									case '&':
										Cob_DMG_Equal_Info.Items.Add(Arr[i][2..]);
										break;
									case '#':
										if (Arr[i].IndexOf(':') < Arr[i].LastIndexOf('['))
										{
											Cob_Transform.Items.Add("□" + Arr[i][1..]);
										}
										else
										{
											Cob_Gain.Items.Add("□" + Arr[i][1..]);
										}
										break;
								}
							} while (++i < Arr.Length);
						}
					}
					else
					{
						Group = "";
						Group_List();
					}
				}
				catch
				{
					Tip("读取数据失败");
				}
			}
		}
		// 保存数据文件
		private bool SaveDate()
		{
			if (!Save_Info) return false;
			string FilePath = Program.GetPath(Program.AppPath.Save, IsCreate: true);
			try
			{
				string[] Arr = File.Exists(FilePath) ? File.ReadAllLines(FilePath) : [];
				List<string> Info = [Group[(Group.IndexOf(' ') + 1)..] + '$'];
				for (int i = 0; i < Arr.Length; i++)
				{
					if (Arr[i].StartsWith('G') && Arr[i] != Group)
					{
						Info.Add(Arr[i][(Arr[i].IndexOf(' ') + 1)..] + '$' + i);
					}
				}
				Save_Info = false;
				if (Info.Count > 1) Info.Sort();
				using StreamWriter Writ = new(FilePath);
				for (int i = 0; i < Info.Count; i++)
				{
					string[] Tp = Info[i].Split('$');
					Writ.WriteLine("G-" + (i + 1) + " " + Tp[0]);
					if (int.TryParse(Tp[1], out int k))
					{
						while (++k < Arr.Length)
						{
							if (Arr[k] == "") continue;
							if (Arr[k].StartsWith('G')) break;
							else Writ.WriteLine(Arr[k]);
						}
					}
					else
					{
						Group = "G-" + (i + 1) + " " + Group[(Group.IndexOf(' ') + 1)..];
						for (k = 0; k < Roles.Count; k++)
						{
							Writ.WriteLine(Roles[k].ToString(k));
						}
						for (k = 1; k < Cob_DMG_Equal_Info.Items.Count; k++)
						{
							Writ.WriteLine("& " + Cob_DMG_Equal_Info.Items[k]);
						}
						for (k = 1; k < Cob_Transform.Items.Count; k++)
						{
							Writ.WriteLine("# " + Cob_Transform.Items[k].ToString()[2..]);
						}
						for (k = 1; k < Cob_Gain.Items.Count; k++)
						{
							Writ.WriteLine("# " + Cob_Gain.Items[k].ToString()[2..]);
						}
					}
					if (i < Info.Count - 1) Writ.WriteLine();
				}
			}
			catch
			{
				return true;
			}
			return false;
		}
		// 保存计划标识
		private bool Save_Info
		{
			get
			{
				return Saved;
			}
			set
			{
				Saved = value;
				if (Saved)
				{
					Lab_Tip.Text = "等待保存";
				}
				else
				{
					Lab_Tip.Text = "";
				}
			}
		}
		// 删除整个数据组
		private void DeleteDate()
		{
			try
			{
				string FilePath = Program.GetPath(Program.AppPath.Save);
				string[] Arr = File.ReadAllLines(FilePath);
				using (StreamWriter Writ = new(FilePath, false))
				{
					int seq = 1;
					int end = -1;
					Save_Info = false;
					int tar = Array.IndexOf(Arr, Group);
					for (int i = 0; i < Arr.Length; i++)
					{
						if (Arr[i].StartsWith('G'))
						{
							if (i != tar)
							{
								Writ.WriteLine($"G-{seq++} " + Arr[i][(Arr[i].IndexOf(' ') + 1)..]);
							}
							else end = seq;
						}
						else if (seq != end && (i < Arr.Length - 1 || Arr[i] != ""))
						{
							Writ.WriteLine(Arr[i]);
						}
					}
				}
				Group = "";
				Group_List();
			}
			catch
			{
				Tip("文件被占用，未能删除 " + Group);
			}
		}

		// 表达式计算
		private void Tex_Calculator_TextChanged(object sender, EventArgs e)
		{
			if (Flags[4]) return;
			string Str = Tex_Calculator.Text;
			if (Str.Contains('='))
			{
				Str = Str[..Str.LastIndexOf('=')];
			}
			if (string.IsNullOrWhiteSpace(Str))
			{
				Tex_Calculator.Text = "";
			}
			else
			{
				Flags[4] = true;
				var (Equ, Val, Loc) = Compute(Str, Roled, Tex_Calculator.SelectionStart);
				if (string.IsNullOrWhiteSpace(Equ))
				{
					Tex_Calculator.Text = "";
				}
				else
				{
					Tex_Calculator.Text = Equ + " = " + Val;
					Tex_Calculator.SelectionStart = Loc;
				}
				Flags[4] = false;
			}
		}
		private void Tex_Calculator_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TextBox Tex = sender as TextBox;
				Tex.Text = "";
				Tex.Focus();
			}
		}
		private static (string Equ, double Val, int Loc) Compute(string Str, Role role, int Loc = 0)
		{
			int Ord = 0;
			int _Loc = 0;
			double Rel = 0;
			string Equ = "";
			string Pend = "";
			char[] Sym = new char[3];
			bool[] Info = new bool[5];
			double[] Cou = new double[3];
			for (int i = 0; i < Str.Length; i++)
			{
				if (char.IsNumber(Str[i]))
				{
					Info[0] = true;
					if (Info[3] || Info[4])
					{
						if (i < Loc) _Loc--;
					}
					else Pend += Str[i];
				}
				else
				{
					switch (Str[i])
					{
						case '.':
							if (Pend.Contains('.'))
							{
								if (i < Loc) _Loc--;
							}
							else
							{
								if (Pend == "")
								{
									Pend = "0.";
									if (i <= Loc) _Loc++;
								}
								else
								{
									Pend += ".";
								}
							}
							break;
						case '%':
							if (Info[1])
							{
								if (i < Loc) _Loc--;
							}
							else Info[1] = true;
							break;
						case '(':
							if (Info[0])
							{
								if (!Info[3] && !Info[4])
								{
									Equ += Pend;
								}
								if (Info[1])
								{
									Equ += '%';
									Info[1] = false;
								}
								Info[3] = true;
							}
							else if (Info[1])
							{
								if (i < Loc) _Loc--;
							}
							string[] Bra = Bracket(Str[i..]);
							Pend = Bra[0][1..^1];
							var Cop = Compute(Pend, role, Loc - i - 1);
							Cou[Ord] = Cop.Val;
							if (Info[2])
							{
								Cou[Ord] *= -1;
								Equ += '-';
								Info[2] = false;
							}
							Equ += '(' + Cop.Equ + ')';
							if (Info[3] || Pend == "") Rel = float.NaN;
							Info[0] = Info[3] = true;
							if (i < Loc)
							{
								_Loc -= Loc - i - 1 - Cop.Loc;
							}
							i += Bra[0].Length - int.Parse(Bra[1]) - 1;
							break;
						case '[':
							if (Info[3])
							{
								Info[3] = false;
								Info[4] = true;
							}
							else if (!Info[4])
							{
								if (Info[0])
								{
									Info[4] = true;
									Equ += Pend;
								}
							}
							if (Info[1])
							{
								if (Info[0])
								{
									Equ += '%';
									Info[1] = false;
								}
								else if (i < Loc) _Loc--;
							}
							Pend = Str[(i + 1)..];
							if (Pend.Contains(']'))
							{
								Pend = Pend[..Pend.IndexOf(']')];
								i += Pend.Length;
								if (i + 1 < Loc) _Loc++;
								if (Info[2])
								{
									Equ += '-';
								}
								Equ += '[' + Pend;
								Pend = role.GetValue(Pend).ToString();
								Equ += "]";
							}
							else
							{
								Equ += "[]";
								Pend = "NaN";
							}
							if (Info[4]) Pend = "NaN";
							Info[0] = Info[4] = true;
							break;
						case '+':
						case '-':
						case '*':
						case '/':
							if (Info[0])
							{
								Sym[Ord] = Str[i];
								if (!Info[3])
								{
									Cou[Ord] = double.Parse(Pend);
									if (Equ.Length < Loc + _Loc)
									{
										_Loc -= Mark(Pend, Loc + _Loc - Equ.Length);
									}
									if (Info[2]) Cou[Ord] *= -1;
									if (!Info[4])
									{
										Equ += Cou[Ord];
										if (i == Loc && Pend.Contains('.'))
										{
											Pend = (Pend.TrimEnd('0').EndsWith('.') ? "." : "") +
												"".PadRight(Pend.Length - Pend.TrimEnd('0').Length, '0');
											Equ += Pend;
											_Loc += Pend.Length;
										}
									}
								}
								if (Info[1])
								{
									Equ += '%';
									Cou[Ord] *= 0.01;
								}
								Equ += Sym[Ord];
								if (Ord == 2)
								{
									if ((Sym[0] == '+' || Sym[0] == '-') ==
										(Sym[1] == '+' || Sym[1] == '-'))
									{
										int k = 0;
										do
										{
											switch (Sym[k++])
											{
												case '+':
													Cou[0] += Cou[k];
													break;
												case '-':
													Cou[0] -= Cou[k];
													break;
												case '*':
													Cou[0] *= Cou[k];
													break;
												case '/':
													Cou[0] /= Cou[k];
													break;
											}
										} while (k < 2);
										Sym[0] = Sym[2];
										Sym[1] = char.MinValue;
										Ord--;
									}
									else
									{
										if (Sym[0] == '+' || Sym[0] == '-')
										{
											if (Sym[1] == '*')
											{
												Cou[1] *= Cou[2];
											}
											else
											{
												Cou[1] /= Cou[2];
											}
										}
										else
										{
											if (Sym[0] == '*')
											{
												Cou[0] *= Cou[1];
											}
											else
											{
												Cou[0] /= Cou[1];
											}
											Sym[0] = Sym[1];
											Cou[1] = Cou[2];
										}
										Sym[1] = Sym[2];
									}
									Sym[2] = char.MinValue;
								}
								else
								{
									Ord = (Ord + 1) % 3;
								}
								Array.Clear(Info, 0, Info.Length);
								Pend = "";
							}
							else if (i == Loc)
							{
								Equ += Str[i];
								Rel = float.NaN;
							}
							else if (Str[i] == '-')
							{
								if (Info[2])
								{
									if (i < Loc) _Loc--;
								}
								else Info[2] = true;
							}
							else if (i < Loc) _Loc--;
							break;
						default:
							if (i < Loc) _Loc--;
							break;
					}
				}
			}
			bool Pam = Pend != "";
			if (Pam && Info[0])
			{
				if (!Info[3])
				{
					Cou[Ord] = double.Parse(Pend);
					if (Equ.Length < Loc + _Loc)
					{
						_Loc -= Mark(Pend, Loc + _Loc - Equ.Length);
					}
					if (Info[2]) Cou[Ord] *= -1;
					if (!Info[4])
					{
						Equ += Cou[Ord];
						if (Loc + _Loc == Equ.Length && Pend.Contains('.'))
						{
							Pend = (Pend.TrimEnd('0').EndsWith('.') ? "." : "") +
								"".PadRight(Pend.Length - Pend.TrimEnd('0').Length, '0');
							Equ += Pend;
							_Loc += Pend.Length;
						}
					}
				}
				if (Info[1])
				{
					Equ += '%';
					Cou[Ord] *= 0.01;
				}
			}
			else
			{
				if (Info[1])
				{
					if (0 < Loc) _Loc--;
				}
				if (Info[2])
				{
					Equ += '-';
				}
			}
			if (Pam && Ord == 2)
			{
				if ((Sym[0] == '+' || Sym[0] == '-') ==
					(Sym[1] == '+' || Sym[1] == '-'))
				{
					int k = 0;
					do
					{
						switch (Sym[k++])
						{
							case '+':
								Cou[0] += Cou[k];
								break;
							case '-':
								Cou[0] -= Cou[k];
								break;
							case '*':
								Cou[0] *= Cou[k];
								break;
							case '/':
								Cou[0] /= Cou[k];
								break;
						}
					} while (k < 2);
				}
				else
				{
					if (Sym[0] == '+' || Sym[0] == '-')
					{
						if (Sym[1] == '*')
						{
							Cou[1] *= Cou[2];
						}
						else
						{
							Cou[1] /= Cou[2];
						}
						if (Sym[0] == '+')
						{
							Cou[0] += Cou[1];
						}
						else
						{
							Cou[0] -= Cou[1];
						}
					}
					else
					{
						if (Sym[0] == '*')
						{
							Cou[0] *= Cou[1];
						}
						else
						{
							Cou[0] /= Cou[1];
						}
						if (Sym[1] == '+')
						{
							Cou[0] += Cou[2];
						}
						else
						{
							Cou[0] -= Cou[2];
						}
					}
				}
			}
			else if (Pam || Ord == 2)
			{
				switch (Sym[0])
				{
					case '+':
						Cou[0] += Cou[1];
						break;
					case '-':
						Cou[0] -= Cou[1];
						break;
					case '*':
						Cou[0] *= Cou[1];
						break;
					case '/':
						Cou[0] /= Cou[1];
						break;
				}
			}
			if (!double.IsNaN(Rel)) Rel = Cou[0];
			return (Equ, Rel, Loc + _Loc);
		}
		private static string[] Bracket(string str)
		{
			int info = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '(') info++;
				else if (str[i] == ')')
				{
					if (--info == 0)
					{
						if (i == str.Length - 1)
						{
							return [str, "0"];
						}
						else
						{
							return [str[..(i + 1)], "0"];
						}
					}
					else if (info < 0)
					{
						str = str.Remove(i, 1);
						info++;
					}
				}
			}
			return [str + "".PadLeft(info, ')'), info.ToString()];
		}
		private static int Mark(string Str, int Loc)
		{
			if (Str == "NaN") return 0;
			if (Loc > Str.Length) Loc = Str.Length;
			if (double.TryParse(Str, out double Val))
			{
				string Tar = Val.ToString();
				if (Loc > Tar.Length)
				{
					return Loc - Tar.Length + Str.IndexOf(Tar);
				}
				else
				{
					return Str.IndexOf(Tar);
				}
			}
			else return 0;
		}

		// 提取数值
		private static float ToFloat(string str)
		{
			if (float.TryParse(str, out float result))
			{
				return result;
			}
			else
			{
				string Info = "";
				foreach (char c in str)
				{
					if (Char.IsNumber(c))
					{
						Info += c;
					}
					else if (c == '.')
					{
						if (!Info.Contains('.'))
						{
							Info += c;
						}
					}
				}
				if (float.TryParse(Info, out result))
				{
					if (str.Contains('-'))
					{
						return -result;
					}
					else return result;
				}
				else return 0;
			}
		}
		// 获取控件目标
		private T GetControl<T>(string Str) where T : Control
		{
			return Controls.Find(Str, true).OfType<T>().FirstOrDefault();
		}
		// 插入ComboBox
		private void Cob_Insert(ComboBox Cob, string Str)
		{
			int Info = -1;
			for (int i = 1; i < Cob.Items.Count; i++)
			{
				if (string.Compare(Str[2..], Cob.Items[i].ToString()[2..]) < 0)
				{
					Cob.Items.Insert(i, Str);
					Info = i;
					break;
				}
			}
			if (Info < 0)
			{
				Info = Cob.Items.Count;
				Cob.Items.Add(Str);
			}
			bool SelType = Cob == Cob_Gain;
			foreach (Role role in Roles)
			{
				List<int> Sel = SelType ? role.Gain : role.Transform;
				for (int i = 0; i < Sel.Count; i++)
				{
					if (Sel[i] >= Info) Sel[i]++;
				}
			}
			Cob.SelectedIndex = Info;
		}
		private void Cob_Delete(ComboBox Cob, int Index)
		{
			bool SelType = Cob == Cob_Gain;
			for (int n = 0; n < Roles.Count; n++)
			{
				List<int> Sel = SelType ? Roles[n].Gain : Roles[n].Transform;
				for (int i = 0; i < Sel.Count; i++)
				{
					if (Sel[i] > Index)
					{
						Sel[i]--;
					}
					else if (Sel[i] == Index)
					{
						Sel.RemoveAt(i--);
						if (Index < Cob.Items.Count)
						{
							if (SelType)
							{
								Gain("□" + Cob_Gain.Items[Index].ToString(), Roles[n], false);
							}
							else
							{
								Transform("□" + Cob_Transform.Items[Index].ToString(), Roles[n], false);
							}
							Cob_Simple.Items[n + 1] = Roles[n].ToSimple(n);
						}
					}
				}
			}
			Cob.Items.RemoveAt(Index);
			Cob.SelectedIndex = 0;
		}
		// 字体文本修改
		private static void SetTexFont(TextBox Tex, string Str)
		{
			if (string.IsNullOrWhiteSpace(Str))
			{
				Tex.Font = new Font(Tex.Font, FontStyle.Italic);
				Tex.Text = "输入数据名称";
			}
			else
			{
				Tex.Font = new Font(Tex.Font, FontStyle.Regular);
				Tex.Text = Str;
			}
		}
		private void Btn_MouseClick(object sender, MouseEventArgs e)
		{
			Ceb_TopMost.Focus();
		}
		// 只允许勾选一项
		private void Ceb_Cal_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox Ceb = sender as CheckBox;
			if (Flags[7] = Ceb.Checked)
			{
				if (Ceb != Ceb_Gain) Ceb_Gain.Checked = false;
				if (Ceb != Ceb_Transform) Ceb_Transform.Checked = false;
				if (Ceb != Ceb_Cal_1) Ceb_Cal_1.Checked = false;
				if (Ceb != Ceb_Cal_2) Ceb_Cal_2.Checked = false;
				if (Ceb != Ceb_Cal_3) Ceb_Cal_3.Checked = false;
				if (Ceb != Ceb_Cal_4) Ceb_Cal_4.Checked = false;
				if (Ceb != Ceb_Cal_5) Ceb_Cal_5.Checked = false;
			}
			else
			{
				Ceb.ForeColor = this.ForeColor;
				Flags[8] = false;
			}
		}
		private void Ceb_Cal_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				CheckBox Ceb = sender as CheckBox;
				if (Ceb.Checked)
				{
					if (Flags[8])
					{
						Ceb.ForeColor = this.ForeColor;
					}
					else
					{
						Ceb.ForeColor = Color.RoyalBlue;
					}
					Flags[8] = !Flags[8];
				}
				else
				{
					Ceb.ForeColor = Color.RoyalBlue;
					Ceb.Checked = true;
					Flags[8] = true;
				}
			}
		}
		// 提示信息
		private void Tip(string str)
		{
			Timer.Stop();
			Lab_Hint.Text = str;
			Lab_Hint.Show();
			Timer.Start();
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			Lab_Hint.Hide();
		}
		// 限制字符输入
		private void Tex_Name_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r') Ceb_TopMost.Focus();
		}
		private void Tex_Legal_1_KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;
			if (char.IsNumber(c) || c == 3 || c == 8 || c == 22) return;
			else if (c == '\r') Ceb_TopMost.Focus();
			else e.Handled = true;
		}
		private void Tex_Legal_2_KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;
			if (char.IsNumber(c) || c == '.' || c == 3 || c == 8 || c == 22) return;
			else if (c == '\r') Ceb_TopMost.Focus();
			else e.Handled = true;
		}
		private void Tex_Legal_3_KeyPress(object sender, KeyPressEventArgs e)
		{
			char c = e.KeyChar;
			if (char.IsNumber(c) || c == '.' || c == 3 || c == 8 || c == 22 || c == 45) return;
			else if (c == '\r') Ceb_TopMost.Focus();
			else e.Handled = true;
		}
		private void Tex_Calculator_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case '\r':
					Ceb_TopMost.Focus();
					break;
				case '（':
					e.KeyChar = '(';
					break;
				case '）':
					e.KeyChar = ')';
					break;
				case '【':
					e.KeyChar = '[';
					break;
				case '】':
					e.KeyChar = ']';
					break;
				case '：':
					e.KeyChar = ':';
					break;
				case '。':
					e.KeyChar = '.';
					break;
				case '《':
					e.KeyChar = '<';
					break;
			}
		}

	}
}