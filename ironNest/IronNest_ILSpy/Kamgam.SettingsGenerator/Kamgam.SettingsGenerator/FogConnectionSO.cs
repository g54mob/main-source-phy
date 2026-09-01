namespace Kamgam.SettingsGenerator;

public class FogConnectionSO : BoolConnectionSO
{
	protected FogConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		FogConnection connection = new FogConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		FogConnection connection = new FogConnection();
		_connection = connection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}
}
