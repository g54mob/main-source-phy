using System;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public class SettingKeyCombinationEvent : SettingEvent<KeyCombination>
{
	[NonSerialized]
	protected KeyCombination _combo;

	public UnityEvent<KeyCombination> OnDown;

	public UnityEvent<KeyCombination> OnUp;

	public UnityEvent<KeyCombination> OnHold;

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		//IL_0057: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+40]");
		if ((nint)0 == 0)
		{
			SettingData.DataType[] array = new SettingData.DataType[1];
			if (array.Length <= 0)
			{
				return (SettingData.DataType[])(object)new IndexOutOfRangeException();
			}
			_ = 6;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+40]");
		return (SettingData.DataType[])0;
	}

	public unsafe override void TriggerEvent()
	{
		//IL_002c: Expected O, but got I
		//IL_00d0: Expected O, but got I
		//IL_0120: Expected O, but got Ref
		//IL_0120: Expected O, but got I
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+30]");
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
		if ((nint)obj == 6)
		{
			SettingsProvider settingsProvider2 = base.SettingsProvider;
			Settings settings2 = settingsProvider2.Settings;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+30]");
			SettingKeyCombination keyCombination = settings2.GetKeyCombination((string)0);
			KeyCombination value = keyCombination.GetValue();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+48]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+48]");
				object obj2 = default(object);
				((UnityEvent<KeyCombination>)0).Invoke((KeyCombination)(&obj2));
			}
			_combo = value;
		}
	}

	public unsafe void Update()
	{
		//IL_008a: Expected I4, but got O
		//IL_0188: Expected I4, but got O
		//IL_0286: Expected I4, but got O
		//IL_00d5: Expected O, but got Ref
		//IL_01d3: Expected O, but got Ref
		//IL_02d1: Expected O, but got Ref
		KeyCombination combo = default(KeyCombination);
		if (OnDown != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
			if (!InputUtils.GetUniversalKeyDown(UniversalKeyCode.None))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
					if ((nint)0 != 1)
					{
						goto IL_00e4;
					}
				}
			}
			if (InputUtils.GetUniversalKeyDown((UniversalKeyCode)_combo) && OnDown != null)
			{
				OnDown.Invoke((KeyCombination)(&combo));
				combo = _combo;
			}
		}
		goto IL_00e4;
		IL_01e2:
		if (OnHold == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
		if (!InputUtils.GetUniversalKey(UniversalKeyCode.None))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
				if ((nint)0 != 1)
				{
					return;
				}
			}
		}
		if (InputUtils.GetUniversalKey((UniversalKeyCode)_combo) && OnHold != null)
		{
			OnHold.Invoke((KeyCombination)(&combo));
		}
		return;
		IL_00e4:
		if (OnUp != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
			if (!InputUtils.GetUniversalKeyUp(UniversalKeyCode.None))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.SettingKeyCombinationEvent)+54]");
					if ((nint)0 != 1)
					{
						goto IL_01e2;
					}
				}
			}
			if (InputUtils.GetUniversalKeyUp((UniversalKeyCode)_combo) && OnUp != null)
			{
				OnUp.Invoke((KeyCombination)(&combo));
				combo = _combo;
			}
		}
		goto IL_01e2;
	}
}
