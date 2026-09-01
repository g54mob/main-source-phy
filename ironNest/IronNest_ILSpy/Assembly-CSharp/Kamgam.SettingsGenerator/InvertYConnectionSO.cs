using System;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class InvertYConnectionSO : BoolConnectionSO
{
	public string TargetTag;

	protected InvertYConnection _connection;

	public override IConnection<bool> GetConnection()
	{
		//IL_0077: Expected O, but got I
		if (_connection == null)
		{
			string targetTag = TargetTag;
			InvertYConnection invertYConnection = new InvertYConnection((string)0);
			if (string.IsNullOrWhiteSpace(TargetTag))
			{
				targetTag = "Player";
			}
			if (invertYConnection == null)
			{
				return (IConnection<bool>)new NullReferenceException();
			}
			invertYConnection._targetTag = targetTag;
			_connection = invertYConnection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0041: Expected O, but got I
		string targetTag = TargetTag;
		InvertYConnection invertYConnection = new InvertYConnection((string)0);
		if (string.IsNullOrWhiteSpace(TargetTag))
		{
			targetTag = "Player";
		}
		invertYConnection._targetTag = targetTag;
		_connection = invertYConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			InvertYConnection connection = _connection;
			connection._cachedController = null;
		}
		_connection = null;
	}

	public InvertYConnectionSO()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A6A6]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		TargetTag = "Player";
		base._002Ector();
	}
}
