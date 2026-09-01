using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class SettingBoolEvent : SettingEvent<bool>
	{
		public static string FalseStringValue0;

		public static string FalseStringValue1;

		public static int FalseIntValue;

		public static float FalseFloatValue;

		public static Color FalseColorValue;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void TriggerEvent()
		{
		}
	}
}
