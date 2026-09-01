namespace Kamgam.SettingsGenerator
{
	public class SettingStringEvent : SettingEvent<string>
	{
		public string FloatFormat;

		public override SettingData.DataType[] GetSupportedDataTypes()
		{
			return null;
		}

		public override void TriggerEvent()
		{
		}
	}
}
