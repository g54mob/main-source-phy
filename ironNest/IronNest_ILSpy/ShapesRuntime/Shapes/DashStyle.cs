using System;
using Cpp2ILInjected;

namespace Shapes;

[Serializable]
public struct DashStyle
{
	public static readonly DashStyle defaultDashStyle;

	public static readonly DashStyle defaultDashStyleRing;

	public static readonly DashStyle defaultDashStyleLine;

	public DashType type;

	public DashSpace space;

	public DashSnapping snap;

	public float size;

	public float spacing;

	public float offset;

	public float shapeModifier;

	public float UniformSize
	{
		get
		{
			return size;
		}
		set
		{
			size = value;
			if (space == DashSpace.FixedCount)
			{
				spacing = 0.5f;
			}
			else
			{
				spacing = value;
			}
		}
	}

	public unsafe static DashStyle DefaultDashStyle
	{
		get
		{
			//IL_0013: Expected I, but got O
			//IL_0036: Expected I4, but got O
			//IL_0031: Expected native int or pointer, but got O
			//IL_004b: Expected F4, but got I
			//IL_0046: Expected native int or pointer, but got O
			//IL_0060: Expected F4, but got I
			//IL_005b: Expected native int or pointer, but got O
			nint num = (nint)typeof(DashStyle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
			nint num2 = 0;
			DashStyle dashStyle = default(DashStyle);
			((DashStyle*)(nint)dashStyle)->type = (DashType)defaultDashStyle;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+10]");
			((DashStyle*)(nint)dashStyle)->spacing = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
			((DashStyle*)(nint)dashStyle)->shapeModifier = 0f;
			return dashStyle;
		}
		set
		{
		}
	}

