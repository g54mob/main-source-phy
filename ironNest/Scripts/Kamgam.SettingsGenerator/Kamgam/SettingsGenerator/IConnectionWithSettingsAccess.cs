namespace Kamgam.SettingsGenerator
{
	public interface IConnectionWithSettingsAccess
	{
		void SetSettings(Settings settings);

		Settings GetSettings();
	}
}
