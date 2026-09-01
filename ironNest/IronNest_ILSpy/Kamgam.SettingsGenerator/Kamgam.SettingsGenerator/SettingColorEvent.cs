using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingColorEvent : SettingEvent<Color>
{
	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		//IL_007f: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+40]");
		if ((nint)0 == 0)
		{
			SettingData.DataType[] array = new SettingData.DataType[2];
			if (array.Length > 0)
			{
				_ = 5;
				if (array.Length > 1)
				{
					_ = 8;
					goto IL_0072;
				}
			}
			return (SettingData.DataType[])(object)new IndexOutOfRangeException();
		}
		goto IL_0072;
		IL_0072:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+40]");
		return (SettingData.DataType[])0;
	}

	public unsafe override void TriggerEvent()
	{
		//IL_002c: Expected O, but got I
		//IL_019c: Expected O, but got I
		//IL_00f7: Expected O, but got I
		//IL_0160: Expected O, but got Ref
		//IL_0160: Expected O, but got I
		//IL_010d: Expected O, but got Ref
		//IL_0121: Expected O, but got I
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+30]");
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
		object obj3 = default(object);
		if ((nint)obj != 5)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			if ((nint)obj2 != 8)
			{
				return;
			}
			SettingsProvider settingsProvider2 = base.SettingsProvider;
			Settings settings2 = settingsProvider2.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+30]");
			SettingColorOption colorOption = settings2.GetColorOption((string)0);
			Color colorValue = colorOption.GetColorValue((Color)(&obj3));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
			obj3 = 0;
		}
		else
		{
			SettingsProvider settingsProvider3 = base.SettingsProvider;
			Settings settings3 = settingsProvider3.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+30]");
			SettingColor color = settings3.GetColor((string)0);
			Color value = color.GetValue();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+48]");
		if ((nint)0 != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingColorEvent)+48]");
			((UnityEvent<Color>)0).Invoke((Color)(&obj3));
		}
	}
}
