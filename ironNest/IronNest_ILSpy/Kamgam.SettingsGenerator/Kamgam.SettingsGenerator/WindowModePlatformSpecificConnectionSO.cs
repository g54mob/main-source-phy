namespace Kamgam.SettingsGenerator;

public class WindowModePlatformSpecificConnectionSO : OptionConnectionSO
{
	protected WindowModePlatformSpecificConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		WindowModePlatformSpecificConnection connection = new WindowModePlatformSpecificConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		WindowModePlatformSpecificConnection connection = new WindowModePlatformSpecificConnection();
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
