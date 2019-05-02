using System;
using System.Drawing;
using System.Windows.Forms;

namespace MK2360
{
	public partial class Credits : Form
	{
		public Credits()
		{
			InitializeComponent();

			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			DiscordLabel.MouseEnter += new EventHandler(Label_MouseEnter);
			DiscordLabel.MouseLeave += new EventHandler(Label_MouseLeave);
			TwitterLabel.MouseEnter += new EventHandler(Label_MouseEnter);
			TwitterLabel.MouseLeave += new EventHandler(Label_MouseLeave);
			GithubLabel.MouseEnter += new EventHandler(Label_MouseEnter);
			GithubLabel.MouseLeave += new EventHandler(Label_MouseLeave);
			EmailLabel.MouseEnter += new EventHandler(Label_MouseEnter);
			EmailLabel.MouseLeave += new EventHandler(Label_MouseLeave);

			DiscordLabel.Click += new EventHandler(Label_Click);
			TwitterLabel.Click += new EventHandler(Label_Click);
			GithubLabel.Click += new EventHandler(Label_Click);
			EmailLabel.Click += new EventHandler(Label_Click);
		}

		private void Label_MouseEnter(object sender, EventArgs e)
		{
			((Label)(sender)).ForeColor = Color.FromArgb(144, 180, 237);
		}

		private void Label_MouseLeave(object sender, EventArgs e)
		{
			((Label)(sender)).ForeColor = Color.FromArgb(0, 0, 0);
		}

		private void Label_Click(object sender, EventArgs e)
		{
			string text = ((Label)(sender)).Text;

			string[] del = { ": " };
			string[] s = text.Split(del, StringSplitOptions.RemoveEmptyEntries);
			Clipboard.SetText(s[1]);
		}
	}
}
