using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

public class GamepadManager
{
	public static Vector2 m_LeftStick;

	public static Vector2 m_RightStick;

	public static VirtualMouseUI m_VirtualMouseUI;

	private static GamepadInputActions m_GamepadInputActions;

	public static readonly float CURSOR_SPEED_MIN = 200f;

	public static readonly float CURSOR_SPEED_MAX = 1000f;

	public static readonly float CURSOR_SPEED_MAX_WITH_ACCEL = 1250f;

	public static readonly float CURSOR_SPEED_DEFAULT_NORMALIZED = 0.33f;

	public static readonly float ZOOM_SPEED_MIN = 0.1f;

	public static readonly float ZOOM_SPEED_MAX = 1.2f;

	public static readonly float ZOOM_SPEED_DEFAULT_NORMALIZED = 0.33f;

	public static readonly float ROTATE_CAMERA_SPEED_MIN = 0.05f;

	public static readonly float ROTATE_CAMERA_SPEED_MAX = 1.5f;

	public static readonly float ROTATE_CAMERA_SPEED_DEFAULT = 0.33f;

	private static float CURSOR_TO_SCROLL_FACTOR = 0.0175f;

	private static float CURSOR_ACCEL_TIME_SECONDS = 1.25f;

	public static void Init(VirtualMouseUI virtualMouseUI)
	{
		m_VirtualMouseUI = virtualMouseUI;
		m_GamepadInputActions = new GamepadInputActions();
		m_GamepadInputActions.Gameplay.LeftStick.performed += delegate(InputAction.CallbackContext ctx)
		{
			LeftStickMoved(ctx);
		};
		m_GamepadInputActions.Gameplay.LeftStick.canceled += delegate
		{
			m_LeftStick = Vector2.zero;
		};
		m_GamepadInputActions.Gameplay.RightStick.performed += delegate(InputAction.CallbackContext ctx)
		{
			RightStickMoved(ctx);
		};
		m_GamepadInputActions.Gameplay.RightStick.canceled += delegate
		{
			m_RightStick = Vector2.zero;
		};
		m_GamepadInputActions.Enable();
	}

	public static bool ButtonJustPressed(GamepadButtonType buttonType)
	{
		if (GameInput.GetActiveGameDevice() != GameDevice.Gamepad)
		{
			return false;
		}
		return buttonType switch
		{
			GamepadButtonType.SELECT => m_GamepadInputActions.Gameplay.Select.WasPressedThisFrame(), 
			GamepadButtonType.START => m_GamepadInputActions.Gameplay.Start.WasPressedThisFrame(), 
			GamepadButtonType.SOUTH => m_GamepadInputActions.Gameplay.ButtonSouth.WasPressedThisFrame(), 
			GamepadButtonType.NORTH => m_GamepadInputActions.Gameplay.ButtonNorth.WasPressedThisFrame(), 
			GamepadButtonType.WEST => m_GamepadInputActions.Gameplay.ButtonWest.WasPressedThisFrame(), 
			GamepadButtonType.EAST => m_GamepadInputActions.Gameplay.ButtonEast.WasPressedThisFrame(), 
			GamepadButtonType.DPAD_DOWN => m_GamepadInputActions.Gameplay.DpadDown.WasPressedThisFrame(), 
			GamepadButtonType.DPAD_UP => m_GamepadInputActions.Gameplay.DpadUp.WasPressedThisFrame(), 
			GamepadButtonType.DPAD_LEFT => m_GamepadInputActions.Gameplay.DpadLeft.WasPressedThisFrame(), 
			GamepadButtonType.DPAD_RIGHT => m_GamepadInputActions.Gameplay.DpadRight.WasPressedThisFrame(), 
			GamepadButtonType.SHOULDER_LEFT => m_GamepadInputActions.Gameplay.ShoulderLeft.WasPressedThisFrame(), 
			GamepadButtonType.SHOULDER_RIGHT => m_GamepadInputActions.Gameplay.ShoulderRight.WasPressedThisFrame(), 
			GamepadButtonType.TRIGGER_LEFT => m_GamepadInputActions.Gameplay.TriggerLeft.WasPressedThisFrame(), 
			GamepadButtonType.TRIGGER_RIGHT => m_GamepadInputActions.Gameplay.TriggerRight.WasPressedThisFrame(), 
			GamepadButtonType.LEFTSTICK_PRESSED => m_GamepadInputActions.Gameplay.LeftStickButton.WasPressedThisFrame(), 
			GamepadButtonType.RIGHTSTICK_PRESSED => m_GamepadInputActions.Gameplay.RightStickButton.WasPressedThisFrame(), 
			_ => false, 
		};
	}

