using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using Kamgam.LocalizationForSettings;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class InputBindingUGUIResolver : SettingResolver, ISettingResolver
{
	public delegate bool ResolveBindingConflictDelegate(string previousBindingPath, string newBindingPath, InputBindingConnection currentConnection, InputBindingConnection conflictingConnection);

	protected InputBindingUGUI inputBindingUGUI;

	public static ResolveBindingConflictDelegate ResolveBindingConflictFunc;

	public bool BlockOnBindingConflict;

	[NonSerialized]
	protected SettingData.DataType[] supportedDataTypes;

	public bool LogLocalizedBindingPath;

	protected bool stopPropagation;

	public InputBindingUGUI InputBindingUGUI
	{
		get
		{
			if (this.inputBindingUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				InputBindingUGUI inputBindingUGUI = default(InputBindingUGUI);
				this.inputBindingUGUI = inputBindingUGUI;
			}
			return this.inputBindingUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_053d: Expected I, but got O
		//IL_0116: Expected I, but got O
		//IL_056d: Expected I, but got O
		//IL_0243: Expected I, but got O
		//IL_036c: Expected I, but got O
		//IL_03a8: Expected I, but got O
		//IL_0293: Expected I, but got O
		//IL_040d: Expected I, but got O
		//IL_0446: Expected I, but got O
		//IL_048f: Expected I, but got O
		base.Start();
		InputBindingUGUI inputBindingUGUI = InputBindingUGUI;
		bool flag = (object)inputBindingUGUI == null;
		InputBindingUGUIResolver inputBindingUGUIResolver = this;
		NullReferenceException ex;
		OnLanguageChangedDelegate onLanguageChangedDelegate;
		nint num;
		if (!flag)
		{
			InputBindingUGUI.OnChangedDelegate b = onChanged;
			Delegate obj = Delegate.Combine(inputBindingUGUI.OnChanged, b);
			if ((object)obj == null)
			{
				inputBindingUGUI.OnChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(InputBindingUGUI.OnChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				onLanguageChangedDelegate = null;
				num = (nint)typeof(InputBindingUGUI.OnChangedDelegate);
				if (flag3)
				{
					goto IL_0591;
				}
				inputBindingUGUI.OnChanged = (InputBindingUGUI.OnChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(InputBindingUGUI.OnChangedDelegate);
				Delegate obj3 = null;
				if (!flag4)
				{
					obj3 = obj;
				}
				bool flag5 = (object)obj3 == null;
				onLanguageChangedDelegate = null;
				num = (nint)typeof(InputBindingUGUI.OnChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)typeof(InputBindingUGUI.OnChangedDelegate);
				if (flag5)
				{
					goto IL_059c;
				}
			}
			InputBindingUGUI inputBindingUGUI2 = InputBindingUGUI;
			bool flag6 = (object)inputBindingUGUI2 == null;
			onLanguageChangedDelegate = null;
			num = (nint)typeof(InputBindingUGUI.OnChangedDelegate);
			inputBindingUGUIResolver = this;
			if (!flag6)
			{
				InputBindingForInputSystem inputBinding = inputBindingUGUI2.InputBinding;
				InputBindingForInputSystem.CheckBindingPathDelegate checkBindingPathDelegate = checkBindingForDuplicates;
				bool flag7 = inputBindingUGUI2.InputBinding == null;
				onLanguageChangedDelegate = null;
				num = 0;
				inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)checkBindingPathDelegate;
				if (!flag7)
				{
					inputBinding.CheckBindingPathFunc = checkBindingPathDelegate;
					InputBindingUGUI inputBindingUGUI3 = InputBindingUGUI;
					Func<string, string> func = localizeKeyCode;
					bool flag8 = (object)inputBindingUGUI3 == null;
					onLanguageChangedDelegate = null;
					num = 0;
					inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)func;
					if (!flag8)
					{
						inputBindingUGUI3.PathToDisplayNameFunc = func;
						if (!(LocalizationProvider != null))
						{
							goto IL_030f;
						}
						inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)LocalizationProvider;
						bool flag9 = (object)LocalizationProvider == null;
						onLanguageChangedDelegate = null;
						num = unchecked((nint)null);
						if (!flag9)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
							object obj4 = default(object);
							if (obj4 == null)
							{
								goto IL_030f;
							}
							bool flag10 = (object)LocalizationProvider == null;
							onLanguageChangedDelegate = null;
							num = unchecked((nint)null);
							inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)LocalizationProvider;
							if (!flag10)
							{
								ILocalization localization = LocalizationProvider.GetLocalization();
								OnLanguageChangedDelegate onLanguageChangedDelegate2 = onLanguageChanged;
								bool flag11 = localization == null;
								onLanguageChangedDelegate = null;
								num = 0;
								inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)onLanguageChangedDelegate2;
								if (!flag11)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
									goto IL_030f;
								}
							}
						}
					}
				}
			}
		}
		goto IL_04da;
		IL_04da:
		ex = new NullReferenceException();
		goto IL_059c;
		IL_059c:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0591;
		IL_030f:
		SettingData.DataType[] array = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, array))
		{
			return;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		bool flag12 = (object)settingsProvider == null;
		onLanguageChangedDelegate = null;
		num = (nint)array;
		inputBindingUGUIResolver = this;
		if (!flag12)
		{
			Settings settings = settingsProvider.Settings;
			bool flag13 = (object)settings == null;
			onLanguageChangedDelegate = null;
			num = (nint)array;
			inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)settingsProvider;
			if (!flag13)
			{
				if (!settings.HasActiveID(ID))
				{
					return;
				}
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				bool flag14 = (object)settingsProvider2 == null;
				onLanguageChangedDelegate = null;
				num = unchecked((nint)null);
				inputBindingUGUIResolver = this;
				if (!flag14)
				{
					Settings settings2 = settingsProvider2.Settings;
					bool flag15 = (object)settings2 == null;
					onLanguageChangedDelegate = null;
					num = unchecked((nint)null);
					inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)settingsProvider2;
					if (!flag15)
					{
						ISetting setting = settings2.GetSetting(ID);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v541 @ r8_v15 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingUGUIResolver>)+240]");
						Action action = new Action(this, (IntPtr)0);
						nint num2 = (nint)this;
						bool flag16 = setting == null;
						onLanguageChangedDelegate = null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v541 @ r8_v15 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingUGUIResolver>)+240]");
						num = 0;
						inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)action;
						if (!flag16)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
							return;
						}
					}
				}
			}
		}
		goto IL_04da;
		IL_0591:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	protected unsafe bool checkBindingForDuplicates(string previousPath, string path)
	{
		//IL_00e1: Expected I, but got O
		//IL_00f1: Expected O, but got I
		//IL_0171: Expected O, but got I4
		//IL_02fe: Expected O, but got I4
		//IL_012d: Expected O, but got I
		//IL_0163: Expected O, but got I4
		//IL_01d6: Expected O, but got Ref
		if (!BlockOnBindingConflict && ResolveBindingConflictFunc == null)
		{
			goto IL_02a6;
		}
		SettingsProvider settingsProvider = base.SettingsProvider;
		object obj = default(object);
		object obj5;
		if ((object)settingsProvider != null)
		{
			Settings settings = settingsProvider.Settings;
			if ((object)settings != null)
			{
				ISetting setting = settings.GetSetting(ID);
				if (setting != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj != null)
					{
						object obj2 = obj;
						nint num = (nint)typeof(InputBindingConnection);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rax_v16 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r9_v6+130]");
						nint num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v130 @ rax_v16 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
						if (num2 >= 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v117 @ r9_v6+C8]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v408 @ rcx_v29+FFFFFFF8+v394 @ rcx_v11*8]");
							if (0 == (nint)typeof(InputBindingConnection))
							{
								obj5 = 1;
								goto IL_02e6;
							}
						}
						obj5 = 0;
						goto IL_02e6;
					}
				}
				goto IL_02a6;
			}
		}
		goto IL_02b4;
		IL_02e6:
		bool flag = obj5 == null;
		object obj6 = 0;
		if (!flag)
		{
			obj6 = obj;
		}
		if (obj6 != null)
		{
			if (InputBindingConnection.Connections == null)
			{
				goto IL_02b4;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<InputBindingConnection>.Enumerator enumerator = default(List<InputBindingConnection>.Enumerator);
			object obj7 = default(object);
			string text = default(string);
			object obj9 = default(object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj7 == obj6)
				{
					continue;
				}
				bool flag2 = obj7 == null;
				SettingResolver settingResolver = (SettingResolver)(&enumerator);
				if (!flag2)
				{
					object obj8 = obj7;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v538 @ rdx_v15+208] (should have been resolved before IL gen)");
					if (!(text == path))
					{
						continue;
					}
					if (ResolveBindingConflictFunc != null)
					{
						ResolveBindingConflictDelegate resolveBindingConflictFunc = ResolveBindingConflictFunc;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v555.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
						bool flag3 = obj9 != null;
						object obj2 = obj6;
						if (flag3)
						{
							continue;
						}
					}
					enumerator.Dispose();
					return false;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
		}
		goto IL_02a6;
		IL_02b4:
		throw new NullReferenceException();
		IL_02a6:
		return true;
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		InputBindingUGUI inputBindingUGUI = InputBindingUGUI;
		InputBindingUGUIResolver inputBindingUGUIResolver;
		Delegate obj = default(Delegate);
		InputBindingUGUIResolver language;
		NullReferenceException ex;
		if (inputBindingUGUI != null)
		{
			InputBindingUGUI inputBindingUGUI2 = InputBindingUGUI;
			bool flag = (object)inputBindingUGUI2 == null;
			inputBindingUGUIResolver = this;
			if (!flag)
			{
				InputBindingUGUI.OnChangedDelegate value = onChanged;
				obj = Delegate.Remove(inputBindingUGUI2.OnChanged, value);
				if ((object)obj == null)
				{
					inputBindingUGUI2.OnChanged = (InputBindingUGUI.OnChangedDelegate)obj;
				}
				else
				{
					bool flag2 = (object)obj.GetType() != typeof(InputBindingUGUI.OnChangedDelegate);
					Delegate obj2 = null;
					if (!flag2)
					{
						obj2 = obj;
					}
					bool flag3 = (object)obj2 == null;
					language = (InputBindingUGUIResolver)(object)typeof(InputBindingUGUI.OnChangedDelegate);
					if (flag3)
					{
						goto IL_0305;
					}
					inputBindingUGUI2.OnChanged = (InputBindingUGUI.OnChangedDelegate)obj2;
					bool flag4 = (object)obj.GetType() != typeof(InputBindingUGUI.OnChangedDelegate);
					Delegate obj3 = null;
					if (!flag4)
					{
						obj3 = obj;
					}
					bool flag5 = (object)obj3 == null;
					ex = (NullReferenceException)(object)obj;
					inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)typeof(InputBindingUGUI.OnChangedDelegate);
					if (flag5)
					{
						goto IL_0313;
					}
				}
				InputBindingUGUI inputBindingUGUI3 = InputBindingUGUI;
				bool flag6 = (object)inputBindingUGUI3 == null;
				inputBindingUGUIResolver = this;
				if (!flag6)
				{
					inputBindingUGUI3.PathToDisplayNameFunc = null;
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
		inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)LocalizationProvider;
		if ((object)LocalizationProvider != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A78200");
			object obj4 = default(object);
			if (obj4 == null)
			{
				return;
			}
			bool flag7 = (object)LocalizationProvider == null;
			inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)LocalizationProvider;
			if (!flag7)
			{
				ILocalization localization = LocalizationProvider.GetLocalization();
				OnLanguageChangedDelegate onLanguageChangedDelegate = onLanguageChanged;
				bool flag8 = localization == null;
				inputBindingUGUIResolver = (InputBindingUGUIResolver)(object)onLanguageChangedDelegate;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
					return;
				}
			}
		}
		goto IL_0281;
		IL_0305:
		((InputBindingUGUIResolver)(object)obj).onLanguageChanged((string)(object)language);
		return;
		IL_0313:
		((InputBindingUGUIResolver)(object)ex).onLanguageChanged((string)(object)inputBindingUGUIResolver);
		language = inputBindingUGUIResolver;
		goto IL_0305;
	}

	protected void onLanguageChanged(string language)
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingUGUIResolver>)+238]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingUGUIResolver>)+240]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	protected string localizeKeyCode(string bindingPath)
	{
		if (LocalizationProvider != null)
		{
			if ((object)LocalizationProvider != null)
			{
				if (!LocalizationProvider.HasLocalization())
				{
					goto IL_01d3;
				}
				if ((object)LocalizationProvider != null)
				{
					ILocalization localization = LocalizationProvider.GetLocalization();
					if (localization != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
						object obj = default(object);
						if (obj == null)
						{
							goto IL_01d3;
						}
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
					}
				}
			}
			goto IL_01c0;
		}
		goto IL_01d3;
		IL_01c0:
		return (string)(object)new NullReferenceException();
		IL_01d3:
		string text;
		if (bindingPath != null)
		{
			text = Regex.Replace(bindingPath, "<[^>]*>/", "");
			if (text == null)
			{
				goto IL_01c0;
			}
			if (text._stringLength < 6)
			{
				return text.ToUpper();
			}
		}
		else
		{
			text = null;
		}
		return text;
	}

	protected void onChanged(string bindingPath)
	{
		if (stopPropagation)
		{
			return;
		}
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (HasValidSettingForID(ID, allowedTypes))
		{
			SettingsProvider settingsProvider = base.SettingsProvider;
			Settings settings = settingsProvider.Settings;
			if (settings.HasActiveID(ID))
			{
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				Settings settings2 = settingsProvider2.Settings;
				SettingString settingString = settings2.GetString(ID);
				settingString.SetValue(bindingPath);
			}
		}
	}

	public override void Refresh()
	{
		//IL_010d: Expected I, but got O
		//IL_011b: Expected I, but got O
		//IL_012b: Expected O, but got I
		//IL_0167: Expected O, but got I
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
		SettingString settingString = settings2.GetString(ID);
		if (settingString == null)
		{
			return;
		}
		InputBindingUGUI inputBindingUGUI = InputBindingUGUI;
		string value = settingString.GetValue();
		inputBindingUGUI.InputBinding.SetBindingPath(value);
		InputBindingConnection connection = (InputBindingConnection)settingString.Connection;
		if (settingString.Connection != null)
		{
			nint num = (nint)connection;
			nint num2 = (nint)typeof(InputBindingConnection);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r8_v13 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v26 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ r8_v13 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+130]");
			if (num3 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rcx_v26 (Il2CppClass<Kamgam.SettingsGenerator.InputBindingConnection>)+C8]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rax_v37+FFFFFFF8+v317 @ rax_v36*8]");
				if (0 == (nint)typeof(InputBindingConnection))
				{
					InputBindingUGUI inputBindingUGUI2 = InputBindingUGUI;
					InputBindingForInputSystem inputBinding = inputBindingUGUI2.InputBinding;
					bool allowComposite = ((InputBindingConnection)settingString.Connection).IsComposite();
					inputBinding.AllowComposite = allowComposite;
				}
			}
		}
		InputBindingUGUIResolver inputBindingUGUIResolver = default(InputBindingUGUIResolver);
		inputBindingUGUIResolver.stopPropagation = true;
		InputBindingUGUI inputBindingUGUI3 = inputBindingUGUIResolver.InputBindingUGUI;
		if ((object)inputBindingUGUI3 != null)
		{
			if (inputBindingUGUI3.PathToDisplayNameFunc == null)
			{
				InputBindingUGUI inputBindingUGUI4 = inputBindingUGUIResolver.InputBindingUGUI;
				Func<string, string> pathToDisplayNameFunc = inputBindingUGUIResolver.localizeKeyCode;
				if ((object)inputBindingUGUI4 == null)
				{
					throw new NullReferenceException();
				}
				inputBindingUGUI4.PathToDisplayNameFunc = pathToDisplayNameFunc;
			}
			InputBindingUGUI inputBindingUGUI5 = inputBindingUGUIResolver.InputBindingUGUI;
			inputBindingUGUI5.UpdateDisplayName();
			inputBindingUGUIResolver.stopPropagation = false;
			return;
		}
		throw new NullReferenceException();
	}

	protected string bindingPathToDisplayName(string bindingPath)
	{
		string text;
		if (bindingPath != null)
		{
			text = Regex.Replace(bindingPath, "<[^>]*>/", "");
			if (text == null)
			{
				return (string)(object)new NullReferenceException();
			}
			if (text._stringLength < 6)
			{
				return text.ToUpper();
			}
		}
		else
		{
			text = null;
		}
		return text;
	}

	public InputBindingUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[1];
		_ = 4;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
