namespace Kamgam.SettingsGenerator;

public class ShadowResolutionConnectionSO : OptionConnectionSO
{
	protected ShadowResolutionConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		ShadowResolutionConnection connection = new ShadowResolutionConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		ShadowResolutionConnection connection = new ShadowResolutionConnection();
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
