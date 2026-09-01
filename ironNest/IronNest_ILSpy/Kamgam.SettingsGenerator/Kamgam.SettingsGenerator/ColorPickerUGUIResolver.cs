using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ColorPickerUGUIResolver : SettingResolver, ISettingResolver
{
	protected ColorPickerUGUI colorPickerUGUI;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public ColorPickerUGUI ColorPickerUGUI
	{
		get
		{
			if (this.colorPickerUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				ColorPickerUGUI colorPickerUGUI = default(ColorPickerUGUI);
				this.colorPickerUGUI = colorPickerUGUI;
			}
			return this.colorPickerUGUI;
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
		ColorPickerUGUI colorPickerUGUI = ColorPickerUGUI;
		bool flag = (object)colorPickerUGUI == null;
		ColorPickerUGUIResolver colorPickerUGUIResolver = this;
		NullReferenceException ex;
		if (!flag)
		{
			ColorPickerUGUI.OnSelectionChangedDelegate b = onSelectionChanged;
			Delegate obj = Delegate.Combine(colorPickerUGUI.OnSelectionChanged, b);
			object obj3;
			SettingData.DataType[] typeFromHandle;
			if ((object)obj == null)
			{
				colorPickerUGUI.OnSelectionChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
				if (flag3)
				{
					goto IL_038e;
				}
				colorPickerUGUI.OnSelectionChanged = (ColorPickerUGUI.OnSelectionChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
				Delegate obj4 = null;
				if (!flag4)
				{
					obj4 = obj;
				}
				bool flag5 = (object)obj4 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				colorPickerUGUIResolver = (ColorPickerUGUIResolver)(object)typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
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
			colorPickerUGUIResolver = this;
			if (!flag6)
			{
				Settings settings = settingsProvider.Settings;
				bool flag7 = (object)settings == null;
				obj3 = 0;
				typeFromHandle = array;
				colorPickerUGUIResolver = (ColorPickerUGUIResolver)(object)settingsProvider;
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
					colorPickerUGUIResolver = this;
					if (!flag8)
					{
						Settings settings2 = settingsProvider2.Settings;
						bool flag9 = (object)settings2 == null;
						obj3 = 0;
						typeFromHandle = null;
						colorPickerUGUIResolver = (ColorPickerUGUIResolver)(object)settingsProvider2;
						if (!flag9)
						{
							ISetting setting = settings2.GetSetting(ID);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.ColorPickerUGUIResolver>)+240]");
							Action action = new Action(this, (IntPtr)0);
							nint num = (nint)this;
							bool flag10 = setting == null;
							obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.ColorPickerUGUIResolver>)+240]");
							typeFromHandle = (SettingData.DataType[])0;
							colorPickerUGUIResolver = (ColorPickerUGUIResolver)(object)action;
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

	private void onSelectionChanged(int selectedIndex)
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
			SettingColorOption colorOption = settings2.GetColorOption(ID);
			if (colorOption == null)
			{
				SettingsProvider settingsProvider3 = base.SettingsProvider;
				Settings settings3 = settingsProvider3.Settings;
				settings3.GetInt(ID)?.SetValue(selectedIndex);
				return;
			}
			nint num = (nint)colorOption;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+4E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v263 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingColorOption>)+4F0]");
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
		SettingsProvider settingsProvider2 = base.SettingsProvider;
		Settings settings2 = settingsProvider2.Settings;
		SettingColorOption colorOption = settings2.GetColorOption(ID);
		if (colorOption != null)
		{
			stopPropagation = true;
			ColorPickerUGUIResolver colorPickerUGUIResolver = default(ColorPickerUGUIResolver);
			SettingsProvider settingsProvider3 = colorPickerUGUIResolver.SettingsProvider;
			if ((object)settingsProvider3 != null)
			{
				Settings settings3 = settingsProvider3.Settings;
				if ((object)settings3 != null)
				{
					if (settings3.HasActiveID(colorPickerUGUIResolver.ID))
					{
						SettingsProvider settingsProvider4 = colorPickerUGUIResolver.SettingsProvider;
						if ((object)settingsProvider4 == null)
						{
							throw new NullReferenceException();
						}
						Settings settings4 = settingsProvider4.Settings;
						if ((object)settings4 == null)
						{
							throw new NullReferenceException();
						}
						SettingColorOption colorOption2 = settings4.GetColorOption(colorPickerUGUIResolver.ID);
						if (colorOption2 != null)
						{
							if (!colorOption2.HasOptions())
							{
								ColorPickerUGUI colorPickerUGUI = colorPickerUGUIResolver.ColorPickerUGUI;
								if ((object)colorPickerUGUI == null)
								{
									throw new NullReferenceException();
								}
								List<Color> colorOptions = colorPickerUGUI.GetColorOptions();
								colorOption2.SetOptionLabels(colorOptions);
							}
							else
							{
								ColorPickerUGUI colorPickerUGUI2 = colorPickerUGUIResolver.ColorPickerUGUI;
								List<Color> optionLabels = colorOption2.GetOptionLabels();
								if ((object)colorPickerUGUI2 == null)
								{
									throw new NullReferenceException();
								}
								colorPickerUGUI2.SetColorOptions(optionLabels);
							}
						}
					}
					ColorPickerUGUI colorPickerUGUI3 = colorPickerUGUIResolver.ColorPickerUGUI;
					int value = colorOption.GetValue();
					colorPickerUGUI3.SelectedIndex = value;
					colorPickerUGUIResolver.stopPropagation = false;
					return;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		SettingsProvider settingsProvider5 = base.SettingsProvider;
		Settings settings5 = settingsProvider5.Settings;
		SettingInt settingInt = settings5.GetInt(ID);
		if (settingInt != null)
		{
			stopPropagation = true;
			settingInt.PullFromConnection();
			ColorPickerUGUIResolver colorPickerUGUIResolver2 = default(ColorPickerUGUIResolver);
			ColorPickerUGUI colorPickerUGUI4 = colorPickerUGUIResolver2.ColorPickerUGUI;
			int value2 = settingInt.GetValue();
			if ((object)colorPickerUGUI4 == null)
			{
				throw new NullReferenceException();
			}
			colorPickerUGUI4.SelectedIndex = value2;
			colorPickerUGUIResolver2.stopPropagation = false;
			colorPickerUGUIResolver2.stopPropagation = false;
		}
	}

	private void refreshOptions()
	{
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (!settings.HasActiveID(ID))
		{
			return;
		}
		SettingsProvider settingsProvider2 = base.SettingsProvider;
		Settings settings2 = settingsProvider2.Settings;
		SettingColorOption colorOption = settings2.GetColorOption(ID);
		if (colorOption != null)
		{
			if (!colorOption.HasOptions())
			{
				ColorPickerUGUI colorPickerUGUI = ColorPickerUGUI;
				List<Color> colorOptions = colorPickerUGUI.GetColorOptions();
				colorOption.SetOptionLabels(colorOptions);
			}
			else
			{
				ColorPickerUGUI colorPickerUGUI2 = ColorPickerUGUI;
				List<Color> optionLabels = colorOption.GetOptionLabels();
				colorPickerUGUI2.SetColorOptions(optionLabels);
			}
		}
	}

	public override void OnDestroy()
	{
		//IL_0142: Expected I, but got O
		//IL_0175: Expected O, but got I4
		//IL_0183: Expected I, but got O
		//IL_01a9: Expected O, but got I4
		//IL_01b7: Expected I, but got O
		base.OnDestroy();
		ColorPickerUGUI colorPickerUGUI = ColorPickerUGUI;
		if (!(colorPickerUGUI != null))
		{
			return;
		}
		ColorPickerUGUI colorPickerUGUI2 = ColorPickerUGUI;
		if ((object)colorPickerUGUI2 != null)
		{
			ColorPickerUGUI.OnSelectionChangedDelegate value = onSelectionChanged;
			Delegate obj = Delegate.Remove(colorPickerUGUI2.OnSelectionChanged, value);
			if ((object)obj == null)
			{
				colorPickerUGUI2.OnSelectionChanged = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			object obj3 = 0;
			nint num = (nint)typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
			if (flag2)
			{
				goto IL_01cd;
			}
			colorPickerUGUI2.OnSelectionChanged = (ColorPickerUGUI.OnSelectionChangedDelegate)obj2;
			bool flag3 = (object)obj.GetType() != typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj;
			}
			bool flag4 = (object)obj4 == null;
			obj3 = 0;
			num = (nint)typeof(ColorPickerUGUI.OnSelectionChangedDelegate);
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

	public ColorPickerUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[2];
		_ = 8;
		_ = 1;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
