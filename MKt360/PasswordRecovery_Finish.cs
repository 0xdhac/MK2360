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
	public partial class PasswordRecovery_Finish : Form
	{
		public PasswordRecovery_Finish(string email, string password)
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			EmailTextBox.Text = email;
			PasswordTextbox.Text = password;
		}

		private void DoneButton_Click(object sender, EventArgs e)
		{
			Dispose();
		}
	}
}
