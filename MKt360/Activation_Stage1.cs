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

/*
Stage 1:
• User enters e-mail address
• Send code to user's e-mail as long as the specified e-mail isn't already used in the users table

Stage 2:
• User enters code sent to his e-mail
• User has the option to get the code resent to his e-mail
• Send users code to website and verify it
• Website returns successful output, adds verified e-mail to the payments table, and initializes the payment process (stage 3)

Stage 3:
Users is shown price, invoiceid, bitcoin address to send bitcoin to, qr code of the bitcoin address, and payment status. 
Let the user know that it can take a while. Once the payment is complete, generate an account and send an e-mail with the login credentials, 
and let the user know that the payment status is complete. 
Also, give the user a link to the transaction status (txid should link to it) so they can see how it is progressing.
*/

namespace MK2360
{
	public partial class Activation_Stage1 : Form
	{
		string m_paymentType;
		public Activation_Stage1(string paymentType)
		{
			InitializeComponent();
			//FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			EmailTextbox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Return)
				{
					SubmitButton.PerformClick();
				}
			};

			m_paymentType = paymentType;
		}

		private void SubmitButton_Click(object sender, EventArgs e)
		{
			string email = EmailTextbox.Text;
			if (IsValidEmail(email))
			{
				AttemptSendEmail(email);
			}
			else
			{
				new ErrorBox("Input is not a valid e-mail.").ShowDialog();
			}
		}

		private static bool IsValidEmail(string email)
		{
			int atPos = email.IndexOf('@');
			int dotPos = email.IndexOf('.');

			return (atPos > 0 && dotPos != -1 && dotPos > atPos);
		}

		private void AttemptSendEmail(string email)
		{
			var data = LoginForm.webClient.DownloadString("http://oxdmacro.site.nfoservers.com/activation.php?email=" + email);

			if (data.Equals("INVALID_EMAIL"))
			{
				new ErrorBox("Input is not a valid e-mail.").ShowDialog();
			}
			else if (data.Equals("EMAIL_EXISTS"))
			{
				new ErrorBox("Specified e-mail already exists").ShowDialog();
			}
			else if (data.Equals("EMAIL_NOT_SENT"))
			{
				new ErrorBox("The e-mail failed to send. Sorry <3. My contact info is on the login form.").ShowDialog();
			}
			else if (data.Equals("EMAIL_SENT"))
			{
				Dispose();
				new Activation_Stage2(email, m_paymentType).ShowDialog();
			}
			else
			{
				new ErrorBox("Honestly don't know what happened. Sorry.").ShowDialog();
			}
		}
	}
}
