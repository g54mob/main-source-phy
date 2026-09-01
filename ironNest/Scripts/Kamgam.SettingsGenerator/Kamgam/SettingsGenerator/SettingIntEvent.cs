using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public class SettingIntEvent : SettingEvent<int>
	{
		public enum FloatToIntConversion
		{
			Round = 0,
			Ceil = 1,
			Floor = 2
		}

		[Tooltip("If the input value is a float then this defines how it will be converted to an int.")]
		public FloatToIntConversion FloatToInt;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void TriggerEvent()
		{
		}
	}
}
