using System;
using Cpp2ILInjected;

public static class FlagExtensions
{
	public unsafe static bool Has<T>(T value, T flag) where T : Enum
	{
		//IL_0008: Expected O, but got Ref
		//IL_0065: Expected O, but got I
		//IL_0110: Expected O, but got I
		//IL_014c: Expected I, but got O
		//IL_00ae: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v2 (Il2CppClass<T>)+FC]");
		object value2 = default(object);
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v2 (Il2CppClass<T>)+FC]");
			object obj4 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rcx_v2 (Il2CppClass<T>)+FC]");
			if ((nint)obj4 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
			value2 = (IntPtr)obj2;
		}
		ulong num2 = Convert.ToUInt64(value2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		object value3 = (IntPtr)obj2;
		ulong num3 = Convert.ToUInt64(value3);
		long num4 = (long)(num2 & num3);
		bool flag2 = num4 == 0;
		return !flag2;
	}

	public unsafe static T Add<T>(T value, T flag) where T : Enum
	{
		//IL_0008: Expected O, but got Ref
		//IL_007c: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_00a2: Expected O, but got I
		//IL_023f: Expected O, but got I
		//IL_0278: Expected O, but got I
		//IL_00f2: Expected O, but got I
		//IL_012d: Expected O, but got I
		//IL_0170: Expected O, but got I
		//IL_0184: Expected O, but got I
		//IL_0194: Expected O, but got I
		//IL_01be: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v2+8]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object handle = default(object);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
			handle = 0;
		}
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)handle);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value2 = default(object);
		ulong num = Convert.ToUInt64(value2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value3 = default(object);
		ulong num2 = Convert.ToUInt64(value3);
		long value4 = (long)(num2 | num);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+58]");
		object obj9 = Enum.ToObject((Type)0, (ulong)value4);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v237 @ rcx_v19 (System.Object)+8]");
		NullReferenceException ex = (NullReferenceException)0;
		if (obj9 != null)
		{
			nint num3 = (nint)obj9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ rdx_v11 (Il2CppClass<System.Object>)+40]");
			bool flag2 = 0 != (nint)((Exception)ex)._stackTraceString;
			obj10 = obj9;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				T result = default(T);
				return result;
			}
		}
		else
		{
			ex = new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		T result2 = default(T);
		return result2;
	}

	public unsafe static T Remove<T>(T value, T flag) where T : Enum
	{
		//IL_0008: Expected O, but got Ref
		//IL_007c: Expected O, but got I
		//IL_008c: Expected O, but got I
		//IL_00a2: Expected O, but got I
		//IL_0248: Expected O, but got I
		//IL_0281: Expected O, but got I
		//IL_00f2: Expected O, but got I
		//IL_012d: Expected O, but got I
		//IL_0179: Expected O, but got I
		//IL_018d: Expected O, but got I
		//IL_019d: Expected O, but got I
		//IL_01c7: Expected I, but got O
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v2+8]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object handle = default(object);
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
			object obj6 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
			if ((nint)obj6 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
			handle = 0;
		}
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)handle);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value2 = default(object);
		ulong num = Convert.ToUInt64(value2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value3 = default(object);
		ulong num2 = Convert.ToUInt64(value3);
		long num3 = (long)(~num2);
		long value4 = num3 & (long)num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+58]");
		object obj9 = Enum.ToObject((Type)0, (ulong)value4);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v19 @ r9+38]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rcx_v19 (System.Object)+8]");
		NullReferenceException ex = (NullReferenceException)0;
		if (obj9 != null)
		{
			nint num4 = (nint)obj9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ rdx_v11 (Il2CppClass<System.Object>)+40]");
			bool flag2 = 0 != (nint)((Exception)ex)._stackTraceString;
			obj10 = obj9;
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				T result = default(T);
				return result;
			}
		}
		else
		{
			ex = new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		T result2 = default(T);
		return result2;
	}
}
