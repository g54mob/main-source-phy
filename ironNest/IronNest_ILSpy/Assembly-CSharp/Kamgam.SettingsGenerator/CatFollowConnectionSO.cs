namespace Kamgam.SettingsGenerator;

public class CatFollowConnectionSO : BoolConnectionSO
{
	protected CatFollowConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatFollowConnection connection = new CatFollowConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		CatFollowConnection connection = new CatFollowConnection();
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
