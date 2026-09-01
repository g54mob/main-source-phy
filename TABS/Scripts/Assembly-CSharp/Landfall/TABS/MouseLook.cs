using System;
using System.Collections;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	public class MouseLook : MonoBehaviour
	{
		public float sensitivity = 1f;

		public MouseLookPreset[] m_MouseLookPresets = new MouseLookPreset[3];

		public int m_currentMouseLookPreset;

		private float m_currentX;

		private float m_currentY;

		[HideInInspector]
		public Vector2 mouseDirection;

		private Vector2 thisFrame;

		private Vector2 velocity;

		private PlayerCamera m_playerCamera;

		private SettingsInstance m_settingSensitivity;

		private SettingsInstance m_settingInvertX;

		private SettingsInstance m_settingInvertY;

		private float m_sensitivity;

		private float m_invertX;

		private float m_invertY;

		public bool ignoreMenu;

		private bool m_allowInput;

		private ModalPanel m_modalPanel;

		private ITimeService m_timeService;

		public PlayerActions noPlayerActions { get; private set; }

		private void Awake()
		{
			m_playerCamera = GetComponent<PlayerCamera>();
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			m_settingSensitivity = service.GetSettingsInstance("CONTROL_LOOK");
			m_settingInvertX = service.GetSettingsInstance("CONTROL_INVERT_X");
			m_settingInvertY = service.GetSettingsInstance("CONTROL_INVERT_Y");
			m_settingSensitivity.OnSliderValueChanged += OnChangedSensitivity;
			m_settingInvertX.OnValueChanged += OnChangedInvertX;
			m_settingInvertY.OnValueChanged += OnChangedInvertY;
			OnChangedSensitivity(m_settingSensitivity.NormalizedSliderValue);
			OnChangedInvertX(m_settingInvertX.currentValue);
			OnChangedInvertY(m_settingInvertY.currentValue);
			noPlayerActions = PlayerActions.Instance;
		}

		private void Start()
		{
			m_modalPanel = ServiceLocator.GetService<ModalPanel>();
			m_timeService = ServiceLocator.GetService<ITimeService>();
		}

		private void OnDestroy()
		{
			m_settingSensitivity.OnSliderValueChanged -= OnChangedSensitivity;
			m_settingInvertX.OnValueChanged -= OnChangedInvertX;
			m_settingInvertY.OnValueChanged -= OnChangedInvertY;
		}

		private void OnChangedSensitivity(float value)
		{
			m_sensitivity = Mathf.Max(m_settingSensitivity.NormalizedSliderValue * 2f, 0.05f);
		}

		private void OnChangedInvertX(int value)
		{
			m_invertX = ((value == 1) ? (-1f) : 1f);
		}

		private void OnChangedInvertY(int value)
		{
			m_invertY = ((value == 1) ? (-1f) : 1f);
		}

		public void SetRotationToCurrent()
		{
			m_currentX = base.transform.localEulerAngles.x;
			m_currentY = base.transform.localEulerAngles.y;
			velocity = Vector2.zero;
		}

		public void RotateTowards(float targetX, float targetY, Action doneRotating = null)
		{
			StartCoroutine(RotateTowardsCoroutine(targetX, targetY, doneRotating));
		}

		private IEnumerator RotateTowardsCoroutine(float targetX, float targetY, Action doneRotating)
		{
			m_allowInput = false;
			bool flag = false;
			bool flag2 = false;
			while (!flag || !flag2)
			{
				yield return null;
				m_currentX = Mathf.MoveTowardsAngle(m_currentX, targetX, Time.deltaTime * 1000f);
				m_currentY = Mathf.MoveTowardsAngle(m_currentY, targetY, Time.deltaTime * 1000f);
				base.transform.localEulerAngles = new Vector3(m_currentX, m_currentY, 0f);
				flag = Mathf.Approximately(m_currentX, targetX);
				flag2 = Mathf.Approximately(m_currentY, targetY);
			}
			doneRotating?.Invoke();
			m_allowInput = true;
		}

		private void OnEnable()
		{
			SetRotationToCurrent();
		}

		private void Update()
		{
			if (!m_allowInput)
			{
				return;
			}
			float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f);
			if ((m_timeService.IsPaused() || m_modalPanel.PauseGame) && !ignoreMenu)
			{
				thisFrame.x = 0f;
				thisFrame.y = 0f;
			}
			else
			{
				float num2 = sensitivity * m_sensitivity * m_MouseLookPresets[m_currentMouseLookPreset].sensitivity;
				if (m_playerCamera != null)
				{
					thisFrame.x = (0f - m_playerCamera.Actions.m_aim.Y) * num2 * m_invertY;
					thisFrame.y = m_playerCamera.Actions.m_aim.X * num2 * m_invertX;
				}
				else
				{
					thisFrame.x = (0f - noPlayerActions.m_aim.Y) * num2 * m_invertY;
					thisFrame.y = noPlayerActions.m_aim.X * num2 * m_invertX;
				}
			}
			if (m_MouseLookPresets[m_currentMouseLookPreset].smoothCam)
			{
				velocity.x += thisFrame.x * num;
				velocity.y += thisFrame.y * num;
				velocity.x -= velocity.x * Mathf.Clamp(m_MouseLookPresets[m_currentMouseLookPreset].damper * num * 2f, 0f, 1f);
				velocity.y -= velocity.y * Mathf.Clamp(m_MouseLookPresets[m_currentMouseLookPreset].damper * num * 2f, 0f, 1f);
				m_currentY += velocity.y * num * 100f;
				m_currentX += velocity.x * num * 100f;
			}
			else
			{
				m_currentY += thisFrame.y;
				m_currentX += thisFrame.x;
			}
			mouseDirection = new Vector2(thisFrame.y, 0f - thisFrame.x);
			m_currentX = Mathf.Clamp(m_currentX, -90f, 90f);
			base.transform.localEulerAngles = new Vector3(m_currentX, m_currentY, 0f);
		}

		public void AlowInput(bool allow)
		{
			m_allowInput = allow;
		}
	}
}
