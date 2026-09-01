using System;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class InputKeyUGUIResolver : SettingResolver, ISettingResolver
{
	protected InputKeyUGUI inputKeyUGUI;

	[NonSerialized]
	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public InputKeyUGUI InputKeyUGUI
	{
		get
		{
			if (this.inputKeyUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				InputKeyUGUI inputKeyUGUI = default(InputKeyUGUI);
				this.inputKeyUGUI = inputKeyUGUI;
			}
			return this.inputKeyUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_04e4: Expected I4, but got O
		//IL_0475: Expected I, but got O
		//IL_04a0: Expected I, but got O
		//IL_0190: Expected I, but got O
		//IL_02b8: Expected I, but got O
		//IL_02ef: Expected I, but got O
		//IL_01db: Expected I, but got O
		//IL_034f: Expected I, but got O
		//IL_0383: Expected I, but got O
		//IL_0257: Expected I4, but got O
		//IL_0257: Expected O, but got I4
		//IL_03cc: Expected I, but got O
		base.Start();
		InputKeyUGUI inputKeyUGUI = InputKeyUGUI;
		bool flag = (object)inputKeyUGUI == null;
		InputKeyUGUIResolver inputKeyUGUIResolver = this;
		Delegate obj;
		nint num = default(nint);
		NullReferenceException ex;
		if (!flag)
		{
			InputKeyUGUI.OnChangedDelegate b = onChanged;
			obj = Delegate.Combine(inputKeyUGUI.OnChanged, b);
			if ((object)obj == null)
			{
				inputKeyUGUI.OnChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(InputKeyUGUI.OnChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				num = (nint)typeof(InputKeyUGUI.OnChangedDelegate);
				if (flag3)
				{
					goto IL_04c5;
				}
				inputKeyUGUI.OnChanged = (InputKeyUGUI.OnChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(InputKeyUGUI.OnChangedDelegate);
				Delegate obj3 = null;
				if (!flag4)
				{
					obj3 = obj;
				}
				bool flag5 = (object)obj3 == null;
				num = (nint)typeof(InputKeyUGUI.OnChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)typeof(InputKeyUGUI.OnChangedDelegate);
				if (flag5)
				{
					goto IL_04d7;
				}
			}
			InputKeyUGUI inputKeyUGUI2 = InputKeyUGUI;
			Func<UniversalKeyCode, string> func = localizeKeyCode;
			bool flag6 = (object)inputKeyUGUI2 == null;
			num = 0;
			inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)func;
			if (!flag6)
			{
				inputKeyUGUI2.KeyCodeToKeyNameFunc = func;
				if (!(LocalizationProvider != null))
				{
					goto IL_0260;
				}
				inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)LocalizationProvider;
				bool flag7 = (object)LocalizationProvider == null;
				num = unchecked((nint)null);
				if (!flag7)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
					object obj4 = default(object);
					if (obj4 == null)
					{
						goto IL_0260;
					}
					bool flag8 = (object)LocalizationProvider == null;
					num = unchecked((nint)null);
					inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)LocalizationProvider;
					if (!flag8)
					{
						ILocalization localization = LocalizationProvider.GetLocalization();
						OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
						bool flag9 = localization == null;
						num = 0;
						inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)onLanguageChangedDelegate;
						if (!flag9)
						{
							string text = ((InputKeyUGUIResolver)23).localizeKeyCode((UniversalKeyCode)typeof(ILocalization));
							goto IL_0260;
						}
					}
				}
			}
		}
		goto IL_0417;
		IL_04c5:
		string text2 = ((InputKeyUGUIResolver)(object)obj).localizeKeyCode((UniversalKeyCode)num);
		return;
		IL_0260:
		SettingData.DataType[] array = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, array))
		{
			return;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		bool flag10 = (object)settingsProvider == null;
		num = (nint)array;
		inputKeyUGUIResolver = this;
		if (!flag10)
		{
			Settings settings = settingsProvider.Settings;
			bool flag11 = (object)settings == null;
			num = (nint)array;
			inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)settingsProvider;
			if (!flag11)
			{
				if (!settings.HasActiveID(ID))
				{
					return;
				}
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				bool flag12 = (object)settingsProvider2 == null;
				num = unchecked((nint)null);
				inputKeyUGUIResolver = this;
				if (!flag12)
				{
					Settings settings2 = settingsProvider2.Settings;
					bool flag13 = (object)settings2 == null;
					num = unchecked((nint)null);
					inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)settingsProvider2;
					if (!flag13)
					{
						ISetting setting = settings2.GetSetting(ID);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v503 @ r8_v14 (Il2CppClass<Kamgam.SettingsGenerator.InputKeyUGUIResolver>)+240]");
						Action action = new Action(this, (IntPtr)0);
						nint num2 = (nint)this;
						bool flag14 = setting == null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v503 @ r8_v14 (Il2CppClass<Kamgam.SettingsGenerator.InputKeyUGUIResolver>)+240]");
						num = 0;
						inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)action;
						if (!flag14)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							Refresh();
							return;
						}
					}
				}
			}
		}
		goto IL_0417;
		IL_04d7:
		obj = (Delegate)(object)((InputKeyUGUIResolver)(object)ex).localizeKeyCode((UniversalKeyCode)inputKeyUGUIResolver);
		goto IL_04c5;
		IL_0417:
		ex = new NullReferenceException();
		goto IL_04d7;
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		InputKeyUGUI inputKeyUGUI = InputKeyUGUI;
		InputKeyUGUIResolver inputKeyUGUIResolver;
		Delegate obj = default(Delegate);
		InputKeyUGUIResolver language;
		NullReferenceException ex;
		if (inputKeyUGUI != null)
		{
			InputKeyUGUI inputKeyUGUI2 = InputKeyUGUI;
			bool flag = (object)inputKeyUGUI2 == null;
			inputKeyUGUIResolver = this;
			if (!flag)
			{
				InputKeyUGUI.OnChangedDelegate value = onChanged;
				obj = Delegate.Remove(inputKeyUGUI2.OnChanged, value);
				if ((object)obj == null)
				{
					inputKeyUGUI2.OnChanged = (InputKeyUGUI.OnChangedDelegate)obj;
				}
				else
				{
					bool flag2 = (object)obj.GetType() != typeof(InputKeyUGUI.OnChangedDelegate);
					Delegate obj2 = null;
					if (!flag2)
					{
						obj2 = obj;
					}
					bool flag3 = (object)obj2 == null;
					language = (InputKeyUGUIResolver)(object)typeof(InputKeyUGUI.OnChangedDelegate);
					if (flag3)
					{
						goto IL_0305;
					}
					inputKeyUGUI2.OnChanged = (InputKeyUGUI.OnChangedDelegate)obj2;
					bool flag4 = (object)obj.GetType() != typeof(InputKeyUGUI.OnChangedDelegate);
					Delegate obj3 = null;
					if (!flag4)
					{
						obj3 = obj;
					}
					bool flag5 = (object)obj3 == null;
					ex = (NullReferenceException)(object)obj;
					inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)typeof(InputKeyUGUI.OnChangedDelegate);
					if (flag5)
					{
						goto IL_0313;
					}
				}
				InputKeyUGUI inputKeyUGUI3 = InputKeyUGUI;
				bool flag6 = (object)inputKeyUGUI3 == null;
				inputKeyUGUIResolver = this;
				if (!flag6)
				{
					inputKeyUGUI3.KeyCodeToKeyNameFunc = null;
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
		inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)LocalizationProvider;
		if ((object)LocalizationProvider != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
			object obj4 = default(object);
			if (obj4 == null)
			{
				return;
			}
			bool flag7 = (object)LocalizationProvider == null;
			inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)LocalizationProvider;
			if (!flag7)
			{
				ILocalization localization = LocalizationProvider.GetLocalization();
				OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
				bool flag8 = localization == null;
				inputKeyUGUIResolver = (InputKeyUGUIResolver)(object)onLanguageChangedDelegate;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
					return;
				}
			}
		}
		goto IL_0281;
		IL_0305:
		((InputKeyUGUIResolver)(object)obj).onLanguageChanged((string)(object)language);
		return;
		IL_0313:
		((InputKeyUGUIResolver)(object)ex).onLanguageChanged((string)(object)inputKeyUGUIResolver);
		language = inputKeyUGUIResolver;
		goto IL_0305;
	}

	protected void onLanguageChanged(string language)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.InputKeyUGUIResolver>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.InputKeyUGUIResolver>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected unsafe string localizeKeyCode(UniversalKeyCode keyCode)
	{
		//IL_007e: Expected O, but got Ref
		if (LocalizationProvider != null)
		{
			if ((object)LocalizationProvider == null)
			{
				goto IL_0138;
			}
			if (LocalizationProvider.HasLocalization())
			{
				object obj = default(object);
				string text = ((Enum)(&obj)).ToString();
				ILocalization localization = LocalizationProvider.GetLocalization();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
				object obj2 = default(object);
				if (obj2 != null)
				{
					if ((object)LocalizationProvider != null)
					{
						ILocalization localization2 = LocalizationProvider.GetLocalization();
						if (localization2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							string result = default(string);
							return result;
						}
					}
					goto IL_0138;
				}
			}
		}
		return InputUtils.UniversalKeyName(keyCode);
		IL_0138:
		return (string)(object)new NullReferenceException();
	}

	protected void onChanged(UniversalKeyCode key, UniversalKeyCode modifierKey)
	{
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
			SettingKeyCombination keyCombination = settings2.GetKeyCombination(ID);
			if (keyCombination != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BC150");
				KeyCombination keyCombination2 = default(KeyCombination);
				keyCombination.SetValue(keyCombination2);
			}
		}
	}

	public override void Refresh()
	{
		//IL_0187: Expected I4, but got O
		//IL_01e3: Expected I4, but got O
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
		InputKeyUGUI inputKeyUGUI = InputKeyUGUI;
		inputKeyUGUI.UpdateKeyName();
		SettingsProvider settingsProvider2 = base.SettingsProvider;
		Settings settings2 = settingsProvider2.Settings;
		SettingKeyCombination keyCombination = settings2.GetKeyCombination(ID);
		if (keyCombination == null)
		{
			return;
		}
		InputKeyUGUIResolver inputKeyUGUIResolver = default(InputKeyUGUIResolver);
		inputKeyUGUIResolver.stopPropagation = true;
		InputKeyUGUI inputKeyUGUI2 = inputKeyUGUIResolver.InputKeyUGUI;
		if ((object)inputKeyUGUI2 != null)
		{
			if (inputKeyUGUI2.KeyCodeToKeyNameFunc == null)
			{
				InputKeyUGUI inputKeyUGUI3 = inputKeyUGUIResolver.InputKeyUGUI;
				Func<UniversalKeyCode, string> keyCodeToKeyNameFunc = inputKeyUGUIResolver.localizeKeyCode;
				if ((object)inputKeyUGUI3 == null)
				{
					throw new NullReferenceException();
				}
				inputKeyUGUI3.KeyCodeToKeyNameFunc = keyCodeToKeyNameFunc;
			}
			InputKeyUGUI inputKeyUGUI4 = inputKeyUGUIResolver.InputKeyUGUI;
			UniversalKeyCode key = (UniversalKeyCode)keyCombination.GetValue();
			if ((object)inputKeyUGUI4 != null)
			{
				inputKeyUGUI4.Key = key;
				InputKeyUGUI inputKeyUGUI5 = inputKeyUGUIResolver.InputKeyUGUI;
				KeyCombination value = keyCombination.GetValue();
				UniversalKeyCode modifierKey = (UniversalKeyCode)((object)value >> 32);
				inputKeyUGUI5.ModifierKey = modifierKey;
				inputKeyUGUIResolver.stopPropagation = false;
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public InputKeyUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[1];
		_ = 6;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
