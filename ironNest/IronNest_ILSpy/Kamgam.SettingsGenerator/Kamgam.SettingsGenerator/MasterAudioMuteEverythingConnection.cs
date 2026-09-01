using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MasterAudioMuteEverythingConnection : Connection<bool>
{
	public bool Invert;

	public MasterAudioMuteEverythingConnection(bool invert)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
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
