using Landfall.TABS.AI;
using Landfall.TABS.AI.Systems;
using Unity.Entities;

namespace Landfall.TABS.GameMode
{
	public class UnitCreatorGameMode : BaseGameMode
	{
		public override void Start()
		{
			base.Start();
			base.Brush = null;
			base.BattleBudget = null;
		}

		public override void OnEnterNewScene()
		{
			base.OnEnterNewScene();
		}

		public override void OnEnterPlacementState()
		{
		}

		public override void OnEnterBattleState()
		{
		}

		public override void OnExitBattleState()
		{
		}

		public override void OnExitPlacementState()
		{
		}

		public override void Update()
		{
		}

		public override void OnClearedTeam(Team team)
		{
		}

		public override void OnUnitRemoved(Unit unit, Team team)
		{
		}

		public override void OnUnitSpawned(Unit unit, Team team)
		{
		}

		public override void OnUnitDied(Unit unit)
		{
			unit.GetComponent<UnitAPI>().SetIsDead();
			World.Active.GetExistingManager<TeamSystem>().TriggerUnitDeathAction(unit);
		}
	}
}
