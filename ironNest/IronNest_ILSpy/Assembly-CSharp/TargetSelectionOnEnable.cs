using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using SteamTools;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TargetSelectionOnEnable : MonoBehaviour
{
	private sealed class _003CDelayedSelection_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public TargetSelectionOnEnable _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedSelection_003Ed__3(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_01f7: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_01be: Expected I4, but got I8
			//IL_0052: Expected I4, but got I8
			//IL_022f: Expected I4, but got O
			TargetSelectionOnEnable targetSelectionOnEnable = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						goto IL_01a9;
					}
					_003C_003E1__state = -1;
					if (!Interface.IsInitialised || !SteamUtils.IsSteamRunningOnSteamDeck())
					{
						if ((object)_003C_003E4__this == null || (object)targetSelectionOnEnable._playerInput == null)
						{
							goto IL_0221;
						}
						string currentControlScheme = targetSelectionOnEnable._playerInput.currentControlScheme;
						if (!(currentControlScheme == "Gamepad"))
						{
							goto IL_01a9;
						}
					}
					EventSystem current = EventSystem.current;
					if ((object)current != null)
					{
						current.SetSelectedGameObject(null);
						EventSystem current2 = EventSystem.current;
						if ((object)_003C_003E4__this != null && (object)current2 != null)
						{
							current2.SetSelectedGameObject(targetSelectionOnEnable.objectToSelect);
							goto IL_01a9;
						}
					}
					goto IL_0221;
				}
				_003C_003E1__state = -1;
				WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame;
				_003C_003E1__state = 2;
				return true;
			}
			_003C_003E1__state = -1;
			WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame2;
			_003C_003E1__state = 1;
			return true;
			IL_01a9:
			return false;
			IL_0221:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
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

	private GameObject objectToSelect;

	private PlayerInput _playerInput;

	private void OnEnable()
	{
		GameObject gameObject = base.gameObject;
		if (gameObject.activeInHierarchy)
		{
			PlayerInput playerInput = UnityEngine.Object.FindAnyObjectByType<PlayerInput>();
			_playerInput = playerInput;
			_003CDelayedSelection_003Ed__3 obj = new _003CDelayedSelection_003Ed__3(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	private IEnumerator DelayedSelection()
	{
		_003CDelayedSelection_003Ed__3 obj = new _003CDelayedSelection_003Ed__3(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
