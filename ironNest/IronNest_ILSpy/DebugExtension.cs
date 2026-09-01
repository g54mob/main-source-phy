using Cpp2ILInjected;
using UnityEngine;

public static class DebugExtension
{
	public static void DrawPoint(Vector3 position, Color color, float scale = 1f)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01e9: Expected I, but got O
		//IL_0242: Expected I, but got O
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0111: Expected I, but got O
		//IL_016a: Expected I, but got O
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_017d: Expected I, but got O
		//IL_01d6: Expected I, but got O
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 95;
		float num = scale * 0.1f;
		nint num2 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rdx_v1 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num3 = 0;
		_ = Vector3.upVector;
		_ = position.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		float num4 = 0f * num;
		float num5 = position.z - num4;
		nint num6 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rdx_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num7 = 0;
		_ = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rax_v6 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		float num8 = 0f * num;
		_ = position.x;
		float num9 = num8 + position.z;
		Color color2 = (Color)(obj - 41);
		Vector3 end = (Vector3)(obj - 73);
		Vector3 start = (Vector3)(obj - 57);
		_ = color.r;
		Debug.DrawLine(start, end, color2);
		nint num10 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rdx_v4 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num11 = 0;
		_ = Vector3.rightVector;
		_ = position.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v192 @ rax_v10 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		float num12 = 0f * num;
		float num13 = position.z - num12;
		nint num14 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rdx_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num15 = 0;
		Color color3 = (Color)(obj - 41);
		Vector3 end2 = (Vector3)(obj - 57);
		Vector3 start2 = (Vector3)(obj - 73);
		_ = Vector3.rightVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v12 (Il2CppStaticFields<UnityEngine.Vector3>)+44]");
		float num16 = 0f * num;
		_ = position.x;
		float num17 = num16 + position.z;
		_ = color.r;
		Debug.DrawLine(start2, end2, color3);
		nint num18 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ rdx_v7 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num19 = 0;
		_ = Vector3.forwardVector;
		_ = position.x;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v295 @ rax_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
		float num20 = 0f * num;
		float num21 = position.z - num20;
		nint num22 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rdx_v8 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num23 = 0;
		Color color4 = (Color)(obj - 41);
		Vector3 end3 = (Vector3)(obj - 57);
		Vector3 start3 = (Vector3)(obj - 73);
		_ = Vector3.forwardVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v328 @ rax_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+50]");
		float num24 = 0f * num;
		_ = position.x;
		float num25 = num24 + position.z;
		_ = color.r;
		Debug.DrawLine(start3, end3, color4);
	}
}
