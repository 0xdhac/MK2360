using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Nett;
using System.Collections.Generic;
using NLua;
using ScpDriverInterface;

namespace MK2360
{
	public class Script
	{
		public static string ScriptFolder = "scripts";
		public static string ScriptExtension = "lua";

		public static List<Script> ScriptList = new List<Script>();

		public string ScriptName;
		private Lua m_Lua = new Lua();
		//private List<Key> m_Keys = new List<Key>();
		private List<KeyFunction> m_Keys = new List<KeyFunction>();
		public Dictionary<object, object> ScriptInfo = new Dictionary<object, object>();
		private Dictionary<string, string> Settings = new Dictionary<string, string>();
		public Script(string script)
		{
			ScriptName = script;

			try
			{
				m_Lua.RegisterFunction("Log", this, typeof(Script).GetMethod("Log"));
				m_Lua.RegisterFunction("Wait", this, typeof(Script).GetMethod("Wait"));
				m_Lua.RegisterFunction("GetKeyState", this, typeof(Script).GetMethod("GetState"));
				m_Lua.RegisterFunction("SetButton", typeof(Script).GetMethod("SetButton"));
				m_Lua.RegisterFunction("RegisterKeyDown", this, typeof(Script).GetMethod("RegisterKeyDown"));
				m_Lua.RegisterFunction("RegisterKeyUp", this, typeof(Script).GetMethod("RegisterKeyUp"));
				m_Lua.RegisterFunction("CreateSetting", this, typeof(Script).GetMethod("CreateSetting"));
			}
			catch(Exception e)
			{
				Log(e.Message);
			}


			try
			{
				m_Lua.DoFile(ScriptFolder + "/" + script + "." + ScriptExtension);
			}
			catch (Exception e)
			{
				Form1.Log(e.Message);
			}

			Input.AddKeyListener(KeyPressed);

			try
			{
				LuaTable lt = m_Lua.GetTable("MacroInfo");
				if (lt != null)
				{
					ScriptInfo = m_Lua.GetTableDict(lt);
				}
			}
			catch(Exception e)
			{
				Form1.Log(e.Message);
			}

			ScriptList.Add(this);
		}

		public void CreateSetting(string keyName, string defaultVal, string formType)
		{
			Preset.Current.AddSetting(ScriptName, keyName, defaultVal);
			Settings.Add(keyName, formType);
		}

		public void GetSetting(string key)
		{

		}

		public void RegisterKeyDown(string key, string funcname)
		{
			var func = m_Lua[funcname] as LuaFunction;
			if (func == null)
			{
				Log("Invalid function name: " + funcname);
			}

			Input.DIK kCode;
			Input.MouseInput mCode;
			if (Enum.TryParse(key, out kCode))
			{
				m_Keys.Add(new KeyFunction(new Key(Input.InputType.Keyboard, kCode), func, Input.InputState.Down));
			}
			else if (Enum.TryParse(key, out mCode))
			{
				m_Keys.Add(new KeyFunction(new Key(Input.InputType.Mouse, mCode), func, Input.InputState.Down));
			}
			else
			{
				Log("Invalid key code: " + key);
			}
		}

		public void RegisterKeyUp(string key, string funcname)
		{
			var func = m_Lua[funcname] as LuaFunction;
			if (func == null)
			{
				Log("Invalid function name: " + funcname);
			}

			Input.DIK kCode;
			Input.MouseInput mCode;
			if (Enum.TryParse(key, out kCode))
			{
				m_Keys.Add(new KeyFunction(new Key(Input.InputType.Keyboard, kCode), func, Input.InputState.Up));
			}
			else if (Enum.TryParse(key, out mCode))
			{
				m_Keys.Add(new KeyFunction(new Key(Input.InputType.Mouse, mCode), func, Input.InputState.Up));
			}
			else
			{
				Log("Invalid key code: " + key);
			}
		}

		public void Wait(int ms)
		{
			Thread.Sleep(ms);
		}

		public void Log(string text)
		{
			Form1.Log(ScriptName + "." + ScriptExtension + ": " + text);
		}

		public bool GetState(string key)
		{
			try
			{
				int idx = (int)Enum.Parse(typeof(Input.DIK), key);
				return Input.DIK_KeyState[idx];
			}
			catch (Exception) { }

			try
			{
				int idx = (int)Enum.Parse(typeof(Input.MouseInput), key);
				return Input.Mouse_KeyState[idx];
			}
			catch(Exception e)
			{
				Form1.Log(e.Message);
			}

			return false;
		}

		public static void SetButton(string a, int value)
		{
			switch(a)
			{
				case "A":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.A;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.A;
						break;
					}
				case "B":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.B;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.B;
						break;
					}
				case "X":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.X;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.X;
						break;
					}
				case "Y":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Y;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Y;
						break;
					}
				case "UP":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Up;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Up;
						break;
					}
				case "DOWN":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Down;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Down;
						break;
					}
				case "LEFT":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Left;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Left;
						break;
					}
				case "RIGHT":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Right;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Right;
						break;
					}
				case "START":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Start;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Start;
						break;
					}
				case "BACK":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.Back;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.Back;
						break;
					}
				case "LT":
					{
						if (value > 0)
							XMode.m_Ctrlr.LeftTrigger = (byte)value;
						else
							XMode.m_Ctrlr.LeftTrigger = 0;
						break;
					}
				case "LB":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.LeftBumper;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.LeftBumper;
						break;
					}
				case "RT":
					{
						if (value > 0)
							XMode.m_Ctrlr.RightTrigger = (byte)value;
						else
							XMode.m_Ctrlr.RightTrigger = 0;
						break;
					}
				case "RB":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.RightBumper;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.RightBumper;
						break;
					}
				case "LJOY":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.LeftStick;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.LeftStick;
						break;
					}
				case "RJOY":
					{
						if (value > 0)
							XMode.m_Ctrlr.Buttons |= X360Buttons.RightStick;
						else
							XMode.m_Ctrlr.Buttons &= ~X360Buttons.RightStick;
						break;
					}

				default:
					{
						return;
					}
			}

			XMode.m_Bus.Report(2, XMode.m_Ctrlr.GetReport());
		}

		private Input.InputAction KeyPressed(Input i)
		{
			if (!XMode.IsActive())
				return Input.InputAction.Continue;

			foreach(KeyFunction kf in m_Keys)
			{
				if(kf.m_Key == i && i.m_InputState == kf.m_State)
				{
					if(kf.m_Func != null)
					{
						Task.Run(() =>
						{
							kf.m_Func.Call(i.Code.ToString());
						});
					}

					return Input.InputAction.Block;
				}
			}

			return Input.InputAction.Continue;
		}

		public override string ToString()
		{			
			if(ScriptInfo.ContainsKey("name"))
			{
				return ScriptInfo["name"] + " (" + ScriptName + "." + ScriptExtension + ")";
			}

			return ScriptName + "." + ScriptExtension;
		}
	}

	public class LuaSetting
	{
		public string Key { get; set; }
		public string Value { get; set; }
		public string FormType;
		
		public LuaSetting(string key, string value, string formType)
		{
			Key = key;
			FormType = formType;
			Value = value;
		}

		//void UpdatePreset()
	}

	public class KeyFunction
	{
		public Key m_Key;
		public LuaFunction m_Func;
		public Input.InputState m_State;

		public KeyFunction(Key k, LuaFunction f, Input.InputState s)
		{
			m_Key = k;
			m_Func = f;
			m_State = s;
		}
	}
}
