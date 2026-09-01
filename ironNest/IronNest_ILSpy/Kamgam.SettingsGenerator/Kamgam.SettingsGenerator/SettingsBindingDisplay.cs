using System;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using Kamgam.UGUIComponentsForSettings;
using TMPro;
using UnityEngine.UI;

namespace Kamgam.SettingsGenerator;

public class SettingsBindingDisplay : SettingResolver
{
	public bool PreferConfiguredProvider = true;

	[NonSerialized]
	protected SettingData.DataType[] _supportedDataTypes;

	private bool _searchedForText;

	private TMP_Text _textMeshProText;

	private Text _unityText;

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		if (_supportedDataTypes == null)
		{
			SettingData.DataType[] array = new SettingData.DataType[2];
			if (array.Length > 0)
			{
				_ = 4;
				if (array.Length > 1)
				{
					_ = 6;
					_supportedDataTypes = array;
					goto IL_0077;
				}
			}
			return (SettingData.DataType[])(object)new IndexOutOfRangeException();
		}
		goto IL_0077;
		IL_0077:
		return _supportedDataTypes;
	}

	protected SettingsProvider resolveSettingProvider()
	{
		SettingsProvider settingsProvider = base.SettingsProvider;
		bool flag = settingsProvider != null;
		if (!flag)
		{
			if (PreferConfiguredProvider != flag || !(SettingsProvider.LastUsedSettingsProvider != null))
			{
				return SettingsGeneratorSettings.GetProvider();
			}
			return SettingsProvider.LastUsedSettingsProvider;
		}
		return base.SettingsProvider;
	}

	public ISetting GetSetting()
	{
		ISetting result = (ISetting)resolveSettingProvider();
		Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 12 Invalid \"Jump target not found in method: 0x180A34AE0\"");
		return result;
	}

	public void SetText(string text)
	{
		//IL_0114: Expected I, but got O
		//IL_0124: Expected O, but got I
		//IL_0134: Expected O, but got I
		//IL_00d8: Expected I, but got O
		//IL_00e8: Expected O, but got I
		//IL_00f8: Expected O, but got I
		if (!_searchedForText)
		{
			_searchedForText = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			TMP_Text textMeshProText = default(TMP_Text);
			_textMeshProText = textMeshProText;
			if (_textMeshProText == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Text unityText = default(Text);
				_unityText = unityText;
			}
		}
		if (_textMeshProText == null)
		{
			if (!(_unityText != null))
			{
				return;
			}
			Text unityText2 = _unityText;
			nint num = (nint)unityText2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ r8_v9 (Il2CppClass<UnityEngine.UI.Text>)+5E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ r8_v9 (Il2CppClass<UnityEngine.UI.Text>)+5F0]");
			object obj2 = 0;
		}
		else
		{
			TMP_Text textMeshProText2 = _textMeshProText;
			nint num2 = (nint)textMeshProText2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r8_v6 (Il2CppClass<TMPro.TMP_Text>)+558]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v167 @ r8_v6 (Il2CppClass<TMPro.TMP_Text>)+560]");
			object obj2 = 0;
			Text unityText2 = (Text)(object)textMeshProText2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v215 @ rax_v5 (should have been resolved before IL gen)");
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SettingsProvider provider = resolveSettingProvider();
		ISetting setting = GetSetting(provider, ID);
		if (setting != null)
		{
			Action<ISetting> action = onValueChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SettingsProvider provider = resolveSettingProvider();
		ISetting setting = GetSetting(provider, ID);
		if (setting != null)
		{
			Action<ISetting> action = onValueChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public void BindToSetting()
	{
		SettingsProvider provider = resolveSettingProvider();
		ISetting setting = GetSetting(provider, ID);
		if (setting != null)
		{
			Action<ISetting> action = onValueChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public void UnbindFromSetting()
	{
		SettingsProvider provider = resolveSettingProvider();
		ISetting setting = GetSetting(provider, ID);
		if (setting != null)
		{
			Action<ISetting> action = onValueChanged;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	private void onValueChanged(ISetting setting)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingsBindingDisplay>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingsBindingDisplay>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public override void Refresh()
	{
		//IL_013c: Expected I, but got O
		//IL_014c: Expected O, but got I
		//IL_015c: Expected O, but got I
		//IL_0100: Expected I, but got O
		//IL_0110: Expected O, but got I
		//IL_0120: Expected O, but got I
		SettingsProvider provider = resolveSettingProvider();
		string settingBindingDisplayName = GetSettingBindingDisplayName(provider, ID, LocalizationProvider);
		if (!_searchedForText)
		{
			_searchedForText = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			TMP_Text textMeshProText = default(TMP_Text);
			_textMeshProText = textMeshProText;
			if (_textMeshProText == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Text unityText = default(Text);
				_unityText = unityText;
			}
		}
		if (_textMeshProText == null)
		{
			if (!(_unityText != null))
			{
				return;
			}
			Text unityText2 = _unityText;
			nint num = (nint)unityText2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ r8_v10 (Il2CppClass<UnityEngine.UI.Text>)+5E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v271 @ r8_v10 (Il2CppClass<UnityEngine.UI.Text>)+5F0]");
			object obj2 = 0;
		}
		else
		{
			TMP_Text textMeshProText2 = _textMeshProText;
			nint num2 = (nint)textMeshProText2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ r8_v7 (Il2CppClass<TMPro.TMP_Text>)+558]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v170 @ r8_v7 (Il2CppClass<TMPro.TMP_Text>)+560]");
			object obj2 = 0;
			Text unityText2 = (Text)(object)textMeshProText2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v229 @ rax_v7 (should have been resolved before IL gen)");
	}

	public static ISetting GetSetting(SettingsProvider provider, string id)
	{
		if (!(provider != null))
		{
			goto IL_012d;
		}
		ISetting setting;
		if ((object)provider != null)
		{
			Settings settings = provider.Settings;
			if ((object)settings != null)
			{
				if (!settings.HasActiveID(id))
				{
					goto IL_012d;
				}
				Settings settings2 = provider.Settings;
				if ((object)settings2 != null)
				{
					setting = settings2.GetSetting(id);
					if (setting != null)
					{
						goto IL_014a;
					}
					Settings settings3 = provider.Settings;
					if ((object)settings3 != null)
					{
						return settings3.GetSetting(id);
					}
				}
			}
		}
		return (ISetting)new NullReferenceException();
		IL_012d:
		setting = null;
		goto IL_014a;
		IL_014a:
		return setting;
	}

	public static string GetSettingBindingDisplayName(SettingsProvider provider, string settingId, LocalizationProvider localizationProvider = null)
	{
		//IL_04d9: Expected I, but got O
		//IL_04e1: Expected I, but got O
		//IL_04f1: Expected O, but got I
		//IL_02d0: Expected I, but got O
		//IL_02e0: Expected O, but got I
		//IL_0216: Expected O, but got I
		//IL_031c: Expected O, but got I
		//IL_00ac: Expected I, but got O
		//IL_00e4: Expected O, but got I
		//IL_0294: Expected O, but got I
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_018c: Expected I, but got O
		//IL_01d1: Expected I, but got O
		//IL_0448: Expected I4, but got O
		//IL_01f9: Expected I, but got O
		ISetting setting = GetSetting(provider, settingId);
		Func<string, string> func;
		string text;
		Func<string, string> func2;
		if (setting != null)
		{
			bool flag = localizationProvider != null;
			bool flag2 = !flag;
			func = null;
			text = " + ";
			if (!flag2)
			{
				if ((object)localizationProvider != null)
				{
					ILocalization localization = localizationProvider.GetLocalization();
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rax_v52+8]");
					func2 = new Func<string, string>(localization, (IntPtr)0);
					if (localization != null)
					{
						nint num = (nint)localization;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ r9_v11 (Il2CppClass<Kamgam.LocalizationForSettings.ILocalization>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0120;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ r9_v11 (Il2CppClass<Kamgam.LocalizationForSettings.ILocalization>)+B0]");
						object obj = 0;
						Func<string, string> func3 = null;
						while (true)
						{
							object obj2 = func3 + func3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r8_v21+v454 @ rax_v62*8]");
							if (0 == (nint)typeof(ILocalization))
							{
								break;
							}
							func3 = (Func<string, string>)(func3 + 1);
							Func<string, string> func4 = func3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ r9_v11 (Il2CppClass<Kamgam.LocalizationForSettings.ILocalization>)+12E]");
							if ((nint)func4 < 0)
							{
								continue;
							}
							goto IL_0120;
						}
						object obj3 = func3 + func3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v449 @ r8_v21+8+v514 @ rcx_v46*8]");
						object obj4 = (nint)0 + (nint)18;
						object obj5 = obj4 << 4;
						object obj6 = obj5 + 312;
						object obj7 = obj6 + num;
						goto IL_012f;
					}
				}
				goto IL_048f;
			}
			goto IL_04cb;
		}
		return "";
		IL_0120:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		goto IL_012f;
		IL_04cb:
		nint num2 = (nint)typeof(SettingString);
		nint num3 = (nint)setting;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+130]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
		nint num4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v244 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+130]");
		if (num4 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
			object obj9 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v296 @ rax_v42+FFFFFFF8+v246 @ rax_v7*8]");
			if (0 == (nint)typeof(SettingString))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v245 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8] (should have been resolved before IL gen)");
				string path = default(string);
				return InputUtils.BindingPathToDisplayName(path, func, text);
			}
		}
		nint num5 = (nint)typeof(SettingKeyCombination);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+130]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+130]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v322 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.SettingKeyCombination>)+130]");
		if (num6 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+C8]");
			object obj11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v356 @ rax_v12+FFFFFFF8+v323 @ rax_v9*8]");
			if (0 == (nint)typeof(SettingKeyCombination))
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v245 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.ISetting>)+4D8] (should have been resolved before IL gen)");
				UniversalKeyCode keyCode = default(UniversalKeyCode);
				string text2 = InputUtils.UniversalKeyName(keyCode);
				KeyCombination keyCombination = default(KeyCombination);
				UniversalKeyCode keyCode2 = default(UniversalKeyCode);
				string text4;
				if (keyCombination.HasModifier)
				{
					string text3 = InputUtils.UniversalKeyName(keyCode2);
					text4 = text3;
				}
				else
				{
					text4 = "";
				}
				bool flag3 = func == null;
				string text5 = text2;
				if (!flag3)
				{
					if (keyCombination.HasModifier)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v190 @ rdi_v3 (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
						string text6 = default(string);
						bool flag4 = string.IsNullOrEmpty(text6);
						bool flag5 = !flag4;
						text4 = text6;
						if (!flag5)
						{
							string text7 = InputUtils.UniversalKeyName(keyCode2);
							text4 = text7;
						}
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v190 @ rdi_v3 (System.Func`2<System.String, System.String>)+18] (should have been resolved before IL gen)");
					string text8 = default(string);
					bool flag6 = string.IsNullOrEmpty(text8);
					bool flag7 = !flag6;
					text5 = text8;
					if (!flag7)
					{
						string text9 = InputUtils.UniversalKeyName((UniversalKeyCode)keyCombination);
						text5 = text9;
					}
				}
				if (keyCombination.HasModifier)
				{
					string text10 = text4 + text + text5;
					text5 = text10;
				}
				return text5;
			}
		}
		return "";
		IL_012f:
		ILocalization localization2 = localizationProvider.GetLocalization();
		if (localization2 == null)
		{
			goto IL_048f;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		string text11 = default(string);
		bool flag8 = string.IsNullOrEmpty(text11);
		func = func2;
		nint num7 = unchecked((nint)"CompositeControlSeparator");
		text = " + ";
		if (!flag8)
		{
			bool flag9 = text11 != "CompositeControlSeparator";
			bool flag10 = !flag9;
			func = func2;
			num7 = unchecked((nint)"CompositeControlSeparator");
			text = " + ";
			if (!flag10)
			{
				func = func2;
				num7 = unchecked((nint)"CompositeControlSeparator");
				text = text11;
			}
		}
		goto IL_04cb;
		IL_048f:
		return (string)(object)new NullReferenceException();
	}
}
