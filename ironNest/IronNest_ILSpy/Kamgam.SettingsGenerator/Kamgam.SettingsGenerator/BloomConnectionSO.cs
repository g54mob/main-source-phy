namespace Kamgam.SettingsGenerator;

public class BloomConnectionSO : BoolConnectionSO
{
	protected BloomConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			BloomConnection connection = new BloomConnection();
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		BloomConnection connection = new BloomConnection();
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
