using Landfall.TABS;
using Landfall.TABS.GameState;
using UnityEngine;

namespace TFBGames
{
	public class BattleStateUIController : MonoBehaviour
	{
		private const float WaitTimeBeforeDisableUI = 1.5f;

		[SerializeField]
		private GameObject placementStateUIRoot;

		[SerializeField]
		private PlacementUI placementUI;

		private GameStateManager gameStateManager;

		private float timer;

		private bool timerEnabled;

		private void Awake()
		{
			gameStateManager = ServiceLocator.GetService<GameStateManager>();
		}

		private void OnEnable()
		{
			DisableTimer();
			gameStateManager.GameStateChanged += GameStateManagerOnGameStateChanged;
		}

		private void OnDisable()
		{
			gameStateManager.GameStateChanged -= GameStateManagerOnGameStateChanged;
		}

		private void Update()
		{
			if (!timerEnabled)
			{
				return;
			}
			timer += Time.deltaTime;
			if (timer >= 1.5f)
			{
				DisableTimer();
				if (placementStateUIRoot != null)
				{
					placementStateUIRoot.SetActive(value: false);
				}
			}
		}

		private void GameStateManagerOnGameStateChanged(GameState newGameState)
		{
			if (newGameState == GameState.BattleState)
			{
				OnEnterBattleState();
			}
			else
			{
				OnExitBattleState();
			}
		}

		private void OnEnterBattleState()
		{
			timerEnabled = true;
		}

		private void OnExitBattleState()
		{
			DisableTimer();
			if (placementStateUIRoot != null)
			{
				placementStateUIRoot.SetActive(value: true);
				placementUI.enabled = true;
			}
		}

		private void DisableTimer()
		{
			timer = 0f;
			timerEnabled = false;
		}
	}
}
