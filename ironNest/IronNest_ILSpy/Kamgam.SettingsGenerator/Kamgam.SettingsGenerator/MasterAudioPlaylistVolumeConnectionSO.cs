using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioPlaylistVolumeConnectionSO : FloatConnectionSO
{
	public string PlaylistName;

	public Vector2 InputRange;

	protected MasterAudioPlaylistVolumeConnection _connection;

	public override IConnection<float> GetConnection()
	{
		//IL_0040: Expected O, but got I
		//IL_0057: Expected O, but got I4
		if (_connection == null)
		{
			string playlistName = default(string);
			MasterAudioPlaylistVolumeConnection masterAudioPlaylistVolumeConnection = new MasterAudioPlaylistVolumeConnection((Vector2)0, playlistName);
			masterAudioPlaylistVolumeConnection.InputRange = (Vector2)0;
			_ = 1120403456;
			masterAudioPlaylistVolumeConnection.InputRange = InputRange;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioPlaylistVolumeConnectionSO)+24]");
			_ = 0;
			masterAudioPlaylistVolumeConnection.PlaylistName = PlaylistName;
			_connection = masterAudioPlaylistVolumeConnection;
			return _connection;
		}
		return _connection;
	}

	public void Create()
	{
		//IL_0011: Expected O, but got I
		//IL_0028: Expected O, but got I4
		string playlistName = default(string);
		MasterAudioPlaylistVolumeConnection masterAudioPlaylistVolumeConnection = new MasterAudioPlaylistVolumeConnection((Vector2)0, playlistName);
		masterAudioPlaylistVolumeConnection.InputRange = (Vector2)0;
		_ = 1120403456;
		masterAudioPlaylistVolumeConnection.InputRange = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.MasterAudioPlaylistVolumeConnectionSO)+24]");
		_ = 0;
		masterAudioPlaylistVolumeConnection.PlaylistName = PlaylistName;
		_connection = masterAudioPlaylistVolumeConnection;
	}

	public override void DestroyConnection()
	{
		if (_connection != null)
		{
			_connection.Destroy();
		}
		_connection = null;
	}

	public MasterAudioPlaylistVolumeConnectionSO()
	{
		//IL_000b: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		((ConnectionSO)this)._002Ector();
	}
}
