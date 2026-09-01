using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using Heathen.SteamworksIntegration.API;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputSystemSwitcher : MonoBehaviour
{
	private sealed class _003CDelayedInputEnable_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public InputSystemSwitcher _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedInputEnable_003Ed__12(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0103: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0175: Expected I4, but got O
			//IL_00ca: Expected I4, but got I8
			//IL_005c: Expected I4, but got I8
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj == 1)
					{
						InputSystemSwitcher inputSystemSwitcher = _003C_003E4__this;
						_003C_003E1__state = -1;
						if ((object)_003C_003E4__this == null || (object)inputSystemSwitcher._inputSystemUIInputModule == null)
						{
							goto IL_0167;
						}
						inputSystemSwitcher._inputSystemUIInputModule.enabled = true;
					}
					return false;
				}
				_003C_003E1__state = -1;
				WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame;
				_003C_003E1__state = 2;
				return true;
			}
			_003C_003E1__state = -1;
			EventSystem current = EventSystem.current;
			if ((object)current != null)
			{
				current.SetSelectedGameObject(null);
				WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame2;
				_003C_003E1__state = 1;
				return true;
			}
			goto IL_0167;
			IL_0167:
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

	private PlayerInput _playerInput;

	private InputSystemUIInputModule _inputSystemUIInputModule;

	private List<GameObject> objectToEnableForGamepad;

	private List<GameObject> objectToDisableForGamepad;

	private bool blockDeviceChange = true;

	private void Start()
	{
		//IL_00d8: Expected I, but got O
		_playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
		Action<PlayerInput> value = OnDeviceChanged;
		_playerInput.onControlsChanged += value;
		if (!App._003CInitialised_003Ek__BackingField || !SteamUtils.IsSteamRunningOnSteamDeck())
		{
			return;
		}
		_inputSystemUIInputModule.enabled = true;
		InputDevice[] array = new InputDevice[1];
		if (Gamepad._003Ccurrent_003Ek__BackingField != null)
		{
			nint num = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				IntPtr intPtr = default(IntPtr);
				throw intPtr;
			}
		}
		if (array.Length > 0)
		{
			array[0] = Gamepad._003Ccurrent_003Ek__BackingField;
			_playerInput.SwitchCurrentControlScheme("Gamepad", array);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			GameObject gameObject = default(GameObject);
			List<GameObject>.Enumerator enumerator2 = default(List<GameObject>.Enumerator);
			GameObject gameObject2 = default(GameObject);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if ((object)gameObject == null)
					{
						break;
					}
					gameObject.SetActive(value: true);
					continue;
				}
				enumerator.Dispose();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				while (true)
				{
					if (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if ((object)gameObject2 == null)
						{
							break;
						}
						gameObject2.SetActive(value: false);
						continue;
					}
					enumerator2.Dispose();
					return;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		throw new IndexOutOfRangeException();
	}

	private void OnDeviceChanged(PlayerInput input)
	{
		//IL_00a3: Expected I, but got O
		if (App._003CInitialised_003Ek__BackingField && SteamUtils.IsSteamRunningOnSteamDeck() && blockDeviceChange)
		{
			InputDevice[] array = new InputDevice[1];
			if (Gamepad._003Ccurrent_003Ek__BackingField != null)
			{
				nint num = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj = default(object);
				if (obj == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					IntPtr intPtr = default(IntPtr);
					throw intPtr;
				}
			}
			if (array.Length <= 0)
			{
				throw new IndexOutOfRangeException();
			}
			array[0] = Gamepad._003Ccurrent_003Ek__BackingField;
			_playerInput.SwitchCurrentControlScheme("Gamepad", array);
		}
		string currentControlScheme = _playerInput.currentControlScheme;
		GameObject gameObject = default(GameObject);
		if (currentControlScheme == "Gamepad")
		{
			_inputSystemUIInputModule.enabled = true;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
			List<GameObject>.Enumerator enumerator2 = default(List<GameObject>.Enumerator);
			while (true)
			{
				if (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if ((object)gameObject == null)
					{
						break;
					}
					gameObject.SetActive(value: true);
					continue;
				}
				enumerator.Dispose();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				while (true)
				{
					if (enumerator2.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if ((object)gameObject == null)
						{
							break;
						}
						gameObject.SetActive(value: false);
						continue;
					}
					enumerator2.Dispose();
					return;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		EventSystem current = EventSystem.current;
		current.SetSelectedGameObject(null);
		_inputSystemUIInputModule.enabled = false;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<GameObject>.Enumerator enumerator3 = default(List<GameObject>.Enumerator);
		List<GameObject>.Enumerator enumerator4 = default(List<GameObject>.Enumerator);
		while (true)
		{
			if (enumerator3.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if ((object)gameObject == null)
				{
					break;
				}
				gameObject.SetActive(value: false);
				continue;
			}
			enumerator3.Dispose();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			while (true)
			{
				if (enumerator4.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					if ((object)gameObject == null)
					{
						break;
					}
					gameObject.SetActive(value: true);
					continue;
				}
				enumerator4.Dispose();
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
	}

	private void OnDestroy()
	{
		Action<PlayerInput> value = OnDeviceChanged;
		_playerInput.onControlsChanged -= value;
	}

	public void EnableTextInput()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A957]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = _playerInput.currentControlScheme;
		if (currentControlScheme != "Gamepad")
		{
			_inputSystemUIInputModule.enabled = true;
		}
	}

	public void DisableTextInput()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A958]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = _playerInput.currentControlScheme;
		if (currentControlScheme != "Gamepad")
		{
			_inputSystemUIInputModule.enabled = false;
		}
	}

	public void EnableInputForPopup()
	{
		_inputSystemUIInputModule.enabled = false;
	}

	public void DisableInputForPopup()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A959]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		string currentControlScheme = _playerInput.currentControlScheme;
		if (currentControlScheme == "Gamepad")
		{
			_003CDelayedInputEnable_003Ed__12 obj = new _003CDelayedInputEnable_003Ed__12(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine coroutine = StartCoroutine(obj);
		}
	}

	private IEnumerator DelayedInputEnable()
	{
		_003CDelayedInputEnable_003Ed__12 obj = new _003CDelayedInputEnable_003Ed__12(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void MenuLoaded()
	{
		blockDeviceChange = false;
	}
}
