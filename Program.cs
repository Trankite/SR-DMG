using System;
using System.Drawing;
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
		}

		public static void CommandPrompt(string[] args)
		{
			SR_DMG.DoInit();
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = args[i].ToLower() switch
				{
					"note" => Mihomo.Get_Note().Result,
					"sign" => Mihomo.DoSign().Result,
					"coin" => Mihomo.DoCoin().Result,
					_ => string.Empty
				};
			}
			TipForm(string.Join("\n\n", args));
		}

		public static void TipForm(string Tips, string Title = "SR-DMG")
		{
			Form Form = new()
			{
				Text = Title,
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
				Font = new Font(Form.Font.Name, 10),
				TextAlign = ContentAlignment.TopCenter
			};
			Pan.Controls.Add(Lab);
			Form.Controls.Add(Pan);
			Form.ShowDialog();
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
