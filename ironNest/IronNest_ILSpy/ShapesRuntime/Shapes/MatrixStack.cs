using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

[StructLayout((LayoutKind)0, Size = 1)]
public struct MatrixStack : IDisposable
{
	private static readonly Stack<Matrix4x4> matrices;

	internal unsafe static void Push(Matrix4x4 prevState)
	{
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		matrices.Push((Matrix4x4)(&obj));
	}

	internal static void Pop()
	{
		//IL_0027: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v10 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		Matrix4x4 matrix = default(Matrix4x4);
		Draw.matrix = matrix;
	}

	internal unsafe MatrixStack(Matrix4x4 mtx)
	{
		//IL_0013: Expected O, but got Ref
		object obj = default(object);
		matrices.Push((Matrix4x4)(&obj));
	}

	public void Dispose()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x181042FC0\"");
	}

	static MatrixStack()
	{
		Stack<Matrix4x4> stack = new Stack<Matrix4x4>();
		matrices = stack;
	}
}
