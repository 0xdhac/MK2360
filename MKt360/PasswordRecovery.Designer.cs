namespace MK2360
{
	partial class PasswordRecovery
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordRecovery));
			this.EmailLabel = new System.Windows.Forms.Label();
			this.EmailTextbox = new System.Windows.Forms.TextBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// EmailLabel
			// 
			this.EmailLabel.AutoSize = true;
			this.EmailLabel.Location = new System.Drawing.Point(15, 15);
			this.EmailLabel.Name = "EmailLabel";
			this.EmailLabel.Size = new System.Drawing.Size(38, 13);
			this.EmailLabel.TabIndex = 0;
			this.EmailLabel.Text = "E-mail:";
			// 
			// EmailTextbox
			// 
			this.EmailTextbox.Location = new System.Drawing.Point(59, 12);
			this.EmailTextbox.Name = "EmailTextbox";
			this.EmailTextbox.Size = new System.Drawing.Size(157, 20);
			this.EmailTextbox.TabIndex = 1;
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(79, 38);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(75, 23);
			this.SubmitButton.TabIndex = 2;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// PasswordRecovery
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(233, 72);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.EmailTextbox);
			this.Controls.Add(this.EmailLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PasswordRecovery";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Password Recovery";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label EmailLabel;
		private System.Windows.Forms.TextBox EmailTextbox;
		private System.Windows.Forms.Button SubmitButton;
	}
}