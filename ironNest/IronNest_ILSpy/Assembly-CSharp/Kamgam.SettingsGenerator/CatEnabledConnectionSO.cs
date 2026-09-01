namespace Kamgam.SettingsGenerator;

public class CatEnabledConnectionSO : BoolConnectionSO
{
	protected CatEnabledConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatEnabledConnection connection = new CatEnabledConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		CatEnabledConnection connection = new CatEnabledConnection();
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
