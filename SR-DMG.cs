using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace SR_DMG
{
	public partial class SR_DMG : Form
	{
		private bool Saved;
		private string Group;
		private Role Roled = new Role();
		private float[] DMG = new float[6];
		private readonly bool[] Flags = new bool[9];
		public static readonly string[] App_Path = new string[5];
		private readonly List<Role> Roles = new List<Role> { };

		public SR_DMG()
		{
			InitializeComponent();
			Group = "";
			App_Path[0] = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SR-DMG\\";
			App_Path[1] = App_Path[0] + "App.config";
			App_Path[2] = App_Path[0] + "SR-DMG.csv";
			App_Path[3] = App_Path[0] + "Token.json";
			App_Path[4] = App_Path[0] + "Data\\";
			Role.Start(this);
			Cob_Simple_Clear();
			Cob_DMG_Equal_Clear();
			Cob_Transform_Clear();
			Cob_Gain_Clear();
		}

		// 程序加载及关闭
		private void SR_DMG_Load(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists(App_Path[1]))
				{
					string[] Arr = File.ReadAllLines(App_Path[1]);
					Group = Arr[38].Remove(0, Arr[38].IndexOf('=') + 1);
					if (Group != "")
					{
						Flags[2] = true;
						LoadDate();
					}
					else
					{
						Group_List();
					}
					int i = int.Parse(Arr[39].Remove(0, Arr[39].IndexOf("=") + 1));
					if (Flags[2] && i > 0 && i < Cob_Simple.Items.Count)
					{
						Cob_Simple.SelectedIndex = i;
					}
					else
					{
						string Tar = "";
						foreach (string Str in Arr)
						{
							Tar += Str.Remove(0, Str.IndexOf('=') + 1) + ',';
						}
						Roled = new Role(Tar);
					}
				}
				else Group_List();
			}
			catch
			{
				Mihomo.ErorrTip<int>(-1001, $"：{App_Path[1]}");
			}
			finally
			{
				LoadRole(Roled);
			}
		}
		private void SR_DMG_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (SaveDate())
			{
				if (MessageBox.Show("数据文件未成功保存，是否仍要退出？", "文件被占用",
					MessageBoxButtons.OKCancel, MessageBoxIcon.Error) != DialogResult.OK)
					e.Cancel = true;
			}
			string Dirctoy = Path.GetDirectoryName(App_Path[1]);
			if (!Directory.Exists(Dirctoy))
			{
				Directory.CreateDirectory(Dirctoy);
			}
			try
			{
				using (StreamWriter Writ = new StreamWriter(App_Path[1]))
				{
					Writ.WriteLine("Name=" + Roled.Name);
					Writ.WriteLine("ATK=" + Roled.ATK);
					Writ.WriteLine("HP=" + Roled.HP);
					Writ.WriteLine("DEF=" + Roled.DEF);
					Writ.WriteLine("ATK_Base=" + Roled.ATK_Base);
					Writ.WriteLine("HP_Base=" + Roled.HP_Base);
					Writ.WriteLine("DEF_Base=" + Roled.DEF_Base);
					Writ.WriteLine("DMG_Equal_1=" + Roled.DMG_Equal_1);
					Writ.WriteLine("DMG_Equal_2=" + Roled.DMG_Equal_2);
					Writ.WriteLine("DMG_Equal_3=" + Roled.DMG_Equal_3);
					Writ.WriteLine("DMG_Equal_4=" + Roled.DMG_Equal_4);
					Writ.WriteLine("DMG_Equal_Tpye=" + Roled.DMG_Equal_Tpye);
					Writ.WriteLine("DMG_Equal_Info=" + Roled.DMG_Equal_Info);
					Writ.WriteLine("CRIT_Rate=" + Roled.CRIT_Rate);
					Writ.WriteLine("CRIT_DMG=" + Roled.CRIT_DMG);
					Writ.WriteLine("Character_Level=" + Roled.Character_Level);
					Writ.WriteLine("Enemy_Level=" + Roled.Enemy_Level);
					Writ.WriteLine("DEF_Reduced=" + Roled.DEF_Reduced);
					Writ.WriteLine("DEF_Ignores=" + Roled.DEF_Ignores);
					Writ.WriteLine("RES_Boost=" + Roled.RES_Boost);
					Writ.WriteLine("RES_PEN=" + Roled.RES_PEN);
					Writ.WriteLine("DMG_Boost=" + Roled.DMG_Boost);
					Writ.WriteLine("DMG_Taken=" + Roled.DMG_Taken);
					Writ.WriteLine("DMG_Reduction=" + Roled.DMG_Reduction);
					Writ.WriteLine("Break_Equal=" + Roled.Break_Equal);
					Writ.WriteLine("Break_Effect=" + Roled.Break_Effect);
					Writ.WriteLine("Break_Efficiency=" + Roled.Break_Efficiency);
					Writ.WriteLine("Break_Boost=" + Roled.Break_Boost);
					Writ.WriteLine("Break_Type=" + Roled.Break_Type);
					Writ.WriteLine("SPD=" + Roled.SPD);
					Writ.WriteLine("SPD_Base=" + Roled.SPD_Base);
					Writ.WriteLine("Toughness=" + Roled.Toughness);
					Writ.WriteLine("Toughness_Reduction=" + Roled.Toughness_Reduction);
					Writ.WriteLine("Effect_Hit_Rate=" + Roled.Effect_Hit_Rate);
					Writ.WriteLine("Effect_RES=" + Roled.Effect_RES);
					Writ.WriteLine("Energy_Regeneration_Rate=" + Roled.Energy_Regeneration_Rate);
					Writ.WriteLine("Gain=[" + string.Join("-", Roled.Gain) + ']');
					Writ.WriteLine("Transform=[" + string.Join("-", Roled.Transform) + ']');
					Writ.WriteLine("Group=" + Group);
					Writ.Write("History_Info=" + Cob_Simple.SelectedIndex);
				}
			}
			catch
			{
				if (MessageBox.Show("配置文件未成功保存，是否仍要退出？", "文件被占用",
					MessageBoxButtons.OKCancel, MessageBoxIcon.Error) != DialogResult.OK)
					e.Cancel = true;
			}

		}
		// 窗口透明
		private void SR_DMG_MouseDown(object sender, MouseEventArgs e)
		{
			Ceb_TopMost.Focus();
			if (e.Button == MouseButtons.Right)
			{
				Opacity = 0.1f;
			}
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
			Roled.SetValue(Tex.Name.Remove(0, 4), ToFloat(Tex.Text));
			if (!Flags[0])
			{
				DMG_Compute();
				string Tar = "[" + Role.GetName(Tex.Name.Remove(0, 4)) + "]";
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
				if (Str.StartsWith("□"))
				{
					Str = "√" + Str.Remove(0, 1);
					Roled.AddTransform(Cob_Transform.SelectedIndex);
				}
				else
				{
					Str = "□" + Str.Remove(0, 1);
					Roled.Transform.Remove(Cob_Transform.SelectedIndex);
				}
				if (Str.Contains("："))
				{
					SetTexFont(Tex_Name_4, Str.Substring(2, Str.IndexOf('：') - 2));
					Tex_Transform.Text = Str.Remove(0, Str.IndexOf('：') + 1);
				}
				else
				{
					SetTexFont(Tex_Name_4, null);
					Tex_Transform.Text = Str.Remove(0, 2);
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
			if (Tex_Transform.Text.Contains(":"))
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
					(Val * 100).ToString("0.#") + "%" : Val.ToString());
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
							int Info = Cob_Transform.SelectedIndex;
							foreach (Role role in Roles)
							{
								role.Transform.ForEach(value =>
								{
									if (value >= Info)
									{
										value++;
									}
								});
							}
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
				if (Str.StartsWith("√"))
				{
					Transform("□" + Str, Roled, true);
					Roled.Transform.Remove(Info);
				}
				for (int i = 0; i < Roles.Count; i++)
				{
					Roles[i].Transform.ForEach(value =>
					{
						if (value > Info)
						{
							value--;
						}
						else if (value == Info && Info < Cob_Transform.Items.Count)
						{
							Transform("□" + Cob_Transform.Items[Info].ToString(), Roles[i], false);
							Cob_Simple.Items[i + 1] = Roles[i].ToSimple(i);
						}
					});
					Roles[i].Transform.Remove(Info);
				}
				Cob_Transform.Items.RemoveAt(Info);
				Cob_Transform.SelectedIndex = 0;
				Flags[1] = Flags[6] = false;
			}
		}
		private void Transform(string Str, Role role, bool Info)
		{
			string[] Arr = Str.Remove(0, Str.IndexOf('：') + 1).Split(':', '<');
			Arr[0] = Arr[0].Remove(0, Arr[0].IndexOf('[') + 1).Trim(']');
			float Max = Arr.Length > 2 ? Arr[2].Contains("%") ?
				float.Parse(Arr[2].Remove(Arr[2].IndexOf('%'))) * 0.01f
				: float.Parse(Arr[2]) : float.MaxValue;
			float Tar = (float)Compute(Arr[1], role).Val;
			if (Tar > Max) Tar = Max;
			if (Role.GetType(Arr[0])) Tar *= 100;
			if (Str.StartsWith("□")) Tar *= -1;
			role.SetValue(Arr[0], role.GetValue(Arr[0]) + Tar);
			if (Info)
			{
				GetTextBox("Tex_" + Role.GetName(Arr[0])).Text =
					role.GetValue(Arr[0]).ToString();
			}
		}
		public void TranUpdate(string tar, Role role, bool Info)
		{
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
				if (Str.StartsWith("□"))
				{
					Str = "√" + Str.Remove(0, 1);
					Roled.AddGain(Cob_Gain.SelectedIndex);
				}
				else
				{
					Str = "□" + Str.Remove(0, 1);
					Roled.Gain.Remove(Cob_Gain.SelectedIndex);
				}
				if (Str.Contains("："))
				{
					SetTexFont(Tex_Name_3, Str.Substring(2, Str.IndexOf('：') - 2));
					Tex_Gain.Text = Str.Remove(0, Str.IndexOf('：') + 1);
				}
				else
				{
					SetTexFont(Tex_Name_3, null);
					Tex_Gain.Text = Str.Remove(0, 2);
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
			if (Tex_Gain.Text.IndexOf(':') == -1)
			{
				if (Tex_Gain.Text != "")
				{
					Tex_Gain.Text = "[]:";
					Tex_Gain.SelectionStart = 1;
				}
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
						int Info = Cob_Gain.SelectedIndex;
						foreach (Role role in Roles)
						{
							role.Gain.ForEach(value =>
							{
								if (value >= Info)
								{
									value++;
								}
							});
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
				if (Str.StartsWith("√"))
				{
					Gain("□" + Str, Roled, true);
					Roled.Gain.Remove(Info);
				}
				for (int i = 0; i < Roles.Count; i++)
				{
					Roles[i].Gain.ForEach(value =>
					{
						if (value > Info)
						{
							value--;
						}
						else if (value == Info && Info < Cob_Gain.Items.Count)
						{
							Gain("□" + Cob_Gain.Items[Info].ToString(), Roles[i], false);
							Cob_Simple.Items[i + 1] = Roles[i].ToSimple(i);
						}
					});
					Roles[i].Gain.Remove(Info);
				}
				Cob_Gain.Items.RemoveAt(Info);
				Cob_Gain.SelectedIndex = 0;
				Flags[1] = Flags[5] = false;
			}
		}
		private void Gain(string Str, Role role, bool Info)
		{
			string[] Arr = Str.Remove(0, Str.IndexOf('：') + 1).Split(':');
			Arr[0] = Arr[0].Remove(0, Arr[0].IndexOf('[') + 1).Trim(']');
			float Tar = Arr[1].Contains("%") ?
				float.Parse(Arr[1].TrimEnd('%')) * 0.01f
				: float.Parse(Arr[1]);
			if (Role.GetType(Arr[0])) Tar *= 100;
			if (Str.StartsWith("□")) Tar *= -1;
			role.SetValue(Arr[0], role.GetValue(Arr[0]) + Tar);
			if (Info)
			{
				GetTextBox("Tex_" + Role.GetName(Arr[0])).Text =
					role.GetValue(Arr[0]).ToString();
			}
		}

		// 数据切换
		private void Cob_Simple_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Flags[1]) return;
			if (Cob_Simple.SelectedIndex > 0)
			{
				if (Flags[2])
				{
					LoadRole(Roles[Cob_Simple.SelectedIndex - 1]);
				}
				else
				{
					Group = Cob_Simple.Text.Remove(0, 2);
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
				else
				{
					Tip("组名称不可为空");
				}
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
			else
			{
				if (MessageBox.Show("是否删除组：" + Group, "删除确认",
					MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					DeleteDate();
				}
			}
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
			Role Info = Roles[Roles.Count - 1];
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
					Tip("保存数据失败，暂时无法切换");
					return;
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
				if (File.Exists(App_Path[2]))
				{
					string[] Arr = File.ReadAllLines(App_Path[2]);
					Arr = Array.FindAll(Arr, str => str.StartsWith("G"));
					foreach (string str in Arr)
					{
						Cob_Simple.Items.Add("· " + str);
					}
				}
				Flags[2] = false;
			}
			catch
			{
				Tip("读取组列表失败");
			}
		}

		// 倍率切换
		private void Cob_DMG_Equal_SelectedIndexChanged(object sender, EventArgs e)
		{
			Roled.DMG_Equal_Info = Cob_DMG_Equal.SelectedIndex;
			if (Flags[3]) return;
			if (Cob_DMG_Equal.SelectedIndex > 0)
			{
				string[] Arr = Cob_DMG_Equal.Text.Split('+');
				if (Arr[0].Contains("："))
				{
					SetTexFont(Tex_Name_2, Arr[0].Remove(Arr[0].IndexOf("：")));
					Arr[0] = Arr[0].Remove(0, Arr[0].IndexOf("：") + 1);
				}
				bool[] Info = new bool[3];
				for (int i = 0; i < Arr.Length; i++)
				{
					if (Arr[i].Contains("["))
					{
						Info[0] = true;
						switch (Arr[i].Remove(0, Arr[i].IndexOf("[") + 1).Trim(' ', ']'))
						{
							case "生命值":
								Cob_DMG_Equal_Tpye.SelectedIndex = 1;
								break;
							case "防御力":
								Cob_DMG_Equal_Tpye.SelectedIndex = 2;
								break;
							default:
								Cob_DMG_Equal_Tpye.SelectedIndex = 0;
								break;
						}
						Tex_DMG_Equal_2.Text = Arr[i].Remove(Arr[i].IndexOf("%")).Trim(' ');
					}
					else if (Arr[i].Contains("%"))
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
			Cob_DMG_Equal.Items.Clear();
			Cob_DMG_Equal.Items.Add("切换技能倍率（ AEQ ）");
			Cob_DMG_Equal.SelectedIndex = 0;
		}
		private void Btn_Save_2_Click(object sender, EventArgs e)
		{
			Flags[3] = true;
			string Equal = "";
			if (Tex_Name_2.Font.Style == FontStyle.Regular)
			{
				Equal = Tex_Name_2.Text + "：";
			}
			if (Roled.DMG_Equal_1 != 0)
			{
				Equal += Roled.DMG_Equal_1 + "%";
			}
			if (Roled.DMG_Equal_2 != 0 && Cob_DMG_Equal_Tpye.SelectedIndex > 0)
			{
				Equal += (Equal == "" || Equal.EndsWith("：") ? "" : " + ") + Roled.DMG_Equal_2 + "% [" + Cob_DMG_Equal_Tpye.Text + "]";
			}
			if (Roled.DMG_Equal_3 != 0)
			{
				Equal += (Equal == "" || Equal.EndsWith("：") ? "" : " + ") + Roled.DMG_Equal_3;
			}
			if (Equal == "" || Equal.EndsWith("："))
			{
				Tip("参数不能都为空");
			}
			else
			{
				Save_Info = true;
				Cob_Insert(Cob_DMG_Equal, Equal);
			}
			Flags[3] = false;
		}
		private void Btn_Del_2_Click(object sender, EventArgs e)
		{
			if (Cob_DMG_Equal.SelectedIndex > 0)
			{
				Save_Info = true;
				Cob_DMG_Equal.Items.RemoveAt(Cob_DMG_Equal.SelectedIndex);
				Cob_DMG_Equal.SelectedIndex = 0;
			}
		}

		// 取消保存
		private void Lab_Tip_Click(object sender, EventArgs e)
		{
			if (Save_Info)
			{
				if (MessageBox.Show("是否取消此次保存计划？", "取消保存",
					MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
				{
					Save_Info = false;
				}
			}
		}
		// 参数相关文本框
		private void Tex_DMG_Enter(object sender, EventArgs e)
		{
			if (Flags[7]) return;
			TextBox Tex = sender as TextBox;
			TGUpdate(Role.GetName(Tex.Name.Remove(0, 4)), Roled, false);
			if (Tex.Text == "0")
			{
				Tex.Text = "";
			}
		}
		private void Tex_DMG_Leave(object sender, EventArgs e)
		{
			if (Flags[7]) return;
			TextBox Tex = sender as TextBox;
			TGUpdate(Role.GetName(Tex.Name.Remove(0, 4)), Roled, true);
			if (Tex.Text == "")
			{
				Tex.Text = "0";
			}
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
			TextBox Tex = sender as TextBox ?? GetTextBox("Tex" + (sender as Label).Name.Remove(0, 3));
			string Str = Tex.Name.Remove(0, 4);
			if (Ceb_Gain.Checked || Ceb_Transform.Checked)
			{
				TextBox Tar = Ceb_Gain.Checked ? Tex_Gain : Tex_Transform;
				int Info = Tar.SelectionStart;
				Flags[4] = true;
				if (Ceb_Transform.Checked && Tar.Text.Contains(":") && Tar.Text.IndexOf(":") < Info)
				{
					if (sender.GetType() == typeof(Label) && Role.GetType(Str))
					{
						Str = "[" + Role.GetName(Str) + "]%";
					}
					else
					{
						Str = "[" + Role.GetName(Str) + "]";
					}
					if (Info > Tar.Text.Length)
					{
						Tar.Text += Str;
					}
					else
					{
						Tar.Text = Tar.Text.Insert(Info, Str);
					}
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
				if (Tar == Tex_Transform)
				{
					Tex_Transform_TextChanged(null, null);
				}
				Tar.Focus();
			}
			else
			{
				if (Ceb_Cal_1.Checked || Ceb_Cal_2.Checked || Ceb_Cal_3.Checked)
				{
					float Val = ToFloat(Tex_Calculator.Text.Remove(0, Tex_Calculator.Text.LastIndexOf("=") + 1));
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
				Str = ToFloat(Str) + (Str.EndsWith("%") ? "%" : "");
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
			if (Tex.Font.Style == FontStyle.Italic)
			{
				Tex.Text = "";
			}
			Tex.Font = new Font(Tex.Font, FontStyle.Regular);
		}
		private void Tex_Name_Leave(object sender, EventArgs e)
		{
			TextBox Tex = sender as TextBox;
			if (Tex == Tex_Name_1)
			{
				Roled.Name = Tex.Text;
			}
			if (Tex.Text == "")
			{
				SetTexFont(Tex, null);
			}
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
		private void Improve(Label Lab, float Dmg1, float Dmg2)
		{
			if ((int)Dmg1 == (int)Dmg2)
			{
				Lab.Text = "";
			}
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
				Command(Tex_Cmd.Text);
			}
		}
		private void Command(string str)
		{
			string[] Tar = str.Split(' ');
			switch (Tar[0].ToLower())
			{
				case "path":
					Open(App_Path[0], false);
					break;
				case "uid":
					LoadUID(Tar);
					break;
				case "star":
					Transition();
					break;
				case "login":
					Mihomo.Login();
					break;
				default:
					Tip("无法识别：" + str);
					break;
			}
		}
		// 获取 角色信息
		private async void LoadUID(string[] Tar)
		{
			if (Tar.Length > 1)
			{
				if (Flags[2] == false) { Tip("未创建组"); return; }
				Avatars Avts;
				string path = $"{App_Path[4]}{Tar[1]}.json";
				if (int.TryParse(Tar[1], out int Uid) && Uid >= 100000000)
				{
					if (!File.Exists(path) || Tar[0] == "UID")
					{
						string Rel = await Mihomo.HttpGet($"https://api.mihomo.me/sr_info_parsed/{Uid}?lang=cn");
						if (Rel == null) { Mihomo.ErorrTip<int>(-1005); return; }
						else if (Rel.StartsWith("Internal")) { Mihomo.ErorrTip<int>(-105); return; }
						using (JsonDocument Doc = JsonDocument.Parse(Rel))
						{
							Avts = new Avatars
							{
								UID = Doc.RootElement.GetProperty("player").GetProperty("uid").GetString(),
								Avatar_List = new List<Avatar>()
							};
							foreach (JsonElement Cat in Doc.RootElement.GetProperty("characters").EnumerateArray())
							{
								Avatar Avt = new Avatar
								{
									Name = Cat.GetProperty("name").GetString(),
									Level = Cat.GetProperty("level").GetInt32(),
									Element = Cat.GetProperty("element").GetProperty("name").GetString(),
									Rank = Cat.GetProperty("rank").GetInt32(),
									Properts = new List<Propert>(),
									Servant = new Servant
									{
										Name = "",
										Properts = new List<Propert>()
									}
								};
								float[] Vals = new float[6];
								foreach (JsonElement Atr in Cat.GetProperty("attributes").EnumerateArray())
								{
									switch (Atr.GetProperty("name").GetString())
									{
										case "生命值":
											Vals[0] = Atr.GetProperty("value").GetSingle();
											Avt.Properts.Add(new Propert
											{
												Name = "基础生命",
												Value = $"{(int)Vals[0]}"
											});
											break;
										case "攻击力":
											Vals[1] = Atr.GetProperty("value").GetSingle();
											Avt.Properts.Add(new Propert
											{
												Name = "基础攻击",
												Value = $"{(int)Vals[1]}"
											});
											break;
										case "防御力":
											Vals[2] = Atr.GetProperty("value").GetSingle();
											Avt.Properts.Add(new Propert
											{
												Name = "基础防御",
												Value = $"{(int)Vals[2]}"
											});
											break;
										case "速度":
											Vals[3] = Atr.GetProperty("value").GetSingle();
											Avt.Properts.Add(new Propert
											{
												Name = "基础速度",
												Value = $"{(int)Vals[3]}"
											});
											break;
										case "暴击率":
											Vals[4] = Atr.GetProperty("value").GetSingle() * 100;
											break;
										case "暴击伤害":
											Vals[5] = Atr.GetProperty("value").GetSingle() * 100;
											break;
									}
								}
								foreach (JsonElement Adt in Cat.GetProperty("additions").EnumerateArray())
								{
									switch (Adt.GetProperty("name").GetString())
									{
										case "生命值":
											Vals[0] += Adt.GetProperty("value").GetSingle();
											break;
										case "攻击力":
											Vals[1] += Adt.GetProperty("value").GetSingle();
											break;
										case "防御力":
											Vals[2] += Adt.GetProperty("value").GetSingle();
											break;
										case "速度":
											Vals[3] += Adt.GetProperty("value").GetSingle();
											break;
										case "暴击率":
											Vals[4] += Adt.GetProperty("value").GetSingle() * 100;
											break;
										case "暴击伤害":
											Vals[5] += Adt.GetProperty("value").GetSingle() * 100;
											break;
										case "能量恢复效率":
											Avt.Properts.Add(new Propert
											{
												Name = "充能效率",
												Value = $"{(Adt.GetProperty("value").GetSingle() * 100 + 100).ToString("0.#")}%"
											});
											break;
										case "效果命中":
											Avt.Properts.Add(new Propert
											{
												Name = "效果命中",
												Value = $"{(Adt.GetProperty("value").GetSingle() * 100).ToString("0.#")}%"
											});
											break;
										case "效果抵抗":
											Avt.Properts.Add(new Propert
											{
												Name = "效果抵抗",
												Value = $"{(Adt.GetProperty("value").GetSingle() * 100).ToString("0.#")}%"
											});
											break;
										case "击破特攻":
											Avt.Properts.Add(new Propert
											{
												Name = "击破特攻",
												Value = $"{(Adt.GetProperty("value").GetSingle() * 100).ToString("0.#")}%"
											});
											break;
										default:
											if (Adt.GetProperty("name").GetString().StartsWith(Avt.Element))
											{
												Avt.Properts.Add(new Propert
												{
													Name = "伤害提高",
													Value = $"{(Adt.GetProperty("value").GetSingle() * 100).ToString("0.#")}%"
												});
											}
											break;
									}
								}
								Avt.Properts.Add(new Propert
								{
									Name = "生命值",
									Value = $"{(int)Vals[0]}"
								});
								Avt.Properts.Add(new Propert
								{
									Name = "攻击力",
									Value = $"{(int)Vals[1]}"
								}); Avt.Properts.Add(new Propert
								{
									Name = "防御力",
									Value = $"{(int)Vals[2]}"
								}); Avt.Properts.Add(new Propert
								{
									Name = "速度",
									Value = $"{(int)Vals[3]}"
								});
								Avt.Properts.Add(new Propert
								{
									Name = "暴击率",
									Value = $"{Vals[4].ToString("0.#")}%"
								});
								Avt.Properts.Add(new Propert
								{
									Name = "暴击伤害",
									Value = $"{Vals[5].ToString("0.#")}%"
								});
								Avts.Avatar_List.Add(Avt);
							}
							try
							{
								File.WriteAllText(path, JsonSerializer.Serialize(Avts, Mihomo.JsonSopt()));
							}
							catch
							{
								Mihomo.ErorrTip<int>(-1002, $"：{path}"); return;
							}
						}
					}
					else
					{
						try
						{
							Avts = JsonSerializer.Deserialize<Avatars>(File.ReadAllText(path));
						}
						catch
						{
							Mihomo.ErorrTip<int>(-1001, $"：{path}"); return;
						}
					}
				}
				else if (Tar[1].ToUpper() == "ME")
				{
					if (File.Exists(App_Path[3]))
					{
						try
						{
							using (FileStream Fs = new FileStream(App_Path[3], FileMode.Open, FileAccess.ReadWrite))
							{
								Token Token;
								using (StreamReader Sr = new StreamReader(Fs))
								{
									Token = JsonSerializer.Deserialize<Token>(Sr.ReadToEnd());
								}
								if (new FileInfo(App_Path[3]).LastWriteTime - DateTime.Now > TimeSpan.FromDays(7))
								{
									Token.device_fp = await Mihomo.Get_Fp();
									using (StreamWriter Sw = new StreamWriter(Fs))
									{
										Sw.Write(JsonSerializer.Serialize(Token, Mihomo.JsonSopt()));
									}
								}
								path = $"{App_Path[4]}{Token.uid}.json";
								if (!File.Exists(path) || Tar[0] == "UID")
								{
									string Rel = await Mihomo.Get_Roles(Token);
									if (Rel == null) return;
									Avts = JsonSerializer.Deserialize<Avatars>(Rel);
								}
								else
								{
									try
									{
										Avts = JsonSerializer.Deserialize<Avatars>(File.ReadAllText(path));
									}
									catch
									{
										Mihomo.ErorrTip<int>(-1002, $"：{path}"); return;
									}
								}
							}
						}
						catch
						{
							Mihomo.ErorrTip<int>(-1001, $"：{App_Path[3]}");
							return;
						}
					}
					else { Tip("请先登录"); return; }
				}
				else { Tip("参数错误：UID"); return; };
				Form Form = new Form
				{
					Text = "角色列表",
					Size = new Size(300, 300),
					FormBorderStyle = FormBorderStyle.FixedSingle,
					StartPosition = FormStartPosition.CenterScreen,
					MaximizeBox = false
				};
				ListView Lew = new ListView
				{
					Size = new Size(260, 260),
					Location = new Point(10, 10),
					Font = new Font(this.Font.Name, 15),
					BackColor = Form.BackColor,
					ForeColor = Form.ForeColor,
					FullRowSelect = true,
					View = View.Details,
					GridLines = false
				};
				Lew.Columns.Add($"角色列表", 245, HorizontalAlignment.Center);
				foreach (Avatar Avt in Avts.Avatar_List)
				{
					Lew.Items.Add(Avt.Name);
					if (Avt.Servant.Name == "") continue;
					Lew.Items.Add(Avt.Servant.Name);
				}
				Lew.SelectedIndexChanged += (sender, e) =>
				{
					Role role = new Role();
					role.Name = Lew.SelectedItems[0].Text;
					Avatar Avt = Avts.Avatar_List.Where(Avt
						=> Avt.Name == role.Name || Avt.Servant.Name == role.Name).First();
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
					Form.Close();
				};
				Form.Controls.Add(Lew);
				Form.ShowDialog();
			}
			else Tip("缺少参数：UID");
		}
		// 获取 跃迁记录
		private async void Transition()
		{
			string Url = await Mihomo.Get_GachaLog();
			if (Url == null) return;
			Clipboard.SetText(Url);
			Tip("已复制跃迁记录URL");
		}
		// 打开文件
		public static void Open(string path, bool flag)
		{
			if (flag || Directory.Exists(path))
			{
				Process.Start("explorer", $"\"{path}\"");
			}
			else
			{
				Process.Start("explorer", $"/select,\"{path}\"");
			}
		}

		// 载入数据
		private void LoadRole(Role role)
		{
			Flags[0] = true;
			SetTexFont(Tex_Name_1, role.Name);
			Roled.Name = role.Name;
			Tex_ATK.Text = role.ATK.ToString();
			Tex_HP.Text = role.HP.ToString();
			Tex_DEF.Text = role.DEF.ToString();
			Tex_ATK_Base.Text = role.ATK_Base.ToString();
			Tex_HP_Base.Text = role.HP_Base.ToString();
			Tex_DEF_Base.Text = role.DEF_Base.ToString();
			Tex_DMG_Equal_1.Text = role.DMG_Equal_1.ToString();
			Tex_DMG_Equal_2.Text = role.DMG_Equal_2.ToString();
			Tex_DMG_Equal_3.Text = role.DMG_Equal_3.ToString();
			Tex_DMG_Equal_4.Text = role.DMG_Equal_4.ToString();
			Cob_DMG_Equal.SelectedIndex = (int)role.DMG_Equal_Info;
			Cob_DMG_Equal_Tpye.SelectedIndex = (int)role.DMG_Equal_Tpye;
			Tex_CRIT_Rate.Text = role.CRIT_Rate.ToString();
			Tex_CRIT_DMG.Text = role.CRIT_DMG.ToString();
			Tex_Character_Level.Text = role.Character_Level.ToString();
			Tex_Enemy_Level.Text = role.Enemy_Level.ToString();
			Tex_DEF_Reduced.Text = role.DEF_Reduced.ToString();
			Tex_DEF_Ignores.Text = role.DEF_Ignores.ToString();
			Tex_RES_Boost.Text = role.RES_Boost.ToString();
			Tex_RES_PEN.Text = role.RES_PEN.ToString();
			Tex_DMG_Boost.Text = role.DMG_Boost.ToString();
			Tex_DMG_Taken.Text = role.DMG_Taken.ToString();
			Tex_DMG_Reduction.Text = role.DMG_Reduction.ToString();
			Tex_Break_Equal.Text = role.Break_Equal.ToString();
			Tex_Break_Effect.Text = role.Break_Effect.ToString();
			Tex_Break_Efficiency.Text = role.Break_Efficiency.ToString();
			Tex_Break_Boost.Text = role.Break_Boost.ToString();
			Cob_Break_Type.SelectedIndex = (int)role.Break_Type;
			Tex_SPD.Text = role.SPD.ToString();
			Tex_SPD_Base.Text = role.SPD_Base.ToString();
			Tex_Toughness.Text = role.Toughness.ToString();
			Tex_Toughness_Reduction.Text = role.Toughness_Reduction.ToString();
			Tex_Effect_Hit_Rate.Text = role.Effect_Hit_Rate.ToString();
			Tex_Effect_RES.Text = role.Effect_RES.ToString();
			Tex_Energy_Regeneration_Rate.Text = role.Energy_Regeneration_Rate.ToString();
			Flags[5] = Flags[6] = true;
			Roled.Gain = new List<int>(role.Gain);
			Roled.Transform = new List<int>(role.Transform);
			for (int i = 1; i < Cob_Gain.Items.Count; i++)
			{
				Cob_Gain.Items[i] = "□" + Cob_Gain.Items[i].ToString().Remove(0, 1);
			}
			foreach (int i in role.Gain)
			{
				if (i < Cob_Gain.Items.Count)
				{
					Cob_Gain.Items[i] = "√" + Cob_Gain.Items[i].ToString().Remove(0, 1);
				}
			}
			for (int i = 1; i < Cob_Transform.Items.Count; i++)
			{
				Cob_Transform.Items[i] = "□" + Cob_Transform.Items[i].ToString().Remove(0, 1);
			}
			foreach (int i in role.Transform)
			{
				if (i < Cob_Transform.Items.Count)
				{
					Cob_Transform.Items[i] = "√" + Cob_Transform.Items[i].ToString().Remove(0, 1);
				}
			}
			Tex_Calculator_TextChanged(null, null);
			Tex_Transform_TextChanged(null, null);
			Flags[5] = Flags[6] = false;
			Flags[0] = false;
			DMG_Compute();
		}
		// 读取数据文件
		private void LoadDate()
		{
			if (File.Exists(App_Path[2]))
			{
				try
				{
					string[] Arr = File.ReadAllLines(App_Path[2]);
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
										Role role = new Role(Arr[i]);
										Cob_Simple.Items.Add(role.ToSimple(n++));
										Roles.Add(role);
										break;
									case '&':
										Cob_DMG_Equal.Items.Add(Arr[i].Remove(0, 2));
										break;
									case '#':
										if (Arr[i].IndexOf(':') < Arr[i].LastIndexOf("["))
										{
											Cob_Transform.Items.Add("□" + Arr[i].Remove(0, 1));
										}
										else
										{
											Cob_Gain.Items.Add("□" + Arr[i].Remove(0, 1));
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
			string Dirctoy = Path.GetDirectoryName(App_Path[2]);
			if (!Directory.Exists(Dirctoy))
			{
				Directory.CreateDirectory(Dirctoy);
			}
			try
			{
				string[] Arr = File.Exists(App_Path[2]) ? File.ReadAllLines(App_Path[2]) : new string[0];
				List<string> Info = new List<string> { Group.Remove(0, Group.IndexOf(" ") + 1) + '$' };
				for (int i = 0; i < Arr.Length; i++)
				{
					if (Arr[i].StartsWith("G") && Arr[i] != Group)
					{
						Info.Add(Arr[i].Remove(0, Arr[i].IndexOf(" ") + 1) + '$' + i);
					}
				}
				if (Info.Count > 1) Info.Sort();
				using (StreamWriter Writ = new StreamWriter(App_Path[2], false))
				{
					Save_Info = false;
					for (int i = 0; i < Info.Count; i++)
					{
						string[] Tp = Info[i].Split('$');
						Writ.WriteLine("G-" + (i + 1) + " " + Tp[0]);
						if (int.TryParse(Tp[1], out int k))
						{
							while (++k < Arr.Length)
							{
								if (Arr[k] == "") continue;
								if (Arr[k].StartsWith("G")) break;
								else Writ.WriteLine(Arr[k]);
							}
						}
						else
						{
							Group = "G-" + (i + 1) + " " + Group.Remove(0, Group.IndexOf(" ") + 1);
							for (k = 0; k < Roles.Count; k++)
							{
								Writ.WriteLine(Roles[k].ToString(k));
							}
							for (k = 1; k < Cob_DMG_Equal.Items.Count; k++)
							{
								Writ.WriteLine("& " + Cob_DMG_Equal.Items[k]);
							}
							for (k = 1; k < Cob_Transform.Items.Count; k++)
							{
								Writ.WriteLine("# " + Cob_Transform.Items[k].ToString().Remove(0, 2));
							}
							for (k = 1; k < Cob_Gain.Items.Count; k++)
							{
								Writ.WriteLine("# " + Cob_Gain.Items[k].ToString().Remove(0, 2));
							}
						}
						if (i < Info.Count - 1) Writ.WriteLine();
					}
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
				string[] Arr = File.ReadAllLines(App_Path[2]);
				using (StreamWriter Writ = new StreamWriter(App_Path[2], false))
				{
					int seq = 1;
					int end = -1;
					Save_Info = false;
					int tar = Array.IndexOf(Arr, Group);
					for (int i = 0; i < Arr.Length; i++)
					{
						if (Arr[i].StartsWith("G"))
						{
							if (i != tar)
							{
								Writ.WriteLine($"G-{seq++} " + Arr[i].Remove(0, Arr[i].IndexOf(' ') + 1));
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
			if (Str.Contains("="))
			{
				Str = Str.Remove(Str.LastIndexOf("="));
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
		private (string Equ, double Val, int Loc) Compute(string Str, Role role, int Loc = 0)
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
							if (Pend.Contains("."))
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
							string[] Bra = Bracket(Str.Remove(0, i));
							Pend = Bra[0].Substring(1, Bra[0].Length - 2);
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
							Pend = Str.Remove(0, i + 1);
							if (Pend.Contains("]"))
							{
								Pend = Pend.Remove(Pend.IndexOf(']'));
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
										if (i == Loc && Pend.Contains("."))
										{
											Pend = (Pend.TrimEnd('0').EndsWith(".") ? "." : "") +
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
						if (Loc + _Loc == Equ.Length && Pend.Contains("."))
						{
							Pend = (Pend.TrimEnd('0').EndsWith(".") ? "." : "") +
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
		private string[] Bracket(string str)
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
							return new string[2] { str, "0" };
						}
						else
						{
							return new string[2] { str.Remove(i + 1), "0" };
						}
					}
					else if (info < 0)
					{
						str = str.Remove(i, 1);
						info++;
					}
				}
			}
			return new string[2] { str + "".PadLeft(info, ')'), info.ToString() };
		}
		private int Mark(string Str, int Loc)
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
		private float ToFloat(string str)
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
						if (!Info.Contains("."))
						{
							Info += c;
						}
					}
				}
				if (float.TryParse(Info, out result))
				{
					if (str.Contains("-"))
					{
						return -result;
					}
					else return result;
				}
				else return 0;
			}
		}
		// 获取控件目标
		private TextBox GetTextBox(string Str)
		{
			return Controls.Find(Str, true)[0] as TextBox;
		}
		// 插入ComboBox
		private void Cob_Insert(ComboBox Cob, string Str)
		{
			int Info = -1;
			for (int i = 1; i < Cob.Items.Count; i++)
			{
				if (string.Compare(Str, Cob.Items[i].ToString()) < 0)
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
			Cob.SelectedIndex = Info;
		}
		// 字体文本修改
		private void SetTexFont(TextBox Tex, string Str)
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
				Ceb.ForeColor = Color.White;
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
						Ceb.ForeColor = Color.White;
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