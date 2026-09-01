using System;
using System.Collections.Generic;
using Cpp2ILInjected;

public static class EnumerableExtensions
{
	public unsafe static bool TryFindValue<T>(IEnumerable<T> list, Func<T, bool> func, out T item)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0038: Expected O, but got I
		//IL_00a4: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r10_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r10_v1 (Il2CppClass<T>)+FC]");
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r10_v1 (Il2CppClass<T>)+FC]");
			object obj4 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ r10_v1 (Il2CppClass<T>)+FC]");
			if ((nint)obj4 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806AF080");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66C0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A77D0");
		bool result = default(bool);
		return result;
	}
}
