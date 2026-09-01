using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Kamgam.UGUIComponentsForSettings;

public class TMPInputFocusHelper : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler
{
	private sealed class _003CUnFocusByDefault_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TMPInputFocusHelper _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CUnFocusByDefault_003Ed__5(int _003C_003E1__state)
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
			//IL_00e1: Expected I4, but got O
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
				if ((object)_003C_003E4__this != null)
				{
					TMP_InputField inputTf = _003C_003E4__this.InputTf;
					if ((object)inputTf != null)
					{
						inputTf.DeactivateInputField();
						goto IL_00cd;
					}
				}
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			goto IL_00cd;
			IL_00cd:
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

	private TouchScreenKeyboardType keyboardType;

	protected TMP_InputField inputTf;

	public TMP_InputField InputTf
	{
		get
		{
			if (inputTf == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				TMP_InputField tMP_InputField = default(TMP_InputField);
				inputTf = tMP_InputField;
			}
			return inputTf;
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		_003CUnFocusByDefault_003Ed__5 obj = new _003CUnFocusByDefault_003Ed__5(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator UnFocusByDefault()
	{
		_003CUnFocusByDefault_003Ed__5 obj = new _003CUnFocusByDefault_003Ed__5(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void OnSubmit(BaseEventData eventData)
	{
		TMP_InputField tMP_InputField = InputTf;
		TouchScreenKeyboard touchScreenKeyboard = TouchScreenKeyboard.Open(tMP_InputField.m_Text, keyboardType);
	}

	public void Update()
	{
		if (Keyboard._003Ccurrent_003Ek__BackingField != null)
		{
			KeyControl enterKey = Keyboard._003Ccurrent_003Ek__BackingField.enterKey;
			if (enterKey.wasPressedThisFrame)
			{
				goto IL_0072;
			}
		}
		if (Gamepad._003Ccurrent_003Ek__BackingField == null)
		{
			return;
		}
		Gamepad gamepad = Gamepad._003Ccurrent_003Ek__BackingField;
		if (gamepad._003CbuttonSouth_003Ek__BackingField.wasPressedThisFrame)
		{
			goto IL_0072;
		}
		return;
		IL_0072:
		TMP_InputField tMP_InputField = InputTf;
		if (!tMP_InputField.m_AllowInput)
		{
			return;
		}
		TMP_InputField tMP_InputField2 = InputTf;
		if (!tMP_InputField2.m_AllowInput || Keyboard._003Ccurrent_003Ek__BackingField == null)
		{
			return;
		}
		KeyControl enterKey2 = Keyboard._003Ccurrent_003Ek__BackingField.enterKey;
		if (!enterKey2.wasPressedThisFrame)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803D3F60");
			Keyboard keyboard = default(Keyboard);
			KeyControl numpadEnterKey = keyboard.numpadEnterKey;
			if (!numpadEnterKey.wasPressedThisFrame)
			{
				TMP_InputField tMP_InputField3 = InputTf;
				tMP_InputField3.DeactivateInputField();
			}
		}
	}
}
