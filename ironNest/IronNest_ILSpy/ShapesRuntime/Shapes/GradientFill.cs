using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

[Serializable]
public struct GradientFill
{
	internal const int FILL_NONE = -1;

	public static readonly GradientFill defaultFill;

	public FillType type;

	public FillSpace space;

	public Color colorStart;

	public Color colorEnd;

	public Vector3 linearStart;

	public Vector3 linearEnd;

	public Vector3 radialOrigin;

	public float radialRadius;

	public unsafe static GradientFill Linear(Vector3 start, Vector3 end, Color colorStart, Color colorEnd, FillSpace space = FillSpace.Local)
	{
		//IL_0013: Expected O, but got I4
		//IL_000e: Expected native int or pointer, but got O
		//IL_0022: Expected native int or pointer, but got O
		//IL_0039: Expected O, but got F4
		//IL_0034: Expected native int or pointer, but got O
		//IL_0041: Expected native int or pointer, but got O
		//IL_004e: Expected native int or pointer, but got O
		//IL_0065: Expected O, but got F4
		//IL_0060: Expected native int or pointer, but got O
		//IL_0081: Expected O, but got F4
		//IL_007c: Expected native int or pointer, but got O
		//IL_0099: Expected I, but got O
		//IL_00c3: Expected O, but got I
		//IL_00be: Expected native int or pointer, but got O
		//IL_00f5: Expected F4, but got I
		//IL_00f0: Expected native int or pointer, but got O
		GradientFill gradientFill = default(GradientFill);
		((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)0;
		_ = 0;
		((GradientFill*)(nint)gradientFill)->type = FillType.LinearGradient;
		((GradientFill*)(nint)gradientFill)->colorStart = (Color)colorStart.r;
		FillSpace fillSpace = default(FillSpace);
		((GradientFill*)(nint)gradientFill)->space = fillSpace;
		object obj = default(object);
		((GradientFill*)(nint)gradientFill)->colorEnd = (Color)obj;
		((GradientFill*)(nint)gradientFill)->linearStart = (Vector3)start.x;
		_ = start.z;
		((GradientFill*)(nint)gradientFill)->linearEnd = (Vector3)end.x;
		_ = end.z;
		nint num = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v3 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v8 (Il2CppStaticFields<Shapes.GradientFill>)+40]");
		((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v8 (Il2CppStaticFields<Shapes.GradientFill>)+48]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v3 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rax_v10 (Il2CppStaticFields<Shapes.GradientFill>)+4C]");
		((GradientFill*)(nint)gradientFill)->radialRadius = 0f;
		return gradientFill;
	}

	public unsafe static GradientFill Radial(Vector3 origin, float radius, Color colorInner, Color colorOuter, FillSpace space = FillSpace.Local)
	{
		//IL_0013: Expected O, but got I4
		//IL_000e: Expected native int or pointer, but got O
		//IL_002d: Expected O, but got I4
		//IL_0028: Expected native int or pointer, but got O
		//IL_003b: Expected native int or pointer, but got O
		//IL_0052: Expected O, but got F4
		//IL_004d: Expected native int or pointer, but got O
		//IL_005b: Expected native int or pointer, but got O
		//IL_0068: Expected native int or pointer, but got O
		//IL_007b: Expected I, but got O
		//IL_0098: Expected native int or pointer, but got O
		//IL_00b2: Expected O, but got I
		//IL_00ad: Expected native int or pointer, but got O
		//IL_00e4: Expected O, but got I
		//IL_00df: Expected native int or pointer, but got O
		//IL_0103: Expected O, but got F4
		//IL_00fe: Expected native int or pointer, but got O
		GradientFill gradientFill = default(GradientFill);
		((GradientFill*)(nint)gradientFill)->linearStart = (Vector3)0;
		_ = 0;
		_ = 0;
		((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)0;
		_ = 0;
		FillSpace fillSpace = default(FillSpace);
		((GradientFill*)(nint)gradientFill)->space = fillSpace;
		((GradientFill*)(nint)gradientFill)->colorStart = (Color)colorInner.r;
		((GradientFill*)(nint)gradientFill)->type = FillType.RadialGradient;
		object obj = default(object);
		((GradientFill*)(nint)gradientFill)->colorEnd = (Color)obj;
		nint num = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rcx_v3 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num2 = 0;
		((GradientFill*)(nint)gradientFill)->radialRadius = radius;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v6 (Il2CppStaticFields<Shapes.GradientFill>)+28]");
		((GradientFill*)(nint)gradientFill)->linearStart = (Vector3)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v6 (Il2CppStaticFields<Shapes.GradientFill>)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rcx_v3 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v8 (Il2CppStaticFields<Shapes.GradientFill>)+34]");
		((GradientFill*)(nint)gradientFill)->linearEnd = (Vector3)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v8 (Il2CppStaticFields<Shapes.GradientFill>)+3C]");
		_ = 0;
		((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)origin.x;
		_ = origin.z;
		return gradientFill;
	}

	internal unsafe Vector4 GetShaderStartVector()
	{
		//IL_0074: Expected native int or pointer, but got O
		//IL_002e: Expected F4, but got O
		//IL_0029: Expected native int or pointer, but got O
		//IL_0043: Expected F4, but got I
		//IL_003e: Expected native int or pointer, but got O
		//IL_0058: Expected F4, but got I
		//IL_0053: Expected native int or pointer, but got O
		//IL_0062: Expected native int or pointer, but got O
		Vector4 vector = default(Vector4);
		if (type != FillType.LinearGradient)
		{
			((Vector4*)(nint)vector)->x = (float)radialOrigin;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.GradientFill)+44]");
			((Vector4*)(nint)vector)->y = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (Shapes.GradientFill)+48]");
			((Vector4*)(nint)vector)->z = 0f;
			((Vector4*)(nint)vector)->w = radialRadius;
			return vector;
		}
		float x = default(float);
		((Vector4*)(nint)vector)->x = x;
		return vector;
	}

	internal int GetShaderFillTypeInt(bool use)
	{
		//IL_002a: Expected I4, but got I8
		if (use)
		{
			return (int)type;
		}
		return -1;
	}

	public unsafe static GradientFill CreateLinear(Vector3 start, Vector3 end, Color colorStart, Color colorEnd, FillSpace space)
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_002e: Expected O, but got I4
		//IL_0029: Expected native int or pointer, but got O
		GradientFill gradientFill = default(GradientFill);
		((GradientFill*)(nint)gradientFill)->type = FillType.LinearGradient;
		_ = 0;
		_ = 0;
		_ = 0;
		((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)0;
		return gradientFill;
	}

	public unsafe static GradientFill CreateRadial(Vector3 origin, float radius, Color colorInner, Color colorOuter, FillSpace space)
	{
		//IL_0009: Expected native int or pointer, but got O
		//IL_002e: Expected O, but got I4
		//IL_0029: Expected native int or pointer, but got O
		GradientFill gradientFill = default(GradientFill);
		((GradientFill*)(nint)gradientFill)->type = FillType.LinearGradient;
		_ = 0;
		_ = 0;
		_ = 0;
		((GradientFill*)(nint)gradientFill)->radialOrigin = (Vector3)0;
		return gradientFill;
	}

	static GradientFill()
	{
		//IL_001d: Expected I, but got O
		//IL_0040: Expected I, but got O
		//IL_005a: Expected O, but got I4
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rdx_v1 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		nint num3 = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rax_v12 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num4 = 0;
		defaultFill = (GradientFill)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206F00]");
		_ = 0;
		_ = 1065353216;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rax_v4 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		_ = Vector3.zeroVector;
	}
}
