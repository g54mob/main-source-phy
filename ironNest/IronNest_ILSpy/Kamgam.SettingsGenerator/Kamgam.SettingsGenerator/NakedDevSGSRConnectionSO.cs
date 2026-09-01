namespace Kamgam.SettingsGenerator;

public class NakedDevSGSRConnectionSO : OptionConnectionSO
{
	protected NakedDevSGSRConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			NakedDevSGSRConnection connection = new NakedDevSGSRConnection();
			Logger.LogWarning("NakedDevSGSRConnection: SGSR is not yet set up. Please consult The Naked Dev Games Manual for more info and support. https://docs.google.com/document/d/1s8tQYdpSMZRLf1gndRSekam-t9FYGE_e9QLgVJAbeH8");
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		NakedDevSGSRConnection connection = new NakedDevSGSRConnection();
		Logger.LogWarning("NakedDevSGSRConnection: SGSR is not yet set up. Please consult The Naked Dev Games Manual for more info and support. https://docs.google.com/document/d/1s8tQYdpSMZRLf1gndRSekam-t9FYGE_e9QLgVJAbeH8");
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
