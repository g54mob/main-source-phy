using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class BeautifyLensDirtIntensityConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	public Vector2 OutputRange;

	public bool ResolveEveryAccess;

	public bool LogWarnings;

	private BeautifyLensDirtIntensityConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			Vector2 vector = default(Vector2);
			bool logWarnings = default(bool);
			BeautifyLensDirtIntensityConnection connection = new BeautifyLensDirtIntensityConnection(vector, vector, ResolveEveryAccess, logWarnings);
			_connection = connection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		Vector2 vector = default(Vector2);
		bool logWarnings = default(bool);
		BeautifyLensDirtIntensityConnection connection = new BeautifyLensDirtIntensityConnection(vector, vector, ResolveEveryAccess, logWarnings);
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

	public BeautifyLensDirtIntensityConnectionSO()
	{
		//IL_0011: Expected O, but got I4
		_ = 1120403456;
		InputRange = (Vector2)0;
		_ = 1065353216;
		LogWarnings = true;
		base._002Ector();
	}
}
