using System.IO;
using System.Text;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class SettingsSaverJson : SettingsSaverBase
{
	public bool LogSavePath;

	public override void LoadInto(string key, Settings settings)
	{
		string filePath = getFilePath(key);
		if (File.Exists(filePath))
		{
			Encoding uTF = Encoding.UTF8;
			string text = File.ReadAllText(filePath, uTF);
			if (!string.IsNullOrEmpty(text))
			{
				SettingsSerializer.FromJson(text, settings);
			}
		}
	}

	public override void Save(string key, Settings settings)
	{
		string text = SettingsSerializer.ToJson(settings);
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		string filePath = getFilePath(key);
		string path = filePath + ".tmp";
		Encoding uTF = Encoding.UTF8;
		File.WriteAllText(path, text, uTF);
		string path2 = filePath + ".tmp";
		if (File.Exists(path2))
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			string sourceFileName = filePath + ".tmp";
			File.Move(sourceFileName, filePath);
			if (LogSavePath)
			{
				string message = "Saved to: " + filePath;
				Debug.Log(message);
			}
		}
	}

	public override void Delete(string key)
	{
		string filePath = getFilePath(key);
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
	}

	private string getFilePath(string key)
	{
		string dataPath = Application.dataPath;
		return dataPath + "/" + key + ".json";
	}
}
