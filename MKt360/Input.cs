using System.Collections.Generic;
using System.Linq;
using System;

namespace MK2360
{
	public class Input
	{
		public delegate InputAction KeyStateChangeCallback(Input key);
		private static List<KeyStateChangeCallback> Listeners = new List<KeyStateChangeCallback>();
		private static List<KeyStateChangeCallback> ToAdd = new List<KeyStateChangeCallback>();
		private static List<KeyStateChangeCallback> ToRemove = new List<KeyStateChangeCallback>();
		public static bool[] DIK_KeyState = new bool[Enum.GetNames(typeof(Input.DIK)).Length];
		public static bool[] Mouse_KeyState = new bool[Enum.GetNames(typeof(Input.MouseInput)).Length];
		public InputType m_InputType;
		public InputState m_InputState;
		public Interception.Stroke Stroke;
		public bool IsChanged;
		public object Code;

		public Input(Interception.KeyStroke s)
		{
			m_InputType = InputType.Keyboard;
			Stroke     = s;
			Code = (DIK)(s.code);

			if (s.state == (ushort)Interception.KeyState.KeyDown)
			{
				m_InputState = InputState.Down;
				IsChanged = (DIK_KeyState[(int)Code] == false);
				DIK_KeyState[(int)Code] = true;
			}
			else if (s.state == (ushort)Interception.KeyState.KeyUp)
			{
				m_InputState = InputState.Up;
				IsChanged = (DIK_KeyState[(int)Code] == true);
				DIK_KeyState[(int)Code] = false;
			}
			else
			{
				m_InputState = InputState.Invalid;
			}	
		}
		public Input(Interception.MouseStroke s)
		{
			m_InputType = InputType.Mouse;
			Stroke  = s;

			if (s.state == (ushort)Interception.MouseState.LeftDown || s.state == (ushort)Interception.MouseState.LeftUp)
			{
				Code = MouseInput.LeftMouse;
				m_InputState = (s.state == (ushort)Interception.MouseState.LeftDown) ? InputState.Down : InputState.Up;
				Mouse_KeyState[(int)Code] = (m_InputState == InputState.Down) ? true : false;
			}
			else if (s.state == (ushort)Interception.MouseState.RightDown || s.state == (ushort)Interception.MouseState.RightUp)
			{
				Code = MouseInput.RightMouse;
				m_InputState = (s.state == (ushort)Interception.MouseState.RightDown) ? InputState.Down : InputState.Up;
				Mouse_KeyState[(int)Code] = (m_InputState == InputState.Down) ? true : false;
			}
			else if (s.state == (ushort)Interception.MouseState.MiddleDown || s.state == (ushort)Interception.MouseState.MiddleUp)
			{
				Code = MouseInput.MiddleMouse;
				m_InputState = (s.state == (ushort)Interception.MouseState.MiddleDown) ? InputState.Down : InputState.Up;
				Mouse_KeyState[(int)Code] = (m_InputState == InputState.Down) ? true : false;
			}
			else if (s.state == (ushort)Interception.MouseState.Button4Down || s.state == (ushort)Interception.MouseState.Button4Up)
			{
				Code = MouseInput.Button4;
				m_InputState = (s.state == (ushort)Interception.MouseState.Button4Down) ? InputState.Down : InputState.Up;
				Mouse_KeyState[(int)Code] = (m_InputState == InputState.Down) ? true : false;
			}
			else if (s.state == (ushort)Interception.MouseState.Button5Down || s.state == (ushort)Interception.MouseState.Button5Up)
			{
				Code = MouseInput.Button5;
				m_InputState = (s.state == (ushort)Interception.MouseState.Button5Down) ? InputState.Down : InputState.Up;
				Mouse_KeyState[(int)Code] = (m_InputState == InputState.Down) ? true : false;
			}
			else if (s.state == (ushort)Interception.MouseState.MouseWheel || s.state == (ushort)Interception.MouseState.MouseHWheel)
			{
				if (s.rolling > 0)
				{
					Code = MouseInput.WheelUp;
				}
				else
				{
					Code = MouseInput.WheelDown;
				}

				m_InputState = InputState.Impulse;
			}
			else
			{
				Code = MouseInput.Move;
			}

			IsChanged = true;
		}

		public bool HasKey(object code)
		{
			if(m_InputType == InputType.Keyboard)
			{
				return code == Code;
			}
			else
			{
				return ((MouseInput)code & (MouseInput)Code) > 0;
			}
		}

