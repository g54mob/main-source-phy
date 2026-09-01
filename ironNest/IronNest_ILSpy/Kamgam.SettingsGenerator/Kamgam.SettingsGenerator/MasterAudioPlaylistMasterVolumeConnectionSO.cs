using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioPlaylistMasterVolumeConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	protected MasterAudioPlaylistMasterVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0035: Expected O, but got I
		//IL_004c: Expected O, but got I4
		if (_connection != null)
		{
			return _connection;
		}
		MasterAudioPlaylistMasterVolumeConnection masterAudioPlaylistMasterVolumeConnection = new MasterAudioPlaylistMasterVolumeConnection((Vector2)0);
		masterAudioPlaylistMasterVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioPlaylistMasterVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioPlaylistMasterVolumeConnectionSO)+1C]");
		_ = 0;
		_connection = masterAudioPlaylistMasterVolumeConnection;
		return _connection;
	}

	public void Create()
	{
		//IL_000c: Expected O, but got I
		//IL_0023: Expected O, but got I4
		MasterAudioPlaylistMasterVolumeConnection masterAudioPlaylistMasterVolumeConnection = new MasterAudioPlaylistMasterVolumeConnection((Vector2)0);
		masterAudioPlaylistMasterVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioPlaylistMasterVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioPlaylistMasterVolumeConnectionSO)+1C]");
		_ = 0;
		_connection = masterAudioPlaylistMasterVolumeConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public MasterAudioPlaylistMasterVolumeConnectionSO()
	{
		//IL_000b: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		((ConnectionSO)this)._002Ector();
	}
}
