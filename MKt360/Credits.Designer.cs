namespace MK2360
{
	partial class Credits
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credits));
			this.CreatorLabel = new System.Windows.Forms.Label();
			this.DiscordLabel = new System.Windows.Forms.Label();
			this.TwitterLabel = new System.Windows.Forms.Label();
			this.GithubLabel = new System.Windows.Forms.Label();
			this.EmailLabel = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// CreatorLabel
			// 
			this.CreatorLabel.AutoSize = true;
			this.CreatorLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CreatorLabel.Location = new System.Drawing.Point(58, 20);
			this.CreatorLabel.Name = "CreatorLabel";
			this.CreatorLabel.Size = new System.Drawing.Size(117, 17);
			this.CreatorLabel.TabIndex = 0;
			this.CreatorLabel.Text = "Created by 0xdhac";
			this.CreatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DiscordLabel
			// 
			this.DiscordLabel.AutoSize = true;
			this.DiscordLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DiscordLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DiscordLabel.Location = new System.Drawing.Point(59, 37);
			this.DiscordLabel.Name = "DiscordLabel";
			this.DiscordLabel.Size = new System.Drawing.Size(117, 17);
			this.DiscordLabel.TabIndex = 1;
			this.DiscordLabel.Text = "Discord: 0xd#0057";
			this.toolTip1.SetToolTip(this.DiscordLabel, "Click to copy");
			// 
			// TwitterLabel
			// 
			this.TwitterLabel.AutoSize = true;
			this.TwitterLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.TwitterLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TwitterLabel.Location = new System.Drawing.Point(65, 54);
			this.TwitterLabel.Name = "TwitterLabel";
			this.TwitterLabel.Size = new System.Drawing.Size(106, 17);
			this.TwitterLabel.TabIndex = 2;
			this.TwitterLabel.Text = "Twitter: @0xdhac";
			this.toolTip1.SetToolTip(this.TwitterLabel, "Click to copy");
			// 
			// GithubLabel
			// 
			this.GithubLabel.AutoSize = true;
			this.GithubLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.GithubLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GithubLabel.Location = new System.Drawing.Point(72, 71);
			this.GithubLabel.Name = "GithubLabel";
			this.GithubLabel.Size = new System.Drawing.Size(94, 17);
			this.GithubLabel.TabIndex = 3;
			this.GithubLabel.Text = "Github: 0xdhac";
			this.toolTip1.SetToolTip(this.GithubLabel, "Click to copy");
			// 
			// EmailLabel
			// 
			this.EmailLabel.AutoSize = true;
			this.EmailLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.EmailLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EmailLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.EmailLabel.Location = new System.Drawing.Point(38, 88);
			this.EmailLabel.Name = "EmailLabel";
			this.EmailLabel.Size = new System.Drawing.Size(164, 17);
			this.EmailLabel.TabIndex = 4;
			this.EmailLabel.Text = "E-mail: 0xdhac@gmail.com";
			this.toolTip1.SetToolTip(this.EmailLabel, "Click to copy");
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 100;
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 20;
			// 
			// Credits
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(239, 130);
			this.Controls.Add(this.EmailLabel);
			this.Controls.Add(this.GithubLabel);
			this.Controls.Add(this.TwitterLabel);
			this.Controls.Add(this.DiscordLabel);
			this.Controls.Add(this.CreatorLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Credits";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Credits";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label CreatorLabel;
		private System.Windows.Forms.Label DiscordLabel;
		private System.Windows.Forms.Label TwitterLabel;
		private System.Windows.Forms.Label GithubLabel;
		private System.Windows.Forms.Label EmailLabel;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}