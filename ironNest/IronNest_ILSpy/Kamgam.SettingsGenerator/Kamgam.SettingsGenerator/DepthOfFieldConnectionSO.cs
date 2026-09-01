namespace Kamgam.SettingsGenerator;

public class DepthOfFieldConnectionSO : BoolConnectionSO
{
	protected DepthOfFieldConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			DepthOfFieldConnection connection = new DepthOfFieldConnection();
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		DepthOfFieldConnection connection = new DepthOfFieldConnection();
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
