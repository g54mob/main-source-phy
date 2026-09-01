using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class OptionsButtonUGUIResolver : SettingResolver
{
	protected OptionsButtonUGUI optionsButtonUGUI;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	protected List<string> _localizedOptionLabels;

	public OptionsButtonUGUI OptionsButtonUGUI
	{
		get
		{
			if (this.optionsButtonUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				OptionsButtonUGUI optionsButtonUGUI = default(OptionsButtonUGUI);
				this.optionsButtonUGUI = optionsButtonUGUI;
			}
			return this.optionsButtonUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public unsafe override void Start()
	{
		//IL_0199: Expected I, but got O
		//IL_01d1: Expected O, but got I
		//IL_058a: Expected O, but got I
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Expected O, but got Unknown
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_03ec: Expected I, but got O
		//IL_0428: Expected I, but got O
		//IL_0517: Expected I, but got O
		base.Start();
		OptionsButtonUGUI optionsButtonUGUI = OptionsButtonUGUI;
		bool flag = (object)optionsButtonUGUI == null;
		OptionsButtonUGUIResolver optionsButtonUGUIResolver = this;
		OptionsButtonUGUI optionsButtonUGUI2;
		Func<string, string> func;
		OptionsButtonUGUIResolver typeFromHandle;
		OnLanguageChangedDelegate onLanguageChangedDelegate;
		nint num;
		NullReferenceException ex;
		if (!flag)
		{
			OptionsButtonUGUI.OnValueChangedDelegate b = onValueChanged;
			Delegate obj = Delegate.Combine(optionsButtonUGUI.OnValueChanged, b);
			if ((object)obj == null)
			{
				optionsButtonUGUI.OnValueChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(OptionsButtonUGUI.OnValueChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				onLanguageChangedDelegate = null;
				num = 0;
				typeFromHandle = (OptionsButtonUGUIResolver)(object)typeof(OptionsButtonUGUI.OnValueChangedDelegate);
				if (flag3)
				{
					goto IL_06ab;
				}
				optionsButtonUGUI.OnValueChanged = (OptionsButtonUGUI.OnValueChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(OptionsButtonUGUI.OnValueChangedDelegate);
				Delegate obj3 = null;
				if (!flag4)
				{
					obj3 = obj;
				}
				bool flag5 = (object)obj3 == null;
				onLanguageChangedDelegate = null;
				num = 0;
				ex = (NullReferenceException)(object)obj;
				optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)typeof(OptionsButtonUGUI.OnValueChangedDelegate);
				if (flag5)
				{
					goto IL_06b6;
				}
			}
			optionsButtonUGUI2 = OptionsButtonUGUI;
			bool flag6 = (object)LocalizationProvider == null;
			onLanguageChangedDelegate = null;
			num = 0;
			optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)LocalizationProvider;
			if (!flag6)
			{
				ILocalization localization = LocalizationProvider.GetLocalization();
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ rax_v17+8]");
				func = new Func<string, string>(localization, (IntPtr)0);
				bool flag7 = localization == null;
				onLanguageChangedDelegate = null;
				num = 0;
				optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)typeof(Func<string, string>);
				if (!flag7)
				{
					nint num2 = (nint)localization;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v432 @ r9_v5 (Il2CppClass<Kamgam.LocalizationForSettings.ILocalization>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_020d;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v432 @ r9_v5 (Il2CppClass<Kamgam.LocalizationForSettings.ILocalization>)+B0]");
					object obj4 = 0;
					OptionsButtonUGUI.OnValueChangedDelegate onValueChangedDelegate = null;
					while (true)
					{
						object obj5 = onValueChangedDelegate + onValueChangedDelegate;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ r8_v21+v450 @ rax_v48*8]");
						if (0 == (nint)typeof(ILocalization))
						{
							break;
						}
						onValueChangedDelegate = (OptionsButtonUGUI.OnValueChangedDelegate)(onValueChangedDelegate + 1);
						OptionsButtonUGUI.OnValueChangedDelegate onValueChangedDelegate2 = onValueChangedDelegate;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v432 @ r9_v5 (Il2CppClass<Kamgam.LocalizationForSettings.ILocalization>)+12E]");
						if ((nint)onValueChangedDelegate2 < 0)
						{
							continue;
						}
						goto IL_020d;
					}
					object obj6 = onValueChangedDelegate + onValueChangedDelegate;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v445 @ r8_v21+8+v504 @ rcx_v44*8]");
					object obj7 = (nint)0 + (nint)18;
					object obj8 = obj7 << 4;
					object obj9 = obj8 + 312;
					object obj10 = obj9 + num2;
					goto IL_021c;
				}
			}
		}
		goto IL_05b8;
		IL_06b6:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		typeFromHandle = optionsButtonUGUIResolver;
		goto IL_06ab;
		IL_020d:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_021c;
		IL_038f:
		SettingData.DataType[] array = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, array))
		{
			return;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		bool flag8 = (object)settingsProvider == null;
		onLanguageChangedDelegate = null;
		num = (nint)array;
		optionsButtonUGUIResolver = this;
		if (!flag8)
		{
			Settings settings = settingsProvider.Settings;
			bool flag9 = (object)settings == null;
			onLanguageChangedDelegate = null;
			num = (nint)array;
			optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)settingsProvider;
			if (!flag9)
			{
				if (!settings.HasActiveID(ID))
				{
					return;
				}
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				bool flag10 = (object)settingsProvider2 == null;
				onLanguageChangedDelegate = null;
				num = 0;
				optionsButtonUGUIResolver = this;
				if (!flag10)
				{
					Settings settings2 = settingsProvider2.Settings;
					bool flag11 = (object)settings2 == null;
					onLanguageChangedDelegate = null;
					num = 0;
					optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)settingsProvider2;
					if (!flag11)
					{
						ISetting setting = settings2.GetSetting(ID);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v629 @ r8_v14 (Il2CppClass<Kamgam.SettingsGenerator.OptionsButtonUGUIResolver>)+240]");
						Action action = new Action(this, (IntPtr)0);
						nint num3 = (nint)this;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v629 @ r8_v14 (Il2CppClass<Kamgam.SettingsGenerator.OptionsButtonUGUIResolver>)+240]");
						num = 0;
						bool flag12 = setting == null;
						onLanguageChangedDelegate = null;
						optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)action;
						if (!flag12)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							Refresh();
							return;
						}
					}
				}
			}
		}
		goto IL_05b8;
		IL_021c:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ rax_v17+8]");
		num = 0;
		bool flag13 = (object)optionsButtonUGUI2 == null;
		onLanguageChangedDelegate = null;
		optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)func;
		if (!flag13)
		{
			optionsButtonUGUI2.OptionToTextFunc = func;
			if (!(LocalizationProvider != null))
			{
				goto IL_038f;
			}
			optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)LocalizationProvider;
			bool flag14 = (object)LocalizationProvider == null;
			onLanguageChangedDelegate = null;
			num = 0;
			if (!flag14)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
				object obj11 = default(object);
				if (obj11 == null)
				{
					goto IL_038f;
				}
				bool flag15 = (object)LocalizationProvider == null;
				onLanguageChangedDelegate = null;
				num = 0;
				optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)LocalizationProvider;
				if (!flag15)
				{
					ILocalization localization2 = LocalizationProvider.GetLocalization();
					OnLanguageChangedDelegate onLanguageChangedDelegate2 = onLanguageChanged;
					bool flag16 = localization2 == null;
					onLanguageChangedDelegate = null;
					num = (nint)__ldftn(OptionsButtonUGUIResolver.onLanguageChanged);
					optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)onLanguageChangedDelegate2;
					if (!flag16)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						goto IL_038f;
					}
				}
			}
		}
		goto IL_05b8;
		IL_05b8:
		ex = new NullReferenceException();
		goto IL_06b6;
		IL_06ab:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		OptionsButtonUGUI optionsButtonUGUI = OptionsButtonUGUI;
		OptionsButtonUGUIResolver optionsButtonUGUIResolver;
		Delegate obj = default(Delegate);
		OptionsButtonUGUIResolver language;
		NullReferenceException ex;
		if (optionsButtonUGUI != null)
		{
			OptionsButtonUGUI optionsButtonUGUI2 = OptionsButtonUGUI;
			bool flag = (object)optionsButtonUGUI2 == null;
			optionsButtonUGUIResolver = this;
			if (!flag)
			{
				OptionsButtonUGUI.OnValueChangedDelegate value = onValueChanged;
				obj = Delegate.Remove(optionsButtonUGUI2.OnValueChanged, value);
				if ((object)obj == null)
				{
					optionsButtonUGUI2.OnValueChanged = (OptionsButtonUGUI.OnValueChangedDelegate)obj;
				}
				else
				{
					bool flag2 = (object)obj.GetType() != typeof(OptionsButtonUGUI.OnValueChangedDelegate);
					Delegate obj2 = null;
					if (!flag2)
					{
						obj2 = obj;
					}
					bool flag3 = (object)obj2 == null;
					language = (OptionsButtonUGUIResolver)(object)typeof(OptionsButtonUGUI.OnValueChangedDelegate);
					if (flag3)
					{
						goto IL_0305;
					}
					optionsButtonUGUI2.OnValueChanged = (OptionsButtonUGUI.OnValueChangedDelegate)obj2;
					bool flag4 = (object)obj.GetType() != typeof(OptionsButtonUGUI.OnValueChangedDelegate);
					Delegate obj3 = null;
					if (!flag4)
					{
						obj3 = obj;
					}
					bool flag5 = (object)obj3 == null;
					ex = (NullReferenceException)(object)obj;
					optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)typeof(OptionsButtonUGUI.OnValueChangedDelegate);
					if (flag5)
					{
						goto IL_0313;
					}
				}
				OptionsButtonUGUI optionsButtonUGUI3 = OptionsButtonUGUI;
				bool flag6 = (object)optionsButtonUGUI3 == null;
				optionsButtonUGUIResolver = this;
				if (!flag6)
				{
					optionsButtonUGUI3.OptionToTextFunc = null;
					goto IL_0182;
				}
			}
			goto IL_0281;
		}
		goto IL_0182;
		IL_0281:
		ex = new NullReferenceException();
		goto IL_0313;
		IL_0182:
		if (!(LocalizationProvider != null))
		{
			return;
		}
		optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)LocalizationProvider;
		if ((object)LocalizationProvider != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
			object obj4 = default(object);
			if (obj4 == null)
			{
				return;
			}
			bool flag7 = (object)LocalizationProvider == null;
			optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)LocalizationProvider;
			if (!flag7)
			{
				ILocalization localization = LocalizationProvider.GetLocalization();
				OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
				bool flag8 = localization == null;
				optionsButtonUGUIResolver = (OptionsButtonUGUIResolver)(object)onLanguageChangedDelegate;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
					return;
				}
			}
		}
		goto IL_0281;
		IL_0305:
		((OptionsButtonUGUIResolver)(object)obj).onLanguageChanged((string)(object)language);
		return;
		IL_0313:
		((OptionsButtonUGUIResolver)(object)ex).onLanguageChanged((string)(object)optionsButtonUGUIResolver);
		language = optionsButtonUGUIResolver;
		goto IL_0305;
	}

	protected void onLanguageChanged(string language)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.OptionsButtonUGUIResolver>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.OptionsButtonUGUIResolver>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	private void onValueChanged(int selectedIndex)
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
		OptionsButtonUGUI optionsButtonUGUI = OptionsButtonUGUI;
		optionsButtonUGUI.UpdateText();
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
						OptionsButtonUGUI optionsButtonUGUI2 = ((OptionsButtonUGUIResolver)settingResolver).OptionsButtonUGUI;
						int value = settingInt.GetValue();
						if ((object)optionsButtonUGUI2 == null)
						{
							throw new NullReferenceException();
						}
						optionsButtonUGUI2.SelectedIndex = value;
					}
					_ = 0;
				}
				else
				{
					((OptionsButtonUGUIResolver)settingResolver).refreshOptions();
					OptionsButtonUGUI optionsButtonUGUI3 = ((OptionsButtonUGUIResolver)settingResolver).OptionsButtonUGUI;
					int value2 = option.GetValue();
					if ((object)optionsButtonUGUI3 == null)
					{
						throw new NullReferenceException();
					}
					optionsButtonUGUI3.SelectedIndex = value2;
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
		if (option != null && option.HasOptions())
		{
			List<string> optionLabels = option.GetOptionLabels();
			if (LocalizationProvider != null && LocalizationProvider.HasLocalization())
			{
				ILocalization localization = LocalizationProvider.GetLocalization();
				List<string> list = optionLabels;
				ILocalization localization2 = localization;
			}
			else
			{
				ILocalization localization3 = LocalizationProvider.GetLocalization();
				OptionsButtonUGUI optionsButtonUGUI = OptionsButtonUGUI;
				List<string> options = optionsButtonUGUI.GetOptions();
				List<string> list = options;
				ILocalization localization2 = localization3;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18005ACF0");
			OptionsButtonUGUI optionsButtonUGUI2 = OptionsButtonUGUI;
			optionsButtonUGUI2.SetOptions(_localizedOptionLabels);
		}
	}

	public OptionsButtonUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[2];
		_ = 7;
		_ = 1;
		supportedDataTypes = array;
		_localizedOptionLabels = new List<string>(3);
		((MonoBehaviour)this)._002Ector();
	}
}
