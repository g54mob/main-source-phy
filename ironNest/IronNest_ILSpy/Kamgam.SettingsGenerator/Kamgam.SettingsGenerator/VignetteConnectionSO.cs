namespace Kamgam.SettingsGenerator;

public class VignetteConnectionSO : BoolConnectionSO
{
	protected VignetteConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			VignetteConnection connection = new VignetteConnection();
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		VignetteConnection connection = new VignetteConnection();
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
