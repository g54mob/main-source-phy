using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameState;
using Unity.Entities;
using UnityEngine;

public class Bug_BlackHole : GameStateListener
{
	public GameObject blackHole;

	private TeamSystem m_teamSystem;

	private List<Unit> units;

	public override void OnEnterBattleState()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
		units = m_teamSystem.GetAllUnits();
		for (int i = 0; i < units.Count; i++)
		{
			if (units[i].unitBlueprint.Entity.GUID.m_ID == 1349136291)
			{
				KillAfterSeconds killAfterSeconds = units[i].gameObject.AddComponent<KillAfterSeconds>();
				killAfterSeconds.seconds = 2f;
				killAfterSeconds.objectToSpawn = blackHole;
				killAfterSeconds.spawnObjectOnMainRig = true;
				killAfterSeconds.destroyRoot = true;
			}
		}
	}

	public override void OnEnterPlacementState()
	{
	}
}
