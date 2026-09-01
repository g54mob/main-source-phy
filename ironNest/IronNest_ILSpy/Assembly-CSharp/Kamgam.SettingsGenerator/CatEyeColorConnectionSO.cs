namespace Kamgam.SettingsGenerator;

public class CatEyeColorConnectionSO : IntConnectionSO
{
	protected CatEyeColorConnection _connection;

	public override IConnection<int> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		CatEyeColorConnection connection = new CatEyeColorConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		CatEyeColorConnection connection = new CatEyeColorConnection();
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
