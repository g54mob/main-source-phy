using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingBoolEvent : SettingEvent<bool>
{
	public static string FalseStringValue0;

	public static string FalseStringValue1;

	public static int FalseIntValue;

	public static float FalseFloatValue;

	public static Color FalseColorValue;

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		//IL_0065: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+40]");
		if ((nint)0 == 0)
		{
			SettingData.DataType[] array = new SettingData.DataType[5]
			{
				SettingData.DataType.Bool,
				SettingData.DataType.Int,
				SettingData.DataType.Float,
				SettingData.DataType.Color,
				SettingData.DataType.String
			};
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+40]");
		return (SettingData.DataType[])0;
	}

	public unsafe override void TriggerEvent()
	{
		//IL_002c: Expected O, but got I
		//IL_0386: Expected O, but got I
		//IL_01a1: Expected O, but got I
		//IL_0308: Expected O, but got I
		//IL_0507: Expected O, but got I
		//IL_029b: Expected O, but got I
		//IL_0331: Expected O, but got I4
		//IL_04e0: Invalid comparison between O and F4
		//IL_020a: Expected O, but got I
		//IL_0433: Expected O, but got I
		//IL_016c: Expected O, but got I
		//IL_024d: Invalid comparison between F4 and I4
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+30]");
		if (!settings.HasActiveID((string)0))
		{
			return;
		}
		ISetting setting = GetSetting();
		if (setting == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		UnityEvent<bool> unityEvent;
		bool flag2;
		bool flag5;
		if ((nint)obj != 3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			if ((nint)obj2 == 1)
			{
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				Settings settings2 = settingsProvider2.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+30]");
				SettingInt settingInt = settings2.GetInt((string)0);
				int value = settingInt.GetValue();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+48]");
				unityEvent = (UnityEvent<bool>)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+48]");
				if ((nint)0 == 0)
				{
					return;
				}
				object obj3 = value - FalseIntValue;
				bool flag = obj3 == null;
				flag2 = !flag;
				goto IL_04bb;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj4 = default(object);
			if ((nint)obj4 != 2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj5 = default(object);
				if ((nint)obj5 == 5)
				{
					SettingsProvider settingsProvider3 = base.SettingsProvider;
					Settings settings3 = settingsProvider3.Settings;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+30]");
					SettingColor color = settings3.GetColor((string)0);
					float num = color.GetValue().r - (float)FalseColorValue;
					object obj7 = default(object);
					object obj6 = obj7 - obj7;
					object obj8 = obj7 - obj7;
					object obj9 = obj7 - obj7;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+48]");
					unityEvent = (UnityEvent<bool>)0;
					object obj10 = obj6 * obj6;
					float num2 = num * num;
					object obj11 = obj8 * obj8;
					object obj12 = obj9 * obj9;
					float num3 = (float)obj10 + num2;
					float num4 = num3 + (float)obj11;
					float num5 = num4 + (float)obj12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+48]");
					if ((nint)0 == 0)
					{
						return;
					}
					bool flag3 = 9.9999994E-11f < num5;
					float num6 = 9.9999994E-11f - num5;
					bool flag4 = num6 == 0f;
					flag2 = flag3 | flag4;
					goto IL_04bb;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj13 = default(object);
				if ((nint)obj13 != 4)
				{
					return;
				}
				SettingsProvider settingsProvider4 = base.SettingsProvider;
				Settings settings4 = settingsProvider4.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+30]");
				SettingString settingString = settings4.GetString((string)0);
				string value2 = settingString.GetValue();
				flag5 = value2 != FalseStringValue0;
				if (flag5)
				{
					flag5 = value2 != FalseStringValue1;
				}
			}
			else
			{
				SettingsProvider settingsProvider5 = base.SettingsProvider;
				Settings settings5 = settingsProvider5.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+30]");
				SettingFloat settingFloat = settings5.GetFloat((string)0);
				float value3 = settingFloat.GetValue();
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180A30B8Bh\"");
				object obj14 = default(object);
				flag5 = ((obj14 != (object)FalseFloatValue) ? true : false);
			}
		}
		else
		{
			SettingsProvider settingsProvider6 = base.SettingsProvider;
			Settings settings6 = settingsProvider6.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+30]");
			SettingBool settingBool = settings6.GetBool((string)0);
			flag5 = settingBool.GetValue();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+48]");
		unityEvent = (UnityEvent<bool>)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingBoolEvent)+48]");
		if ((nint)0 != 0)
		{
			flag2 = flag5;
			goto IL_04bb;
		}
		return;
		IL_04bb:
		unityEvent.Invoke((byte)(&flag2) != 0);
	}

	static SettingBoolEvent()
	{
		//IL_003a: Expected O, but got I
		FalseStringValue0 = "";
		FalseStringValue1 = null;
		FalseIntValue = 0;
		FalseFloatValue = 0f;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D80]");
		FalseColorValue = (Color)0;
	}
}
