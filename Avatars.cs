using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SR_DMG
{
	public class Avatars
	{
		public string Name { get; set; }
		public string UID { get; set; }
		public List<Avatar> Avatar_List { get; set; }
	}

	public class Avatar
	{
		public string Name { set; get; }
		public int Level { set; get; }
		public string Element { set; get; }
		public int Rank { set; get; }
		public List<Propert> Properts { set; get; }
		public Servant Servant { set; get; }
		private static readonly string[] Prop_Name =
		[
			"生命值","攻击力","防御力","速度","暴击率","暴击伤害",
			"治疗提高","基础生命","基础攻击","基础防御","基础速度",
			"充能效率","效果命中","效果抵抗","伤害提高","击破特攻"
		];
		public static List<Propert> Get_Propert(TPropert[] _Properts, string Ele)
		{
			string[] Value = new string[16];
			List<Propert> Properts = [];
			foreach (TPropert Prop in _Properts)
			{
				if (Prop.Final == "0" || Prop.Final == "0.0%") continue;
				int index = Prop.Property_Type;
				if (index > 7)
				{
					if (index > 24) Value[15] = Prop.Final;
					else if (index >= 12)
					{
						if (Ele switch
						{
							"火" => 14,
							"冰" => 16,
							"雷" => 18,
							"风" => 20,
							"量子" => 22,
							"虚数" => 24,
							_ => 12

						} == index)
						{
							Value[14] = Prop.Final;
						}
					}
					else Value[index + 2] = Prop.Final;
				}
				else
				{
					Value[index - 1] = Prop.Final;
					if (index > 4) continue;
					Value[index + 6] = Prop.Base;
				}
			}
			for (int i = 0; i < Value.Length; i++)
			{
				if (Value[i] == null) continue;
				Properts.Add(new Propert
				{
					Name = Prop_Name[i],
					Value = Value[i]
				});
			}
			return Properts;
		}
		public static string Get_Element(string name)
		{
			return name switch
			{
				"quantum" => "量子",
				"imaginary" => "虚数",
				"fire" => "火",
				"ice" => "冰",
				"lightning" => "雷",
				"wind" => "风",
				_ => "物理"
			};
		}
	}

	public class Propert
	{
		public string Name { set; get; }
		public string Value { set; get; }
	}

	public class Servant
	{
		public string Name { set; get; }
		public List<Propert> Properts { set; get; }
	}

	public class TAvatar
	{
		[JsonPropertyName("name")] public string Name { set; get; }
		[JsonPropertyName("level")] public int Level { set; get; }
		[JsonPropertyName("element")] public string Element { set; get; }
		[JsonPropertyName("rank")] public int Rank { set; get; }
		[JsonPropertyName("properties")] public TPropert[] Properts { set; get; }
		[JsonPropertyName("servant_detail")] public TServant Servant { set; get; }
	}

	public class TPropert
	{
		[JsonPropertyName("property_type")] public int Property_Type { set; get; }
		[JsonPropertyName("base")] public string Base { set; get; }
		[JsonPropertyName("add")] public string Add { set; get; }
		[JsonPropertyName("final")] public string Final { set; get; }
	}

	public class TServant
	{
		[JsonPropertyName("servant_name")] public string Name { set; get; }
		[JsonPropertyName("servant_properties")] public TPropert[] Properts { set; get; }
	}

}
