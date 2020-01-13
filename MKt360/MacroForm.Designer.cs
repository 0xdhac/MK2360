namespace MK2360
{
	partial class MacroForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MacroForm));
			this.ScriptLabel = new System.Windows.Forms.Label();
			this.ScriptBox = new System.Windows.Forms.ComboBox();
			this.AuthorLabel = new System.Windows.Forms.Label();
			this.PresetLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SettingsPanel = new System.Windows.Forms.Panel();
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.HLine = new System.Windows.Forms.Label();
			this.ReloadSplitButton = new MK2360.SplitButton();
			this.EditButton = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ScriptLabel
			// 
			this.ScriptLabel.AutoSize = true;
			this.ScriptLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ScriptLabel.Location = new System.Drawing.Point(12, 13);
			this.ScriptLabel.Name = "ScriptLabel";
			this.ScriptLabel.Size = new System.Drawing.Size(39, 13);
			this.ScriptLabel.TabIndex = 0;
			this.ScriptLabel.Text = "Script:";
			// 
			// ScriptBox
			// 
			this.ScriptBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ScriptBox.FormattingEnabled = true;
			this.ScriptBox.Location = new System.Drawing.Point(55, 10);
			this.ScriptBox.Name = "ScriptBox";
			this.ScriptBox.Size = new System.Drawing.Size(212, 21);
			this.ScriptBox.TabIndex = 1;
			// 
			// AuthorLabel
			// 
			this.AuthorLabel.AutoSize = true;
			this.AuthorLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AuthorLabel.Location = new System.Drawing.Point(12, 66);
			this.AuthorLabel.Name = "AuthorLabel";
			this.AuthorLabel.Size = new System.Drawing.Size(68, 13);
			this.AuthorLabel.TabIndex = 2;
			this.AuthorLabel.Text = "Author: N/A";
			// 
			// PresetLabel
			// 
			this.PresetLabel.AutoSize = true;
			this.PresetLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PresetLabel.Location = new System.Drawing.Point(12, 46);
			this.PresetLabel.Name = "PresetLabel";
			this.PresetLabel.Size = new System.Drawing.Size(63, 13);
			this.PresetLabel.TabIndex = 3;
			this.PresetLabel.Text = "Preset: N/A";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel1.Controls.Add(this.SettingsPanel);
			this.panel1.Location = new System.Drawing.Point(13, 121);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(254, 289);
			this.panel1.TabIndex = 5;
			// 
			// SettingsPanel
			// 
			this.SettingsPanel.BackColor = System.Drawing.SystemColors.Control;
			this.SettingsPanel.Location = new System.Drawing.Point(4, 3);
			this.SettingsPanel.Name = "SettingsPanel";
			this.SettingsPanel.Size = new System.Drawing.Size(247, 283);
			this.SettingsPanel.TabIndex = 0;
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.AutoSize = true;
			this.DescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DescriptionLabel.Location = new System.Drawing.Point(12, 85);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(91, 13);
			this.DescriptionLabel.TabIndex = 4;
			this.DescriptionLabel.Text = "Description: N/A";
			// 
			// HLine
			// 
			this.HLine.Location = new System.Drawing.Point(11, 39);
			this.HLine.Name = "HLine";
			this.HLine.Size = new System.Drawing.Size(260, 2);
			this.HLine.TabIndex = 6;
			// 
			// ReloadSplitButton
			// 
			this.ReloadSplitButton.AutoSize = true;
			this.ReloadSplitButton.Location = new System.Drawing.Point(192, 416);
			this.ReloadSplitButton.Name = "ReloadSplitButton";
			this.ReloadSplitButton.Size = new System.Drawing.Size(75, 23);
			this.ReloadSplitButton.TabIndex = 7;
			this.ReloadSplitButton.Text = "Reload";
			this.ReloadSplitButton.UseVisualStyleBackColor = true;
			// 
			// EditButton
			// 
			this.EditButton.Location = new System.Drawing.Point(111, 416);
			this.EditButton.Name = "EditButton";
			this.EditButton.Size = new System.Drawing.Size(75, 23);
			this.EditButton.TabIndex = 8;
			this.EditButton.Text = "Edit";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// MacroForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(280, 446);
			this.Controls.Add(this.EditButton);
			this.Controls.Add(this.ReloadSplitButton);
			this.Controls.Add(this.HLine);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.DescriptionLabel);
			this.Controls.Add(this.PresetLabel);
			this.Controls.Add(this.AuthorLabel);
			this.Controls.Add(this.ScriptBox);
			this.Controls.Add(this.ScriptLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MacroForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MK2360 Macros";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label ScriptLabel;
		private System.Windows.Forms.ComboBox ScriptBox;
		private System.Windows.Forms.Label AuthorLabel;
		private System.Windows.Forms.Label PresetLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel SettingsPanel;
		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.Label HLine;
		private SplitButton ReloadSplitButton;
		private System.Windows.Forms.Button EditButton;
	}
}