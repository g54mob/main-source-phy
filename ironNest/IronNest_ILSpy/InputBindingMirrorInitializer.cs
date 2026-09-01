using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using Kamgam.SettingsGenerator;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBindingMirrorInitializer : MonoBehaviour
{
	private SettingsProvider _settingsProvider;

	private InputActionAsset _inputActionAsset;

	private InputBindingMirrorsConfig _mirrorConfig;

	private static InputBindingMirrorInitializer Instance;

	private void Awake()
	{
		if (!Instance)
		{
			Instance = this;
			GameObject target = base.gameObject;
			UnityEngine.Object.DontDestroyOnLoad(target);
		}
		else
		{
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void OnDestroy()
	{
		CleanupMirrors();
		Instance = null;
	}

	private void Start()
	{
		ConfigureMirrors();
	}

	private unsafe void ConfigureMirrors()
	{
		//IL_008d: Expected O, but got Ref
		//IL_00e8: Expected I, but got O
		//IL_017b: Expected O, but got I4
		//IL_0120: Expected O, but got I
		//IL_0129: Expected O, but got I4
		//IL_024a: Expected O, but got I
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_029d: Expected I, but got O
		//IL_0222: Expected I, but got O
		if (!(_settingsProvider != null))
		{
			return;
		}
		Settings settings = _settingsProvider.Settings;
		if (!(settings != null))
		{
			return;
		}
		Settings settings2 = _settingsProvider.Settings;
		IEnumerable<InputBindingMirror> bindingMirrors = _mirrorConfig.BindingMirrors;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		InputBindingMirrorInitializer inputBindingMirrorInitializer = default(InputBindingMirrorInitializer);
		object obj = (object)(&inputBindingMirrorInitializer);
		InputBindingMirrorInitializer inputBindingMirrorInitializer2 = null;
		object obj2 = default(object);
		object obj11 = default(object);
		InputBindingMirror inputBindingMirror = default(InputBindingMirror);
		while (true)
		{
			object obj10;
			object obj3;
			if ((object)inputBindingMirrorInitializer != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = (object)inputBindingMirrorInitializer == null;
					inputBindingMirrorInitializer2 = null;
					if (!flag)
					{
						nint num = (nint)inputBindingMirrorInitializer;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ r10_v6 (Il2CppClass<InputBindingMirrorInitializer>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0160;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ r10_v6 (Il2CppClass<InputBindingMirrorInitializer>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ r8_v12+v450 @ rax_v44*8]");
							if (0 == (nint)typeof(IEnumerator<InputBindingMirror>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v306 @ r10_v6 (Il2CppClass<InputBindingMirrorInitializer>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_0160;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v512 @ r8_v12+8+v506 @ rcx_v36*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_036b;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_0160:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj11;
			obj3 = 0;
			goto IL_036b;
			IL_036b:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v511 @ rdx_v16] (should have been resolved before IL gen)");
			if (inputBindingMirror != null)
			{
				if ((object)settings2 == null)
				{
					break;
				}
				if (settings2.HasID(inputBindingMirror._settingsId))
				{
					SettingString settingString = settings2.GetString(inputBindingMirror._settingsId);
					Action<SettingString> value = OnSettingStringChanged;
					settingString.OnSettingStringChanged += value;
					ApplyMirrors(settingString, inputBindingMirror);
					nint num2 = unchecked((nint)null);
				}
				else
				{
					string message = "Can't find setting: " + inputBindingMirror._settingsId;
					Debug.LogError(message);
					nint num2 = (nint)typeof(IEnumerator<InputBindingMirror>);
				}
				continue;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private unsafe void CleanupMirrors()
	{
		//IL_008d: Expected O, but got Ref
		//IL_00e8: Expected I, but got O
		//IL_017b: Expected O, but got I4
		//IL_0120: Expected O, but got I
		//IL_0129: Expected O, but got I4
		//IL_023d: Expected O, but got I
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_01b2: Expected O, but got I
		//IL_0273: Expected O, but got I
		//IL_0293: Expected I, but got O
		//IL_01e4: Expected O, but got I
		//IL_0215: Expected I, but got O
		if (!(_settingsProvider != null))
		{
			return;
		}
		Settings settings = _settingsProvider.Settings;
		if (!(settings != null))
		{
			return;
		}
		Settings settings2 = _settingsProvider.Settings;
		IEnumerable<InputBindingMirror> bindingMirrors = _mirrorConfig.BindingMirrors;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		SettingString settingString = default(SettingString);
		object obj = (object)(&settingString);
		SettingString settingString2 = null;
		object obj2 = default(object);
		object obj11 = default(object);
		object obj12 = default(object);
		while (true)
		{
			object obj10;
			object obj3;
			if (settingString != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = settingString == null;
					settingString2 = null;
					if (!flag)
					{
						nint num = (nint)settingString;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r10_v6 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_0160;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r10_v6 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v502 @ r8_v12+v440 @ rax_v43*8]");
							if (0 == (nint)typeof(IEnumerator<InputBindingMirror>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v297 @ r10_v6 (Il2CppClass<Kamgam.SettingsGenerator.SettingString>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_0160;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v502 @ r8_v12+8+v496 @ rcx_v35*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_0361;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_0160:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj11;
			obj3 = 0;
			goto IL_0361;
			IL_0361:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v501 @ rdx_v16] (should have been resolved before IL gen)");
			if (obj12 != null)
			{
				if ((object)settings2 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v518 @ rax_v24+10]");
				if (settings2.HasID((string)0))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v518 @ rax_v24+10]");
					SettingString settingString3 = settings2.GetString((string)0);
					Action<SettingString> value = OnSettingStringChanged;
					settingString3.OnSettingStringChanged -= value;
					nint num2 = unchecked((nint)null);
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v518 @ rax_v24+10]");
					string message = "Can't find setting: " + (string)0;
					Debug.LogError(message);
					nint num2 = (nint)typeof(IEnumerator<InputBindingMirror>);
				}
				continue;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private unsafe void ApplyMirrors(SettingString setting, InputBindingMirror bindingMirror)
	{
		//IL_0270: Expected I4, but got I8
		//IL_004d: Expected O, but got Ref
		//IL_00a8: Expected I, but got O
		//IL_0133: Expected O, but got I4
		//IL_00e0: Expected O, but got I
		//IL_00e9: Expected O, but got I4
		//IL_0160: Expected O, but got I
		//IL_0169: Expected I, but got O
		//IL_01e0: Expected O, but got I
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_0196: Expected O, but got I
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		string value = setting.GetValue();
		InputBindingMirror._003Cget_BindingsToMirror_003Ed__5 obj = new InputBindingMirror._003Cget_BindingsToMirror_003Ed__5(0);
		obj._003C_003E1__state = -2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180E439F0");
		int num = default(int);
		obj._003C_003El__initialThreadId = num;
		obj._003C_003E4__this = bindingMirror;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		InputActionAsset inputActionAsset = default(InputActionAsset);
		object obj2 = (object)(&inputActionAsset);
		InputActionAsset inputActionAsset2 = null;
		object obj3 = default(object);
		object obj12 = default(object);
		string overrideProcessors = default(string);
		while (true)
		{
			object obj4;
			object obj11;
			if ((object)inputActionAsset != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj3 != null)
				{
					bool flag = (object)inputActionAsset == null;
					inputActionAsset2 = null;
					if (flag)
					{
						break;
					}
					nint num2 = (nint)inputActionAsset;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v5 (Il2CppClass<UnityEngine.InputSystem.InputActionAsset>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_0120;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v5 (Il2CppClass<UnityEngine.InputSystem.InputActionAsset>)+B0]");
					obj4 = 0;
					object obj5 = 0;
					while (true)
					{
						object obj6 = obj5 + obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r8_v9+v362 @ rcx_v26*8]");
						if (0 == (nint)typeof(IEnumerator<InputBindingReference>))
						{
							break;
						}
						obj5++;
						object obj7 = obj5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v218 @ r10_v5 (Il2CppClass<UnityEngine.InputSystem.InputActionAsset>)+12E]");
						if ((nint)obj7 < 0)
						{
							continue;
						}
						goto IL_0120;
					}
					object obj8 = obj5 + obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ r8_v9+8+v416 @ rcx_v28*8]");
					object obj9 = (nint)0 << 4;
					object obj10 = obj9 + 312;
					obj11 = obj10 + num2;
					goto IL_02de;
				}
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_0120:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj4 = 0;
			obj11 = obj12;
			goto IL_02de;
			IL_02de:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v421 @ rdx_v14] (should have been resolved before IL gen)");
			InputActionAsset inputActionAsset3 = _inputActionAsset;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v435 @ rax_v24+18]");
			bool flag2 = InputActionRebindingExtensionsExtensions.ApplyBindingOverrideWithResult(inputActionAsset3, (string)0, value, null, overrideProcessors);
			nint num3 = unchecked((nint)null);
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v435 @ rax_v24+18]");
				string message = "Failed to apply binding: " + value + " to binding " + (string)0;
				Debug.LogError(message);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v435 @ rax_v24+18]");
				num3 = 0;
			}
		}
		throw new NullReferenceException();
	}

	private unsafe void OnSettingStringChanged(SettingString setting)
	{
		//IL_002b: Expected O, but got Ref
		IEnumerable<InputBindingMirror> bindingMirrors = _mirrorConfig.BindingMirrors;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj2 = default(object);
		object obj = (object)(&obj2);
		SettingWithValue<string> settingWithValue = null;
		object obj3 = default(object);
		InputBindingMirror inputBindingMirror = default(InputBindingMirror);
		while (obj2 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			if (obj3 != null)
			{
				bool flag = obj2 == null;
				settingWithValue = null;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (setting.MatchesID(inputBindingMirror._settingsId))
					{
						ApplyMirrors(setting, inputBindingMirror);
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						return;
					}
					continue;
				}
				throw new NullReferenceException();
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			return;
		}
		throw new NullReferenceException();
	}
}
