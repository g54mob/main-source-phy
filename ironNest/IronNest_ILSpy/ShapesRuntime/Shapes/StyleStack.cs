using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;

namespace Shapes;

[StructLayout((LayoutKind)0, Size = 1)]
public struct StyleStack : IDisposable
{
	private static readonly Stack<DrawStyle> styles;

	internal unsafe static void Push(DrawStyle prevState)
	{
		//IL_000e: Expected O, but got Ref
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_006b: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		DrawStyle drawStyle = prevState;
		object obj3 = default(object);
		obj = obj3;
		DrawStyle drawStyle2 = default(DrawStyle);
		drawStyle = drawStyle2;
		do
		{
			obj += 128;
			drawStyle = (DrawStyle)(drawStyle + 128);
			_ = drawStyle.renderState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-10]");
			_ = 0;
		}
		while (styles != null);
		obj = drawStyle.renderState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)+20]");
		_ = 0;
		_ = drawStyle.color;
		styles.Push((DrawStyle)(&obj2));
	}

	internal unsafe static void Pop()
	{
		//IL_001d: Expected I, but got O
		//IL_002b: Expected I, but got O
		//IL_0089: Expected O, but got Ref
		//IL_00a7: Expected I, but got O
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0038: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
		nint num = (nint)typeof(Draw);
		nint num2 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rax_v8 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num3 = (nint)0 + (nint)200;
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = default(object);
		obj = obj3;
		IntPtr intPtr = default(IntPtr);
		num3 = intPtr;
		do
		{
			num3 = (nint)obj;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+60]");
			_ = 0;
			num3 += 128;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+70]");
			_ = 0;
			obj += 128;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v64 @ rax_v7 (Il2CppClass<Shapes.Draw>)+E4]");
		}
		while ((nint)0 != 0);
		num3 = (nint)obj;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v10+30]");
		_ = 0;
	}

	internal unsafe StyleStack(DrawStyle style)
	{
		//IL_000e: Expected O, but got Ref
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_006b: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		DrawStyle drawStyle = style;
		object obj3 = default(object);
		obj = obj3;
		DrawStyle drawStyle2 = default(DrawStyle);
		drawStyle = drawStyle2;
		do
		{
			obj += 128;
			drawStyle = (DrawStyle)(drawStyle + 128);
			_ = drawStyle.renderState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-60]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)-10]");
			_ = 0;
		}
		while (styles != null);
		obj = drawStyle.renderState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rbx_v2 (Shapes.DrawStyle)+20]");
		_ = 0;
		_ = drawStyle.color;
		styles.Push((DrawStyle)(&obj2));
	}

	public void Dispose()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x181068B70\"");
	}

	static StyleStack()
	{
		Stack<DrawStyle> stack = new Stack<DrawStyle>();
		styles = stack;
	}
}
