namespace Landfall.TABS.GameState
{
	public class GameStateListenerSurrogate : GameStateListener
	{
		public delegate void SurrogateCallback();

		public SurrogateCallback OnEnterNewSceneDelegate { get; set; }

		public SurrogateCallback OnEnterPlacementStateEarlyDelegate { get; set; }

		public SurrogateCallback OnEnterPlacementStateDelegate { get; set; }

		public SurrogateCallback OnExitPlacementStateDelegate { get; set; }

		public SurrogateCallback OnEnterBattleStateCallback { get; set; }

		public SurrogateCallback OnExitBattleStateCallback { get; set; }

		public override void OnEnterNewScene()
		{
			OnEnterNewSceneDelegate?.Invoke();
		}

		public override void OnEnterPlacementStateEarly()
		{
			OnEnterPlacementStateEarlyDelegate?.Invoke();
		}

		public override void OnEnterPlacementState()
		{
			OnEnterPlacementStateDelegate?.Invoke();
		}

		public override void OnEnterBattleState()
		{
			OnEnterBattleStateCallback?.Invoke();
		}

		public override void OnExitPlacementState()
		{
			OnExitPlacementStateDelegate?.Invoke();
		}

		public override void OnExitBattleState()
		{
			OnExitBattleStateCallback?.Invoke();
		}
	}
}
