using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	[CreateAssetMenu(fileName = "SettingsSaverJson", menuName = "SettingsGenerator/Saver/Json", order = 2)]
	public class SettingsSaverJson : SettingsSaverBase
	{
		public bool LogSavePath;

		public override void LoadInto(string key, Settings settings)
		{
		}

		public override void Save(string key, Settings settings)
		{
		}

		public override void Delete(string key)
		{
		}

		private string getFilePath(string key)
		{
			return null;
		}
	}
}
