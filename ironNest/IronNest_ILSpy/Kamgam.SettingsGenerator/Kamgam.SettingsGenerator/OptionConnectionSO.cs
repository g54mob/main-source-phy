namespace Kamgam.SettingsGenerator;

public abstract class OptionConnectionSO : ConnectionSO, IConnectionSO<IConnectionWithOptions<string>>
{
	public abstract IConnectionWithOptions<string> GetConnection();

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.String;
	}
}
