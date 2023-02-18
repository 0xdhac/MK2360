using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace MK2360
{
	public partial class MacroForm : Form
	{
		public MacroForm()
		{
			InitializeComponent();

			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			ScrollBar vScrollBar1 = new VScrollBar();
			vScrollBar1.Dock = DockStyle.Right;
			vScrollBar1.Scroll += (sender, e) => { SettingsPanel.VerticalScroll.Value = vScrollBar1.Value; };
			vScrollBar1.LargeChange = vScrollBar1.Maximum / 2;
			SettingsPanel.Controls.Add(vScrollBar1);
			ScriptBox.SelectedIndexChanged += ScriptBox_Changed;
			ScriptBox.DataSource = Script.ScriptList;

			if(Script.ScriptList.Count > 0)
				ScriptBox.SelectedIndex = 0;

			PresetLabel.Text = "Preset: " + Preset.Current.m_Name;
			DescriptionLabel.MaximumSize = new Size(SettingsPanel.Width - 3, 0);
			DescriptionLabel.AutoSize = true;

			HLine.BorderStyle = BorderStyle.Fixed3D;

			ActiveControl = PresetLabel; // Focus the the preset label for aesthetics

			ReloadSplitButton.FlatStyle = FlatStyle.Flat;

			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Add("Current");
			menu.Items.Add("All");
			menu.ItemClicked += new ToolStripItemClickedEventHandler(ReloadItem_Clicked);

			ReloadSplitButton.ContextMenuStrip = menu;

			//EditButton.Hide();
		}

		private void ReloadItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
		{
			Script item = (Script)ScriptBox.SelectedItem;

			if (item == null)
				return;

			string name = item.ScriptName;
			string text = e.ClickedItem.Text;
			if(text == "Current")
			{
				ScriptBox.DataSource = null;
				Script.Reload(item);
				ScriptBox.DataSource = Script.ScriptList;
			}
			else if(text == "All")
			{
				ScriptBox.DataSource = null;
				Script.ReloadAll();
				ScriptBox.DataSource = Script.ScriptList;
			}

			if(ScriptBox.Items.Count > 0)
			{
				bool bFoundCurrent = false;
				foreach (object script in ScriptBox.Items)
				{
					if (((Script)script).ScriptName == name)
					{
						ScriptBox.SelectedItem = script;
						ScriptBox.SelectedText = ((Script)script).ToString();
						bFoundCurrent = true;
						break;
					}
				}

				if (bFoundCurrent == false)
				{
					ScriptBox.SelectedIndex = 0;
				}
			}
			
		}

		private void ScriptBox_Changed(object sender, EventArgs e)
		{
			UpdateUI();
		}

		private void UpdateUI()
		{
			Script s = (Script)ScriptBox.SelectedItem;

			if (s != null && s.ScriptInfo.ContainsKey("author"))
			{
				AuthorLabel.Text = "Author: " + s.ScriptInfo["author"];
			}
			else
			{
				AuthorLabel.Text = "Author: N/A";
			}

			if (s != null && s.ScriptInfo.ContainsKey("description"))
			{
				DescriptionLabel.Text = "Description: " + s.ScriptInfo["description"];
			}
			else
			{
				DescriptionLabel.Text = "Description: N/A";
			}

			SettingsPanel.Controls.Clear();

			foreach (KeyValuePair<string, string> k in s.Settings)
			{
				string value = Preset.Current.GetSetting(s.ScriptName, k.Key);
				if (value != null)
				{
					string formType = k.Value;
					if (formType == "ButtonControl")
					{
						ButtonControl b = new ButtonControl();
						SettingsPanel.Controls.Add(b);
						b.Location = new Point(10, 10 + (SettingsPanel.Controls.Count - 1) * 25);
						b.Size = new Size(178, 174);
						b.m_Label.Text = k.Key + ":";
						b.m_Bind.Key = new Key(value);
						b.m_Bind.Location = new Point(118, 0);
						b.Size = new Size(SettingsPanel.Width, 23);
						b.m_Bind.Info.Add("script", s.ScriptName);
						b.m_Bind.Info.Add("setting", k.Key);
						b.m_Bind.Callback = OnKeySettingChanged;
					}
					else if(formType == "X360Control")
					{
						X360ControlItem x = new X360ControlItem();
						SettingsPanel.Controls.Add(x);
						x.m_Label.Text = k.Key + ":";
						x.m_Bind.Location = new Point(118, 0);
						x.Location = new Point(10, 10 + (SettingsPanel.Controls.Count - 1) * 25);
						x.Size = new Size(SettingsPanel.Width, 23);
						x.m_Bind.SelectedIndex = x.m_Bind.FindString(value.ToString());
						x.m_Bind.Info.Add("script", s.ScriptName);
						x.m_Bind.Info.Add("setting", k.Key);
						x.m_Bind.Callback = OnX360KeySettingChanged;

						Form1.Log(value);
						//x.m_Bind.SelectedIndexChanged += OnKeySettingChanged;
						//SettingsPanel.Controls.Add(x);
					}
				}
			}
		}

		private void OnX360KeySettingChanged(Dictionary<string, string> info, X360Key key)
		{
			foreach (Script s in Script.ScriptList)
			{
				if (s.ScriptName == info["script"])
				{
					s.ChangeSetting(info["setting"], key.ToString());
				}
			}
		}

		private void OnKeySettingChanged(Dictionary<string, string> info, Key newKey)
		{
			foreach(Script s in Script.ScriptList)
			{
				if(s.ScriptName == info["script"])
				{
					s.ChangeSetting(info["setting"], newKey.ToString());
				}
			}
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			Script s = (Script)ScriptBox.SelectedItem;

			if (s != null)
			{
				string path = Path.GetFullPath(Script.ScriptFolder + "/" + s.ScriptName + "." + Script.ScriptExtension);
				if (File.Exists(path))
				{
					System.Diagnostics.Process.Start(path);
				}
			}
		}
	}

	public class GrowLabel : Label
	{
		private bool mGrowing;
		public GrowLabel()
		{
			this.AutoSize = false;
		}
		private void resizeLabel()
		{
			if (mGrowing) return;
			try
			{
				mGrowing = true;
				Size sz = new Size(this.Width, Int32.MaxValue);
				sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
				this.Height = sz.Height;
			}
			finally
			{
				mGrowing = false;
			}
		}
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			resizeLabel();
		}
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			resizeLabel();
		}
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			resizeLabel();
		}
	}
}
