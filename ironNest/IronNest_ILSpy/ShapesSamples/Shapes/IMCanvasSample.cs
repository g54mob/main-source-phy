using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

public class IMCanvasSample : ImmediateModeCanvas
{
	public unsafe override void DrawCanvasShapes(ImCanvasContext ctx)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_018d: Expected O, but got Ref
		//IL_01a9: Expected I, but got O
		//IL_01d2: Expected O, but got Ref
		//IL_022e: Expected O, but got Ref
		//IL_0245: Expected I, but got O
		//IL_02ec: Expected O, but got F4
		//IL_0088: Expected I, but got O
		//IL_00bf: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_00e7: Expected I, but got O
		//IL_033b: Expected I, but got O
		//IL_029e: Expected I, but got O
		//IL_0133: Expected O, but got F4
		//IL_0133: Expected O, but got Ref
		//IL_0133: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = ctx + 40;
		object obj4 = ctx + 44;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
		{
			obj3 = obj4;
		}
		float num = (float)obj3 * 0.5f;
		float num2 = num * 0.9f;
		Vector3 pos = default(Vector3);
		DiscColors discColors = (Color)(&pos);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B590");
		nint num3 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v366 @ rax_v16 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num4 = 0;
		Quaternion q = default(Quaternion);
		Vector3 s = default(Vector3);
		Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref pos, ref q, ref s);
		Matrix4x4 matrix4x2 = (Matrix4x4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
		_ = matrix4x.m00;
		_ = matrix4x.m01;
		_ = matrix4x.m02;
		_ = matrix4x.m03;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v17 (Il2CppStaticFields<Shapes.Draw>)+98]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v17 (Il2CppStaticFields<Shapes.Draw>)+A8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v17 (Il2CppStaticFields<Shapes.Draw>)+B8]");
		_ = 0;
		Matrix4x4 matrix4x4 = default(Matrix4x4);
		Matrix4x4 matrix4x3 = (Matrix4x4)(&matrix4x4) * matrix4x2;
		nint num5 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v451 @ rax_v26 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num6 = 0;
		_ = discColors.outerStart;
		Draw.matrix = (Matrix4x4)matrix4x3.m00;
		_ = matrix4x3.m01;
		_ = matrix4x3.m02;
		_ = matrix4x3.m03;
		_ = discColors.innerEnd;
		_ = discColors.outerEnd;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180F9B6A0");
		MatrixStack.Pop();
		nint num7 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v550 @ rax_v36 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v557 @ rcx_v28 (Il2CppStaticFields<Shapes.Draw>)+108]");
		float num9 = default(float);
		Vector4 vector = default(Vector4);
		Draw.Rectangle_Internal(ShapesBlendMode.Opaque, true, (Rect)(&s), (Color)(&pos), num9, vector);
		DrawPanels();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18106D140");
		Rect canvasRect = ctx.canvasRect;
		pos = Vector3.zeroVector;
		float thickness = default(float);
		do
		{
			nint num10 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v724 @ rax_v51 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num11 = 0;
			nint num12 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v760 @ rax_v54 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num13 = 0;
			nint num14 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v761 @ rcx_v42 (Il2CppStaticFields<Shapes.Draw>)+F8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v726 @ rcx_v39 (Il2CppStaticFields<Shapes.Draw>)+190]");
			Draw.Line_Internal(LineEndCap.Round, ThicknessSpace.Meters, (Vector3)(&canvasRect), (Vector3)(&pos), (Color)num9, (Color)vector, thickness);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rcx_v44 (Il2CppClass<Shapes.Draw>)+E4]");
		}
		while ((nint)0 != 0);
	}
}
