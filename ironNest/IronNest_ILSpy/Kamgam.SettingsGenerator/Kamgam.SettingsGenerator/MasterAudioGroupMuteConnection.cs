using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MasterAudioGroupMuteConnection : Connection<bool>
{
	public bool Invert;

	public string GroupName;

	public MasterAudioGroupMuteConnection(string groupName, bool invert)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		GroupName = groupName;
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
