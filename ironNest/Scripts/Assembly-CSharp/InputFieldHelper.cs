using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputFieldHelper : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CUnFocusByDefault_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InputFieldHelper _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CUnFocusByDefault_003Ed__11(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[SerializeField]
	private TMP_InputField inputTf;

	[SerializeField]
	private UILeaderboardUsernameSetting uiLeaderboardUsernameSetting;

	[SerializeField]
	private CatNameSetting catNameSetting;

	[SerializeField]
	private PlayerInput playerInput;

	private Callback<GamepadTextInputDismissed_t> gamepadTextInputDismissedCallback;

	public string promptText;

	public int maxCharLimit;

	private void Start()
	{
	}

	private void ShowVirtualKeyboard(string selected)
	{
	}

	private void OnSelected(string arg0)
	{
	}

	private void OnDeselect(string arg0)
	{
	}

	[IteratorStateMachine(typeof(_003CUnFocusByDefault_003Ed__11))]
	private IEnumerator UnFocusByDefault()
	{
		return null;
	}

	private void OnGamepadTextInputDismissed(GamepadTextInputDismissed_t callbackData)
	{
	}
}
