using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AudioPausedConnection : Connection<bool>
{
	public bool Invert;

	public AudioPausedConnection(bool invert = true)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		Invert = invert;
	}

	public override bool Get()
	{
		if (Invert)
		{
			bool pause = AudioListener.pause;
			return (byte)((pause ? 1u : 0u) ^ 1u) != 0;
		}
		return AudioListener.pause;
	}

	public override void Set(bool pause)
	{
		bool flag = (byte)((pause ? 1u : 0u) ^ 1u) != 0;
		bool flag2 = Invert;
		bool pause2 = flag;
		if (!flag2)
		{
			pause2 = pause;
		}
		AudioListener.pause = pause2;
	}
}