	public static bool ButtonJustReleased(GamepadButtonType buttonType)
	{
		if (GameInput.GetActiveGameDevice() != GameDevice.Gamepad)
		{
			return false;
		}
		return buttonType switch
		{
			GamepadButtonType.SELECT => m_GamepadInputActions.Gameplay.Select.WasReleasedThisFrame(), 
			GamepadButtonType.START => m_GamepadInputActions.Gameplay.Start.WasReleasedThisFrame(), 
			GamepadButtonType.SOUTH => m_GamepadInputActions.Gameplay.ButtonSouth.WasReleasedThisFrame(), 
			GamepadButtonType.NORTH => m_GamepadInputActions.Gameplay.ButtonNorth.WasReleasedThisFrame(), 
			GamepadButtonType.WEST => m_GamepadInputActions.Gameplay.ButtonWest.WasReleasedThisFrame(), 
			GamepadButtonType.EAST => m_GamepadInputActions.Gameplay.ButtonEast.WasReleasedThisFrame(), 
			GamepadButtonType.DPAD_DOWN => m_GamepadInputActions.Gameplay.DpadDown.WasReleasedThisFrame(), 
			GamepadButtonType.DPAD_UP => m_GamepadInputActions.Gameplay.DpadUp.WasReleasedThisFrame(), 
			GamepadButtonType.DPAD_LEFT => m_GamepadInputActions.Gameplay.DpadLeft.WasReleasedThisFrame(), 
			GamepadButtonType.DPAD_RIGHT => m_GamepadInputActions.Gameplay.DpadRight.WasReleasedThisFrame(), 
			GamepadButtonType.SHOULDER_LEFT => m_GamepadInputActions.Gameplay.ShoulderLeft.WasReleasedThisFrame(), 
			GamepadButtonType.SHOULDER_RIGHT => m_GamepadInputActions.Gameplay.ShoulderRight.WasReleasedThisFrame(), 
			GamepadButtonType.TRIGGER_LEFT => m_GamepadInputActions.Gameplay.TriggerLeft.WasReleasedThisFrame(), 
			GamepadButtonType.TRIGGER_RIGHT => m_GamepadInputActions.Gameplay.TriggerRight.WasReleasedThisFrame(), 
			GamepadButtonType.LEFTSTICK_PRESSED => m_GamepadInputActions.Gameplay.RightStickButton.WasReleasedThisFrame(), 
			GamepadButtonType.RIGHTSTICK_PRESSED => m_GamepadInputActions.Gameplay.LeftStickButton.WasReleasedThisFrame(), 
			_ => false, 
		};
	}

	public static bool ButtonIsDown(GamepadButtonType buttonType)
	{
		if (GameInput.GetActiveGameDevice() != GameDevice.Gamepad)
		{
			return false;
		}
		return buttonType switch
		{
			GamepadButtonType.SELECT => m_GamepadInputActions.Gameplay.Select.IsPressed(), 
			GamepadButtonType.START => m_GamepadInputActions.Gameplay.Start.IsPressed(), 
			GamepadButtonType.SOUTH => m_GamepadInputActions.Gameplay.ButtonSouth.IsPressed(), 
			GamepadButtonType.NORTH => m_GamepadInputActions.Gameplay.ButtonNorth.IsPressed(), 
			GamepadButtonType.WEST => m_GamepadInputActions.Gameplay.ButtonWest.IsPressed(), 
			GamepadButtonType.EAST => m_GamepadInputActions.Gameplay.ButtonEast.IsPressed(), 
			GamepadButtonType.DPAD_DOWN => m_GamepadInputActions.Gameplay.DpadDown.IsPressed(), 
			GamepadButtonType.DPAD_UP => m_GamepadInputActions.Gameplay.DpadUp.IsPressed(), 
			GamepadButtonType.DPAD_LEFT => m_GamepadInputActions.Gameplay.DpadLeft.IsPressed(), 
			GamepadButtonType.DPAD_RIGHT => m_GamepadInputActions.Gameplay.DpadRight.IsPressed(), 
			GamepadButtonType.SHOULDER_LEFT => m_GamepadInputActions.Gameplay.ShoulderLeft.IsPressed(), 
			GamepadButtonType.SHOULDER_RIGHT => m_GamepadInputActions.Gameplay.ShoulderRight.IsPressed(), 
			GamepadButtonType.TRIGGER_LEFT => m_GamepadInputActions.Gameplay.TriggerLeft.IsPressed(), 
			GamepadButtonType.TRIGGER_RIGHT => m_GamepadInputActions.Gameplay.TriggerRight.IsPressed(), 
			GamepadButtonType.LEFTSTICK_PRESSED => m_GamepadInputActions.Gameplay.RightStickButton.IsPressed(), 
			GamepadButtonType.RIGHTSTICK_PRESSED => m_GamepadInputActions.Gameplay.LeftStickButton.IsPressed(), 
			_ => false, 
		};
	}

