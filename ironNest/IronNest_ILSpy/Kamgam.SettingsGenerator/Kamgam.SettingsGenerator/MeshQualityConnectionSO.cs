namespace Kamgam.SettingsGenerator;

public class MeshQualityConnectionSO : OptionConnectionSO
{
	protected MeshQualityConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		MeshQualityConnection connection = new MeshQualityConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		MeshQualityConnection connection = new MeshQualityConnection();
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
