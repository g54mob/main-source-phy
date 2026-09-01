namespace Kamgam.SettingsGenerator;

public class FullScreenConnectionSO : BoolConnectionSO
{
	protected FullScreenConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		FullScreenConnection connection = new FullScreenConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		FullScreenConnection connection = new FullScreenConnection();
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
