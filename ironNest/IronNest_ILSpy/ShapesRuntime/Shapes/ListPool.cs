using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace Shapes;

internal static class ListPool<T>
{
	private static readonly Stack<List<T>> pool;

	public static List<T> Alloc()
	{
		//IL_002a: Expected O, but got I
		//IL_003f: Expected O, but got I
		//IL_0124: Expected O, but got I
		//IL_013e: Expected O, but got I4
		//IL_00ad: Expected O, but got I
		//IL_00c7: Expected O, but got I4
		//IL_0180: Expected O, but got I
		//IL_0195: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v9 (Il2CppRgctx<Shapes.ListPool`1>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rax_v11+B8]");
		object obj2 = 0;
		object obj3 = obj2;
		if (obj2 != null)
		{
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rdi_v1+18]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v16 (Il2CppClass<Shapes.ListPool`1>)+135]");
				object obj4 = (nint)0 & (nint)1;
				bool flag = obj4 == null;
				object obj5 = !flag;
				if (obj5 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
				}
				nint num3 = 0;
				List<T> result = null;
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
				return result;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v16 (Il2CppClass<Shapes.ListPool`1>)+135]");
			object obj6 = (nint)0 & (nint)1;
			bool flag2 = obj6 == null;
			object obj7 = !flag2;
			if (obj7 == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
			}
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v294 @ rax_v24 (Il2CppRgctx<Shapes.ListPool`1>)+10]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ rax_v26+B8]");
			object obj9 = 0;
			if (obj9 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
				List<T> result2 = default(List<T>);
				return result2;
			}
		}
		return (List<T>)(object)new NullReferenceException();
	}

	public static void Free(List<T> list)
	{
		//IL_001b: Expected O, but got I
		//IL_004b: Expected O, but got I
		//IL_005b: Expected O, but got I
		//IL_0116: Expected O, but got I
		//IL_012b: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v5 (Il2CppRgctx<Shapes.ListPool`1>)+38]");
		object obj = 0;
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rcx_v3+20]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v6+C0]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj4 = default(object);
		if (obj4 == null)
		{
			list._size = 0;
		}
		else
		{
			list._size = 0;
			if (list._size > 0)
			{
				Array.Clear(list._items, 0, list._size);
			}
		}
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v17 (Il2CppRgctx<Shapes.ListPool`1>)+10]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ rax_v19+B8]");
		object obj6 = 0;
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809176D0");
	}

	static ListPool()
	{
		//IL_0045: Expected O, but got I
		//IL_005a: Expected O, but got I
		nint num = 0;
		object obj = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180918060");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12 (Il2CppRgctx<Shapes.ListPool`1>)+10]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v14+B8]");
		object obj3 = 0;
		obj3 = obj;
	}
}
