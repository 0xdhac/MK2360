namespace MK2360
{
	partial class PasswordRecovery_Code
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordRecovery_Code));
			this.label1 = new System.Windows.Forms.Label();
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.BackButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(228, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter the confirmation code sent to your e-mail.";
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(87, 25);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(100, 20);
			this.CodeTextbox.TabIndex = 1;
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(140, 51);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(75, 23);
			this.SubmitButton.TabIndex = 2;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// BackButton
			// 
			this.BackButton.Location = new System.Drawing.Point(59, 51);
			this.BackButton.Name = "BackButton";
			this.BackButton.Size = new System.Drawing.Size(75, 23);
			this.BackButton.TabIndex = 3;
			this.BackButton.Text = "Back";
			this.BackButton.UseVisualStyleBackColor = true;
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
			// 
			// PasswordRecovery_Code
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(274, 84);
			this.Controls.Add(this.BackButton);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.CodeTextbox);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PasswordRecovery_Code";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Confirmation Code";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox CodeTextbox;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Button BackButton;
	}
}