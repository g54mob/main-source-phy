namespace Kamgam.SettingsGenerator;

public abstract class BoolConnectionSO : ConnectionSO, IConnectionSO<IConnection<bool>>
{
	public abstract IConnection<bool> GetConnection();

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.Bool;
	}
}
