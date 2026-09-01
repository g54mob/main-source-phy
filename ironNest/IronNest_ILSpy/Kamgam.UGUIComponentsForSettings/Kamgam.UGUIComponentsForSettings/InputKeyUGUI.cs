using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace Kamgam.UGUIComponentsForSettings;

public class InputKeyUGUI : MonoBehaviour
{
	public delegate void OnChangedDelegate(UniversalKeyCode key, UniversalKeyCode modifierKey);

	public Func<UniversalKeyCode, string> KeyCodeToKeyNameFunc;

	protected UniversalKeyCode _key;

	protected UniversalKeyCode _modifierKey;

	public bool AllowMouseButtons;

	public bool AllowKeyCombinations;

	public bool AllowAbortWithCancelButton;

	public UnityEvent<UniversalKeyCode, UniversalKeyCode> OnChangedEvent;

	public OnChangedDelegate OnChanged;

	public Button Button;

	public GameObject Normal;

	public GameObject Active;

	public TextMeshProUGUI TextTf;

	public TextMeshProUGUI KeyNameTf;

	public TextMeshProUGUI ActiveTextTf;

	protected bool waitForKeyRelease;

	protected UniversalKeyCode _modifierKeyWhileActive;

	protected UniversalKeyCode _keyWhileActive;

	protected bool _aKeyWasPressedWhileActive;

	public UniversalKeyCode Key
	{
		get
		{
			return _key;
		}
		set
		{
			if (value != _key)
			{
				_key = value;
				UpdateKeyName();
			}
		}
	}

	public UniversalKeyCode ModifierKey
	{
		get
		{
			return _modifierKey;
		}
		set
		{
			if (value != _modifierKey)
			{
				_modifierKey = value;
				UpdateKeyName();
			}
		}
	}

	public bool IsActive
	{
		get
		{
			//IL_0041: Expected I4, but got O
			if ((object)Active != null)
			{
				return Active.activeSelf;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
	}

	public string Text
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI textTf = TextTf;
			if ((object)TextTf != null)
			{
				nint num = (nint)textTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = TextTf.text;
			if (value != text)
			{
				TextTf.text = value;
			}
		}
	}

	public string KeyName
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI keyNameTf = KeyNameTf;
			if ((object)KeyNameTf != null)
			{
				nint num = (nint)keyNameTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = KeyNameTf.text;
			if (value != text)
			{
				KeyNameTf.text = value;
			}
		}
	}

	public string ActiveText
	{
		get
		{
			//IL_0031: Expected I, but got O
			TextMeshProUGUI activeTextTf = ActiveTextTf;
			if ((object)ActiveTextTf != null)
			{
				nint num = (nint)activeTextTf;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: [v12 @ rdx_v1 (Il2CppClass<TMPro.TextMeshProUGUI>)+548] (should have been resolved before IL gen)");
			}
			return (string)(object)new NullReferenceException();
		}
		set
		{
			string text = ActiveTextTf.text;
			if (value != text)
			{
				ActiveTextTf.text = value;
			}
		}
	}

	public void SetActive(bool active)
	{
		//IL_0063: Expected O, but got I4
		//IL_0199: Expected O, but got I4
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_0050: Expected O, but got I4
		bool activeSelf = Active.activeSelf;
		bool activeSelf2 = Active.activeSelf;
		object obj = ((activeSelf2 != active) ? ((object)((active ? 1 : 0) ^ 1)) : ((object)0));
		object obj2 = activeSelf ^ active;
		object obj3 = active & obj2;
		if (obj3 != null)
		{
			InputUtils.ResetStuckKeyStates();
			if (InputUtils.AnyKey())
			{
				waitForKeyRelease = true;
			}
		}
		if (obj != null)
		{
			EventSystem current = EventSystem.current;
			if (current != null)
			{
				GameObject go = Button.gameObject;
				SelectionUtils.SetSelected(go);
			}
		}
		bool active2 = (byte)((active ? 1u : 0u) ^ 1u) != 0;
		Normal.SetActive(active2);
		Active.SetActive(active);
		bool interactable = (byte)((active ? 1u : 0u) ^ 1u) != 0;
		Button.interactable = interactable;
		if (active)
		{
			_modifierKeyWhileActive = UniversalKeyCode.None;
			_aKeyWasPressedWhileActive = false;
		}
	}

