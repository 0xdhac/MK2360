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
	public partial class PaypalPayment : Form
	{
		private string m_Email;
		private string m_Code;

		public PaypalPayment(string email, string code)
		{
			InitializeComponent();

			m_Email = email;
			m_Code = code;

			System.Diagnostics.Process.Start("oxdmacro");
		}
	}
}
