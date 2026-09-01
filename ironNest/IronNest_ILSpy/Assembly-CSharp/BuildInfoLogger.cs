using Cpp2ILInjected;
using UnityEngine;

public static class BuildInfoLogger
{
	private static void LogBuildInfo()
	{
		//IL_0038: Expected I, but got O
		//IL_00a5: Expected I, but got O
		//IL_00b5: Expected O, but got I
		//IL_0125: Expected I, but got O
		//IL_0135: Expected O, but got I
		//IL_01b4: Expected I, but got O
		//IL_01c4: Expected O, but got I
		object[] array = new object[4];
		string version = Application.version;
		if (version != null)
		{
			nint num = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj2 = default(object);
				throw obj2;
			}
		}
		array[0] = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object obj3 = default(object);
		if (obj3 != null)
		{
			nint num2 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rdx_v26 (Il2CppClass<System.Object[]>)+40]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj5 = default(object);
			bool flag = obj5 == null;
			object obj6 = obj3;
			if (flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj7 = default(object);
				throw obj7;
			}
		}
		array[1] = obj3;
		bool flag2 = "/main" == null;
		object obj8 = "/main";
		if (!flag2)
		{
			nint num3 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v324 @ rdx_v24 (Il2CppClass<System.Object[]>)+40]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj10 = default(object);
			bool flag3 = obj10 == null;
			object obj11 = "/main";
			if (flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj12 = default(object);
				throw obj12;
			}
			obj8 = "/main";
		}
		array[2] = obj8;
		bool flag4 = "2026-08-06 16:56" == null;
		object obj13 = "2026-08-06 16:56";
		if (!flag4)
		{
			nint num4 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v384 @ rdx_v22 (Il2CppClass<System.Object[]>)+40]");
			object obj14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj15 = default(object);
			bool flag5 = obj15 == null;
			object obj16 = "2026-08-06 16:56";
			if (flag5)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				object obj17 = default(object);
				throw obj17;
			}
			obj13 = "2026-08-06 16:56";
		}
		array[3] = obj13;
		string message = string.Format("Build: {0} (cs:{1})\nBranch: {2}\nBuild date: {3}", array);
		Debug.Log(message);
	}
}
