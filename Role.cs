using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SR_DMG
{
	public class Role : IComparable
	{
		public Role()
		{
			Base[14] = 80;
			Base[15] = 72;
			Base[22] = 10;
			Base[23] = 100;
			Base[30] = 150;
			Base[31] = 30;
			Base[34] = 100;
		}
		public Role(Role role)
		{
			Name = role.Name;
			Gain = new List<int>(role.Gain);
			Transform = new List<int>(role.Transform);
			Array.Copy(role.Base, Base, Base.Length);
		}
		public Role(string str)
		{
			string[] Arr = str.Split(',');
			Name = Arr[0].Remove(0, Arr[0].IndexOf(" ") + 1);
			int i = 0;
			while (i < Base.Length)
			{
				Base[i++] = float.Parse(Arr[i]);
			}
			string[] Info = Arr[++i].Trim('[', ']').Split('-');
			if (Info[0] != "")
			{
				for (int k = 0; k < Info.Length; k++)
				{
					Gain.Add(int.Parse(Info[k]));
				}
			}
			Info = Arr[++i].Trim('[', ']').Split('-');
			if (Info[0] != "")
			{
				for (int k = 0; k < Info.Length; k++)
				{
					Transform.Add(int.Parse(Info[k]));
				}
			}
			Transform = Transform.Distinct().ToList();
			Gain = Gain.Distinct().ToList();
		}
		private static SR_DMG App;
		private static readonly float[] Break_Ratio = new float[14]
		{
		4,4,
		1,1.2f,
		1,0,
		4,2,
		2,2,
		2,4,
		3,2
		};
		private static readonly float[] Break_Factor = new float[100]
		{
		0, 27, 29, 31, 33.76319f, 35.2547f, 36.76141f, 38.28302f, 39.81923f, 41.36973f, 42.93422f, 45.7472f,
		48.53399f, 51.29458f, 54.02897f, 56.73717f, 59.41916f, 62.07496f, 64.70456f, 67.30796f, 69.88517f, 74.66615f,
		79.40056f, 84.08841f, 88.7297f, 93.32443f, 97.87259f, 102.37419f, 106.82923f, 111.2377f, 115.59962f,
		123.21378f, 130.59049f, 137.73666f, 144.65895f, 151.36374f, 157.85719f, 164.14523f, 170.23357f, 176.12769f,
		181.83291f, 204.062f, 225.89415f, 247.33989f, 268.40939f, 289.11246f, 309.45858f, 329.45691f, 349.11628f,
		368.44524f, 387.45205f, 435.52998f, 482.43527f, 528.2103f, 572.8955f, 616.52925f, 659.14825f, 700.7875f,
		741.4804f, 781.2589f, 820.1534f, 876.16075f, 930.95055f, 984.5621f, 1037.03295f, 1088.39915f, 1138.6952f,
		1187.95425f, 1236.208f, 1283.48695f, 1329.8203f, 1390.1522f, 1449.3011f, 1507.30145f, 1564.18645f,
		1619.9879f, 1674.7365f, 1728.4618f, 1781.19215f, 1832.95495f, 1883.77665f, 1978.9309f, 2077.6059f,
		2179.9319f, 2286.0439f, 2396.08205f, 2510.19165f, 2628.5233f, 2751.2332f, 2878.48335f, 3010.4418f,
		3147.2827f, 3289.1867f, 3436.34115f, 3588.9403f, 3747.18565f, 3911.28605f, 4081.4582f, 4257.9267f,
		4440.92455f
		};
		private static readonly Dictionary<string, bool> Map_Mark = new Dictionary<string, bool>
	{
		{ "攻击力", false },{ "ATK", false },
		{ "生命值", false },{ "HP", false },
		{ "防御力", false },{ "DEF", false },
		{ "基础攻击", false },{ "ATK_Base", false },
		{ "基础生命", false },{ "HP_Base", false },
		{ "基础防御", false },{ "DEF_Base", false },
		{ "攻击倍率", true },{ "DMG_Equal_1", true },
		{ "特殊倍率", true },{ "DMG_Equal_2", true },
		{ "固定数值", false },{ "DMG_Equal_3", false },
		{ "独立乘区", true },{ "DMG_Equal_4", true },
		{ "倍率类型", false },{ "DMG_Equal_Tpye", false },
		{ "倍率标号", false },{ "DMG_Equal_Info", false },
		{ "暴击率", true },{ "CRIT_Rate", true },
		{ "暴击伤害", true },{ "CRIT_DMG", true },
		{ "角色等级", false },{ "Character_Level", false },
		{ "怪物等级", false },{ "Enemy_Level", false },
		{ "减防", true },{ "DEF_Reduced", true },
		{ "无视防御", true },{ "DEF_Ignores", true },
		{ "抗性", true },{ "RES_Boost", true },
		{ "穿透", true },{ "RES_PEN", true },
		{ "增伤", true },{ "DMG_Boost", true },
		{ "易伤", true },{ "DMG_Taken", true },
		{ "免伤", true },{ "DMG_Reduction", true },
		{ "超击破", true },{ "Break_Equal", true },
		{ "击破特攻", true },{ "Break_Effect", true },
		{ "击破效率", true },{ "Break_Efficiency", true },
		{ "击破增伤", true },{ "Break_Boost", true },
		{ "击破类型", false },{ "Break_Type", false },
		{ "速度", false },{ "SPD", false },
		{ "基础速度", false },{ "SPD_Base", false },
		{ "韧性", false },{ "Toughness", false },
		{ "削韧", false },{ "Toughness_Reduction", false },
		{ "效果命中", true },{ "Effect_Hit_Rate", true },
		{ "效果抵抗", true },{ "Effect_RES", true },
		{ "充能效率", true },{ "Energy_Regeneration_Rate", true }
	};
		private static readonly Dictionary<string, string> Map_Name = new Dictionary<string, string>
	{
		{ "攻击力", "ATK" },{ "ATK", "攻击力" },
		{ "生命值", "HP" },{ "HP", "生命值" },
		{ "防御力", "DEF" },{ "DEF", "防御力" },
		{ "基础攻击", "ATK_Base" },{ "ATK_Base", "基础攻击" },
		{ "基础生命", "HP_Base" },{ "HP_Base", "基础生命" },
		{ "基础防御", "DEF_Base" },{ "DEF_Base", "基础防御" },
		{ "攻击倍率", "DMG_Equal_1" },{ "DMG_Equal_1", "攻击倍率" },
		{ "特殊倍率", "DMG_Equal_2" },{ "DMG_Equal_2", "特殊倍率" },
		{ "固定数值", "DMG_Equal_3" },{ "DMG_Equal_3", "固定数值" },
		{ "独立乘区", "DMG_Equal_4" },{ "DMG_Equal_4", "独立乘区" },
		{ "倍率类型", "DMG_Equal_Tpye" },{ "DMG_Equal_Tpye", "倍率类型" },
		{ "倍率标号", "DMG_Equal_Info" },{ "DMG_Equal_Info", "倍率标号" },
		{ "暴击率", "CRIT_Rate" },{ "CRIT_Rate", "暴击率" },
		{ "暴击伤害", "CRIT_DMG" },{ "CRIT_DMG", "暴击伤害" },
		{ "角色等级", "Character_Level" },{ "Character_Level", "角色等级" },
		{ "怪物等级", "Enemy_Level" },{ "Enemy_Level", "怪物等级" },
		{ "减防", "DEF_Reduced" },{ "DEF_Reduced", "减防" },
		{ "无视防御", "DEF_Ignores" },{ "DEF_Ignores", "无视防御" },
		{ "抗性", "RES_Boost" },{ "RES_Boost", "抗性" },
		{ "穿透", "RES_PEN" },{ "RES_PEN", "穿透" },
		{ "增伤", "DMG_Boost" },{ "DMG_Boost", "增伤" },
		{ "易伤", "DMG_Taken" },{ "DMG_Taken", "易伤" },
		{ "免伤", "DMG_Reduction" },{ "DMG_Reduction", "免伤" },
		{ "超击破", "Break_Equal" },{ "Break_Equal", "超击破" },
		{ "击破特攻", "Break_Effect" },{ "Break_Effect", "击破特攻" },
		{ "击破效率", "Break_Efficiency" },{ "Break_Efficiency", "击破效率" },
		{ "击破增伤", "Break_Boost" },{ "Break_Boost", "击破增伤" },
		{ "击破类型", "Break_Type" },{ "Break_Type", "击破类型" },
		{ "速度", "SPD" },{ "SPD", "速度" },
		{ "基础速度", "SPD_Base" },{ "SPD_Base", "基础速度" },
		{ "韧性", "Toughness" },{ "Toughness", "韧性" },
		{ "削韧", "Toughness_Reduction" },{ "Toughness_Reduction", "削韧" },
		{ "效果命中", "Effect_Hit_Rate" },{ "Effect_Hit_Rate", "效果命中" },
		{ "效果抵抗", "Effect_RES" },{ "Effect_RES", "效果抵抗" },
		{ "充能效率", "Energy_Regeneration_Rate" },{ "Energy_Regeneration_Rate", "充能效率" }
	};
		private static readonly Dictionary<string, PropertyInfo> Map_Property = new Dictionary<string, PropertyInfo>(70);

		// 实例参数
		public float[] Base = new float[35];

		// 名称
		public string Name = "";

		// 攻击力
		public float ATK
		{
			get { return Base[0]; }
			set
			{
				Set("攻击力", ref Base[0], value, 0);
			}
		}

		// 生命值
		public float HP
		{
			get { return Base[1]; }
			set
			{
				Set("生命值", ref Base[1], value, 0);
			}
		}

		// 防御力
		public float DEF
		{
			get { return Base[2]; }
			set
			{
				Set("防御力", ref Base[2], value, 0);
			}
		}

		// 基础攻击
		public float ATK_Base
		{
			get { return Base[3]; }
			set
			{
				Set("基础攻击", ref Base[3], value, 0);
			}
		}

		// 基础生命
		public float HP_Base
		{
			get { return Base[4]; }
			set
			{
				Set("基础生命", ref Base[4], value, 0);
			}
		}

		// 基础防御 
		public float DEF_Base
		{
			get { return Base[5]; }
			set
			{
				Set("基础防御", ref Base[5], value, 0);
			}
		}

		// 攻击倍率
		public float DMG_Equal_1
		{
			get { return Base[6]; }
			set
			{
				Set("攻击倍率", ref Base[6], value, 1);
			}
		}

		// 特殊倍率
		public float DMG_Equal_2
		{
			get { return Base[7]; }
			set
			{
				Set("特殊倍率", ref Base[7], value, 1);
			}
		}

		// 固定数值
		public float DMG_Equal_3
		{
			get { return Base[8]; }
			set
			{
				Set("固定数值", ref Base[8], value, 0);
			}
		}

		// 独立乘区
		public float DMG_Equal_4
		{
			get { return Base[9]; }
			set
			{
				Set("独立乘区", ref Base[9], value, 1);
			}
		}

		// 倍率类型
		public float DMG_Equal_Tpye
		{
			get { return Base[10]; }
			set
			{
				Set("倍率类型", ref Base[10], value, 0);
			}
		}

		// 倍率标号
		public float DMG_Equal_Info
		{
			get { return Base[11]; }
			set
			{
				Set("倍率标号", ref Base[11], value, 0);
			}
		}

		// 暴击率
		public float CRIT_Rate
		{
			get { return Base[12]; }
			set
			{
				Set("暴击率", ref Base[12], value, 1);
			}
		}

		// 暴击伤害
		public float CRIT_DMG
		{
			get { return Base[13]; }
			set
			{
				Set("暴击伤害", ref Base[13], value, 1);
			}
		}

		// 角色等级
		public float Character_Level
		{
			get { return Base[14]; }
			set
			{
				Set("角色等级", ref Base[14], value, 0);
			}
		}

		// 怪物等级
		public float Enemy_Level
		{
			get { return Base[15]; }
			set
			{
				Set("怪物等级", ref Base[15], value, 0);
			}
		}

		// 减防
		public float DEF_Reduced
		{
			get { return Base[16]; }
			set
			{
				Set("减防", ref Base[16], value, 1);
			}
		}

		// 无视防御
		public float DEF_Ignores
		{
			get { return Base[17]; }
			set
			{
				Set("无视防御", ref Base[17], value, 1);
			}
		}

		// 抗性
		public float RES_Boost
		{
			get { return Base[18]; }
			set
			{
				Set("抗性", ref Base[18], value, 1);
			}
		}

		// 穿透
		public float RES_PEN
		{
			get { return Base[19]; }
			set
			{
				Set("穿透", ref Base[19], value, 1);
			}
		}

		// 增伤
		public float DMG_Boost
		{
			get { return Base[20]; }
			set
			{
				Set("增伤", ref Base[20], value, 1);
			}
		}

		// 易伤
		public float DMG_Taken
		{
			get { return Base[21]; }
			set
			{
				Set("易伤", ref Base[21], value, 1);
			}
		}

		// 免伤
		public float DMG_Reduction
		{
			get { return Base[22]; }
			set
			{
				Set("免伤", ref Base[22], value, 1);
			}
		}

		// 超击破
		public float Break_Equal
		{
			get { return Base[23]; }
			set
			{
				Set("超击破", ref Base[23], value, 1);
			}
		}

		// 击破特攻
		public float Break_Effect
		{
			get { return Base[24]; }
			set
			{
				Set("击破特攻", ref Base[24], value, 1);
			}
		}

		// 击破效率
		public float Break_Efficiency
		{
			get { return Base[25]; }
			set
			{
				Set("击破效率", ref Base[25], value, 1);
			}
		}

		// 击破增伤
		public float Break_Boost
		{
			get { return Base[26]; }
			set
			{
				Set("击破增伤", ref Base[26], value, 1);
			}
		}

		// 击破类型
		public float Break_Type
		{
			get { return Base[27]; }
			set
			{
				Set("击破类型", ref Base[27], value, 0);
			}
		}

		// 速度
		public float SPD
		{
			get { return Base[28]; }
			set
			{
				Set("速度", ref Base[28], value, 0);
			}
		}

		//基础速度
		public float SPD_Base
		{
			get { return Base[29]; }
			set
			{
				Set("基础速度", ref Base[29], value, 0);
			}
		}

		// 韧性
		public float Toughness
		{
			get { return Base[30]; }
			set
			{
				Set("韧性", ref Base[30], value, 0);
			}
		}

		// 削韧
		public float Toughness_Reduction
		{
			get { return Base[31]; }
			set
			{
				Set("削韧", ref Base[31], value, 0);
			}
		}

		// 效果命中
		public float Effect_Hit_Rate
		{
			get { return Base[32]; }
			set
			{
				Set("效果命中", ref Base[32], value, 1);
			}
		}

		// 效果抵抗
		public float Effect_RES
		{
			get { return Base[33]; }
			set
			{
				Set("效果抵抗", ref Base[33], value, 1);
			}
		}

		// 充能效率
		public float Energy_Regeneration_Rate
		{
			get { return Base[34]; }
			set
			{
				Set("充能效率", ref Base[34], value, 1);
			}
		}

		// 转化更新器
		private void Set(string name, ref float date, float value, int acc)
		{
			App.TranUpdate(name, this, false);
			date = (float)Math.Round(value, acc);
			App.TranUpdate(name, this, true);
		}

		// 增益
		public List<int> Gain = new List<int>();

		// 转化
		public List<int> Transform = new List<int>();

		// 顺序插入
		public void AddGain(int tar)
		{
			int index = 0;
			while (index < Gain.Count && Gain[index] <= tar)
			{
				index++;
			}
			Gain.Insert(index, tar);
		}
		public void AddTransform(int tar)
		{
			int index = 0;
			while (index < Transform.Count && Transform[index] <= tar)
			{
				index++;
			}
			Transform.Insert(index, tar);
		}

		// 重写 ToString
		public override string ToString()
		{
			return $"{Name},{string.Join(",", Base)},[{string.Join("-", Gain)}],[{string.Join("-", Transform)}]";
		}

		// 带标号的
		public string ToString(int Info)
		{
			return $"D-{Info + 1} {Name},{string.Join(",", Base)},[{string.Join("-", Gain)}],[{string.Join("-", Transform)}]";
		}

		// 简化显示
		public string ToSimple(int Info)
		{
			return $"· D-{(Info + 1)} {Name}：攻击力 {ATK} | 倍率 {DMG_Equal_1} % | 暴率 {CRIT_Rate} % | 暴伤 {CRIT_DMG} % | 击破特攻 {Break_Effect} %";
		}

		// 排序
		public int CompareTo(object obj)
		{
			return Name.CompareTo((obj as Role).Name);
		}

		// 伤害计算
		public float[][] DMG()
		{
			float[][] DMG = new float[2][] { new float[8], new float[6] };
			DMG[0][1] = (DMG_Equal_Tpye == 0 ? 0 : DMG_Equal_Tpye == 1 ? HP : DEF) * DMG_Equal_2 * 0.01f;
			DMG[0][0] = ATK * DMG_Equal_1 * 0.01f + DMG[0][1];
			DMG[0][2] = (Character_Level * 10 + 200) / ((Character_Level * 10 + 200) +
				(Enemy_Level * 10 + 200) * (1 - (DEF_Reduced + DEF_Ignores) * 0.01f));
			DMG[0][3] = 1 - (RES_Boost - RES_PEN) * 0.01f;
			DMG[0][4] = 1 + CRIT_Rate * 0.01f * CRIT_DMG * 0.01f;
			DMG[0][5] = (1 - DMG_Reduction * 0.01f) * (1 + DMG_Taken * 0.01f) * (1 + DMG_Boost * 0.01f);
			DMG[0][6] = Break_Factor[Math.Min(Math.Max(0, (int)Character_Level), 99)];
			DMG[0][7] = (Toughness * 0.1f + 2) * 0.5f;
			DMG[1][0] = DMG[0][0] * DMG[0][2] * DMG[0][3] * DMG[0][5] * (1 + DMG_Equal_4 * 0.01f);
			DMG[1][1] = (1 + CRIT_DMG * 0.01f) * DMG[1][0];
			DMG[1][2] = DMG[0][4] * DMG[1][0];
			DMG[1][3] = DMG[0][2] * DMG[0][3] * DMG[0][6] * (1 - DMG_Reduction * 0.01f) * (1 + DMG_Taken * 0.01f) * (1 + Break_Effect * 0.01f);
			if (Break_Type == 2)
			{
				DMG[1][5] = 30 * (1 + Break_Effect * 0.01f);
			}
			else
			{
				DMG[1][5] = Break_Ratio[((int)Break_Type + 1) * 2 - 1] * DMG[1][3];
				if (Break_Type <= 1) DMG[1][5] *= DMG[0][7];
			}
			DMG[1][3] *= (1 + Break_Boost * 0.01f);
			DMG[1][4] = Break_Ratio[((int)Break_Type + 1) * 2 - 2] * DMG[0][7] * DMG[1][3];
			DMG[1][3] *= Break_Equal * 0.01f * Toughness_Reduction * (1 + Break_Efficiency * 0.01f);
			return DMG;
		}

		// 外部接口
		public static void Start(SR_DMG App)
		{
			Role.App = App;
			foreach (PropertyInfo Info in
				typeof(Role).GetProperties())
			{
				Map_Property.Add(Info.Name, Info);
				Map_Property.Add(GetName(Info.Name), Info);
			}
		}
		public static bool GetType(string str)
		{
			if (str == null) return false;
			if (Map_Mark.TryGetValue(str, out bool Info))
			{
				return Info;
			}
			else return false;
		}
		public static string GetName(string str)
		{
			if (str == null) return null;
			if (Map_Name.TryGetValue(str, out str))
			{
				return str;
			}
			else return null;
		}
		public void SetValue(string name, float value)
		{
			if (name == null) return;
			if (Map_Property.TryGetValue(name, out PropertyInfo Info))
			{
				Info.SetValue(this, value);
			}
		}
		public float GetValue(string name)
		{
			if (name == null) return float.NaN;
			if (Map_Property.TryGetValue(name, out PropertyInfo Info))
			{
				return Convert.ToSingle(Info.GetValue(this));
			}
			else return float.NaN;
		}

	}
}