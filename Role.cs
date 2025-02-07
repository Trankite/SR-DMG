﻿using System;
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
		private static readonly float[] Break_Ratio =
		[
		4,4,
		1,1.2f,
		1,0,
		4,2,
		2,2,
		2,4,
		3,2
		];
		private static readonly float[] Break_Factor =
		[
			0, 54.00f, 58.00f, 62.00f, 67.53f, 70.51f, 73.52f, 76.57f, 79.64f, 82.74f, 85.87f, 91.49f, 97.07f, 102.59f, 108.06f, 113.47f, 118.84f, 124.15f, 129.41f, 134.62f, 139.77f,
			149.33f, 158.80f, 168.18f, 177.46f, 186.65f, 195.75f, 204.75f, 213.66f, 222.48f, 231.20f, 246.43f, 261.18f, 275.47f, 289.32f, 302.73f, 315.71f, 328.29f, 340.47f, 352.26f, 363.67f,
			408.12f, 451.79f, 494.68f, 536.82f, 578.22f, 618.92f, 658.91f, 698.23f, 736.89f, 774.90f, 871.06f, 964.87f, 1056.42f, 1145.79f, 1233.06f, 1318.30f, 1401.58f, 1482.96f, 1562.52f, 1640.31f,
			1752.32f, 1861.90f, 1969.12f, 2074.07f, 2176.80f, 2277.39f, 2375.91f, 2472.42f, 2566.97f, 2659.64f, 2780.30f, 2898.60f, 3014.60f, 3128.37f, 3239.98f, 3349.47f, 3456.92f, 3562.38f, 3665.91f, 3767.55f
		];
		public static readonly Dictionary<string, bool> Map_Mark = new(72)
		{
			{ "攻击力", false },
			{ "生命值", false },
			{ "防御力", false },
			{ "基础攻击", false },
			{ "基础生命", false },
			{ "基础防御", false },
			{ "攻击倍率", true },
			{ "特殊倍率", true },
			{ "固定数值", false },
			{ "独立乘区", true },
			{ "倍率类型", false },
			{ "倍率标号", false },
			{ "暴击率", true },
			{ "暴击伤害", true },
			{ "角色等级", false },
			{ "怪物等级", false },
			{ "减防", true },
			{ "无视防御", true },
			{ "抗性", true },
			{ "穿透", true },
			{ "增伤", true },
			{ "易伤", true },
			{ "免伤", true },
			{ "超击破", true },
			{ "击破特攻", true },
			{ "击破效率", true },
			{ "击破增伤", true },
			{ "击破类型", false },
			{ "速度", false },
			{ "基础速度", false },
			{ "韧性", false },
			{ "削韧", false },
			{ "效果命中", true },
			{ "效果抵抗", true },
			{ "充能效率", true },
			{ "治疗提高", true }
		};
		private static readonly Dictionary<string, string> Map_Name = new(72)
		{
			{ "攻击力", "ATK" },
			{ "生命值", "HP" },
			{ "防御力", "DEF" },
			{ "基础攻击", "ATK_Base" },
			{ "基础生命", "HP_Base" },
			{ "基础防御", "DEF_Base" },
			{ "攻击倍率", "DMG_Equal_1" },
			{ "特殊倍率", "DMG_Equal_2" },
			{ "固定数值", "DMG_Equal_3" },
			{ "独立乘区", "DMG_Equal_4" },
			{ "倍率类型", "DMG_Equal_Tpye" },
			{ "倍率标号", "DMG_Equal_Info" },
			{ "暴击率", "CRIT_Rate" },
			{ "暴击伤害", "CRIT_DMG" },
			{ "角色等级", "Character_Level" },
			{ "怪物等级", "Enemy_Level" },
			{ "减防", "DEF_Reduced" },
			{ "无视防御", "DEF_Ignores" },
			{ "抗性", "RES_Boost" },
			{ "穿透", "RES_PEN" },
			{ "增伤", "DMG_Boost" },
			{ "易伤", "DMG_Taken" },
			{ "免伤", "DMG_Reduction" },
			{ "超击破", "Break_Equal" },
			{ "击破特攻", "Break_Effect" },
			{ "击破效率", "Break_Efficiency" },
			{ "击破增伤", "Break_Boost" },
			{ "击破类型", "Break_Type" },
			{ "速度", "SPD" },
			{ "基础速度", "SPD_Base" },
			{ "韧性", "Toughness" },
			{ "削韧", "Toughness_Reduction" },
			{ "效果命中", "Effect_Hit_Rate" },
			{ "效果抵抗", "Effect_RES" },
			{ "充能效率", "Energy_Regeneration_Rate" },
			{ "治疗提高", "Heal_Rate" }
		};
		private static readonly Dictionary<string, PropertyInfo> Map_Property = new(72);

		// 实例参数
		public float[] Base = new float[36];

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

		// 治疗提高
		public float Heal_Rate
		{
			get { return Base[35]; }
			set
			{
				Set("治疗提高", ref Base[35], value, 1);
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
		public List<int> Gain = [];

		// 转化
		public List<int> Transform = [];

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
			float[][] DMG = [new float[8], new float[6]];
			DMG[0][1] = (DMG_Equal_Tpye == 0 ? 0 : DMG_Equal_Tpye == 1 ? HP : DEF) * DMG_Equal_2 * 0.01f;
			DMG[0][0] = ATK * DMG_Equal_1 * 0.01f + DMG[0][1];
			DMG[0][2] = (Character_Level * 10 + 200) / ((Character_Level * 10 + 200) +
				(Enemy_Level * 10 + 200) * (1 - (DEF_Reduced + DEF_Ignores) * 0.01f));
			DMG[0][3] = 1 - (RES_Boost - RES_PEN) * 0.01f;
			DMG[0][4] = 1 + CRIT_Rate * 0.01f * CRIT_DMG * 0.01f;
			DMG[0][5] = (1 - DMG_Reduction * 0.01f) * (1 + DMG_Taken * 0.01f) * (1 + DMG_Boost * 0.01f);
			DMG[0][6] = Break_Factor[Math.Min(Math.Max(0, (int)Character_Level), 80)];
			DMG[0][7] = (Toughness * 0.1f + 2) * 0.5f;
			DMG[1][0] = DMG[0][0] * DMG[0][2] * DMG[0][3] * DMG[0][5] * (1 + DMG_Equal_4 * 0.01f);
			DMG[1][1] = (1 + CRIT_DMG * 0.01f) * DMG[1][0];
			DMG[1][2] = DMG[0][4] * DMG[1][0];
			DMG[1][3] = DMG[0][2] * DMG[0][3] * DMG[0][6] * 0.1f * (1 - DMG_Reduction * 0.01f) * (1 + DMG_Taken * 0.01f) * (1 + Break_Effect * 0.01f);
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
			Dictionary<string, string> _Map_Name = new(Map_Name.Count);
			foreach (KeyValuePair<string, string> data in Map_Name)
			{
				_Map_Name[data.Value] = data.Key;
			}
			foreach (KeyValuePair<string, string> data in _Map_Name)
			{
				Map_Name[data.Key] = data.Value;
				Map_Mark[data.Key] = Map_Mark[data.Value];
			}
			foreach (PropertyInfo Info in typeof(Role).GetProperties())
			{
				Map_Property[Info.Name] = Info;
				Map_Property[GetName(Info.Name)] = Info;
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