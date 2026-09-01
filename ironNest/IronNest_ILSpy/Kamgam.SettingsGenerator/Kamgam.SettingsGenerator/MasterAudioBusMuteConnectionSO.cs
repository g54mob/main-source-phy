namespace Kamgam.SettingsGenerator;

public class MasterAudioBusMuteConnectionSO : BoolConnectionSO
{
	public string BusName;

	public bool Invert;

	protected MasterAudioBusMuteConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		//IL_0040: Expected O, but got I
		if (_connection == null)
		{
			bool invert = default(bool);
			MasterAudioBusMuteConnection masterAudioBusMuteConnection = new MasterAudioBusMuteConnection((string)0, invert);
			masterAudioBusMuteConnection.BusName = BusName;
			masterAudioBusMuteConnection.Invert = Invert;
			_connection = masterAudioBusMuteConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0011: Expected O, but got I
		bool invert = default(bool);
		MasterAudioBusMuteConnection masterAudioBusMuteConnection = new MasterAudioBusMuteConnection((string)0, invert);
		masterAudioBusMuteConnection.BusName = BusName;
		masterAudioBusMuteConnection.Invert = Invert;
		_connection = masterAudioBusMuteConnection;
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
