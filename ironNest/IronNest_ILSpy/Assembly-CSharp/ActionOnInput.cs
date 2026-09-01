using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionOnInput : MonoBehaviour
{
	private InputActionReference action;

	private UnityEvent onEvent;

	private void Start()
	{
		if (action != null)
		{
			InputActionAsset asset = action.asset;
			if (!asset.enabled)
			{
				InputAction inputAction = action.action;
				inputAction.Enable();
			}
		}
		InputAction inputAction2 = action.action;
		Action<InputAction.CallbackContext> value = OnEvent;
		inputAction2.performed += value;
	}

	private void Update()
	{
		if (action != null)
		{
			InputActionAsset asset = action.asset;
			if (!asset.enabled)
			{
				InputAction inputAction = action.action;
				inputAction.Enable();
			}
		}
	}

	private void OnEvent(InputAction.CallbackContext callbackContext)
	{
		if (onEvent != null)
		{
			onEvent.Invoke();
		}
	}
}
