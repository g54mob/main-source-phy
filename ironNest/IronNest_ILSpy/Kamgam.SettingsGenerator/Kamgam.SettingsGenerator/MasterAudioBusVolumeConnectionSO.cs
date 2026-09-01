using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioBusVolumeConnectionSO : FloatConnectionSO
{
	public string BusName;

	public Vector2 InputRange;

	protected MasterAudioBusVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0040: Expected O, but got I
		//IL_0057: Expected O, but got I4
		if (_connection == null)
		{
			string busName = default(string);
			MasterAudioBusVolumeConnection masterAudioBusVolumeConnection = new MasterAudioBusVolumeConnection((Vector2)0, busName);
			masterAudioBusVolumeConnection.InputRange = (Vector2)0;
			_ = 1120403456;
			masterAudioBusVolumeConnection.InputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioBusVolumeConnectionSO)+24]");
			_ = 0;
			masterAudioBusVolumeConnection.BusName = BusName;
			_connection = masterAudioBusVolumeConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0011: Expected O, but got I
		//IL_0028: Expected O, but got I4
		string busName = default(string);
		MasterAudioBusVolumeConnection masterAudioBusVolumeConnection = new MasterAudioBusVolumeConnection((Vector2)0, busName);
		masterAudioBusVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioBusVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioBusVolumeConnectionSO)+24]");
		_ = 0;
		masterAudioBusVolumeConnection.BusName = BusName;
		_connection = masterAudioBusVolumeConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public MasterAudioBusVolumeConnectionSO()
	{
		//IL_000b: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		((ConnectionSO)this)._002Ector();
	}
}
