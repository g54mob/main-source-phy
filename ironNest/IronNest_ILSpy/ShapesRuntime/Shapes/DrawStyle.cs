using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal struct DrawStyle
{
	private const float DEFAULT_THICKNESS = 0.05f;

	private const ThicknessSpace DEFAULT_THICKNESS_SPACE = ThicknessSpace.Meters;

	public static DrawStyle @default;

	public RenderState renderState;

	public Color color;

	public ShapesBlendMode blendMode;

	public ScaleMode scaleMode;

	public DetailLevel detailLevel;

	public bool useDashes;

	public DashStyle dashStyle;

	public bool useGradients;

	public GradientFill gradientFill;

	public float radius;

	public float thickness;

	public ThicknessSpace thicknessSpace;

	public ThicknessSpace radiusSpace;

	public ThicknessSpace sizeSpace;

	public LineEndCap lineEndCaps;

	public LineGeometry lineGeometry;

	public PolygonTriangulation polygonTriangulation;

	public PolylineGeometry polylineGeometry;

	public PolylineJoins polylineJoins;

	public DiscGeometry discGeometry;

	public int regularPolygonSideCount;

	public RegularPolygonGeometry regularPolygonGeometry;

	public TextStyle textStyle;

	unsafe static DrawStyle()
	{
		//IL_0008: Expected O, but got Ref
		//IL_00b4: Expected O, but got Ref
		//IL_00eb: Expected I, but got O
		//IL_0134: Expected I, but got O
		//IL_0345: Expected I, but got O
		//IL_035b: Expected O, but got I
		//IL_039a: Expected I, but got O
		//IL_0191: Expected O, but got Ref
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_0015: Expected O, but got I
		//IL_0040: Expected I, but got O
		//IL_005e: Expected O, but got Ref
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		_ = 0;
		_ = 1;
		_ = 2;
		_ = 0;
		nint num = (nint)typeof(DashStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v5 (Il2CppClass<Shapes.DashStyle>)+B8]");
		nint num2 = 0;
		_ = DashStyle.defaultDashStyle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v6 (Il2CppStaticFields<Shapes.DashStyle>)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v6 (Il2CppStaticFields<Shapes.DashStyle>)+18]");
		_ = 0;
		_ = 0;
		nint num3 = (nint)typeof(GradientFill);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v9 (Il2CppClass<Shapes.GradientFill>)+B8]");
		nint num4 = 0;
		_ = GradientFill.defaultFill;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v10 (Il2CppStaticFields<Shapes.GradientFill>)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v10 (Il2CppStaticFields<Shapes.GradientFill>)+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v10 (Il2CppStaticFields<Shapes.GradientFill>)+30]");
		_ = 0;
		nint num5 = (nint)typeof(TextStyle);
		_ = 1028443341;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v10 (Il2CppStaticFields<Shapes.GradientFill>)+40]");
		obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [1822E8410]");
		_ = 0;
		_ = 0;
		_ = 1065353216;
		_ = 2;
		_ = 1;
		_ = 1;
		_ = 0;
		nint num6 = (nint)typeof(TextStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v12 (Il2CppClass<Shapes.TextStyle>)+B8]");
		nint num7 = 0;
		_ = TextStyle.defaultTextStyle;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v13 (Il2CppStaticFields<Shapes.TextStyle>)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v13 (Il2CppStaticFields<Shapes.TextStyle>)+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v13 (Il2CppStaticFields<Shapes.TextStyle>)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v124 @ rax_v13 (Il2CppStaticFields<Shapes.TextStyle>)+40]");
		_ = 0;
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
		object obj5 = default(object);
		nint num8 = (nint)(&obj5);
		object obj6 = default(object);
		obj4 = obj6;
		IntPtr intPtr = default(IntPtr);
		num8 = intPtr;
		do
		{
			obj4 += 128;
			num8 += 128;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)-10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rax_v11 (Il2CppClass<Shapes.TextStyle>)+E4]");
		}
		while ((nint)0 != 0);
		obj4 = num8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)+10]");
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v143 @ rcx_v9 (Il2CppMethodInfo)+30]");
		_ = 0;
		nint num9 = (nint)typeof(DrawStyle);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ rax_v18 (Il2CppClass<Shapes.DrawStyle>)+B8]");
		nint num10 = 0;
		object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 144));
		obj7 = obj6;
		num10 = intPtr;
		do
		{
			num10 += 128;
			obj7 += 128;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20-10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v106 @ rax_v11 (Il2CppClass<Shapes.TextStyle>)+E4]");
		}
		while ((nint)0 != 0);
		@default = (DrawStyle)obj7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ rax_v20+30]");
		_ = 0;
	}
}
