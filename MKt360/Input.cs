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
		public static bool[] DIK_KeyState = new bool[Enum.GetNames(typeof(DIK)).Length];
		public static int Mouse_KeyState = 0;
		public InputType m_InputType;
		public List<InputState> m_InputState = new List<InputState>();
		public Interception.Stroke Stroke;
		public List<bool> IsChanged = new List<bool>();
		public List<ushort> Code = new List<ushort>();

		public Input(Interception.KeyStroke s)
		{
			m_InputType = InputType.Keyboard;
			Stroke     = s;
			Code.Add(s.code);

			if (s.state == (ushort)Interception.KeyState.KeyDown)
			{
				m_InputState.Add(InputState.Down);
				IsChanged.Add(DIK_KeyState[s.code] == false);
				DIK_KeyState[s.code] = true;
			}
			else if (s.state == (ushort)Interception.KeyState.KeyUp)
			{
				m_InputState.Add(InputState.Up);
				IsChanged.Add(DIK_KeyState[s.code] == true);
				DIK_KeyState[s.code] = false;
			}
			else
			{
				m_InputState.Add(InputState.Invalid);
			}	
		}
		public Input(Interception.MouseStroke s)
		{
			m_InputType = InputType.Mouse;
			Stroke  = s;

			if ((s.state & (ushort)Interception.MouseState.LeftDown) > 0 || (s.state & (ushort)Interception.MouseState.LeftUp) > 0)
			{
				Code.Add((ushort)MouseInput.LeftMouse);
				InputState inputstate = (s.state == ((ushort)Interception.MouseState.LeftDown) ? InputState.Down : InputState.Up);
				m_InputState.Add(inputstate);
				Mouse_KeyState = (inputstate == InputState.Down) ? Mouse_KeyState | (ushort)MouseInput.LeftMouse : Mouse_KeyState & ~(ushort)MouseInput.LeftMouse;
				IsChanged.Add(true);
			}

			if ((s.state & (ushort)Interception.MouseState.RightDown) > 0 || (s.state & (ushort)Interception.MouseState.RightUp) > 0)
			{
				Code.Add((ushort)MouseInput.RightMouse);
				InputState inputstate = (s.state == ((ushort)Interception.MouseState.RightDown) ? InputState.Down : InputState.Up);
				m_InputState.Add(inputstate);
				Mouse_KeyState = (inputstate == InputState.Down) ? Mouse_KeyState | (ushort)MouseInput.RightMouse : Mouse_KeyState & ~(ushort)MouseInput.RightMouse;
				IsChanged.Add(true);
			}
			if ((s.state & (ushort)Interception.MouseState.MiddleDown) > 0 || (s.state & (ushort)Interception.MouseState.MiddleUp) > 0)
			{
				Code.Add((ushort)MouseInput.MiddleMouse);
				InputState inputstate = (s.state == ((ushort)Interception.MouseState.MiddleDown) ? InputState.Down : InputState.Up);
				m_InputState.Add(inputstate);
				Mouse_KeyState = (inputstate == InputState.Down) ? Mouse_KeyState | (ushort)MouseInput.MiddleMouse : Mouse_KeyState & ~(ushort)MouseInput.MiddleMouse;
				IsChanged.Add(true);
			}
			if ((s.state & (ushort)Interception.MouseState.Button4Down) > 0 || (s.state & (ushort)Interception.MouseState.Button4Up) > 0)
			{
				Code.Add((ushort)MouseInput.Button4);
				InputState inputstate = (s.state == ((ushort)Interception.MouseState.Button4Down) ? InputState.Down : InputState.Up);
				m_InputState.Add(inputstate);
				Mouse_KeyState = (inputstate == InputState.Down) ? Mouse_KeyState | (ushort)MouseInput.Button4 : Mouse_KeyState & ~(ushort)MouseInput.Button4;
				IsChanged.Add(true);
			}
			if ((s.state & (ushort)Interception.MouseState.Button5Down) > 0 || (s.state & (ushort)Interception.MouseState.Button5Up) > 0)
			{
				Code.Add((ushort)MouseInput.Button5);
				InputState inputstate = (s.state == ((ushort)Interception.MouseState.Button5Down) ? InputState.Down : InputState.Up);
				m_InputState.Add(inputstate);
				Mouse_KeyState = (inputstate == InputState.Down) ? Mouse_KeyState | (ushort)MouseInput.Button5 : Mouse_KeyState & ~(ushort)MouseInput.Button5;
				IsChanged.Add(true);
			}
			if ((s.state & (ushort)Interception.MouseState.MouseWheel) > 0 || (s.state & (ushort)Interception.MouseState.MouseHWheel) > 0)
			{
				if (s.rolling > 0)
				{
					Code.Add((ushort)MouseInput.WheelUp);
				}
				else
				{
					Code.Add((ushort)MouseInput.WheelDown);
				}

				m_InputState.Add(InputState.Impulse);
				IsChanged.Add(true);
			}
			
			if(s.x != 0 || s.y != 0)
			{
				Code.Add((ushort)MouseInput.Move);
				IsChanged.Add(true);
			}
		}

		public bool HasCode(ushort code)
		{
			return Code.Contains(code);
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
				return DIK_KeyState[k.Code];
			}
			else if(k.m_InputType == InputType.Mouse)
			{
				return (Mouse_KeyState & k.Code) > 0;
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
			Move = 0x80
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
	}

	public class Key
	{
		public Input.InputType m_InputType { get; set; }
		public ushort Code { get; set; }

		public Key() { }

		public Key(Input.InputType m_InputType, ushort code)
		{
			this.m_InputType = m_InputType;
			Code = code;
		}

		public Key(string k)
		{
			Input.DIK kCode;
			Input.MouseInput mCode;
			if (Enum.TryParse(k, out kCode))
			{
				m_InputType = Input.InputType.Keyboard;
				Code = (ushort)kCode;
			}
			else if (Enum.TryParse(k, out mCode))
			{
				m_InputType = Input.InputType.Mouse;
				Code = (ushort)mCode;
			}
			else
			{
				Form1.Log("Invalid key code: " + k);
			}
		}

		public override string ToString()
		{
			if (m_InputType == Input.InputType.Keyboard)
				return ((Input.DIK)Code).ToString();
			else
				return ((Input.MouseInput)Code).ToString();
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

		public static bool operator ==(Key k, Input i)
		{
			return (k.m_InputType == i.m_InputType && i.HasCode(k.Code));
		}

		public static bool operator !=(Key k, Input i)
		{
			return !(k.m_InputType == i.m_InputType && i.HasCode(k.Code));
		}

		public static bool operator ==(Input i, Key k)
		{
			return (k.m_InputType == i.m_InputType && i.HasCode(k.Code));
		}

		public static bool operator !=(Input i, Key k)
		{
			return !(k.m_InputType == i.m_InputType && i.HasCode(k.Code));
		}
	}

	public class BindList
	{
		public Key StartButton { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key BackButton { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key YButton { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key XButton { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key AButton { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Space);
		public Key BButton { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key RightJoyPress { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Input.InputType RightJoyType { get; set; } = Input.InputType.Mouse;
		public Key RightJoyMove { get; set; } = new Key(Input.InputType.Mouse, (ushort)Input.MouseInput.Move);
		public Key RightJoyUp { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.W);
		public Key RightJoyDown { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.S);
		public Key RightJoyRight { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.D);
		public Key RightJoyLeft { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.A);
		public Key LeftJoyPress { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Input.InputType LeftJoyType { get; set; } = Input.InputType.Keyboard;
		public Key LeftJoyMove { get; set; } = new Key(Input.InputType.Mouse, (ushort)Input.MouseInput.Move);
		public Key LeftJoyUp { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.W);
		public Key LeftJoyDown { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.S);
		public Key LeftJoyRight { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.D);
		public Key LeftJoyLeft { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.A);
		public Key DPadUp { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key DPadDown { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key DPadRight { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key DPadLeft { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key LeftTrigger { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key LeftBumper { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key RightTrigger { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
		public Key RightBumper { get; set; } = new Key(Input.InputType.Keyboard, (ushort)Input.DIK.Invalid);
	}
}
