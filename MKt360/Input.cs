using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK2360
{
	public class Input
	{
		public enum InputType
		{
			Keyboard,
			Mouse
		};

		public InputType m_InputType;
		public Interception.Stroke Stroke;
		public object Code;

		public Input(Interception.KeyStroke s)
		{
			m_InputType = InputType.Keyboard;
			Stroke     = s;
			Code = (DIK)(s.code);
		}
		public Input(Interception.MouseStroke s)
		{
			m_InputType = InputType.Mouse;
			Stroke  = s;

			if (s.state == (ushort)Interception.MouseState.LeftDown || s.state == (ushort)Interception.MouseState.LeftUp)
			{
				Code = MouseInput.LeftMouse;
			}
			else if (s.state == (ushort)Interception.MouseState.RightDown || s.state == (ushort)Interception.MouseState.RightUp)
			{
				Code = MouseInput.RightMouse;
			}
			else if (s.state == (ushort)Interception.MouseState.MiddleDown || s.state == (ushort)Interception.MouseState.MiddleUp)
			{
				Code = MouseInput.MiddleMouse;
			}
			else if (s.state == (ushort)Interception.MouseState.Button4Down || s.state == (ushort)Interception.MouseState.Button4Up)
			{
				Code = MouseInput.Button4;
			}
			else if (s.state == (ushort)Interception.MouseState.Button5Down || s.state == (ushort)Interception.MouseState.Button5Up)
			{
				Code = MouseInput.Button5;
			}
			else if (s.state == (ushort)Interception.MouseState.MouseWheel)
			{
				if (s.rolling > 0)
				{
					Code = MouseInput.WheelUp;
				}
				else
				{
					Code = MouseInput.WheelDown;
				}
			}
			else
			{
				Code = MouseInput.Invalid;
			}
		}

		public delegate InputAction KeyStateChangeCallback(Input key);
		private static List<KeyStateChangeCallback> Listeners = new List<KeyStateChangeCallback>();
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
		public enum MouseInput
		{
			Invalid = -1,
			LeftMouse,
			RightMouse,
			MiddleMouse,
			WheelUp,
			WheelDown,
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
			return Code.ToString();
		}
	}

	public class Key
	{
		public Input.InputType m_InputType { get; set; }
		public object Code { get; set; }

		public Key(Input.MouseInput p)
		{
			m_InputType = Input.InputType.Mouse;
			Code       = p;
		}

		public Key(Input.DIK p)
		{
			m_InputType = Input.InputType.Keyboard;
			Code       = p;
		}

		public Key() { }

		public override string ToString()
		{
			return Code.ToString();
		}
	}

	public class BindList
	{
		public Key StartButton { get; set; } = new Key(Input.DIK.Invalid);
		public Key BackButton { get; set; } = new Key(Input.DIK.Invalid);
		public Key YButton { get; set; } = new Key(Input.DIK.Invalid);
		public Key XButton { get; set; } = new Key(Input.DIK.Invalid);
		public Key AButton { get; set; } = new Key(Input.DIK.Space);
		public Key BButton { get; set; } = new Key(Input.DIK.Invalid);
		public Key RightJoyPress { get; set; } = new Key(Input.DIK.Invalid);
		public Input.InputType RightJoyType { get; set; } = Input.InputType.Mouse;
		public Key RightJoyMove { get; set; } = new Key(Input.MouseInput.Move);
		public Key RightJoyUp { get; set; } = new Key(Input.DIK.W);
		public Key RightJoyDown { get; set; } = new Key(Input.DIK.S);
		public Key RightJoyRight { get; set; } = new Key(Input.DIK.D);
		public Key RightJoyLeft { get; set; } = new Key(Input.DIK.A);
		public Key LeftJoyPress { get; set; } = new Key(Input.DIK.Invalid);
		public Input.InputType LeftJoyType { get; set; } = Input.InputType.Keyboard;
		public Key LeftJoyMove { get; set; } = new Key(Input.MouseInput.Move);
		public Key LeftJoyUp { get; set; } = new Key(Input.DIK.W);
		public Key LeftJoyDown { get; set; } = new Key(Input.DIK.S);
		public Key LeftJoyRight { get; set; } = new Key(Input.DIK.D);
		public Key LeftJoyLeft { get; set; } = new Key(Input.DIK.A);
		public Key DPadUp { get; set; } = new Key(Input.DIK.Invalid);
		public Key DPadDown { get; set; } = new Key(Input.DIK.Invalid);
		public Key DPadRight { get; set; } = new Key(Input.DIK.Invalid);
		public Key DPadLeft { get; set; } = new Key(Input.DIK.Invalid);
		public Key LeftTrigger { get; set; } = new Key(Input.DIK.Invalid);
		public Key LeftBumper { get; set; } = new Key(Input.DIK.Invalid);
		public Key RightTrigger { get; set; } = new Key(Input.DIK.Invalid);
		public Key RightBumper { get; set; } = new Key(Input.DIK.Invalid);
	}
}
