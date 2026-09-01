using System;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class TextfieldUGUIResolver : SettingResolver, ISettingResolver
{
	protected TextfieldUGUI textfieldUGUI;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public TextfieldUGUI TextfieldUGUI
	{
		get
		{
			if (this.textfieldUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				TextfieldUGUI textfieldUGUI = default(TextfieldUGUI);
				this.textfieldUGUI = textfieldUGUI;
			}
			return this.textfieldUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_0327: Expected O, but got I4
		//IL_035b: Expected O, but got I4
		//IL_0143: Expected O, but got I4
		//IL_0183: Expected O, but got I4
		//IL_01ef: Expected O, but got I4
		//IL_022c: Expected O, but got I4
		//IL_027a: Expected I, but got O
		//IL_0293: Expected O, but got I4
		//IL_02a3: Expected O, but got I
		base.Start();
		TextfieldUGUI textfieldUGUI = TextfieldUGUI;
		bool flag = (object)textfieldUGUI == null;
		TextfieldUGUIResolver textfieldUGUIResolver = this;
		NullReferenceException ex;
		if (!flag)
		{
			TextfieldUGUI.OnTextChangedDelegate b = onTextChanged;
			Delegate obj = Delegate.Combine(textfieldUGUI.OnTextChanged, b);
			object obj3;
			SettingData.DataType[] typeFromHandle;
			if ((object)obj == null)
			{
				textfieldUGUI.OnTextChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(TextfieldUGUI.OnTextChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(TextfieldUGUI.OnTextChangedDelegate);
				if (flag3)
				{
					goto IL_038e;
				}
				textfieldUGUI.OnTextChanged = (TextfieldUGUI.OnTextChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(TextfieldUGUI.OnTextChangedDelegate);
				Delegate obj4 = null;
				if (!flag4)
				{
					obj4 = obj;
				}
				bool flag5 = (object)obj4 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(TextfieldUGUI.OnTextChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				textfieldUGUIResolver = (TextfieldUGUIResolver)(object)typeof(TextfieldUGUI.OnTextChangedDelegate);
				if (flag5)
				{
					goto IL_0399;
				}
			}
			SettingData.DataType[] array = GetSupportedDataTypes();
			if (!HasValidSettingForID(ID, array))
			{
				return;
			}
			SettingsProvider settingsProvider = base.SettingsProvider;
			bool flag6 = (object)settingsProvider == null;
			obj3 = 0;
			typeFromHandle = array;
			textfieldUGUIResolver = this;
			if (!flag6)
			{
				Settings settings = settingsProvider.Settings;
				bool flag7 = (object)settings == null;
				obj3 = 0;
				typeFromHandle = array;
				textfieldUGUIResolver = (TextfieldUGUIResolver)(object)settingsProvider;
				if (!flag7)
				{
					if (!settings.HasActiveID(ID))
					{
						return;
					}
					SettingsProvider settingsProvider2 = base.SettingsProvider;
					bool flag8 = (object)settingsProvider2 == null;
					obj3 = 0;
					typeFromHandle = null;
					textfieldUGUIResolver = this;
					if (!flag8)
					{
						Settings settings2 = settingsProvider2.Settings;
						bool flag9 = (object)settings2 == null;
						obj3 = 0;
						typeFromHandle = null;
						textfieldUGUIResolver = (TextfieldUGUIResolver)(object)settingsProvider2;
						if (!flag9)
						{
							ISetting setting = settings2.GetSetting(ID);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.TextfieldUGUIResolver>)+240]");
							Action action = new Action(this, (IntPtr)0);
							nint num = (nint)this;
							bool flag10 = setting == null;
							obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.TextfieldUGUIResolver>)+240]");
							typeFromHandle = (SettingData.DataType[])0;
							textfieldUGUIResolver = (TextfieldUGUIResolver)(object)action;
							if (!flag10)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
								Refresh();
								return;
							}
						}
					}
				}
			}
		}
		ex = new NullReferenceException();
		goto IL_0399;
		IL_038e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		return;
		IL_0399:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_038e;
	}

	public override void OnDestroy()
	{
		//IL_0142: Expected I, but got O
		//IL_0175: Expected O, but got I4
		//IL_0183: Expected I, but got O
		//IL_01a9: Expected O, but got I4
		//IL_01b7: Expected I, but got O
		base.OnDestroy();
		TextfieldUGUI textfieldUGUI = TextfieldUGUI;
		if (!(textfieldUGUI != null))
		{
			return;
		}
		TextfieldUGUI textfieldUGUI2 = TextfieldUGUI;
		if ((object)textfieldUGUI2 != null)
		{
			TextfieldUGUI.OnTextChangedDelegate value = onTextChanged;
			Delegate obj = Delegate.Remove(textfieldUGUI2.OnTextChanged, value);
			if ((object)obj == null)
			{
				textfieldUGUI2.OnTextChanged = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(TextfieldUGUI.OnTextChangedDelegate);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			object obj3 = 0;
			nint num = (nint)typeof(TextfieldUGUI.OnTextChangedDelegate);
			if (flag2)
			{
				goto IL_01cd;
			}
			textfieldUGUI2.OnTextChanged = (TextfieldUGUI.OnTextChangedDelegate)obj2;
			bool flag3 = (object)obj.GetType() != typeof(TextfieldUGUI.OnTextChangedDelegate);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj;
			}
			bool flag4 = (object)obj4 == null;
			obj3 = 0;
			num = (nint)typeof(TextfieldUGUI.OnTextChangedDelegate);
			NullReferenceException ex = (NullReferenceException)(object)obj;
			if (!flag4)
			{
				return;
			}
		}
		else
		{
			NullReferenceException ex = new NullReferenceException();
			nint num = unchecked((nint)null);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_01cd;
		IL_01cd:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	private void onTextChanged(string text)
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
				settings2.GetString(ID)?.SetValue(text);
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
			SettingString settingString = settings2.GetString(settingResolver.ID);
			if (settingString != null)
			{
				TextfieldUGUI textfieldUGUI = ((TextfieldUGUIResolver)settingResolver).TextfieldUGUI;
				string value = settingString.GetValue();
				if ((object)textfieldUGUI == null)
				{
					throw new NullReferenceException();
				}
				textfieldUGUI.Text = value;
			}
			_ = 0;
			return;
		}
		throw new NullReferenceException();
	}

	public TextfieldUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[1];
		_ = 4;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
