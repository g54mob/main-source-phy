namespace Kamgam.SettingsGenerator
{
	public abstract class FloatConnectionSO : ConnectionSO, IConnectionSO<IConnection<float>>
	{
		public abstract IConnection<float> GetConnection();

		public override SettingData.DataType GetDataType()
		{
			return default(SettingData.DataType);
		}
	}
}
