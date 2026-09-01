using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput
{
	public static bool m_IgnoreAllGameInput;

	public static KeyCode m_ModKeybindIgnoreGameInput;

	private static GameDevice m_ActiveGameDevice;

	public static void Init()
	{
		InputSystem.onActionChange += InputSystem_OnActionChange;
		if (Game.IsRunningOnSteamDeck())
		{
			ChangeActiveGameDevice(GameDevice.Gamepad);
		}
		else
		{
			ChangeActiveGameDevice(GameDevice.KeyboardAndMouse);
		}
	}

	public static bool GetMouseButtonJustPressed(int button)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return button switch
			{
				0 => GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH), 
				1 => GamepadManager.ButtonJustPressed(GamepadButtonType.EAST), 
				_ => false, 
			};
		}
		bool flag = EmulateRightClickIsDown();
		if (button == 1)
		{
			if (!flag || !Input.GetMouseButtonDown(0))
			{
				return Input.GetMouseButtonDown(1);
			}
			return true;
		}
		if (!flag)
		{
			return Input.GetMouseButtonDown(button);
		}
		return false;
	}

	public static bool GetMouseButtonJustReleased(int button)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return button switch
			{
				0 => GamepadManager.ButtonJustReleased(GamepadButtonType.SOUTH), 
				1 => GamepadManager.ButtonJustReleased(GamepadButtonType.EAST), 
				_ => false, 
			};
		}
		bool flag = EmulateRightClickIsDown();
		if (button == 1)
		{
			if (!flag || !Input.GetMouseButtonUp(0))
			{
				return Input.GetMouseButtonUp(1);
			}
			return true;
		}
		if (!flag)
		{
			return Input.GetMouseButtonUp(button);
		}
		return false;
	}

	public static bool GetMouseButtonIsDown(int button)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return button switch
			{
				0 => GamepadManager.ButtonIsDown(GamepadButtonType.SOUTH), 
				1 => GamepadManager.ButtonIsDown(GamepadButtonType.EAST), 
				_ => false, 
			};
		}
		bool flag = EmulateRightClickIsDown();
		if (button == 1)
		{
			if (!flag || !Input.GetMouseButton(0))
			{
				return Input.GetMouseButton(1);
			}
			return true;
		}
		if (!flag)
		{
			return Input.GetMouseButton(button);
		}
		return false;
	}

	public static bool JustPressed(BindingType type)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (!Bindings.m_Bindings.ContainsKey(type))
		{
			return false;
		}
		Binding binding = Bindings.m_Bindings[type];
		if (CampaignTutorial.BindingIsBlocked(binding))
		{
			return false;
		}
		return binding.JustPressed();
	}

	public static bool JustPressedRaw(BindingType type)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (!Bindings.m_Bindings.ContainsKey(type))
		{
			return false;
		}
		return Bindings.m_Bindings[type].JustPressedRaw();
	}

	public static bool JustReleased(BindingType type)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (!Bindings.m_Bindings.ContainsKey(type))
		{
			return false;
		}
		Binding binding = Bindings.m_Bindings[type];
		if (CampaignTutorial.BindingIsBlocked(binding))
		{
			return false;
		}
		return binding.JustReleased();
	}

	public static bool IsDown(BindingType type)
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (!Bindings.m_Bindings.ContainsKey(type))
		{
			return false;
		}
		Binding binding = Bindings.m_Bindings[type];
		if (CampaignTutorial.BindingIsBlocked(binding))
		{
			return false;
		}
		return binding.IsDown();
	}

	public static bool IsMouseButton(KeyCode keyCode)
	{
		if (keyCode >= KeyCode.Mouse0)
		{
			return keyCode <= KeyCode.Mouse6;
		}
		return false;
	}

	public static bool EmulateRightClickIsDown()
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		if (!Bindings.m_Bindings.ContainsKey(BindingType.EMULATE_MOUSE1))
		{
			return false;
		}
		Binding binding = Bindings.m_Bindings[BindingType.EMULATE_MOUSE1];
		if (binding.m_KeyCode != KeyCode.None && Input.GetKey(binding.m_KeyCode))
		{
			return true;
		}
		if (binding.m_AltKeyCode != KeyCode.None && Input.GetKey(binding.m_AltKeyCode))
		{
			return true;
		}
		return false;
	}

	public static Vector3 GetMousePosition()
	{
		if (GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GetVirtualMousePosition();
		}
		return Input.mousePosition;
	}

	public static bool MultiSelectIsDown()
	{
		if (IgnoreAllGameInput())
		{
			return false;
		}
		return IsDown(BindingType.MULTI_SELECT);
	}

	public static bool AnyKeyDown()
	{
		if (!Input.anyKeyDown && !GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH) && !GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH) && !GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			return GamepadManager.ButtonJustPressed(GamepadButtonType.EAST);
		}
		return true;
	}

	public static bool MousePointerOutsideGame()
	{
		Vector3 mousePosition = GetMousePosition();
		if (!(mousePosition.x < 0f) && !(mousePosition.y < 0f) && !(mousePosition.x > (float)Screen.width))
		{
			return mousePosition.y > (float)Screen.height;
		}
		return true;
	}

	public static GameDevice GetActiveGameDevice()
	{
		return m_ActiveGameDevice;
	}

	public static Vector2 GetVirtualMousePosition()
	{
		return GamepadManager.m_VirtualMouseUI.GetVirtualMousePosition();
	}

	public static void SetVirtualMousePosition(Vector2 pos)
	{
		GamepadManager.m_VirtualMouseUI.SetVirtualMousePosition(pos);
	}

	public static void ChangeActiveGameDevice(GameDevice activeGameDevice)
	{
		GameDevice activeGameDevice2 = m_ActiveGameDevice;
		if (!Profiles.m_ActiveProfile.m_BlockGamepadInput || activeGameDevice != GameDevice.Gamepad)
		{
			m_ActiveGameDevice = activeGameDevice;
		}
		Cursor.visible = m_ActiveGameDevice == GameDevice.KeyboardAndMouse;
		if (m_ActiveGameDevice == GameDevice.Gamepad && activeGameDevice2 != m_ActiveGameDevice)
		{
			GamepadManager.m_VirtualMouseUI.ResetMouseToCenter();
		}
		if (GameUI.m_Instance != null)
		{
			GameUI.m_Instance.m_GamepadLegend.m_Background.gameObject.SetActive(Panel_MainMenuNew.m_NumUpdates != 0 && !GameUI.m_Instance.m_MainMenuNew.gameObject.activeInHierarchy && m_ActiveGameDevice == GameDevice.Gamepad);
			GameUI.m_Instance.m_GamepadLegend.gameObject.SetActive(m_ActiveGameDevice == GameDevice.Gamepad);
			GameUI.m_Instance.m_BuildToolBar.UpdateForCurrentDevice();
			GameUI.m_Instance.m_BottomBar.UpdateForCurrentDevice();
			GameUI.m_Instance.m_HydraulicsController.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Selection.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Clipboard.UpdateForCurrentDevice();
			GameUI.m_Instance.m_LiveStress.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxMultiSelect.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditAnchor.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditCustomShape.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditFlyingObject.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditCheckpoint.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditPlatform.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditRamp.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditRock.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditPillar.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditTerrain.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditVehicle.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditVehicleStopTrigger.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditBuildZone.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditDecor.UpdateForCurrentDevice();
			GameUI.m_Instance.m_ProfileSelect.m_ProfileEdit.UpdateForCurrentDevice();
			GameUI.m_Instance.m_WorkshopSubmit.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Workshop.m_SubmitModPanel.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Workshop.m_LocalModsPanel.m_SubmitModPanel.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.UpdateForCurrentDevice();
			GameUI.m_Instance.m_CustomShapeReset.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Gallery.UpdateForCurrentDevice();
			GameUI.m_Instance.m_WeeklyChallenges.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Workshop.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Workshop.m_FilterBar.UpdateForCurrentDevice();
			GameUI.m_Instance.m_SandboxEditCustomShape.m_ColorPicker.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Settings.m_GraphicsPanel.UpdateForCurrentDevice();
			GameUI.m_Instance.m_Settings.m_TwitchPanel.UpdateForCurrentDevice();
			GameUI.m_Instance.m_GamepadSafeArea.offsetMin = new Vector2(GameUI.m_Instance.m_GamepadSafeArea.offsetMin.x, (GetActiveGameDevice() == GameDevice.Gamepad) ? GamepadLegend.HEIGHT : 0);
			SandboxInputFields.UpdateForCurrentDevice();
			GameStatePhoto.UpdateForCurrentDevice();
			if (GamepadManager.m_VirtualMouseUI != null)
			{
				GamepadManager.m_VirtualMouseUI.UpdateVisibility();
			}
		}
	}

	private static bool IgnoreAllGameInput()
	{
		if (m_IgnoreAllGameInput)
		{
			return true;
		}
		if (m_ModKeybindIgnoreGameInput != KeyCode.None)
		{
			return Input.GetKey(m_ModKeybindIgnoreGameInput);
		}
		return false;
	}

	private static void InputSystem_OnActionChange(object arg1, InputActionChange inputActionChange)
	{
		if (inputActionChange != InputActionChange.ActionPerformed || !(arg1 is InputAction))
		{
			return;
		}
		InputAction inputAction = arg1 as InputAction;
		if (inputAction.activeControl.device.displayName == "VirtualMouse")
		{
			return;
		}
		if (inputAction.activeControl.device is Gamepad)
		{
			if (m_ActiveGameDevice != GameDevice.Gamepad)
			{
				ChangeActiveGameDevice(GameDevice.Gamepad);
			}
		}
		else if (m_ActiveGameDevice != GameDevice.KeyboardAndMouse)
		{
			ChangeActiveGameDevice(GameDevice.KeyboardAndMouse);
		}
	}
}
