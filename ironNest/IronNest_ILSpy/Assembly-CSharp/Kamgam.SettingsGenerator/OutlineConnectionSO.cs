using System;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class OutlineConnectionSO : BoolConnectionSO
{
	public string TargetTag;

	public bool ResolveEverySet;

	public bool LogWarnings;

	private OutlineConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		if (_connection == null)
		{
			string targetTag = TargetTag;
			OutlineConnection outlineConnection = (OutlineConnection)new Connection<bool>();
			if (string.IsNullOrWhiteSpace(TargetTag))
			{
				targetTag = "OutlineController";
			}
			if (outlineConnection == null)
			{
				return (IConnection<bool>)new NullReferenceException();
			}
			outlineConnection._targetTag = targetTag;
			outlineConnection._resolveEverySet = ResolveEverySet;
			outlineConnection._logWarnings = LogWarnings;
			_connection = outlineConnection;
		}
		return _connection;
	}

	public void Create()
	{
		string targetTag = TargetTag;
		OutlineConnection outlineConnection = (OutlineConnection)new Connection<bool>();
		if (string.IsNullOrWhiteSpace(TargetTag))
		{
			targetTag = "OutlineController";
		}
		outlineConnection._targetTag = targetTag;
		outlineConnection._resolveEverySet = ResolveEverySet;
		outlineConnection._logWarnings = LogWarnings;
		_connection = outlineConnection;
	}

	public override void DestroyConnection()
	{
		OutlineConnection connection = _connection;
		if (_connection != null)
		{
			connection._cachedController = null;
		}
		_connection = null;
	}

	public OutlineConnectionSO()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A6B8]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		TargetTag = "OutlineController";
		ResolveEverySet = true;
		base._002Ector();
	}
}
