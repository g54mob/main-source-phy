namespace Kamgam.SettingsGenerator
{
	public abstract class KeyCombinationConnectionSO : ConnectionSO, IConnectionSO<IConnection<KeyCombination>>
	{
		public abstract IConnection<KeyCombination> GetConnection();

		public override SettingData.DataType GetDataType()
		{
			return default(SettingData.DataType);
		}
	}
}
