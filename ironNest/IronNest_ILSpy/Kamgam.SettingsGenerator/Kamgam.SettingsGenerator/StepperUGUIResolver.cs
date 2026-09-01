using System;
using Cpp2ILInjected;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class StepperUGUIResolver : SettingResolver, ISettingResolver
{
	protected StepperUGUI stepperUGUI;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public StepperUGUI StepperUGUI
	{
		get
		{
			if (this.stepperUGUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				StepperUGUI stepperUGUI = default(StepperUGUI);
				this.stepperUGUI = stepperUGUI;
			}
			return this.stepperUGUI;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_0035: Expected O, but got I4
		//IL_038c: Expected O, but got I4
		//IL_03c0: Expected O, but got I4
		//IL_01bb: Expected O, but got I4
		//IL_01fb: Expected O, but got I4
		//IL_0267: Expected O, but got I4
		//IL_02a4: Expected O, but got I4
		//IL_02f2: Expected I, but got O
		//IL_030b: Expected O, but got I4
		//IL_031b: Expected O, but got I
		base.Start();
		StepperUGUI stepperUGUI = StepperUGUI;
		SettingData.DataType dataType = GetDataType();
		bool flag = (object)stepperUGUI == null;
		SettingResolver settingResolver = this;
		NullReferenceException ex;
		if (!flag)
		{
			object obj = dataType - 1;
			bool wholeNumbers = obj == null;
			stepperUGUI.WholeNumbers = wholeNumbers;
			StepperUGUI stepperUGUI2 = StepperUGUI;
			bool flag2 = (object)stepperUGUI2 == null;
			settingResolver = this;
			if (!flag2)
			{
				StepperUGUI.OnValueChangedDelegate b = onValueChanged;
				Delegate obj2 = Delegate.Combine(stepperUGUI2.OnValueChanged, b);
				object obj4;
				SettingData.DataType[] typeFromHandle;
				if ((object)obj2 == null)
				{
					stepperUGUI2.OnValueChanged = null;
				}
				else
				{
					bool flag3 = (object)obj2.GetType() != typeof(StepperUGUI.OnValueChangedDelegate);
					Delegate obj3 = null;
					if (!flag3)
					{
						obj3 = obj2;
					}
					bool flag4 = (object)obj3 == null;
					obj4 = 0;
					typeFromHandle = (SettingData.DataType[])(object)typeof(StepperUGUI.OnValueChangedDelegate);
					if (flag4)
					{
						goto IL_03f3;
					}
					stepperUGUI2.OnValueChanged = (StepperUGUI.OnValueChangedDelegate)obj3;
					bool flag5 = (object)obj2.GetType() != typeof(StepperUGUI.OnValueChangedDelegate);
					Delegate obj5 = null;
					if (!flag5)
					{
						obj5 = obj2;
					}
					bool flag6 = (object)obj5 == null;
					obj4 = 0;
					typeFromHandle = (SettingData.DataType[])(object)typeof(StepperUGUI.OnValueChangedDelegate);
					ex = (NullReferenceException)(object)obj2;
					settingResolver = (SettingResolver)(object)typeof(StepperUGUI.OnValueChangedDelegate);
					if (flag6)
					{
						goto IL_03fe;
					}
				}
				SettingData.DataType[] array = GetSupportedDataTypes();
				if (!HasValidSettingForID(ID, array))
				{
					return;
				}
				SettingsProvider settingsProvider = base.SettingsProvider;
				bool flag7 = (object)settingsProvider == null;
				obj4 = 0;
				typeFromHandle = array;
				settingResolver = this;
				if (!flag7)
				{
					Settings settings = settingsProvider.Settings;
					bool flag8 = (object)settings == null;
					obj4 = 0;
					typeFromHandle = array;
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
						typeFromHandle = null;
						settingResolver = this;
						if (!flag9)
						{
							Settings settings2 = settingsProvider2.Settings;
							bool flag10 = (object)settings2 == null;
							obj4 = 0;
							typeFromHandle = null;
							settingResolver = (SettingResolver)(object)settingsProvider2;
							if (!flag10)
							{
								ISetting setting = settings2.GetSetting(ID);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.StepperUGUIResolver>)+240]");
								Action action = new Action(this, (IntPtr)0);
								nint num = (nint)this;
								bool flag11 = setting == null;
								obj4 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ r8_v11 (Il2CppClass<Kamgam.SettingsGenerator.StepperUGUIResolver>)+240]");
								typeFromHandle = (SettingData.DataType[])0;
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
		goto IL_03fe;
		IL_03fe:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_03f3;
		IL_03f3:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
	}

	public override void OnDestroy()
	{
		//IL_0142: Expected I, but got O
		//IL_0175: Expected O, but got I4
		//IL_0183: Expected I, but got O
		//IL_01a9: Expected O, but got I4
		//IL_01b7: Expected I, but got O
		base.OnDestroy();
		StepperUGUI stepperUGUI = StepperUGUI;
		if (!(stepperUGUI != null))
		{
			return;
		}
		StepperUGUI stepperUGUI2 = StepperUGUI;
		if ((object)stepperUGUI2 != null)
		{
			StepperUGUI.OnValueChangedDelegate value = onValueChanged;
			Delegate obj = Delegate.Remove(stepperUGUI2.OnValueChanged, value);
			if ((object)obj == null)
			{
				stepperUGUI2.OnValueChanged = null;
				return;
			}
			bool flag = (object)obj.GetType() != typeof(StepperUGUI.OnValueChangedDelegate);
			Delegate obj2 = null;
			if (!flag)
			{
				obj2 = obj;
			}
			bool flag2 = (object)obj2 == null;
			object obj3 = 0;
			nint num = (nint)typeof(StepperUGUI.OnValueChangedDelegate);
			if (flag2)
			{
				goto IL_01cd;
			}
			stepperUGUI2.OnValueChanged = (StepperUGUI.OnValueChangedDelegate)obj2;
			bool flag3 = (object)obj.GetType() != typeof(StepperUGUI.OnValueChangedDelegate);
			Delegate obj4 = null;
			if (!flag3)
			{
				obj4 = obj;
			}
			bool flag4 = (object)obj4 == null;
			obj3 = 0;
			num = (nint)typeof(StepperUGUI.OnValueChangedDelegate);
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

	private void onValueChanged(float value)
	{
		//IL_0174: Expected I, but got O
		//IL_0184: Expected O, but got I
		//IL_0194: Expected O, but got I
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
			SettingInt settingInt = settings2.GetInt(ID);
			if (settingInt == null)
			{
				SettingsProvider settingsProvider3 = base.SettingsProvider;
				Settings settings3 = settingsProvider3.Settings;
				settings3.GetFloat(ID)?.SetValue(value);
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9370");
			nint num = (nint)settingInt;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingInt>)+4E8]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v269 @ r9_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingInt>)+4F0]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v90 @ r10_v1 (should have been resolved before IL gen)");
		}
	}

	public override void Refresh()
	{
		//IL_0210: Expected F4, but got I4
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
					SettingFloat settingFloat = settings3.GetFloat(settingResolver.ID);
					if (settingFloat != null)
					{
						StepperUGUI stepperUGUI = ((StepperUGUIResolver)settingResolver).StepperUGUI;
						float value = settingFloat.GetValue();
						if ((object)stepperUGUI == null)
						{
							throw new NullReferenceException();
						}
						float value2 = default(float);
						stepperUGUI.Value = value2;
					}
					_ = 0;
				}
				else
				{
					StepperUGUI stepperUGUI2 = ((StepperUGUIResolver)settingResolver).StepperUGUI;
					int value3 = settingInt.GetValue();
					if ((object)stepperUGUI2 == null)
					{
						throw new NullReferenceException();
					}
					stepperUGUI2.Value = value3;
					_ = 0;
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	public StepperUGUIResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[2];
		_ = 1;
		_ = 2;
		supportedDataTypes = array;
		((MonoBehaviour)this)._002Ector();
	}
}
