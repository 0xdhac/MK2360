using System;
using System.Windows.Forms;

namespace MK2360
{
	class KeyPanelClass
	{
		private static object m_CurrentPanel = null;
		public static object CurrentPanel
		{
			get => m_CurrentPanel;
			set
			{
				if (m_CurrentPanel != null)
				{
					((Panel)(m_CurrentPanel)).Dispose();
				}

				m_CurrentPanel = value;
			}
		}
	}

	public class JoyControl : Panel
	{
		public bool IsLeft = true;
		public Label m_JoyPress_Label = new Label();
		public BindControl m_JoyPress_Ctrl = new BindControl();
		public Label m_InputType_Label = new Label();
		public ComboBox m_InputType_Box = new ComboBox();
		public BindControl m_BindUp = new BindControl();
		public BindControl m_BindDown = new BindControl();
		public BindControl m_BindRight = new BindControl();
		public BindControl m_BindLeft = new BindControl();
		public Label m_BindUp_Label = new Label();
		public Label m_BindDown_Label = new Label();
		public Label m_BindRight_Label = new Label();
		public Label m_BindLeft_Label = new Label();
		const int LabelPos_X = 0;
		const int LabelPos_Y = 3;
		const int LabelSize_X = 90;
		const int LabelSize_Y = 20;
		const int BindPos_X = LabelPos_X + LabelSize_X;
		const int BindPos_Y = 0;
		const int BindSize_X = 88;
		const int BindSize_Y = 23;
		const int GapY = 25;

		public JoyControl()
		{
			Controls.Add(m_JoyPress_Label);
			m_JoyPress_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y);
			m_JoyPress_Label.Size = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_JoyPress_Label.Text = "Joystick press:";

