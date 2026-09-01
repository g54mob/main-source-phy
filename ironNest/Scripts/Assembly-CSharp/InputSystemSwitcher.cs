using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputSystemSwitcher : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayedInputEnable_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InputSystemSwitcher _003C_003E4__this;

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
		public _003CDelayedInputEnable_003Ed__12(int _003C_003E1__state)
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
	private PlayerInput _playerInput;

	[SerializeField]
	private InputSystemUIInputModule _inputSystemUIInputModule;

	[SerializeField]
	private List<GameObject> objectToEnableForGamepad;

	[SerializeField]
	private List<GameObject> objectToDisableForGamepad;

	private bool blockDeviceChange;

	private void Start()
	{
	}

	private void OnDeviceChanged(PlayerInput input)
	{
	}

	private void OnDestroy()
	{
	}

	public void EnableTextInput()
	{
	}

	public void DisableTextInput()
	{
	}

	public void EnableInputForPopup()
	{
	}

	public void DisableInputForPopup()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedInputEnable_003Ed__12))]
	private IEnumerator DelayedInputEnable()
	{
		return null;
	}

	public void MenuLoaded()
	{
	}
}
