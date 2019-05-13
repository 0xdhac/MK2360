namespace MK2360
{
	partial class Activation_Stage2
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Activation_Stage2));
			this.CodeTextbox = new System.Windows.Forms.TextBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CodeTextbox
			// 
			this.CodeTextbox.Location = new System.Drawing.Point(92, 37);
			this.CodeTextbox.Name = "CodeTextbox";
			this.CodeTextbox.Size = new System.Drawing.Size(100, 20);
			this.CodeTextbox.TabIndex = 1;
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(104, 72);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(75, 23);
			this.SubmitButton.TabIndex = 2;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.AutoSize = true;
			this.DescriptionLabel.Location = new System.Drawing.Point(31, 11);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(225, 13);
			this.DescriptionLabel.TabIndex = 3;
			this.DescriptionLabel.Text = "Enter the confirmation code sent to your e-mail";
			// 
			// Activation_Stage2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(285, 107);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.CodeTextbox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Activation_Stage2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Enter code";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox CodeTextbox;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Label DescriptionLabel;
	}
}