using Landfall.TABS;
using UnityEngine;

public class SetCameraPreset : MonoBehaviour
{
	private MouseLook mouseLook;

	private FreeCameraBase cameraMove;

	private SettingsInstance m_cameraMode;

	private SettingsInstance m_cameraModeFP;

	private CameraAbilityPossess m_cameraPossess;

	private void Start()
	{
		m_cameraPossess = GetComponent<CameraAbilityPossess>();
		mouseLook = GetComponentInChildren<MouseLook>();
		cameraMove = GetComponentInChildren<FreeCameraBase>();
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		if (service != null)
		{
			m_cameraMode = service.GetSettingsInstance("GAMEPLAY_CAMERAMODE");
			m_cameraMode.OnValueChanged += UpdateCameraMode;
			if (m_cameraPossess != null)
			{
				m_cameraModeFP = service.GetSettingsInstance("GAMEPLAY_CAMERAMODE_FP");
				m_cameraModeFP.OnValueChanged += UpdateCameraModeFP;
				m_cameraPossess.OnPossessUpdate += UpdateCameraPossess;
			}
		}
		UpdateCameraPossess(m_cameraPossess != null && m_cameraPossess.IsPossessing);
	}

	private void OnDestroy()
	{
		if (m_cameraMode != null)
		{
			m_cameraMode.OnValueChanged -= UpdateCameraMode;
		}
		if (m_cameraModeFP != null)
		{
			m_cameraModeFP.OnValueChanged -= UpdateCameraModeFP;
		}
		if (m_cameraPossess != null)
		{
			m_cameraPossess.OnPossessUpdate -= UpdateCameraPossess;
		}
	}

	private void UpdateCameraPossess(bool value)
	{
		UpdateCameraMode(m_cameraMode.currentValue);
		if (m_cameraModeFP != null)
		{
			UpdateCameraModeFP(m_cameraModeFP.currentValue);
		}
	}

	private void UpdateCameraMode(int value)
	{
		if (!(m_cameraPossess != null) || !m_cameraPossess.IsPossessing)
		{
			mouseLook.m_currentMouseLookPreset = value;
			if (cameraMove != null)
			{
				cameraMove.presetID = value;
			}
		}
	}

	private void UpdateCameraModeFP(int value)
	{
		if (!(m_cameraPossess == null) && m_cameraPossess.IsPossessing)
		{
			mouseLook.m_currentMouseLookPreset = value;
			if (cameraMove != null)
			{
				cameraMove.presetID = value;
			}
		}
	}
}
