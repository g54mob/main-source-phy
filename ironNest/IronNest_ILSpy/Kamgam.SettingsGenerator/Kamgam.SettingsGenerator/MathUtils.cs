using System;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public static class MathUtils
{
	public static float MapWithAnchor(float inValue, float inMin, float inAnchor, float inMax, float outMin, float outAnchor, float outMax, bool clamp = true)
	{
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		if (!(inMin > inAnchor) && inMin < inMax)
		{
			float num = default(float);
			float num2 = default(float);
			float num3 = default(float);
			if (!(num > num2) && num < num3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
					object obj2 = default(object);
					object obj3 = default(object);
					if (obj2 == null && (obj3 == null || !(inMin > inValue)))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18038AA80");
						object obj4 = default(object);
						if (obj4 == null && (obj3 == null || !(inValue > inMax)))
						{
							float num4 = inAnchor - inValue;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
							object obj5 = num4 & 0;
							float num6;
							float num7;
							if (!(inAnchor > inValue))
							{
								float num5 = inMax - inAnchor;
								num6 = (float)obj5 / num5;
								num7 = num3;
							}
							else
							{
								float num8 = inAnchor - inMin;
								num6 = (float)obj5 / num8;
								num7 = num;
							}
							float num9 = num7 - num2;
							float num10 = num6 * num9;
							return num10 + num2;
						}
						return num3;
					}
					return num;
				}
				return num2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			object arg3 = default(object);
			string message = $"outMin ({arg}) has to be below outAnchor ({arg2}) and outMax ({arg3})";
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			Exception ex = new Exception(message);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg4 = default(object);
		object arg5 = default(object);
		object arg6 = default(object);
		string message2 = $"inMin ({arg4}) has to be below inAnchor ({arg5}) and inMax ({arg6})";
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex2 = new Exception(message2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}
}
