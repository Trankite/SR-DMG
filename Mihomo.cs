using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR_DMG
{
	internal class Mihomo
	{
		public static HttpClient Http;
		const string APP_VERSION = "2.71.1";
		const string APP_ID = "bll8iq97cem8";
		const string ACT_ID = "e202304121516551";
		const string REF_URL = "https://app.mihoyo.com";
		const string SALT_4X = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
		const string SALT_6X = "t0qEgfub6cvueAPgR5m9aQWWVciEer7v";
		const string SALT_K2 = "rtvTthKxEyreVXQCnhluFgLXPOFKPHlA";
		const string SALT_LK2 = "EJncUPGnOHajenjLhBOsdpwEMZmiCmQX";
		const string PASS_URL = "https://passport-api.mihoyo.com/account/";
		const string AT_URL = "https://api-takumi.miyoushe.com/binding/api/";
		const string ATR_URL = "https://api-takumi-record.mihoyo.com/game_record/app/hkrpg/api/";
		const string FP_URL = "https://public-data-api.mihoyo.com/device-fp/api/getFp";
		const string SIGN_URL = "https://api-takumi.mihoyo.com/event/luna/hkrpg/";
		const string BBS_URL = "https://bbs-api.miyoushe.com/";
		const string MHM_URL = "https://api.mihomo.me/sr_info_parsed/";

		// 获取所有的 Token
		public static async Task Login()
		{
			try
			{
				Token Token = Get_Token(false) ?? new();
				(string Qr_Url, string Ticket) = await Get_QR_URL(Token);
				await Check_Login(Token, Qr_Url, Ticket);
				if (string.IsNullOrEmpty(Token.Stoken) ||
					!Program.Message("是否更新Token数据？", "Token")) return;
				await Get_LToken(Token);
				await Get_Cookie_Token(Token);
				await Get_Uid(Token);
				await Get_Fp(Token);
				if (Set_Token(Token))
				{
					Program.TipForm($"{Token.Nickname}UID:{Token.Uid}\n已更新 Token 信息", "Token");
				}
			}
			catch
			{
				Program.TipForm("意外错误：Token请求失败", "Token");
			}
		}

		// 获取米游社的签名字符串
		private static string Get_DS(bool Salt = false, string Body = "", string Query = "")
		{
			Random Rad = new();
			string r = Rad.Next(100001, 200001).ToString();
			string t = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
			string c = $"salt={(Salt ? SALT_K2 : SALT_LK2)}&t={t}&r={r}";
			if (Body.Length + Query.Length > 0)
			{
				if (Body.Length > 0) Body = SortJson(Body);
				if (Query.Length > 0) Query = string.Join("&", Query.Split('&').OrderBy(x => x));
				c = $"salt={(Salt ? SALT_6X : SALT_4X)}&t={t}&r={r}&b={Body}&q={Query}";
			}
			StringBuilder Str = new();
			foreach (byte bt in MD5.HashData(Encoding.UTF8.GetBytes(c)))
			{
				Str.Append(bt.ToString("x2"));
			}
			return $"{t},{r},{Str}";
		}
		private static string SortJson(string Json)
		{
			JsonNode Node = JsonNode.Parse(Json);
			SortJsonNode(Node);
			return Node.ToJsonString();
		}
		private static void SortJsonNode(JsonNode Node)
		{
			if (Node is JsonObject Obj)
			{
				List<KeyValuePair<string, JsonNode>> Props = [.. Obj.OrderBy(p => p.Key)];
				Obj.Clear();
				foreach (KeyValuePair<string, JsonNode> Prop in Props)
				{
					Obj[Prop.Key] = Prop.Value;
					SortJsonNode(Prop.Value);
				}
			}
			else if (Node is JsonArray Arr)
			{
				foreach (JsonNode Item in Arr)
				{
					if (Item != null) SortJsonNode(Item);
				}
			}
		}

		// 获取二维码 URL
		private static async Task<(string qr_url, string ticket)> Get_QR_URL(Token Token)
		{
			Token.Guid ??= Guid.NewGuid().ToString();
			string Rel = await HttpPost($"{PASS_URL}ma-cn-passport/app/createQRLogin", Head:
				["x-rpc-device_id", Token.Guid, "x-rpc-app_id", APP_ID]);
			if (Rel == null) return (null, null);
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			JsonElement Data = Doc.RootElement.GetProperty("data");
			string qr_url = Data.GetProperty("url").GetString();
			string ticket = Data.GetProperty("ticket").GetString();
			return (qr_url, ticket);
		}

		// 检查二维码登录状态
		private static async Task Check_Login(Token Token, string qr_url, string ticket)
		{
			Form Form = new()
			{
				Text = "扫码登录",
				Icon = Program.Icon,
				Size = new Size(300, 300),
				FormBorderStyle = FormBorderStyle.FixedSingle,
				StartPosition = FormStartPosition.CenterScreen,
				BackgroundImageLayout = ImageLayout.Center,
				BackgroundImage = new Bitmap(new QRCode(new QRCodeGenerator().CreateQrCode(
						qr_url, QRCodeGenerator.ECCLevel.Q)).GetGraphic(20), 200, 200),
				MaximizeBox = false,
				TopMost = true
			};
			Task Login = Task.Run(async () =>
			{
				while (true)
				{
					await Task.Delay(3000);
					if (!Form.Visible) return;
					string Rel = HttpPost($"{PASS_URL}ma-cn-passport/app/queryQRLoginStatus",
						JsonSerializer.Serialize(new { ticket }),
						["x-rpc-app_id", APP_ID, "x-rpc-device_id", Token.Guid]).Result;
					if (Rel == null) break;
					using JsonDocument Doc = JsonDocument.Parse(Rel);
					JsonElement Data = Doc.RootElement.GetProperty("data");
					if (Data.TryGetProperty("status", out JsonElement Status))
					{
						if (Status.GetString() == "Confirmed")
						{
							JsonElement User_Info = Data.GetProperty("user_info");
							Token.Aid = User_Info.GetProperty("aid").GetString();
							Token.Mid = User_Info.GetProperty("mid").GetString();
							Token.Stoken = Data.GetProperty("tokens").EnumerateArray().First().GetProperty("token").GetString();
						}
						else continue;
					}
					else
					{
						Program.TipForm("二维码超时失效\n请重新尝试扫码登录", "Token");
					}
					break;
				}
				Form.Close();
			});
			Form.ShowDialog();
			await Login;
			Form.Dispose();
		}

		// 获取 Cookie Token
		private static async Task Get_Cookie_Token(Token Token)
		{
			string body = JsonSerializer.Serialize(new
			{
				src_token = new
				{
					token = Token.Stoken,
					token_type = 1
				},
				mid = Token.Mid,
				dst_token_type = 4,
			});
			string Rel = await HttpPost($"{PASS_URL}ma-cn-session/app/exchange", body, ["x-rpc-app_id", APP_ID]);
			if (Rel == null) return;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (Retcode(Doc))
			{
				Token.Cookie_Token = Doc.RootElement.GetProperty("data").GetProperty("token").GetProperty("token").GetString();
			}
		}

		// 获取 LToken
		private static async Task Get_LToken(Token Token)
		{
			string Rel = await HttpGet($"{PASS_URL}auth/api/getLTokenBySToken",
				["Cookie", $"mid={Token.Mid};stoken={Token.Stoken};"]);
			if (Rel == null) return;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			Token.Ltoken = Doc.RootElement.GetProperty("data").GetProperty("ltoken").GetString();
		}

		// 获取 Uid
		private static async Task Get_Uid(Token Token)
		{
			string Rel = await HttpGet($"{AT_URL}getUserGameRolesByStoken",
				[
					"x-rpc-app_version",APP_VERSION,
					"x-rpc-client_type","2",
					"DS",Get_DS(true),
					"Cookie", $"mid={Token.Mid};stoken={Token.Stoken};"
				]);
			if (Rel == null) return;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			foreach (JsonElement List in Doc.RootElement.GetProperty("data").GetProperty("list").EnumerateArray())
			{
				string game_biz = List.GetProperty("game_biz").GetString();
				if (game_biz.StartsWith("hkrpg"))
				{
					Token.Game_Biz = game_biz;
					Token.Nickname = List.GetProperty("nickname").GetString();
					Token.Uid = List.GetProperty("game_uid").GetString();
					Token.Server = List.GetProperty("region").GetString();
					break;
				}
			}
		}

		// 获取 Fp
		public static async Task Get_Fp(Token Token)
		{
			string device = RadString(12);
			string product = RadString(6);
			Dictionary<string, object> extend = new()
			{
				{ "proxyStatus", 0 },
				{ "isRoot", 0 },
				{ "romCapacity", "512" },
				{ "deviceName", device },
				{ "productName", product },
				{ "romRemain", "512" },
				{ "hostname", "Android-XiaoMi" },
				{ "screenSize", "900x1600" },
				{ "isTablet", 0 },
				{ "aaid", string.Empty },
				{ "model", device },
				{ "brand", "XiaoMi" },
				{ "hardware", "XiaoMi" },
				{ "deviceType", product },
				{ "devId", "REL" },
				{ "serialNumber", "unknown" },
				{ "sdCapacity", 512000 },
				{ "buildTime", "1600000000000" },
				{ "buildUser", "android-build" },
				{ "simState", 5 },
				{ "ramRemain", "128000" },
				{ "appUpdateTimeDiff", 1600000000000 },
				{ "deviceInfo", "XiaoMi" },
				{ "vaid", string.Empty },
				{ "buildType", "user" },
				{ "sdkVersion", "32" },
				{ "ui_mode", "UI_MODE_TYPE_NORMAL" },
				{ "isMockLocation", 0 },
				{ "cpuType", "arm64-v8a" },
				{ "isAirMode", 0 },
				{ "ringMode", 2 },
				{ "chargeStatus", 1 },
				{ "manufacturer", "XiaoMi" },
				{ "emulatorStatus", 0 },
				{ "appMemory", "512" },
				{ "osVersion", "12" },
				{ "vendor", "unknown" },
				{ "accelerometer", "0.10000000x9.800000x0.2000000" },
				{ "sdRemain",  240000 },
				{ "buildTags", "release-keys" },
				{ "packageName", "com.mihoyo.hyperion" },
				{ "networkType", "WiFi" },
				{ "oaid", string.Empty },
				{ "debugStatus", 1 },
				{ "ramCapacity", "460000" },
				{ "magnetometer", "15.000x-28.00x-32.000" },
				{ "display", $"{product} release-keys" },
				{ "appInstallTimeDiff", 1600000000000 },
				{ "packageVersion", "2.35.0" },
				{ "gyroscope", "0.0x0.0x0.0" },
				{ "batteryStatus", 100 },
				{ "hasKeyboard", 0 },
				{ "board", device}
			};
			string device_id = RadString(16);
			string seed_id = Guid.NewGuid().ToString();
			string seed_time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
			string platform = "2";
			string device_fp = RadString(13);
			string app_name = "bbs_cn";
			string ext_fields = JsonSerializer.Serialize(extend);
			string bbs_device_id = Guid.NewGuid().ToString();
			string Rel = await HttpPost(FP_URL, JsonSerializer.Serialize(
				new { device_id, seed_id, seed_time, platform, device_fp, app_name, ext_fields, bbs_device_id }));
			if (Rel == null) return;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			Token.Device_Fp = Doc.RootElement.GetProperty("data").GetProperty("device_fp").GetString();
		}

		// 获取 标识符
		private static string RadString(int length)
		{
			Span<char> Sp = stackalloc char[length];
			Random.Shared.GetItems(length > 12 ? "0123456789abcdef" : "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", Sp);
			return Sp.ToString();
		}

		// 获取角色信息
		public static async Task<string> Get_Roles(Token Token)
		{
			string query = $"server={Token.Server}&role_id={Token.Uid}";
			string Rel = await HttpGet($"{ATR_URL}avatar/info?{query}",
				[
					"x-rpc-app_version", APP_VERSION,
					"x-rpc-device_fp", Token.Device_Fp,
					"x-rpc-client_type", "5",
					"DS", Get_DS(Query: query),
					"Cookie", $"ltuid={Token.Aid};ltoken={Token.Ltoken};"
				]);
			if (Rel == null) return null;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (!Retcode(Doc)) return null;
			Avatars Avatars = new() { Name = Token.Nickname, UID = Token.Uid, Avatar_List = [] };
			foreach (JsonElement List in Doc.RootElement.GetProperty("data").GetProperty("avatar_list").EnumerateArray())
			{
				TAvatar TAvt = JsonSerializer.Deserialize<TAvatar>(List);
				Rel = Avatar.Get_Element(TAvt.Element);
				Avatars.Avatar_List.Add(new Avatar
				{
					Name = TAvt.Name,
					Level = TAvt.Level,
					Element = Rel,
					Rank = TAvt.Rank,
					Properts = Avatar.Get_Propert(TAvt.Properts, Rel),
					Servant = TAvt.Servant == null ?
					new Servant
					{
						Name = "",
						Properts = []
					} :
					new Servant
					{
						Name = TAvt.Servant.Name,
						Properts = Avatar.Get_Propert(TAvt.Servant.Properts, Rel)
					}
				});
			}
			Rel = JsonSerializer.Serialize(Avatars, JsonSopt());
			if (Avatars.Avatar_List[0].Properts.Count > 0)
			{
				string FilePath = Program.GetPath(Program.AppPath.Data, Token.Uid, true);
				try
				{
					File.WriteAllText(FilePath, Rel);
				}
				catch
				{
					Program.TipForm($"文件保存失败\n{FilePath}"); return null;
				}
			}
			return Rel;
		}

		// Mihomo API
		public static async Task<Avatars> Get_Roles(string UID)
		{
			Avatars Avts;
			string Rel = await HttpGet($"{MHM_URL}{UID}?lang=cn");
			if (Rel == null) return null;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (Doc.RootElement.TryGetProperty("player", out JsonElement Player))
			{
				Avts = new Avatars
				{
					Name = Player.GetProperty("nickname").GetString(),
					UID = Player.GetProperty("uid").GetString(),
					Avatar_List = []
				};
			}
			else { Program.TipForm($"UID不存在\n{UID}"); return null; }
			foreach (JsonElement Characters in Doc.RootElement.GetProperty("characters").EnumerateArray())
			{
				Avatar Avt = new()
				{
					Name = Characters.GetProperty("name").GetString(),
					Level = Characters.GetProperty("level").GetInt32(),
					Element = Characters.GetProperty("element").GetProperty("name").GetString(),
					Rank = Characters.GetProperty("rank").GetInt32(),
					Properts = [],
					Servant = new Servant { Name = "", Properts = [] }
				};
				float[] Vals = new float[6];
				foreach (JsonElement Atr in Characters.GetProperty("attributes").EnumerateArray())
				{
					switch (Atr.GetProperty("name").GetString())
					{
						case "生命值":
							Vals[0] = Atr.GetProperty("value").GetSingle();
							Avt.Properts.Add(new Propert { Name = "基础生命", Value = $"{(int)Vals[0]}" });
							break;
						case "攻击力":
							Vals[1] = Atr.GetProperty("value").GetSingle();
							Avt.Properts.Add(new Propert { Name = "基础攻击", Value = $"{(int)Vals[1]}" });
							break;
						case "防御力":
							Vals[2] = Atr.GetProperty("value").GetSingle();
							Avt.Properts.Add(new Propert { Name = "基础防御", Value = $"{(int)Vals[2]}" });
							break;
						case "速度":
							Vals[3] = Atr.GetProperty("value").GetSingle();
							Avt.Properts.Add(new Propert { Name = "基础速度", Value = $"{(int)Vals[3]}" });
							break;
						case "暴击率":
							Vals[4] = Atr.GetProperty("value").GetSingle() * 100;
							break;
						case "暴击伤害":
							Vals[5] = Atr.GetProperty("value").GetSingle() * 100;
							break;
					}
				}
				foreach (JsonElement Additons in Characters.GetProperty("additions").EnumerateArray())
				{
					switch (Additons.GetProperty("name").GetString())
					{
						case "生命值":
							Vals[0] += Additons.GetProperty("value").GetSingle();
							break;
						case "攻击力":
							Vals[1] += Additons.GetProperty("value").GetSingle();
							break;
						case "防御力":
							Vals[2] += Additons.GetProperty("value").GetSingle();
							break;
						case "速度":
							Vals[3] += Additons.GetProperty("value").GetSingle();
							break;
						case "暴击率":
							Vals[4] += Additons.GetProperty("value").GetSingle() * 100;
							break;
						case "暴击伤害":
							Vals[5] += Additons.GetProperty("value").GetSingle() * 100;
							break;
						case "能量恢复效率":
							Avt.Properts.Add(new Propert { Name = "充能效率", Value = $"{Additons.GetProperty("value").GetSingle() * 100 + 100:0.#}%" });
							break;
						case "效果命中":
							Avt.Properts.Add(new Propert { Name = "效果命中", Value = $"{Additons.GetProperty("value").GetSingle() * 100:0.#}%" });
							break;
						case "效果抵抗":
							Avt.Properts.Add(new Propert { Name = "效果抵抗", Value = $"{Additons.GetProperty("value").GetSingle() * 100:0.#}%" });
							break;
						case "击破特攻":
							Avt.Properts.Add(new Propert { Name = "击破特攻", Value = $"{Additons.GetProperty("value").GetSingle() * 100:0.#}%" });
							break;
						default:
							if (Additons.GetProperty("name").GetString().StartsWith(Avt.Element))
								Avt.Properts.Add(new Propert { Name = "增伤", Value = $"{Additons.GetProperty("value").GetSingle() * 100:0.#}%" });
							break;
					}
				}
				Avt.Properts.Add(new Propert { Name = "生命值", Value = $"{(int)Vals[0]}" });
				Avt.Properts.Add(new Propert { Name = "攻击力", Value = $"{(int)Vals[1]}" });
				Avt.Properts.Add(new Propert { Name = "防御力", Value = $"{(int)Vals[2]}" });
				Avt.Properts.Add(new Propert { Name = "速度", Value = $"{(int)Vals[3]}" });
				Avt.Properts.Add(new Propert { Name = "暴击率", Value = $"{Vals[4]:0.#}%" });
				Avt.Properts.Add(new Propert { Name = "暴击伤害", Value = $"{Vals[5]:0.#}%" });
				Avts.Avatar_List.Add(Avt);
			}
			string FilePath = Program.GetPath(Program.AppPath.Data, UID, true);
			try
			{
				File.WriteAllText(FilePath, JsonSerializer.Serialize(Avts, JsonSopt()));
			}
			catch
			{
				Program.TipForm($"文件保存失败\n{FilePath}"); return null;
			}
			return Avts;
		}

		// 实时便筏
		public static async Task<string> Get_Note()
		{
			Token Token = Get_Token();
			if (Token == null) return null;
			string query = $"server={Token.Server}&role_id={Token.Uid}";
			string Rel = await HttpGet($"{ATR_URL}note?{query}",
				[
					"x-rpc-app_version",APP_VERSION,
					"x-rpc-device_fp",Token.Device_Fp,
					"x-rpc-client_type","5",
					"DS",Get_DS(Query:query),
					"Cookie", $"ltuid={Token.Aid};ltoken={Token.Ltoken};"
				]);
			if (Rel == null) return null;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (Retcode(Doc))
			{
				JsonElement Data = Doc.RootElement.GetProperty("data");
				int Stamina = Data.GetProperty("current_stamina").GetInt32();
				int Max_Stamina = Data.GetProperty("max_stamina").GetInt32();
				int Recover_Time = Data.GetProperty("stamina_recover_time").GetInt32();
				int Full_Ts = Data.GetProperty("stamina_full_ts").GetInt32();
				DateTime Now_Date = DateTime.Today;
				DateTime Full_Time = DateTimeOffset.FromUnixTimeSeconds(Full_Ts).LocalDateTime;
				string Str = Now_Date.Day == Full_Time.Day ? "今天" : Now_Date.AddDays(1).Day == Full_Time.Day ? "明天" : "后天";
				Str += $" {Full_Time:HH:mm} 回满\n{(Recover_Time / 3600).ToString().PadLeft(2, '0')} 时 {(Recover_Time % 3600 / 60).ToString().PadLeft(2, '0')} 分";
				return $"开拓力：{Stamina} / {Max_Stamina}\n{Str}";
			}
			else return null;
		}

		// 每日签到
		public static async Task<string> DoSign()
		{
			Token Token = Get_Token();
			if (Token == null) return null;
			(int Sign_Day, int Today, bool IsSign) = await Get_Sign_Info(Token);
			if (!IsSign)
			{
				string body = JsonSerializer.Serialize(new
				{
					act_id = ACT_ID,
					region = Token.Server,
					uid = Token.Uid,
					lang = "zh-cn"
				});
				string[] Cookie = ["Cookie", $"account_mid_v2={Token.Mid};cookie_token_v2={Token.Cookie_Token};"];
				string Rel = await HttpPost($"{SIGN_URL}sign", body, Cookie);
				if (Rel == null) return null;
				using JsonDocument Doc = JsonDocument.Parse(Rel);
				if (Retcode(Doc)) Sign_Day++;
			}
			return $"已签到 {Sign_Day} / {Today} 天\n{Get_Sign_Home(await Get_Sign_Home(), Today)}";
		}
		// 签到奖励
		private static string Get_Sign_Home(List<string> SignHome, int Day)
		{
			return Day > 0 && Day < SignHome.Count + 1 ? SignHome[Day - 1] : "???";
		}
		private static async Task<List<string>> Get_Sign_Home()
		{
			List<string> Sign_Home = [];
			string Rel = await HttpGet($"{SIGN_URL}home?lang=zh-cn&act_id={ACT_ID}");
			if (Rel == null) return null;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (Retcode(Doc))
			{
				foreach (JsonElement Item in Doc.RootElement.GetProperty("data").GetProperty("awards").EnumerateArray())
				{
					int Num = Item.GetProperty("cnt").GetInt32();
					Sign_Home.Add($"{Item.GetProperty("name").GetString()}{(Num > 1 ? $" × {Num}" : string.Empty)}");
				}
				return Sign_Home.Count > 0 ? Sign_Home : null;
			}
			else return null;
		}
		// 签到信息
		private static async Task<(int Sign_Day, int Today, bool IsSign)> Get_Sign_Info(Token Token)
		{
			string Rel = await HttpGet($"{SIGN_URL}info?lang=zh-cn&act_id={ACT_ID}&region={Token.Server}&uid={Token.Uid}",
				["Cookie", $"account_mid_v2={Token.Mid};cookie_token_v2={Token.Cookie_Token};"]);
			if (Rel == null) return (-1, -1, false);
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (Retcode(Doc))
			{
				JsonElement Data = Doc.RootElement.GetProperty("data");
				int Sign_Day = Data.GetProperty("total_sign_day").GetInt32();
				int Today = DateTime.Parse(Data.GetProperty("today").GetString()).Day;
				bool IsSign = Data.GetProperty("is_sign").GetBoolean();
				return (Sign_Day, Today, IsSign);
			}
			else return (-1, -1, false);
		}

		// 米游币任务
		public static async Task<string> DoCoin()
		{
			Token Token = Get_Token();
			if (Token == null) return null;
			int[] State = await Get_State(Token);
			if (State == null) return null;
			if (State[0] < 1) await DoSignIn(Token);
			if (State[3] < 1 || State[2] < 5 || State[1] < 3)
			{
				List<string> News_List = await Get_News_List();
				if (News_List == null) return null;
				if (State[3] < 1)
				{
					if (!await DoShare(Token, News_List[0])) return null;
				}
				if (State[1] < 3)
				{
					for (int i = 0; i < 3 - State[1]; i++)
					{
						if (!await Get_Page(Token, News_List[i])) return null;
					}
				}
				if (State[2] < 5)
				{
					for (int n = 0; n < 2; n++)
					{
						for (int i = 0; i < 5 - State[2]; i++)
						{
							if (!await DoUpvote(Token, News_List[i], n == 0)) return null;
							if (i < 4 - State[2]) await Task.Delay(100);
						}
					}
				}
				else await Task.Delay(100);
			}
			if (State[6] > 0) State = await Get_State(Token);
			if (State == null) return null;
			return $"米游币：{State[5]}"
				+ $"\n今日奖励：{State[4]}"
				+ $"\n任务进度：{(State[6] > 0 ? "未完成" : "已完成")}"
				+ $"\n打卡：{State[0]} / 1"
				+ $"\n浏览：{State[1]} / 3"
				+ $"\n点赞：{State[2]} / 5"
				+ $"\n分享：{State[3]} / 1";
		}
		// 任务进度
		private static async Task<int[]> Get_State(Token Token)
		{
			string Rel = await HttpGet($"{BBS_URL}apihub/wapi/getUserMissionsState?point_sn=myb",
				["Cookie", $"ltuid={Token.Aid};ltoken={Token.Ltoken};"]);
			if (Rel == null) return null;
			int[] State = new int[7];
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			if (Retcode(Doc))
			{
				JsonElement Data = Doc.RootElement.GetProperty("data");
				foreach (JsonElement Item in Data.GetProperty("states").EnumerateArray())
				{
					int mission_id = Item.GetProperty("mission_id").GetInt32();
					if (mission_id > 57 && mission_id < 62)
					{
						State[mission_id - 58] = Item.GetProperty("happened_times").GetInt32();
					}
				}
				State[4] = Data.GetProperty("already_received_points").GetInt32();
				State[5] = Data.GetProperty("total_points").GetInt32();
				State[6] = Data.GetProperty("can_get_points").GetInt32();
				return State;
			}
			else return null;
		}
		// 板块签到
		private static async Task<bool> DoSignIn(Token Token)
		{
			string body = JsonSerializer.Serialize(new { gids = "6" });
			string Rel = await HttpPost($"{BBS_URL}apihub/app/api/signIn", body,
				[
					"Referer", REF_URL,
					"x-rpc-app_version", APP_VERSION,
					"x-rpc-client_type", "2",
					"DS", Get_DS(true, Body:body),
					"Cookie", $"mid={Token.Mid};stoken={Token.Stoken};"
				]);
			if (Rel == null) return false;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			return Retcode(Doc, [0, 1008]);
		}
		// 最新帖子 列表
		private static async Task<List<string>> Get_News_List()
		{
			List<string> News_List = [];
			string Rel = await HttpGet($"{BBS_URL}painter/api/getRecentForumPostList?forum_id=52&sort_type=2&page_size=5");
			if (Rel == null) return null;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			foreach (JsonElement Item in Doc.RootElement.GetProperty("data").GetProperty("list").EnumerateArray())
			{
				News_List.Add(Item.GetProperty("post").GetProperty("post_id").GetString());
			}
			return News_List;
		}
		// 分享
		private static async Task<bool> DoShare(Token Token, string Entity_id)
		{
			string query = $"entity_type=1&entity_id={Entity_id}";
			string Rel = await HttpGet($"{BBS_URL}apihub/api/getShareConf?{query}",
				[
					"Referer", REF_URL,
					"x-rpc-app_version", APP_VERSION,
					"x-rpc-client_type", "2",
					"DS", Get_DS(true),
					"Cookie", $"mid={Token.Mid};stoken={Token.Stoken};"
				]);
			if (Rel == null) return false;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			return Retcode(Doc, [0, -1]);
		}
		// 点赞
		private static async Task<bool> DoUpvote(Token Token, string Entity_id, bool Upvote_Type)
		{
			string body = JsonSerializer.Serialize(new
			{
				csm_source = "discussion",
				is_cancel = !Upvote_Type,
				post_id = Entity_id,
				upvote_type = Upvote_Type ? "1" : "0"
			});
			string Rel = await HttpPost($"{BBS_URL}post/api/post/upvote", body,
				[
					"Referer", REF_URL,
					"x-rpc-app_version", APP_VERSION,
					"x-rpc-client_type", "2",
					"DS", Get_DS(true),
					"Cookie", $"mid={Token.Mid};stoken={Token.Stoken};"
				]);
			if (Rel == null) return false;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			return Retcode(Doc, [0, 1101]);
		}
		// 浏览
		private static async Task<bool> Get_Page(Token Token, string Entity_id)
		{
			string Rel = await HttpGet($"{BBS_URL}post/api/getPostFull?post_id={Entity_id}",
				[
					"Referer", REF_URL,
					"x-rpc-app_version", APP_VERSION,
					"x-rpc-client_type", "2",
					"DS", Get_DS(true),
					"Cookie", $"mid={Token.Mid};stoken={Token.Stoken};"
				]);
			if (Rel == null) return false;
			using JsonDocument Doc = JsonDocument.Parse(Rel);
			return Retcode(Doc);
		}

		// Http请求
		private static async Task<string> HttpPost(string Url, string Body = null, string[] Head = null)
		{
			return await Get_Http(true, Url, Head, Body);
		}
		private static async Task<string> HttpGet(string Url, string[] Head = null)
		{
			return await Get_Http(false, Url, Head);
		}
		private static async Task<string> Get_Http(bool IsPost, string Url, string[] Head = null, string Body = null)
		{
			try
			{
				Http ??= new HttpClient()
				{
					Timeout = TimeSpan.FromSeconds(10)
				};
				HttpRequestMessage Request = new(IsPost ? HttpMethod.Post : HttpMethod.Get, Url);
				if (IsPost && Body != null)
				{
					Request.Content = new StringContent(Body, Encoding.UTF8, "application/json");
				}
				if (Head != null)
				{
					for (int i = 0; i < Head.Length; i += 2)
					{
						Request.Headers.Add(Head[i], Head[i + 1]);
					}
				}
				using HttpResponseMessage Response = await Http.SendAsync(Request);
				return await Response.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				Program.TipForm($"网络错误\n{e.Message}"); return null;
			}
		}

		// API返回信息
		private static bool Retcode(JsonDocument Doc, int[] IsOk = null)
		{
			int RetCode = Doc.RootElement.GetProperty("retcode").GetInt32();
			if (IsOk?.Contains(RetCode) ?? RetCode == 0) return true;
			else
			{
				string Mes = RetCode switch
				{
					10001 or -100 => "登录失效：请重新登录",
					1034 => "账号风控：请前往米游社完成人机验证",
					_ => Doc.RootElement.GetProperty("message").GetString()
				};
				Program.TipForm($"错误代码：{RetCode}\n{Mes}", "网页消息");
				return false;
			}
		}

		public static Token Get_Token(bool NotNull = true)
		{
			string FilePath = Program.GetPath(Program.AppPath.Token);
			try
			{
				if (File.Exists(FilePath))
				{
					return JsonSerializer.Deserialize<Token>(File.ReadAllText(FilePath));
				}
				else { if (NotNull) Program.TipForm("未登录\n请先扫码登录 ( Login )"); return null; }
			}
			catch
			{
				if (NotNull) Program.TipForm($"文件读取失败\n{FilePath}"); return null;
			}
		}
		public static bool Set_Token(Token Token)
		{
			string FilePath = Program.GetPath(Program.AppPath.Token, IsCreate: true);
			try
			{
				File.WriteAllText(FilePath, JsonSerializer.Serialize(Token, JsonSopt())); return true;
			}
			catch
			{
				Program.TipForm($"文件保存失败\n{FilePath}", "Token"); return false;
			}
		}

		// 序列化
		private static JsonSerializerOptions JsonSopt()
		{
			return new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
		}

		public class Token
		{
			public string Nickname { set; get; }
			public string Aid { set; get; }
			public string Uid { set; get; }
			public string Mid { set; get; }
			public string Server { set; get; }
			public string Game_Biz { set; get; }
			public string Device_Fp { set; get; }
			public string Guid { set; get; }
			public string Cookie_Token { set; get; }
			public string Stoken { set; get; }
			public string Ltoken { set; get; }
		}

	}

}