using Cpp2ILInjected;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingStringEvent : SettingEvent<string>
{
	public string FloatFormat = "{0:0.00}";

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		//IL_0065: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+40]");
		if ((nint)0 == 0)
		{
			SettingData.DataType[] array = new SettingData.DataType[4]
			{
				SettingData.DataType.String,
				SettingData.DataType.Bool,
				SettingData.DataType.Int,
				SettingData.DataType.Float
			};
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+40]");
		return (SettingData.DataType[])0;
	}

	public override void TriggerEvent()
	{
		//IL_002c: Expected O, but got I
		//IL_0332: Expected O, but got I
		//IL_0294: Expected O, but got I
		//IL_0382: Expected O, but got I
		//IL_01fb: Expected O, but got I
		//IL_02f6: Expected O, but got I
		//IL_0258: Expected O, but got I
		//IL_0145: Expected O, but got I
		//IL_0156: Expected I, but got O
		//IL_01bf: Expected O, but got I
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+30]");
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
		if ((nint)obj != 4)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			if ((nint)obj2 != 3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj3 = default(object);
				if ((nint)obj3 != 1)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj4 = default(object);
					if ((nint)obj4 == 2)
					{
						SettingsProvider settingsProvider2 = base.SettingsProvider;
						Settings settings2 = settingsProvider2.Settings;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+30]");
						SettingFloat settingFloat = settings2.GetFloat((string)0);
						nint num = (nint)settingFloat;
						float value = settingFloat.GetValue();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							object arg2 = default(object);
							string arg = string.Format(FloatFormat, arg2);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
							((UnityEvent<string>)0).Invoke(arg);
						}
					}
				}
				else
				{
					SettingsProvider settingsProvider3 = base.SettingsProvider;
					Settings settings3 = settingsProvider3.Settings;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+30]");
					SettingInt settingInt = settings3.GetInt((string)0);
					int value2 = settingInt.GetValue();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
					if ((nint)0 != 0)
					{
						int num2 = default(int);
						string arg3 = num2.ToString();
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
						((UnityEvent<string>)0).Invoke(arg3);
					}
				}
			}
			else
			{
				SettingsProvider settingsProvider4 = base.SettingsProvider;
				Settings settings4 = settingsProvider4.Settings;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+30]");
				SettingBool settingBool = settings4.GetBool((string)0);
				bool value3 = settingBool.GetValue();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
				if ((nint)0 != 0)
				{
					bool flag = default(bool);
					string arg4 = flag.ToString();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
					((UnityEvent<string>)0).Invoke(arg4);
				}
			}
		}
		else
		{
			SettingsProvider settingsProvider5 = base.SettingsProvider;
			Settings settings5 = settingsProvider5.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+30]");
			SettingString settingString = settings5.GetString((string)0);
			string value4 = settingString.GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingStringEvent)+48]");
				((UnityEvent<string>)0).Invoke(value4);
			}
		}
	}
}
