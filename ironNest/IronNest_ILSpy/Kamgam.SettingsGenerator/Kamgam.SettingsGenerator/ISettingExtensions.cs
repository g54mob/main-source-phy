using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public static class ISettingExtensions
{
	public static float GetFloatValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingFloat);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+130]");
			bool flag2 = num4 < 0;
			num = (nint)typeof(SettingFloat);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v10+FFFFFFF8+v39 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingFloat);
				num = (nint)typeof(SettingFloat);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4E0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v11 (should have been resolved before IL gen)");
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a float setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static float GetIntValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_009f: Expected I, but got O
		//IL_00af: Expected O, but got I
		//IL_0135: Expected I, but got O
		//IL_0145: Expected O, but got I
		//IL_0171: Expected I, but got O
		//IL_0067: Expected O, but got I
		//IL_00eb: Expected O, but got I
		//IL_018f: Expected O, but got I
		//IL_01b9: Expected I, but got O
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingInt);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingInt>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingInt>)+130]");
			if (num4 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rax_v19+FFFFFFF8+v43 @ rax_v9*8]");
				if (0 == (nint)typeof(SettingInt))
				{
					goto IL_0118;
				}
			}
			nint num5 = (nint)typeof(SettingOption);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rdx_v7 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v123 @ rdx_v7 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v141 @ rax_v18+FFFFFFF8+v124 @ rax_v14*8]");
				if (0 == (nint)typeof(SettingOption))
				{
					goto IL_0118;
				}
			}
			nint num7 = (nint)typeof(SettingColorOption);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rdx_v8 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+130]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num8 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rdx_v8 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+130]");
			bool flag2 = num8 < 0;
			num = (nint)typeof(SettingColorOption);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v60 @ rax_v17+FFFFFFF8+v61 @ rax_v16*8]");
				bool flag3 = 0 == (nint)typeof(SettingColorOption);
				num = (nint)typeof(SettingColorOption);
				if (flag3)
				{
					goto IL_0118;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not an integer, option or color option setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_0118:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v56 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8] (should have been resolved before IL gen)");
		float result = default(float);
		return result;
	}

	public static bool GetBoolValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingBool);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingBool>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingBool>)+130]");
			bool flag2 = num4 < 0;
			num = (nint)typeof(SettingBool);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v10+FFFFFFF8+v39 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingBool);
				num = (nint)typeof(SettingBool);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4E0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v11 (should have been resolved before IL gen)");
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a bool setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static string GetStringValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingString);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+130]");
			bool flag2 = num4 < 0;
			num = (nint)typeof(SettingString);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v10+FFFFFFF8+v39 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingString);
				num = (nint)typeof(SettingString);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4E0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v11 (should have been resolved before IL gen)");
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a string setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe static Color GetColorValue(ISetting setting)
	{
		//IL_0127: Expected I, but got O
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c7: Expected F4, but got O
		//IL_00c2: Expected native int or pointer, but got O
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		nint num2 = (nint)setting;
		if (!flag)
		{
			nint num3 = (nint)typeof(SettingColor);
			num = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColor>)+130]");
			bool flag2 = num4 < 0;
			num2 = (nint)typeof(SettingColor);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v58 @ rax_v10+FFFFFFF8+v42 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingColor);
				num2 = (nint)typeof(SettingColor);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v55 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8] (should have been resolved before IL gen)");
					Color color = default(Color);
					object obj3 = default(object);
					((Color*)(nint)color)->r = (float)obj3;
					return color;
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a color setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static int GetColorOptionValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingColorOption);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+130]");
			bool flag2 = num4 < 0;
			num = (nint)typeof(SettingColorOption);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v10+FFFFFFF8+v39 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingColorOption);
				num = (nint)typeof(SettingColorOption);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4E0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v11 (should have been resolved before IL gen)");
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a color option setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static KeyCombination GetKeyCombinationValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingKeyCombination);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+130]");
			bool flag2 = num4 < 0;
			num = (nint)typeof(SettingKeyCombination);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v10+FFFFFFF8+v39 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingKeyCombination);
				num = (nint)typeof(SettingKeyCombination);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4E0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v11 (should have been resolved before IL gen)");
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a KeyCombination setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public static int GetOptionValue(ISetting setting)
	{
		//IL_0013: Expected I, but got O
		//IL_001b: Expected I, but got O
		//IL_002b: Expected O, but got I
		//IL_0057: Expected I, but got O
		//IL_0075: Expected O, but got I
		//IL_00a2: Expected I, but got O
		//IL_00c0: Expected O, but got I
		//IL_00d0: Expected O, but got I
		bool flag = setting == null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			nint num2 = (nint)typeof(SettingOption);
			nint num3 = (nint)setting;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+130]");
			bool flag2 = num4 < 0;
			num = (nint)typeof(SettingOption);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v55 @ rax_v10+FFFFFFF8+v39 @ rax_v9*8]");
				bool flag3 = 0 != (nint)typeof(SettingOption);
				num = (nint)typeof(SettingOption);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ r8_v1 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4E0]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v84 @ rax_v11 (should have been resolved before IL gen)");
				}
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		Exception ex = new Exception("Setting is not a option setting!");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}
}
