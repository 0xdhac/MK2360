using System;
using System.IO;
using Nett;

namespace MK2360
{
	public class Preset
	{
		public string m_Name { get; set; }
		public ProcessItem m_ProcessItem { get; set; }

		public static string m_Path        = @"presets";
		public static string m_FileType    = @"cfg";
		public static string m_DefaultName = @"preset";
		private static Preset m_Current;

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
				Form1.MainForm.UpdateProcessList();
				Form1.MainForm.PresetList.SelectedItem = m_Current;
				Form1.MainForm.PresetList.Text = m_Current.m_Name;
			}
		}

		public static string GetNextAvailableName()
		{
			try
			{
				string[] files = Directory.GetFiles(m_Path + "/", "*." + m_FileType, SearchOption.TopDirectoryOnly);
				int count      = files.GetLength(0) + 1;

				return m_DefaultName + count;
			}
			catch(Exception ex)
			{
				Form1.Log(ex.Message);

				return "";
			}
		}

		public static bool Rename(Preset preset, string name)
		{
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
			return File.Exists(m_Path + "/" + preset + "." + m_FileType);
		}

		public static Preset Get(string preset)
		{
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
			Toml.WriteFile(this, m_Path + "/" + m_Name + "." + m_FileType);
			Form1.Log("Saved preset '" + m_Name + "'.");
		}

		public void Delete()
		{
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
}
