namespace MK2360
{
	partial class LoginForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.LoginButton = new System.Windows.Forms.Button();
			this.UsernameLabel = new System.Windows.Forms.Label();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.UsernameTextbox = new System.Windows.Forms.TextBox();
			this.PasswordTextbox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.PurchaseButton = new System.Windows.Forms.Button();
			this.IForgotLabel = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// LoginButton
			// 
			this.LoginButton.Location = new System.Drawing.Point(99, 58);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(75, 23);
			this.LoginButton.TabIndex = 0;
			this.LoginButton.Text = "Login";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
			// 
			// UsernameLabel
			// 
			this.UsernameLabel.AutoSize = true;
			this.UsernameLabel.Location = new System.Drawing.Point(59, 9);
			this.UsernameLabel.Name = "UsernameLabel";
			this.UsernameLabel.Size = new System.Drawing.Size(38, 13);
			this.UsernameLabel.TabIndex = 1;
			this.UsernameLabel.Text = "E-mail:";
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.AutoSize = true;
			this.PasswordLabel.Location = new System.Drawing.Point(41, 35);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(56, 13);
			this.PasswordLabel.TabIndex = 2;
			this.PasswordLabel.Text = "Password:";
			// 
			// UsernameTextbox
			// 
			this.UsernameTextbox.Location = new System.Drawing.Point(99, 6);
			this.UsernameTextbox.Name = "UsernameTextbox";
			this.UsernameTextbox.Size = new System.Drawing.Size(100, 20);
			this.UsernameTextbox.TabIndex = 3;
			// 
			// PasswordTextbox
			// 
			this.PasswordTextbox.Location = new System.Drawing.Point(99, 32);
			this.PasswordTextbox.Name = "PasswordTextbox";
			this.PasswordTextbox.PasswordChar = '•';
			this.PasswordTextbox.Size = new System.Drawing.Size(100, 20);
			this.PasswordTextbox.TabIndex = 4;
			this.PasswordTextbox.UseSystemPasswordChar = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 85);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(244, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Business/Customer support: 0xd#0057 on Discord";
			// 
			// PurchaseButton
			// 
			this.PurchaseButton.Location = new System.Drawing.Point(17, 58);
			this.PurchaseButton.Name = "PurchaseButton";
			this.PurchaseButton.Size = new System.Drawing.Size(75, 23);
			this.PurchaseButton.TabIndex = 7;
			this.PurchaseButton.Text = "Purchase";
			this.PurchaseButton.UseVisualStyleBackColor = true;
			this.PurchaseButton.Click += new System.EventHandler(this.PurchaseButton_Click);
			// 
			// IForgotLabel
			// 
			this.IForgotLabel.AutoSize = true;
			this.IForgotLabel.Location = new System.Drawing.Point(180, 63);
			this.IForgotLabel.Name = "IForgotLabel";
			this.IForgotLabel.Size = new System.Drawing.Size(58, 13);
			this.IForgotLabel.TabIndex = 8;
			this.IForgotLabel.TabStop = true;
			this.IForgotLabel.Text = "I FORGOT";
			this.IForgotLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IForgotLabel_LinkClicked);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(250, 107);
			this.Controls.Add(this.IForgotLabel);
			this.Controls.Add(this.PurchaseButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.PasswordTextbox);
			this.Controls.Add(this.UsernameTextbox);
			this.Controls.Add(this.PasswordLabel);
			this.Controls.Add(this.UsernameLabel);
			this.Controls.Add(this.LoginButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.Label UsernameLabel;
		private System.Windows.Forms.Label PasswordLabel;
		private System.Windows.Forms.TextBox UsernameTextbox;
		private System.Windows.Forms.TextBox PasswordTextbox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button PurchaseButton;
		private System.Windows.Forms.LinkLabel IForgotLabel;
	}
}