	public static float GetDefaultCursorSpeedNormalized()
	{
		return CURSOR_SPEED_DEFAULT_NORMALIZED;
	}

	public static float GetDefaultRotateCameraSpeedNormalized()
	{
		return ROTATE_CAMERA_SPEED_DEFAULT;
	}

	public static float GetDefaultZoomSpeedNormalized()
	{
		return ZOOM_SPEED_DEFAULT_NORMALIZED;
	}

	public static float GetCursorSpeed()
	{
		return Mathf.Lerp(CURSOR_SPEED_MIN, GetMaxCursorSpeed(), Profiles.m_ActiveProfile.m_GamepadCursorSpeedNormalized) * ((float)Screen.width / GameUI.m_Instance.m_CanvasScaler.referenceResolution.x);
	}

	public static float GetCursorAcceleration()
	{
		return GetCursorSpeed() / CURSOR_ACCEL_TIME_SECONDS;
	}

	public static float GetCursorPanSpeed()
	{
		return GetCursorSpeed() * CURSOR_TO_SCROLL_FACTOR;
	}

	public static float GetZoomSpeed()
	{
		return Mathf.Lerp(ZOOM_SPEED_MIN, ZOOM_SPEED_MAX, Profiles.m_ActiveProfile.m_GamepadZoomSpeedNormalized);
	}

	public static float GetRotateCameraSpeed()
	{
		return Mathf.Lerp(ROTATE_CAMERA_SPEED_MIN, ROTATE_CAMERA_SPEED_MAX, Profiles.m_ActiveProfile.m_GamepadRotateCameraSpeedNormalized);
	}

	public static GamepadType GetGamepadType()
	{
		return Profiles.m_ActiveProfile.m_GamepadButtonIconsChoice switch
		{
			GamepadButtonIconsChoice.STEAMDECK => GamepadType.STEAMDECK, 
			GamepadButtonIconsChoice.PLAYSTATION => GamepadType.PLAYSTATION, 
			GamepadButtonIconsChoice.XBOX => GamepadType.XBOX, 
			GamepadButtonIconsChoice.SWITCH => GamepadType.SWITCH, 
			_ => DetectGamepadType(), 
		};
	}

	public static string GetLocalizedGamepadType(GamepadType gamepadType)
	{
		return gamepadType switch
		{
			GamepadType.STEAMDECK => Localize.Get("UI_STEAMDECK"), 
			GamepadType.PLAYSTATION => Localize.Get("UI_PLAYSTATION_SERIES"), 
			GamepadType.XBOX => Localize.Get("UI_XBOX_SERIES"), 
			GamepadType.SWITCH => Localize.Get("UI_SWITCH"), 
			_ => Localize.Get("UI_STEAMDECK"), 
		};
	}

	public static GamepadType DetectGamepadType()
	{
		Gamepad current = Gamepad.current;
		if (current == null)
		{
			return GamepadType.STEAMDECK;
		}
		if (current is DualShockGamepad)
		{
			return GamepadType.PLAYSTATION;
		}
		if (current is XInputController)
		{
			return GamepadType.XBOX;
		}
		return GamepadType.STEAMDECK;
	}

	public static bool CursorMovingSlowly()
	{
		if (GameInput.GetActiveGameDevice() != GameDevice.Gamepad)
		{
			return false;
		}
		if (GameStateSandbox.m_CameraInTransition || GameStateBuild.m_CameraInTransition || GameStateSim.m_CameraInTransition)
		{
			return true;
		}
		if (m_LeftStick.magnitude < 0.1f && m_RightStick.magnitude < 0.1f && !ButtonIsDown(GamepadButtonType.TRIGGER_LEFT))
		{
			return !ButtonIsDown(GamepadButtonType.TRIGGER_RIGHT);
		}
		return false;
	}

	private static void LeftStickMoved(InputAction.CallbackContext ctx)
	{
		m_LeftStick = ctx.ReadValue<Vector2>();
	}

	private static void RightStickMoved(InputAction.CallbackContext ctx)
	{
		m_RightStick = ctx.ReadValue<Vector2>();
	}

	private static float GetMaxCursorSpeed()
	{
		if (!Profiles.m_ActiveProfile.m_GamepadAcceleration)
		{
			return CURSOR_SPEED_MAX;
		}
		return CURSOR_SPEED_MAX_WITH_ACCEL;
	}
}
