using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator;

public class DropdownFieldUIElementResolver : SettingResolverForVisualElement, ISettingResolver
{
	protected DropdownField _dropDown;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	protected List<string> _localizedOptionLabels;

	public DropdownField DropDown
	{
		get
		{
			//IL_0095: Expected I, but got O
			//IL_00a3: Expected I, but got O
			//IL_00b3: Expected O, but got I
			//IL_00ef: Expected O, but got I
			//IL_0114: Expected O, but got I4
			//IL_024e: Expected I, but got O
			//IL_0256: Expected I, but got O
			//IL_0266: Expected O, but got I
			//IL_0149: Expected O, but got I
			//IL_016e: Expected O, but got I4
			if (_dropDown == null)
			{
				VisualElement visualElement = base.VisualElement;
				if (visualElement != null)
				{
					goto IL_0057;
				}
			}
			VisualElement visualElement2 = base.VisualElement;
			if (_dropDown != visualElement2)
			{
				goto IL_0057;
			}
			goto IL_01d6;
			IL_01fc:
			DropdownField dropdownField;
			bool flag = dropdownField == null;
			VisualElement dropDown = null;
			VisualElement visualElement3;
			if (!flag)
			{
				dropDown = visualElement3;
			}
			DropdownField dropdownField2;
			do
			{
				_dropDown = (DropdownField)dropDown;
				nint num = (nint)typeof(DropdownField);
				nint num2 = (nint)visualElement3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v4 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r11_v4 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v4 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r11_v4 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rax_v18+FFFFFFF8+v271 @ rax_v15*8]");
					bool flag2 = 0 == (nint)typeof(DropdownField);
					dropdownField2 = (DropdownField)1;
					if (flag2)
					{
						continue;
					}
				}
				dropdownField2 = null;
			}
			while (dropdownField2 != null);
			goto IL_01dd;
			IL_01d6:
			return _dropDown;
			IL_0057:
			visualElement3 = base.VisualElement;
			if (visualElement3 == null)
			{
				_dropDown = null;
				goto IL_01dd;
			}
			nint num4 = (nint)visualElement3;
			nint num5 = (nint)typeof(DropdownField);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v3 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r11_v3 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v3 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r11_v3 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v21+FFFFFFF8+v175 @ rax_v11*8]");
				bool flag3 = 0 == (nint)typeof(DropdownField);
				dropdownField = (DropdownField)1;
				if (flag3)
				{
					goto IL_01fc;
				}
			}
			dropdownField = null;
			goto IL_01fc;
			IL_01dd:
			if (_dropDown != null)
			{
				EventCallback<ChangeEvent<string>> callback = onSelectionChanged;
				bool flag4 = INotifyValueChangedExtensions.RegisterValueChangedCallback(_dropDown, callback);
			}
			goto IL_01d6;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_011a: Expected I, but got O
		base.Start();
		if (LocalizationProvider != null && LocalizationProvider.HasLocalization())
		{
			ILocalization localization = LocalizationProvider.GetLocalization();
			OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (HasValidSettingForID(ID, allowedTypes))
		{
			SettingsProvider settingsProvider = base.SettingsProvider;
			Settings settings = settingsProvider.Settings;
			ISetting setting = settings.GetSetting(ID);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v281 @ r8_v7 (Il2CppClass<Kamgam.SettingsGenerator.DropdownFieldUIElementResolver>)+240]");
			Action action = new Action(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public override void OnDisable()
	{
		_dropDown = null;
		base.OnDisable();
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		DropdownField dropDown = DropDown;
		if (dropDown != null)
		{
			DropdownField dropDown2 = DropDown;
			EventCallback<ChangeEvent<string>> callback = onSelectionChanged;
			bool flag = INotifyValueChangedExtensions.UnregisterValueChangedCallback(dropDown2, callback);
		}
		if (LocalizationProvider != null && LocalizationProvider.HasLocalization())
		{
			ILocalization localization = LocalizationProvider.GetLocalization();
			OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	protected void onLanguageChanged(string language)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.DropdownFieldUIElementResolver>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.DropdownFieldUIElementResolver>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected unsafe void onSelectionChanged(ChangeEvent<string> evt)
	{
		//IL_0008: Expected O, but got Ref
		//IL_004a: Expected O, but got I
		//IL_026a: Expected O, but got I
		//IL_0077: Expected O, but got Ref
		//IL_009b: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		DropdownField dropDown = DropDown;
		List<string> choices = dropDown.choices;
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rcx_v6 (Il2CppClass<System.String>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ rcx_v6 (Il2CppClass<System.String>)+FC]");
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v11 (Il2CppClass<UnityEngine.UIElements.ChangeEvent`1<System.String>>)+80]");
			object obj4 = (nint)0 + (nint)32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+38]");
		int num3 = choices.IndexOf((string)0);
		if (num3 < 0 || stopPropagation)
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
				settings3.GetInt(ID)?.SetValue(num3);
			}
			else
			{
				option.SetValue(num3);
			}
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
						DropdownField dropDown = ((DropdownFieldUIElementResolver)settingResolver).DropDown;
						int value = settingInt.GetValue();
						if (dropDown == null)
						{
							throw new NullReferenceException();
						}
						dropDown.index = value;
					}
					_ = 0;
				}
				else
				{
					((DropdownFieldUIElementResolver)settingResolver).refreshOptions();
					DropdownField dropDown2 = ((DropdownFieldUIElementResolver)settingResolver).DropDown;
					int value2 = option.GetValue();
					if (dropDown2 == null)
					{
						throw new NullReferenceException();
					}
					dropDown2.index = value2;
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
		//IL_01d8: Expected I, but got O
		//IL_01e8: Expected O, but got I
		//IL_01f8: Expected O, but got I
		//IL_0192: Expected I, but got O
		//IL_01ac: Expected O, but got I
		//IL_01bc: Expected O, but got I
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
		SettingsProvider settingsProvider2 = base.SettingsProvider;
		Settings settings2 = settingsProvider2.Settings;
		SettingOption option = settings2.GetOption(ID);
		DropdownField dropDown2;
		List<string> list3;
		object obj;
		object obj2;
		if (!option.HasOptions())
		{
			ILocalization localization = LocalizationProvider.GetLocalization();
			DropdownField dropDown = DropDown;
			List<string> choices = dropDown.choices;
			List<string> list = choices;
			ILocalization localization2 = localization;
		}
		else
		{
			List<string> optionLabels = option.GetOptionLabels();
			List<string> list2 = new List<string>(optionLabels);
			if (!(LocalizationProvider != null) || !LocalizationProvider.HasLocalization())
			{
				dropDown2 = DropDown;
				nint num = (nint)dropDown2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v443 @ r8_v13 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+B78]");
				obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v443 @ r8_v13 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+B80]");
				obj2 = 0;
				list3 = list2;
				goto IL_0256;
			}
			ILocalization localization3 = LocalizationProvider.GetLocalization();
			List<string> list = list2;
			ILocalization localization2 = localization3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18005ACF0");
		dropDown2 = DropDown;
		nint num2 = (nint)dropDown2;
		list3 = _localizedOptionLabels;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v447 @ r8_v9 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+B78]");
		obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v447 @ r8_v9 (Il2CppClass<UnityEngine.UIElements.DropdownField>)+B80]");
		obj2 = 0;
		goto IL_0256;
		IL_0256:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v334 @ r9_v5 (should have been resolved before IL gen)");
	}

	public DropdownFieldUIElementResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[2];
		_ = 7;
		_ = 1;
		supportedDataTypes = array;
		_localizedOptionLabels = new List<string>(3);
		base._002Ector();
	}
}
