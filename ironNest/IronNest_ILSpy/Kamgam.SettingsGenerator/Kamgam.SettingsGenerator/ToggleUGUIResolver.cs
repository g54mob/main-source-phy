using System;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ToggleUGUIResolver : SettingResolver, ISettingResolver
{
	protected ToggleUGUI toggleUGUI;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public ToggleUGUI ToggleUGUI
	{
		get
		{
			if (this.toggleUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				ToggleUGUI toggleUGUI = default(ToggleUGUI);
				this.toggleUGUI = toggleUGUI;
			}
			return this.toggleUGUI;
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
		ToggleUGUI toggleUGUI = ToggleUGUI;
		bool flag = (object)toggleUGUI == null;
		ToggleUGUIResolver toggleUGUIResolver = this;
		NullReferenceException ex;
		if (!flag)
		{
			ToggleUGUI.ValueChangedDelegate b = onValueChanged;
			Delegate obj = Delegate.Combine(toggleUGUI.OnValueChanged, b);
			object obj3;
			SettingData.DataType[] typeFromHandle;
			if ((object)obj == null)
			{
				toggleUGUI.OnValueChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(ToggleUGUI.ValueChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(ToggleUGUI.ValueChangedDelegate);
				if (flag3)
				{
					goto IL_038e;
				}
				toggleUGUI.OnValueChanged = (ToggleUGUI.ValueChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(ToggleUGUI.ValueChangedDelegate);
				Delegate obj4 = null;
				if (!flag4)
				{
					obj4 = obj;
				}
				bool flag5 = (object)obj4 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(ToggleUGUI.ValueChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				toggleUGUIResolver = (ToggleUGUIResolver)(object)typeof(ToggleUGUI.ValueChangedDelegate);
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
			toggleUGUIResolver = this;
			if (!flag6)
			{
				Settings settings = settingsProvider.Settings;
				bool flag7 = (object)settings == null;
				obj3 = 0;
				typeFromHandle = array;
				toggleUGUIResolver = (ToggleUGUIResolver)(object)settingsProvider;
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
					toggleUGUIResolver = this;
					if (!flag8)
					{
						Settings settings2 = settingsProvider2.Settings;
						bool flag9 = (object)settings2 == null;
						obj3 = 0;
						typeFromHandle = null;
						toggleUGUIResolver = (ToggleUGUIResolver)(object)settingsProvider2;
						if (!flag9)
						{
							ISetting setting = settings2.GetSetting(ID);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.ToggleUGUIResolver>)+240]");
							Action action = new Action(this, (IntPtr)0);
							nint num = (nint)this;
							bool flag10 = setting == null;
							obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.ToggleUGUIResolver>)+240]");
							typeFromHandle = (SettingData.DataType[])0;
							toggleUGUIResolver = (ToggleUGUIResolver)(object)action;
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
		ToggleUGUI toggleUGUI = ToggleUGUI;
		if (!(toggleUGUI != null))
		{
			return;
		}
		ToggleUGUI toggleUGUI2 = ToggleUGUI;
		if ((object)toggleUGUI2 != null)
		{
			ToggleUGUI.ValueChangedDelegate value = onValueChanged;
			Delegate obj = Delegate.Remove(toggleUGUI2.OnValueChanged, value);
			if ((object)obj == null)
			{
				toggleUGUI2.OnValueChanged = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(ToggleUGUI.ValueChangedDelegate);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			object obj3 = 0;
			nint num = (nint)typeof(ToggleUGUI.ValueChangedDelegate);
			if (flag2)
			{
				goto IL_01cd;
			}
			toggleUGUI2.OnValueChanged = (ToggleUGUI.ValueChangedDelegate)obj2;
			bool flag3 = (object)obj.GetType() != typeof(ToggleUGUI.ValueChangedDelegate);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj;
			}
			bool flag4 = (object)obj4 == null;
			obj3 = 0;
			num = (nint)typeof(ToggleUGUI.ValueChangedDelegate);
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

	private void onValueChanged(bool value)
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
				settings2.GetBool(ID)?.SetValue(value);
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
			SettingBool settingBool = settings2.GetBool(settingResolver.ID);
			if (settingBool != null)
			{
				ToggleUGUI toggleUGUI = ((ToggleUGUIResolver)settingResolver).ToggleUGUI;
				bool value = settingBool.GetValue();
				if ((object)toggleUGUI == null)
				{
					throw new NullReferenceException();
				}
				toggleUGUI.Value = value;
			}
			_ = 0;
			return;
		}
		throw new NullReferenceException();
	}

	public ToggleUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[1];
		_ = 3;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
