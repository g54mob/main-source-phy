namespace Kamgam.SettingsGenerator;

public class MasterAudioPlaylistMasterMuteConnectionSO : BoolConnectionSO
{
	public bool Invert;

	protected MasterAudioPlaylistMasterMuteConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		MasterAudioPlaylistMasterMuteConnection masterAudioPlaylistMasterMuteConnection = new MasterAudioPlaylistMasterMuteConnection(invert: false);
		masterAudioPlaylistMasterMuteConnection.Invert = Invert;
		_connection = masterAudioPlaylistMasterMuteConnection;
		return _connection;
	}

	public void Create()
	{
		MasterAudioPlaylistMasterMuteConnection masterAudioPlaylistMasterMuteConnection = new MasterAudioPlaylistMasterMuteConnection(invert: false);
		masterAudioPlaylistMasterMuteConnection.Invert = Invert;
		_connection = masterAudioPlaylistMasterMuteConnection;
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
