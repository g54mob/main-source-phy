using System.IO;
using System.Text;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples;

public class SettingsInitializerSaveLoadToFile : MonoBehaviour
{
	private void Awake()
	{
		Settings.CustomStorageMethod customLoadMethod = load;
		Settings.CustomLoadMethod = customLoadMethod;
		Settings.CustomStorageMethod customSaveMethod = save;
		Settings.CustomSaveMethod = customSaveMethod;
		Settings.CustomStorageMethod customDeleteMethod = delete;
		Settings.CustomDeleteMethod = customDeleteMethod;
	}

	private string getFilePath(string key)
	{
		string dataPath = Application.dataPath;
		return dataPath + "/" + key + ".json";
	}

	private void delete(string key, Settings settings)
	{
		string filePath = getFilePath(key);
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
	}

	private void save(string key, Settings settings)
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
			string message = "Saved to: " + filePath;
			Debug.Log(message);
		}
	}

	private void load(string key, Settings settings)
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
}
