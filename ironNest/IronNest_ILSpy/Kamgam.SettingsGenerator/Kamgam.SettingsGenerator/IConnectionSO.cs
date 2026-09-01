namespace Kamgam.SettingsGenerator;

public interface IConnectionSO<TConnection>
{
	TConnection GetConnection();

	void DestroyConnection();
}
