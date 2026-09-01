using System;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MouseClampingConnectionSO : BoolConnectionSO
{
	public string TargetTag;

	public bool ResolveEverySet;

	public bool LogWarnings;

	private MouseClampingConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			string targetTag = TargetTag;
			MouseClampingConnection mouseClampingConnection = (MouseClampingConnection)new Connection<bool>();
			if (string.IsNullOrWhiteSpace(TargetTag))
			{
				targetTag = "CursorManager";
			}
			if (mouseClampingConnection == null)
			{
				return (IConnection<bool>)new NullReferenceException();
			}
			mouseClampingConnection._targetTag = targetTag;
			mouseClampingConnection._resolveEverySet = ResolveEverySet;
			mouseClampingConnection._logWarnings = LogWarnings;
			_connection = mouseClampingConnection;
		}
		return _connection;
	}

	public void Create()
	{
		string targetTag = TargetTag;
		MouseClampingConnection mouseClampingConnection = (MouseClampingConnection)new Connection<bool>();
		if (string.IsNullOrWhiteSpace(TargetTag))
		{
			targetTag = "CursorManager";
		}
		mouseClampingConnection._targetTag = targetTag;
		mouseClampingConnection._resolveEverySet = ResolveEverySet;
		mouseClampingConnection._logWarnings = LogWarnings;
		_connection = mouseClampingConnection;
	}

	public override void DestroyConnection()
	{
		MouseClampingConnection connection = _connection;
		if (_connection != null)
		{
			connection._cachedController = null;
		}
		_connection = null;
	}

	public MouseClampingConnectionSO()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A6AC]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		TargetTag = "CursorManager";
		ResolveEverySet = true;
		base._002Ector();
	}
}
