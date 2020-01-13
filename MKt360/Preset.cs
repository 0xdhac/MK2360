using System;
using System.Windows.Forms;
using System.IO;
using Nett;
using System.Collections.Generic;

namespace MK2360
{
	public class Preset
	{
		public string m_Name { get; set; }
		public ProcessItem m_ProcessItem { get; set; }
		public BindList m_BindList { get; set; } = new BindList();
		public Dictionary<
			string, 
			Dictionary<string, string>> m_ScriptSettings { get; set; } = new Dictionary<string, Dictionary<string, string>>();
		//public List<Macro> m_Macros { get; set; }

		public static string m_Path        = "presets";
		public static string m_FileType    = "cfg";
		public static string m_DefaultName = "preset";
		private static Preset m_Current;

		public Preset()
		{
			CheckDirectory();
		}

		public static Preset Current
		{
			get
			{
				return m_Current;
			}
			set
			{
				m_Current = value;

				// Save current preset as preferred preset
				if (Config.m_Config != null)
				{
					Config.m_Config.m_Preset = m_Current.m_Name;
					Config.m_Config.Save();
				}

				// Update UI information based on the current preset
				Form1.MainForm.ProcessComboBox.Enabled = true;
				Form1.MainForm.PresetNameTextbox.Enabled = true;
				Form1.MainForm.PresetList.SelectedItem = m_Current;
				Form1.MainForm.PresetList.Text = m_Current.m_Name;
				Form1.MainForm.PresetNameTextbox.Text = m_Current.m_Name;
				Form1.MainForm.UpdateProcessList();

				if(KeyPanelClass.CurrentPanel != null)
				{
					((Panel)(KeyPanelClass.CurrentPanel)).Dispose();
				}
			}
		}

		public bool AddSetting(string script, string key, string val)
		{
			// If there is no dictionary for the this script, create one
			if (!m_ScriptSettings.ContainsKey(script))
				m_ScriptSettings.Add(script, new Dictionary<string, string>());

			// If the setting doesn't exist, add it
			Dictionary<string, string> scriptDictionary;
			if (!m_ScriptSettings.TryGetValue(script, out scriptDictionary))
				return false;

			if (!scriptDictionary.ContainsKey(key))
				scriptDictionary.Add(key, val);

			return true;
		}

		public string GetSetting(string script, string key)
		{
			if (!m_ScriptSettings.ContainsKey(script))
				return null;

			Dictionary<string, string> scriptDict;
			if (!m_ScriptSettings.TryGetValue(script, out scriptDict))
				return null;

			string value;
			scriptDict.TryGetValue(key, out value);

			return value;
		}

		public void ChangeSetting(string script, string key, string value)
		{
			try
			{
				m_ScriptSettings[script][key] = value;
			}
			catch(Exception e)
			{
				Form1.Log(e.Message);
			}
		}

		public static void CheckDirectory()
		{
			if (!Directory.Exists(m_Path))
			{
				Directory.CreateDirectory(m_Path);
			}
		}

		public static string GetNextAvailableName()
		{
			CheckDirectory();

			int count = 0;
			while (File.Exists(m_Path + "/" + m_DefaultName + ++count + "." + m_FileType));

			return m_DefaultName + count;
		}

		public static bool Rename(Preset preset, string name)
		{
			CheckDirectory();

			try
			{
				File.Delete(m_Path + "/" + preset.m_Name + "." + m_FileType);
				preset.m_Name = name;

				return true;
			}
			catch(Exception ex)
			{
				Form1.Log(ex.Message);

				return false;
			}
		}

		public static int GetCount()
		{
			CheckDirectory();

			try
			{
				string[] files = Directory.GetFiles(m_Path + "/", "*." + m_FileType, SearchOption.TopDirectoryOnly);
				return files.GetLength(0);
			}
			catch(Exception ex)
			{
				Form1.Log(ex.Message);
				return 0;
			}
		}

		public static Preset GetFirst()
		{
			CheckDirectory();

			string[] files = Directory.GetFiles(m_Path + "/", "*." + m_FileType, SearchOption.TopDirectoryOnly);
			if (files.GetLength(0) > 0)
			{
				try
				{
					Preset p = Toml.ReadFile<Preset>(files[0]);
					return p;
				}
				catch(ArgumentNullException ex)
				{
					Form1.Log(ex.Message);
					return null;
				}
			}

			return null;
		}

		public static bool Exists(string preset)
		{
			CheckDirectory();

			return File.Exists(m_Path + "/" + preset + "." + m_FileType);
		}

		public static Preset Get(string preset)
		{
			CheckDirectory();

			try
			{
				Preset p = Toml.ReadFile<Preset>(m_Path + "/" + preset + "." + m_FileType);
				return p;
			}
			catch(ArgumentNullException ex)
			{
				Form1.Log(ex.Message);
				return null;
			}
		}

		public void Save()
		{
			CheckDirectory();

			Toml.WriteFile(this, m_Path + "/" + m_Name + "." + m_FileType);
			Form1.Log("Saved preset '" + m_Name + "'.");
		}

		public void Delete()
		{
			CheckDirectory();

			File.Delete(m_Path + "/" + m_Name + "." + m_FileType);

			// If this preset is the current preset, change the current preset to another preset if there is another available one
			if(this == Current)
			{
				Current = GetFirst();
			}

			Form1.MainForm.UpdatePresetList();
			Form1.MainForm.PresetList.SelectedItem = Current;
			Form1.MainForm.PresetList.Text = Current.m_Name;
		}

		public override string ToString()
		{
			return m_Name;
		}
	}

	public class Config
	{
		public static Config m_Config;
		public string m_Preset { get; set; }
		public int m_Sens { get; set; } = 2800;
		public int m_AntiAccelerationOffset { get; set; } = 2500;
		public Input.DIK m_KillSwitch { get; set; } = Input.DIK.F9;

		public void Save()
		{
			Toml.WriteFile(this, "config.cfg");
		}
		public static void Load()
		{
			m_Config = Toml.ReadFile<Config>("config.cfg");
		}
		public static bool Exists()
		{
			return File.Exists("config.cfg");
		}
	}
}
