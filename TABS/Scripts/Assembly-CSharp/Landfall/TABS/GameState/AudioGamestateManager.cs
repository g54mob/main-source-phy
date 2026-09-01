namespace Landfall.TABS.GameState
{
	public class AudioGamestateManager : GameStateListener
	{
		public override void OnEnterBattleState()
		{
		}

		public override void OnEnterPlacementState()
		{
		}

		public override void OnExitBattleState()
		{
			ServiceLocator.GetService<SoundPlayer>().KillAllSoundInstances();
		}
	}
}
