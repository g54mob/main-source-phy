using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class MMSignal : MonoBehaviour
{
	public enum SignalType
	{
		DigitalNoise,
		Pulse,
		Sawtooth,
		Sine,
		Square,
		Triangle,
		WhiteNoise
	}

	public static float GetValue(float time, SignalType signalType, float phase, float amplitude, float frequency, float offset, bool Invert = false)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0054: Expected O, but got I8
		//IL_006e: Expected O, but got I8
		object obj2 = default(object);
		object obj = obj2 ^ 1;
		object obj3 = obj * 2;
		object obj4 = obj3 - 1;
		if (signalType <= SignalType.WhiteNoise)
		{
			object obj5 = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v1+A8B328+signalType @ rdx (Lofelt.NiceVibrations.MMSignal+SignalType)*4]");
			object obj6 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v39 @ rcx_v2 (should have been resolved before IL gen)");
		}
		float num = (float)obj4 * amplitude;
		float num2 = num * 0f;
		object obj7 = default(object);
		return num2 + (float)obj7;
	}
}
