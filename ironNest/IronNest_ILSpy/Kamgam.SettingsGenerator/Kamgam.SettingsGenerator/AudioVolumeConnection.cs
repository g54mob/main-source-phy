using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AudioVolumeConnection : Connection<float>
{
	public Vector2 InputRange;

	public AudioVolumeConnection(Vector2 inputRange)
	{
		//IL_0010: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		InputRange = inputRange;
	}

	public override float Get()
	{
		float volume = AudioListener.volume;
		float outMin = default(float);
		float outAnchor = default(float);
		float outMax = default(float);
		bool clamp = default(bool);
		return MathUtils.MapWithAnchor(volume, 0f, 0f, 1f, outMin, outAnchor, outMax, clamp);
	}

	public override void Set(float volume)
	{
		//IL_0031: Expected F4, but got I
		//IL_0031: Expected F4, but got O
		//IL_0031: Expected F4, but got O
		Vector2 inputRange = InputRange;
		Vector2 inputRange2 = InputRange;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.AudioVolumeConnection)+2C]");
		float outMin = default(float);
		float outAnchor = default(float);
		float outMax = default(float);
		bool clamp = default(bool);
		float volume2 = MathUtils.MapWithAnchor(volume, (float)inputRange, (float)inputRange2, 0f, outMin, outAnchor, outMax, clamp);
		AudioListener.volume = volume2;
	}
}
