using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public sealed class KeyPressUnityEvent : MonoBehaviour
{
	public enum ListenMode
	{
		AnyAction,
		SpecificAction
	}

	private sealed class _003CHoldTimerRoutine_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int tokenAtStart;

		public KeyPressUnityEvent _003C_003E4__this;

		public float durationSeconds;

		private float _003Ct_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CHoldTimerRoutine_003Ed__28(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0038: Expected F4, but got I4
			//IL_006e: Expected I4, but got I8
			//IL_019e: Expected I4, but got O
			KeyPressUnityEvent keyPressUnityEvent = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003Ct_003E5__2 = _003C_003E1__state;
			}
			else if (_003C_003E1__state != 1)
			{
				goto IL_018a;
			}
			_003C_003E1__state = -1;
			if ((object)_003C_003E4__this != null)
			{
				if (durationSeconds > _003Ct_003E5__2)
				{
					if (tokenAtStart == keyPressUnityEvent.holdToken)
					{
						float unscaledDeltaTime = Time.unscaledDeltaTime;
						float num = unscaledDeltaTime + _003Ct_003E5__2;
						_003C_003E2__current = null;
						_003Ct_003E5__2 = num;
						_003C_003E1__state = 1;
						return true;
					}
				}
				else if (tokenAtStart == keyPressUnityEvent.holdToken)
				{
					keyPressUnityEvent.holdElapsedForCurrentToken = true;
					if (keyPressUnityEvent.onHoldElapsed == null)
					{
						goto IL_0190;
					}
					keyPressUnityEvent.onHoldElapsed.Invoke();
					keyPressUnityEvent.holdRoutine = null;
				}
				goto IL_018a;
			}
			goto IL_0190;
			IL_0190:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_018a:
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

	private ListenMode listenFor = ListenMode.SpecificAction;

	private InputActionReference specificAction;

	private InputActionReference[] anyActions;

	private UnityEvent onKeyPressed;

	private bool toggleEnabled;

	private bool initialToggleState;

	private UnityEvent onToggleOn;

	private UnityEvent onToggleOff;

	private bool currentToggleState;

	private bool holdEnabled;

	private float holdDurationSeconds;

	private bool holdCancelsOnActionCanceled;

	private UnityEvent onHoldElapsed;

	private UnityEvent onHoldCanceled;

	private readonly List<InputAction> subscribedActions;

	private Coroutine holdRoutine;

	private InputAction holdAction;

	private int holdToken;

	private bool holdElapsedForCurrentToken;

	public UnityEvent OnKeyPressedEvent => onKeyPressed;

	public UnityEvent OnToggleOnEvent => onToggleOn;

	public UnityEvent OnToggleOffEvent => onToggleOff;

	public UnityEvent OnHoldElapsedEvent => onHoldElapsed;

	public UnityEvent OnHoldCanceledEvent => onHoldCanceled;

	public bool CurrentToggleState => currentToggleState;

	public bool ToggleEnabled => toggleEnabled;

	public bool HoldEnabled => holdEnabled;

	public float HoldDurationSeconds => holdDurationSeconds;

	private void OnEnable()
	{
		currentToggleState = initialToggleState;
		SubscribeAll();
	}

	private void OnDisable()
	{
		CancelHold(invokeCanceledEvent: true);
		UnsubscribeAll();
	}

	private void SubscribeAll()
	{
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0110: Expected O, but got I4
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		UnsubscribeAll();
		if (specificAction != null)
		{
			InputAction action = specificAction.action;
			if (action != null)
			{
				InputAction action2 = specificAction.action;
				Action<InputAction.CallbackContext> value = HandleActionStarted;
				action2.started += value;
				Action<InputAction.CallbackContext> value2 = HandleActionCanceled;
				action2.canceled += value2;
				subscribedActions.Add(action2);
			}
		}
		if (anyActions == null)
		{
			return;
		}
		InputActionReference[] array = anyActions;
		object obj = anyActions + 32;
		object obj2 = 0;
		while ((nint)obj2 < array.Length)
		{
			if ((UnityEngine.Object)obj != null)
			{
				InputAction action3 = ((InputActionReference)obj).action;
				if (action3 != null)
				{
					InputAction action4 = ((InputActionReference)obj).action;
					if (!subscribedActions.Contains(action4))
					{
						Action<InputAction.CallbackContext> value3 = HandleActionStarted;
						action4.started += value3;
						Action<InputAction.CallbackContext> value4 = HandleActionCanceled;
						action4.canceled += value4;
						subscribedActions.Add(action4);
					}
				}
			}
			obj2++;
			obj += 8;
		}
	}

	private unsafe void UnsubscribeAll()
	{
		//IL_01bb: Expected I, but got O
		//IL_01c0: Expected I, but got O
		//IL_005c: Expected O, but got Ref
		//IL_00be: Expected I, but got O
		//IL_00c7: Expected O, but got I4
		//IL_00cc: Expected I, but got O
		List<InputAction> list = subscribedActions;
		nint num = unchecked((nint)null);
		nint num2 = unchecked((nint)null);
		List<InputAction> list2;
		InputAction inputAction = default(InputAction);
		while (true)
		{
			list2 = subscribedActions;
			if (num2 >= list._size)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag = inputAction == null;
			nint num3 = num;
			object obj = (object)(&inputAction);
			nint num4 = 0;
			if (!flag)
			{
				Action<InputAction.CallbackContext> value = HandleActionStarted;
				inputAction.started -= value;
				Action<InputAction.CallbackContext> action = HandleActionCanceled;
				inputAction.canceled -= action;
				num3 = (nint)action;
				obj = 0;
				num4 = unchecked((nint)null);
			}
			list = subscribedActions;
			num++;
			num2 = num;
		}
		int version = list2._version + 1;
		list2._version = version;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj2 = default(object);
		if (obj2 == null)
		{
			list2._size = 0;
			return;
		}
		list2._size = 0;
		if (list2._size > 0)
		{
			Array.Clear(list2._items, 0, list2._size);
		}
	}

	private unsafe void HandleActionStarted(InputAction.CallbackContext ctx)
	{
		if (listenFor == ListenMode.SpecificAction)
		{
			if (!(specificAction != null))
			{
				return;
			}
			InputAction action = specificAction.action;
			InputAction action2 = ((InputAction.CallbackContext*)ctx)->action;
			if (action != action2)
			{
				return;
			}
		}
		onKeyPressed.Invoke();
		if (toggleEnabled)
		{
			bool flag = !currentToggleState;
			currentToggleState = flag;
			UnityEvent unityEvent = ((~(currentToggleState ? 1u : 0u) != 0) ? onToggleOn : onToggleOff);
			unityEvent.Invoke();
		}
		if (holdEnabled)
		{
			InputAction action3 = ((InputAction.CallbackContext*)ctx)->action;
			CancelHold(invokeCanceledEvent: true);
			holdAction = action3;
			int tokenAtStart = ++holdToken;
			holdElapsedForCurrentToken = false;
			_003CHoldTimerRoutine_003Ed__28 obj = new _003CHoldTimerRoutine_003Ed__28(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			obj.durationSeconds = holdDurationSeconds;
			obj.tokenAtStart = tokenAtStart;
			Coroutine coroutine = StartCoroutine(obj);
			holdRoutine = coroutine;
		}
	}

	private unsafe void HandleActionCanceled(InputAction.CallbackContext ctx)
	{
		if (holdEnabled && holdCancelsOnActionCanceled && holdAction != null)
		{
			InputAction action = ((InputAction.CallbackContext*)ctx)->action;
			if (action == holdAction)
			{
				CancelHold(invokeCanceledEvent: true);
			}
		}
	}

	private void StartHold(InputAction action)
	{
		CancelHold(invokeCanceledEvent: true);
		holdAction = action;
		int tokenAtStart = ++holdToken;
		holdElapsedForCurrentToken = false;
		_003CHoldTimerRoutine_003Ed__28 obj = new _003CHoldTimerRoutine_003Ed__28(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.durationSeconds = holdDurationSeconds;
		obj.tokenAtStart = tokenAtStart;
		Coroutine coroutine = StartCoroutine(obj);
		holdRoutine = coroutine;
	}

	private void CancelHold(bool invokeCanceledEvent)
	{
		if (holdRoutine != null || holdAction != null)
		{
			bool flag = !invokeCanceledEvent;
			bool flag2 = false;
			if (!flag)
			{
				bool flag3 = !holdElapsedForCurrentToken;
				flag2 = flag3;
			}
			int num = holdToken + 1;
			holdToken = num;
			if (holdRoutine != null)
			{
				StopCoroutine(holdRoutine);
				holdRoutine = null;
			}
			holdAction = null;
			bool flag4 = !flag2;
			holdElapsedForCurrentToken = false;
			if (!flag4)
			{
				onHoldCanceled.Invoke();
			}
		}
	}

	private IEnumerator HoldTimerRoutine(int tokenAtStart, float durationSeconds)
	{
		_003CHoldTimerRoutine_003Ed__28 obj = new _003CHoldTimerRoutine_003Ed__28(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.durationSeconds = durationSeconds;
		obj.tokenAtStart = tokenAtStart;
		return obj;
	}

	public void SetListenMode(ListenMode mode)
	{
		listenFor = mode;
	}

	public void SetSpecificAction(InputActionReference actionReference)
	{
		if (specificAction != actionReference)
		{
			specificAction = actionReference;
			if (base.isActiveAndEnabled)
			{
				SubscribeAll();
			}
		}
	}

	public void SetAnyActions(InputActionReference[] actions)
	{
		bool flag = actions != null;
		InputActionReference[] array = actions;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180001E00");
			InputActionReference[] array2 = default(InputActionReference[]);
			array = array2;
		}
		anyActions = array;
		if (base.isActiveAndEnabled)
		{
			SubscribeAll();
		}
	}

	public void SetToggleEnabled(bool enabled)
	{
		toggleEnabled = enabled;
	}

	public void SetToggleState(bool newState, bool invokeEvent = false)
	{
		if (currentToggleState != newState)
		{
			currentToggleState = newState;
			if (!invokeEvent)
			{
				return;
			}
			if (!newState)
			{
				goto IL_004f;
			}
		}
		else
		{
			if (!invokeEvent)
			{
				return;
			}
			if (!currentToggleState)
			{
				goto IL_004f;
			}
		}
		UnityEvent unityEvent = onToggleOn;
		goto IL_005e;
		IL_004f:
		unityEvent = onToggleOff;
		goto IL_005e;
		IL_005e:
		unityEvent.Invoke();
	}

	public void ResetToggleState(bool invokeEvent = false)
	{
		if (currentToggleState != initialToggleState)
		{
			currentToggleState = initialToggleState;
			if (!invokeEvent)
			{
				return;
			}
			if (!initialToggleState)
			{
				goto IL_0055;
			}
		}
		else
		{
			if (!invokeEvent)
			{
				return;
			}
			if (!currentToggleState)
			{
				goto IL_0055;
			}
		}
		UnityEvent unityEvent = onToggleOn;
		goto IL_0064;
		IL_0055:
		unityEvent = onToggleOff;
		goto IL_0064;
		IL_0064:
		unityEvent.Invoke();
	}

	public void ForceToggle(bool invokeEvent = true)
	{
		bool flag = !currentToggleState;
		if (currentToggleState != flag)
		{
			currentToggleState = flag;
			if (!invokeEvent)
			{
				return;
			}
			if (~(currentToggleState ? 1u : 0u) == 0)
			{
				goto IL_006b;
			}
		}
		else
		{
			if (!invokeEvent)
			{
				return;
			}
			if (~(currentToggleState ? 1u : 0u) != 0)
			{
				goto IL_006b;
			}
		}
		UnityEvent unityEvent = onToggleOn;
		goto IL_007a;
		IL_006b:
		unityEvent = onToggleOff;
		goto IL_007a;
		IL_007a:
		unityEvent.Invoke();
	}

	public void RefreshSubscriptions()
	{
		if (base.isActiveAndEnabled)
		{
			SubscribeAll();
		}
	}

	public void SetHoldEnabled(bool enabled)
	{
		if (holdEnabled != enabled)
		{
			holdEnabled = enabled;
			if (!enabled)
			{
				CancelHold(invokeCanceledEvent: true);
			}
		}
	}

	public void SetHoldDurationSeconds(float seconds)
	{
		//IL_0009: Invalid comparison between I4 and F4
		//IL_001b: Expected F4, but got I4
		bool flag = !(0f < seconds);
		float num = 0f;
		if (!flag)
		{
			num = seconds;
		}
		holdDurationSeconds = num;
	}

	public void CancelHoldTimer()
	{
		CancelHold(invokeCanceledEvent: true);
	}

	public KeyPressUnityEvent()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180001E00");
		InputActionReference[] array = default(InputActionReference[]);
		anyActions = array;
		UnityEvent unityEvent = new UnityEvent();
		unityEvent._002Ector();
		onKeyPressed = unityEvent;
		toggleEnabled = true;
		UnityEvent unityEvent2 = new UnityEvent();
		onToggleOn = unityEvent2;
		UnityEvent unityEvent3 = new UnityEvent();
		onToggleOff = unityEvent3;
		holdDurationSeconds = 0.5f;
		holdCancelsOnActionCanceled = true;
		UnityEvent unityEvent4 = new UnityEvent();
		onHoldElapsed = unityEvent4;
		UnityEvent unityEvent5 = new UnityEvent();
		onHoldCanceled = unityEvent5;
		List<InputAction> list = new List<InputAction>();
		subscribedActions = list;
		base._002Ector();
	}
}
