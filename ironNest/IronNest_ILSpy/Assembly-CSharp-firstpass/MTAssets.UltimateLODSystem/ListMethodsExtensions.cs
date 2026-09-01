using System.Collections.Generic;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem;

public static class ListMethodsExtensions
{
	public unsafe static void RemoveAllNullItems<T>(List<T> list)
	{
		//IL_0008: Expected O, but got Ref
		//IL_003d: Expected O, but got I
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_006e: Expected O, but got I8
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ r8_v1 (Il2CppClass<T>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ r8_v1 (Il2CppClass<T>)+FC]");
		if ((nint)obj4 <= 0)
		{
			obj3 = 1152921504606846960L;
		}
		object obj5 = obj3 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		bool flag = (nint)list < 0;
		int num2 = list._size - 1;
		if (flag)
		{
			return;
		}
		object obj6 = default(object);
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A77D0");
			if (obj6 == null)
			{
				list.RemoveAt(num2);
			}
			num2--;
		}
		while ((nint)obj6 >= 0);
	}
}
