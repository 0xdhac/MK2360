using System;
using System.Windows.Forms;

namespace MK2360
{
	public partial class PurchaseMenu : Form
	{
		public PurchaseMenu()
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
		}

		private void BitcoinOption_Click(object sender, EventArgs e)
		{
			Dispose();
			new Activation_Stage1("BTC").ShowDialog();
		}

		private void PayPalOption_Click(object sender, EventArgs e)
		{
			Dispose();
			new Activation_Stage1("PayPal").ShowDialog();
		}
	}
}
