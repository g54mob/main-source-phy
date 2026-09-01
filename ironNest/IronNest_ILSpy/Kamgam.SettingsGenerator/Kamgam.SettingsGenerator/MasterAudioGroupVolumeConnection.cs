using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class MasterAudioGroupVolumeConnection : Connection<float>
{
	public string GroupName;

	public Vector2 InputRange;

	public MasterAudioGroupVolumeConnection(Vector2 inputRange, string groupName)
	{
		//IL_0010: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		InputRange = inputRange;
		GroupName = groupName;
	}

	public override float Get()
	{
		return 1f;
	}

	public override void Set(float volume)
	{
	}
}
