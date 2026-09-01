using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class MasterAudioBusMuteConnection : Connection<bool>
{
	public bool Invert;

	public string BusName;

	public MasterAudioBusMuteConnection(string busName, bool invert)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		BusName = busName;
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
