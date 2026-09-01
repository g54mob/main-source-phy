using System;

namespace Kamgam.SettingsGenerator;

public class AmbientLightConnectionSO : FloatConnectionSO
{
	public float MinColorIntensity = 0.01f;

	public float MaxColorIntensity = 2f;

	protected AmbientLightConnection _connection;

	public override IConnection<float> GetConnection()
	{
		if (_connection == null)
		{
			AmbientLightConnection ambientLightConnection = new AmbientLightConnection();
			ambientLightConnection.MinColorIntensity = 0.01f;
			ambientLightConnection.MaxColorIntensity = 2f;
			_connection = ambientLightConnection;
			AmbientLightConnection connection = _connection;
			if (_connection != null)
			{
				connection.MinColorIntensity = MinColorIntensity;
				AmbientLightConnection connection2 = _connection;
				if (_connection != null)
				{
					connection2.MaxColorIntensity = MaxColorIntensity;
					goto IL_0081;
				}
			}
			return (IConnection<float>)new NullReferenceException();
		}
		goto IL_0081;
		IL_0081:
		return _connection;
	}

	public void Create()
	{
		AmbientLightConnection ambientLightConnection = new AmbientLightConnection();
		ambientLightConnection.MinColorIntensity = 0.01f;
		ambientLightConnection.MaxColorIntensity = 2f;
		_connection = ambientLightConnection;
		AmbientLightConnection connection = _connection;
		connection.MinColorIntensity = MinColorIntensity;
		AmbientLightConnection connection2 = _connection;
		connection2.MaxColorIntensity = MaxColorIntensity;
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
