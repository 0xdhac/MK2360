using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace MK2360
{
	public partial class PasswordRecovery : Form
	{
		public PasswordRecovery()
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			EmailTextbox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Return)
				{
					SubmitButton.PerformClick();
				}
			};
		}

		private void SubmitButton_Click(object sender, EventArgs e)
		{
			string email = EmailTextbox.Text;
			if(IsValidEmail(email))
			{
				AttemptSendEmail(email);
			}
			else
			{
				new ErrorBox("Input is not a valid e-mail.").ShowDialog();
			}
		}

		private async void AttemptSendEmail(string email)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://oxdmacro.site.nfoservers.com/recovery.php?email="+email);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				string result = await reader.ReadToEndAsync();

				if(result.Equals("INVALID_EMAIL"))
				{
					new ErrorBox("Input is not a valid e-mail.").ShowDialog();
				}
				else if(result.Equals("EMAIL_DOESNT_EXIST"))
				{
					new ErrorBox("Specified e-mail doesn't exist in the database.").ShowDialog();
				}
				else if(result.Equals("EMAIL_NOT_SENT"))
				{
					new ErrorBox("The e-mail failed to send. Sorry <3. My contact info is on the login form.").ShowDialog();
				}
				else if(result.Equals("EMAIL_SENT"))
				{
					Dispose();
					new PasswordRecovery_Code(email).ShowDialog();
				}
				else
				{
					new ErrorBox("Honestly don't know what happened. Sorry.");
				}
			}
		}

		private static bool IsValidEmail(string email)
		{
			int atPos = email.IndexOf('@');
			int dotPos = email.IndexOf('.');

			return (atPos > 0 && dotPos != -1 && dotPos > atPos);
		}
	}
}
