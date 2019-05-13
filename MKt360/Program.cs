using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Nett;

namespace MK2360
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			/*
			//current 1557732990
			int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			if(unixTimestamp - 1557732990 > 604800)
			{
				return;
			}
			*/

			Application.ApplicationExit += new EventHandler(OnApplicationExit);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LoginForm());
		}

		private static void OnApplicationExit(object sender, EventArgs e)
		{
			if(XMode.IsActive())
			{
				XMode.Stop(true);
			}
		}
	}
}
