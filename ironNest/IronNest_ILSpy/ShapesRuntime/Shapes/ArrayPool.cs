using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace Shapes;

internal static class ArrayPool<T>
{
	private static readonly Stack<T[]> pool;

	public static T[] Alloc(int maxCount)
	{
		//IL_002a: Expected O, but got I
		//IL_003f: Expected O, but got I
		//IL_0114: Expected O, but got I
		//IL_012e: Expected O, but got I4
		//IL_00ad: Expected O, but got I
		//IL_00c7: Expected O, but got I4
		//IL_0170: Expected O, but got I
		//IL_0185: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rax_v9 (Il2CppRgctx<Shapes.ArrayPool`1>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v11+B8]");
		object obj2 = 0;
		object obj3 = obj2;
		if (obj2 != null)
		{
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rdi_v1+18]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rax_v16 (Il2CppClass<Shapes.ArrayPool`1>)+135]");
				object obj4 = (nint)0 & (nint)1;
				bool flag = obj4 == null;
				object obj5 = !flag;
				if (obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
				}
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"SzArrayNew\"");
				T[] result = default(T[]);
				return result;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v152 @ rax_v16 (Il2CppClass<Shapes.ArrayPool`1>)+135]");
			object obj6 = (nint)0 & (nint)1;
			bool flag2 = obj6 == null;
			object obj7 = !flag2;
			if (obj7 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
			}
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v282 @ rax_v24 (Il2CppRgctx<Shapes.ArrayPool`1>)+10]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ rax_v26+B8]");
			object obj9 = 0;
			if (obj9 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
				T[] result2 = default(T[]);
				return result2;
			}
		}
		return (T[])(object)new NullReferenceException();
	}

	public static void Free(T[] obj)
	{
		//IL_002a: Expected O, but got I
		//IL_003f: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rax_v9 (Il2CppRgctx<Shapes.ArrayPool`1>)+10]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v11+B8]");
		object obj3 = 0;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809176D0");
	}

	static ArrayPool()
	{
		//IL_0045: Expected O, but got I
		//IL_005a: Expected O, but got I
		nint num = 0;
		object obj = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180918060");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12 (Il2CppRgctx<Shapes.ArrayPool`1>)+10]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v14+B8]");
		object obj3 = 0;
		obj3 = obj;
	}
}
