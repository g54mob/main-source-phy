using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

[StructLayout((LayoutKind)0, Size = 1)]
public struct ColorStack : IDisposable
{
	private static readonly Stack<Color> colors;

	internal unsafe static void Push(Color prevState)
	{
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		colors.Push((Color)(&obj));
	}

	internal static void Pop()
	{
		//IL_0027: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rax_v10 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
	}

	internal unsafe ColorStack(Color mtx)
	{
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		colors.Push((Color)(&obj));
	}

	public void Dispose()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x181037EA0\"");
	}

	static ColorStack()
	{
		Stack<Color> stack = new Stack<Color>();
		colors = stack;
	}
}
