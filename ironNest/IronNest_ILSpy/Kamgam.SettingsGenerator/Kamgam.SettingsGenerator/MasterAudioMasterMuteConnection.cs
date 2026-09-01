using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MasterAudioMasterMuteConnection : Connection<bool>
{
	public bool Invert;

	public MasterAudioMasterMuteConnection(bool invert)
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
