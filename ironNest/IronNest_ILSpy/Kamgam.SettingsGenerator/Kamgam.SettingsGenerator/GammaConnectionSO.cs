namespace Kamgam.SettingsGenerator;

public class GammaConnectionSO : FloatConnectionSO
{
	protected GammaConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			GammaConnection connection = new GammaConnection();
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		GammaConnection connection = new GammaConnection();
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
