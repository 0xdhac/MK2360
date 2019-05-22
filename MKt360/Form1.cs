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
using System.Text;

namespace MK2360
{
	public partial class Form1 : Form
	{
		public static Form1 MainForm;
		public Form1()
		{
			
			MainForm = this;
			InitializeComponent();
			FormClosing += Form1_Closing;
			
			// Check if program never ran or if the config doesn't exist somehow
			if (!Config.Exists())
			{
				Config.m_Config = new Config();
				Config.m_Config.Save();
			}
			else
			{
				try
				{
					Config.m_Config = Toml.ReadFile<Config>("config.cfg");
				}
				catch (ArgumentNullException a)
				{
					Log(a.Message);
				}
			}

			KillSwitchTextBox.Text = Config.m_Config.m_KillSwitch.ToString();

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
				if (Preset.Exists(Config.m_Config.m_Preset))
				{
					Preset.Current = Preset.Get(Config.m_Config.m_Preset);
				}
				else if (Preset.GetCount() > 0)
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

			Thread u = new Thread(Scripts);
			u.IsBackground = true;
			u.Start();

			Thread t = new Thread(Intercept);
			t.IsBackground = true;
			t.Start();

			/*
			Thread d = new Thread(Destroy);
			d.IsBackground = true;
			d.Start();
			*/

			// Update UI
			UpdatePresetList();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			PresetList.SelectedItem = Preset.Current;
			PresetList.Text = Preset.Current.m_Name;
			PresetList.SelectedIndexChanged += PresetList_SelectedIndexChanged;
			ProcessComboBox.SelectedIndexChanged += ProcessList_SelectedIndexChanged;
			ProcessComboBox.DropDown += ProcessComboBox_DropDown;
			ProcessComboBox.DropDownWidth = 350;
			LogTextBox.ScrollBars = ScrollBars.Vertical;

			// Bindings
			ControllerModeButton.Click += ControllerModeButton_Click;
			KillSwitchTextBox.Click += KillSwitchTextBox_Clicked;
			AButton.Click += AButton_Click;
			YButton.Click += YButton_Click;
			XButton.Click += XButton_Click;
			BButton.Click += BButton_Click;
			StartButton.Click += StartButton_Click;
			BackButton.Click += BackButton_Click;
			RightJoystickButton.Click += RightJoy_Click;
			LeftJoystickButton.Click += LeftJoy_Click;
			DpadButton.Click += DPad_Click;
			LTButton.Click += LTrigger_Click;
			LBButton.Click += LBButton_Click;
			RTButton.Click += RTrigger_Click;
			RBButton.Click += RBButton_Click;

			ControllerModeButton.FlatStyle = FlatStyle.Flat;

#if !NOVERIFY
			Thread v = new Thread(ValidateSelfThread);
			v.IsBackground = true;
			v.Start();
#endif
		}

		private void Scripts()
		{
			new Script("holdtoedit");
			new Script("wallreplace");
		}

		private void ValidateSelfThread()
		{
			while(true)
			{
				if (ValidateSelf() != "SUCCESS")
				{
					Application.Exit();
				}

				Thread.Sleep(300000);
			}
		}

		private static string ValidateSelf()
		{
			string validateUrl = "http://oxdmacro.site.nfoservers.com/validate.php";
			string sHash = Program.GetExecutingFileHash();

			LoginForm.webClient.QueryString.Clear();
			LoginForm.webClient.QueryString.Add("hash", sHash);
			var data = LoginForm.webClient.UploadValues(validateUrl, "POST", LoginForm.webClient.QueryString);
			return Encoding.UTF8.GetString(data);
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Exit();
		}

		private void ControllerModeButton_Click(object sender, EventArgs e)
		{
			if (XMode.IsActive())
			{
				XMode.Stop(true);
			}
			else
			{
				XMode.Start();
			}
		}

