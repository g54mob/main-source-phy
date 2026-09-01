using System;
using Cpp2ILInjected;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingIntEvent : SettingEvent<int>
{
	public enum FloatToIntConversion
	{
		Round,
		Ceil,
		Floor
	}

	public FloatToIntConversion FloatToInt;

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		//IL_0065: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+40]");
		if ((nint)0 == 0)
		{
			SettingData.DataType[] array = new SettingData.DataType[4]
			{
				SettingData.DataType.Int,
				SettingData.DataType.Float,
				SettingData.DataType.Option,
				SettingData.DataType.ColorOption
			};
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+40]");
		return (SettingData.DataType[])0;
	}

	public unsafe override void TriggerEvent()
	{
		//IL_002c: Expected O, but got I
		//IL_036e: Expected O, but got I
		//IL_016b: Expected O, but got I
		//IL_0220: Expected O, but got I
		//IL_0231: Expected I, but got O
		//IL_01d9: Expected O, but got I
		//IL_030e: Expected O, but got I
		//IL_0145: Expected O, but got I
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+30]");
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
		SettingColorOption settingColorOption;
		if ((nint)obj != 1)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			if ((nint)obj2 == 2)
			{
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				Settings settings2 = settingsProvider2.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+30]");
				SettingFloat settingFloat = settings2.GetFloat((string)0);
				nint num = (nint)settingFloat;
				float value = settingFloat.GetValue();
				FloatToIntConversion floatToInt = FloatToInt;
				bool flag = FloatToInt == FloatToIntConversion.Round;
				if (flag)
				{
					goto IL_02ef;
				}
				floatToInt--;
				if (!flag)
				{
					if (floatToInt != FloatToIntConversion.Ceil)
					{
						goto IL_02ef;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
					double num2 = Math.Floor(0.0);
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
					double num3 = Math.Ceiling(0.0);
				}
				goto IL_02fe;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj3 = default(object);
			if ((nint)obj3 != 7)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj4 = default(object);
				if ((nint)obj4 != 8)
				{
					return;
				}
				SettingsProvider settingsProvider3 = base.SettingsProvider;
				Settings settings3 = settingsProvider3.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+30]");
				settingColorOption = settings3.GetColorOption((string)0);
			}
			else
			{
				SettingsProvider settingsProvider4 = base.SettingsProvider;
				Settings settings4 = settingsProvider4.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+30]");
				SettingOption option = settings4.GetOption((string)0);
				settingColorOption = (SettingColorOption)(object)option;
			}
		}
		else
		{
			SettingsProvider settingsProvider5 = base.SettingsProvider;
			Settings settings5 = settingsProvider5.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+30]");
			SettingInt settingInt = settings5.GetInt((string)0);
			settingColorOption = (SettingColorOption)(object)settingInt;
		}
		int value2 = settingColorOption.GetValue();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+48]");
		UnityEvent<int> unityEvent = (UnityEvent<int>)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+48]");
		if ((nint)0 != 0)
		{
			goto IL_0190;
		}
		return;
		IL_02fe:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+48]");
		unityEvent = (UnityEvent<int>)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingIntEvent)+48]");
		if ((nint)0 == 0)
		{
			return;
		}
		goto IL_0190;
		IL_0190:
		object obj5 = default(object);
		unityEvent.Invoke((int)(&obj5));
		return;
		IL_02ef:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		goto IL_02fe;
	}
}
