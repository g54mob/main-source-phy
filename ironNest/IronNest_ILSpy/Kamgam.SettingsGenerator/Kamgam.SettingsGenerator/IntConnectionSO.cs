namespace Kamgam.SettingsGenerator;

public abstract class IntConnectionSO : ConnectionSO, IConnectionSO<IConnection<int>>
{
	public abstract IConnection<int> GetConnection();

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.Int;
	}
}
