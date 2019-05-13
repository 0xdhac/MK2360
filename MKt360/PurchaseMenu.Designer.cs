namespace MK2360
{
	partial class PurchaseMenu
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseMenu));
			this.BitcoinOption = new System.Windows.Forms.PictureBox();
			this.PayPalOption = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.BitcoinOption)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PayPalOption)).BeginInit();
			this.SuspendLayout();
			// 
			// BitcoinOption
			// 
			this.BitcoinOption.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BitcoinOption.Image = ((System.Drawing.Image)(resources.GetObject("BitcoinOption.Image")));
			this.BitcoinOption.InitialImage = null;
			this.BitcoinOption.Location = new System.Drawing.Point(19, 12);
			this.BitcoinOption.Name = "BitcoinOption";
			this.BitcoinOption.Size = new System.Drawing.Size(100, 81);
			this.BitcoinOption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.BitcoinOption.TabIndex = 1;
			this.BitcoinOption.TabStop = false;
			this.toolTip1.SetToolTip(this.BitcoinOption, "Bitcoin");
			this.BitcoinOption.Click += new System.EventHandler(this.BitcoinOption_Click);
			// 
			// PayPalOption
			// 
			this.PayPalOption.Cursor = System.Windows.Forms.Cursors.Hand;
			this.PayPalOption.Image = ((System.Drawing.Image)(resources.GetObject("PayPalOption.Image")));
			this.PayPalOption.Location = new System.Drawing.Point(125, 12);
			this.PayPalOption.Name = "PayPalOption";
			this.PayPalOption.Size = new System.Drawing.Size(100, 81);
			this.PayPalOption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PayPalOption.TabIndex = 3;
			this.PayPalOption.TabStop = false;
			this.toolTip1.SetToolTip(this.PayPalOption, "PayPal");
			this.PayPalOption.Click += new System.EventHandler(this.PayPalOption_Click);
			// 
			// PurchaseMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(245, 103);
			this.Controls.Add(this.PayPalOption);
			this.Controls.Add(this.BitcoinOption);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PurchaseMenu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select payment method";
			((System.ComponentModel.ISupportInitialize)(this.BitcoinOption)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PayPalOption)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.PictureBox BitcoinOption;
		private System.Windows.Forms.PictureBox PayPalOption;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}