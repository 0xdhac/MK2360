using System;
using System.Text;
using System.Runtime.InteropServices;
using ScpDriverInterface;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace MK2360
{
	public class XMode
	{
		[DllImport("user32.dll", EntryPoint = "BlockInput")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)] bool fBlockIt);

		private const int m_SampleCount			= 3;
		private static List<Pair> m_Past		= new List<Pair>();
		private static long m_LastElapsed		= 0;
		private static Stopwatch m_Stopwatch	= new Stopwatch();
		private static bool m_bStopThread		= false;
		public static ScpBus m_Bus				= new ScpBus();
		public static X360Controller m_Ctrlr	= new X360Controller();
		private static bool m_Active			= false;
		private static Thread m_MouseThread;
		private static List<string> m_DisabledButtons = new List<string>();
		public static bool Start()
		{
			if (m_Active == true)
			{
				Form1.Log("Controller mode is already active.");
				return false;
			}

			if(Preset.Current == null)
			{
				Form1.Log("No active preset, can't start.");
				return false;
			}

			if(Preset.Current.m_ProcessItem == null)
			{
				Form1.Log("Can't start unless you specify a process.");
				return false;
			}

			Input.AddKeyListener(OnKeyPress);
			Form1.MainForm.HidePresetControls();
			if (Form1.MainForm.InvokeRequired)
				Form1.MainForm.Invoke(new Action(() => Form1.MainForm.ControllerModeButton.Text = "Stop"));
			else
				Form1.MainForm.ControllerModeButton.Text = "Stop";
			
			m_Bus.PlugIn(2);
			m_Stopwatch.Start();
			m_bStopThread = false;
			m_MouseThread = new Thread(CheckMouse);
			m_MouseThread.IsBackground = true;
			m_MouseThread.Start();

			m_Active = true;
			return true;
		}

		public static void Stop(bool remove)
		{
			Form1.MainForm.ShowPresetControls();
			if(remove)
				Input.RemoveKeyListener(OnKeyPress);
			

			if (Form1.MainForm.InvokeRequired)
				Form1.MainForm.Invoke(new Action(() => Form1.MainForm.ControllerModeButton.Text = "Start"));
			else
				Form1.MainForm.ControllerModeButton.Text = "Start";

			m_Bus.Unplug(2);
			m_Stopwatch.Stop();
			m_bStopThread = true;
			m_Active = false;
		}

		public static bool IsActive()
		{
			return m_Active;
		}

		public static void DisableOriginalBind(string button)
		{
			if (!m_DisabledButtons.Contains(button))
				m_DisabledButtons.Add(button);
		}

		public static void EnableOriginalBind(string button)
		{
			if (m_DisabledButtons.Contains(button))
				m_DisabledButtons.Remove(button);
		}

		private static Input.InputAction OnKeyPress(Input i)
		{
			if (m_Active == false)
			{
				return Input.InputAction.Continue;
			}

			if(!bInputBlocked)
			{
				return Input.InputAction.Continue;
			}

			if(!i.IsChanged.Contains(true))
			{
				return Input.InputAction.Block;
			}

			ConvertTo360(i);

			return Input.InputAction.Block;
		}

		private static void ConvertTo360(Input i)
		{
			BindList b = Preset.Current.m_BindList;
			bool bPastUpdated = false;

			for(int idx = 0; idx < i.m_InputState.Count; idx++)
			{
				if (!m_DisabledButtons.Contains("A") && i.m_InputType == b.AButton.m_InputType && i.Code[idx] == b.AButton.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.A;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.A;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.A;
						ImpulseRelease(X360Buttons.A);
					}
				}

				if (!m_DisabledButtons.Contains("X") && i.m_InputType == b.XButton.m_InputType && i.Code[idx] == b.XButton.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.X;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.X;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.X;
						ImpulseRelease(X360Buttons.X);
					}
				}

				if (!m_DisabledButtons.Contains("Y") && i.m_InputType == b.YButton.m_InputType && i.Code[idx] == b.YButton.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Y;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Y;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Y;
						ImpulseRelease(X360Buttons.Y);
					}
				}

				if (!m_DisabledButtons.Contains("B") && i.m_InputType == b.BButton.m_InputType && i.Code[idx] == b.BButton.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.B;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.B;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.B;
						ImpulseRelease(X360Buttons.B);
					}
				}

				if (!m_DisabledButtons.Contains("START") && i.m_InputType == b.StartButton.m_InputType && i.Code[idx] == b.StartButton.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Start;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Start;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Start;
						ImpulseRelease(X360Buttons.Start);
					}
				}

				if (!m_DisabledButtons.Contains("BACK") && i.m_InputType == b.BackButton.m_InputType && i.Code[idx] == b.BackButton.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Back;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Back;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Back;
						ImpulseRelease(X360Buttons.Back);
					}
				}

				if (!m_DisabledButtons.Contains("UP") && i.m_InputType == b.DPadUp.m_InputType && i.Code[idx] == b.DPadUp.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Up;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Up;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Up;
						ImpulseRelease(X360Buttons.Up);
					}
				}

				if (!m_DisabledButtons.Contains("DOWN") && i.m_InputType == b.DPadDown.m_InputType && i.Code[idx] == b.DPadDown.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Down;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Down;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Down;
						ImpulseRelease(X360Buttons.Down);
					}
				}

				if (!m_DisabledButtons.Contains("RIGHT") && i.m_InputType == b.DPadRight.m_InputType && i.Code[idx] == b.DPadRight.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Right;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Right;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Right;
						ImpulseRelease(X360Buttons.Right);
					}
				}

				if (!m_DisabledButtons.Contains("LEFT") && i.m_InputType == b.DPadLeft.m_InputType && i.Code[idx] == b.DPadLeft.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.Left;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.Left;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.Left;
						ImpulseRelease(X360Buttons.Left);
					}
				}

				if (!m_DisabledButtons.Contains("LB") && i.m_InputType == b.LeftBumper.m_InputType && i.Code[idx] == b.LeftBumper.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.LeftBumper;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.LeftBumper;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.LeftBumper;
						ImpulseRelease(X360Buttons.LeftBumper);
					}
				}

				if (!m_DisabledButtons.Contains("RB") && i.m_InputType == b.RightBumper.m_InputType && i.Code[idx] == b.RightBumper.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.RightBumper;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.RightBumper;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.RightBumper;
						ImpulseRelease(X360Buttons.RightBumper);
					}
				}

				if (!m_DisabledButtons.Contains("LT") && i.m_InputType == b.LeftTrigger.m_InputType && i.Code[idx] == b.LeftTrigger.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.LeftTrigger = 255;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.LeftTrigger = 0;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.LeftTrigger = 255;
						Task.Run(() =>
						{
							Thread.Sleep(10);
							m_Ctrlr.LeftTrigger = 0;
							m_Bus.Report(2, m_Ctrlr.GetReport());
						});
					}
				}

				if (!m_DisabledButtons.Contains("RT") && i.m_InputType == b.RightTrigger.m_InputType && i.Code[idx] == b.RightTrigger.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.RightTrigger = 255;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.RightTrigger = 0;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.RightTrigger = 255;
						Task.Run(() =>
						{
							Thread.Sleep(10);
							m_Ctrlr.RightTrigger = 0;
							m_Bus.Report(2, m_Ctrlr.GetReport());
						});
					}
				}

				if (!m_DisabledButtons.Contains("LJOY") && i.m_InputType == b.LeftJoyPress.m_InputType && i.Code[idx] == b.LeftJoyPress.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.LeftStick;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.LeftStick;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.LeftStick;
						ImpulseRelease(X360Buttons.LeftStick);
					}
				}

				if (!m_DisabledButtons.Contains("RJOY") && i.m_InputType == b.RightJoyPress.m_InputType && i.Code[idx] == b.RightJoyPress.Code)
				{
					if (i.m_InputState[idx] == Input.InputState.Down)
					{
						m_Ctrlr.Buttons |= X360Buttons.RightStick;
					}
					else if (i.m_InputState[idx] == Input.InputState.Up)
					{
						m_Ctrlr.Buttons &= ~X360Buttons.RightStick;
					}
					else if (i.m_InputState[idx] == Input.InputState.Impulse)
					{
						m_Ctrlr.Buttons |= X360Buttons.RightStick;
						ImpulseRelease(X360Buttons.RightStick);
					}
				}

				if (b.RightJoyType == Input.InputType.Keyboard)
				{
					if ((i.m_InputType == b.RightJoyUp.m_InputType && i.Code[idx] == b.RightJoyUp.Code) ||
						(i.m_InputType == b.RightJoyDown.m_InputType && i.Code[idx] == b.RightJoyDown.Code))
					{
						short y = 0;
						if (Input.GetKeyState(b.RightJoyUp))
							y += short.MaxValue;

						if (Input.GetKeyState(b.RightJoyDown))
							y -= short.MinValue;

						m_Ctrlr.RightStickY = y;
					}

					if ((i.m_InputType == b.RightJoyRight.m_InputType && i.Code[idx] == b.RightJoyRight.Code) ||
						(i.m_InputType == b.RightJoyLeft.m_InputType && i.Code[idx] == b.RightJoyLeft.Code))
					{
						short x = 0;
						if (Input.GetKeyState(b.RightJoyLeft))
							x -= short.MinValue;

						if (Input.GetKeyState(b.RightJoyRight))
							x += short.MaxValue;

						m_Ctrlr.RightStickX = x;
					}
				}

				if (b.LeftJoyType == Input.InputType.Keyboard)
				{
					if ((i.m_InputType == b.LeftJoyUp.m_InputType && i.Code[idx] == b.LeftJoyUp.Code) ||
						(i.m_InputType == b.LeftJoyDown.m_InputType && i.Code[idx] == b.LeftJoyDown.Code))
					{
						short y = 0;
						if (Input.GetKeyState(b.LeftJoyUp))
							y += short.MaxValue;

						if (Input.GetKeyState(b.LeftJoyDown))
							y -= short.MinValue;

						m_Ctrlr.LeftStickY = y;
					}

					if ((i.m_InputType == b.LeftJoyRight.m_InputType && i.Code[idx] == b.LeftJoyRight.Code) ||
						(i.m_InputType == b.LeftJoyLeft.m_InputType && i.Code[idx] == b.LeftJoyLeft.Code))
					{
						short x = 0;
						if (Input.GetKeyState(b.LeftJoyLeft))
							x -= short.MinValue;

						if (Input.GetKeyState(b.LeftJoyRight))
							x += short.MaxValue;

						m_Ctrlr.LeftStickX = x;
					}
				}
			}

			if (b.RightJoyType == Input.InputType.Mouse && i.m_InputType == Input.InputType.Mouse)
			{
				UpdatePast(i);
				bPastUpdated = true;
				CalcJoy(value => m_Ctrlr.RightStickX = value, value => m_Ctrlr.RightStickY = value);
			}

			if (b.LeftJoyType == Input.InputType.Mouse && i.m_InputType == Input.InputType.Mouse)
			{
				if (!bPastUpdated)
					UpdatePast(i);
				CalcJoy(value => m_Ctrlr.LeftStickX = value, value => m_Ctrlr.LeftStickY = value);
			}

			m_Bus.Report(2, m_Ctrlr.GetReport());
		}

		private static async void ImpulseRelease(X360Buttons b)
		{
			await Task.Run(() =>
			{
				Thread.Sleep(10);
				m_Ctrlr.Buttons &= ~b;
				m_Bus.Report(2, m_Ctrlr.GetReport());
			});
		}
		private static void CalcJoy(Action<short> x, Action<short> y)
		{
			Pair p = GetPastAverage();
			x((short)p.x);
			y((short)p.y);
		}
		private static void UpdatePast(Input i)
		{
			m_LastElapsed = m_Stopwatch.ElapsedMilliseconds;
			// Get mouse data
			Interception.MouseStroke strk = i.Stroke;
			int mX = strk.x;
			int mY = strk.y;

			// Convert data to joystick values
			int convX = mX * Config.m_Config.m_Sens;
			int convY = -(mY * Config.m_Config.m_Sens);

			int offset = Config.m_Config.m_AntiAccelerationOffset;
			convX += (convX > 0) ? offset : ((convX < 0) ? -offset : 0);
			convY += (convY > 0) ? offset : ((convY < 0) ? -offset : 0);
			convX = (convX < short.MinValue) ? short.MinValue : ((convX > short.MaxValue) ? short.MaxValue : convX);
			convY = (convY < short.MinValue) ? short.MinValue : ((convY > short.MaxValue) ? short.MaxValue : convY);

			Pair p = new Pair(convX, convY);
			m_Past.Add(p);
			while (m_Past.Count > m_SampleCount)
				m_Past.RemoveAt(0);
		}

		private static bool bInputBlocked = false;
		private static void CheckMouse()
		{
			while(m_bStopThread == false)
			{
				const long msElapse = 15;
				if (Preset.Current.m_BindList.RightJoyType == Input.InputType.Mouse)
				{
					if (m_Stopwatch.ElapsedMilliseconds - m_LastElapsed > msElapse)
					{
						if (m_Ctrlr.RightStickX != 0 || m_Ctrlr.RightStickY != 0)
						{
							m_Ctrlr.RightStickX = 0;
							m_Ctrlr.RightStickY = 0;
							m_Bus.Report(2, m_Ctrlr.GetReport());
						}
					}
				}

				if (Preset.Current.m_BindList.LeftJoyType == Input.InputType.Mouse)
				{
					if (m_Stopwatch.ElapsedMilliseconds - m_LastElapsed > msElapse)
					{
						if (m_Ctrlr.LeftStickX != 0 || m_Ctrlr.LeftStickY != 0)
						{
							m_Ctrlr.LeftStickX = 0;
							m_Ctrlr.LeftStickY = 0;
							m_Bus.Report(2, m_Ctrlr.GetReport());
						}
					}
				}

				var active = GetActiveWindowTitle();

				if (active != null && active.Trim() == Preset.Current.m_ProcessItem.m_Display)
				{
					if(!bInputBlocked)
					{
						BlockInput(true);
						bInputBlocked = true;
					}
				}
				else if(bInputBlocked)
				{
					BlockInput(false);
					bInputBlocked = false;
				}

				Thread.Sleep(2);
			}

		}
		private static Pair GetPastAverage()
		{
			int total = 0;
			Pair sumOfPairs = new Pair(0, 0);
			foreach (Pair p in m_Past)
			{
				total++;
				sumOfPairs.x += p.x;
				sumOfPairs.y += p.y;
			}

			sumOfPairs.x = (int)Math.Round((float)sumOfPairs.x / total);
			sumOfPairs.y = (int)Math.Round((float)sumOfPairs.y / total);

			return sumOfPairs;
		}

		[DllImport("user32.dll")]
		static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		private static string GetActiveWindowTitle()
		{
			const int nChars = 256;
			StringBuilder Buff = new StringBuilder(nChars);
			IntPtr handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, nChars) > 0)
			{
				return Buff.ToString();
			}
			return null;
		}
	}

	class Pair
	{
		public Pair(int xin, int yin)
		{
			x = xin;
			y = yin;
		}

		public int x;
		public int y;
	}
}
