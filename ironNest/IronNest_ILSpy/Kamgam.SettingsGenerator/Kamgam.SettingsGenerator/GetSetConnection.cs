using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public class GetSetConnection<T> : Connection<T>
{
	private Func<T> m_Getter;

	private Action<T> m_Setter;

	protected T _value;

	public event Func<T> Getter
	{
		add
		{
			//IL_002e: Expected O, but got I4
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			Delegate obj = (Delegate)obj2;
			object obj5 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj, value);
				nint num2 = 0;
				if ((object)obj3 == null)
				{
					object obj4 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj5 == null;
					object obj4 = obj5;
					if (flag)
					{
						break;
					}
				}
				nint num3 = 0;
				IntPtr intPtr2 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_002e: Expected O, but got I4
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj2 = default(object);
			Delegate obj = (Delegate)obj2;
			object obj5 = default(object);
			Delegate obj6 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj, value);
				nint num2 = 0;
				if ((object)obj3 == null)
				{
					object obj4 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj5 == null;
					object obj4 = obj5;
					if (flag)
					{
						break;
					}
				}
				nint num3 = 0;
				IntPtr intPtr2 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj6 != obj;
				obj = obj6;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public event Action<T> Setter
	{
		add
		{
			//IL_0024: Expected O, but got I
			//IL_0044: Expected O, but got I4
			//IL_009c: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
			object obj = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			Delegate obj2 = (Delegate)obj3;
			object obj6 = default(object);
			Delegate obj8 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Combine(obj2, value);
				nint num2 = 0;
				if ((object)obj4 == null)
				{
					object obj5 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj6 == null;
					object obj5 = obj6;
					if (flag)
					{
						break;
					}
				}
				nint num3 = 0;
				IntPtr intPtr2 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
				object obj7 = (nint)0 + (nint)32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj8 != obj2;
				obj2 = obj8;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_0024: Expected O, but got I
			//IL_0044: Expected O, but got I4
			//IL_009c: Expected O, but got I
			nint num = 0;
			IntPtr intPtr = num;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v22 @ rax_v2 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
			object obj = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj3 = default(object);
			Delegate obj2 = (Delegate)obj3;
			object obj6 = default(object);
			Delegate obj8 = default(Delegate);
			while (true)
			{
				Delegate obj4 = Delegate.Remove(obj2, value);
				nint num2 = 0;
				if ((object)obj4 == null)
				{
					object obj5 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag = obj6 == null;
					object obj5 = obj6;
					if (flag)
					{
						break;
					}
				}
				nint num3 = 0;
				IntPtr intPtr2 = num3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
				object obj7 = (nint)0 + (nint)32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj8 != obj2;
				obj2 = obj8;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	public GetSetConnection(Func<T> getter, Action<T> setter)
	{
		//IL_0026: Expected O, but got I
		//IL_0036: Expected O, but got I
		//IL_0046: Expected O, but got I
		//IL_02a9: Expected O, but got I
		//IL_02b9: Expected O, but got I
		//IL_02c9: Expected O, but got I
		//IL_025b: Expected O, but got I4
		//IL_0073: Expected O, but got I4
		//IL_00b7: Expected O, but got I
		//IL_00c7: Expected O, but got I
		//IL_0125: Expected O, but got I
		//IL_0135: Expected O, but got I
		//IL_0145: Expected O, but got I
		//IL_0163: Expected O, but got I
		//IL_031a: Expected O, but got I
		//IL_032a: Expected O, but got I
		//IL_033a: Expected O, but got I
		//IL_01b2: Expected O, but got I4
		//IL_0183: Expected O, but got I4
		//IL_01d8: Expected O, but got I
		//IL_01e8: Expected O, but got I
		//IL_0206: Expected O, but got I
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ r10_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+38]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ r14_v1+20]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v30 @ rax_v4+C0]");
		object obj3 = 0;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj6 = default(object);
		Delegate obj5 = (Delegate)obj6;
		object obj12 = default(object);
		Delegate obj18 = default(Delegate);
		object obj25 = default(object);
		object obj30 = default(object);
		Delegate obj35 = default(Delegate);
		while (true)
		{
			Delegate obj7 = Delegate.Combine(obj5, getter);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ r14_v1+20]");
			object obj8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rcx_v4+C0]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rdx_v7+8]");
			object obj10 = 0;
			if ((object)obj7 == null)
			{
				object obj11 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				bool flag = obj12 == null;
				object obj11 = obj12;
				if (flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
					object obj13 = 0;
					Delegate obj14 = obj7;
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ r14_v1+20]");
			object obj15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v153 @ rax_v12+C0]");
			object obj16 = 0;
			object obj17 = obj16;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag2 = (object)obj18 != obj5;
			obj5 = obj18;
			if (flag2)
			{
				continue;
			}
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v12 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+40]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r14_v3+20]");
			object obj20 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v17+C0]");
			object obj21 = 0;
			object obj22 = obj21;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v168 @ rax_v18+80]");
			object obj23 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Delegate obj24 = (Delegate)obj25;
			while (true)
			{
				Delegate obj26 = Delegate.Combine(obj24, setter);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r14_v3+20]");
				object obj27 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rcx_v17+C0]");
				object obj28 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rdx_v18+18]");
				obj10 = 0;
				if ((object)obj26 == null)
				{
					object obj29 = 0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag3 = obj30 == null;
					object obj29 = obj30;
					object obj13 = 0;
					Delegate obj14 = obj26;
					if (flag3)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v165 @ r14_v3+20]");
				object obj31 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v331 @ rax_v24+C0]");
				object obj32 = 0;
				object obj33 = obj32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v333 @ rax_v25+80]");
				object obj34 = (nint)0 + (nint)32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag4 = (object)obj35 != obj24;
				obj24 = obj35;
				if (!flag4)
				{
					return;
				}
			}
			break;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public unsafe override T Get()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_0047: Expected O, but got I
		//IL_005d: Expected O, but got I
		//IL_01d0: Expected O, but got I
		//IL_01e0: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_009c: Expected O, but got I
		//IL_0152: Expected O, but got I
		//IL_0162: Expected O, but got I
		//IL_0180: Expected O, but got I
		//IL_00ed: Expected O, but got I
		//IL_00fd: Expected O, but got I
		//IL_011b: Expected O, but got I
		//IL_012d: Expected O, but got Ref
		//IL_013d: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v93 @ r9_v2+50]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rax_v7+C0]");
			object obj8 = 0;
			object obj9 = obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj10 = default(object);
			bool flag = obj10 == null;
			object obj12 = default(object);
			object obj11 = obj12;
			if (flag)
			{
				goto IL_0142;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v15+C0]");
		object obj14 = 0;
		object obj15 = obj14;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj17 = default(object);
		object obj16 = obj17;
		if (obj17 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v104 @ rcx_v10+18] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
			object obj18 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v114 @ rax_v20+C0]");
			object obj19 = 0;
			object obj20 = obj19;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rax_v21+80]");
			object obj21 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7800");
			object obj11 = (object)(&obj2);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
			obj4 = 0;
			goto IL_0142;
		}
		return (T)new NullReferenceException();
		IL_0142:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8+20]");
		object obj22 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rax_v10+C0]");
		object obj23 = 0;
		object obj24 = obj23;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v99 @ rax_v11+80]");
		object obj25 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe override void Set(T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002d: Expected O, but got I
		//IL_0043: Expected O, but got I
		//IL_0194: Expected O, but got Ref
		//IL_019c: Expected O, but got Ref
		//IL_01b2: Expected O, but got I
		//IL_0208: Expected O, but got I
		//IL_0236: Expected O, but got I
		//IL_00b0: Expected O, but got I
		//IL_00c8: Expected O, but got Ref
		//IL_00e6: Expected O, but got I
		//IL_0131: Expected O, but got I
		//IL_0147: Expected O, but got I
		//IL_0161: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ r9_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ rax_v2+FC]");
		object obj5 = default(object);
		T val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
			obj5 = (object)(&obj2);
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v49 @ rcx_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+50]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_01da;
			}
		}
		val = value;
		goto IL_01da;
		IL_01da:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		IntPtr intPtr = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rax_v11 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
		object obj7 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7800");
		nint num4 = 0;
		IntPtr intPtr2 = num4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v14 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
		object obj8 = (nint)0 + (nint)32;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
		object obj9 = default(object);
		if (obj9 != null)
		{
			nint num5 = 0;
			IntPtr intPtr3 = num5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rax_v18 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
			object obj10 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			T val2 = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
			object obj12 = default(object);
			object obj11 = obj12;
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v146 @ rcx_v10 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+50]");
			object obj13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rax_v21+28]");
			if ((nint)0 < (nint)0)
			{
				val2 = value;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			nint num7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rcx_v12 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+50]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v198 @ rax_v25+28]");
			object obj15 = (nint)0 >> 31;
			bool flag = obj15 != null;
			object obj16 = (object)(&obj2);
			if (!flag)
			{
				obj16 = obj5;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v130 @ rdi_v4+18] (should have been resolved before IL gen)");
		}
	}

	public unsafe T GetLastKnownValue()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0018: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_004c: Expected O, but got I
		//IL_0062: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_00ba: Expected O, but got I
		//IL_00d8: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3 @ rax_v1+C0]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ r9_v1+50]");
		object obj5 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rax_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ r8+20]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rax_v7+C0]");
			object obj8 = 0;
			object obj9 = obj8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ rax_v8+80]");
			object obj10 = (nint)0 + (nint)64;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe void SetLastKnownValue(T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0028: Expected O, but got I
		//IL_003e: Expected O, but got I
		//IL_0082: Expected O, but got Ref
		//IL_0098: Expected O, but got I
		//IL_00ee: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		object obj4 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v18 @ rax_v2+FC]");
		T val;
		if ((nint)obj4 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 40));
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rcx_v1 (Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>)+50]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v8+28]");
			if ((nint)0 >= (nint)0)
			{
				goto IL_00c0;
			}
		}
		val = value;
		goto IL_00c0;
		IL_00c0:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		IntPtr intPtr = num3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v11 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.GetSetConnection`1>>)+80]");
		object obj6 = (nint)0 + (nint)64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A7800");
	}
}
