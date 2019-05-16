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
		/*
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
		*/
		private const int m_SampleCount			= 3;
		private static List<Pair> m_Past		= new List<Pair>();
		private static long m_LastElapsed		= 0;
		private static Stopwatch m_Stopwatch	= new Stopwatch();
		private static bool m_bStopThread		= false;
		private static ScpBus m_Bus				= new ScpBus();
		private static X360Controller m_Ctrlr	= new X360Controller();
		private static bool m_Active			= false;
		private static Thread m_MouseThread;
		public static bool Start()
		{
			/*
			bool isDebuggerPresent = false;
			CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);

			if (isDebuggerPresent)
			{
				return false;
			}

			int unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			if (unixTimestamp - 1557732990 > 604800)
			{
				return false;
			}
			*/

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

		public static void Stop(bool remove){
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

		public static bool IsActive(){
			return m_Active;
		}

		private static Input.InputAction OnKeyPress(Input i)
		{
			if (m_Active == false)
			{
				return Input.InputAction.Continue;
			}

			if(i.m_InputType == Input.InputType.Keyboard && Config.m_Config.m_KillSwitch == (Input.DIK)i.Code)
			{
				Stop(false);
				return Input.InputAction.Stop;
			}

			if(!bInputBlocked)
			{
				return Input.InputAction.Continue;
			}

			if (i.m_InputState == Input.InputState.Invalid)
			{
				return Input.InputAction.Continue;
			}

			ConvertTo360(i);

			return Input.InputAction.Block;
		}

		private static void ConvertTo360(Input i)
		{
			BindList b = Preset.Current.m_BindList;
			bool bPastUpdated = false;

			if(i == b.AButton){
				if(i.m_InputState == Input.InputState.Down){
					m_Ctrlr.Buttons |= X360Buttons.A;
				}
				else if(i.m_InputState == Input.InputState.Up){
					m_Ctrlr.Buttons &= ~X360Buttons.A;
				}
				else if(i.m_InputState == Input.InputState.Impulse){
					m_Ctrlr.Buttons |= X360Buttons.A;
					ImpulseRelease(X360Buttons.A);
				}
			}
			if(i == b.XButton)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.X;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.X;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.X;
					ImpulseRelease(X360Buttons.X);
				}
			}
			if (i == b.YButton)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Y;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Y;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Y;
					ImpulseRelease(X360Buttons.Y);
				}
			}
			if (i == b.BButton)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.B;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.B;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.B;
					ImpulseRelease(X360Buttons.B);
				}
			}
			if (i == b.StartButton)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Start;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Start;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Start;
					ImpulseRelease(X360Buttons.Start);
				}
			}
			if (i == b.BackButton)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Back;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Back;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Back;
					ImpulseRelease(X360Buttons.Back);
				}
			}
			if (i == b.DPadUp)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Up;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Up;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Up;
					ImpulseRelease(X360Buttons.Up);
				}
			}
			if (i == b.DPadDown)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Down;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Down;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Down;
					ImpulseRelease(X360Buttons.Down);
				}
			}
			if (i == b.DPadRight)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Right;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Right;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Right;
					ImpulseRelease(X360Buttons.Right);
				}
			}
			if (i == b.DPadLeft)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.Left;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.Left;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.Left;
					ImpulseRelease(X360Buttons.Left);
				}
			}
			if (i == b.LeftBumper)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.LeftBumper;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.LeftBumper;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.LeftBumper;
					ImpulseRelease(X360Buttons.LeftBumper);
				}
			}
			if (i == b.RightBumper)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.RightBumper;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.RightBumper;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.RightBumper;
					ImpulseRelease(X360Buttons.RightBumper);
				}
			}
			if (i == b.LeftTrigger)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.LeftTrigger = 255;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.LeftTrigger = 0;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
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
			if (i == b.RightTrigger)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.RightTrigger = 255;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.RightTrigger = 0;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
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
			if (i == b.LeftJoyPress)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.LeftStick;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.LeftStick;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.LeftStick;
					ImpulseRelease(X360Buttons.LeftStick);
				}
			}
			if (i == b.RightJoyPress)
			{
				if (i.m_InputState == Input.InputState.Down)
				{
					m_Ctrlr.Buttons |= X360Buttons.RightStick;
				}
				else if (i.m_InputState == Input.InputState.Up)
				{
					m_Ctrlr.Buttons &= ~X360Buttons.RightStick;
				}
				else if (i.m_InputState == Input.InputState.Impulse)
				{
					m_Ctrlr.Buttons |= X360Buttons.RightStick;
					ImpulseRelease(X360Buttons.RightStick);
				}
			}
			if (b.RightJoyType == Input.InputType.Keyboard)
			{
				if(i == b.RightJoyUp || i == b.RightJoyDown)
				{
					short y = 0;
					if (Input.GetKeyState(b.RightJoyUp))
						y += short.MaxValue;

					if (Input.GetKeyState(b.RightJoyDown))
						y -= short.MinValue;

					m_Ctrlr.RightStickY = y;
				}

				if (i == b.RightJoyRight || i == b.RightJoyLeft)
				{
					short x = 0;
					if (Input.GetKeyState(b.RightJoyLeft))
						x -= short.MinValue;

					if (Input.GetKeyState(b.RightJoyRight))
						x += short.MaxValue;

					m_Ctrlr.RightStickX = x;
				}
			}
			else if(i.m_InputType == Input.InputType.Mouse)
			{
				UpdatePast(i);
				bPastUpdated = true;
				CalcJoy(value => m_Ctrlr.RightStickX = value, value => m_Ctrlr.RightStickY = value);
			}

			if (b.LeftJoyType == Input.InputType.Keyboard)
			{
				if (i == b.LeftJoyUp || i == b.LeftJoyDown)
				{
					short y = 0;
					if (Input.GetKeyState(b.LeftJoyUp))
						y += short.MaxValue;

					if (Input.GetKeyState(b.LeftJoyDown))
						y -= short.MinValue;

					m_Ctrlr.LeftStickY = y;
				}

				if (i == b.LeftJoyRight || i == b.LeftJoyLeft)
				{
					short x = 0;
					if (Input.GetKeyState(b.LeftJoyLeft))
						x -= short.MinValue;

					if (Input.GetKeyState(b.LeftJoyRight))
						x += short.MaxValue;

					m_Ctrlr.LeftStickX = x;
				}
			}
			else if (i.m_InputType == Input.InputType.Mouse)
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
