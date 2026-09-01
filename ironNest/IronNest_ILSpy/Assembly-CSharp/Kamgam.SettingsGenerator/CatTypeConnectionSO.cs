namespace Kamgam.SettingsGenerator;

public class CatTypeConnectionSO : BoolConnectionSO
{
	protected CatTypeConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatTypeConnection connection = new CatTypeConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		CatTypeConnection connection = new CatTypeConnection();
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
