namespace Kamgam.SettingsGenerator;

public class MSAAConnectionSO : OptionConnectionSO
{
	protected MSAAConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		MSAAConnection connection = new MSAAConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		MSAAConnection connection = new MSAAConnection();
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
