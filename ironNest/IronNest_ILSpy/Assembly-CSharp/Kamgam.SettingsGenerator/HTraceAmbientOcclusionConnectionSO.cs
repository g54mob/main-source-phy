namespace Kamgam.SettingsGenerator;

public class HTraceAmbientOcclusionConnectionSO : BoolConnectionSO
{
	private HTraceAmbientOcclusionConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		HTraceAmbientOcclusionConnection connection = new HTraceAmbientOcclusionConnection();
		_connection = connection;
		return _connection;
	}

	private void Create()
	{
		HTraceAmbientOcclusionConnection connection = new HTraceAmbientOcclusionConnection();
		_connection = connection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
			_connection = null;
		}
	}
}
