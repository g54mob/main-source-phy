using System;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class InvertXConnectionSO : BoolConnectionSO
{
	public string TargetTag;

	protected InvertXConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		//IL_0077: Expected O, but got I
		if (_connection == null)
		{
			string targetTag = TargetTag;
			InvertXConnection invertXConnection = new InvertXConnection((string)0);
			if (string.IsNullOrWhiteSpace(TargetTag))
			{
				targetTag = "Player";
			}
			if (invertXConnection == null)
			{
				return (IConnection<bool>)new NullReferenceException();
			}
			invertXConnection._targetTag = targetTag;
			_connection = invertXConnection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0041: Expected O, but got I
		string targetTag = TargetTag;
		InvertXConnection invertXConnection = new InvertXConnection((string)0);
		if (string.IsNullOrWhiteSpace(TargetTag))
		{
			targetTag = "Player";
		}
		invertXConnection._targetTag = targetTag;
		_connection = invertXConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			InvertXConnection connection = _connection;
			connection._cachedController = null;
		}
		_connection = null;
	}

	public InvertXConnectionSO()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A6A0]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		TargetTag = "Player";
		base._002Ector();
	}
}
