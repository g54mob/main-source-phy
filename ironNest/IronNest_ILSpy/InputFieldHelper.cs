using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration.API;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputFieldHelper : MonoBehaviour
{
	private sealed class _003CUnFocusByDefault_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InputFieldHelper _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CUnFocusByDefault_003Ed__11(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_006f: Expected I4, but got I8
			//IL_00dc: Expected I4, but got O
			InputFieldHelper inputFieldHelper = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame;
				_003C_003E1__state = 1;
				return true;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null || (object)inputFieldHelper.inputTf == null)
				{
					NullReferenceException ex = new NullReferenceException();
					return (byte)(int)ex != 0;
				}
				inputFieldHelper.inputTf.DeactivateInputField();
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	private TMP_InputField inputTf;

	private UILeaderboardUsernameSetting uiLeaderboardUsernameSetting;

	private CatNameSetting catNameSetting;

	private PlayerInput playerInput;

	private Callback<GamepadTextInputDismissed_t> gamepadTextInputDismissedCallback;

	public string promptText;

	public int maxCharLimit;

	private void Start()
	{
		TMP_InputField tMP_InputField = inputTf;
		UnityAction<string> call = ShowVirtualKeyboard;
		tMP_InputField.m_OnSubmit.AddListener(call);
		TMP_InputField tMP_InputField2 = inputTf;
		UnityAction<string> call2 = OnSelected;
		tMP_InputField2.m_OnSelect.AddListener(call2);
		TMP_InputField tMP_InputField3 = inputTf;
		UnityAction<string> call3 = OnDeselect;
		tMP_InputField3.m_OnDeselect.AddListener(call3);
		Callback<GamepadTextInputDismissed_t>.DispatchDelegate func = OnGamepadTextInputDismissed;
		Callback<GamepadTextInputDismissed_t> callback = Callback<GamepadTextInputDismissed_t>.Create(func);
		gamepadTextInputDismissedCallback = callback;
	}

	private void ShowVirtualKeyboard(string selected)
	{
		if (App._003CInitialised_003Ek__BackingField && SteamUtils.IsSteamRunningOnSteamDeck())
		{
			TMP_InputField tMP_InputField = inputTf;
			if (!tMP_InputField.m_AllowInput)
			{
				tMP_InputField.ActivateInputField();
			}
			else
			{
				string pchExistingText = default(string);
				bool flag = SteamUtils.ShowGamepadTextInput(EGamepadTextInputMode.k_EGamepadTextInputModeNormal, EGamepadTextInputLineMode.k_EGamepadTextInputLineModeSingleLine, promptText, (uint)maxCharLimit, pchExistingText);
			}
			return;
		}
		TMP_InputField tMP_InputField2 = inputTf;
		if (tMP_InputField2.m_AllowInput)
		{
			tMP_InputField2.DeactivateInputField();
			if ((object)uiLeaderboardUsernameSetting != null)
			{
				uiLeaderboardUsernameSetting.Save();
			}
			if ((object)catNameSetting != null)
			{
				catNameSetting.Save();
			}
		}
	}

	private void OnSelected(string arg0)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A540]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = playerInput.currentControlScheme;
		if (currentControlScheme == "Gamepad")
		{
			_003CUnFocusByDefault_003Ed__11 obj = new _003CUnFocusByDefault_003Ed__11(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	private void OnDeselect(string arg0)
	{
		inputTf.DeactivateInputField();
		if ((object)uiLeaderboardUsernameSetting != null)
		{
			uiLeaderboardUsernameSetting.Save();
		}
		if ((object)catNameSetting != null)
		{
			catNameSetting.Save();
		}
	}

	private IEnumerator UnFocusByDefault()
	{
		_003CUnFocusByDefault_003Ed__11 obj = new _003CUnFocusByDefault_003Ed__11(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private void OnGamepadTextInputDismissed(GamepadTextInputDismissed_t callbackData)
	{
		if (callbackData.m_bSubmitted && SteamUtils.GetEnteredGamepadTextInput(out var pchText, (uint)maxCharLimit))
		{
			inputTf.text = pchText;
			inputTf.DeactivateInputField();
			if ((object)uiLeaderboardUsernameSetting != null)
			{
				uiLeaderboardUsernameSetting.Save();
			}
			if ((object)catNameSetting != null)
			{
				catNameSetting.Save();
			}
		}
	}

	public InputFieldHelper()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A542]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		promptText = "Username for leaderboards";
		maxCharLimit = 30;
		base._002Ector();
	}
}
