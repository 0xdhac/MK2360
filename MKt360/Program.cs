using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace MK2360
{
	static class Program
	{
		[DllImport("Kernel32.dll", SetLastError = true)]
		public static extern bool IsDebuggerPresent();

		[STAThread]
		static void Main(string[] args)
		{
#if !DEBUG
			if (Debugger.IsAttached)
				return;

			if (IsDebuggerPresent())
				return;
#endif
			using (Process p = Process.GetCurrentProcess())
				p.PriorityClass = ProcessPriorityClass.High;

			Application.ApplicationExit += new EventHandler(OnApplicationExit);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
#if !NOVERIFY
			Application.Run(new LoginForm());
#endif

#if NOVERIFY
			Application.Run(new Form1());
#endif
		}

		private static void OnApplicationExit(object sender, EventArgs e)
		{
			if (XMode.IsActive())
			{
				XMode.Stop(true);
			}

			XMode.BlockInput(false);
		}

		public static string GetExecutingFileHash()
		{
			byte[] myFileData = File.ReadAllBytes(Application.ExecutablePath);
			byte[] myHash = MD5.Create().ComputeHash(myFileData);

			return BitConverter.ToString(myHash).Replace("-", "").ToLower();
		}
	}
}
