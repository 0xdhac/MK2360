namespace MK2360
{
    partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.PresetLabel = new System.Windows.Forms.Label();
			this.NewPresetButton = new System.Windows.Forms.Button();
			this.SavePresetButton = new System.Windows.Forms.Button();
			this.PresetList = new System.Windows.Forms.ComboBox();
			this.LogTextBox = new MK2360.ReadOnlyTextBox();
			this.ProcessLabel = new System.Windows.Forms.Label();
			this.ProcessComboBox = new System.Windows.Forms.ComboBox();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.CreditsButton = new System.Windows.Forms.Button();
			this.ControllerModeButton = new System.Windows.Forms.Button();
			this.KillSwitchKey = new System.Windows.Forms.Label();
			this.KillSwitchTextBox = new System.Windows.Forms.TextBox();
			this.RTButton = new System.Windows.Forms.PictureBox();
			this.RBButton = new System.Windows.Forms.PictureBox();
			this.LTButton = new System.Windows.Forms.PictureBox();
			this.LBButton = new System.Windows.Forms.PictureBox();
			this.StartButton = new System.Windows.Forms.PictureBox();
			this.BackButton = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.PresetNameLabel = new System.Windows.Forms.Label();
			this.PresetNameTextbox = new System.Windows.Forms.TextBox();
			this.LeftJoystickButton = new MK2360.OvalPictureBox();
			this.RightJoystickButton = new MK2360.OvalPictureBox();
			this.XButton = new MK2360.OvalPictureBox();
			this.BButton = new MK2360.OvalPictureBox();
			this.YButton = new MK2360.OvalPictureBox();
			this.AButton = new MK2360.OvalPictureBox();
			this.DpadButton = new MK2360.OvalPictureBox();
			((System.ComponentModel.ISupportInitialize)(this.RTButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RBButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LTButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LBButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StartButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BackButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftJoystickButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RightJoystickButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.XButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.YButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DpadButton)).BeginInit();
			this.SuspendLayout();
			// 
			// PresetLabel
			// 
			this.PresetLabel.AutoSize = true;
			this.PresetLabel.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PresetLabel.Location = new System.Drawing.Point(7, 15);
			this.PresetLabel.Name = "PresetLabel";
			this.PresetLabel.Size = new System.Drawing.Size(83, 13);
			this.PresetLabel.TabIndex = 15;
			this.PresetLabel.Text = "Choose Preset:";
			// 
			// NewPresetButton
			// 
			this.NewPresetButton.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.NewPresetButton.Location = new System.Drawing.Point(10, 119);
			this.NewPresetButton.Name = "NewPresetButton";
			this.NewPresetButton.Size = new System.Drawing.Size(85, 23);
			this.NewPresetButton.TabIndex = 16;
			this.NewPresetButton.Text = "New";
			this.NewPresetButton.UseVisualStyleBackColor = true;
			this.NewPresetButton.Click += new System.EventHandler(this.NewPresetButton_Click);
			// 
			// SavePresetButton
			// 
			this.SavePresetButton.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SavePresetButton.Location = new System.Drawing.Point(102, 119);
			this.SavePresetButton.Name = "SavePresetButton";
			this.SavePresetButton.Size = new System.Drawing.Size(87, 23);
			this.SavePresetButton.TabIndex = 17;
			this.SavePresetButton.Text = "Save";
			this.SavePresetButton.UseVisualStyleBackColor = true;
			this.SavePresetButton.Click += new System.EventHandler(this.SavePresetButton_Click);
			// 
			// PresetList
			// 
			this.PresetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PresetList.FormattingEnabled = true;
			this.PresetList.Location = new System.Drawing.Point(91, 12);
			this.PresetList.Name = "PresetList";
			this.PresetList.Size = new System.Drawing.Size(98, 21);
			this.PresetList.TabIndex = 20;
			// 
			// LogTextBox
			// 
			this.LogTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.LogTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.LogTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LogTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.LogTextBox.Location = new System.Drawing.Point(12, 386);
			this.LogTextBox.MaxLength = 1000000;
			this.LogTextBox.Multiline = true;
			this.LogTextBox.Name = "LogTextBox";
			this.LogTextBox.ReadOnly = true;
			this.LogTextBox.Size = new System.Drawing.Size(856, 128);
			this.LogTextBox.TabIndex = 21;
			// 
			// ProcessLabel
			// 
			this.ProcessLabel.AutoSize = true;
			this.ProcessLabel.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ProcessLabel.Location = new System.Drawing.Point(7, 68);
			this.ProcessLabel.Name = "ProcessLabel";
			this.ProcessLabel.Size = new System.Drawing.Size(48, 13);
			this.ProcessLabel.TabIndex = 22;
			this.ProcessLabel.Text = "Process:";
			// 
			// ProcessComboBox
			// 
			this.ProcessComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProcessComboBox.Enabled = false;
			this.ProcessComboBox.FormattingEnabled = true;
			this.ProcessComboBox.Location = new System.Drawing.Point(92, 65);
			this.ProcessComboBox.Name = "ProcessComboBox";
			this.ProcessComboBox.Size = new System.Drawing.Size(97, 21);
			this.ProcessComboBox.TabIndex = 23;
			// 
			// DeleteButton
			// 
			this.DeleteButton.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DeleteButton.Location = new System.Drawing.Point(10, 147);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(85, 23);
			this.DeleteButton.TabIndex = 24;
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// CreditsButton
			// 
			this.CreditsButton.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CreditsButton.Location = new System.Drawing.Point(102, 147);
			this.CreditsButton.Name = "CreditsButton";
			this.CreditsButton.Size = new System.Drawing.Size(87, 23);
			this.CreditsButton.TabIndex = 25;
			this.CreditsButton.Text = "Credits";
			this.CreditsButton.UseVisualStyleBackColor = true;
			this.CreditsButton.Click += new System.EventHandler(this.CreditsButton_Click);
			// 
			// ControllerModeButton
			// 
			this.ControllerModeButton.BackColor = System.Drawing.Color.SkyBlue;
			this.ControllerModeButton.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ControllerModeButton.Location = new System.Drawing.Point(10, 356);
			this.ControllerModeButton.Name = "ControllerModeButton";
			this.ControllerModeButton.Size = new System.Drawing.Size(178, 23);
			this.ControllerModeButton.TabIndex = 26;
			this.ControllerModeButton.Text = "Start";
			this.ControllerModeButton.UseVisualStyleBackColor = false;
			// 
			// KillSwitchKey
			// 
			this.KillSwitchKey.AutoSize = true;
			this.KillSwitchKey.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KillSwitchKey.Location = new System.Drawing.Point(7, 96);
			this.KillSwitchKey.Name = "KillSwitchKey";
			this.KillSwitchKey.Size = new System.Drawing.Size(62, 13);
			this.KillSwitchKey.TabIndex = 27;
			this.KillSwitchKey.Text = "Kill Switch:";
			// 
			// KillSwitchTextBox
			// 
			this.KillSwitchTextBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.KillSwitchTextBox.Location = new System.Drawing.Point(92, 93);
			this.KillSwitchTextBox.Name = "KillSwitchTextBox";
			this.KillSwitchTextBox.ReadOnly = true;
			this.KillSwitchTextBox.Size = new System.Drawing.Size(97, 20);
			this.KillSwitchTextBox.TabIndex = 28;
			// 
			// RTButton
			// 
			this.RTButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.RTButton.Image = ((System.Drawing.Image)(resources.GetObject("RTButton.Image")));
			this.RTButton.Location = new System.Drawing.Point(780, 52);
			this.RTButton.Name = "RTButton";
			this.RTButton.Size = new System.Drawing.Size(72, 65);
			this.RTButton.TabIndex = 13;
			this.RTButton.TabStop = false;
			// 
			// RBButton
			// 
			this.RBButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.RBButton.Image = ((System.Drawing.Image)(resources.GetObject("RBButton.Image")));
			this.RBButton.Location = new System.Drawing.Point(809, 123);
			this.RBButton.Name = "RBButton";
			this.RBButton.Size = new System.Drawing.Size(56, 46);
			this.RBButton.TabIndex = 12;
			this.RBButton.TabStop = false;
			// 
			// LTButton
			// 
			this.LTButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.LTButton.Image = ((System.Drawing.Image)(resources.GetObject("LTButton.Image")));
			this.LTButton.Location = new System.Drawing.Point(229, 54);
			this.LTButton.Name = "LTButton";
			this.LTButton.Size = new System.Drawing.Size(71, 64);
			this.LTButton.TabIndex = 11;
			this.LTButton.TabStop = false;
			// 
			// LBButton
			// 
			this.LBButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.LBButton.Image = ((System.Drawing.Image)(resources.GetObject("LBButton.Image")));
			this.LBButton.Location = new System.Drawing.Point(215, 124);
			this.LBButton.Name = "LBButton";
			this.LBButton.Size = new System.Drawing.Size(58, 45);
			this.LBButton.TabIndex = 10;
			this.LBButton.TabStop = false;
			// 
			// StartButton
			// 
			this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.StartButton.Image = global::MK2360.Properties.Resources.x360_button_start;
			this.StartButton.Location = new System.Drawing.Point(574, 104);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new System.Drawing.Size(35, 21);
			this.StartButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.StartButton.TabIndex = 3;
			this.StartButton.TabStop = false;
			// 
			// BackButton
			// 
			this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BackButton.Image = global::MK2360.Properties.Resources.x360_button_back;
			this.BackButton.Location = new System.Drawing.Point(459, 104);
			this.BackButton.Name = "BackButton";
			this.BackButton.Size = new System.Drawing.Size(35, 21);
			this.BackButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.BackButton.TabIndex = 1;
			this.BackButton.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(196, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(688, 367);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// PresetNameLabel
			// 
			this.PresetNameLabel.AutoSize = true;
			this.PresetNameLabel.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PresetNameLabel.Location = new System.Drawing.Point(7, 42);
			this.PresetNameLabel.Name = "PresetNameLabel";
			this.PresetNameLabel.Size = new System.Drawing.Size(57, 13);
			this.PresetNameLabel.TabIndex = 29;
			this.PresetNameLabel.Text = "Set name:";
			// 
			// PresetNameTextbox
			// 
			this.PresetNameTextbox.Location = new System.Drawing.Point(91, 39);
			this.PresetNameTextbox.Name = "PresetNameTextbox";
			this.PresetNameTextbox.Size = new System.Drawing.Size(98, 20);
			this.PresetNameTextbox.TabIndex = 30;
			// 
			// LeftJoystickButton
			// 
			this.LeftJoystickButton.BackColor = System.Drawing.Color.DarkGray;
			this.LeftJoystickButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.LeftJoystickButton.Image = ((System.Drawing.Image)(resources.GetObject("LeftJoystickButton.Image")));
			this.LeftJoystickButton.Location = new System.Drawing.Point(340, 80);
			this.LeftJoystickButton.Name = "LeftJoystickButton";
			this.LeftJoystickButton.Size = new System.Drawing.Size(76, 72);
			this.LeftJoystickButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.LeftJoystickButton.TabIndex = 9;
			this.LeftJoystickButton.TabStop = false;
			// 
			// RightJoystickButton
			// 
			this.RightJoystickButton.BackColor = System.Drawing.Color.DarkGray;
			this.RightJoystickButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.RightJoystickButton.Image = ((System.Drawing.Image)(resources.GetObject("RightJoystickButton.Image")));
			this.RightJoystickButton.Location = new System.Drawing.Point(565, 160);
			this.RightJoystickButton.Name = "RightJoystickButton";
			this.RightJoystickButton.Size = new System.Drawing.Size(85, 85);
			this.RightJoystickButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.RightJoystickButton.TabIndex = 8;
			this.RightJoystickButton.TabStop = false;
			// 
			// XButton
			// 
			this.XButton.BackColor = System.Drawing.Color.DarkGray;
			this.XButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.XButton.Image = ((System.Drawing.Image)(resources.GetObject("XButton.Image")));
			this.XButton.Location = new System.Drawing.Point(625, 92);
			this.XButton.Name = "XButton";
			this.XButton.Size = new System.Drawing.Size(43, 41);
			this.XButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.XButton.TabIndex = 7;
			this.XButton.TabStop = false;
			// 
			// BButton
			// 
			this.BButton.BackColor = System.Drawing.Color.DarkGray;
			this.BButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BButton.Image = ((System.Drawing.Image)(resources.GetObject("BButton.Image")));
			this.BButton.Location = new System.Drawing.Point(708, 95);
			this.BButton.Name = "BButton";
			this.BButton.Size = new System.Drawing.Size(40, 37);
			this.BButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.BButton.TabIndex = 6;
			this.BButton.TabStop = false;
			// 
			// YButton
			// 
			this.YButton.BackColor = System.Drawing.Color.DarkGray;
			this.YButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.YButton.Image = ((System.Drawing.Image)(resources.GetObject("YButton.Image")));
			this.YButton.Location = new System.Drawing.Point(668, 58);
			this.YButton.Name = "YButton";
			this.YButton.Size = new System.Drawing.Size(40, 37);
			this.YButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.YButton.TabIndex = 5;
			this.YButton.TabStop = false;
			// 
			// AButton
			// 
			this.AButton.BackColor = System.Drawing.Color.DarkGray;
			this.AButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.AButton.Image = ((System.Drawing.Image)(resources.GetObject("AButton.Image")));
			this.AButton.Location = new System.Drawing.Point(668, 129);
			this.AButton.Name = "AButton";
			this.AButton.Size = new System.Drawing.Size(41, 40);
			this.AButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.AButton.TabIndex = 4;
			this.AButton.TabStop = false;
			// 
			// DpadButton
			// 
			this.DpadButton.BackColor = System.Drawing.Color.DarkGray;
			this.DpadButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DpadButton.Image = global::MK2360.Properties.Resources.x360_button_dpad;
			this.DpadButton.Location = new System.Drawing.Point(393, 146);
			this.DpadButton.Name = "DpadButton";
			this.DpadButton.Size = new System.Drawing.Size(113, 104);
			this.DpadButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.DpadButton.TabIndex = 2;
			this.DpadButton.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(880, 526);
			this.Controls.Add(this.PresetNameTextbox);
			this.Controls.Add(this.PresetNameLabel);
			this.Controls.Add(this.KillSwitchTextBox);
			this.Controls.Add(this.KillSwitchKey);
			this.Controls.Add(this.ControllerModeButton);
			this.Controls.Add(this.CreditsButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.ProcessComboBox);
			this.Controls.Add(this.ProcessLabel);
			this.Controls.Add(this.LogTextBox);
			this.Controls.Add(this.PresetList);
			this.Controls.Add(this.SavePresetButton);
			this.Controls.Add(this.NewPresetButton);
			this.Controls.Add(this.PresetLabel);
			this.Controls.Add(this.RTButton);
			this.Controls.Add(this.RBButton);
			this.Controls.Add(this.LTButton);
			this.Controls.Add(this.LBButton);
			this.Controls.Add(this.LeftJoystickButton);
			this.Controls.Add(this.RightJoystickButton);
			this.Controls.Add(this.XButton);
			this.Controls.Add(this.BButton);
			this.Controls.Add(this.YButton);
			this.Controls.Add(this.AButton);
			this.Controls.Add(this.StartButton);
			this.Controls.Add(this.DpadButton);
			this.Controls.Add(this.BackButton);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MK2360";
			((System.ComponentModel.ISupportInitialize)(this.RTButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RBButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LTButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LBButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StartButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BackButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LeftJoystickButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RightJoystickButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.XButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.YButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DpadButton)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox BackButton;
        private OvalPictureBox DpadButton;
        private System.Windows.Forms.PictureBox StartButton;
        private OvalPictureBox AButton;
        private OvalPictureBox YButton;
        private OvalPictureBox BButton;
        private OvalPictureBox XButton;
        private OvalPictureBox RightJoystickButton;
        private OvalPictureBox LeftJoystickButton;
        private System.Windows.Forms.PictureBox LBButton;
        private System.Windows.Forms.PictureBox LTButton;
        private System.Windows.Forms.PictureBox RBButton;
        private System.Windows.Forms.PictureBox RTButton;
        private System.Windows.Forms.Label PresetLabel;
        private System.Windows.Forms.Button NewPresetButton;
        private System.Windows.Forms.Button SavePresetButton;
		private ReadOnlyTextBox LogTextBox;
		private System.Windows.Forms.Label ProcessLabel;
		public System.Windows.Forms.ComboBox PresetList;
		public System.Windows.Forms.ComboBox ProcessComboBox;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button CreditsButton;
		private System.Windows.Forms.Label KillSwitchKey;
		public System.Windows.Forms.TextBox KillSwitchTextBox;
		private System.Windows.Forms.Label PresetNameLabel;
		public System.Windows.Forms.TextBox PresetNameTextbox;
		public System.Windows.Forms.Button ControllerModeButton;
	}
}

