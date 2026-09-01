namespace Kamgam.SettingsGenerator;

public class WindowModeConnectionSO : OptionConnectionSO
{
	protected WindowModeConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		WindowModeConnection connection = new WindowModeConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		WindowModeConnection connection = new WindowModeConnection();
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
