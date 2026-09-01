using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameState;
using Unity.Entities;
using UnityEngine;

public class AddObjectToAllUnits : GameStateListener
{
	public GameObject objectToAdd;

	private TeamSystem m_teamSystem;

	public override void OnEnterBattleState()
	{
		if (base.gameObject.activeSelf)
		{
			GoToCenterInternal(m_teamSystem.GetAllUnits());
		}
	}

	public override void OnEnterPlacementState()
	{
	}

	private void Start()
	{
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
	}

	private void GoToCenterInternal(List<Unit> allUnits)
	{
		for (int i = 0; i < allUnits.Count; i++)
		{
			Object.Instantiate(objectToAdd, allUnits[i].transform.position, allUnits[i].transform.rotation, allUnits[i].transform);
		}
	}
}
