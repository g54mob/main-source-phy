using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABS
{
	public class DestroyOnBattle : GameStateListener
	{
		protected override void Awake()
		{
			base.Awake();
		}

		public override void OnEnterBattleState()
		{
			Object.Destroy(base.gameObject);
		}

		public override void OnEnterPlacementState()
		{
		}
	}
}
