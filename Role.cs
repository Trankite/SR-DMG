using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
			Gain = [.. role.Gain];
			Transform = [.. role.Transform];
			Array.Copy(role.Base, Base, Base.Length);
		}
		public Role(string str)
		{
			string[] Arr = str.TrimEnd(',').Split(',');
			Name = Arr[0][(Arr[0].IndexOf(' ') + 1)..];
			foreach (string s in Arr[^1].Trim('[', ']').Split('-'))
			{
				if (int.TryParse(s, out int n)) Transform.Add(n);
			}
			foreach (string s in Arr[^2].Trim('[', ']').Split('-'))
			{
				if (int.TryParse(s, out int n)) Gain.Add(n);
			}
			for (int i = 0; i < Base.Length; i++)
			{
				if (i < Arr.Length - 1 && float.TryParse(Arr[i + 1], out float Val)) Base[i] = Val;
			}
			Transform = [.. Transform.Distinct()];
			Gain = [.. Gain.Distinct()];
		}
		private static float[][] BreakRecord;
		private static Dictionary<string, Property> Map_Property;
		private static List<Property> Properties;
		private static string Tips;

		// 实例参数
		public float[] Base = new float[38];

		// 名称
		public string Name = string.Empty;

		// 攻击力
		public float ATK
		{
			get { return Get(nameof(ATK)); }
			set { Set(nameof(ATK), value); }
		}

		// 生命值
		public float HP
		{
			get { return Get(nameof(HP)); }
			set { Set(nameof(HP), value); }
		}

		// 防御力
		public float DEF
		{
			get { return Get(nameof(DEF)); }
			set { Set(nameof(DEF), value); }
		}

		// 基础攻击
		public float ATK_Base
		{
			get { return Get(nameof(ATK_Base)); }
			set { Set(nameof(ATK_Base), value); }
		}

		// 基础生命
		public float HP_Base
		{
			get { return Get(nameof(HP_Base)); }
			set { Set(nameof(HP_Base), value); }
		}

		// 基础防御 
		public float DEF_Base
		{
			get { return Get(nameof(DEF_Base)); }
			set { Set(nameof(DEF_Base), value); }

		}

		// 攻击倍率
		public float DMG_Equal_1
		{
			get { return Get(nameof(DMG_Equal_1)); }
			set { Set(nameof(DMG_Equal_1), value); }
		}

		// 特殊倍率
		public float DMG_Equal_2
		{
			get { return Get(nameof(DMG_Equal_2)); }
			set { Set(nameof(DMG_Equal_2), value); }
		}

		// 固定数值
		public float DMG_Equal_3
		{
			get { return Get(nameof(DMG_Equal_3)); }
			set { Set(nameof(DMG_Equal_3), value); }
		}

		// 独立乘区
		public float DMG_Equal_4
		{
			get { return Get(nameof(DMG_Equal_4)); }
			set { Set(nameof(DMG_Equal_4), value); }
		}

		// 倍率类型
		public float DMG_Equal_Tpye
		{
			get { return Get(nameof(DMG_Equal_Tpye)); }
			set { Set(nameof(DMG_Equal_Tpye), value); }
		}

		// 倍率标号
		public float DMG_Equal_Info
		{
			get { return Get(nameof(DMG_Equal_Info)); }
			set { Set(nameof(DMG_Equal_Info), value); }
		}

		// 暴击率
		public float CRIT_Rate
		{
			get { return Get(nameof(CRIT_Rate)); }
			set { Set(nameof(CRIT_Rate), value); }
		}

		// 暴击伤害
		public float CRIT_DMG
		{
			get { return Get(nameof(CRIT_DMG)); }
			set { Set(nameof(CRIT_DMG), value); }
		}

		// 角色等级
		public float Character_Level
		{
			get { return Get(nameof(Character_Level)); }
			set { Set(nameof(Character_Level), value); }
		}

		// 怪物等级
		public float Enemy_Level
		{
			get { return Get(nameof(Enemy_Level)); }
			set { Set(nameof(Enemy_Level), value); }
		}

		// 减防
		public float DEF_Reduced
		{
			get { return Get(nameof(DEF_Reduced)); }
			set { Set(nameof(DEF_Reduced), value); }
		}

		// 无视防御
		public float DEF_Ignores
		{
			get { return Get(nameof(DEF_Ignores)); }
			set { Set(nameof(DEF_Ignores), value); }
		}

		// 抗性
		public float RES_Boost
		{
			get { return Get(nameof(RES_Boost)); }
			set { Set(nameof(RES_Boost), value); }
		}

		// 穿透
		public float RES_PEN
		{
			get { return Get(nameof(RES_PEN)); }
			set { Set(nameof(RES_PEN), value); }
		}

		// 增伤
		public float DMG_Boost
		{
			get { return Get(nameof(DMG_Boost)); }
			set { Set(nameof(DMG_Boost), value); }
		}

		// 易伤
		public float DMG_Taken
		{
			get { return Get(nameof(DMG_Taken)); }
			set { Set(nameof(DMG_Taken), value); }
		}

		// 免伤
		public float DMG_Reduction
		{
			get { return Get(nameof(DMG_Reduction)); }
			set { Set(nameof(DMG_Reduction), value); }
		}

		// 超击破
		public float Break_Equal
		{
			get { return Get(nameof(Break_Equal)); }
			set { Set(nameof(Break_Equal), value); }
		}

		// 击破特攻
		public float Break_Effect
		{
			get { return Get(nameof(Break_Effect)); }
			set { Set(nameof(Break_Effect), value); }
		}

		// 击破效率
		public float Break_Efficiency
		{
			get { return Get(nameof(Break_Efficiency)); }
			set { Set(nameof(Break_Efficiency), value); }
		}

		// 击破增伤
		public float Break_Boost
		{
			get { return Get(nameof(Break_Boost)); }
			set { Set(nameof(Break_Boost), value); }
		}

		// 击破类型
		public float Break_Type
		{
			get { return Get(nameof(Break_Type)); }
			set { Set(nameof(Break_Type), value); }
		}

		// 速度
		public float SPD
		{
			get { return Get(nameof(SPD)); }
			set { Set(nameof(SPD), value); }
		}

		//基础速度
		public float SPD_Base
		{
			get { return Get(nameof(SPD_Base)); }
			set { Set(nameof(SPD_Base), value); }
		}

		// 韧性
		public float Toughness
		{
			get { return Get(nameof(Toughness)); }
			set { Set(nameof(Toughness), value); }
		}

		// 削韧
		public float Toughness_Reduction
		{
			get { return Get(nameof(Toughness_Reduction)); }
			set { Set(nameof(Toughness_Reduction), value); }
		}

		// 效果命中
		public float Effect_Hit_Rate
		{
			get { return Get(nameof(Effect_Hit_Rate)); }
			set { Set(nameof(Effect_Hit_Rate), value); }
		}

		// 效果抵抗
		public float Effect_RES
		{
			get { return Get(nameof(Effect_RES)); }
			set { Set(nameof(Effect_RES), value); }
		}

		// 充能效率
		public float Energy_Regeneration_Rate
		{
			get { return Get(nameof(Energy_Regeneration_Rate)); }
			set { Set(nameof(Energy_Regeneration_Rate), value); }
		}

		// 治疗提高
		public float Heal_Rate
		{
			get { return Get(nameof(Heal_Rate)); }
			set { Set(nameof(Heal_Rate), value); }
		}

		// 累计数值
		public float Accumulate
		{
			get { return Get(nameof(Accumulate)); }
			set { Set(nameof(Accumulate), value); }
		}

		// 效果层数
		public float Effect_Layers
		{
			get { return Get(nameof(Effect_Layers)); }
			set { Set(nameof(Effect_Layers), value); }
		}

		// 转化更新器
		private ref float Get(string name)
		{
			return ref Base[Map_Property[name].Index];
		}
		private void Set(string name, float value)
		{
			ref float data = ref Get(name);
			if (data == value) return;
			name = Map_Property[name].NickName;
			Program.MainForm.TranUpdate(name, this, false);
			data = (float)Math.Round(value, Map_Property[name].Accuracy);
			Program.MainForm.TranUpdate(name, this, true);
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
			DMG[0][1] = DMG_Equal_Tpye switch { 1 => HP, 2 => DEF, _ => 0 } * DMG_Equal_2 * 0.01f;
			DMG[0][0] = ATK * DMG_Equal_1 * 0.01f + DMG[0][1];
			DMG[0][2] = (Character_Level * 10 + 200) / ((Character_Level * 10 + 200) + (Enemy_Level * 10 + 200) * Math.Max(1 - (DEF_Reduced + DEF_Ignores) * 0.01f, 0));
			DMG[0][3] = 1 - (RES_Boost - RES_PEN) * 0.01f;
			DMG[0][4] = 1 + Math.Min(CRIT_Rate * 0.01f, 1) * CRIT_DMG * 0.01f;
			DMG[0][5] = (1 - DMG_Reduction * 0.01f) * (1 + DMG_Taken * 0.01f) * (1 + DMG_Boost * 0.01f);
			DMG[0][6] = Character_Level > 0 && Character_Level <= 80 ? BreakRecord[1][(int)Character_Level - 1] : 0;
			DMG[0][7] = (Toughness * 0.1f + 2) * 0.5f;
			DMG[1][0] = DMG[0][0] * DMG[0][2] * DMG[0][3] * DMG[0][5] * (1 + DMG_Equal_4 * 0.01f);
			DMG[1][1] = (1 + CRIT_DMG * 0.01f) * DMG[1][0];
			DMG[1][2] = DMG[0][4] * DMG[1][0];
			DMG[1][3] = DMG[0][2] * DMG[0][3] * DMG[0][6] * 0.1f * (1 - DMG_Reduction * 0.01f) * (1 + DMG_Taken * 0.01f) * (1 + Break_Effect * 0.01f);
			DMG[1][5] = Break_Type == 2 ? 30 * (1 + Break_Effect * 0.01f) : BreakRecord[0][((int)Break_Type + 1) * 2 - 1] * DMG[1][3];
			if (Break_Type <= 1) DMG[1][5] *= DMG[0][7];
			DMG[1][3] *= (1 + Break_Boost * 0.01f);
			DMG[1][4] = BreakRecord[0][((int)Break_Type + 1) * 2 - 2] * DMG[0][7] * DMG[1][3];
			DMG[1][3] *= Break_Equal * 0.01f * Toughness_Reduction * (1 + Break_Efficiency * 0.01f);
			return DMG;
		}

		// 外部接口
		public static void Init()
		{
			BreakRecord = [[4, 4, 1, 1.2f, 1, 0, 4, 2, 2, 2, 2, 4, 3, 2], [54, 58, 62, 68, 71, 74, 77, 80, 83, 86, 91, 97, 103, 108, 113, 119, 124, 129, 135, 140, 149, 159, 168, 177, 187, 196, 205, 214, 222, 231, 246, 261, 275, 289, 303, 316, 328, 340, 352, 364, 408, 452, 495, 537, 578, 619, 659, 698, 737, 775, 871, 965, 1056, 1146, 1233, 1318, 1402, 1483, 1563, 1640, 1752, 1862, 1969, 2074, 2177, 2277, 2376, 2472, 2567, 2660, 2780, 2899, 3015, 3128, 3240, 3349, 3457, 3562, 3666, 3768]];
			Properties =
			[
				new Property("攻击力", "ATK", 0),
				new Property("生命值", "HP", 0),
				new Property("防御力", "DEF", 0),
				new Property("基础攻击", "ATK_Base", 0),
				new Property("基础生命", "HP_Base", 0),
				new Property("基础防御", "DEF_Base", 0),
				new Property("攻击倍率", "DMG_Equal_1", 1),
				new Property("特殊倍率", "DMG_Equal_2", 1),
				new Property("固定数值", "DMG_Equal_3", 0),
				new Property("独立乘区", "DMG_Equal_4", 1),
				new Property("倍率类型", "DMG_Equal_Tpye", 0),
				new Property("倍率标号", "DMG_Equal_Info", 0),
				new Property("暴击率", "CRIT_Rate", 1),
				new Property("暴击伤害", "CRIT_DMG", 1),
				new Property("角色等级", "Character_Level", 0),
				new Property("怪物等级", "Enemy_Level", 0),
				new Property("减防", "DEF_Reduced", 1),
				new Property("无视防御", "DEF_Ignores", 1),
				new Property("抗性", "RES_Boost", 1),
				new Property("穿透", "RES_PEN", 1),
				new Property("增伤", "DMG_Boost", 1),
				new Property("易伤", "DMG_Taken", 1),
				new Property("免伤", "DMG_Reduction", 1),
				new Property("超击破", "Break_Equal", 1),
				new Property("击破特攻", "Break_Effect", 1),
				new Property("击破效率", "Break_Efficiency", 1),
				new Property("击破增伤", "Break_Boost", 1),
				new Property("击破类型", "Break_Type", 0),
				new Property("速度", "SPD", 0),
				new Property("基础速度", "SPD_Base", 0),
				new Property("韧性", "Toughness", 0),
				new Property("削韧", "Toughness_Reduction", 0),
				new Property("效果命中", "Effect_Hit_Rate", 1),
				new Property("效果抵抗", "Effect_RES", 1),
				new Property("充能效率", "Energy_Regeneration_Rate", 1),
				new Property("治疗提高", "Heal_Rate", 1),
				new Property("累积数值", "Accumulate", 0),
				new Property("效果层数", "Effect_Layers", 0)
			];
			for (int i = 0; i < Properties.Count; i++)
			{
				Properties[i].Index = i;
			}
			Map_Property = new Dictionary<string, Property>(72);
			foreach (Property Item in Properties)
			{
				Map_Property[Item.NickName] = Item;
				Map_Property[Item.PropertyName] = Item;
			}
			Type Type = typeof(Role);
			foreach (PropertyInfo Prop in Type.GetProperties())
			{
				Property Property = Map_Property[Prop.Name];
				ParameterExpression ObjParam = Expression.Parameter(Type);
				ParameterExpression ValueParam = Expression.Parameter(typeof(float));
				MemberExpression PropAccess = Expression.Property(Expression.Convert(ObjParam, Type), Prop);
				Property.SetValue = Expression.Lambda<Action<Role, float>>(Expression.Assign(PropAccess, ValueParam), ObjParam, ValueParam).Compile();
				Property.GetValue = Expression.Lambda<Func<Role, float>>(PropAccess, ObjParam).Compile();
			}
			Tips = string.Join(',', new[] { "SR-DMG" }.Concat(Properties.Select(s => s.NickName)).Concat(["增益", "转化"]));
		}
		public static bool GetType(string str)
		{
			if (str == null) return false;
			if (Map_Property.TryGetValue(str, out Property Info))
			{
				return Info.Accuracy == 1;
			}
			else return false;
		}
		public static string GetName(string str)
		{
			if (str == null) return null;
			if (Map_Property.TryGetValue(str, out Property Item))
			{
				return str == Item.NickName ? Item.PropertyName : Item.NickName;
			}
			else return null;
		}
		public void SetValue(string name, float value)
		{
			if (name == null) return;
			if (Map_Property.TryGetValue(name, out Property Item))
			{
				Item.SetValue(this, value);
			}
		}
		public float GetValue(string name)
		{
			if (name == null) return float.NaN;
			if (Map_Property.TryGetValue(name, out Property Item))
			{
				return Convert.ToSingle(Item.GetValue(this));
			}
			else return float.NaN;
		}
		// 外部访问
		public static List<Property> GetProperties()
		{
			return Properties;
		}
		public static string GetTips()
		{
			return Tips;
		}

		public class Property
		{
			public int Index;
			public string NickName;
			public string PropertyName;
			public Func<Role, float> GetValue;
			public Action<Role, float> SetValue;
			public int Accuracy;
			public Property() { }
			public Property(string name, string pname, int acc)
			{
				NickName = name;
				PropertyName = pname;
				Accuracy = acc;
			}
		}

	}
}