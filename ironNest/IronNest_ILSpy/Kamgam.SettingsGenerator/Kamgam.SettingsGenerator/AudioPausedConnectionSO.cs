namespace Kamgam.SettingsGenerator;

public class AudioPausedConnectionSO : BoolConnectionSO
{
	public bool Invert;

	protected AudioPausedConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		AudioPausedConnection audioPausedConnection = new AudioPausedConnection(invert: false);
		audioPausedConnection.Invert = Invert;
		_connection = audioPausedConnection;
		return _connection;
	}

	public void Create()
	{
		AudioPausedConnection audioPausedConnection = new AudioPausedConnection(invert: false);
		audioPausedConnection.Invert = Invert;
		_connection = audioPausedConnection;
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
