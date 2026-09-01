namespace Kamgam.SettingsGenerator;

public class TheNakedDevUpscalerConnectionSO : OptionConnectionSO
{
	protected TheNakedDevUpscalerConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		TheNakedDevUpscalerConnection theNakedDevUpscalerConnection = new TheNakedDevUpscalerConnection();
		theNakedDevUpscalerConnection.CheckForCameraMarker = true;
		_connection = theNakedDevUpscalerConnection;
		return _connection;
	}

	public void Create()
	{
		TheNakedDevUpscalerConnection theNakedDevUpscalerConnection = new TheNakedDevUpscalerConnection();
		theNakedDevUpscalerConnection.CheckForCameraMarker = true;
		_connection = theNakedDevUpscalerConnection;
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
