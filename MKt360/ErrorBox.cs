using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MK2360
{
	public partial class ErrorBox : Form
	{
		public ErrorBox(string error)
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			label1.AutoSize = false;
			label1.TextAlign = ContentAlignment.MiddleCenter;
			label1.Dock = DockStyle.Fill;
			label1.Text = error;
		}
	}
}
