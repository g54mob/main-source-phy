using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;

namespace Shapes;

[StructLayout((LayoutKind)0, Size = 1)]
public struct GradientFillStack : IDisposable
{
	private static readonly Stack<(bool, GradientFill)> gradients;

	internal unsafe static void Push(bool prevOn, GradientFill prevState)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0061: Expected O, but got Ref
		//IL_00a8: Expected O, but got I
		//IL_001b: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		GradientFill item = (GradientFill)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
		bool item2 = (byte)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 64)) != 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [prevState @ rdx (Shapes.GradientFill)+10]");
		_ = 0;
		_ = prevState.type;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [prevState @ rdx (Shapes.GradientFill)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [prevState @ rdx (Shapes.GradientFill)+20]");
		obj = 0;
		_ = prevState.radialOrigin;
		(bool, GradientFill) tuple = (item2, item);
		(bool, GradientFill) item3 = ((bool, GradientFill))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		gradients.Push(item3);
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rax_v14 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num4 = 0;
	}

	internal unsafe GradientFillStack(bool on, GradientFill gradient)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0061: Expected O, but got Ref
		//IL_00a8: Expected O, but got I
		//IL_001b: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		GradientFill item = (GradientFill)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 32));
		bool item2 = (byte)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72)) != 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gradient @ r8 (Shapes.GradientFill)+10]");
		_ = 0;
		_ = gradient.type;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gradient @ r8 (Shapes.GradientFill)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [gradient @ r8 (Shapes.GradientFill)+20]");
		obj = 0;
		_ = gradient.radialOrigin;
		(bool, GradientFill) tuple = (item2, item);
		(bool, GradientFill) item3 = ((bool, GradientFill))System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		gradients.Push(item3);
	}

	public void Dispose()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 32 Invalid \"Jump target not found in method: 0x18103D7A0\"");
	}

	static GradientFillStack()
	{
		Stack<(bool, GradientFill)> stack = new Stack<(bool, GradientFill)>();
		gradients = stack;
	}
}
