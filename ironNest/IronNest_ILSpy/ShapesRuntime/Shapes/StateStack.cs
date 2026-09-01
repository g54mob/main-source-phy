using System;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

[StructLayout((LayoutKind)0, Size = 1)]
public struct StateStack : IDisposable
{
	internal unsafe static void Push(DrawStyle style, Matrix4x4 mtx)
	{
		//IL_0028: Expected O, but got Ref
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_012b: Expected O, but got I4
		//IL_0085: Expected O, but got Ref
		//IL_0093: Expected O, but got Ref
		bool flag = StyleStack.styles == null;
		object obj2 = default(object);
		object obj = (object)(&obj2);
		DrawStyle drawStyle = style;
		object obj3 = default(object);
		obj = obj3;
		DrawStyle drawStyle2 = default(DrawStyle);
		drawStyle = drawStyle2;
		object obj4;
		do
		{
			obj += 128;
			drawStyle = (DrawStyle)(drawStyle + 128);
			_ = drawStyle.renderState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-10]");
			_ = 0;
			obj4 = !flag;
		}
		while (obj4 != null);
		obj = drawStyle.renderState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)+20]");
		_ = 0;
		_ = drawStyle.color;
		StyleStack.styles.Push((DrawStyle)(&obj2));
		object obj5 = default(object);
		MatrixStack.Push((Matrix4x4)(&obj5));
	}

	internal static void Pop()
	{
		MatrixStack.Pop();
		StyleStack.Pop();
	}

	internal unsafe StateStack(DrawStyle style, Matrix4x4 mtx)
	{
		//IL_0028: Expected O, but got Ref
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_012b: Expected O, but got I4
		//IL_0085: Expected O, but got Ref
		//IL_0093: Expected O, but got Ref
		bool flag = StyleStack.styles == null;
		object obj2 = default(object);
		object obj = (object)(&obj2);
		DrawStyle drawStyle = style;
		object obj3 = default(object);
		obj = obj3;
		DrawStyle drawStyle2 = default(DrawStyle);
		drawStyle = drawStyle2;
		object obj4;
		do
		{
			obj += 128;
			drawStyle = (DrawStyle)(drawStyle + 128);
			_ = drawStyle.renderState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)-10]");
			_ = 0;
			obj4 = !flag;
		}
		while (obj4 != null);
		obj = drawStyle.renderState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v105 @ rbx_v2 (Shapes.DrawStyle)+20]");
		_ = 0;
		_ = drawStyle.color;
		StyleStack.styles.Push((DrawStyle)(&obj2));
		object obj5 = default(object);
		MatrixStack.Push((Matrix4x4)(&obj5));
	}

	public void Dispose()
	{
		MatrixStack.Pop();
		StyleStack.Pop();
	}
}