	public void UpdateKeyName()
	{
		Func<UniversalKeyCode, string> keyCodeToKeyNameFunc = KeyCodeToKeyNameFunc;
		string keyName;
		string text2;
		if (_modifierKey == UniversalKeyCode.None)
		{
			if (KeyCodeToKeyNameFunc != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v39 @ rax_v2 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
				string text = default(string);
				keyName = text;
				goto IL_0121;
			}
			text2 = InputUtils.UniversalKeyName(_key);
		}
		else
		{
			string text3;
			string text5;
			if (KeyCodeToKeyNameFunc != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v39 @ rax_v2 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
				Func<UniversalKeyCode, string> keyCodeToKeyNameFunc2 = KeyCodeToKeyNameFunc;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v98 @ rcx_v12 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
				string text4 = default(string);
				text3 = text4;
				string text6 = default(string);
				text5 = text6;
			}
			else
			{
				string text7 = InputUtils.UniversalKeyName(_modifierKey);
				string text8 = InputUtils.UniversalKeyName(_key);
				text3 = text8;
				text5 = text7;
			}
			text2 = text5 + " + " + text3;
		}
		keyName = text2;
		goto IL_0121;
		IL_0121:
		KeyName = keyName;
	}

	public bool IsCancelKeyPressed()
	{
		return InputUtils.CancelDown();
	}

	public void OnEnable()
	{
		UpdateKeyName();
	}

