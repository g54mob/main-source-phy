using Sirenix.OdinInspector;

namespace Landfall.TABS.GameState
{
	public abstract class GameStateListener : SerializedMonoBehaviour
	{
		private GameStateManager m_manager;

		public GameStateManager GameStateManager => m_manager;

		protected virtual void Awake()
		{
			m_manager = ServiceLocator.GetService<GameStateManager>();
			m_manager.RegisterListener(this);
		}

		protected virtual void OnDestroy()
		{
			if (m_manager != null)
			{
				m_manager.UnregisterListener(this);
				m_manager = null;
			}
		}

		public virtual void OnEnterNewScene()
		{
		}

		public virtual void OnEnterPlacementStateEarly()
		{
		}

		public abstract void OnEnterPlacementState();

		public virtual void OnExitPlacementState()
		{
		}

		public abstract void OnEnterBattleState();

		public virtual void OnExitBattleState()
		{
		}

		public virtual void OnMatchOver()
		{
		}
	}
}
