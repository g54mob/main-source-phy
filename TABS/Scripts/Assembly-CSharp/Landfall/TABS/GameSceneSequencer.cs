using System.Collections;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	public class GameSceneSequencer : GameStateListener
	{
		public PlacementUI placementUI;

		private Camera cam;

		private SandboxPlacementCamera m_camera;

		private PlayerActions m_playerActions;

		private FixedTimeStepService fixedTimeStepService;

		private CursorController m_cursorController;

		private bool m_inPlacementState;

		private Vector3 m_lastCursorPosition;

		private Vector3 m_lastMousePosition;

		private float m_maxSpeed = 500f;

		private float m_minSpeed = 100f;

		private const float m_minDeltaMovement = 0.15f;

		private Vector3 lastPos;

		private int unitsSpawned;

		protected override void Awake()
		{
			base.Awake();
			cam = GetComponentInChildren<Camera>();
			m_camera = GetComponentInChildren<SandboxPlacementCamera>();
			m_playerActions = PlayerActions.Instance;
			fixedTimeStepService = ServiceLocator.GetService<FixedTimeStepService>();
		}

		private void Start()
		{
			ServiceLocator.GetService<GameModeService>().CurrentGameMode.Brush.SetGameSceneSequencer(this);
			placementUI = Object.FindObjectOfType<PlacementUI>();
			m_cursorController = ServiceLocator.GetService<CursorController>();
		}

		public override void OnEnterBattleState()
		{
			m_inPlacementState = false;
			Time.maximumDeltaTime = 0.1f;
			StartCoroutine(RunEnterBattleStateSequence());
		}

		private IEnumerator RunEnterBattleStateSequence()
		{
			yield return null;
		}

		public override void OnEnterPlacementState()
		{
			switch (fixedTimeStepService.CurrentFixedTimeStep)
			{
			case FixedTimeStepService.FixedTimeStep.SixtyUpdates:
				Time.maximumDeltaTime = 1f / 60f;
				break;
			case FixedTimeStepService.FixedTimeStep.ThirtyUpdates:
				Time.maximumDeltaTime = 1f / 30f;
				break;
			default:
				Debug.LogError("This FixedTimeStep has not been handled. This should not happen.Defaulting to 60 fixed updates per second.");
				Time.maximumDeltaTime = 1f / 60f;
				break;
			}
			RemoveAfterSeconds[] array = Object.FindObjectsOfType<RemoveAfterSeconds>();
			for (int i = 0; i < array.Length; i++)
			{
				Object.Destroy(array[i].gameObject);
			}
			unitsSpawned = 0;
		}

		private void Update()
		{
			_ = m_inPlacementState;
		}

		public Ray GetCameraRay()
		{
			return cam.ScreenPointToRay(m_cursorController.CursorPosition);
		}
	}
}
