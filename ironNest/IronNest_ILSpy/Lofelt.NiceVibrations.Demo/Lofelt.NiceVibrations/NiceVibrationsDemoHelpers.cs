using Cpp2ILInjected;

namespace Lofelt.NiceVibrations;

public static class NiceVibrationsDemoHelpers
{
	public static float Round(float value, int digits)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm1,edx\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033FCB0");
		float num = 10f * value;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1800033E0");
		return num / 10f;
	}

	public static float Remap(float x, float A, float B, float C, float D)
	{
		float num = x - A;
		float num2 = B - A;
		object obj = default(object);
		float num3 = (float)obj - C;
		float num4 = num / num2;
		float num5 = num4 * num3;
		return num5 + C;
	}
}
