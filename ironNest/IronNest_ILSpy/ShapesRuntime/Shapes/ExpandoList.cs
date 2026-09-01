using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Shapes;

public class ExpandoList<T>
{
	public List<T> list;

	// C# has no syntax for parameterized property 'Item'.
	public unsafe T get_Item(int i)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_005d: Expected O, but got I
		//IL_016e: Expected O, but got I
		//IL_00d9: Expected O, but got I
		//IL_00e9: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ r9_v1+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ r10_v1+18]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rax_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rax_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ rax_v2+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			if (i < 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				throw ex;
			}
		}
		List<T> list = this.list;
		if (this.list != null)
		{
			if (i < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ r9_v1+20]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rax_v23+C0]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			T result = default(T);
			return result;
		}
		return (T)new NullReferenceException();
	}

	public unsafe void set_Item(int i, T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0023: Expected O, but got I
		//IL_0039: Expected O, but got I
		//IL_0311: Expected O, but got I
		//IL_0319: Expected O, but got Ref
		//IL_0214: Expected O, but got Ref
		//IL_022a: Expected O, but got I
		//IL_009a: Expected O, but got I4
		//IL_027d: Expected O, but got I
		//IL_0293: Expected O, but got I
		//IL_02ad: Expected O, but got Ref
		//IL_0262: Expected O, but got I
		//IL_0151: Expected O, but got Ref
		//IL_0167: Expected O, but got I
		//IL_00c8: Expected O, but got I4
		//IL_00dd: Expected O, but got I
		//IL_01ba: Expected O, but got I
		//IL_01d0: Expected O, but got I
		//IL_01ea: Expected O, but got Ref
		//IL_019f: Expected O, but got I
		//IL_00f8: Expected O, but got I
		//IL_010e: Expected O, but got I
		//IL_0128: Expected O, but got Ref
		//IL_038a: Expected O, but got I
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Expected O, but got Unknown
		//IL_03b6: Expected O, but got I
		//IL_03c6: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ r10_v1 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+FC]");
		object obj6 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+FC]");
			object obj5 = (nint)0 + (nint)15;
			obj6 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+FC]");
			if ((nint)obj5 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			if (i < 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				throw ex;
			}
		}
		List<T> list = this.list;
		if (i >= list._size)
		{
			object obj7 = i - list._size;
			bool flag = (nint)obj7 <= 0;
			ExpandoList<T> expandoList = this;
			if (!flag)
			{
				object obj8 = 0;
				ExpandoList<T> expandoList2 = this;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+FC]");
				object obj9 = 0;
				bool flag3;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					nint num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ rcx_v26 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
					object obj10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ rax_v38+28]");
					object obj11 = (nint)0 >> 31;
					bool flag2 = obj11 != null;
					object obj12 = (object)(&obj2);
					if (!flag2)
					{
						obj12 = obj6;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+40]");
					expandoList = (ExpandoList<T>)0;
					obj8++;
					flag3 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj8) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+40]");
					expandoList2 = (ExpandoList<T>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2+FC]");
					obj9 = 0;
				}
				while (flag3);
			}
			object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 80));
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rcx_v17 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rax_v28+28]");
			if ((nint)0 < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+50]");
				obj13 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v362 @ rcx_v19 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
			object obj15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v363 @ rax_v31+28]");
			object obj16 = (nint)0 >> 31;
			bool flag4 = obj16 != null;
			object obj17 = (object)(&obj2);
			if (!flag4)
			{
				obj17 = obj6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
		}
		else
		{
			object obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 80));
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v211 @ rcx_v9 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rax_v20+28]");
			if ((nint)0 < (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rbp_v1+50]");
				obj18 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v345 @ rcx_v11 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ rax_v23+28]");
			object obj21 = (nint)0 >> 31;
			bool flag5 = obj21 != null;
			object obj22 = (object)(&obj2);
			if (!flag5)
			{
				obj22 = obj6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6FE0");
		}
	}

	public unsafe void SetCountToAtLeast(int minCount)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_014e: Expected O, but got I
		//IL_0156: Expected O, but got Ref
		//IL_0199: Expected O, but got I
		//IL_01a9: Expected O, but got I
		//IL_009e: Expected O, but got I
		//IL_00c7: Expected O, but got I4
		//IL_00e2: Expected O, but got I
		//IL_00f8: Expected O, but got I
		//IL_0112: Expected O, but got Ref
		//IL_01c8: Expected O, but got I
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v23 @ r9_v1 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
		object obj6 = default(object);
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
			object obj5 = (nint)0 + (nint)15;
			obj6 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rax_v2+FC]");
			if ((nint)obj5 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+30]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v239 @ rcx_v6+10]");
			object obj8 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rax_v12+18]");
		if ((nint)0 >= (nint)minCount)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rax_v12+18]");
		object obj9 = (nint)minCount - (nint)0;
		if ((nint)obj9 <= 0)
		{
			return;
		}
		object obj10 = 0;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ rcx_v9 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
			object obj11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ rax_v21+28]");
			object obj12 = (nint)0 >> 31;
			bool flag = obj12 != null;
			object obj13 = (object)(&obj2);
			if (!flag)
			{
				obj13 = obj6;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+30]");
			object obj7 = 0;
			obj10++;
		}
		while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj10) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9));
	}

	public unsafe void Add(T item)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0028: Expected O, but got I
		//IL_003e: Expected O, but got I
		//IL_00e3: Expected O, but got Ref
		//IL_00f1: Expected O, but got Ref
		//IL_0101: Expected O, but got I
		//IL_0080: Expected O, but got I
		//IL_0096: Expected O, but got I
		//IL_00b0: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r9_v1 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rax_v2+FC]");
		object obj5 = default(object);
		T val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
			nint num2 = 0;
			obj5 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rcx_v1 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0129;
			}
		}
		val = item;
		goto IL_0129;
		IL_0129:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v3 (Il2CppRgctx<Shapes.ExpandoList`1>)+18]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12+28]");
		object obj8 = (nint)0 >> 31;
		bool flag = obj8 != null;
		object obj9 = (object)(&obj2);
		if (!flag)
		{
			obj9 = obj5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371280");
	}

	public void Clear()
	{
		//IL_0025: Expected O, but got I
		//IL_0055: Expected O, but got I
		//IL_0065: Expected O, but got I
		List<T> list = this.list;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rcx_v1 (Il2CppRgctx<Shapes.ExpandoList`1>)+38]");
		object obj = 0;
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rdx_v1+20]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v3+C0]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj4 = default(object);
		if (obj4 == null)
		{
			list._size = 0;
			return;
		}
		list._size = 0;
		if (list._size > 0)
		{
			Array.Clear(list._items, 0, list._size);
		}
	}

	public void ClearAndSetMinCapacity(int minCapacity)
	{
		//IL_0025: Expected O, but got I
		//IL_0055: Expected O, but got I
		//IL_0065: Expected O, but got I
		List<T> list = this.list;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v26 @ r9_v2 (Il2CppRgctx<Shapes.ExpandoList`1>)+38]");
		object obj = 0;
		int version = list._version + 1;
		list._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ r10_v2+20]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rax_v4+C0]");
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
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180832960");
		object obj5 = default(object);
		if ((nint)obj5 < minCapacity)
		{
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6EC0");
		}
	}

	public ExpandoList()
	{
		nint num = 0;
		List<T> list = null;
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
		this.list = list;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
	}
}
