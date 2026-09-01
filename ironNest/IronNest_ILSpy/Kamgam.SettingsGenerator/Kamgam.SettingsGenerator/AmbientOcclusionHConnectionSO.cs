namespace Kamgam.SettingsGenerator;

public class AmbientOcclusionHConnectionSO : OptionConnectionSO
{
	protected AmbientOcclusionHConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		AmbientOcclusionHConnection connection = new AmbientOcclusionHConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		AmbientOcclusionHConnection connection = new AmbientOcclusionHConnection();
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
