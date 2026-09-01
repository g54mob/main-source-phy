using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioPlaylistVolumeConnection : Connection<float>
{
	public string PlaylistName;

	public Vector2 InputRange;

	public MasterAudioPlaylistVolumeConnection(Vector2 inputRange, string playlistName)
	{
		//IL_0010: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		InputRange = inputRange;
		PlaylistName = playlistName;
	}

	public override float Get()
	{
		return 1f;
	}

	public override void Set(float volume)
	{
	}
}
