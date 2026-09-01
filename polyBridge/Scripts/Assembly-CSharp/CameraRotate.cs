using Poly;
using Poly.Game;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public float m_SensitivityX;

	public float m_SensitivityY;

	public float m_MinPitch;

	public float m_MaxPitch;

	public static CameraRotate m_Instance;

	private static Vector2 m_VirtualMousePositionWhenStartRotation;

	private void Awake()
	{
		m_Instance = this;
	}

	public void UpdateManual()
	{
		if ((GameStateManager.GetState() != GameState.SIM && GameStateManager.GetState() != GameState.PHOTO) || (GameStateManager.GetState() == GameState.SIM && Profiles.m_ActiveProfile.m_LockBuildCamera) || Cameras.InLocked2DMode() || GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy || GameUI.m_Instance.m_Settings.gameObject.activeInHierarchy || GameUI.m_Instance.m_ProfileSelect.gameObject.activeInHierarchy || GameUI.m_Instance.m_ShareReplay.gameObject.activeInHierarchy || GameUI.m_Instance.m_ShareReplayStatus.gameObject.activeInHierarchy || GameUI.m_Instance.m_PopUpMessage.gameObject.activeInHierarchy || GameUI.PointerOver(typeof(Panel_Stages)) || GameUI.PointerOver(typeof(Panel_TopBar)) || GameUI.PointerOver(typeof(Panel_SimToolBar)) || (GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy && GameUI.m_Instance.m_LevelInfoLite.IsDraggingScrollbar()) || GameUI.m_Instance.m_TopBar.m_BridgeSimSpeedSlider.IsDragging() || GameUI.LevelEndPanelIsActive())
		{
			return;
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			m_VirtualMousePositionWhenStartRotation = GamepadManager.m_VirtualMouseUI.GetVirtualMousePosition();
		}
		if (!CanRotate())
		{
			return;
		}
		float deltaAngleHorizontal = GetDeltaAngleHorizontal();
		float deltaAngleVertical = GetDeltaAngleVertical();
		if (Mathf.Abs(deltaAngleVertical) > 0.05f || Mathf.Abs(deltaAngleHorizontal) > 0.05f)
		{
			GameUI.m_Instance.m_SimToolBar.HighlightPointOfView(PointOfViewType.SIM_CUSTOM);
			GamepadManager.m_VirtualMouseUI.SetVirtualMousePosition(m_VirtualMousePositionWhenStartRotation);
		}
		if ((bool)CameraControl.instance && CameraControl.instance.isSimActive)
		{
			if (!Mathf.Approximately(deltaAngleHorizontal, 0f) || !Mathf.Approximately(deltaAngleVertical, 0f))
			{
				CameraControl.instance.RotMouse(new Vec2(deltaAngleHorizontal, deltaAngleVertical));
			}
			return;
		}
		base.transform.RotateAround(PointsOfView.m_Pivot, Vector3.up, deltaAngleHorizontal);
		float angle = Mathf.Min(90f, deltaAngleVertical);
		base.transform.RotateAround(PointsOfView.m_Pivot, Cameras.MainCamera().transform.right, angle);
		if (base.transform.forward.y > 0f)
		{
			float angle2 = Vector3.Angle(Cameras.MainCamera().transform.forward, new Vector3(Cameras.MainCamera().transform.forward.x, 0f, Cameras.MainCamera().transform.forward.z));
			Cameras.MainCamera().transform.RotateAround(PointsOfView.m_Pivot, Cameras.MainCamera().transform.right, angle2);
		}
		float x = Mathf.Clamp(Cameras.MainCamera().transform.eulerAngles.x, Cameras.GetMinPitch(), Cameras.GetMaxPitch());
		Cameras.MainCamera().transform.rotation = Quaternion.Euler(x, Cameras.MainCamera().transform.eulerAngles.y, Cameras.MainCamera().transform.eulerAngles.z);
	}

	private bool CanRotate()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.ButtonIsDown(GamepadButtonType.NORTH);
		}
		if (GameInput.IsDown(BindingType.ROTATE_SIM_CAMERA))
		{
			if (!GameInput.IsDown(BindingType.DRAW_BUILD))
			{
				return !GameInput.IsDown(BindingType.PAN_WITH_MOUSE);
			}
			return false;
		}
		return false;
	}

	private float GetDeltaAngleHorizontal()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.m_LeftStick.x * m_SensitivityX * GamepadManager.GetRotateCameraSpeed();
		}
		return Input.GetAxis("Mouse X") * m_SensitivityX * Profiles.m_ActiveProfile.m_CameraRotateSpeedNormalized;
	}

	private float GetDeltaAngleVertical()
	{
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad)
		{
			return GamepadManager.m_LeftStick.y * (0f - m_SensitivityY) * GamepadManager.GetRotateCameraSpeed();
		}
		return Input.GetAxis("Mouse Y") * (0f - m_SensitivityY) * Profiles.m_ActiveProfile.m_CameraRotateSpeedNormalized;
	}
}
