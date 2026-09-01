using System;

namespace Kamgam.SettingsGenerator;

public class AntiAliasingConnectionSO : OptionConnectionSO
{
	public bool LimitToMainCamera;

	public bool IncludeMSAA;

	protected AntiAliasingConnection _connection;

	public override IConnectionWithOptions<string> GetConnection()
	{
		if (_connection == null)
		{
			AntiAliasingConnection connection = new AntiAliasingConnection();
			_connection = connection;
			AntiAliasingConnection connection2 = _connection;
			if (_connection != null)
			{
				connection2.LimitToMainCamera = LimitToMainCamera;
				AntiAliasingConnection connection3 = _connection;
				if (_connection != null)
				{
					connection3.IncludeMSAA = IncludeMSAA;
					goto IL_00aa;
				}
			}
			return (IConnectionWithOptions<string>)new NullReferenceException();
		}
		goto IL_00aa;
		IL_00aa:
		return _connection;
	}

	public void Create()
	{
		AntiAliasingConnection connection = new AntiAliasingConnection();
		_connection = connection;
		AntiAliasingConnection connection2 = _connection;
		connection2.LimitToMainCamera = LimitToMainCamera;
		AntiAliasingConnection connection3 = _connection;
		connection3.IncludeMSAA = IncludeMSAA;
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
