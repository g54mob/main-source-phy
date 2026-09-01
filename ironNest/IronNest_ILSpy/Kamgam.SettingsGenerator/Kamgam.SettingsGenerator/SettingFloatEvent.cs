using Cpp2ILInjected;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingFloatEvent : SettingEvent<float>
{
	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		//IL_0065: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+40]");
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
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+40]");
		return (SettingData.DataType[])0;
	}

	public unsafe override void TriggerEvent()
	{
		//IL_002c: Expected O, but got I
		//IL_0294: Expected O, but got I
		//IL_016b: Expected O, but got I
		//IL_020e: Expected O, but got I
		//IL_02c1: Expected F4, but got Ref
		//IL_0234: Expected O, but got I
		//IL_01c7: Expected O, but got I
		//IL_0145: Expected O, but got I
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+30]");
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
		UnityEvent<float> unityEvent;
		SettingColorOption settingColorOption;
		if ((nint)obj != 1)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			if ((nint)obj2 == 2)
			{
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				Settings settings2 = settingsProvider2.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+30]");
				SettingFloat settingFloat = settings2.GetFloat((string)0);
				float value = settingFloat.GetValue();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+48]");
				unityEvent = (UnityEvent<float>)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+48]");
				if ((nint)0 == 0)
				{
					return;
				}
				goto IL_02b4;
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
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+30]");
				settingColorOption = settings3.GetColorOption((string)0);
			}
			else
			{
				SettingsProvider settingsProvider4 = base.SettingsProvider;
				Settings settings4 = settingsProvider4.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+30]");
				SettingOption option = settings4.GetOption((string)0);
				settingColorOption = (SettingColorOption)(object)option;
			}
		}
		else
		{
			SettingsProvider settingsProvider5 = base.SettingsProvider;
			Settings settings5 = settingsProvider5.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+30]");
			SettingInt settingInt = settings5.GetInt((string)0);
			settingColorOption = (SettingColorOption)(object)settingInt;
		}
		int value2 = settingColorOption.GetValue();
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+48]");
		unityEvent = (UnityEvent<float>)0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingFloatEvent)+48]");
		if ((nint)0 != 0)
		{
			goto IL_02b4;
		}
		return;
		IL_02b4:
		object obj5 = default(object);
		unityEvent.Invoke((nint)(&obj5));
	}
}
