using Landfall.TABS.GameState;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Landfall.TABS
{
	public class PlacementCameraMovement : GameStateListener
	{
		public float m_PlacementFieldOfView = 45f;

		public float m_BattleFieldOfView = 45f;

		public float m_Speed = 1f;

		public AnimationCurve m_ReturnPosCurve;

		public AnimationCurve m_ReturnRotCurve;

		public AnimationCurve m_EnterFOVCurve;

		public AnimationCurve m_ReturnFOVCurve;

		private Vector3 m_velocity;

		private Vector3 m_startPosistion;

		private Quaternion m_startRotation;

		private Camera m_cam;

		private CameraMovement m_cameraMovement;

		private bool canMove;

		private EventSystem eventSystem;

		public override void OnEnterBattleState()
		{
			m_startPosistion = base.transform.position;
		}

		public override void OnEnterPlacementState()
		{
			base.enabled = true;
		}

		protected override void Awake()
		{
			base.Awake();
			m_startPosistion = base.transform.position;
			m_startRotation = base.transform.rotation;
			m_cam = GetComponentInChildren<Camera>();
			m_cameraMovement = GetComponent<CameraMovement>();
			eventSystem = base.transform.root.GetComponentInChildren<EventSystem>();
		}

		private void Update()
		{
			if (canMove)
			{
				MoveUpdate();
			}
		}

		private void MoveUpdate()
		{
			float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.1f);
			if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E))
			{
				m_cameraMovement.Velocity += Vector3.up * num;
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Q))
			{
				m_cameraMovement.Velocity += -Vector3.up * num;
			}
			if (Input.GetKey(KeyCode.W))
			{
				m_cameraMovement.Velocity += Vector3.forward * num;
			}
			if (Input.GetKey(KeyCode.S))
			{
				m_cameraMovement.Velocity += -Vector3.forward * num;
			}
			if (Input.GetKey(KeyCode.A))
			{
				m_cameraMovement.Velocity += -Vector3.right * num;
			}
			if (Input.GetKey(KeyCode.D))
			{
				m_cameraMovement.Velocity += Vector3.right * num;
			}
			if (!eventSystem.IsPointerOverGameObject())
			{
				m_cameraMovement.Velocity += base.transform.forward * Input.GetAxis("Mouse ScrollWheel") * num * 100f;
			}
		}
	}
}
