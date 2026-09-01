using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;

namespace Shapes;

[StructLayout((LayoutKind)0, Size = 1)]
public struct DashStack : IDisposable
{
	private static readonly Stack<(bool, DashStyle)> dashes;

	internal unsafe static void Push(bool prevOn, DashStyle prevState)
	{
		//IL_002a: Expected O, but got Ref
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		(bool, DashStyle) tuple = ((byte)(&obj) != 0, (DashStyle)(&obj2));
		object obj3 = default(object);
		dashes.Push(((bool, DashStyle))(&obj3));
	}

	internal static void Pop()
	{
		//IL_0027: Expected I, but got O
		//IL_005a: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v144 @ rax_v10 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		nint num3 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v9 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num4 = 0;
	}

	internal unsafe DashStack(bool on, DashStyle dash)
	{
		//IL_002a: Expected O, but got Ref
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		object obj2 = default(object);
		(bool, DashStyle) tuple = ((byte)(&obj) != 0, (DashStyle)(&obj2));
		object obj3 = default(object);
		dashes.Push(((bool, DashStyle))(&obj3));
	}

	public void Dispose()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x181038C20\"");
	}

	static DashStack()
	{
		Stack<(bool, DashStyle)> stack = new Stack<(bool, DashStyle)>();
		dashes = stack;
	}
}
