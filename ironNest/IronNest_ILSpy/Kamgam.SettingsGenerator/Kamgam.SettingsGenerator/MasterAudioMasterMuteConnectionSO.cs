namespace Kamgam.SettingsGenerator;

public class MasterAudioMasterMuteConnectionSO : BoolConnectionSO
{
	public bool Invert;

	protected MasterAudioMasterMuteConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection != null)
		{
			return _connection;
		}
		MasterAudioMasterMuteConnection masterAudioMasterMuteConnection = new MasterAudioMasterMuteConnection(invert: false);
		masterAudioMasterMuteConnection.Invert = Invert;
		_connection = masterAudioMasterMuteConnection;
		return _connection;
	}

	public void Create()
	{
		MasterAudioMasterMuteConnection masterAudioMasterMuteConnection = new MasterAudioMasterMuteConnection(invert: false);
		masterAudioMasterMuteConnection.Invert = Invert;
		_connection = masterAudioMasterMuteConnection;
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
