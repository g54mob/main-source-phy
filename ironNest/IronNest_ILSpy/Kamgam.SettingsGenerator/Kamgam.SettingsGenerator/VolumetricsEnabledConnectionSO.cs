namespace Kamgam.SettingsGenerator;

public class VolumetricsEnabledConnectionSO : BoolConnectionSO
{
	protected VolumetricsEnabledConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		VolumetricsEnabledConnection connection = new VolumetricsEnabledConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		VolumetricsEnabledConnection connection = new VolumetricsEnabledConnection();
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
