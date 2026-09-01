using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioMasterVolumeConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	protected MasterAudioMasterVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0035: Expected O, but got I
		//IL_004c: Expected O, but got I4
		if (_connection != null)
		{
			return _connection;
		}
		MasterAudioMasterVolumeConnection masterAudioMasterVolumeConnection = new MasterAudioMasterVolumeConnection((Vector2)0);
		masterAudioMasterVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioMasterVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioMasterVolumeConnectionSO)+1C]");
		_ = 0;
		_connection = masterAudioMasterVolumeConnection;
		return _connection;
	}

	public void Create()
	{
		//IL_000c: Expected O, but got I
		//IL_0023: Expected O, but got I4
		MasterAudioMasterVolumeConnection masterAudioMasterVolumeConnection = new MasterAudioMasterVolumeConnection((Vector2)0);
		masterAudioMasterVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioMasterVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioMasterVolumeConnectionSO)+1C]");
		_ = 0;
		_connection = masterAudioMasterVolumeConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public MasterAudioMasterVolumeConnectionSO()
	{
		//IL_000b: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		((ConnectionSO)this)._002Ector();
	}
}
