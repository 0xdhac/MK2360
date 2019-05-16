using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK2360
{
	public class Macro
	{
		private bool IsRunning;
		public bool Enabled { get; set; }
		public Dictionary<string, Key> Binds { get; set; }
		public delegate void MacroFunc();
		public string Func { get; set; }

		public Macro()
		{
			Enabled = true;
			Binds = new Dictionary<string, Key>();
		}

		public static void CreateMacros(Preset p)
		{
			//Macro m = new Macro();
			//m.Binds.Add("EditKey", new Key(Input.InputType.Keyboard, Input.DIK.G));
			//m.Func = "EasyEdit";
			//p.m_Macros.Add(m);
		}

		public void Start()
		{
			IsRunning = true;
			Type t = GetType();
			MethodInfo m = t.GetMethod(Func);
			//m.Invoke(this, null); // MAKE SURE THIS IS RAN ON ANOTHER THREAD
		}

		public void Stop()
		{
			IsRunning = false;
		}

		private void EasyEdit()
		{

		}
	}
}