	public void OnDisable()
	{
		if (Active.activeSelf)
		{
			waitForKeyRelease = false;
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 36 Invalid \"Jump target not found in method: 0x180A5B830\"");
		}
	}

	public void Refresh()
	{
		UpdateKeyName();
	}

	public void Update()
	{
		//IL_0168: Expected O, but got I
		//IL_0207: Expected I, but got O
		//IL_0e9e: Expected I, but got O
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_0228: Expected I, but got O
		//IL_0d0d: Expected I4, but got O
		//IL_0d37: Expected I4, but got O
		//IL_0252: Expected I, but got O
		//IL_027c: Expected I, but got O
		//IL_0b46: Expected O, but got I
		//IL_02aa: Expected O, but got I
		//IL_02b3: Expected I, but got O
		//IL_0bc1: Expected O, but got I
		//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Expected I4, but got Unknown
		//IL_0aae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab3: Expected I4, but got Unknown
		//IL_02e1: Expected O, but got I
		//IL_02f5: Expected I, but got O
		//IL_02fa: Expected I, but got O
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1a: Expected O, but got Unknown
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Expected O, but got Unknown
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Expected O, but got Unknown
		//IL_0890: Expected O, but got I
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Expected O, but got Unknown
		//IL_08f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f6: Expected O, but got Unknown
		//IL_091f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Expected O, but got Unknown
		//IL_092d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0932: Expected O, but got Unknown
		//IL_0953: Expected O, but got I
		//IL_0963: Expected O, but got I
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Expected O, but got Unknown
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Expected O, but got Unknown
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Expected O, but got Unknown
		//IL_04f8: Expected O, but got I
		//IL_070f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Expected O, but got Unknown
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Expected O, but got Unknown
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Expected O, but got Unknown
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Expected O, but got Unknown
		//IL_06ab: Expected O, but got I
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Expected O, but got Unknown
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Expected O, but got Unknown
		//IL_05bd: Expected O, but got I
		//IL_05cd: Expected O, but got I
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Expected O, but got Unknown
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Expected O, but got Unknown
		//IL_077f: Expected O, but got I
		//IL_078f: Expected O, but got I
		bool flag = InputUtils.AnyKey();
		if (!flag)
		{
			waitForKeyRelease = flag;
		}
		if (!Active.activeSelf || waitForKeyRelease)
		{
			return;
		}
		if (AllowAbortWithCancelButton && InputUtils.CancelDown())
		{
			SetActive(active: false);
		}
		List<UniversalKeyCode> tmpUniversalKeyResults = InputUtils._tmpUniversalKeyResults;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rbx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<UniversalKeyCode>())
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rbx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rbx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rbx_v4 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		List<UniversalKeyCode> list = InputUtils._tmpUniversalKeyResults;
		List<UniversalKeyCode> universalKeysUp = InputUtils.GetUniversalKeysUp(excludeModifierKeys: false, excludeMouseButtons: true, InputUtils._tmpUniversalKeyResults);
		List<UniversalKeyCode> tmpUniversalKeyResults2 = InputUtils._tmpUniversalKeyResults;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v113 @ rdx_v7 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
		object obj = default(object);
		UniversalKeyCode universalKeyCode;
		if ((nint)0 != 0)
		{
			list = (List<UniversalKeyCode>)(obj + 24);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+18]");
			universalKeyCode = UniversalKeyCode.None;
			nint num2 = 0;
		}
		else
		{
			universalKeyCode = UniversalKeyCode.None;
			nint num2 = unchecked((nint)null);
		}
		bool flag2 = Mouse._003Ccurrent_003Ek__BackingField == null;
		nint num3 = (nint)typeof(Mouse);
		if (flag2)
		{
			goto IL_0323;
		}
		Mouse mouse = Mouse._003Ccurrent_003Ek__BackingField;
		bool wasReleasedThisFrame = mouse._003CleftButton_003Ek__BackingField.wasReleasedThisFrame;
		nint num4 = unchecked((nint)null);
		if (!wasReleasedThisFrame)
		{
			Mouse mouse2 = Mouse._003Ccurrent_003Ek__BackingField;
			bool wasReleasedThisFrame2 = mouse2._003CmiddleButton_003Ek__BackingField.wasReleasedThisFrame;
			num4 = unchecked((nint)null);
			if (!wasReleasedThisFrame2)
			{
				Mouse mouse3 = Mouse._003Ccurrent_003Ek__BackingField;
				bool wasReleasedThisFrame3 = mouse3._003CrightButton_003Ek__BackingField.wasReleasedThisFrame;
				num4 = unchecked((nint)null);
				if (!wasReleasedThisFrame3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v134 @ rax_v174+1E8]");
					bool wasReleasedThisFrame4 = ((ButtonControl)0).wasReleasedThisFrame;
					num4 = unchecked((nint)null);
					if (!wasReleasedThisFrame4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3FA0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rax_v176+1E0]");
						bool wasReleasedThisFrame5 = ((ButtonControl)0).wasReleasedThisFrame;
						bool flag3 = !wasReleasedThisFrame5;
						num4 = unchecked((nint)null);
						num3 = unchecked((nint)null);
						if (flag3)
						{
							goto IL_0323;
						}
					}
				}
			}
		}
		bool flag4 = true;
		num3 = num4;
		goto IL_0339;
		IL_0a5f:
		OnChangedDelegate onChanged = OnChanged;
		if (OnChanged != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: v1423.invoke_impl (System.IntPtr) (should have been resolved before IL gen)");
		}
		UniversalKeyCode arg = (UniversalKeyCode)(obj + 24);
		UniversalKeyCode arg2 = (UniversalKeyCode)(obj + 40);
		_ = _modifierKey;
		_ = _key;
		OnChangedEvent.Invoke(arg2, arg);
		goto IL_0adf;
		IL_0e20:
		string keyName;
		KeyName = keyName;
		goto IL_0a28;
		IL_061c:
		if (_modifierKeyWhileActive != _key)
		{
			_key = _modifierKeyWhileActive;
			Func<UniversalKeyCode, string> keyCodeToKeyNameFunc = KeyCodeToKeyNameFunc;
			if (_modifierKey == UniversalKeyCode.None)
			{
				if (KeyCodeToKeyNameFunc != null)
				{
					_ = _key;
					object obj2 = obj + 40;
					object obj3 = obj + 24;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v138 @ rax_v106 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+28]");
					KeyName = (string)0;
				}
				else
				{
					string keyName2 = InputUtils.UniversalKeyName(_key);
					KeyName = keyName2;
				}
			}
			else
			{
				string text;
				string text2;
				if (KeyCodeToKeyNameFunc != null)
				{
					Func<UniversalKeyCode, string> keyCodeToKeyNameFunc2 = KeyCodeToKeyNameFunc;
					object obj4 = obj - 40;
					object obj5 = obj + 24;
					_ = _modifierKey;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v159 @ rcx_v94 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
					Func<UniversalKeyCode, string> keyCodeToKeyNameFunc3 = KeyCodeToKeyNameFunc;
					object obj6 = obj + 48;
					object obj7 = obj + 40;
					_ = _key;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v160 @ rcx_v96 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+30]");
					text = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-28]");
					text2 = (string)0;
				}
				else
				{
					string text3 = InputUtils.UniversalKeyName(_modifierKey);
					string text4 = InputUtils.UniversalKeyName(_key);
					text = text4;
					text2 = text3;
				}
				string keyName3 = text2 + " + " + text;
				KeyName = keyName3;
			}
		}
		goto IL_0a5f;
		IL_0a1b:
		string text5;
		keyName = text5;
		goto IL_0e20;
		IL_08ae:
		Func<UniversalKeyCode, string> keyCodeToKeyNameFunc4;
		string text6;
		string text7;
		if (keyCodeToKeyNameFunc4 != null)
		{
			Func<UniversalKeyCode, string> keyCodeToKeyNameFunc5 = KeyCodeToKeyNameFunc;
			object obj8 = obj - 40;
			object obj9 = obj + 24;
			_ = _modifierKey;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v161 @ rcx_v73 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
			Func<UniversalKeyCode, string> keyCodeToKeyNameFunc6 = KeyCodeToKeyNameFunc;
			object obj10 = obj + 48;
			object obj11 = obj + 40;
			_ = _key;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v162 @ rcx_v75 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+30]");
			text6 = (string)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-28]");
			text7 = (string)0;
		}
		else
		{
			string text8 = InputUtils.UniversalKeyName(_modifierKey);
			string text9 = InputUtils.UniversalKeyName(_key);
			text6 = text9;
			text7 = text8;
		}
		text5 = text7 + " + " + text6;
		goto IL_0a1b;
		IL_084b:
		Func<UniversalKeyCode, string> keyCodeToKeyNameFunc7 = KeyCodeToKeyNameFunc;
		object obj12 = obj + 40;
		object obj13 = obj + 24;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1642 @ rcx_v79 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+28]");
		keyName = (string)0;
		goto IL_0e20;
		IL_0d79:
		string keyName4;
		KeyName = keyName4;
		goto IL_061c;
		IL_0a28:
		if (_keyWhileActive != _key)
		{
			_key = _keyWhileActive;
			UpdateKeyName();
		}
		goto IL_0a5f;
		IL_0339:
		bool flag5 = InputUtils.MouseWheelUsed();
		bool flag6 = !flag5;
		bool flag7 = (byte)num3 != 0;
		if (!flag6)
		{
			_aKeyWasPressedWhileActive = true;
			UniversalKeyCode universalKeyDown = InputUtils.GetUniversalKeyDown(excludeModifierKeys: true, excludeMouseButtons: false);
			_keyWhileActive = universalKeyDown;
			list = null;
			flag7 = false;
		}
		bool flag8 = !_aKeyWasPressedWhileActive;
		arg = (UniversalKeyCode)list;
		arg2 = (flag7 ? UniversalKeyCode.Unknown : UniversalKeyCode.None);
		if (!flag8)
		{
			bool flag9 = universalKeyCode == UniversalKeyCode.None;
			bool flag10 = flag4;
			if (!flag9)
			{
				flag10 = true;
			}
			bool flag11 = !flag10;
			arg = (UniversalKeyCode)list;
			arg2 = (flag7 ? UniversalKeyCode.Unknown : UniversalKeyCode.None);
			if (!flag11)
			{
				SetActive(active: false);
				if (flag4)
				{
					bool flag12 = !AllowMouseButtons;
					arg = UniversalKeyCode.None;
					arg2 = UniversalKeyCode.None;
					if (flag12)
					{
						goto IL_0adf;
					}
				}
				if (_modifierKeyWhileActive != UniversalKeyCode.None && _keyWhileActive == UniversalKeyCode.None)
				{
					if (_modifierKey == UniversalKeyCode.None)
					{
						goto IL_061c;
					}
					_modifierKey = UniversalKeyCode.None;
					Func<UniversalKeyCode, string> keyCodeToKeyNameFunc8 = KeyCodeToKeyNameFunc;
					string text10;
					if (_modifierKey == UniversalKeyCode.None)
					{
						if (KeyCodeToKeyNameFunc != null)
						{
							_ = _key;
							object obj14 = obj + 40;
							object obj15 = obj + 24;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v136 @ rax_v130 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+28]");
							keyName4 = (string)0;
							goto IL_0d79;
						}
						text10 = InputUtils.UniversalKeyName(_key);
					}
					else
					{
						string text11;
						string text12;
						if (KeyCodeToKeyNameFunc != null)
						{
							Func<UniversalKeyCode, string> keyCodeToKeyNameFunc9 = KeyCodeToKeyNameFunc;
							object obj16 = obj - 40;
							object obj17 = obj + 24;
							_ = _modifierKey;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v157 @ rcx_v116 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
							Func<UniversalKeyCode, string> keyCodeToKeyNameFunc10 = KeyCodeToKeyNameFunc;
							object obj18 = obj + 48;
							object obj19 = obj + 40;
							_ = _key;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v158 @ rcx_v118 (System.Func`2<Kamgam.UGUIComponentsForSettings.UniversalKeyCode, System.String>)+18] (should have been resolved before IL gen)");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+30]");
							text11 = (string)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp-28]");
							text12 = (string)0;
						}
						else
						{
							string text13 = InputUtils.UniversalKeyName(_modifierKey);
							string text14 = InputUtils.UniversalKeyName(_key);
							text11 = text14;
							text12 = text13;
						}
						text10 = text12 + " + " + text11;
					}
					keyName4 = text10;
					goto IL_0d79;
				}
				if (!AllowKeyCombinations)
				{
					if (_modifierKey != UniversalKeyCode.None)
					{
						_modifierKey = UniversalKeyCode.None;
						keyCodeToKeyNameFunc4 = KeyCodeToKeyNameFunc;
						if (_modifierKey != UniversalKeyCode.None)
						{
							goto IL_08ae;
						}
						UniversalKeyCode key = _key;
						if (KeyCodeToKeyNameFunc != null)
						{
							goto IL_084b;
						}
						text5 = InputUtils.UniversalKeyName(_key);
						goto IL_0a1b;
					}
				}
				else if (_modifierKeyWhileActive != _modifierKey)
				{
					_modifierKey = _modifierKeyWhileActive;
					keyCodeToKeyNameFunc4 = KeyCodeToKeyNameFunc;
					if (_modifierKey != UniversalKeyCode.None)
					{
						goto IL_08ae;
					}
					UniversalKeyCode key = _key;
					if (KeyCodeToKeyNameFunc != null)
					{
						goto IL_084b;
					}
					text5 = InputUtils.UniversalKeyName(_key);
					goto IL_0a1b;
				}
				goto IL_0a28;
			}
		}
		goto IL_0adf;
		IL_0323:
		bool flag13 = InputUtils.MouseWheelUsed();
		flag4 = flag13;
		goto IL_0339;
		IL_0adf:
		if (!InputUtils.AnyKeyDown())
		{
			return;
		}
		_aKeyWasPressedWhileActive = true;
		List<UniversalKeyCode> tmpUniversalKeyResults3 = InputUtils._tmpUniversalKeyResults;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rbx_v7 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+1C]");
		_ = (nint)0 + (nint)1;
		((UnityEvent<UniversalKeyCode, UniversalKeyCode>)0).Invoke(arg2, arg);
		object obj20 = default(object);
		if (obj20 == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rbx_v7 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rbx_v7 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+10]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rbx_v7 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
				Array.Clear((Array)num5, 0, 0);
			}
		}
		List<UniversalKeyCode> modifierKeysDown = InputUtils.GetModifierKeysDown(InputUtils._tmpUniversalKeyResults);
		List<UniversalKeyCode> tmpUniversalKeyResults4 = InputUtils._tmpUniversalKeyResults;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rdx_v15 (System.Collections.Generic.List`1<Kamgam.UGUIComponentsForSettings.UniversalKeyCode>)+18]");
		if ((nint)0 != 0)
		{
			object obj21 = obj + 24;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+18]");
			if ((nint)0 != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v5 @ rsp+18]");
				_modifierKeyWhileActive = UniversalKeyCode.None;
			}
		}
		bool excludeMouseButtons = !AllowMouseButtons;
		UniversalKeyCode universalKeyDown2 = InputUtils.GetUniversalKeyDown(excludeModifierKeys: true, excludeMouseButtons);
		if (universalKeyDown2 != UniversalKeyCode.None)
		{
			_keyWhileActive = universalKeyDown2;
		}
	}
}
