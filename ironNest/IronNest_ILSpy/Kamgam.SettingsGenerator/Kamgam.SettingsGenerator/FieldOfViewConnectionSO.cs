namespace Kamgam.SettingsGenerator;

public class FieldOfViewConnectionSO : FloatConnectionSO
{
	public bool UseMain = true;

	public bool UseMarkers;

	protected FieldOfViewConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			FieldOfViewConnection connection = new FieldOfViewConnection(UseMain, UseMarkers);
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		FieldOfViewConnection connection = new FieldOfViewConnection(UseMain, UseMarkers);
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
