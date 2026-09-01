using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class SettingColorEvent : SettingEvent<Color>
	{
		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void TriggerEvent()
		{
		}
	}
}
