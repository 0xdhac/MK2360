using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
using Nett;
using System.Runtime.InteropServices;
using NLog;
using ScpDriverInterface;

namespace MK2360
{
	public partial class Form1 : Form
	{
		public static Form1 MainForm;
		public Form1()
		{
			InitializeComponent();
			MainForm = this;
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;

			PresetNameTextbox.KeyDown += (sender, args) =>
			{
				if (args.KeyCode == Keys.Return)
				{
					SavePresetButton.PerformClick();
				}
			};

			// Check if program never ran or if the config doesn't exist somehow
			if (!Config.Exists())
			{
				Config.m_Config = new Config();
				Config.m_Config.KillSwitch = Input.DIK.F9;
				Config.m_Config.Save();
			}
			else
			{
				try
				{
					Config.m_Config = Toml.ReadFile<Config>("config.cfg");
				}
				catch(ArgumentNullException a)
				{
					Log(a.Message);
				}
			}

			// Make sure there is a preset
			if (Config.m_Config.m_Preset == "")
			{
				Preset p = new Preset();
				p.m_Name = Preset.GetNextAvailableName();
				p.Save();

				Preset.Current = p;
			}
			else
			{
				if(Preset.Exists(Config.m_Config.m_Preset))
				{
					Preset.Current = Preset.Get(Config.m_Config.m_Preset);
				}
				else if(Preset.GetCount() > 0)
				{
					Log("Current preset no longer exists. Attempting to use another preset.");
					Preset.Current = Preset.GetFirst();
				}
				else
				{
					Log("Current preset no longer exists and there are no other presets available. Generating new preset.");
					Preset p = new Preset();
					p.m_Name = Preset.GetNextAvailableName();
					p.Save();

					Preset.Current = p;
				}
			}

			Thread t = new Thread(Intercept);
			t.IsBackground = true;
			t.Start();
			/*
			Thread d = new Thread(Destroy);
			d.IsBackground = true;
			d.Start();
			*/

			using (Process p = Process.GetCurrentProcess())
				p.PriorityClass = ProcessPriorityClass.High;

			PresetList.SelectedIndexChanged      += PresetList_SelectedIndexChanged;
			ProcessComboBox.SelectedIndexChanged += ProcessList_SelectedIndexChanged;
			ProcessComboBox.DropDown             += ProcessComboBox_DropDown;
			ProcessComboBox.DropDownWidth         = 350;
			KillSwitchTextBox.Click += new EventHandler(KillSwitchTextBox_Clicked);

			UpdatePresetList();
			PresetList.SelectedItem = Preset.Current;
			PresetList.Text = Preset.Current.m_Name;
		}

		private void ProcessComboBox_DropDown(object sender, EventArgs e)
		{
			UpdateProcessList();
		}

		private void ProcessList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(Preset.Current != null)
			{
				Preset.Current.m_ProcessItem = (ProcessItem)ProcessComboBox.SelectedItem;
			}
			else
			{
				Log("No preset selected");
			}
		}

		private void Destroy()
		{
			Thread.Sleep(10000);
			Application.Exit();
		}

