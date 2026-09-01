namespace Kamgam.SettingsGenerator;

public class TextureResolutionConnectionSO : OptionConnectionSO
{
	protected TextureResolutionConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		TextureResolutionConnection connection = new TextureResolutionConnection();
		_connection = connection;
		return _connection;
	}

	public void Create()
	{
		TextureResolutionConnection connection = new TextureResolutionConnection();
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
