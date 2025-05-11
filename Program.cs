using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SR_DMG
{
	internal static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetColorMode(SystemColorMode.Dark);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			if (args.Length == 0) Application.Run(new SR_DMG());
			else CommandPrompt(args);
			Mihomo.Http?.Dispose();
		}

		public enum AppPath { None, Config, Save, Token, Data, Readme }
		public static Icon Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
		private static readonly string App_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SR-DMG");

		private static void CommandPrompt(string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = args[i].ToLower() switch
				{
					"note" => Mihomo.Get_Note().Result,
					"sign" => Mihomo.DoSign().Result,
					"coin" => Mihomo.DoCoin().Result,
					_ => $"无效指令\n{args[i]}",
				};
			}
			TipForm(string.Join("\n\n", args), Top: true);
		}

		public static void TipForm(string Tips, string Title = "SR-DMG", bool Top = false)
		{
			Form Form = new()
			{
				Icon = Icon,
				Text = Title,
				TopMost = Top,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				FormBorderStyle = FormBorderStyle.FixedSingle,
				StartPosition = FormStartPosition.CenterScreen,
				MaximizeBox = false
			};
			Panel Pan = new()
			{
				Left = 10,
				AutoSize = true,
				AutoScroll = true,
				MinimumSize = new Size(300, 100),
				MaximumSize = new Size(300, 300)
			};
			Label Lab = new()
			{
				Text = Tips,
				AutoSize = true,
				Padding = new Padding(20),
				MaximumSize = new Size(280, 0),
				MinimumSize = new Size(280, 100),
				Font = new Font(Form.Font.Name, 10, FontStyle.Bold),
				TextAlign = ContentAlignment.TopCenter
			};
			Pan.Controls.Add(Lab);
			Form.Controls.Add(Pan);
			Form.ShowDialog();
			Form.Dispose();
		}

		public static int ListForm(string Title, string ListName, List<string> ListText)
		{
			int Index = -1;
			Form Form = new()
			{
				Icon = Icon,
				Text = Title,
				Size = new Size(300, 300),
				FormBorderStyle = FormBorderStyle.FixedSingle,
				StartPosition = FormStartPosition.CenterScreen,
				MaximizeBox = false
			};
			ListView ListView = new()
			{
				Size = new Size(260, 245),
				Location = new Point(10, 10),
				Font = new Font(Form.Font.Name, 15),
				BackColor = Form.BackColor,
				ForeColor = Form.ForeColor,
				FullRowSelect = true,
				View = View.Details,
				MultiSelect = false,
				GridLines = false
			};
			ListView.Columns.Add(ListName, 235);
			foreach (string str in ListText)
			{
				ListView.Items.Add(str);
			}
			ListView.SelectedIndexChanged += (sender, e) =>
			{
				Index = ListView.SelectedIndices[0];
				Form.Close();
			};
			Form.Controls.Add(ListView);
			Form.ShowDialog();
			Form.Dispose();
			return Index;
		}

		public static bool Message(string Msg, string Tietle)
		{
			return MessageBox.Show(Msg, Tietle, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
		}

		public static string GetPath(AppPath FileType = AppPath.None, string FileName = null, bool IsCreate = false)
		{
			if (IsCreate)
			{
				string FilePath = GetPath(FileType, FileName);
				string DirPath = Path.GetDirectoryName(FilePath);
				if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
				return FilePath;
			}
			else return GetPath(FileType, FileName);
		}

		private static string GetPath(AppPath FilePath, string FileName)
		{
			if (FilePath == AppPath.None) return App_Path;
			else return Path.Combine(App_Path,
				FilePath switch
				{
					AppPath.Config => "App.config",
					AppPath.Save => "SR-DMG.csv",
					AppPath.Token => "Token.json",
					AppPath.Data => Path.Combine("Data", $"{FileName}.json"),
					AppPath.Readme => "Readme.md",
					_ => string.Empty
				});
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			string[] Stk = e.Exception.StackTrace.Split('\n');
			string Error = "意外错误：" + e.Exception.Message;
			int Ins = Error.Length;
			bool Flag = true;
			foreach (string Str in Stk)
			{
				string Tar = Str[..Str.IndexOf('(')];
				if (Flag || Tar.Contains("SR_DMG"))
				{
					if (Flag && Tar.Contains("SR_DMG")) Flag = false;
					Error = Error.Insert(Ins, "\n in " + Tar[(Tar.LastIndexOf('.') + 1)..]);
				}
				else break;
			}
			MessageBox.Show(Error, "程序崩溃", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

	}
}
