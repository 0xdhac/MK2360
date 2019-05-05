using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ScpDriverInterface;

namespace MK2360
{
	public class XMode
	{
		private static bool m_Active = false;
		public static bool Start()
		{
			if(m_Active == true)
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
			m_Active = true;
			return true;
		}

		public static void Stop(bool remove)
		{
			Form1.MainForm.ShowPresetControls();
			if(remove)
				Input.RemoveKeyListener(OnKeyPress);
			m_Active = false;

			if (Form1.MainForm.InvokeRequired)
				Form1.MainForm.Invoke(new Action(() => Form1.MainForm.ControllerModeButton.Text = "Start"));
			else
				Form1.MainForm.ControllerModeButton.Text = "Start";			
		}

		public static bool IsActive()
		{
			return m_Active;
		}

		private static Input.InputAction OnKeyPress(Input i)
		{
			if (m_Active == false)
			{
				return Input.InputAction.Continue;
			}

			if(i.m_InputType == Input.InputType.Keyboard && Config.m_Config.KillSwitch == (Input.DIK)i.Code)
			{
				Stop(false);
				return Input.InputAction.Stop;
			}

			var active = GetActiveWindowTitle();

			if(active == null)
			{
				return Input.InputAction.Continue;
			}

			if (active.Trim() != Preset.Current.m_ProcessItem.m_Display)
			{
				return Input.InputAction.Continue;
			}

			return Input.InputAction.Block;
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
}
