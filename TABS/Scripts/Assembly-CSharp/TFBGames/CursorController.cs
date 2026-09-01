using System.Collections.Generic;
using Landfall.TABS.Services;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TFBGames
{
	public class CursorController : ServicePrefab
	{
		private const float ReferenceScreenHeight = 1080f;

		[SerializeField]
		[Tooltip("The mouse must move at least this distance to update the cursor position. (This is in pixels.)")]
		protected float m_mouseMinDeltaMovement = 0.387298f;

		[SerializeField]
		[Tooltip("The gamepad must move at least this distance to update the cursor position. (This is a deadzone in the range 0 to 1.)")]
		[Range(0f, 1f)]
		protected float m_gamepadMinDeltaMovement = 0.15f;

		[SerializeField]
		[Tooltip("The min speed, used when the thumbstick is at the centre. (This is in pixels per second.)")]
		protected float m_gamepadMinSpeed = 100f;

		[SerializeField]
		[Tooltip("The max speed, used when the thumbstick is at its max position in any direction. (This is in pixels per second.)")]
		protected float m_gamepadMaxSpeed = 500f;

		private PlayerActions m_playerActions;

		private Vector3 m_lastCursorPosition;

		private Vector3 m_lastMousePosition;

		private float m_mouseMinDeltaMovementSqr;

		private float m_gamepadMinDeltaMovementSqr;

		private float m_gamepadMinSpeedScaled;

		private float m_gamepadMaxSpeedScaled;

		private bool m_allowCursorControllerMovement;

		private List<RaycastResult> m_raycastResults = new List<RaycastResult>();

		private PointerEventData m_pointerEventData;

		private EventSystem m_eventSystem;

		private RaycastResult m_topMostObjectBeneathCursor;

		private int m_raycastFrame;

		private ITimeService m_timeService;

		public Vector3 CursorPosition => m_lastCursorPosition;

		public PointerEventData PointerEventData => m_pointerEventData;

		public GameObject GetObjectBeneathPointer()
		{
			FindObjectsBeneathCursor();
			return m_topMostObjectBeneathCursor.gameObject;
		}

		public void GetObjectsBeneathPointer(List<RaycastResult> raycastResults)
		{
			if (raycastResults != null)
			{
				FindObjectsBeneathCursor();
				raycastResults.AddRange(m_raycastResults);
			}
		}

		public bool IsPointerOverGameObject()
		{
			return GetObjectBeneathPointer() != null;
		}

		public override void OnStart()
		{
			base.OnStart();
			m_playerActions = PlayerActions.Instance;
			float num = ScalePixelsForScreen(m_mouseMinDeltaMovement);
			m_mouseMinDeltaMovementSqr = num * num;
			m_gamepadMinDeltaMovementSqr = m_gamepadMinDeltaMovement * m_gamepadMinDeltaMovement;
			m_gamepadMinSpeedScaled = ScalePixelsForScreen(m_gamepadMinSpeed);
			m_gamepadMaxSpeedScaled = ScalePixelsForScreen(m_gamepadMaxSpeed);
			m_timeService = ServiceLocator.GetService<ITimeService>();
			CenterCursor();
			SetEventSystem(EventSystem.current);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			EventSystem current = EventSystem.current;
			if (m_eventSystem != current)
			{
				SetEventSystem(current);
			}
			UpdatePosition();
		}

		private void SetEventSystem(EventSystem newEventSystem)
		{
			m_eventSystem = newEventSystem;
			if (m_eventSystem != null)
			{
				m_pointerEventData = new PointerEventData(m_eventSystem);
			}
		}

		private float ScalePixelsForScreen(float pixelsToScale)
		{
			return pixelsToScale * ((float)Screen.height / 1080f);
		}

		private void UpdatePosition()
		{
			Vector3 lastCursorPosition = m_lastCursorPosition;
			lastCursorPosition = Input.mousePosition;
			bool num = (m_lastMousePosition - lastCursorPosition).sqrMagnitude > m_mouseMinDeltaMovementSqr;
			m_lastMousePosition = Input.mousePosition;
			if (!num)
			{
				lastCursorPosition = m_lastCursorPosition;
			}
			if (!m_allowCursorControllerMovement)
			{
				return;
			}
			if (m_playerActions.InputType == InputType.Controller)
			{
				Vector2 value = m_playerActions.m_aim.Value;
				float sqrMagnitude = value.sqrMagnitude;
				bool num2 = sqrMagnitude > m_gamepadMinDeltaMovementSqr;
				bool flag = m_timeService.IsPaused();
				if (num2 && !flag)
				{
					sqrMagnitude = Mathf.Sqrt(sqrMagnitude);
					Vector3 vector = Mathf.Lerp(m_gamepadMinSpeedScaled, m_gamepadMaxSpeedScaled, sqrMagnitude) * Time.unscaledDeltaTime * value.normalized;
					lastCursorPosition = m_lastCursorPosition + vector;
				}
			}
			lastCursorPosition.x = Mathf.Clamp(lastCursorPosition.x, 0f, Screen.width);
			lastCursorPosition.y = Mathf.Clamp(lastCursorPosition.y, 0f, Screen.height);
			m_lastCursorPosition = lastCursorPosition;
		}

		private void FindObjectsBeneathCursor()
		{
			if (!(m_eventSystem == null) && m_raycastFrame != Time.frameCount)
			{
				m_raycastFrame = Time.frameCount;
				m_pointerEventData.position = new Vector2(CursorPosition.x, CursorPosition.y);
				m_raycastResults.Clear();
				m_eventSystem.RaycastAll(m_pointerEventData, m_raycastResults);
				if (m_raycastResults.Count <= 0)
				{
					m_topMostObjectBeneathCursor.Clear();
				}
				else
				{
					m_topMostObjectBeneathCursor = m_raycastResults[0];
				}
			}
		}

		public void CenterCursor()
		{
			m_lastCursorPosition = new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f);
		}

		public void AllowCursorMovement(bool allow)
		{
			m_allowCursorControllerMovement = allow;
		}
	}
}
