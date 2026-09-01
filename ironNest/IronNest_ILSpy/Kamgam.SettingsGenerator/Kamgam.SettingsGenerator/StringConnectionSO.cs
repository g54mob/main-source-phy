namespace Kamgam.SettingsGenerator;

public abstract class StringConnectionSO : ConnectionSO, IConnectionSO<IConnection<string>>
{
	public abstract IConnection<string> GetConnection();

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.String;
	}
}
