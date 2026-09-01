using EzECS;
using Landfall.TABS.GameState;

namespace Landfall.TABS.AI
{
	public class AIWorld : GameStateListener
	{
		protected override void Awake()
		{
			base.Awake();
		}

		public override void OnEnterBattleState()
		{
			WorldManager.EnableAllSystems();
		}

		public override void OnEnterPlacementState()
		{
			WorldManager.DisableAllSystems();
		}
	}
}
