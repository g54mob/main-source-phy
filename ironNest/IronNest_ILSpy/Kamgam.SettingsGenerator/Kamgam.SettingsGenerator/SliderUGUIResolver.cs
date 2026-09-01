using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class SliderUGUIResolver : SettingResolver, ISettingResolver
{
	protected SliderUGUI _sliderUGUI;

	[NonSerialized]
	protected SettingData.DataType[] supportedDataTypes = new SettingData.DataType[3]
	{
		SettingData.DataType.Int,
		SettingData.DataType.Float,
		SettingData.DataType.Option
	};

	protected float _lastValue = -1f / 0f;

	protected bool stopPropagation;

	public SliderUGUI SliderUGUI
	{
		get
		{
			if (_sliderUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				SliderUGUI sliderUGUI = default(SliderUGUI);
				_sliderUGUI = sliderUGUI;
			}
			return _sliderUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_0035: Expected O, but got I4
		//IL_0396: Expected O, but got I4
		//IL_03ca: Expected O, but got I4
		//IL_01c5: Expected O, but got I4
		//IL_0205: Expected O, but got I4
		//IL_0271: Expected O, but got I4
		//IL_02ae: Expected O, but got I4
		//IL_02fc: Expected I, but got O
		//IL_0315: Expected O, but got I4
		//IL_0325: Expected O, but got I
		base.Start();
		SliderUGUI sliderUGUI = SliderUGUI;
		SettingData.DataType dataType = GetDataType();
		bool flag = (object)sliderUGUI == null;
		SettingResolver settingResolver = this;
		NullReferenceException ex;
		if (!flag)
		{
			object obj = dataType - 1;
			bool wholeNumbers = obj == null;
			sliderUGUI.WholeNumbers = wholeNumbers;
			SliderUGUI sliderUGUI2 = SliderUGUI;
			bool flag2 = (object)sliderUGUI2 == null;
			SettingData.DataType[] array = null;
			settingResolver = this;
			if (!flag2)
			{
				SliderUGUI.ValueChangedDelegate b = onValueChanged;
				Delegate obj2 = Delegate.Combine(sliderUGUI2.OnValueChanged, b);
				object obj4;
				if ((object)obj2 == null)
				{
					sliderUGUI2.OnValueChanged = null;
				}
				else
				{
					bool flag3 = (object)obj2.GetType() != typeof(SliderUGUI.ValueChangedDelegate);
					Delegate obj3 = null;
					if (!flag3)
					{
						obj3 = obj2;
					}
					bool flag4 = (object)obj3 == null;
					obj4 = 0;
					array = (SettingData.DataType[])(object)typeof(SliderUGUI.ValueChangedDelegate);
					if (flag4)
					{
						goto IL_03fd;
					}
					sliderUGUI2.OnValueChanged = (SliderUGUI.ValueChangedDelegate)obj3;
					bool flag5 = (object)obj2.GetType() != typeof(SliderUGUI.ValueChangedDelegate);
					Delegate obj5 = null;
					if (!flag5)
					{
						obj5 = obj2;
					}
					bool flag6 = (object)obj5 == null;
					obj4 = 0;
					array = (SettingData.DataType[])(object)typeof(SliderUGUI.ValueChangedDelegate);
					ex = (NullReferenceException)(object)obj2;
					settingResolver = (SettingResolver)(object)typeof(SliderUGUI.ValueChangedDelegate);
					if (flag6)
					{
						goto IL_0408;
					}
				}
				SettingData.DataType[] array2 = GetSupportedDataTypes();
				if (!HasValidSettingForID(ID, array2))
				{
					return;
				}
				SettingsProvider settingsProvider = base.SettingsProvider;
				bool flag7 = (object)settingsProvider == null;
				obj4 = 0;
				array = array2;
				settingResolver = this;
				if (!flag7)
				{
					Settings settings = settingsProvider.Settings;
					bool flag8 = (object)settings == null;
					obj4 = 0;
					array = array2;
					settingResolver = (SettingResolver)(object)settingsProvider;
					if (!flag8)
					{
						if (!settings.HasActiveID(ID))
						{
							return;
						}
						SettingsProvider settingsProvider2 = base.SettingsProvider;
						bool flag9 = (object)settingsProvider2 == null;
						obj4 = 0;
						array = null;
						settingResolver = this;
						if (!flag9)
						{
							Settings settings2 = settingsProvider2.Settings;
							bool flag10 = (object)settings2 == null;
							obj4 = 0;
							array = null;
							settingResolver = (SettingResolver)(object)settingsProvider2;
							if (!flag10)
							{
								ISetting setting = settings2.GetSetting(ID);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r8_v12 (Il2CppClass<Kamgam.SettingsGenerator.SliderUGUIResolver>)+240]");
								Action action = new Action(this, (IntPtr)0);
								nint num = (nint)this;
								bool flag11 = setting == null;
								obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ r8_v12 (Il2CppClass<Kamgam.SettingsGenerator.SliderUGUIResolver>)+240]");
								array = (SettingData.DataType[])0;
								settingResolver = (SettingResolver)(object)action;
								if (!flag11)
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
		}
		ex = new NullReferenceException();
		goto IL_0408;
		IL_0408:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_03fd;
		IL_03fd:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		SliderUGUI sliderUGUI = SliderUGUI;
		bool flag = sliderUGUI != null;
	}

	private void onValueChanged(float value)
	{
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_027e: Invalid comparison between F4 and O
		//IL_0114: Expected O, but got I4
		//IL_0125: Expected O, but got I4
		//IL_020f: Expected I, but got O
		//IL_01e9: Expected O, but got I4
		//IL_01fa: Expected O, but got I4
		if (stopPropagation)
		{
			return;
		}
		float num = value - _lastValue;
		float lastValue = _lastValue;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj = lastValue & 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj2 = value & 0;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2))
		{
			obj = obj2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		object obj3 = num & 0;
		float num2 = (float)obj * 1E-06f;
		float num3 = Mathf.Epsilon * 8f;
		if (num2 < num3)
		{
			num2 = num3;
		}
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num2) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3))
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
		if (!settings.HasActiveID(ID))
		{
			return;
		}
		SettingsProvider settingsProvider2 = base.SettingsProvider;
		Settings settings2 = settingsProvider2.Settings;
		SettingInt settingInt = settings2.GetInt(ID);
		bool flag = settingInt != null;
		object obj4 = 0;
		SettingInt settingInt2 = settingInt;
		object obj5 = 0;
		Settings settings3 = settings2;
		if (!flag)
		{
			SettingsProvider settingsProvider3 = base.SettingsProvider;
			Settings settings4 = settingsProvider3.Settings;
			SettingFloat settingFloat = settings4.GetFloat(ID);
			if (settingFloat != null)
			{
				nint num4 = (nint)settingFloat;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v410 @ r9_v6 (Il2CppClass<Kamgam.SettingsGenerator.SettingFloat>)+4E8] (should have been resolved before IL gen)");
				return;
			}
			SettingsProvider settingsProvider4 = base.SettingsProvider;
			Settings settings5 = settingsProvider4.Settings;
			SettingOption option = settings5.GetOption(ID);
			if (option == null)
			{
				return;
			}
			obj4 = 0;
			settingInt2 = (SettingInt)(object)option;
			obj5 = 0;
			settings3 = settings5;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
		int value2 = default(int);
		settingInt2.SetValue(value2);
	}

	public override void Refresh()
	{
		//IL_02e9: Expected F4, but got I4
		//IL_0251: Expected F4, but got I4
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
				SettingInt settingInt = settings2.GetInt(settingResolver.ID);
				if (settingInt == null)
				{
					SettingsProvider settingsProvider3 = settingResolver.SettingsProvider;
					if ((object)settingsProvider3 == null)
					{
						throw new NullReferenceException();
					}
					Settings settings3 = settingsProvider3.Settings;
					if ((object)settings3 == null)
					{
						throw new NullReferenceException();
					}
					SettingFloat settingFloat = settings3.GetFloat(settingResolver.ID);
					if (settingFloat == null)
					{
						SettingsProvider settingsProvider4 = settingResolver.SettingsProvider;
						if ((object)settingsProvider4 == null)
						{
							throw new NullReferenceException();
						}
						Settings settings4 = settingsProvider4.Settings;
						SettingOption option = settings4.GetOption(settingResolver.ID);
						if (option != null)
						{
							SliderUGUI sliderUGUI = ((SliderUGUIResolver)settingResolver).SliderUGUI;
							int value = option.GetValue();
							if ((object)sliderUGUI == null)
							{
								throw new NullReferenceException();
							}
							sliderUGUI.Value = value;
						}
						_ = 0;
					}
					else
					{
						SliderUGUI sliderUGUI2 = ((SliderUGUIResolver)settingResolver).SliderUGUI;
						float value2 = settingFloat.GetValue();
						if ((object)sliderUGUI2 == null)
						{
							throw new NullReferenceException();
						}
						float value3 = default(float);
						sliderUGUI2.Value = value3;
						_ = 0;
					}
				}
				else
				{
					SliderUGUI sliderUGUI3 = ((SliderUGUIResolver)settingResolver).SliderUGUI;
					int value4 = settingInt.GetValue();
					if ((object)sliderUGUI3 == null)
					{
						throw new NullReferenceException();
					}
					sliderUGUI3.Value = value4;
					_ = 0;
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}
}
