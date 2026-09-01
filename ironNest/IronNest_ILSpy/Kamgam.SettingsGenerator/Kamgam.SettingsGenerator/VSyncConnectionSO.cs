namespace Kamgam.SettingsGenerator;

public class VSyncConnectionSO : BoolConnectionSO
{
	protected VSyncConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		VSyncConnection connection = new VSyncConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		VSyncConnection connection = new VSyncConnection();
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
