namespace Kamgam.SettingsGenerator;

public class AlteregoFSR2ConnectionSO : OptionConnectionSO
{
	protected AlteregoFSR2Connection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		AlteregoFSR2Connection connection = new AlteregoFSR2Connection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		AlteregoFSR2Connection connection = new AlteregoFSR2Connection();
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
