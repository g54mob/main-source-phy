using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class DropDownUGUIResolver : SettingResolver, ISettingResolver
{
	protected DropDownUGUI dropDownUGUI;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	protected List<string> _localizedOptionLabels;

	public DropDownUGUI DropDownUGUI
	{
		get
		{
			if (this.dropDownUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				DropDownUGUI dropDownUGUI = default(DropDownUGUI);
				this.dropDownUGUI = dropDownUGUI;
			}
			return this.dropDownUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_046b: Expected O, but got I
		//IL_040f: Expected I, but got O
		//IL_043a: Expected I, but got O
		//IL_0133: Expected I, but got O
		//IL_0257: Expected I, but got O
		//IL_028e: Expected I, but got O
		//IL_017e: Expected I, but got O
		//IL_02ee: Expected I, but got O
		//IL_0322: Expected I, but got O
		//IL_01fa: Expected O, but got I4
		//IL_036b: Expected I, but got O
		base.Start();
		DropDownUGUI dropDownUGUI = DropDownUGUI;
		bool flag = (object)dropDownUGUI == null;
		DropDownUGUIResolver language = this;
		Delegate obj = default(Delegate);
		nint num = default(nint);
		NullReferenceException ex;
		if (!flag)
		{
			DropDownUGUI.OnSelectionChangedDelegate b = onSelectionChanged;
			obj = Delegate.Combine(dropDownUGUI.OnSelectionChanged, b);
			if ((object)obj == null)
			{
				dropDownUGUI.OnSelectionChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(DropDownUGUI.OnSelectionChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				num = (nint)typeof(DropDownUGUI.OnSelectionChangedDelegate);
				if (flag3)
				{
					goto IL_045e;
				}
				dropDownUGUI.OnSelectionChanged = (DropDownUGUI.OnSelectionChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(DropDownUGUI.OnSelectionChangedDelegate);
				Delegate obj3 = null;
				if (!flag4)
				{
					obj3 = obj;
				}
				bool flag5 = (object)obj3 == null;
				num = (nint)typeof(DropDownUGUI.OnSelectionChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				language = (DropDownUGUIResolver)(object)typeof(DropDownUGUI.OnSelectionChangedDelegate);
				if (flag5)
				{
					goto IL_046c;
				}
			}
			if (!(LocalizationProvider != null))
			{
				goto IL_01ff;
			}
			language = (DropDownUGUIResolver)(object)LocalizationProvider;
			bool flag6 = (object)LocalizationProvider == null;
			num = unchecked((nint)null);
			if (!flag6)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
				object obj4 = default(object);
				if (obj4 == null)
				{
					goto IL_01ff;
				}
				bool flag7 = (object)LocalizationProvider == null;
				num = unchecked((nint)null);
				language = (DropDownUGUIResolver)(object)LocalizationProvider;
				if (!flag7)
				{
					ILocalization localization = LocalizationProvider.GetLocalization();
					OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
					bool flag8 = localization == null;
					num = 0;
					language = (DropDownUGUIResolver)(object)onLanguageChangedDelegate;
					if (!flag8)
					{
						((DropDownUGUIResolver)23).onLanguageChanged((string)(object)typeof(ILocalization));
						goto IL_01ff;
					}
				}
			}
		}
		goto IL_03b1;
		IL_01ff:
		SettingData.DataType[] array = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, array))
		{
			return;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		bool flag9 = (object)settingsProvider == null;
		num = (nint)array;
		language = this;
		if (!flag9)
		{
			Settings settings = settingsProvider.Settings;
			bool flag10 = (object)settings == null;
			num = (nint)array;
			language = (DropDownUGUIResolver)(object)settingsProvider;
			if (!flag10)
			{
				if (!settings.HasActiveID(ID))
				{
					return;
				}
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				bool flag11 = (object)settingsProvider2 == null;
				num = unchecked((nint)null);
				language = this;
				if (!flag11)
				{
					Settings settings2 = settingsProvider2.Settings;
					bool flag12 = (object)settings2 == null;
					num = unchecked((nint)null);
					language = (DropDownUGUIResolver)(object)settingsProvider2;
					if (!flag12)
					{
						ISetting setting = settings2.GetSetting(ID);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v475 @ r8_v13 (Il2CppClass<Kamgam.SettingsGenerator.DropDownUGUIResolver>)+240]");
						Action action = new Action(this, (IntPtr)0);
						nint num2 = (nint)this;
						bool flag13 = setting == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v475 @ r8_v13 (Il2CppClass<Kamgam.SettingsGenerator.DropDownUGUIResolver>)+240]");
						num = 0;
						language = (DropDownUGUIResolver)(object)action;
						if (!flag13)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							return;
						}
					}
				}
			}
		}
		goto IL_03b1;
		IL_03b1:
		ex = new NullReferenceException();
		goto IL_046c;
		IL_045e:
		((DropDownUGUIResolver)(object)obj).onLanguageChanged((string)num);
		return;
		IL_046c:
		((DropDownUGUIResolver)(object)ex).onLanguageChanged((string)(object)language);
		goto IL_045e;
	}

	public override void OnDestroy()
	{
		//IL_004a: Expected I, but got O
		//IL_018b: Expected I, but got O
		//IL_02eb: Expected O, but got I
		//IL_028f: Expected I, but got O
		//IL_01d6: Expected I, but got O
		//IL_02ba: Expected I, but got O
		base.OnDestroy();
		DropDownUGUI dropDownUGUI = DropDownUGUI;
		nint num;
		DropDownUGUIResolver language;
		Delegate obj = default(Delegate);
		NullReferenceException ex;
		if (dropDownUGUI != null)
		{
			DropDownUGUI dropDownUGUI2 = DropDownUGUI;
			bool flag = (object)dropDownUGUI2 == null;
			num = unchecked((nint)null);
			language = this;
			if (flag)
			{
				goto IL_024e;
			}
			DropDownUGUI.OnSelectionChangedDelegate value = onSelectionChanged;
			obj = Delegate.Remove(dropDownUGUI2.OnSelectionChanged, value);
			if ((object)obj == null)
			{
				dropDownUGUI2.OnSelectionChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(DropDownUGUI.OnSelectionChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				num = (nint)typeof(DropDownUGUI.OnSelectionChangedDelegate);
				if (flag3)
				{
					goto IL_02de;
				}
				dropDownUGUI2.OnSelectionChanged = (DropDownUGUI.OnSelectionChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(DropDownUGUI.OnSelectionChangedDelegate);
				Delegate obj3 = null;
				if (!flag4)
				{
					obj3 = obj;
				}
				bool flag5 = (object)obj3 == null;
				num = (nint)typeof(DropDownUGUI.OnSelectionChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				language = (DropDownUGUIResolver)(object)typeof(DropDownUGUI.OnSelectionChangedDelegate);
				if (flag5)
				{
					goto IL_02ec;
				}
			}
		}
		if (!(LocalizationProvider != null))
		{
			return;
		}
		language = (DropDownUGUIResolver)(object)LocalizationProvider;
		bool flag6 = (object)LocalizationProvider == null;
		num = unchecked((nint)null);
		if (!flag6)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
			object obj4 = default(object);
			if (obj4 == null)
			{
				return;
			}
			bool flag7 = (object)LocalizationProvider == null;
			num = unchecked((nint)null);
			language = (DropDownUGUIResolver)(object)LocalizationProvider;
			if (!flag7)
			{
				ILocalization localization = LocalizationProvider.GetLocalization();
				OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
				bool flag8 = localization == null;
				num = 0;
				language = (DropDownUGUIResolver)(object)onLanguageChangedDelegate;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
					return;
				}
			}
		}
		goto IL_024e;
		IL_02ec:
		((DropDownUGUIResolver)(object)ex).onLanguageChanged((string)(object)language);
		goto IL_02de;
		IL_02de:
		((DropDownUGUIResolver)(object)obj).onLanguageChanged((string)num);
		return;
		IL_024e:
		ex = new NullReferenceException();
		goto IL_02ec;
	}

	protected void onLanguageChanged(string language)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.DropDownUGUIResolver>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.DropDownUGUIResolver>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected void onSelectionChanged(int selectedIndex)
	{
		//IL_016a: Expected I, but got O
		//IL_017a: Expected O, but got I
		//IL_018a: Expected O, but got I
		if (stopPropagation)
		{
			return;
		}
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, allowedTypes))
		{
			return;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (settings.HasActiveID(ID))
		{
			SettingsProvider settingsProvider2 = base.SettingsProvider;
			Settings settings2 = settingsProvider2.Settings;
			SettingOption option = settings2.GetOption(ID);
			if (option == null)
			{
				SettingsProvider settingsProvider3 = base.SettingsProvider;
				Settings settings3 = settingsProvider3.Settings;
				settings3.GetInt(ID)?.SetValue(selectedIndex);
				return;
			}
			nint num = (nint)option;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+4E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingOption>)+4F0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v99 @ rax_v14 (should have been resolved before IL gen)");
		}
	}

	public override void Refresh()
	{
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, allowedTypes))
		{
			return;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (!settings.HasActiveID(ID))
		{
			return;
		}
		_ = 1;
		SettingResolver settingResolver = default(SettingResolver);
		SettingsProvider settingsProvider2 = settingResolver.SettingsProvider;
		if ((object)settingsProvider2 != null)
		{
			Settings settings2 = settingsProvider2.Settings;
			if ((object)settings2 != null)
			{
				SettingOption option = settings2.GetOption(settingResolver.ID);
				if (option == null)
				{
					SettingsProvider settingsProvider3 = settingResolver.SettingsProvider;
					if ((object)settingsProvider3 == null)
					{
						throw new NullReferenceException();
					}
					Settings settings3 = settingsProvider3.Settings;
					SettingInt settingInt = settings3.GetInt(settingResolver.ID);
					if (settingInt != null)
					{
						DropDownUGUI dropDownUGUI = ((DropDownUGUIResolver)settingResolver).DropDownUGUI;
						int value = settingInt.GetValue();
						if ((object)dropDownUGUI == null)
						{
							throw new NullReferenceException();
						}
						dropDownUGUI.SelectedIndex = value;
					}
					_ = 0;
				}
				else
				{
					((DropDownUGUIResolver)settingResolver).refreshOptions();
					DropDownUGUI dropDownUGUI2 = ((DropDownUGUIResolver)settingResolver).DropDownUGUI;
					int value2 = option.GetValue();
					if ((object)dropDownUGUI2 == null)
					{
						throw new NullReferenceException();
					}
					dropDownUGUI2.SelectedIndex = value2;
					_ = 0;
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	protected void refreshOptions()
	{
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (!settings.HasActiveID(ID))
		{
			return;
		}
		SettingsProvider settingsProvider2 = base.SettingsProvider;
		Settings settings2 = settingsProvider2.Settings;
		SettingOption option = settings2.GetOption(ID);
		DropDownUGUI dropDownUGUI2;
		IList<string> options2;
		if (!option.HasOptions())
		{
			ILocalization localization = LocalizationProvider.GetLocalization();
			DropDownUGUI dropDownUGUI = DropDownUGUI;
			List<string> options = dropDownUGUI.GetOptions();
			List<string> list = options;
			ILocalization localization2 = localization;
		}
		else
		{
			List<string> optionLabels = option.GetOptionLabels();
			List<string> list2 = new List<string>(optionLabels);
			if (!(LocalizationProvider != null) || !LocalizationProvider.HasLocalization())
			{
				dropDownUGUI2 = DropDownUGUI;
				options2 = list2;
				goto IL_01d1;
			}
			ILocalization localization3 = LocalizationProvider.GetLocalization();
			List<string> list = list2;
			ILocalization localization2 = localization3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18005ACF0");
		dropDownUGUI2 = DropDownUGUI;
		options2 = _localizedOptionLabels;
		goto IL_01d1;
		IL_01d1:
		dropDownUGUI2.SetOptions(options2);
	}

	public DropDownUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[2];
		_ = 7;
		_ = 1;
		supportedDataTypes = array;
		_localizedOptionLabels = new List<string>(3);
		((MonoBehaviour)this)._002Ector();
	}
}