		private void Intercept()
		{
			IntPtr context = Interception.interception_create_context();
			Interception.Stroke stroke = new Interception.Stroke();
			int device;

			// Check for keyboard changes
			Interception.InterceptionPredicate interception_is_keyboard = Interception.interception_is_keyboard;
			Interception.interception_set_filter(
				context,
				interception_is_keyboard,
				((ushort)Interception.FilterKeyState.KeyDown | 
				(ushort)Interception.KeyState.KeyUp));

			// Check for mouse changes
			Interception.InterceptionPredicate interception_is_mouse = Interception.interception_is_mouse;
			Interception.interception_set_filter(
				context,
				interception_is_mouse,
				(ushort)Interception.FilterMouseState.LeftDown);

			while (Interception.interception_receive(context, device = Interception.interception_wait(context), ref stroke, 1) > 0)
			{
				byte[] strokeBytes = Interception.getBytes(stroke);
				if (Interception.interception_is_keyboard(device) != 0)
				{
					Interception.KeyStroke kstroke = stroke;
					Input i = new Input(kstroke);

					Input.InputAction act = i.CallKeyListeners();

					if((act & Input.InputAction.Block) == 0)
					{
						if((act & Input.InputAction.Change) > 0)
						{

						}

						Interception.interception_send(context, device, strokeBytes, 1);
					}
				}
				else if (Interception.interception_is_mouse(device) != 0)
				{
					Interception.MouseStroke kstroke = stroke;
					Input i = new Input(kstroke);

					Input.InputAction act = i.CallKeyListeners();

					if ((act & Input.InputAction.Block) == 0)
					{
						if ((act & Input.InputAction.Change) > 0)
						{

						}

						Interception.interception_send(context, device, strokeBytes, 1);
					}
				}
			}

			Interception.interception_destroy_context(context);
		}

		private void PresetList_SelectedIndexChanged(object sender, EventArgs e)
		{
			Preset.Current = (Preset)PresetList.SelectedItem;
			PresetNameTextbox.Text = Preset.Current.m_Name;
		}

		private void NewPresetButton_Click(object sender, EventArgs e)
        {
            Preset p = new Preset();
			p.m_Name = Preset.GetNextAvailableName();
			Log("Created new preset '" + p.m_Name + "'.");
			p.Save();

			UpdatePresetList();
			PresetList.SelectedItem = p;
			PresetList.Text = p.m_Name;
        }

        public void UpdatePresetList()
        {
            PresetList.Items.Clear();

            string[] files = Directory.GetFiles(Preset.m_Path + "/", "*." + Preset.m_FileType, SearchOption.TopDirectoryOnly);
            foreach(string s in files)
            {
				PresetList.Items.Add(Toml.ReadFile<Preset>(s));
            }
        }

		public void UpdateProcessList()
		{
			ProcessComboBox.Items.Clear();

			if(Preset.Current.m_ProcessItem != null)
			{
				ProcessComboBox.Items.Add(Preset.Current.m_ProcessItem);
				ProcessComboBox.SelectedIndex = 0;
			}

			Process[] processlist = Process.GetProcesses();

			foreach (Process p in processlist)
			{
				if (p.Id == Process.GetCurrentProcess().Id)
					continue;

				// Don't show processes that aren't even available
				if(p.Threads[0].ThreadState == System.Diagnostics.ThreadState.Wait && p.Threads[0].WaitReason == ThreadWaitReason.Suspended)
					continue;

				// Don't show processes that arent winform applications
				if (p.MainWindowTitle.Length > 0)
				{
					ProcessItem pi = new ProcessItem();
					pi.m_Display = p.MainWindowTitle;
					pi.m_Process = p.ProcessName;
					ProcessComboBox.Items.Add(pi);
				}
			}
		}

