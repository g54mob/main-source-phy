using System;
using SteamTools;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TargetSelectionOnBack : MonoBehaviour
{
	private InputActionReference backAction;

	private GameObject objectToSelect;

	private PlayerInput _playerInput;

	private void Start()
	{
		if (backAction != null)
		{
			InputActionAsset asset = backAction.asset;
			if (!asset.enabled)
			{
				InputAction action = backAction.action;
				action.Enable();
			}
		}
		InputAction action2 = backAction.action;
		Action<InputAction.CallbackContext> value = BackPerformed;
		action2.performed += value;
		PlayerInput playerInput = UnityEngine.Object.FindAnyObjectByType<PlayerInput>();
		_playerInput = playerInput;
	}

	private void Update()
	{
		if (backAction != null)
		{
			InputActionAsset asset = backAction.asset;
			if (!asset.enabled)
			{
				InputAction action = backAction.action;
				action.Enable();
			}
		}
	}

	private void BackPerformed(InputAction.CallbackContext callbackContext)
	{
		GameObject gameObject = base.gameObject;
		if (!gameObject.activeInHierarchy)
		{
			return;
		}
		if (!Interface.IsInitialised || !SteamUtils.IsSteamRunningOnSteamDeck())
		{
			string currentControlScheme = _playerInput.currentControlScheme;
			if (!(currentControlScheme == "Gamepad"))
			{
				return;
			}
		}
		EventSystem current = EventSystem.current;
		current.SetSelectedGameObject(null);
		EventSystem current2 = EventSystem.current;
		current2.SetSelectedGameObject(objectToSelect);
	}
}
