using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "SettingsSaverPlayerPrefs", menuName = "SettingsGenerator/Saver/PlayerPrefs", order = 1)]
	public class SettingsSaverPlayerPrefs : SettingsSaverBase
	{
		public override void LoadInto(string key, Settings settings)
		{
		}

		public override void Save(string key, Settings settings)
		{
		}

		public override void Delete(string key)
		{
		}
	}
}