		public void HidePresetControls()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() =>
				{
					PresetLabel.Hide();
					PresetList.Hide();
					PresetNameLabel.Hide();
					PresetNameTextbox.Hide();
					ProcessLabel.Hide();
					ProcessComboBox.Hide();
					NewPresetButton.Hide();
					SavePresetButton.Hide();
					DeleteButton.Hide();
					KeyPanelClass.CurrentPanel = null;
				}));
			}
			else
			{
				PresetLabel.Hide();
				PresetList.Hide();
				PresetNameLabel.Hide();
				PresetNameTextbox.Hide();
				ProcessLabel.Hide();
				ProcessComboBox.Hide();
				NewPresetButton.Hide();
				SavePresetButton.Hide();
				DeleteButton.Hide();
				KeyPanelClass.CurrentPanel = null;
			}
		}

		public void ShowPresetControls()
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() =>
				{
					PresetLabel.Show();
					PresetList.Show();
					PresetNameLabel.Show();
					PresetNameTextbox.Show();
					ProcessLabel.Show();
					ProcessComboBox.Show();
					NewPresetButton.Show();
					SavePresetButton.Show();
					DeleteButton.Show();
				}));
			}
			else
			{
				PresetLabel.Show();
				PresetList.Show();
				PresetNameLabel.Show();
				PresetNameTextbox.Show();
				ProcessLabel.Show();
				ProcessComboBox.Show();
				NewPresetButton.Show();
				SavePresetButton.Show();
				DeleteButton.Show();
			}
		}

		private void LeftJoy_Click(object sender, EventArgs e)
		{
			JoyControl b = new JoyControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.IsLeft = true;
			b.m_InputType_Box.SelectedIndex = (int)Preset.Current.m_BindList.LeftJoyType;
			b.m_JoyPress_Ctrl.Key = Preset.Current.m_BindList.LeftJoyPress;
			b.m_BindUp.Key = Preset.Current.m_BindList.LeftJoyUp;
			b.m_BindDown.Key = Preset.Current.m_BindList.LeftJoyDown;
			b.m_BindRight.Key = Preset.Current.m_BindList.LeftJoyRight;
			b.m_BindLeft.Key = Preset.Current.m_BindList.LeftJoyLeft;

			KeyPanelClass.CurrentPanel = b;
		}
		private void RightJoy_Click(object sender, EventArgs e)
		{
			JoyControl b = new JoyControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.IsLeft = false;
			b.m_InputType_Box.SelectedIndex = (int)Preset.Current.m_BindList.RightJoyType;
			b.m_JoyPress_Ctrl.Key = Preset.Current.m_BindList.RightJoyPress;
			b.m_BindUp.Key = Preset.Current.m_BindList.RightJoyUp;
			b.m_BindDown.Key = Preset.Current.m_BindList.RightJoyDown;
			b.m_BindRight.Key = Preset.Current.m_BindList.RightJoyRight;
			b.m_BindLeft.Key = Preset.Current.m_BindList.RightJoyLeft;

			KeyPanelClass.CurrentPanel = b;
		}
		private void DPad_Click(object sender, EventArgs e)
		{
			DPadControl b = new DPadControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_BindUp.Key = Preset.Current.m_BindList.DPadUp;
			b.m_BindDown.Key = Preset.Current.m_BindList.DPadDown;
			b.m_BindRight.Key = Preset.Current.m_BindList.DPadRight;
			b.m_BindLeft.Key = Preset.Current.m_BindList.DPadLeft;

			KeyPanelClass.CurrentPanel = b;
		}
		private void RBButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Right bumper: ";
			b.m_Bind.Key = Preset.Current.m_BindList.RightBumper;

			KeyPanelClass.CurrentPanel = b;
		}
		private void RTrigger_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Right trigger: ";
			b.m_Bind.Key = Preset.Current.m_BindList.RightTrigger;

			KeyPanelClass.CurrentPanel = b;
		}
		private void LBButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Left bumper: ";
			b.m_Bind.Key = Preset.Current.m_BindList.LeftBumper;

			KeyPanelClass.CurrentPanel = b;
		}
		private void LTrigger_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Left trigger: ";
			b.m_Bind.Key = Preset.Current.m_BindList.LeftTrigger;

			KeyPanelClass.CurrentPanel = b;
		}
		private void BackButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Back key: ";
			b.m_Bind.Key = Preset.Current.m_BindList.BackButton;

			KeyPanelClass.CurrentPanel = b;
		}
		private void StartButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Start key: ";
			b.m_Bind.Key = Preset.Current.m_BindList.StartButton;

			KeyPanelClass.CurrentPanel = b;
		}
		private void BButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "B key: ";
			b.m_Bind.Key = Preset.Current.m_BindList.BButton;

			KeyPanelClass.CurrentPanel = b;
		}
		private void XButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "X key: ";
			b.m_Bind.Key = Preset.Current.m_BindList.XButton;

			KeyPanelClass.CurrentPanel = b;
		}
		private void YButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "Y key: ";
			b.m_Bind.Key = Preset.Current.m_BindList.YButton;

			KeyPanelClass.CurrentPanel = b;
		}
		private void AButton_Click(object sender, EventArgs e)
		{
			ButtonControl b = new ButtonControl();
			Controls.Add(b);
			b.Location = new Point(10, 176);
			b.Size = new Size(178, 174);
			b.m_Label.Text = "A key: ";
			b.m_Bind.Key = Preset.Current.m_BindList.AButton;

			KeyPanelClass.CurrentPanel = b;
		}
		private void ProcessComboBox_DropDown(object sender, EventArgs e)
		{
			UpdateProcessList();
		}
		private void ProcessList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Preset.Current != null)
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
				(ushort)Interception.FilterKeyState.KeyUp));

			// Check for mouse changes
			Interception.InterceptionPredicate interception_is_mouse = Interception.interception_is_mouse;
			Interception.interception_set_filter(
				context,
				interception_is_mouse,
				(ushort)Interception.FilterMouseState.All);

			while (Interception.interception_receive(context, device = Interception.interception_wait(context), ref stroke, 1) > 0)
			{
				byte[] strokeBytes = Interception.getBytes(stroke);
				if (Interception.interception_is_keyboard(device) != 0)
				{
					Interception.KeyStroke kstroke = stroke;
					Input i = new Input(kstroke);
					if(i.IsChanged)
					{
						Input.InputAction act = i.CallKeyListeners();

						if ((act & Input.InputAction.Block) == 0)
							Interception.interception_send(context, device, strokeBytes, 1);
					}
					else
					{
						Interception.interception_send(context, device, strokeBytes, 1);
					}
				}
				else if (Interception.interception_is_mouse(device) != 0)
				{
					Interception.MouseStroke kstroke = stroke;
					Input i = new Input(kstroke);

					if (i.IsChanged)
					{
						Input.InputAction act = i.CallKeyListeners();

						if ((act & Input.InputAction.Block) == 0)
							Interception.interception_send(context, device, strokeBytes, 1);
					}
					else
					{
						Interception.interception_send(context, device, strokeBytes, 1);
					}
				}
			}

			Interception.interception_destroy_context(context);
		}
		private void PresetList_SelectedIndexChanged(object sender, EventArgs e)
		{
			Preset.Current = (Preset)PresetList.SelectedItem;
		}
		private void PresetList_TextChanged(object sender, EventArgs e)
		{
			Preset.Current.m_Name = ((ComboBox)(sender)).Text;
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
			foreach (string s in files)
			{
				PresetList.Items.Add(Toml.ReadFile<Preset>(s));
			}
		}
		public void UpdateProcessList()
		{
			ProcessComboBox.Items.Clear();

			if (Preset.Current.m_ProcessItem != null)
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
				if (p.Threads[0].ThreadState == System.Diagnostics.ThreadState.Wait && p.Threads[0].WaitReason == ThreadWaitReason.Suspended)
					continue;

				// Don't show processes that arent winform applications
				if (p.MainWindowTitle.Length > 0)
				{
					ProcessItem pi = new ProcessItem();
					pi.m_Display = p.MainWindowTitle.Trim();
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
				if (PresetNameTextbox.Text.IndexOfAny(Path.GetInvalidPathChars()) == -1 &&
				PresetNameTextbox.Text.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
				{
					if (!Preset.Exists(PresetNameTextbox.Text))
					{
						if (Preset.Rename(Preset.Current, PresetNameTextbox.Text))
						{
							PresetList.Text = PresetNameTextbox.Text;
						}
					}
					else
					{
						Log("Preset '" + PresetNameTextbox.Text + "' already exists.");
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
				MainForm.Invoke(new Action(() => MainForm.LogTextBox.AppendText("- " + s + "\r\n")));
			}
			else
			{
				MainForm.LogTextBox.AppendText(s + "\r\n");
			}

			var logger = LogManager.GetCurrentClassLogger();
			logger.Info(s);
		}
		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (Preset.GetCount() == 1)
			{
				Log("Can't delete because it's your only preset.");
			}
			else if (Preset.Current != null)
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
			if (key.m_InputType == Input.InputType.Keyboard)
			{
				Interception.KeyStroke ks = key.Stroke;

				Config.m_Config.m_KillSwitch = (Input.DIK)(ks.code);
				Config.m_Config.Save();

				Invoke(new Action(() => KillSwitchTextBox.Text = Config.m_Config.m_KillSwitch.ToString()));

				return Input.InputAction.Stop;
			}
			else
			{
				return Input.InputAction.Continue;
			}
		}

		private void MacrosButton_Click(object sender, EventArgs e)
		{
			new MacroForm().ShowDialog();
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

	public class ReadOnlyTextBox : TextBox
	{
		[DllImport("user32.dll")]
		static extern bool HideCaret(IntPtr hWnd);

		public ReadOnlyTextBox()
		{
			ReadOnly = true;
			BackColor = Color.White;
			GotFocus += TextBoxGotFocus;
			Cursor = Cursors.Arrow; // mouse cursor like in other controls
		}

		private void TextBoxGotFocus(object sender, EventArgs args)
		{
			HideCaret(Handle);
		}
	}
}
