﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Net;

namespace MK2360
{
	public partial class LoginForm : Form
	{
		public static CookedWebClient webClient = new CookedWebClient();
		private string loginUrl = "http://oxdmacro.site.nfoservers.com/login.php";

		public LoginForm()
		{

			InitializeComponent();
			UsernameTextbox.Select();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			PasswordTextbox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Return)
				{
					LoginButton.PerformClick();
				}
			};

			UsernameTextbox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Return)
				{
					LoginButton.PerformClick();
				}
			};
		}

		private string AttemptLogin(string username, string password)
		{
			webClient.Headers.Add("user-agent", "EasyEdit");
			webClient.QueryString.Add("email", username);
			webClient.QueryString.Add("pass", password);

			var data = webClient.UploadValues(loginUrl, "POST", webClient.QueryString);

			var responseString = Encoding.UTF8.GetString(data);
			webClient.QueryString.Clear();
			return responseString;
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
#if !NOVERIFY
			if (UsernameTextbox.TextLength > 0 && PasswordTextbox.TextLength > 0)
			{
				LoginButton.Enabled = false;
				string response = AttemptLogin(UsernameTextbox.Text, PasswordTextbox.Text);
				if (response == "SUCCESS:LOGGED_IN")
				{
					Hide();
					new Form1().Show();
				}
				else if (response == "ERROR:ACCOUNT_NOT_FOUND")
				{
					new ErrorBox("Account not found").ShowDialog();
				}
				else if (response == "ERROR:BANNED_ACCOUNT")
				{
					new ErrorBox("Your account is banned").ShowDialog();
				}
				LoginButton.Enabled = true;
			}
#endif
		}

		private void PurchaseButton_Click(object sender, EventArgs e)
		{
			new PurchaseMenu().ShowDialog();
		}

		private void IForgotLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			new PasswordRecovery().ShowDialog();
		}

		private void IForgotPicture_Click(object sender, EventArgs e)
		{
			new PasswordRecovery().ShowDialog();
		}
	}

	public class CookedWebClient : WebClient
	{
		public CookieContainer Cookies { get; } = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest request = base.GetWebRequest(address);

			if (request.GetType() == typeof(HttpWebRequest))
				((HttpWebRequest)request).CookieContainer = Cookies;

			return request;
		}
	}
}
