namespace Kamgam.SettingsGenerator;

public class CatHatConnectionSO : IntConnectionSO
{
	protected CatHatConnection _connection;

	public override IConnection<int> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatHatConnection connection = new CatHatConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		CatHatConnection connection = new CatHatConnection();
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
