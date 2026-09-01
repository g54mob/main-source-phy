namespace Kamgam.SettingsGenerator;

public interface ISettingResolver
{
	void Refresh();

	string GetID();

	void RegisterAsActivated();

	void Unregister();

	SettingData.DataType[] GetSupportedDataTypes();

	SettingsProvider GetProvider();

	SettingsProvider SetProvider(SettingsProvider provider);
}
