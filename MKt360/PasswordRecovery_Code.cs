using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace MK2360
{
	public partial class PasswordRecovery_Code : Form
	{
		public PasswordRecovery_Code(string email)
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
		}

		private void SubmitButton_Click(object sender, EventArgs e)
		{
			AttemptSendCode(CodeTextbox.Text);
		}

		private async void AttemptSendCode(string code)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://oxdmacro.site.nfoservers.com/recovery.php?code=" + code);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				string result = await reader.ReadToEndAsync();

				if (result.Equals("INVALID_CODE"))
				{
					new ErrorBox("Incorrect confirmation code.").ShowDialog();
				}
				else
				{
					dynamic loginInfo;
					try
					{
						loginInfo = JsonConvert.DeserializeObject(result);
					}
					catch (JsonReaderException)
					{
						new ErrorBox("Something went wrong. Sorry <3. My contact info is on the login form.").ShowDialog();
						return;
					}

					Dispose();
					string email = loginInfo.email;
					string pass = loginInfo.pass;
					new PasswordRecovery_Finish(email, pass).ShowDialog();
				}
			}
		}

		private void BackButton_Click(object sender, EventArgs e)
		{
			Dispose();
			new PasswordRecovery().ShowDialog();
		}
	}
}
