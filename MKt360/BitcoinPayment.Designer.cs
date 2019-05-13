namespace MK2360
{
	partial class BitcoinPayment
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BitcoinPayment));
			this.PriceLabel = new System.Windows.Forms.Label();
			this.InvoiceLabel = new System.Windows.Forms.Label();
			this.BitcoinAddressLabel = new System.Windows.Forms.Label();
			this.PriceTextBox = new System.Windows.Forms.TextBox();
			this.InvoiceTextBox = new System.Windows.Forms.TextBox();
			this.BitcoinAddressTextBox = new System.Windows.Forms.TextBox();
			this.PaymentStatusLabel = new System.Windows.Forms.Label();
			this.PaymentStatusTextBox = new System.Windows.Forms.TextBox();
			this.QRCodeBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.QRCodeBox)).BeginInit();
			this.SuspendLayout();
			// 
			// PriceLabel
			// 
			this.PriceLabel.AutoSize = true;
			this.PriceLabel.Location = new System.Drawing.Point(61, 15);
			this.PriceLabel.Name = "PriceLabel";
			this.PriceLabel.Size = new System.Drawing.Size(34, 13);
			this.PriceLabel.TabIndex = 0;
			this.PriceLabel.Text = "Price:";
			// 
			// InvoiceLabel
			// 
			this.InvoiceLabel.AutoSize = true;
			this.InvoiceLabel.Location = new System.Drawing.Point(36, 42);
			this.InvoiceLabel.Name = "InvoiceLabel";
			this.InvoiceLabel.Size = new System.Drawing.Size(59, 13);
			this.InvoiceLabel.TabIndex = 1;
			this.InvoiceLabel.Text = "Invoice ID:";
			// 
			// BitcoinAddressLabel
			// 
			this.BitcoinAddressLabel.AutoSize = true;
			this.BitcoinAddressLabel.Location = new System.Drawing.Point(12, 69);
			this.BitcoinAddressLabel.Name = "BitcoinAddressLabel";
			this.BitcoinAddressLabel.Size = new System.Drawing.Size(86, 13);
			this.BitcoinAddressLabel.TabIndex = 2;
			this.BitcoinAddressLabel.Text = "Bitcoin Address: ";
			// 
			// PriceTextBox
			// 
			this.PriceTextBox.Location = new System.Drawing.Point(101, 12);
			this.PriceTextBox.Name = "PriceTextBox";
			this.PriceTextBox.ReadOnly = true;
			this.PriceTextBox.Size = new System.Drawing.Size(194, 20);
			this.PriceTextBox.TabIndex = 3;
			this.PriceTextBox.Text = "Loading..";
			// 
			// InvoiceTextBox
			// 
			this.InvoiceTextBox.Location = new System.Drawing.Point(101, 39);
			this.InvoiceTextBox.Name = "InvoiceTextBox";
			this.InvoiceTextBox.ReadOnly = true;
			this.InvoiceTextBox.Size = new System.Drawing.Size(194, 20);
			this.InvoiceTextBox.TabIndex = 4;
			this.InvoiceTextBox.Text = "Loading..";
			// 
			// BitcoinAddressTextBox
			// 
			this.BitcoinAddressTextBox.Location = new System.Drawing.Point(101, 66);
			this.BitcoinAddressTextBox.Name = "BitcoinAddressTextBox";
			this.BitcoinAddressTextBox.ReadOnly = true;
			this.BitcoinAddressTextBox.Size = new System.Drawing.Size(194, 20);
			this.BitcoinAddressTextBox.TabIndex = 5;
			this.BitcoinAddressTextBox.Text = "Loading..";
			// 
			// PaymentStatusLabel
			// 
			this.PaymentStatusLabel.AutoSize = true;
			this.PaymentStatusLabel.Location = new System.Drawing.Point(55, 96);
			this.PaymentStatusLabel.Name = "PaymentStatusLabel";
			this.PaymentStatusLabel.Size = new System.Drawing.Size(40, 13);
			this.PaymentStatusLabel.TabIndex = 7;
			this.PaymentStatusLabel.Text = "Status:";
			// 
			// PaymentStatusTextBox
			// 
			this.PaymentStatusTextBox.Location = new System.Drawing.Point(101, 93);
			this.PaymentStatusTextBox.Name = "PaymentStatusTextBox";
			this.PaymentStatusTextBox.ReadOnly = true;
			this.PaymentStatusTextBox.Size = new System.Drawing.Size(194, 20);
			this.PaymentStatusTextBox.TabIndex = 8;
			this.PaymentStatusTextBox.Text = "Unpaid";
			// 
			// QRCodeBox
			// 
			this.QRCodeBox.Location = new System.Drawing.Point(18, 119);
			this.QRCodeBox.Name = "QRCodeBox";
			this.QRCodeBox.Size = new System.Drawing.Size(270, 270);
			this.QRCodeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.QRCodeBox.TabIndex = 9;
			this.QRCodeBox.TabStop = false;
			// 
			// BitcoinPayment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(304, 395);
			this.Controls.Add(this.QRCodeBox);
			this.Controls.Add(this.PaymentStatusTextBox);
			this.Controls.Add(this.PaymentStatusLabel);
			this.Controls.Add(this.BitcoinAddressTextBox);
			this.Controls.Add(this.InvoiceTextBox);
			this.Controls.Add(this.PriceTextBox);
			this.Controls.Add(this.BitcoinAddressLabel);
			this.Controls.Add(this.InvoiceLabel);
			this.Controls.Add(this.PriceLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BitcoinPayment";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Pay with Bitcoin";
			((System.ComponentModel.ISupportInitialize)(this.QRCodeBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label PriceLabel;
		private System.Windows.Forms.Label InvoiceLabel;
		private System.Windows.Forms.Label BitcoinAddressLabel;
		private System.Windows.Forms.TextBox PriceTextBox;
		private System.Windows.Forms.TextBox InvoiceTextBox;
		private System.Windows.Forms.TextBox BitcoinAddressTextBox;
		private System.Windows.Forms.Label PaymentStatusLabel;
		private System.Windows.Forms.TextBox PaymentStatusTextBox;
		private System.Windows.Forms.PictureBox QRCodeBox;
	}
}