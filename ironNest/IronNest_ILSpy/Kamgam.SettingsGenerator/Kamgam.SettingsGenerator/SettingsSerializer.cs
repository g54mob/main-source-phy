using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public static class SettingsSerializer
{
	public unsafe static string ToJson(Settings settings)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_00ea: Expected O, but got Ref
		//IL_0100: Expected O, but got I
		//IL_0141: Expected O, but got I4
		//IL_02a1: Expected O, but got I
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_01e9: Expected O, but got I4
		//IL_0302: Expected I, but got O
		//IL_0231: Expected O, but got I4
		List<SettingData> fields = new List<SettingData>();
		SettingFieldsData settingFieldsData = new SettingFieldsData(null);
		settingFieldsData._002Ector(null);
		settingFieldsData.Fields = fields;
		List<SettingData> list = (List<SettingData>)(settingFieldsData + 16);
		if ((object)settings != null)
		{
			if (CollectionExtensions.IsNullOrEmpty(settings._settingsCache))
			{
				settings.RebuildSettingsCache();
			}
			list = (List<SettingData>)(object)settings._settingsCache;
			if (settings._settingsCache != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				nint num = 0;
				List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
				IntPtr intPtr = default(IntPtr);
				string value = default(string);
				object obj10 = default(object);
				SettingData settingData = default(SettingData);
				object obj11 = default(object);
				while (true)
				{
					object obj9;
					if (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						bool flag = intPtr == (IntPtr)0;
						list = (List<SettingData>)(&enumerator);
						if (!flag)
						{
							object obj = (nint)intPtr;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ r10_v6+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_0178;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ r10_v6+B0]");
							num = 0;
							object obj2 = 0;
							while (true)
							{
								object obj3 = obj2 + obj2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r8_v6 (Il2CppMethodInfo)+v453 @ rax_v42*8]");
								if (0 == (nint)typeof(ISetting))
								{
									break;
								}
								obj2++;
								object obj4 = obj2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ r10_v6+12E]");
								if ((nint)obj4 < 0)
								{
									continue;
								}
								goto IL_0178;
							}
							object obj5 = obj2 + obj2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r8_v6 (Il2CppMethodInfo)+8+v509 @ rcx_v34*8]");
							object obj6 = (nint)0 + (nint)4;
							object obj7 = obj6 << 4;
							object obj8 = obj7 + 312;
							obj9 = obj8 + obj;
							goto IL_03cc;
						}
						throw new NullReferenceException();
					}
					enumerator.Dispose();
					return JsonUtility.ToJson(settingFieldsData);
					IL_03cc:
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v516 @ rdx_v18] (should have been resolved before IL gen)");
					if (string.IsNullOrEmpty(value))
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					bool flag2 = obj10 == null;
					num = intPtr;
					if (flag2)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					bool flag3 = settingData == null;
					list = (List<SettingData>)17;
					if (!flag3)
					{
						if (settingData.Type != SettingData.DataType.Unknown)
						{
							bool flag4 = settingFieldsData == null;
							list = (List<SettingData>)17;
							if (flag4)
							{
								throw new NullReferenceException();
							}
							if (settingFieldsData.Fields == null)
							{
								break;
							}
							settingFieldsData.Fields.Add(settingData);
							num = 0;
						}
						else
						{
							string message = "SGSettings: Unknown data type for path '" + settingData.ID + "'. Ignoring.";
							Debug.LogError(message);
							num = unchecked((nint)"'. Ignoring.");
						}
						continue;
					}
					throw new NullReferenceException();
					IL_0178:
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
					num = 4;
					obj9 = obj11;
					goto IL_03cc;
				}
				throw new NullReferenceException();
			}
		}
		throw new NullReferenceException();
	}

	public unsafe static void FromJson(string json, Settings settings)
	{
		//IL_00ca: Expected O, but got I4
		//IL_0464: Expected O, but got I
		//IL_01fe: Expected O, but got Ref
		//IL_0179: Expected O, but got I4
		//IL_018b: Expected I, but got O
		//IL_01bc: Expected O, but got I4
		//IL_015b: Expected O, but got I4
		//IL_0233: Expected O, but got Ref
		//IL_0281: Expected O, but got Ref
		//IL_02d6: Expected O, but got I4
		//IL_039d: Expected I, but got O
		//IL_03d5: Expected O, but got I
		//IL_03e5: Expected I, but got O
		//IL_03f4: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3F2B1]");
		bool flag = (nint)0 != 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18070BE60");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 96 Invalid \"Jump target not found in method: 0x180A48F6C\"");
		if (CollectionExtensions.IsNullOrEmpty(settings._settingsCache))
		{
			settings.RebuildSettingsCache();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 126 Invalid \"Jump target not found in method: 0x180A48F6C\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<ISetting>.Enumerator enumerator = default(List<ISetting>.Enumerator);
		object obj = default(object);
		List<SettingData>.Enumerator enumerator3 = default(List<SettingData>.Enumerator);
		List<SettingData>.Enumerator enumerator4 = default(List<SettingData>.Enumerator);
		string text = default(string);
		string text2 = default(string);
		object obj2 = default(object);
		nint num2 = default(nint);
		nint num3 = default(nint);
		object obj4 = default(object);
		while (enumerator.MoveNext())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 169 Invalid \"Jump target not found in method: 0x180A48FC4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 181 Invalid \"Jump target not found in method: 0x180A48FBF\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			obj = obj;
			List<ISetting>.Enumerator enumerator2 = (List<ISetting>.Enumerator)enumerator3;
			while (true)
			{
				if (enumerator4.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 222 Invalid \"Jump target not found in method: 0x180A48FB4\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 234 Invalid \"Jump target not found in method: 0x180A48FAF\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (!((string)text._stringLength == text2))
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_-C8 (System.String)+18]");
					if (0 == (nint)obj2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_-C8 (System.String)+18]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18004DF40");
							object obj3 = 1;
						}
						else
						{
							string message = "SGSettings: Unknown data type for path '" + (string)text._stringLength + "'. Ignoring.";
							nint num = (nint)typeof(Debug);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v684 @ rcx_v55 (Il2CppClass<UnityEngine.Debug>)+E4]");
							flag = (nint)0 != 0;
							Debug.LogError(message);
							object obj3 = 0;
						}
						enumerator4.Dispose();
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 335 Invalid \"Jump target not found in method: 0x180A48FAA\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 350 Invalid \"Jump target not found in method: 0x180A48F72\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 368 Invalid \"Jump target not found in method: 0x180A48F78\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 386 Invalid \"Jump target not found in method: 0x180A48F7D\"");
					string text3 = ((Enum)(&num2)).ToString();
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 411 Invalid \"Jump target not found in method: 0x180A48F82\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 430 Invalid \"Jump target not found in method: 0x180A48F87\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					string text4 = ((Enum)(&num3)).ToString();
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 458 Invalid \"Jump target not found in method: 0x180A48F8C\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 477 Invalid \"Jump target not found in method: 0x180A48F91\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 498 Invalid \"Jump target not found in method: 0x180A48F96\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 517 Invalid \"Jump target not found in method: 0x180A48F9B\"");
					string text5 = ((Enum)(&enumerator2)).ToString();
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 542 Invalid \"Jump target not found in method: 0x180A48FA0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 561 Invalid \"Jump target not found in method: 0x180A48FA5\"");
					string message2 = "SGSettings: Data type conflict for path '" + (string)text._stringLength + "'. Saved setting type is '" + text3 + "' but setting type is '" + text4 + "'. Maybe you have two settings with the same ID '" + (string)obj4 + "' OR you have some settings saved with that type? Will skip loading data from type '" + text5 + "'. Solutions: Check for duplicate IDs and delete any saved settings data. Then try again.";
					nint num4 = (nint)typeof(Debug);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ rcx_v50 (Il2CppClass<UnityEngine.Debug>)+E4]");
					flag = (nint)0 != 0;
					Debug.LogWarning(message2);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v151 @ stack_-C8 (System.String)+18]");
					obj = 0;
					num3 = (nint)typeof(SettingData.DataType);
					num2 = (nint)typeof(SettingData.DataType);
					continue;
				}
				enumerator4.Dispose();
				break;
			}
		}
		enumerator.Dispose();
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 644 Invalid \"Jump target not found in method: 0x180A48F6C\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 656 Invalid \"Jump target not found in method: 0x180A48F6C\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<SettingData>.Enumerator enumerator5 = default(List<SettingData>.Enumerator);
		List<ISetting>.Enumerator enumerator6 = default(List<ISetting>.Enumerator);
		string text6 = default(string);
		while (true)
		{
			bool flag2 = enumerator5.MoveNext();
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 682 Invalid \"Jump target not found in method: 0x180A48F15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			while (true)
			{
				if (enumerator6.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 730 Invalid \"Jump target not found in method: 0x180A48FD5\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 743 Invalid \"Jump target not found in method: 0x180A48FD0\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ stack_20_v1+10]");
					if ((string)0 == text6)
					{
						enumerator6.Dispose();
						break;
					}
					continue;
				}
				enumerator6.Dispose();
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 813 Invalid \"Jump target not found in method: 0x180A48FE0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 824 Invalid \"Jump target not found in method: 0x180A48EC5\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v52 @ stack_20_v1+18]");
				if ((nint)0 > (nint)8)
				{
					break;
				}
				return;
			}
		}
	}
}
