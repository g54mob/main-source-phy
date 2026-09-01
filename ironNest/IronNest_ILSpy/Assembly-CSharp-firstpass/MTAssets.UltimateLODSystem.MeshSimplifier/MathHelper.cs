using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public static class MathHelper
{
	public const float PI = (float)Math.PI;

	public const double PId = Math.PI;

	public const float Deg2Rad = (float)Math.PI / 180f;

	public const double Deg2Radd = Math.PI / 180.0;

	public const float Rad2Deg = 180f / (float)Math.PI;

	public const double Rad2Degd = 180.0 / Math.PI;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Min(double val1, double val2, double val3)
	{
		//IL_0023: Expected O, but got I4
		//IL_00a4: Expected O, but got I4
		//IL_0061: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm0\"");
		object obj = default(object);
		object obj2 = default(object);
		bool flag = obj == obj2;
		object obj3 = !flag;
		object obj5 = default(object);
		object obj4 = obj3 | obj5;
		if (obj4 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm0\"");
			bool flag2 = obj == obj2;
			object obj6 = !flag2;
			object obj7 = obj6 | obj5;
			if (obj7 == null)
			{
				return val1;
			}
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm1\"");
			bool flag3 = obj == obj2;
			object obj8 = !flag3;
			object obj9 = obj8 | obj5;
			if (obj9 == null)
			{
				return val2;
			}
		}
		return val3;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Clamp(double value, double min, double max)
	{
		//IL_0023: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
		object obj = default(object);
		object obj2 = default(object);
		bool flag = obj == obj2;
		object obj3 = !flag;
		if (obj3 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm0\"");
			if (obj != obj2)
			{
				return max;
			}
		}
		return min;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double TriangleArea(ref Vector3d p0, ref Vector3d p1, ref Vector3d p2)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0367: Expected I, but got O
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0186: Expected F8, but got I
		//IL_038b: Expected I, but got O
		//IL_02f8: Expected F8, but got I
		//IL_02e2: Expected F8, but got I4
		object obj2 = default(object);
		object obj = obj2 - 95;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p1 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p0 @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
		object obj3 = obj + 23;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rbp+7]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+27]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1+27]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p2 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm7,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p0 @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [p2 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm6\"");
		_ = p1;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm1,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm2\"");
		_ = p2;
		_ = p2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAF40");
		_ = 0;
		_ = 0;
		object obj4 = obj - 9;
		object obj5 = obj + 23;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAE70");
		nint num = (nint)typeof(Vector3d);
		_ = 0;
		_ = 0;
		object obj6 = obj + 23;
		object obj7 = obj - 41;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAE70");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rbp+17h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-1]");
		double d = 0.0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [rbp+1Fh]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rbp+27h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rcx_v10 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
		bool flag = (nint)0 < (nint)0;
		double num2 = -1.0;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rcx_v10 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
			bool flag2 = (nint)0 >= (nint)0;
			num2 = 1.0;
			if (flag2)
			{
				goto IL_0236;
			}
		}
		d = num2;
		goto IL_0236;
		IL_0236:
		double a = Math.Acos(d);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [182206E98h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,qword ptr [182206E70h]\"");
		double num3 = Math.Sin(a);
		nint num4 = (nint)typeof(Math);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v231 @ rcx_v15 (Il2CppClass<System.Math>)+E4]");
		double result;
		if ((nint)0 <= (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm2\"");
			result = 0.0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rbp_v1-21]");
			result = Math.Sqrt(0.0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [182206D18h]\"");
		return result;
	}
}
