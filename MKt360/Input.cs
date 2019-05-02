using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK2360
{
	public class Input
	{
		public bool IsMouse = false;
		public bool IsKeyboard = false;
		public Interception.Stroke Stroke;

		public Input(Interception.KeyStroke s)
		{
			IsKeyboard = true;
			Stroke     = s;

			if(Listeners == null)
			{
				Listeners = new List<KeyStateChangeCallback>();
			}
		}
		public Input(Interception.Stroke s)
		{
			IsMouse = true;
			Stroke  = s;

			if (Listeners == null)
			{
				Listeners = new List<KeyStateChangeCallback>();
			}
		}

		public delegate InputAction KeyStateChangeCallback(Input key);
		private static List<KeyStateChangeCallback> Listeners;
		public static void AddKeyListener(KeyStateChangeCallback func)
		{
			Listeners.Add(func);
		}
		public static void RemoveKeyListener(KeyStateChangeCallback func)
		{
			foreach(KeyStateChangeCallback k in Listeners)
			{
				if(k == func)
				{
					Listeners.Remove(k);
				}
			}
		}
		public InputAction CallKeyListeners()
		{
			InputAction result = 0;
			List<KeyStateChangeCallback> remove = new List<KeyStateChangeCallback>();
			foreach(KeyStateChangeCallback k in Listeners)
			{
				InputAction funcResult = k(this);
				result |= funcResult;

				if((funcResult & InputAction.Stop) > 0)
				{
					remove.Add(k);
				}

				Listeners = Listeners.Except(remove).ToList();
			}

			return result;
		}

		public enum InputAction
		{
			Continue = 1,
			Change = (1 << 1),
			Block = (1 << 2),
			Stop = (1 << 3)
		}

		public enum MouseInput : ushort
		{
			LeftMouse,
			RightMouse,
			MiddleMouse,
			Wheel,
			Button4,
			Button5,
			Move
		}
		public enum DIK
		{
			Invalid = 0x0,
			Escape,
			D1,
			D2,
			D3,
			D4,
			D5,
			D6,
			D7,
			D8,
			D9,
			D0,
			Minus,
			Equals,
			Back,
			Tab,
			Q,
			W,
			E,
			R,
			T,
			Y,
			U,
			I,
			O,
			P,
			LBracket,
			RBracket,
			Return,
			LControl,
			A,
			S,
			D,
			F,
			G,
			H,
			J,
			K,
			L,
			Semicolon,
			Apostrophe,
			Grave,
			LShift,
			Backslash,
			Z,
			X,
			C,
			V,
			B,
			N,
			M,
			Comma,
			Period,
			Slash,
			RShift,
			Multiply,
			LMenu,
			Space,
			Capital,
			F1,
			F2,
			F3,
			F4,
			F5,
			F6,
			F7,
			F8,
			F9,
			F10,
			Numlock,
			Scroll,
			Numpad7,
			Numpad8,
			Numpad9,
			Subtract,
			Numpad4,
			Numpad5,
			Numpad6,
			Add,
			Numpad1,
			Numpad2,
			Numpad3,
			Numpad0,
			Decimal,
			F11 = 0x57,
			F12,
			F13 = 0x64,
			F14,
			F15,
			Kana = 0x70,
			Convert = 0x79,
			NoConvert = 0x7B,
			Yen = 0x7D,
			NumpadEquals = 0x8D,
			Circumflex = 0x90,
			At,
			Colon,
			Underline,
			Kanji,
			Stop,
			Ax,
			Unlabeled,
			NumpadEnter = 0x9C,
			RControl,
			NumpadComma = 0xB3,
			Divide = 0xB5,
			SysRq = 0xB7,
			RMenu,
			Pause = 0xC5,
			Home = 0xC7,
			Up,
			Prior,
			Left = 0xCB,
			Right = 0xCD,
			End = 0xCF,
			Down,
			Next,
			Insert,
			Delete,
			LWin = 0xDB,
			RWin,
			Apps,
			Power,
			Sleep
		}

		public override string ToString()
		{
			if(IsMouse)
			{
				Interception.MouseStroke s = Stroke;
				if (s.state == (ushort)Interception.MouseState.LeftDown || 
					s.state == (ushort)Interception.MouseState.LeftUp ||
					s.state == (ushort)Interception.MouseState.Button1Down ||
					s.state == (ushort)Interception.MouseState.Button1Up)
				{
					return MouseInput.LeftMouse.ToString();
				}
				else if (s.state == (ushort)Interception.MouseState.RightDown ||
					 s.state == (ushort)Interception.MouseState.RightUp ||
					 s.state == (ushort)Interception.MouseState.Button2Down ||
					 s.state == (ushort)Interception.MouseState.Button2Up)
				{
					return MouseInput.RightMouse.ToString();
				}
				else if (s.state == (ushort)Interception.MouseState.MiddleDown ||
					 s.state == (ushort)Interception.MouseState.MiddleUp ||
					 s.state == (ushort)Interception.MouseState.Button3Down ||
					 s.state == (ushort)Interception.MouseState.Button3Up)
				{
					return MouseInput.MiddleMouse.ToString();
				}
				else if (s.state == (ushort)Interception.MouseState.Button4Down ||
					 s.state == (ushort)Interception.MouseState.Button4Up)
				{
					return MouseInput.Button4.ToString();
				}
				else if (s.state == (ushort)Interception.MouseState.Button5Down ||
					s.state == (ushort)Interception.MouseState.Button5Up)
				{
					return MouseInput.Button5.ToString();
				}
				else
				{
					return "Unknown";
				}
			}
			else
			{
				Interception.KeyStroke s = Stroke;
				return ((DIK)s.code).ToString();
			}
		}
	}

	public class Key
	{
		public bool IsMouse;
		public bool IsKeyboard;
		public ushort Code;
		public string Name;

		public Key(Input.MouseInput p, string Name)
		{
			IsMouse    = true;
			IsKeyboard = false;
			Code       = (ushort)p;
		}

		public Key(Input.DIK p, string name)
		{
			IsMouse    = false;
			IsKeyboard = true;
			Code       = (ushort)p;
		}

		public override string ToString()
		{
			if(IsMouse)
			{
				return ((Input.MouseInput)Code).ToString();
			}
			else
			{
				return ((Input.DIK)Code).ToString();
			}
		}
	}
}
