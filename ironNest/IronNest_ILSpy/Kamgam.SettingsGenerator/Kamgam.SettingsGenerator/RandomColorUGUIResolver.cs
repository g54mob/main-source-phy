using System;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class RandomColorUGUIResolver : SettingResolver, ISettingResolver
{
	protected SettingData.DataType[] supportedDataTypes;

	protected RandomColorUGUI randomColorUGUI;

	protected bool stopPropagation;

	public RandomColorUGUI RandomColorUGUI
	{
		get
		{
			if (this.randomColorUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				RandomColorUGUI randomColorUGUI = default(RandomColorUGUI);
				this.randomColorUGUI = randomColorUGUI;
			}
			return this.randomColorUGUI;
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
		RandomColorUGUI randomColorUGUI = RandomColorUGUI;
		bool flag = (object)randomColorUGUI == null;
		RandomColorUGUIResolver randomColorUGUIResolver = this;
		NullReferenceException ex;
		if (!flag)
		{
			RandomColorUGUI.OnColorChangedDelegate b = onColorChanged;
			Delegate obj = Delegate.Combine(randomColorUGUI.OnColorChanged, b);
			object obj3;
			SettingData.DataType[] typeFromHandle;
			if ((object)obj == null)
			{
				randomColorUGUI.OnColorChanged = null;
			}
			else
			{
				bool flag2 = (object)obj.GetType() != typeof(RandomColorUGUI.OnColorChangedDelegate);
				Delegate obj2 = null;
				if (!flag2)
				{
					obj2 = obj;
				}
				bool flag3 = (object)obj2 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(RandomColorUGUI.OnColorChangedDelegate);
				if (flag3)
				{
					goto IL_038e;
				}
				randomColorUGUI.OnColorChanged = (RandomColorUGUI.OnColorChangedDelegate)obj2;
				bool flag4 = (object)obj.GetType() != typeof(RandomColorUGUI.OnColorChangedDelegate);
				Delegate obj4 = null;
				if (!flag4)
				{
					obj4 = obj;
				}
				bool flag5 = (object)obj4 == null;
				obj3 = 0;
				typeFromHandle = (SettingData.DataType[])(object)typeof(RandomColorUGUI.OnColorChangedDelegate);
				ex = (NullReferenceException)(object)obj;
				randomColorUGUIResolver = (RandomColorUGUIResolver)(object)typeof(RandomColorUGUI.OnColorChangedDelegate);
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
			randomColorUGUIResolver = this;
			if (!flag6)
			{
				Settings settings = settingsProvider.Settings;
				bool flag7 = (object)settings == null;
				obj3 = 0;
				typeFromHandle = array;
				randomColorUGUIResolver = (RandomColorUGUIResolver)(object)settingsProvider;
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
					randomColorUGUIResolver = this;
					if (!flag8)
					{
						Settings settings2 = settingsProvider2.Settings;
						bool flag9 = (object)settings2 == null;
						obj3 = 0;
						typeFromHandle = null;
						randomColorUGUIResolver = (RandomColorUGUIResolver)(object)settingsProvider2;
						if (!flag9)
						{
							ISetting setting = settings2.GetSetting(ID);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.RandomColorUGUIResolver>)+240]");
							Action action = new Action(this, (IntPtr)0);
							nint num = (nint)this;
							bool flag10 = setting == null;
							obj3 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v360 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.RandomColorUGUIResolver>)+240]");
							typeFromHandle = (SettingData.DataType[])0;
							randomColorUGUIResolver = (RandomColorUGUIResolver)(object)action;
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
		RandomColorUGUI randomColorUGUI = RandomColorUGUI;
		if (!(randomColorUGUI != null))
		{
			return;
		}
		RandomColorUGUI randomColorUGUI2 = RandomColorUGUI;
		if ((object)randomColorUGUI2 != null)
		{
			RandomColorUGUI.OnColorChangedDelegate value = onColorChanged;
			Delegate obj = Delegate.Remove(randomColorUGUI2.OnColorChanged, value);
			if ((object)obj == null)
			{
				randomColorUGUI2.OnColorChanged = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(RandomColorUGUI.OnColorChangedDelegate);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			object obj3 = 0;
			nint num = (nint)typeof(RandomColorUGUI.OnColorChangedDelegate);
			if (flag2)
			{
				goto IL_01cd;
			}
			randomColorUGUI2.OnColorChanged = (RandomColorUGUI.OnColorChangedDelegate)obj2;
			bool flag3 = (object)obj.GetType() != typeof(RandomColorUGUI.OnColorChangedDelegate);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj;
			}
			bool flag4 = (object)obj4 == null;
			obj3 = 0;
			num = (nint)typeof(RandomColorUGUI.OnColorChangedDelegate);
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

	private unsafe void onColorChanged(Color color)
	{
		//IL_00f1: Expected O, but got Ref
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
				SettingColor color2 = settings2.GetColor(ID);
				object obj = default(object);
				color2.SetValue((Color)(&obj));
			}
		}
	}

	public unsafe override void Refresh()
	{
		//IL_0128: Expected O, but got Ref
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
		SettingColor color = settings2.GetColor(ID);
		if (color != null)
		{
			RandomColorUGUIResolver randomColorUGUIResolver = default(RandomColorUGUIResolver);
			randomColorUGUIResolver.stopPropagation = true;
			RandomColorUGUI randomColorUGUI = randomColorUGUIResolver.RandomColorUGUI;
			Color value = color.GetValue();
			if ((object)randomColorUGUI == null)
			{
				throw new NullReferenceException();
			}
			float num = default(float);
			randomColorUGUI.Color = (Color)(&num);
			randomColorUGUIResolver.stopPropagation = false;
		}
	}

	public RandomColorUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[1];
		_ = 5;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