		public static void AddKeyListener(KeyStateChangeCallback func)
		{
			ToAdd.Add(func);
		}

		public static void RemoveKeyListener(KeyStateChangeCallback func)
		{
			ToRemove.Add(func);
		}

		public InputAction CallKeyListeners()
		{
			if (ToAdd.Count > 0)
			{
				Listeners.AddRange(ToAdd);
				ToAdd.Clear();
			}

			InputAction result = 0;
			List<KeyStateChangeCallback> stoplist = new List<KeyStateChangeCallback>();
			foreach(KeyStateChangeCallback k in Listeners)
			{
				InputAction funcResult = k(this);
				result |= funcResult;

				if((funcResult & InputAction.Stop) > 0)
				{
					stoplist.Add(k);
				}
			}

			if(stoplist.Count > 0)
				Listeners = Listeners.Except(stoplist).ToList();

			if(ToRemove.Count > 0)
			{
				Listeners = Listeners.Except(ToRemove).ToList();
				ToRemove.Clear();
			}
				

			return result;
		}

		public static bool GetKeyState(Key k)
		{
			if (k.m_InputType == InputType.Keyboard)
			{
				int idx = (int)Enum.Parse(typeof(DIK), k.Code.ToString());
				return DIK_KeyState[idx];
			}
			else if(k.m_InputType == InputType.Mouse)
			{
				int idx = (int)Enum.Parse(typeof(MouseInput), k.Code.ToString());
				return Mouse_KeyState[idx];
			}

			Form1.Log("Input.cs GetKeyState - Shouldn't see this msg");
			return false;
		}
		public enum InputType
		{
			Keyboard,
			Mouse
		}
		public enum InputState
		{
			Down,
			Up,
			Impulse,
			Invalid
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
			Invalid,
			LeftMouse = 0x01,
			RightMouse = 0x02,
			MiddleMouse = 0x04,
			WheelUp = 0x08,
			WheelDown = 0x10,
			Button4 = 0x20,
			Button5 = 0x40,
			Move = 0x50
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

		public Key() { }

		public Key(Input.InputType m_InputType, object code)
		{
			this.m_InputType = m_InputType;
			Code = code;
		}

		public override string ToString()
		{
			return Code.ToString();
		}

		public override bool Equals(object obj)
		{
			return obj is Key key &&
				   m_InputType == key.m_InputType &&
				   EqualityComparer<object>.Default.Equals(Code, key.Code);
		}

		public override int GetHashCode()
		{
			var hashCode = -1670320561;
			hashCode = hashCode * -1521134295 + m_InputType.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Code);
			return hashCode;
		}

		public static bool operator ==(Key a, Input b)
		{
			return (a.m_InputType == b.m_InputType && a.Code.ToString() == b.Code.ToString());
		}

		public static bool operator !=(Key a, Input b)
		{
			return !(a.m_InputType == b.m_InputType && a.Code.ToString() == b.Code.ToString());
		}

		public static bool operator ==(Input b, Key a)
		{
			return (a.m_InputType == b.m_InputType && a.Code.ToString() == b.Code.ToString());
		}

		public static bool operator !=(Input b, Key a)
		{
			return !(a.m_InputType == b.m_InputType && a.Code.ToString() == b.Code.ToString());
		}
	}

	public class BindList
	{
		public Key StartButton { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key BackButton { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key YButton { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key XButton { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key AButton { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Space);
		public Key BButton { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key RightJoyPress { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Input.InputType RightJoyType { get; set; } = Input.InputType.Mouse;
		public Key RightJoyMove { get; set; } = new Key(Input.InputType.Mouse, Input.MouseInput.Move);
		public Key RightJoyUp { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.W);
		public Key RightJoyDown { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.S);
		public Key RightJoyRight { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.D);
		public Key RightJoyLeft { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.A);
		public Key LeftJoyPress { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Input.InputType LeftJoyType { get; set; } = Input.InputType.Keyboard;
		public Key LeftJoyMove { get; set; } = new Key(Input.InputType.Mouse, Input.MouseInput.Move);
		public Key LeftJoyUp { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.W);
		public Key LeftJoyDown { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.S);
		public Key LeftJoyRight { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.D);
		public Key LeftJoyLeft { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.A);
		public Key DPadUp { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key DPadDown { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key DPadRight { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key DPadLeft { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key LeftTrigger { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key LeftBumper { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key RightTrigger { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
		public Key RightBumper { get; set; } = new Key(Input.InputType.Keyboard, Input.DIK.Invalid);
	}
}
