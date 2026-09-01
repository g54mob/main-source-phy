using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Shapes;

internal static class ObjectPool<T> where T : new()
{
	private static readonly Stack<T> pool;

	public unsafe static T Alloc()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_003c: Expected O, but got I
		//IL_004c: Expected O, but got I
		//IL_0062: Expected O, but got I
		//IL_009b: Expected O, but got I
		//IL_00b0: Expected O, but got I
		//IL_00c0: Expected O, but got I
		//IL_00d5: Expected O, but got I
		//IL_010f: Expected O, but got I
		//IL_01b8: Expected O, but got I
		//IL_01d2: Expected O, but got I4
		//IL_014d: Expected O, but got I
		//IL_0167: Expected O, but got I4
		//IL_0209: Expected O, but got I
		//IL_021e: Expected O, but got I
		//IL_022e: Expected O, but got I
		//IL_0243: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rax_v2+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rax_v3+28]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rcx_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx+20]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ rax_v15+C0]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rax_v16+10]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rax_v18+B8]");
		object obj10 = 0;
		object obj11 = obj10;
		if (obj10 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx+20]");
			object obj12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v132 @ rdi_v1+18]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v23+135]");
				object obj13 = (nint)0 & (nint)1;
				bool flag = obj13 == null;
				object obj14 = !flag;
				if (obj14 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18067CD50");
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v194 @ rax_v23+135]");
				object obj15 = (nint)0 & (nint)1;
				bool flag2 = obj15 == null;
				object obj16 = !flag2;
				if (obj16 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx+20]");
				object obj17 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v334 @ rax_v30+C0]");
				object obj18 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ rax_v31+10]");
				object obj19 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v347 @ rax_v33+B8]");
				object obj20 = 0;
				if (obj20 == null)
				{
					goto IL_0274;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			T result = default(T);
			return result;
		}
		goto IL_0274;
		IL_0274:
		return (T)new NullReferenceException();
	}

	public unsafe static void Free(T obj)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0032: Expected O, but got I
		//IL_0048: Expected O, but got I
		//IL_0182: Expected O, but got Ref
		//IL_008c: Expected O, but got I
		//IL_00a1: Expected O, but got I
		//IL_00ba: Expected O, but got Ref
		//IL_00ca: Expected O, but got I
		//IL_0125: Expected O, but got I
		//IL_013b: Expected O, but got I
		//IL_0155: Expected O, but got Ref
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v3 (Il2CppRgctx<Shapes.ObjectPool`1>)+28]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rcx_v2+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v36 @ rcx_v2+FC]");
		object obj6 = default(object);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			obj6 = (object)(&obj3);
		}
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ rax_v16 (Il2CppRgctx<Shapes.ObjectPool`1>)+10]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v131 @ rax_v18+B8]");
		object obj8 = 0;
		nint num3 = 0;
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj3, 32));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ rax_v22 (Il2CppRgctx<Shapes.ObjectPool`1>)+28]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ rcx_v9+28]");
		if ((nint)0 < (nint)0)
		{
			val = obj;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num4 = 0;
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v208 @ rax_v30 (Il2CppRgctx<Shapes.ObjectPool`1>)+28]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v209 @ rcx_v13+28]");
		object obj11 = (nint)0 >> 31;
		bool flag = obj11 != null;
		object obj12 = (object)(&obj3);
		if (!flag)
		{
			obj12 = obj6;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809176D0");
	}

	static ObjectPool()
	{
		//IL_0045: Expected O, but got I
		//IL_005a: Expected O, but got I
		nint num = 0;
		object obj = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180918060");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12 (Il2CppRgctx<Shapes.ObjectPool`1>)+10]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v14+B8]");
		object obj3 = 0;
		obj3 = obj;
	}
}
