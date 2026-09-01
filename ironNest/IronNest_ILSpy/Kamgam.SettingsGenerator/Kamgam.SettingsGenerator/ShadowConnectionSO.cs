namespace Kamgam.SettingsGenerator;

public class ShadowConnectionSO : BoolConnectionSO
{
	protected ShadowConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		ShadowConnection connection = new ShadowConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		ShadowConnection connection = new ShadowConnection();
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
