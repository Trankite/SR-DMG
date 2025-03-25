using System;
using System.Threading;
using System.Windows.Forms;

namespace SR_DMG
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetColorMode(SystemColorMode.Dark);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			Application.Run(new SR_DMG());
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
			if (MessageBox.Show(Error, "程序崩溃",
				MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
			{
				Environment.Exit(0);
			}
		}

	}
}
