namespace Kamgam.SettingsGenerator;

public class MotionBlurConnectionSO : BoolConnectionSO
{
	protected MotionBlurConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			MotionBlurConnection connection = new MotionBlurConnection();
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		MotionBlurConnection connection = new MotionBlurConnection();
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
