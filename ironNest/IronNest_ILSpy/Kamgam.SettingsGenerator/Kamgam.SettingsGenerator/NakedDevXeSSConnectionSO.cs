namespace Kamgam.SettingsGenerator;

public class NakedDevXeSSConnectionSO : OptionConnectionSO
{
	protected NakedDevXeSSConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			NakedDevXeSSConnection connection = new NakedDevXeSSConnection();
			Logger.LogWarning("NakedDevXeSSConnection: XeSS is not yet set up. Please consult The Naked Dev Games Manual for more info and support. https://docs.google.com/document/d/1nb1cdNNc9zzmvbDbwPERKm21g_Cp8o9V2sjVV1JsQNM");
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		NakedDevXeSSConnection connection = new NakedDevXeSSConnection();
		Logger.LogWarning("NakedDevXeSSConnection: XeSS is not yet set up. Please consult The Naked Dev Games Manual for more info and support. https://docs.google.com/document/d/1nb1cdNNc9zzmvbDbwPERKm21g_Cp8o9V2sjVV1JsQNM");
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
