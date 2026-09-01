namespace Kamgam.SettingsGenerator;

public class CatBodyColorConnectionSO : IntConnectionSO
{
	protected CatBodyColorConnection _connection;

	public override IConnection<int> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatBodyColorConnection connection = new CatBodyColorConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		CatBodyColorConnection connection = new CatBodyColorConnection();
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
