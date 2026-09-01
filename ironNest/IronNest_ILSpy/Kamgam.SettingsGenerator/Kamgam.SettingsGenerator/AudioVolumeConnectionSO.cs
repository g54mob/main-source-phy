using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AudioVolumeConnectionSO : FloatConnectionSO
{
	public Vector2 InputRange;

	protected AudioVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0035: Expected O, but got I
		//IL_004c: Expected O, but got I4
		if (_connection != null)
		{
			return _connection;
		}
		AudioVolumeConnection audioVolumeConnection = new AudioVolumeConnection((Vector2)0);
		audioVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		audioVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.AudioVolumeConnectionSO)+1C]");
		_ = 0;
		_connection = audioVolumeConnection;
		return _connection;
	}

	public void Create()
	{
		//IL_000c: Expected O, but got I
		//IL_0023: Expected O, but got I4
		AudioVolumeConnection audioVolumeConnection = new AudioVolumeConnection((Vector2)0);
		audioVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		audioVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.AudioVolumeConnectionSO)+1C]");
		_ = 0;
		_connection = audioVolumeConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public AudioVolumeConnectionSO()
	{
		//IL_000b: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		base._002Ector();
	}
}