        private void SavePresetButton_Click(object sender, EventArgs e)
        {
			int index = PresetList.SelectedIndex;
			if (PresetNameTextbox.Text.Length > 0 &&
				PresetNameTextbox.Text != Preset.Current.m_Name)
            {
				if(PresetNameTextbox.Text.IndexOfAny(Path.GetInvalidPathChars()) == -1 &&
				PresetNameTextbox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
				{
					if (Preset.Rename((Preset)(PresetList.SelectedItem), PresetNameTextbox.Text))
					{
						PresetList.Text = PresetNameTextbox.Text;
					}
				}
				else
				{
					Log("\"Name\" text box contains invalid filename characters.");
				}
			}

			Preset.Current.Save();

			UpdatePresetList();
			PresetList.SelectedIndex = index;	
		}

		public static void Log(string s)
		{
			if (MainForm.InvokeRequired)
			{
				MainForm.Invoke(new Action(() => MainForm.LogTextBox.Text += "- " + s + "\r\n"));
			}
			else
			{
				MainForm.LogTextBox.Text += s + "\r\n";
			}

			var logger = LogManager.GetCurrentClassLogger();
			logger.Info(s);
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if(Preset.GetCount() == 1)
			{
				Log("Can't delete because it's your only preset.");
			}
			else if(Preset.Current != null)
			{
				Preset.Current.Delete();
			}
			else
			{
				Log("No preset selected.");
			}
		}

		private void CreditsButton_Click(object sender, EventArgs e)
		{
			new Credits().ShowDialog();
		}

		private void KillSwitchTextBox_Clicked(object sender, EventArgs e)
		{
			((TextBox)(sender)).Text = "Press any key..";

			Input.AddKeyListener(OnKillSwitchKeyChanged);
		}

		private Input.InputAction OnKillSwitchKeyChanged(Input key)
		{
			if(key.IsKeyboard)
			{
				Interception.KeyStroke ks = key.Stroke;

				Config.m_Config.KillSwitch = (Input.DIK)(ks.code);
				Config.m_Config.Save();

				return Input.InputAction.Stop;
			}
			else
			{
				return Input.InputAction.Continue;
			}
		}
	}

	public class BindList
	{
		public enum BindType
		{
			Mouse,
			Keys
		}

		public Key StartButton { get; set; }
		public Key BackButton { get; set; }
		public Key YButton { get; set; }
		public Key XButton { get; set; }
		public Key AButton { get; set; }
		public Key BButton { get; set; }
		public Key RightJoyPress { get; set; }
		public BindType RightJoyType { get; set; }
		public Key RightJoyMove { get; set; }
		public Key RightJoyUp { get; set; }
		public Key RightJoyDown { get; set; }
		public Key RightJoyRight { get; set; }
		public Key RightJoyLeft { get; set; }
		public Key LeftJoyPress { get; set; }
		public BindType LeftJoyType { get; set; }
		public Key LeftJoyMove { get; set; }
		public Key LeftJoyUp { get; set; }
		public Key LeftJoyDown { get; set; }
		public Key LeftJoyRight { get; set; }
		public Key LeftJoyLeft { get; set; }
		public Key DPadUp { get; set; }
		public Key DPadDown { get; set; }
		public Key DPadRight { get; set; }
		public Key DPadLeft { get; set; }
		public Key LeftTrigger { get; set; }
		public Key LeftBumper { get; set; }
		public Key RightTrigger { get; set; }
		public Key RightBumper { get; set; }
	}

	public class Config
	{
		public static Config m_Config;
		public string m_Preset { get; set; }
		private Input.DIK m_KillSwitch;
		public Input.DIK KillSwitch
		{
			get
			{
				return m_KillSwitch;
			}
			set
			{
				m_KillSwitch = value;

				if(Form1.MainForm.InvokeRequired)
				{
					Form1.MainForm.Invoke(new Action(() => Form1.MainForm.KillSwitchTextBox.Text = m_KillSwitch.ToString()));
				}
				else
				{
					Form1.MainForm.KillSwitchTextBox.Text = m_KillSwitch.ToString();
				}
			}
		}

		public void Save()
		{
			Toml.WriteFile(this, "config.cfg");
		}
		public static void Load()
		{
			m_Config = Toml.ReadFile<Config>("config.cfg");
		}
		public static bool Exists()
		{
			return File.Exists("config.cfg");
		}
	}

	public class ProcessItem
	{
		public string m_Display { get; set; }
		public string m_Process { get; set; }

		public override string ToString()
		{
			return m_Display + " (" + m_Process + ")";
		}
	}

	class OvalPictureBox : PictureBox
	{
		public OvalPictureBox()
		{
			BackColor = Color.DarkGray;
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			using (var gp = new GraphicsPath())
			{
				gp.AddEllipse(new Rectangle(0, 0, Width - 1, Height - 1));
				Region = new Region(gp);
			}
		}
	}
}
