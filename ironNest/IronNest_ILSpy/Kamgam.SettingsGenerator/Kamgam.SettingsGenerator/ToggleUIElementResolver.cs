using System;
using Cpp2ILInjected;
using UnityEngine.UIElements;

namespace Kamgam.SettingsGenerator;

public class ToggleUIElementResolver : SettingResolverForVisualElement, ISettingResolver
{
	protected Toggle _toggle;

	protected SettingData.DataType[] supportedDataTypes;

	protected bool stopPropagation;

	public Toggle Toggle
	{
		get
		{
			//IL_0095: Expected I, but got O
			//IL_00a3: Expected I, but got O
			//IL_00b3: Expected O, but got I
			//IL_00ef: Expected O, but got I
			//IL_0114: Expected O, but got I4
			//IL_024e: Expected I, but got O
			//IL_0256: Expected I, but got O
			//IL_0266: Expected O, but got I
			//IL_0149: Expected O, but got I
			//IL_016e: Expected O, but got I4
			if (_toggle == null)
			{
				VisualElement visualElement = base.VisualElement;
				if (visualElement != null)
				{
					goto IL_0057;
				}
			}
			VisualElement visualElement2 = base.VisualElement;
			if (_toggle != visualElement2)
			{
				goto IL_0057;
			}
			goto IL_01d6;
			IL_01fc:
			Toggle toggle;
			bool flag = toggle == null;
			VisualElement toggle2 = null;
			VisualElement visualElement3;
			if (!flag)
			{
				toggle2 = visualElement3;
			}
			Toggle toggle3;
			do
			{
				_toggle = (Toggle)toggle2;
				nint num = (nint)typeof(Toggle);
				nint num2 = (nint)visualElement3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v4 (Il2CppClass<UnityEngine.UIElements.Toggle>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r11_v4 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r10_v4 (Il2CppClass<UnityEngine.UIElements.Toggle>)+130]");
				if (num3 >= 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r11_v4 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v287 @ rax_v18+FFFFFFF8+v271 @ rax_v15*8]");
					bool flag2 = 0 == (nint)typeof(Toggle);
					toggle3 = (Toggle)1;
					if (flag2)
					{
						continue;
					}
				}
				toggle3 = null;
			}
			while (toggle3 != null);
			goto IL_01dd;
			IL_01d6:
			return _toggle;
			IL_0057:
			visualElement3 = base.VisualElement;
			if (visualElement3 == null)
			{
				_toggle = null;
				goto IL_01dd;
			}
			nint num4 = (nint)visualElement3;
			nint num5 = (nint)typeof(Toggle);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v3 (Il2CppClass<UnityEngine.UIElements.Toggle>)+130]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r11_v3 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+130]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ r10_v3 (Il2CppClass<UnityEngine.UIElements.Toggle>)+130]");
			if (num6 >= 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ r11_v3 (Il2CppClass<UnityEngine.UIElements.VisualElement>)+C8]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ rax_v21+FFFFFFF8+v175 @ rax_v11*8]");
				bool flag3 = 0 == (nint)typeof(Toggle);
				toggle = (Toggle)1;
				if (flag3)
				{
					goto IL_01fc;
				}
			}
			toggle = null;
			goto IL_01fc;
			IL_01dd:
			if (_toggle != null)
			{
				EventCallback<ChangeEvent<bool>> callback = onValueChanged;
				bool flag4 = INotifyValueChangedExtensions.RegisterValueChangedCallback(_toggle, callback);
			}
			goto IL_01d6;
		}
	}

	public override SettingData.DataType[] GetSupportedDataTypes()
	{
		return supportedDataTypes;
	}

	public override void Start()
	{
		//IL_0059: Expected I, but got O
		base.Start();
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (HasValidSettingForID(ID, allowedTypes))
		{
			SettingsProvider settingsProvider = base.SettingsProvider;
			Settings settings = settingsProvider.Settings;
			ISetting setting = settings.GetSetting(ID);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r8_v5 (Il2CppClass<Kamgam.SettingsGenerator.ToggleUIElementResolver>)+240]");
			Action action = new Action(this, (IntPtr)0);
			nint num = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180003ED0");
			Refresh();
		}
	}

	public override void OnDisable()
	{
		_toggle = null;
		base.resetUIElements();
		StopAllCoroutines();
		((SettingResolver)this).OnDisable();
	}

	public override void OnDestroy()
	{
		base.resetUIElements();
		BindingClass = null;
		StopAllCoroutines();
		((SettingResolver)this).OnDestroy();
		Toggle toggle = Toggle;
		if (toggle != null)
		{
			Toggle toggle2 = Toggle;
			EventCallback<ChangeEvent<bool>> callback = onValueChanged;
			bool flag = INotifyValueChangedExtensions.UnregisterValueChangedCallback(toggle2, callback);
		}
	}

	protected void onValueChanged(ChangeEvent<bool> evt)
	{
		if (stopPropagation)
		{
			return;
		}
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (HasValidSettingForID(ID, allowedTypes) && HasActiveSettingForID(ID))
		{
			SettingsProvider settingsProvider = base.SettingsProvider;
			Settings settings = settingsProvider.Settings;
			SettingBool settingBool = settings.GetBool(ID);
			if (settingBool != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18077C4C0");
				bool value = default(bool);
				settingBool.SetValue(value);
			}
		}
	}

	public override void Refresh()
	{
		SettingData.DataType[] allowedTypes = GetSupportedDataTypes();
		if (!HasValidSettingForID(ID, allowedTypes) || !HasActiveSettingForID(ID))
		{
			return;
		}
		_ = 1;
		SettingResolver settingResolver = default(SettingResolver);
		SettingsProvider settingsProvider = settingResolver.SettingsProvider;
		if ((object)settingsProvider != null)
		{
			Settings settings = settingsProvider.Settings;
			SettingBool settingBool = settings.GetBool(settingResolver.ID);
			if (settingBool != null)
			{
				Toggle toggle = ((ToggleUIElementResolver)settingResolver).Toggle;
				bool value = settingBool.GetValue();
				if (toggle == null)
				{
					throw new NullReferenceException();
				}
				toggle.value = value;
			}
			_ = 0;
			return;
		}
		throw new NullReferenceException();
	}

	public ToggleUIElementResolver()
	{
		SettingData.DataType[] array = new SettingData.DataType[1];
		_ = 3;
		supportedDataTypes = array;
		((SettingResolver)this)._002Ector();
	}
}
