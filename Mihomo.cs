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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR_DMG
{
	internal class Mihomo
	{
		const string BBS_VERSION = "2.71.1";
		const string BBS_MOBILE = "Mozilla/5.0 (Linux; Android 12) Mobile miHoYoBBS";
		const string SALT_4X = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
		const string SALT_LK2 = "EJncUPGnOHajenjLhBOsdpwEMZmiCmQX";
		const string QR_URL = "https://passport-api.mihoyo.com/";
		const string AT_URL = "https://api-takumi.mihoyo.com/binding/api/";
		const string ATR_URL = "https://api-takumi-record.mihoyo.com/game_record/app/hkrpg/api/";
		const string FP_URL = "https://public-data-api.mihoyo.com/device-fp/api/getFp";
		const string AUTH_URL = "https://api-takumi.miyoushe.com/binding/api/genAuthKey";
		const string GACHA_URL = "https://public-operation-hkrpg.mihoyo.com/common/gacha_record/api/getGachaLog";

		// 获取所有的 Token
		public static async void Login()
		{
			try
			{
				Token Token = new Token();
				(string device_id, string qr_url, string ticket) = await Get_QR_URL();
				(Token.aid, Token.mid, Token.stoken) = await Check_Login(qr_url, ticket);
				if (Token.stoken == null) ErorrTip<int>(-103);
				else
				{
					Token.cookie_token = await Get_Cookie_Token(Token);
					Token.ltoken = await Get_LToken(Token);
					(Token.nickname, Token.uid, Token.server, Token.game_biz) = await Get_Uid(Token);
					Token.device_fp = await Get_Fp();
					try
					{
						File.WriteAllText(SR_DMG.App_Path[3],
							JsonSerializer.Serialize(Token, JsonSopt()));
						ErorrTip<int>(0, $"UID:{Token.uid} {Token.nickname}\n已保存 Token 信息", "Token");
					}
					catch
					{
						ErorrTip<int>(-1002, $"：{SR_DMG.App_Path[3]}", "Token");
					}
				}
			}
			catch
			{
				ErorrTip<int>(-1005, "\n请求Token失败", "Token");
			}
		}

		// 获取米游社的签名字符串
		private static string Get_DS(string body = "", string query = "")
		{
			Random Rad = new Random();
			string r = Rad.Next(100001, 200001).ToString();
			string t = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
			string c = $"salt={SALT_LK2}&t={t}&r={r}";
			if (body.Length + query.Length > 0)
			{
				query = string.Join("&", query.Split('&').OrderBy(x => x));
				c = $"salt={SALT_4X}&t={t}&r={r}&b={body}&q={query}";
			}
			using (MD5 md5 = MD5.Create())
			{
				StringBuilder Str = new StringBuilder();
				foreach (byte bt in md5.ComputeHash(Encoding.UTF8.GetBytes(c)))
				{
					Str.Append(bt.ToString("x2"));
				}
				return $"{t},{r},{Str.ToString()}";
			}
		}

		// 获取二维码 URL
		private static async Task<(string device_id, string qr_url, string ticket)> Get_QR_URL()
		{
			string device_id = Uuid_V4();
			string Rel = await HttpPost($"{QR_URL}account/ma-cn-passport/app/createQRLogin", Head:
				new string[] {
				"x-rpc-device_id", device_id,
				"x-rpc-app_id", "ddxf5dufpuyo" });
			if (Rel == null) return (null, null, null);
			using (JsonDocument Doc = JsonDocument.Parse(Rel))
			{
				JsonElement Data = Doc.RootElement.GetProperty("data");
				string qr_url = Data.GetProperty("url").GetString();
				string ticket = Data.GetProperty("ticket").GetString();
				return (device_id, qr_url, ticket);
			}
		}

		// 检查二维码登录状态
		private static async Task<(string aid, string mid, string stoken)> Check_Login(string qr_url, string ticket)
		{
			Form Form = Show_Qrcode(qr_url);
			while (true)
			{
				await Task.Delay(1000);
				if (Form.IsDisposed) return (null, null, null);
				string Rel = await HttpPost($"{QR_URL}account/ma-cn-passport/app/queryQRLoginStatus",
					JsonSerializer.Serialize(new { ticket }));
				if (Rel == null) return (null, null, null);
				using (JsonDocument Doc = JsonDocument.Parse(Rel))
				{
					JsonElement Data = Doc.RootElement.GetProperty("data");
					if (Data.ValueKind == JsonValueKind.Null)
					{
						return (null, null, null);
					}
					else
					{
						switch (Data.GetProperty("status").GetString())
						{
							case "Init":
								break;
							case "Scanned":
								break;
							case "Confirmed":
								Form.Close();
								JsonElement User_info = Data.GetProperty("user_info");
								string aid = User_info.GetProperty("aid").GetString();
								string mid = User_info.GetProperty("mid").GetString();
								string stoken = Data.GetProperty("tokens").EnumerateArray().First().GetProperty("token").GetString();
								return (aid, mid, stoken);
							default:
								break;
						}
					}
				}
			}
		}

		// 显示二维码
		private static Form Show_Qrcode(string qr_url)
		{
			Form Form = new Form
			{
				Text = "扫码登录",
				Size = new Size(300, 300),
				FormBorderStyle = FormBorderStyle.FixedSingle,
				StartPosition = FormStartPosition.CenterScreen,
				BackgroundImageLayout = ImageLayout.Center,
				BackgroundImage = new Bitmap(new QRCode(new QRCodeGenerator().CreateQrCode(
					qr_url, QRCodeGenerator.ECCLevel.Q)).GetGraphic(20), 200, 200),
				MaximizeBox = false,
				TopMost = true
			};
			Form.Show();
			return Form;
		}

		// 获取 Cookie Token
		private static async Task<string> Get_Cookie_Token(Token Token)
		{
			string Rel = await HttpGet($"{QR_URL}account/auth/api/getCookieAccountInfoBySToken",
				new string[] { "cookie", $"mid={Token.mid};stoken={Token.stoken};" });
			if (Rel == null) return null;
			using (JsonDocument Doc = JsonDocument.Parse(Rel))
			{
				return Doc.RootElement.GetProperty("data").GetProperty("cookie_token").GetString();
			}
		}

		// 获取 LToken
		private static async Task<string> Get_LToken(Token Token)
		{
			string Rel = await HttpGet($"{QR_URL}account/auth/api/getLTokenBySToken",
				new string[] { "cookie", $"mid={Token.mid};stoken={Token.stoken};" });
			if (Rel == null) return null;
			using (JsonDocument Doc = JsonDocument.Parse(Rel))
			{
				return Doc.RootElement.GetProperty("data").GetProperty("ltoken").GetString();
			}
		}

		// 获取 祈愿记录URL
		public static async Task<string> Get_GachaLog()
		{
			if (File.Exists(SR_DMG.App_Path[3]))
			{
				Token Token;
				try
				{
					Token = JsonSerializer.Deserialize<Token>(File.ReadAllText(SR_DMG.App_Path[3]));
				}
				catch
				{
					return ErorrTip<string>(-1001, $"：{SR_DMG.App_Path[3]}");
				}
				string Rel = await HttpPost(AUTH_URL, JsonSerializer.Serialize(
					new { Token.game_biz, game_uid = int.Parse(Token.uid), region = Token.server, auth_appid = "webview_gacha" }),
					new string[]
					{
						"x-rpc-app_version", BBS_VERSION,
						"x-rpc-client_type", "5",
						"DS", Get_DS(),
						"Cookie", $"mid={Token.mid};stoken={Token.stoken};"
					});
				if (Rel == null) return ErorrTip<string>(-105);
				using (JsonDocument Doc = JsonDocument.Parse(Rel))
				{
					int Code = Doc.RootElement.GetProperty("retcode").GetInt32();
					if (Code == -100) return ErorrTip<string>(-102);
					string authkey = Doc.RootElement.GetProperty("data").GetProperty("authkey").GetString();
					return ($"{GACHA_URL}?authkey_ver=1&lang=zh-cn&game_biz={Token.game_biz}&authkey={authkey}");
				}
			}
			else return ErorrTip<string>(-101);
		}

		// 获取 Uid
		private static async Task<(string nickname, string uid, string server, string game_biz)> Get_Uid(Token Token)
		{
			string Rel = await HttpGet($"{AT_URL}getUserGameRolesByCookie",
				new string[] { "cookie", $"account_id={Token.aid};cookie_token={Token.cookie_token};" });
			if (Rel == null) return (null, null, null, null);
			using (JsonDocument Doc = JsonDocument.Parse(Rel))
			{
				foreach (JsonElement List in Doc.RootElement.GetProperty("data").GetProperty("list").EnumerateArray())
				{
					string game_biz = List.GetProperty("game_biz").GetString();
					if (game_biz.StartsWith("hkrpg"))
					{
						string nickname = List.GetProperty("nickname").GetString();
						string uid = List.GetProperty("game_uid").GetString();
						string server = List.GetProperty("region").GetString();
						return (nickname, uid, server, game_biz);
					}
				}
				return (null, null, null, null);
			}
		}

		// 获取 Fp
		public static async Task<string> Get_Fp()
		{
			string device = RadString(12);
			string product = RadString(6);
			Dictionary<string, object> extendProperties = new()
			{
				{ "proxyStatus", 0 },
				{ "isRoot", 0 },
				{ "romCapacity", "512" },
				{ "deviceName", device },
				{ "productName", product },
				{ "romRemain", "512" },
				{ "hostname", "dg02-pool03-kvm87" },
				{ "screenSize", "1440x2905" },
				{ "isTablet", 0 },
				{ "aaid", string.Empty },
				{ "model", device },
				{ "brand", "XiaoMi" },
				{ "hardware", "qcom" },
				{ "deviceType", "OP5913L1" },
				{ "devId", "REL" },
				{ "serialNumber", "unknown" },
				{ "sdCapacity", 512215 },
				{ "buildTime", "1693626947000" },
				{ "buildUser", "android-build" },
				{ "simState", 5 },
				{ "ramRemain", "239814" },
				{ "appUpdateTimeDiff", 1702604034482 },
				{ "deviceInfo", $@"XiaoMi\/{product}\/OP5913L1:13\/SKQ1.221119.001\/T.118e6c7-5aa23-73911:user\/release-keys" },
				{ "vaid", string.Empty },
				{ "buildType", "user" },
				{ "sdkVersion", "34" },
				{ "ui_mode", "UI_MODE_TYPE_NORMAL" },
				{ "isMockLocation", 0 },
				{ "cpuType", "arm64-v8a" },
				{ "isAirMode", 0 },
				{ "ringMode", 2 },
				{ "chargeStatus", 1 },
				{ "manufacturer", "XiaoMi" },
				{ "emulatorStatus", 0 },
				{ "appMemory", "512" },
				{ "osVersion", "14" },
				{ "vendor", "unknown" },
				{ "accelerometer", "1.4883357x7.1712894x6.2847486" },
				{ "sdRemain",  239600 },
				{ "buildTags", "release-keys" },
				{ "packageName", "com.mihoyo.hyperion" },
				{ "networkType", "WiFi" },
				{ "oaid", string.Empty },
				{ "debugStatus", 1 },
				{ "ramCapacity", "469679" },
				{ "magnetometer", "20.081251x-27.487501x2.1937501" },
				{ "display", $"{product}_13.1.0.181(CN01)" },
				{ "appInstallTimeDiff", 1688455751496 },
				{ "packageVersion", "2.20.1" },
				{ "gyroscope", "0.030226856x0.014647375x0.010652636" },
				{ "batteryStatus", 100 },
				{ "hasKeyboard", 0 },
				{ "board", "taro" },
			};
			string device_id = RadString(16);
			string seed_id = Guid.NewGuid().ToString();
			string platform = "2";
			string seed_time = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
			string ext_fields = JsonSerializer.Serialize(extendProperties);
			string app_name = "bbs_cn";
			string bbs_device_id = Guid.NewGuid().ToString();
			string device_fp = RadString(13);
			string Rel = await HttpPost(FP_URL, JsonSerializer.Serialize(
				new { device_id, seed_id, platform, seed_time, ext_fields, app_name, bbs_device_id, device_fp }));
			if (Rel == null) return null;
			using (JsonDocument Doc = JsonDocument.Parse(Rel))
			{
				return Doc.RootElement.GetProperty("data").GetProperty("device_fp").GetString();
			}
		}

		// 获取 UUID V4
		private static string Uuid_V4()
		{
			string UUID = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx";
			StringBuilder Str = new StringBuilder();
			Random Rd = new Random();
			foreach (var c in UUID)
			{
				if (c == 'x')
				{
					Str.Append(Rd.Next(0, 16).ToString("x"));
				}
				else if (c == 'y')
				{
					Str.Append(((Rd.Next(0, 16) & 0x03) | 0x08).ToString("x"));
				}
				else
				{
					Str.Append(c);
				}
			}
			return Str.ToString();
		}

		// 获取 标识符
		private static string RadString(int length, int flag = 0)
		{
			Span<char> Sp = stackalloc char[length];
			System.Random.Shared.GetItems(length > 12 ? "0123456789abcdef" : "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", Sp);
			return Sp.ToString();
		}

		// 获取角色信息
		public static async Task<string> Get_Roles(Token Token)
		{
			string query = $"server={Token.server}&role_id={Token.uid}";
			string Rel = await HttpGet($"{ATR_URL}avatar/info?{query}",
				new string[] {
					"x-rpc-app_version", BBS_VERSION,
					"x-rpc-device_fp", Token.device_fp,
					"x-rpc-client_type", "5",
					"DS", Get_DS(query: query),
					"cookie", $"ltuid={Token.aid};ltoken={Token.ltoken};"});
			if (Rel == null) return ErorrTip<string>(-105);
			using (JsonDocument Doc = JsonDocument.Parse(Rel))
			{
				int code = Doc.RootElement.GetProperty("retcode").GetInt32();
				if (code == 10001) return ErorrTip<string>(-102);
				else if (code == 10041)
				{
					try
					{
						Token.device_fp = await Get_Fp();
						File.WriteAllText(SR_DMG.App_Path[3],
							JsonSerializer.Serialize(Token, JsonSopt()));
						return await Get_Roles(Token);
					}
					catch
					{
						return ErorrTip<string>(-1002, $"：{SR_DMG.App_Path[3]}");
					}
				}
				Avatars Avatars = new Avatars { UID = Token.uid, Avatar_List = new List<Avatar>() };
				foreach (JsonElement List in Doc.RootElement.GetProperty("data").GetProperty("avatar_list").EnumerateArray())
				{
					_Avatar _Avt = JsonSerializer.Deserialize<_Avatar>(List);
					Rel = Avatar.Get_Element(_Avt.Element);
					Avatars.Avatar_List.Add(new Avatar
					{
						Name = _Avt.Name,
						Level = _Avt.Level,
						Element = Rel,
						Rank = _Avt.Rank,
						Properts = Avatar.Get_Propert(_Avt._Properts, Rel),
						Servant = _Avt._Servant == null ?
						new Servant
						{
							Name = "",
							Properts = new List<Propert>()
						} :
						new Servant
						{
							Name = _Avt._Servant.Name,
							Properts = Avatar.Get_Propert(_Avt._Servant._Properts, Rel)
						}
					});
				}
				Rel = JsonSerializer.Serialize(Avatars, JsonSopt());
				if (Avatars.Avatar_List[0].Properts.Count > 0)
				{
					string Pat = $"{SR_DMG.App_Path[4]}";
					Directory.CreateDirectory(Pat);
					try
					{
						File.WriteAllText($"{Pat}{Token.uid}.json", Rel);
					}
					catch
					{
						return ErorrTip<string>(-1002, $"：{Pat}{Token.uid}");
					}
				}
				return Rel;
			}
		}

		// Http请求
		public static async Task<string> HttpPost(string Url, string Json = null, string[] Head = null)
		{
			try
			{
				using HttpClient Http = new HttpClient();
				HttpContent Cont = Json == null ? null : new StringContent(Json, Encoding.UTF8, "application/json");
				if (Head != null)
				{
					for (int i = 0; i < Head.Length; i++)
					{
						Http.DefaultRequestHeaders.Add(Head[i++], Head[i]);
					}
				}
				return await (await Http.PostAsync(Url, Cont)).Content.ReadAsStringAsync();
			}
			catch
			{
				return null;
			}
		}
		public static async Task<string> HttpGet(string Url, string[] Head = null)
		{
			try
			{
				using HttpClient Http = new HttpClient();
				if (Head != null)
				{
					for (int i = 0; i < Head.Length; i++)
					{
						Http.DefaultRequestHeaders.Add(Head[i++], Head[i]);
					}
				}
				return await (await Http.GetAsync(Url)).Content.ReadAsStringAsync();
			}
			catch
			{
				return null;
			}
		}

		// 错误提示
		public static T ErorrTip<T>(int Code, string Msg = "", string Tietle = "错误提示")
		{
			MessageBox.Show(Code > -1 ? Msg : Code switch
			{
				-101 => "请先登录",
				-102 => "登录状态失效",
				-103 => "二维码超时失效",
				-105 => "角色信息未公开",
				-1001 => "文件读取失败",
				-1002 => "文件保存失败",
				-1005 => "网络错误",
				_ => $"未知的错误"
			} + $"{Msg}", Tietle, MessageBoxButtons.OK,
			Code >= 0 ? MessageBoxIcon.Information :
			Code > -1000 ? MessageBoxIcon.Exclamation :
			MessageBoxIcon.Error);
			return default;
		}

		// 序列化
		public static JsonSerializerOptions JsonSopt()
		{
			return new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
		}

	}

	public class Token
	{
		public string nickname { set; get; }
		public string aid { set; get; }
		public string uid { set; get; }
		public string mid { set; get; }
		public string server { set; get; }
		public string game_biz { set; get; }
		public string device_fp { set; get; }
		public string cookie_token { set; get; }
		public string stoken { set; get; }
		public string ltoken { set; get; }
	}

}
