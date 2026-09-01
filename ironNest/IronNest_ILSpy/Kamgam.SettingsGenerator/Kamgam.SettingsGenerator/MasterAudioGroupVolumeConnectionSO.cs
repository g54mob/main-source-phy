using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioGroupVolumeConnectionSO : FloatConnectionSO
{
	public string GroupName;

	public Vector2 InputRange;

	protected MasterAudioGroupVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0040: Expected O, but got I
		//IL_0057: Expected O, but got I4
		if (_connection == null)
		{
			string groupName = default(string);
			MasterAudioGroupVolumeConnection masterAudioGroupVolumeConnection = new MasterAudioGroupVolumeConnection((Vector2)0, groupName);
			masterAudioGroupVolumeConnection.InputRange = (Vector2)0;
			_ = 1120403456;
			masterAudioGroupVolumeConnection.InputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioGroupVolumeConnectionSO)+24]");
			_ = 0;
			masterAudioGroupVolumeConnection.GroupName = GroupName;
			_connection = masterAudioGroupVolumeConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0011: Expected O, but got I
		//IL_0028: Expected O, but got I4
		string groupName = default(string);
		MasterAudioGroupVolumeConnection masterAudioGroupVolumeConnection = new MasterAudioGroupVolumeConnection((Vector2)0, groupName);
		masterAudioGroupVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioGroupVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioGroupVolumeConnectionSO)+24]");
		_ = 0;
		masterAudioGroupVolumeConnection.GroupName = GroupName;
		_connection = masterAudioGroupVolumeConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public MasterAudioGroupVolumeConnectionSO()
	{
		//IL_000b: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		((ConnectionSO)this)._002Ector();
	}
}
