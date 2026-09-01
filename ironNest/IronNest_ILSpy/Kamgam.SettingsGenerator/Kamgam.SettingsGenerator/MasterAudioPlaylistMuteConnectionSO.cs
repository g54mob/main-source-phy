namespace Kamgam.SettingsGenerator;

public class MasterAudioPlaylistMuteConnectionSO : BoolConnectionSO
{
	public string PlaylistName;

	public bool Invert;

	protected MasterAudioPlaylistMuteConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		//IL_0040: Expected O, but got I
		if (_connection == null)
		{
			bool invert = default(bool);
			MasterAudioPlaylistMuteConnection masterAudioPlaylistMuteConnection = new MasterAudioPlaylistMuteConnection((string)0, invert);
			masterAudioPlaylistMuteConnection.PlaylistName = PlaylistName;
			masterAudioPlaylistMuteConnection.Invert = Invert;
			_connection = masterAudioPlaylistMuteConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0011: Expected O, but got I
		bool invert = default(bool);
		MasterAudioPlaylistMuteConnection masterAudioPlaylistMuteConnection = new MasterAudioPlaylistMuteConnection((string)0, invert);
		masterAudioPlaylistMuteConnection.PlaylistName = PlaylistName;
		masterAudioPlaylistMuteConnection.Invert = Invert;
		_connection = masterAudioPlaylistMuteConnection;
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
