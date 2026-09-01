using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator;

public abstract class SettingEvent : SettingResolver
{
	public bool TriggerOnStart = true;

	public bool TriggerOnEnable;

	public bool TriggerIfDisabled;

	[NonSerialized]
	protected SettingData.DataType[] _supportedDataTypes;

	public abstract override SettingData.DataType[] GetSupportedDataTypes();

	public ISetting GetSetting()
	{
		SettingsProvider settingsProvider = base.SettingsProvider;
		if ((object)settingsProvider != null)
		{
			Settings settings = settingsProvider.Settings;
			if ((object)settings != null)
			{
				if (!settings.HasActiveID(ID))
				{
					return null;
				}
				SettingsProvider settingsProvider2 = base.SettingsProvider;
				if ((object)settingsProvider2 != null)
				{
					Settings settings2 = settingsProvider2.Settings;
					if ((object)settings2 != null)
					{
						return settings2.GetSetting(ID);
					}
				}
			}
		}
		return (ISetting)new NullReferenceException();
	}

	public override void Start()
	{
		//IL_002a: Expected I, but got O
		//IL_003a: Expected O, but got I
		//IL_004a: Expected O, but got I
		base.Start();
		if (TriggerOnStart)
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+268]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+270]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v17 @ rax_v2 (should have been resolved before IL gen)");
		}
	}

	public override void OnEnable()
	{
		//IL_0073: Expected I, but got O
		base.OnEnable();
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (settings.HasActiveID(ID))
		{
			ISetting setting = GetSetting();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v175 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+250]");
			Action<ISetting> action = new Action<ISetting>(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
		if (TriggerOnEnable)
		{
			TriggerEvent();
		}
	}

	public override void OnDisable()
	{
		//IL_0073: Expected I, but got O
		base.OnDisable();
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (settings.HasActiveID(ID))
		{
			ISetting setting = GetSetting();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+250]");
			Action<ISetting> action = new Action<ISetting>(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public void Register()
	{
		//IL_006d: Expected I, but got O
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (settings.HasActiveID(ID))
		{
			ISetting setting = GetSetting();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+250]");
			Action<ISetting> action = new Action<ISetting>(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	public void UnRegister()
	{
		//IL_006d: Expected I, but got O
		SettingsProvider settingsProvider = base.SettingsProvider;
		Settings settings = settingsProvider.Settings;
		if (settings.HasActiveID(ID))
		{
			ISetting setting = GetSetting();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r8_v4 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+250]");
			Action<ISetting> action = new Action<ISetting>(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
		}
	}

	protected virtual void onChanged(ISetting setting)
	{
		//IL_0028: Expected I, but got O
		//IL_0038: Expected O, but got I
		//IL_0048: Expected O, but got I
		if (shoudTrigger())
		{
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rdx_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+268]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rdx_v3 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+270]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v26 @ rax_v3 (should have been resolved before IL gen)");
		}
	}

	public override void Refresh()
	{
		//IL_0005: Expected I, but got O
		//IL_0015: Expected O, but got I
		//IL_0025: Expected O, but got I
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+268]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v0 @ rdx_v1 (Il2CppClass<Kamgam.SettingsGenerator.SettingEvent>)+270]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v2 @ rax_v1 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public virtual bool shoudTrigger()
	{
		//IL_00b1: Expected I4, but got O
		if (!TriggerIfDisabled)
		{
			GameObject gameObject = base.gameObject;
			if (gameObject != null)
			{
				GameObject gameObject2 = base.gameObject;
				if ((object)gameObject2 == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				if (gameObject2.activeInHierarchy)
				{
					return base.isActiveAndEnabled;
				}
			}
			return false;
		}
		return true;
	}

	public abstract void TriggerEvent();
}
public abstract class SettingEvent<T> : SettingEvent
{
	public UnityEvent<T> OnValueChanged;

	public unsafe void Log(T value)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0057: Expected O, but got I
		//IL_00ad: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = 0;
		IntPtr intPtr = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingEvent`1>>)+FC]");
		object obj3 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rax_v3 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingEvent`1>>)+FC]");
		if ((nint)obj3 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 24));
			nint num2 = 0;
			IntPtr intPtr2 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rax_v9 (Il2CppClass<Il2CppRgctx<Kamgam.SettingsGenerator.SettingEvent`1>>)+28]");
			if ((nint)0 < (nint)0)
			{
				val = value;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object message = default(object);
		Debug.Log(message);
	}
}
