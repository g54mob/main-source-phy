namespace Kamgam.SettingsGenerator
{
	public interface IConnectionWithProviderAccess
	{
		void SetProvider(SettingsProvider provider);

		SettingsProvider GetProvider();
	}
}
