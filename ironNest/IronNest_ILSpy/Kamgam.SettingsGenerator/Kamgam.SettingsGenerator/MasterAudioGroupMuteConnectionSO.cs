namespace Kamgam.SettingsGenerator;

public class MasterAudioGroupMuteConnectionSO : BoolConnectionSO
{
	public string GroupName;

	public bool Invert;

	protected MasterAudioGroupMuteConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		//IL_0040: Expected O, but got I
		if (_connection == null)
		{
			bool invert = default(bool);
			MasterAudioGroupMuteConnection masterAudioGroupMuteConnection = new MasterAudioGroupMuteConnection((string)0, invert);
			masterAudioGroupMuteConnection.GroupName = GroupName;
			masterAudioGroupMuteConnection.Invert = Invert;
			_connection = masterAudioGroupMuteConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0011: Expected O, but got I
		bool invert = default(bool);
		MasterAudioGroupMuteConnection masterAudioGroupMuteConnection = new MasterAudioGroupMuteConnection((string)0, invert);
		masterAudioGroupMuteConnection.GroupName = GroupName;
		masterAudioGroupMuteConnection.Invert = Invert;
		_connection = masterAudioGroupMuteConnection;
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
