using System;
using UnityEngine.InputSystem;

namespace Kamgam.SettingsGenerator;

public class InputBindingConnectionSO : StringConnectionSO
{
	public InputActionAsset InputActionAsset;

	public string BindingId;

	protected InputBindingConnection _connection;

	public override IConnection<string> GetConnection()
	{
		if (_connection == null)
		{
			InputBindingConnection inputBindingConnection = new InputBindingConnection();
			if (InputBindingConnection.Connections != null)
			{
				InputBindingConnection.Connections.Add(inputBindingConnection);
				_connection = inputBindingConnection;
				InputBindingConnection connection = _connection;
				if (_connection != null)
				{
					connection._inputActionAsset = InputActionAsset;
					InputBindingConnection connection2 = _connection;
					if (_connection != null)
					{
						connection2._bindingId = BindingId;
						goto IL_00b9;
					}
				}
			}
			return (IConnection<string>)new NullReferenceException();
		}
		goto IL_00b9;
		IL_00b9:
		return _connection;
	}

	public void Create()
	{
		InputBindingConnection inputBindingConnection = new InputBindingConnection();
		InputBindingConnection.Connections.Add(inputBindingConnection);
		_connection = inputBindingConnection;
		InputBindingConnection connection = _connection;
		connection._inputActionAsset = InputActionAsset;
		InputBindingConnection connection2 = _connection;
		connection2._bindingId = BindingId;
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
