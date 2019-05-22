using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

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

			foreach(Script script in Script.ScriptList)
			{
				ScriptBox.Items.Add(script);
			}

			ScriptBox.SelectedIndex = 0;

			PresetLabel.Text = "Preset: " + Preset.Current.m_Name;
			DescriptionLabel.MaximumSize = new Size(SettingsPanel.Width - 3, 0);
			DescriptionLabel.AutoSize = true;

			HLine.BorderStyle = BorderStyle.Fixed3D;

			ActiveControl = PresetLabel;

			ReloadSplitButton.FlatStyle = FlatStyle.Flat;

			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Add("Current");
			menu.Items.Add("All");
			menu.ItemClicked += new ToolStripItemClickedEventHandler(ReloadItem_Clicked);

			ReloadSplitButton.ContextMenuStrip = menu;

			EditButton.Hide();
		}

		private void ReloadItem_Clicked(object sender, ToolStripItemClickedEventArgs e)
		{
			string text = e.ClickedItem.Text;
			if(text == "Current")
			{

			}
			else if(text == "All")
			{

			}
		}

		private void ScriptBox_Changed(object sender, EventArgs e)
		{
			Script s = (Script)ScriptBox.SelectedItem;

			if(s.ScriptInfo.ContainsKey("author"))
			{
				AuthorLabel.Text = "Author: " + s.ScriptInfo["author"];
			}
			else
			{
				AuthorLabel.Text = "Author: N/A";
			}

			if (s.ScriptInfo.ContainsKey("description"))
			{
				DescriptionLabel.Text = "Description: " + s.ScriptInfo["description"];
			}
			else
			{
				DescriptionLabel.Text = "Description: N/A";
			}
		}

		private void EditButton_Click(object sender, EventArgs e)
		{
			Script s = (Script)ScriptBox.SelectedItem;
			if(s != null)
			{
				string path = Script.ScriptFolder + "/" + s.ScriptName + Script.ScriptExtension;

				if(File.Exists(path))
				{
					
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
