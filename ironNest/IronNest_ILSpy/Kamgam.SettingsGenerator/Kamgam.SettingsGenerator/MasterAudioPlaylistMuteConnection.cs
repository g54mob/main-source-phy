using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MasterAudioPlaylistMuteConnection : Connection<bool>
{
	public bool Invert;

	public string PlaylistName;

	public MasterAudioPlaylistMuteConnection(string playlistName, bool invert)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		PlaylistName = playlistName;
		Invert = invert;
	}

	public override bool Get()
	{
		return true;
	}

	public override void Set(bool mute)
	{
	}
}
