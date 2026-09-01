using UnityEngine;

namespace Landfall.TABS.GameState
{
	public class GameStateForcer : MonoBehaviour
	{
		[SerializeField]
		private GameState m_gameStateToForce;

		[SerializeField]
		private bool m_runOnAwake;

		[SerializeField]
		private bool m_runOnStart = true;

		private void Awake()
		{
			if (m_runOnAwake)
			{
				ForceState();
			}
			if (m_runOnAwake && m_runOnStart)
			{
				Debug.LogError("Can't run GameStateForcer in RunOnAwake AND RunOnStart. Only running in awake, please uncheck either RunOnAwake or RunOnStart!", base.gameObject);
			}
		}

		private void Start()
		{
			if (!m_runOnAwake && m_runOnStart)
			{
				ForceState();
			}
			if (!m_runOnAwake && !m_runOnStart)
			{
				Debug.LogError("Didn't run GameStateForcer, neither RunOnAwake or RunOnStart were checked.");
			}
		}

		private void ForceState()
		{
			switch (m_gameStateToForce)
			{
			case GameState.BattleState:
				ServiceLocator.GetService<GameStateManager>().EnterBattleState();
				break;
			case GameState.PlacementState:
				ServiceLocator.GetService<GameStateManager>().EnterPlacementState();
				break;
			case GameState.None:
				ServiceLocator.GetService<GameStateManager>().EnterNoneState();
				break;
			}
		}
	}
}
