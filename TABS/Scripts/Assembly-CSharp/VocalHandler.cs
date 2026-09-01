using System.Collections.Generic;
using Landfall.MonoBatch;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameState;
using Unity.Entities;
using UnityEngine;

public class VocalHandler : BatchedMonobehaviour
{
	private float nextScream = 1f;

	public float speed = 1f;

	private GameStateManager m_gameStateManager;

	private TeamSystem m_teamSystem;

	protected override void Start()
	{
		base.Start();
		m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
	}

	public override void BatchedUpdate()
	{
		if (m_gameStateManager.GameState != GameState.BattleState)
		{
			return;
		}
		nextScream -= Time.deltaTime * speed;
		if (!(nextScream < 0f))
		{
			return;
		}
		List<Unit> allUnits = m_teamSystem.GetAllUnits();
		if (allUnits.Count == 0)
		{
			return;
		}
		Unit unit = allUnits[Random.Range(0, allUnits.Count)];
		if ((bool)unit && !unit.dead)
		{
			UnitSounds componentInChildren = unit.GetComponentInChildren<UnitSounds>();
			if ((bool)componentInChildren)
			{
				componentInChildren.PlayVocalSound(0, 1f, unit.data.mainRig.position);
			}
		}
		nextScream = Random.Range(0.5f, 15f) / Mathf.Pow(Mathf.Clamp(allUnits.Count, 1, 50), 0.3f);
	}
}