			Controls.Add(m_JoyPress_Ctrl);
			m_JoyPress_Ctrl.Location = new System.Drawing.Point(BindPos_X, BindPos_Y);
			m_JoyPress_Ctrl.Size = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_InputType_Label);
			m_InputType_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + GapY);
			m_InputType_Label.Size = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_InputType_Label.Text = "Input type:";

			Controls.Add(m_InputType_Box);
			m_InputType_Box.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + GapY);
			m_InputType_Box.Size = new System.Drawing.Size(BindSize_X, BindSize_Y);
			m_InputType_Box.Items.Add(Input.InputType.Keyboard);
			m_InputType_Box.Items.Add(Input.InputType.Mouse);
			m_InputType_Box.DropDownStyle = ComboBoxStyle.DropDownList;
			m_InputType_Box.SelectedIndexChanged += new EventHandler(InputTypeChanged);

			Controls.Add(m_BindUp_Label);
			m_BindUp_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + (GapY * 2));
			m_BindUp_Label.Size = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindUp_Label.Text = "Joystick Up:";

			Controls.Add(m_BindUp);
			m_BindUp.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + (GapY * 2));
			m_BindUp.Size = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_BindDown_Label);
			m_BindDown_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + (GapY * 3));
			m_BindDown_Label.Size = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindDown_Label.Text = "Joystick Down:";

			Controls.Add(m_BindDown);
			m_BindDown.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + (GapY * 3));
			m_BindDown.Size = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_BindRight_Label);
			m_BindRight_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + (GapY * 4));
			m_BindRight_Label.Size = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindRight_Label.Text = "Joystick Right:";

			Controls.Add(m_BindRight);
			m_BindRight.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + (GapY * 4));
			m_BindRight.Size = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_BindLeft_Label);
			m_BindLeft_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + (GapY * 5));
			m_BindLeft_Label.Size = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindLeft_Label.Text = "Joystick Left:";

			Controls.Add(m_BindLeft);
			m_BindLeft.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + (GapY * 5));
			m_BindLeft.Size = new System.Drawing.Size(BindSize_X, BindSize_Y);
		}

		private void InputTypeChanged(object sender, EventArgs e)
		{
			if(IsLeft)
				Preset.Current.m_BindList.LeftJoyType = (Input.InputType)m_InputType_Box.SelectedItem;
			else
				Preset.Current.m_BindList.RightJoyType = (Input.InputType)m_InputType_Box.SelectedItem;

			Input.InputType t = (Input.InputType)m_InputType_Box.SelectedItem;
			if (t == Input.InputType.Mouse)
			{
				m_BindUp.Hide();
				m_BindUp_Label.Hide();
				m_BindDown.Hide();
				m_BindDown_Label.Hide();
				m_BindRight.Hide();
				m_BindRight_Label.Hide();
				m_BindLeft.Hide();
				m_BindLeft_Label.Hide();
			}
			else if(t == Input.InputType.Keyboard)
			{
				m_BindUp.Show();
				m_BindUp_Label.Show();
				m_BindDown.Show();
				m_BindDown_Label.Show();
				m_BindRight.Show();
				m_BindRight_Label.Show();
				m_BindLeft.Show();
				m_BindLeft_Label.Show();
			}
		}
	}

	public class ButtonControl : Panel
	{
		public BindControl m_Bind = new BindControl();
		public Label m_Label      = new Label();

		public ButtonControl()
		{
			Controls.Add(m_Label);
			m_Label.Location = new System.Drawing.Point(0, 3);
			m_Label.Size = new System.Drawing.Size(70, 20);

			Controls.Add(m_Bind);
			m_Bind.Location = new System.Drawing.Point(70, 0);
			m_Bind.Size = new System.Drawing.Size(108, 20);
		}
	}
	
	public class DPadControl : Panel
	{
		public BindControl m_BindUp = new BindControl();
		public BindControl m_BindDown = new BindControl();
		public BindControl m_BindRight = new BindControl();
		public BindControl m_BindLeft = new BindControl();
		public Label m_BindUp_Label = new Label();
		public Label m_BindDown_Label = new Label();
		public Label m_BindRight_Label = new Label();
		public Label m_BindLeft_Label = new Label();
		const int LabelPos_X = 0;
		const int LabelPos_Y = 3;
		const int LabelSize_X = 90;
		const int LabelSize_Y = 20;
		const int BindPos_X = LabelPos_X + LabelSize_X;
		const int BindPos_Y = 0;
		const int BindSize_X = 88;
		const int BindSize_Y = 23;
		const int GapY = 25;

		public DPadControl()
		{
			Controls.Add(m_BindUp_Label);
			m_BindUp_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y);
			m_BindUp_Label.Size     = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindUp_Label.Text     = "DPad Up:";

			Controls.Add(m_BindUp);
			m_BindUp.Location = new System.Drawing.Point(BindPos_X, BindPos_Y);
			m_BindUp.Size     = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_BindDown_Label);
			m_BindDown_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + GapY);
			m_BindDown_Label.Size     = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindDown_Label.Text     = "DPad Down:";

			Controls.Add(m_BindDown);
			m_BindDown.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + GapY);
			m_BindDown.Size     = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_BindRight_Label);
			m_BindRight_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + (GapY * 2));
			m_BindRight_Label.Size     = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindRight_Label.Text     = "DPad Right:";

			Controls.Add(m_BindRight);
			m_BindRight.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + (GapY * 2));
			m_BindRight.Size     = new System.Drawing.Size(BindSize_X, BindSize_Y);

			Controls.Add(m_BindLeft_Label);
			m_BindLeft_Label.Location = new System.Drawing.Point(LabelPos_X, LabelPos_Y + (GapY * 3));
			m_BindLeft_Label.Size     = new System.Drawing.Size(LabelSize_X, LabelSize_Y);
			m_BindLeft_Label.Text     = "DPad Left:";

			Controls.Add(m_BindLeft);
			m_BindLeft.Location = new System.Drawing.Point(BindPos_X, BindPos_Y + (GapY * 3));
			m_BindLeft.Size     = new System.Drawing.Size(BindSize_X, BindSize_Y);
		}
	}

	public class BindControl : TextBox
	{
		private Key m_Key = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key Key
		{
			set
			{
				m_Key = value;
				Text = m_Key.ToString();
			}
			get
			{
				return m_Key;
			}
		}
		public BindControl()
		{
			Cursor = Cursors.Hand;
			ReadOnly = true;
			Click += new EventHandler(BindControl_Click);
			Text = m_Key.ToString();
		}

		/*
		public BindControl(Key key)
		{
			Cursor = Cursors.Hand;
			ReadOnly = true;
			Click += new EventHandler(BindControl_Click);
			m_Key = key;
		}
		*/

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if(InvokeRequired)
				{
					Invoke(new Action(() => base.Text = value));
				}
				else
				{
					base.Text = value;
				}
			}
		}

		public void BindControl_Click(object sender, EventArgs e)
		{
			Text = "Press any key..";
			Input.AddKeyListener(OnKeyPress);
		}
		private Input.InputAction OnKeyPress(Input key)
		{
			if(key.m_InputState == Input.InputState.Up)
			{
				return Input.InputAction.Continue;
			}

			if(key.m_InputType == Input.InputType.Mouse)
			{
				Interception.MouseStroke s = key.Stroke;
				if (s.state == 0)
				{
					return Input.InputAction.Continue;
				}
			}

			if(key.m_InputType == Input.InputType.Keyboard && key.Code.ToString() == Config.m_Config.m_KillSwitch.ToString())
			{
				Form1.Log("Please avoid binding keys to the same key as your kill switch key.");
				return Input.InputAction.Continue;
			}

			m_Key.m_InputType        = key.m_InputType;
			m_Key.Code               = key.Code;
			Text                     = key.ToString();
			Click                   += new EventHandler(BindControl_Click);

			return Input.InputAction.Stop;
		}
	}
}
