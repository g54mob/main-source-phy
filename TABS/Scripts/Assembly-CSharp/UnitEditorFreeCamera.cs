using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;

public class UnitEditorFreeCamera : FreeCameraBase
{
	public MouseLook mouseLook;

	public Transform camParent;

	public CameraMovementPreset[] freeLookPresets;

	private SettingsInstance m_cameraMode;

	private CameraMovement m_cameraMovement;

	private Camera cam;

	private PlayerActions m_playerActions;

	private FieldOfViewLerp fieldOfViewLerp;

	private void Awake()
	{
		m_playerActions = PlayerActions.Instance;
		GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
		m_cameraMode = service.GetSettingsInstance("GAMEPLAY_CAMERAMODE");
		presetID = m_cameraMode.currentValue;
		m_cameraMovement = GetComponent<CameraMovement>();
	}

	public void SetMouseLook()
	{
		mouseLook.SetRotationToCurrent();
	}

	private void OnEnable()
	{
		mouseLook.AlowInput(allow: true);
	}

	private void OnDisable()
	{
		mouseLook.AlowInput(allow: true);
	}

	public void ParentCamera(Camera cam)
	{
		this.cam = cam;
		fieldOfViewLerp = cam.GetComponent<FieldOfViewLerp>();
		fieldOfViewLerp.targetFOV = 60f;
		fieldOfViewLerp.lerpFactor = 5f;
		cam.transform.parent = camParent;
		cam.transform.localRotation = Quaternion.identity;
		cam.transform.localPosition = Vector3.zero;
	}

	private void Update()
	{
		if (cam != null)
		{
			float num = 0.45f;
			if (m_playerActions.m_moveFast.IsPressed)
			{
				num = 1.5f;
			}
			Vector3 force = default(Vector3);
			force += base.transform.right * m_playerActions.m_move.X;
			force += base.transform.forward * m_playerActions.m_move.Y;
			force += base.transform.up * m_playerActions.m_moveVertical;
			force *= freeLookPresets[presetID].force * num;
			m_cameraMovement.ApplyVelocity(force);
		}
	}
}
