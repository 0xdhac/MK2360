using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using WebSocketSharp;
using System;
using QRCoder;
using System.Drawing;
using System.Globalization;

namespace MK2360
{
	public partial class BitcoinPayment : Form
	{
		private string m_Price;
		private decimal m_Satoshis;
		private decimal m_PaidAmount;
		private string m_Invoice;
		private string m_Address;
		private string m_Email;
		private string m_Code;

		WebSocket ws;
		public BitcoinPayment(string email, string code)
		{
			InitializeComponent();
			m_Email = email;
			m_Code = code;
			LoadFormData();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			m_PaidAmount = new decimal(0.0);
		}

		public async void LoadFormData()
		{
			string url = "http://oxdmacro.site.nfoservers.com/btcpayment.php?email=" + m_Email + "&code=" + m_Code;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				string result = await reader.ReadToEndAsync();

				if(result.Equals("HEADER_NOT_SET") || result.Equals("INVALID_CODE"))
				{
					Dispose();
					new ErrorBox("plis stop trying to hack me ill become homeless again").ShowDialog();
					return;
				}

				dynamic paymentInfo;
				try
				{
					paymentInfo = JsonConvert.DeserializeObject(result);
				}
				catch (JsonReaderException)
				{
					Dispose();
					return;
				}
				
				if(!paymentInfo.ContainsKey("address"))
				{
					Dispose();
					return;
				}

				m_Price						= paymentInfo.price;
				m_Invoice					= paymentInfo.invoice;
				m_Address					= paymentInfo.address;

				PriceTextBox.Text = m_Price + " Bitcoin";
				InvoiceTextBox.Text			= m_Invoice;
				BitcoinAddressTextBox.Text	= m_Address;

				// Convert price to satoshis
				m_Satoshis = decimal.Parse(m_Price, NumberStyles.AllowDecimalPoint) / new decimal(0.00000001);

				// Create QR Code
				QRCodeGenerator qrGenerator = new QRCodeGenerator();
				QRCodeData qrCodeData       = qrGenerator.CreateQrCode("bitcoin:" + m_Address + "?amount=" + m_Price, QRCodeGenerator.ECCLevel.Q);

				QRCode qrCode               = new QRCode(qrCodeData);
				Bitmap qrCodeImage          = qrCode.GetGraphic(20);
				QRCodeBox.Image             = qrCodeImage;
				int unixTimestamp			= (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

				ws = new WebSocket("wss://www.blockonomics.co/payment/" + m_Address + "?timestamp=" + unixTimestamp);
				ws.OnMessage += (sender, e) =>
				{
					paymentInfo = JsonConvert.DeserializeObject(e.Data);
					if(paymentInfo.status == -2)
					{
						PaymentStatusTextBox.Text = "Expired (Close and re-open payment Window.)";
						Dispose();
					}
					else if (paymentInfo.status == -1)
					{
						PaymentStatusTextBox.Text = "Error";
					}
					else if (paymentInfo.status == 0)
					{
						PaymentStatusTextBox.Text = "Unpaid";
					}
					else if (paymentInfo.status == 1)
					{
						PaymentStatusTextBox.Text = "In Process";
					}
					else if(paymentInfo.status == 2)
					{
						int value = paymentInfo.value;
						m_PaidAmount = new decimal(value);
						if(m_PaidAmount >= (m_Satoshis * new decimal(0.99)))
						{
							PaymentStatusTextBox.Text = "Paid (Check your e-mail)";
						}
						else
						{
							string required = ((m_Satoshis - m_PaidAmount) * new decimal(0.00000001)).ToString();
							PaymentStatusTextBox.Text = "Insufficient amount (Required " + required + " more BTC)";
						}
					}
				};

				ws.Connect();
			}
		}
	}
}
