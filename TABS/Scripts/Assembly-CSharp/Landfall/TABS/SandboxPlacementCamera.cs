using System.Collections;
using Landfall.TABS.GameMode;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS
{
	public class SandboxPlacementCamera : MonoBehaviour
	{
		public int presetID;

		public CameraMovementPreset[] freeLookPresets;

		public float m_PlacementFOV = 35f;

		private float m_TargetFOV = 60f;

		public float m_FreelookFOV = 60f;

		public float m_FreelookFOVFP = 80f;

		public float m_PlacementCameraForce = 10f;

		public float m_PlacementDamper = 0.95f;

		public float m_controllerZoomDamper = 0.01f;

		public float m_GoToPosForce = 10f;

		public float m_GoToPosDamper = 0.8f;

		private Camera m_camera;

		private CameraMovement m_cameraMovement;

		private MouseLook m_mouseLook;

		private Vector3 placementDefaultPos;

		private Vector3 cameraPlacementStartPosition;

		private Quaternion m_freelookStartRotation;

		private Coroutine m_resetPosistionCoroutine;

		private bool m_isGoingToPos;

		private BaseGameMode m_currentGameMode;

		private ScreenShake m_screenShake;

		private RotationShake m_rotationShake;

		private SettingsInstance m_fovSetting;

		private SettingsInstance m_fovSettingFP;

		private SettingsInstance m_shakeSetting;

		private CameraAbilityPossess m_cameraPossess;

		private PiratePlacementTransparency[] m_placementTransparency;

		private CursorVisibilityController m_cursorVisibility;

		private PlayerCamera m_playerCamera;

		private bool m_changedFreeLookThisFrame;

		private bool m_AllowMovement = true;

		private SettingsInstance m_cameraMode;

		public bool IsReturning => m_isGoingToPos;

		public bool IsResettingRotation { get; private set; }

		public static bool InFreeLook { get; private set; }

		public void SetPresetID(int newValue)
		{
			presetID = newValue;
		}

		private void Awake()
		{
			if ((bool)MapSettings.Instance)
			{
				base.transform.position += MapSettings.Instance.cameraPositionOffset;
				placementDefaultPos = base.transform.position;
				cameraPlacementStartPosition = base.transform.position;
			}
			m_camera = base.transform.GetComponentInChildren<Camera>();
			m_cameraMovement = GetComponent<CameraMovement>();
			m_mouseLook = GetComponent<MouseLook>();
			m_freelookStartRotation = base.transform.rotation;
			m_currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			m_screenShake = GetComponentInChildren<ScreenShake>();
			m_rotationShake = GetComponentInChildren<RotationShake>();
			m_fovSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("VIDEO_FOV");
			m_fovSettingFP = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("VIDEO_FOV_FP");
			m_shakeSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_SCREENSHAKE");
			m_cameraMode = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_CAMERAMODE");
			if (m_cameraMode != null)
			{
				m_cameraMode.OnValueChanged += SetPresetID;
				SetPresetID(m_cameraMode.currentValue);
			}
			m_fovSetting.OnSliderValueChanged += OnFOVChanged;
			OnFOVChanged(m_fovSetting.currentSliderValue);
			m_fovSettingFP.OnSliderValueChanged += OnFOVChangedFP;
			OnFOVChangedFP(m_fovSettingFP.currentSliderValue);
			m_shakeSetting.OnSliderValueChanged += OnShakeChanged;
			OnShakeChanged(m_shakeSetting.currentSliderValue);
			m_cameraPossess = GetComponent<CameraAbilityPossess>();
			if (m_cameraPossess != null)
			{
				m_cameraPossess.OnPossessUpdate += OnFirstPersonChanged;
				OnFirstPersonChanged();
			}
			m_placementTransparency = Object.FindObjectsOfType<PiratePlacementTransparency>();
			m_cursorVisibility = ServiceLocator.GetService<CursorVisibilityController>();
			m_playerCamera = GetComponent<PlayerCamera>();
		}

		private void Start()
		{
			m_currentGameMode.Brush.SetPlacementCamera(this);
			PiratePlacementTransparency[] array = Object.FindObjectsOfType<PiratePlacementTransparency>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].MakeTransparent();
			}
		}

		private void OnFirstPersonChanged()
		{
			OnFirstPersonChanged(!(m_cameraPossess == null) && m_cameraPossess.IsPossessing);
		}

		private void OnFirstPersonChanged(bool isFirstPerson)
		{
			m_TargetFOV = (isFirstPerson ? m_FreelookFOVFP : m_FreelookFOV);
		}

		private void OnDestroy()
		{
			if (m_cameraMode != null)
			{
				m_cameraMode.OnValueChanged -= SetPresetID;
			}
			if (m_fovSetting != null)
			{
				m_fovSetting.OnSliderValueChanged -= OnFOVChanged;
			}
			if (m_fovSettingFP != null)
			{
				m_fovSettingFP.OnSliderValueChanged -= OnFOVChangedFP;
			}
			if (m_shakeSetting != null)
			{
				m_shakeSetting.OnSliderValueChanged -= OnShakeChanged;
			}
			if (m_cameraPossess != null)
			{
				m_cameraPossess.OnPossessUpdate -= OnFirstPersonChanged;
			}
		}

		private void OnFOVChanged(float value)
		{
			m_FreelookFOV = value;
			OnFirstPersonChanged();
		}

		private void OnFOVChangedFP(float value)
		{
			m_FreelookFOVFP = value;
			OnFirstPersonChanged();
		}

		private void OnShakeChanged(float value)
		{
			float rotationMultiplier = value * 0.01f;
			m_screenShake.RotationMultiplier = rotationMultiplier;
			m_rotationShake.RotationMultiplier = rotationMultiplier;
		}

		public IEnumerator GoToPosInternal(Vector3 pos, float t = 2f)
		{
			m_isGoingToPos = true;
			float timer = 0f;
			while (timer <= t)
			{
				m_cameraMovement.ApplyVelocity((pos - base.transform.position) * m_GoToPosForce);
				bool hasReachedTarget = ((Vector3.Distance(pos, base.transform.position) < 1.5f) ? true : false);
				yield return null;
				timer += Time.unscaledDeltaTime;
				if (hasReachedTarget)
				{
					timer += Time.unscaledDeltaTime * 19f;
				}
			}
			m_isGoingToPos = false;
		}

		public void ResetRotation()
		{
		}

		private IEnumerator ResetRotationInernal(Quaternion rotation, float t = 1f)
		{
			IsResettingRotation = true;
			for (float timer = 0f; timer <= t; timer += Time.unscaledDeltaTime)
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, rotation, Time.unscaledDeltaTime * 7.5f);
				yield return null;
			}
			IsResettingRotation = false;
		}

		private void LateUpdate()
		{
			m_changedFreeLookThisFrame = false;
		}

		private void Update()
		{
			if (UIScreenInputBlocker.BlockCameraMovement || !m_AllowMovement)
			{
				return;
			}
			bool flag = m_playerCamera.Actions.InputType == InputType.Controller;
			if (!m_currentGameMode.TimeService.IsPaused())
			{
				if (InFreeLook)
				{
					m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_TargetFOV, Time.unscaledDeltaTime * 4.5f);
					float num = 1f;
					if (m_playerCamera.Actions.m_moveFast.IsPressed)
					{
						num = 3f;
					}
					Vector3 force = default(Vector3);
					Transform transform = base.transform;
					force += transform.right * m_playerCamera.Actions.m_move.X;
					force += transform.forward * m_playerCamera.Actions.m_move.Y;
					force += transform.up * m_playerCamera.Actions.m_moveVertical;
					force *= freeLookPresets[presetID].force * num;
					m_cameraMovement.ApplyVelocity(force);
				}
				else
				{
					float num2 = 1f;
					if (!flag && m_playerCamera.Actions.m_moveFast.IsPressed)
					{
						num2 = 3f;
					}
					m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_PlacementFOV, Time.unscaledDeltaTime * 4.5f);
					Vector3 force2 = default(Vector3);
					force2.x += m_playerCamera.Actions.m_move.X;
					force2.z += m_playerCamera.Actions.m_move.Y;
					force2.y += (flag ? 0f : ((float)m_playerCamera.Actions.m_moveVertical));
					if ((bool)EventSystem.current && !EventSystem.current.IsPointerOverGameObject() && !SceneSettings.IsEditingLine)
					{
						float num3 = m_playerCamera.Actions.m_placementZoom.Value;
						if (ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("UI_INPUT_MODE").currentValue == 1)
						{
							num3 = ((flag && !m_playerCamera.Actions.m_placementZoomActivate.IsPressed) ? 0f : num3);
						}
						float num4 = (flag ? m_controllerZoomDamper : 1f);
						Vector3 force3 = Mathf.Clamp(Mathf.Sqrt(Mathf.Abs(base.transform.position.y)), 10f, 50f) * num3 * m_PlacementCameraForce * num4 * 0.3f * base.transform.forward;
						m_cameraMovement.ApplyVelocityRaw(force3);
					}
					force2 *= m_PlacementCameraForce * num2;
					m_cameraMovement.ApplyVelocity(force2);
				}
			}
			if (m_isGoingToPos)
			{
				m_cameraMovement.TargetDamper = m_GoToPosDamper;
			}
			else if (InFreeLook)
			{
				m_cameraMovement.TargetDamper = freeLookPresets[presetID].damper;
			}
			else
			{
				m_cameraMovement.TargetDamper = m_PlacementDamper;
			}
		}

		public void ToggleFreeLook()
		{
			if (InFreeLook)
			{
				ExitFreeLook();
			}
			else
			{
				EnterFreeLook();
			}
		}

		public void EnterFreeLook()
		{
			if (m_placementTransparency != null)
			{
				for (int i = 0; i < m_placementTransparency.Length; i++)
				{
					m_placementTransparency[i].MakeVisable();
				}
			}
			if (m_resetPosistionCoroutine != null)
			{
				StopCoroutine(m_resetPosistionCoroutine);
			}
			InFreeLook = true;
			m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.Locked, visible: false);
			BaseGameMode currentGameMode = ServiceLocator.GetService<GameModeService>().CurrentGameMode;
			if (currentGameMode.Brush.Cursor != null)
			{
				currentGameMode.Brush.Cursor.SetCursorState(PlacementCursorState.Hide);
			}
			m_mouseLook.enabled = true;
			placementDefaultPos = base.transform.position;
		}

		public void ExitFreeLook()
		{
			if (m_placementTransparency != null)
			{
				for (int i = 0; i < m_placementTransparency.Length; i++)
				{
					m_placementTransparency[i].MakeTransparent();
				}
			}
			InFreeLook = false;
			m_cursorVisibility.SetLockStateAndVisibility(CursorLockMode.None, visible: true);
			m_mouseLook.enabled = false;
			CenterCameraPositionAndZoom(placementDefaultPos);
		}

		public void CenterCameraPositionAndZoom(Vector3 positionToCenterTo)
		{
			StartCoroutine(ResetRotationInernal(m_freelookStartRotation));
			StartCoroutine(GoToPosInternal(positionToCenterTo));
		}

		public void CenterCameraPositionAndZoom()
		{
			StartCoroutine(ResetRotationInernal(m_freelookStartRotation));
			StartCoroutine(GoToPosInternal(cameraPlacementStartPosition));
		}

		public bool IsInFreeLook()
		{
			return InFreeLook;
		}

		public void EnterBattleState()
		{
			EnterFreeLook();
		}

		public void AllowMovement(bool allow)
		{
			m_AllowMovement = allow;
			m_mouseLook.AlowInput(allow);
		}
	}
}
