namespace Kamgam.SettingsGenerator;

public class MasterAudioMuteEverythingConnectionSO : BoolConnectionSO
{
	public bool Invert;

	protected MasterAudioMuteEverythingConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		MasterAudioMuteEverythingConnection masterAudioMuteEverythingConnection = new MasterAudioMuteEverythingConnection(invert: false);
		masterAudioMuteEverythingConnection.Invert = Invert;
		_connection = masterAudioMuteEverythingConnection;
		return _connection;
	}

	public void Create()
	{
		MasterAudioMuteEverythingConnection masterAudioMuteEverythingConnection = new MasterAudioMuteEverythingConnection(invert: false);
		masterAudioMuteEverythingConnection.Invert = Invert;
		_connection = masterAudioMuteEverythingConnection;
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
