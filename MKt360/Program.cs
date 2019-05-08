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
			Application.ApplicationExit += new EventHandler(OnApplicationExit);

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
