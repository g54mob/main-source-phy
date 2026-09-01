using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public static class CollectionExtensions
{
	public static bool IsNull(string text)
	{
		return string.IsNullOrEmpty(text);
	}

	public static bool IsNull(ICollection list)
	{
		return list == null;
	}

	public static bool IsNullOrEmpty(ICollection list)
	{
		if (list == null)
		{
			return true;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		return obj == null;
	}

	public static bool IsNullOrEmpty(IEnumerable source)
	{
		//IL_0027: Expected O, but got I4
		if (source != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			bool flag = obj == null;
			object obj2 = 0;
			if (flag)
			{
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj3 = default(object);
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj4 = default(object);
				if (obj4 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return false;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj5 = default(object);
			if (obj5 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		return true;
	}

	public unsafe static bool HasValuesThatAreNotNull(IEnumerable source)
	{
		//IL_0129: Expected I4, but got O
		//IL_0034: Expected O, but got Ref
		//IL_003c: Expected O, but got Ref
		//IL_0045: Expected O, but got I4
		//IL_0093: Expected O, but got I4
		if (!IsNullOrEmpty(source))
		{
			if (source == null)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			object obj = (object)(&obj2);
			object obj4 = default(object);
			object obj3 = (object)(&obj4);
			object obj5 = 0;
			object obj6 = default(object);
			object obj7 = default(object);
			while (true)
			{
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj6 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					bool flag = obj7 == null;
					obj5 = 1;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371000");
						return true;
					}
					continue;
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj8 = default(object);
			obj3 = obj8;
			if (obj8 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		return false;
	}

	public static bool IsIndexOutOfBounds(ICollection list, int index)
	{
		//IL_00a5: Expected I4, but got O
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected I4, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected I4, but got Unknown
		if (index < 0)
		{
			return true;
		}
		if (list != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			object obj = index - obj2;
			int num = index ^ obj2;
			int num2 = index ^ obj;
			int num3 = num & num2;
			bool flag = num3 < 0;
			bool flag2 = (nint)obj < 0;
			return flag2 == flag;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public static void RemoveRange(IList list, IEnumerable collection)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_004f: Expected O, but got I4
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_00b6: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F057]");
		bool flag = (nint)0 != 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj2 = default(object);
		object obj = obj2 - 1;
		object obj4 = default(object);
		object obj5 = default(object);
		object obj6 = default(object);
		object obj7 = default(object);
		while ((nint)obj >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj3 = 0;
			while (true)
			{
				if (obj4 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj5 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
						flag = obj6 != obj7;
						object obj8 = obj;
						obj3 = 0;
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180007350");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371000");
							obj8 = obj;
							break;
						}
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371000");
					break;
				}
				throw new NullReferenceException();
			}
			obj--;
		}
	}

	public unsafe static void AddIfNotContained(IList list, IEnumerable collection)
	{
		//IL_0017: Expected O, but got Ref
		//IL_001f: Expected O, but got Ref
		//IL_007a: Expected I, but got O
		//IL_010d: Expected O, but got I4
		//IL_00b2: Expected O, but got I
		//IL_00bb: Expected O, but got I4
		//IL_011a: Expected I, but got O
		//IL_01ec: Expected O, but got I
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0394: Expected I, but got O
		//IL_0152: Expected O, but got I
		//IL_015b: Expected O, but got I4
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_01c4: Expected O, but got I4
		//IL_023d: Expected O, but got I
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		IList list2 = default(IList);
		object obj = (object)(&list2);
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		IList list3 = null;
		object obj4 = default(object);
		object obj14 = default(object);
		object obj24 = default(object);
		object obj25 = default(object);
		IntPtr intPtr = default(IntPtr);
		object obj26 = default(object);
		while (true)
		{
			object obj13;
			object obj5;
			if (list2 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj4 != null)
				{
					bool flag = list2 == null;
					list3 = null;
					if (flag)
					{
						break;
					}
					nint num = (nint)list2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ r10_v5 (Il2CppClass<System.Collections.IList>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_00f2;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ r10_v5 (Il2CppClass<System.Collections.IList>)+B0]");
					obj5 = 0;
					object obj6 = 0;
					while (true)
					{
						object obj7 = obj6 + obj6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r8_v9+v291 @ rax_v32*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						obj6++;
						object obj8 = obj6;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ r10_v5 (Il2CppClass<System.Collections.IList>)+12E]");
						if ((nint)obj8 < 0)
						{
							continue;
						}
						goto IL_00f2;
					}
					object obj9 = obj6 + obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ r8_v9+8+v351 @ rcx_v30*8]");
					object obj10 = (nint)0 + (nint)1;
					object obj11 = obj10 << 4;
					object obj12 = obj11 + 312;
					obj13 = obj12 + num;
					goto IL_035b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				obj2 = obj14;
				if (obj14 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_035b:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v358 @ rdx_v11] (should have been resolved before IL gen)");
			nint num2 = (nint)list;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r10_v6 (Il2CppClass<System.Collections.IList>)+12E]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_0192;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r10_v6 (Il2CppClass<System.Collections.IList>)+B0]");
			object obj15 = 0;
			object obj16 = 0;
			while (true)
			{
				object obj17 = obj16 + obj16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r8_v16+v389 @ rax_v27*8]");
				if (0 == (nint)typeof(IList))
				{
					break;
				}
				obj16++;
				object obj18 = obj16;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ r10_v6 (Il2CppClass<System.Collections.IList>)+12E]");
				if ((nint)obj18 < 0)
				{
					continue;
				}
				goto IL_0192;
			}
			object obj19 = obj16 + obj16;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ r8_v16+8+v445 @ rcx_v22*8]");
			object obj20 = (nint)0 + (nint)3;
			object obj21 = obj20 << 4;
			object obj22 = obj21 + 312;
			object obj23 = obj22 + num2;
			goto IL_036a;
			IL_00f2:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj13 = obj24;
			obj5 = 1;
			goto IL_035b;
			IL_036a:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v453 @ r8_v10] (should have been resolved before IL gen)");
			bool flag2 = obj25 != null;
			nint num3 = (nint)typeof(IList);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				num3 = intPtr;
				list3 = (IList)2;
			}
			continue;
			IL_0192:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj23 = obj26;
			goto IL_036a;
		}
		throw new NullReferenceException();
	}

	public unsafe static void AddIfNotContained<T>(IList<T> list, T item)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0029: Expected O, but got I
		//IL_0229: Expected O, but got Ref
		//IL_0231: Expected O, but got Ref
		//IL_0071: Expected O, but got I
		//IL_008b: Expected O, but got Ref
		//IL_02fd: Expected I, but got O
		//IL_0118: Expected O, but got Ref
		//IL_0128: Expected O, but got I
		//IL_00bf: Expected O, but got I4
		//IL_0168: Expected O, but got Ref
		//IL_01e3: Expected O, but got I
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_02c8: Expected O, but got I
		//IL_02e2: Expected O, but got Ref
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r9_v1 (Il2CppClass<T>)+FC]");
		object obj4 = default(object);
		T val;
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
			obj4 = (object)(&obj2);
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rcx_v2 (Il2CppClass<T>)+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_025f;
			}
		}
		val = item;
		goto IL_025f;
		IL_00f6:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_0105;
		IL_0105:
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 80));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v15+8]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v274 @ rdx_v5+10] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+40]");
		if ((nint)0 == 0)
		{
			T val2 = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 72));
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v303 @ rcx_v11 (Il2CppClass<T>)+28]");
			if ((nint)0 < (nint)0)
			{
				val2 = item;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rcx_v13 (Il2CppClass<T>)+28]");
			object obj7 = (nint)0 >> 31;
			bool flag = obj7 != null;
			object obj8 = (object)(&obj2);
			if (!flag)
			{
				obj8 = obj4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180038F60");
		}
		return;
		IL_025f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rcx_v4 (Il2CppClass<T>)+28]");
		object obj9 = (nint)0 >> 31;
		bool flag2 = obj9 != null;
		object obj10 = (object)(&obj2);
		if (!flag2)
		{
			obj10 = obj4;
		}
		nint num6 = (nint)list;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ r10_v1 (Il2CppClass<System.Collections.Generic.IList`1<T>>)+12E]");
		bool flag3 = (nint)0 >= (nint)0;
		nint num7 = 0;
		if (flag3)
		{
			goto IL_00f6;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ r10_v1 (Il2CppClass<System.Collections.Generic.IList`1<T>>)+B0]");
		num7 = 0;
		object obj11 = 0;
		while (true)
		{
			object obj12 = obj11 + obj11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r9_v6 (Il2CppClass<T>)+v204 @ rax_v33*8]");
			if ((nint)0 == 0)
			{
				break;
			}
			obj11++;
			object obj13 = obj11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ r10_v1 (Il2CppClass<System.Collections.Generic.IList`1<T>>)+12E]");
			if ((nint)obj13 < 0)
			{
				continue;
			}
			goto IL_00f6;
		}
		object obj14 = obj11 + obj11;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ r9_v6 (Il2CppClass<T>)+8+v261 @ rcx_v21*8]");
		object obj15 = (nint)0 + (nint)4;
		object obj16 = obj15 << 4;
		object obj17 = obj16 + 312;
		object obj18 = obj17 + num6;
		goto IL_0105;
	}
}
