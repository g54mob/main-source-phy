namespace Kamgam.SettingsGenerator;

public class GammaVolumeConnectionSO : FloatConnectionSO
{
	protected GammaVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			GammaVolumeConnection connection = new GammaVolumeConnection();
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		GammaVolumeConnection connection = new GammaVolumeConnection();
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
