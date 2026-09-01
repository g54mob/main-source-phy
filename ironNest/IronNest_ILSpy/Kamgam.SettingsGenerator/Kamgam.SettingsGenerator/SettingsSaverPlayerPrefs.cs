using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class SettingsSaverPlayerPrefs : SettingsSaverBase
{
	public override void LoadInto(string key, Settings settings)
	{
		string text = PlayerPrefs.GetString(key, null);
		if (!string.IsNullOrEmpty(text))
		{
			SettingsSerializer.FromJson(text, settings);
		}
	}

	public override void Save(string key, Settings settings)
	{
		string value = SettingsSerializer.ToJson(settings);
		if (!string.IsNullOrEmpty(value))
		{
			PlayerPrefs.SetString(key, value);
			PlayerPrefs.Save();
		}
	}

	public override void Delete(string key)
	{
		PlayerPrefs.DeleteKey(key);
		PlayerPrefs.Save();
	}
}
