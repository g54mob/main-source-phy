using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDataStore : Singleton<InputDataStore>
{
	public static PlayerInputActions baseInputActions = null;

	private PlayerInput playerInput;

	private InputManager inputManager;

	private static string currentControlScheme;

	public Action OnControlRebindChanged;

	public Action OnControlApplied;

	public Action<DeviceDisplaySettings> OnDeviceLostAction;

	public Action OnDeviceLostRawAction;

	public Action<DeviceDisplaySettings> OnDeviceRegainedAction;

	[Space]
	[Header("Input ReBinding")]
	public DeviceDisplayConfigurator deviceDisplayConfigurator;

	public List<string> IgnoredBindings = new List<string>();

	[SerializeField]
	private bool isDebugOn = true;

	public static List<string> fixedIgnoredKeys = new List<string> { "<Keyboard>/printScreen", "<Keyboard>/anyKey" };

	private void Start()
	{
		InputSystem.onDeviceChange += ConfigureDevicesChanges;
		SetPlayerInput();
	}

	private void OnDisable()
	{
		InputSystem.onDeviceChange -= ConfigureDevicesChanges;
	}

	public void ConfigureDevicesChanges(InputDevice device, InputDeviceChange change)
	{
		switch (change)
		{
		case InputDeviceChange.Added:
			if (isDebugOn)
			{
				Debug.Log("Detected Input Device Chance on device " + device?.ToString() + " " + change);
			}
			OnDeviceRegained(device);
			break;
		case InputDeviceChange.Removed:
		case InputDeviceChange.Disconnected:
			if (isDebugOn)
			{
				Debug.Log("Detected Input Device Chance on device " + device?.ToString() + " " + change);
			}
			OnDeviceLost(device);
			break;
		}
	}

	public void OnDeviceLost(InputDevice device)
	{
		if (OnDeviceLostAction != null)
		{
			OnDeviceLostAction(deviceDisplayConfigurator.GetDeviceSetting(device));
		}
		OnDeviceLostRawAction?.Invoke();
		if (isDebugOn)
		{
			Debug.Log($"Device {device} was removed");
		}
	}

	public void OnDeviceRegained(InputDevice device)
	{
		if (OnDeviceRegainedAction != null)
		{
			OnDeviceRegainedAction(deviceDisplayConfigurator.GetDeviceSetting(device));
		}
		if (isDebugOn)
		{
			Debug.Log($"Device {device} was added");
		}
	}

	public void SetPlayerInput()
	{
		if (isDebugOn)
		{
			Debug.Log("SetPlayerInput");
		}
		if (baseInputActions == null)
		{
			baseInputActions = new PlayerInputActions();
			SaveSystem.LoadPlayerInput();
		}
		inputManager = UnityEngine.Object.FindObjectOfType<InputManager>();
		playerInput = inputManager.GetComponent<PlayerInput>();
		playerInput.actions = baseInputActions.asset;
		playerInput.uiInputModule.actionsAsset = baseInputActions.asset;
		currentControlScheme = playerInput.currentControlScheme;
	}

	public List<string> GetDeviceIgnoredKeys()
	{
		return deviceDisplayConfigurator.GetDeviceBindingIgnoreKeys(playerInput);
	}

	public List<string> GetDeviceCancelKeys()
	{
		return deviceDisplayConfigurator.GetDeviceBindingCancelKeys(playerInput);
	}

	public Sprite GetDeviceBindingIcon(string playerInputDeviceInputBinding)
	{
		return deviceDisplayConfigurator.GetDeviceBindingIcon(playerInput, playerInputDeviceInputBinding);
	}

	public InputAction GetInputAction(string actionName)
	{
		if (playerInput == null)
		{
			SetPlayerInput();
		}
		return playerInput.actions.FindAction(actionName);
	}

	public void OnControlsChangedEvent(PlayerInput player)
	{
		if (player != null && player.currentControlScheme != currentControlScheme)
		{
			if (isDebugOn)
			{
				Debug.Log("OnControlsChangedEvent " + player.currentControlScheme);
			}
			currentControlScheme = player.currentControlScheme;
			OnControlRebindChanged?.Invoke();
			EventManager.ActivateEvent(EventTypes.OnControlsChange);
		}
	}

	public static string GetActionID(InputAction action, int bindingIndex = 0)
	{
		if (bindingIndex != 0)
		{
			return "Option_Rebind_" + action.name + "_" + bindingIndex;
		}
		return "Option_Rebind_" + action.name;
	}

	public static string GetHumanReadableBinding(InputBinding inputBinding)
	{
		return InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
	}
}
