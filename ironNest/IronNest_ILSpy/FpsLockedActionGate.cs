using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FpsLockedActionGate : MonoBehaviour
{
	public enum TriggerPhase
	{
		Started,
		Performed
	}

	private DynamicCursorManager dynamicCursorManager;

	private InputActionReference menuToggleAction;

	private bool enableActionOnEnable = true;

	private TriggerPhase triggerPhase;

	private bool blockWhileDragging = true;

	private bool blockWhileUIBlocking;

	private bool autoToggle = true;

	private bool emitIgnoredEvent;

	private UnityEvent OnActionWhileFpsLocked;

	private UnityEvent OnMenuOpenRequested;

	private UnityEvent OnMenuCloseRequested;

	private UnityEvent OnActionIgnoredWhenNotFpsLocked;

	private bool _menuOpen;

	private bool _subscribed;

	public bool IsMenuOpen => _menuOpen;

	private void OnEnable()
	{
		SubscribeAction();
	}

	private void OnDisable()
	{
		if (!_subscribed || !(menuToggleAction != null))
		{
			return;
		}
		InputAction action = menuToggleAction.action;
		if (action != null)
		{
			if (triggerPhase == TriggerPhase.Started)
			{
				InputAction action2 = menuToggleAction.action;
				Action<InputAction.CallbackContext> value = OnActionPhase;
				action2.started -= value;
			}
			else if (triggerPhase == TriggerPhase.Performed)
			{
				InputAction action3 = menuToggleAction.action;
				Action<InputAction.CallbackContext> value2 = OnActionPhase;
				action3.performed -= value2;
			}
			_subscribed = false;
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
	}

	private void OnApplicationPause(bool pause)
	{
	}

	private void SubscribeAction()
	{
		object message;
		if (menuToggleAction != null)
		{
			InputAction action = menuToggleAction.action;
			if (action != null)
			{
				bool flag = dynamicCursorManager == null;
				if (!flag)
				{
					if (enableActionOnEnable != flag)
					{
						InputAction action2 = menuToggleAction.action;
						if (!action2.enabled)
						{
							InputAction action3 = menuToggleAction.action;
							action3.Enable();
						}
					}
					if (triggerPhase == TriggerPhase.Started)
					{
						InputAction action4 = menuToggleAction.action;
						Action<InputAction.CallbackContext> value = OnActionPhase;
						action4.started += value;
						_subscribed = true;
						return;
					}
					if (triggerPhase == TriggerPhase.Performed)
					{
						InputAction action5 = menuToggleAction.action;
						Action<InputAction.CallbackContext> value2 = OnActionPhase;
						action5.performed += value2;
					}
					_subscribed = true;
					return;
				}
				message = "[FpsLockedActionGate] DynamicCursorManager reference not assigned.";
				goto IL_01db;
			}
		}
		message = "[FpsLockedActionGate] MenuToggleAction not assigned.";
		goto IL_01db;
		IL_01db:
		Debug.LogWarning(message, this);
	}

	private void UnsubscribeAction()
	{
		if (!_subscribed || !(menuToggleAction != null))
		{
			return;
		}
		InputAction action = menuToggleAction.action;
		if (action != null)
		{
			if (triggerPhase == TriggerPhase.Started)
			{
				InputAction action2 = menuToggleAction.action;
				Action<InputAction.CallbackContext> value = OnActionPhase;
				action2.started -= value;
			}
			else if (triggerPhase == TriggerPhase.Performed)
			{
				InputAction action3 = menuToggleAction.action;
				Action<InputAction.CallbackContext> value2 = OnActionPhase;
				action3.performed -= value2;
			}
			_subscribed = false;
		}
	}

	private void OnActionPhase(InputAction.CallbackContext ctx)
	{
		UnityEvent unityEvent;
		if (this.dynamicCursorManager != null)
		{
			DynamicCursorManager dynamicCursorManager = this.dynamicCursorManager;
			if (dynamicCursorManager._currentMode == DynamicCursorManager.PresentationMode.FPSLocked && (!blockWhileDragging || !dynamicCursorManager.IsDragging))
			{
				if (!autoToggle)
				{
					unityEvent = OnActionWhileFpsLocked;
				}
				else if (!_menuOpen)
				{
					unityEvent = OnMenuOpenRequested;
					_menuOpen = true;
				}
				else
				{
					unityEvent = OnMenuCloseRequested;
					_menuOpen = false;
				}
				goto IL_015a;
			}
		}
		if (emitIgnoredEvent)
		{
			unityEvent = OnActionIgnoredWhenNotFpsLocked;
			goto IL_015a;
		}
		return;
		IL_015a:
		unityEvent?.Invoke();
	}

	private bool CanProceedIfUIBlocking()
	{
		return true;
	}

	public void ForceOpen()
	{
		if (autoToggle && !_menuOpen)
		{
			_menuOpen = true;
			if (OnMenuOpenRequested != null)
			{
				OnMenuOpenRequested.Invoke();
			}
		}
	}

	public void ForceClose()
	{
		if (autoToggle && _menuOpen)
		{
			_menuOpen = false;
			if (OnMenuCloseRequested != null)
			{
				OnMenuCloseRequested.Invoke();
			}
		}
	}

	public void ForceToggle()
	{
		if (autoToggle)
		{
			UnityEvent unityEvent;
			if (!_menuOpen)
			{
				_menuOpen = true;
				unityEvent = OnMenuOpenRequested;
			}
			else
			{
				_menuOpen = false;
				unityEvent = OnMenuCloseRequested;
			}
			unityEvent?.Invoke();
		}
	}
}
