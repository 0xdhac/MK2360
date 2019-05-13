namespace MK2360
{
	partial class Activation_Stage1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Activation_Stage1));
			this.EmailTextbox = new System.Windows.Forms.TextBox();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// EmailTextbox
			// 
			this.EmailTextbox.Location = new System.Drawing.Point(45, 27);
			this.EmailTextbox.Name = "EmailTextbox";
			this.EmailTextbox.Size = new System.Drawing.Size(162, 20);
			this.EmailTextbox.TabIndex = 1;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.AutoSize = true;
			this.DescriptionLabel.Location = new System.Drawing.Point(62, 8);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(125, 13);
			this.DescriptionLabel.TabIndex = 2;
			this.DescriptionLabel.Text = "Enter your e-mail address";
			this.DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(87, 53);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(75, 23);
			this.SubmitButton.TabIndex = 3;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			// 
			// Activation_Stage1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(250, 83);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.EmailTextbox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Activation_Stage1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "E-mail verification step";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox EmailTextbox;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.Button SubmitButton;
	}
}