using System;
using System.Windows.Forms;

namespace MK2360
{
	public partial class Activation_Stage2 : Form
	{
		string m_Email;
		string m_PaymentType;
		public Activation_Stage2(string email, string paymentType)
		{
			InitializeComponent();

			CodeTextbox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Return)
				{
					SubmitButton.PerformClick();
				}
			};

			m_Email = email;
			m_PaymentType = paymentType;
		}

		private void SubmitButton_Click(object sender, EventArgs e)
		{
			if (IsValidEmail(m_Email) && CodeTextbox.Text.Length > 0)
			{
				AttemptCodeValidation(CodeTextbox.Text);
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

		private void AttemptCodeValidation(string code)
		{
			var data = LoginForm.webClient.DownloadString("http://oxdmacro.site.nfoservers.com/activation.php?email=" + m_Email + "&code=" + code);

			if (data.Equals("INVALID_CODE"))
			{
				new ErrorBox("Wrong code.").ShowDialog();
			}
			else if(data.Equals("ADDRESS_LIMIT"))
			{
				Dispose();
				new ErrorBox("Sorry, you've hit the limit of activation attempts. \nPlease contact the developer 0xd#0057 on Discord.").ShowDialog();
			}
			else if (data.Equals("SUCCESS"))
			{
				Dispose();
				if(m_PaymentType.Equals("BTC"))
				{
					new BitcoinPayment(m_Email, code).ShowDialog();
				}
				if(m_PaymentType.Equals("PayPal"))
				{
					System.Diagnostics.Process.Start("http://oxdmacro.site.nfoservers.com/paypalbuy.php?email=" + m_Email + "&code=" + code);
				}
			}
			else
			{
				new ErrorBox("Honestly don't know what happened. Sorry.");
			}
		}
	}
}