	public unsafe static DashStyle DefaultDashStyleRing
	{
		get
		{
			//IL_0013: Expected I, but got O
			//IL_0036: Expected I4, but got O
			//IL_0031: Expected native int or pointer, but got O
			//IL_004b: Expected F4, but got I
			//IL_0046: Expected native int or pointer, but got O
			//IL_0060: Expected F4, but got I
			//IL_005b: Expected native int or pointer, but got O
			nint num = (nint)typeof(DashStyle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
			nint num2 = 0;
			DashStyle dashStyle = default(DashStyle);
			((DashStyle*)(nint)dashStyle)->type = (DashType)defaultDashStyleRing;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+2C]");
			((DashStyle*)(nint)dashStyle)->spacing = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+34]");
			((DashStyle*)(nint)dashStyle)->shapeModifier = 0f;
			return dashStyle;
		}
		set
		{
		}
	}

	public unsafe static DashStyle DefaultDashStyleLine
	{
		get
		{
			//IL_0013: Expected I, but got O
			//IL_0036: Expected I4, but got O
			//IL_0031: Expected native int or pointer, but got O
			//IL_004b: Expected F4, but got I
			//IL_0046: Expected native int or pointer, but got O
			//IL_0060: Expected F4, but got I
			//IL_005b: Expected native int or pointer, but got O
			nint num = (nint)typeof(DashStyle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
			nint num2 = 0;
			DashStyle dashStyle = default(DashStyle);
			((DashStyle*)(nint)dashStyle)->type = (DashType)defaultDashStyleLine;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+48]");
			((DashStyle*)(nint)dashStyle)->spacing = 0f;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+50]");
			((DashStyle*)(nint)dashStyle)->shapeModifier = 0f;
			return dashStyle;
		}
		set
		{
		}
	}

	private float GetNet(float v, float thickness)
	{
		float num = default(float);
		if (space == DashSpace.Relative)
		{
			return num * thickness;
		}
		return num;
	}

	internal float GetNetAbsoluteSize(bool dashed, float thickness)
	{
		//IL_0050: Expected F4, but got I4
		if (dashed)
		{
			float num = size;
			if (space == DashSpace.Relative)
			{
				num *= thickness;
			}
			return num;
		}
		return 0f;
	}

	internal float GetNetAbsoluteSpacing(bool dashed, float thickness)
	{
		//IL_0050: Expected F4, but got I4
		if (dashed)
		{
			float num = spacing;
			if (space == DashSpace.Relative)
			{
				num *= thickness;
			}
			return num;
		}
		return 0f;
	}

	public unsafe static DashStyle RelativeDashes(DashType type, float size, float spacing, DashSnapping snap = DashSnapping.Off, float offset = 0f, float shapeModifier = 1f)
	{
		//IL_0008: Expected native int or pointer, but got O
		//IL_0015: Expected native int or pointer, but got O
		//IL_0022: Expected native int or pointer, but got O
		//IL_002f: Expected native int or pointer, but got O
		//IL_003c: Expected native int or pointer, but got O
		//IL_0053: Expected I4, but got I8
		//IL_004e: Expected native int or pointer, but got O
		//IL_005b: Expected native int or pointer, but got O
		DashStyle dashStyle = default(DashStyle);
		DashSnapping dashSnapping = default(DashSnapping);
		((DashStyle*)(nint)dashStyle)->snap = dashSnapping;
		float num = default(float);
		((DashStyle*)(nint)dashStyle)->offset = num;
		float num2 = default(float);
		((DashStyle*)(nint)dashStyle)->shapeModifier = num2;
		((DashStyle*)(nint)dashStyle)->size = size;
		((DashStyle*)(nint)dashStyle)->spacing = spacing;
		((DashStyle*)(nint)dashStyle)->space = DashSpace.Relative;
		((DashStyle*)(nint)dashStyle)->type = type;
		return dashStyle;
	}

	public unsafe static DashStyle FixedDashCount(DashType type, float count, float spacingFraction = 0.5f, DashSnapping snap = DashSnapping.Off, float offset = 0f, float shapeModifier = 1f)
	{
		//IL_0008: Expected native int or pointer, but got O
		//IL_0015: Expected native int or pointer, but got O
		//IL_0022: Expected native int or pointer, but got O
		//IL_002f: Expected native int or pointer, but got O
		//IL_003c: Expected native int or pointer, but got O
		//IL_0053: Expected I4, but got I8
		//IL_004e: Expected native int or pointer, but got O
		//IL_005b: Expected native int or pointer, but got O
		DashStyle dashStyle = default(DashStyle);
		DashSnapping dashSnapping = default(DashSnapping);
		((DashStyle*)(nint)dashStyle)->snap = dashSnapping;
		float num = default(float);
		((DashStyle*)(nint)dashStyle)->offset = num;
		float num2 = default(float);
		((DashStyle*)(nint)dashStyle)->shapeModifier = num2;
		((DashStyle*)(nint)dashStyle)->size = count;
		((DashStyle*)(nint)dashStyle)->spacing = spacingFraction;
		((DashStyle*)(nint)dashStyle)->space = DashSpace.FixedCount;
		((DashStyle*)(nint)dashStyle)->type = type;
		return dashStyle;
	}

	public unsafe static DashStyle MeterDashes(DashType type, float size, float spacing, DashSnapping snap = DashSnapping.Off, float offset = 0f, float shapeModifier = 1f)
	{
		//IL_0008: Expected native int or pointer, but got O
		//IL_0015: Expected native int or pointer, but got O
		//IL_0022: Expected native int or pointer, but got O
		//IL_002f: Expected native int or pointer, but got O
		//IL_003c: Expected native int or pointer, but got O
		//IL_004a: Expected native int or pointer, but got O
		//IL_0057: Expected native int or pointer, but got O
		DashStyle dashStyle = default(DashStyle);
		DashSnapping dashSnapping = default(DashSnapping);
		((DashStyle*)(nint)dashStyle)->snap = dashSnapping;
		float num = default(float);
		((DashStyle*)(nint)dashStyle)->offset = num;
		float num2 = default(float);
		((DashStyle*)(nint)dashStyle)->shapeModifier = num2;
		((DashStyle*)(nint)dashStyle)->size = size;
		((DashStyle*)(nint)dashStyle)->spacing = spacing;
		((DashStyle*)(nint)dashStyle)->space = DashSpace.Meters;
		((DashStyle*)(nint)dashStyle)->type = type;
		return dashStyle;
	}

	public DashStyle(float size)
	{
		//IL_0015: Expected I4, but got O
		//IL_0023: Expected I, but got O
		//IL_0053: Expected I, but got O
		//IL_0097: Expected I, but got O
		//IL_00b9: Expected F4, but got I
		//IL_00c7: Expected I, but got O
		//IL_00e9: Expected F4, but got I
		type = (DashType)defaultDashStyle;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rcx_v4 (Il2CppStaticFields<Shapes.DashStyle>)+4]");
		space = DashSpace.Meters;
		nint num3 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v7 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num4 = 0;
		this.size = size;
		spacing = size;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v5 (Il2CppStaticFields<Shapes.DashStyle>)+8]");
		snap = DashSnapping.Off;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rax_v9 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rcx_v6 (Il2CppStaticFields<Shapes.DashStyle>)+14]");
		offset = 0f;
		nint num7 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v11 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rcx_v7 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
		shapeModifier = 0f;
	}

	public DashStyle(float size, DashType type)
	{
		//IL_001d: Expected I, but got O
		//IL_0052: Expected I, but got O
		//IL_0096: Expected I, but got O
		//IL_00b8: Expected F4, but got I
		//IL_00c6: Expected I, but got O
		//IL_00e8: Expected F4, but got I
		this.type = type;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+4]");
		space = DashSpace.Meters;
		nint num3 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num4 = 0;
		this.size = size;
		spacing = size;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v4 (Il2CppStaticFields<Shapes.DashStyle>)+8]");
		snap = DashSnapping.Off;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rax_v7 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rcx_v5 (Il2CppStaticFields<Shapes.DashStyle>)+14]");
		offset = 0f;
		nint num7 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rax_v9 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rcx_v6 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
		shapeModifier = 0f;
	}

	public DashStyle(float size, float spacing, DashType type)
	{
		//IL_001d: Expected I, but got O
		//IL_0052: Expected I, but got O
		//IL_0096: Expected I, but got O
		//IL_00b8: Expected F4, but got I
		//IL_00c6: Expected I, but got O
		//IL_00e8: Expected F4, but got I
		this.type = type;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rax_v4 (Il2CppStaticFields<Shapes.DashStyle>)+4]");
		space = DashSpace.Meters;
		nint num3 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num4 = 0;
		this.size = size;
		this.spacing = spacing;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rcx_v4 (Il2CppStaticFields<Shapes.DashStyle>)+8]");
		snap = DashSnapping.Off;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v7 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v5 (Il2CppStaticFields<Shapes.DashStyle>)+14]");
		offset = 0f;
		nint num7 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v9 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v6 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
		shapeModifier = 0f;
	}

	public DashStyle(float size, float spacing)
	{
		//IL_0015: Expected I4, but got O
		//IL_0023: Expected I, but got O
		//IL_0053: Expected I, but got O
		//IL_0097: Expected I, but got O
		//IL_00b9: Expected F4, but got I
		//IL_00c7: Expected I, but got O
		//IL_00e9: Expected F4, but got I
		type = (DashType)defaultDashStyle;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v4 (Il2CppStaticFields<Shapes.DashStyle>)+4]");
		space = DashSpace.Meters;
		nint num3 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v7 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num4 = 0;
		this.size = size;
		this.spacing = spacing;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rcx_v5 (Il2CppStaticFields<Shapes.DashStyle>)+8]");
		snap = DashSnapping.Off;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v9 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rcx_v6 (Il2CppStaticFields<Shapes.DashStyle>)+14]");
		offset = 0f;
		nint num7 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rax_v11 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rcx_v7 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
		shapeModifier = 0f;
	}

	public DashStyle(float size, float spacing, float offset)
	{
		//IL_0015: Expected I4, but got O
		//IL_0023: Expected I, but got O
		//IL_0053: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00c3: Expected F4, but got I
		type = (DashType)defaultDashStyle;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rcx_v4 (Il2CppStaticFields<Shapes.DashStyle>)+4]");
		space = DashSpace.Meters;
		nint num3 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v7 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num4 = 0;
		this.size = size;
		this.spacing = spacing;
		this.offset = offset;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ rcx_v5 (Il2CppStaticFields<Shapes.DashStyle>)+8]");
		snap = DashSnapping.Off;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v9 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v65 @ rcx_v6 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
		shapeModifier = 0f;
	}

	static DashStyle()
	{
		//IL_0013: Expected I, but got O
		//IL_002d: Expected O, but got I4
		//IL_0046: Expected I, but got O
		//IL_0060: Expected O, but got I4
		//IL_0079: Expected I, but got O
		//IL_0093: Expected O, but got I4
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v3 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		defaultDashStyle = (DashStyle)0;
		_ = 1f;
		nint num3 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ rax_v4 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num4 = 0;
		defaultDashStyleRing = (DashStyle)0;
		_ = 1f;
		nint num5 = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v62 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num6 = 0;
		defaultDashStyleLine = (DashStyle)0;
		_ = 1f;
	}
}
