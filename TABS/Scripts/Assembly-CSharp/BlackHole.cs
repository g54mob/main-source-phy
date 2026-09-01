using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameState;
using Unity.Entities;
using UnityEngine;

public class BlackHole : GameStateListener
{
	public float force;

	public float unscalingForce;

	public float dmg;

	private TeamSystem m_teamSystem;

	private float startRange = 1f;

	public AnimationCurve rangeCurve;

	public AnimationCurve dmgCurve;

	public AnimationCurve curve;

	public float time = 60f;

	private float c;

	private List<Unit> units;

	private void Start()
	{
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
		if (units == null)
		{
			units = new List<Unit>();
			List<Unit> allUnits = m_teamSystem.GetAllUnits();
			for (int i = 0; i < allUnits.Count; i++)
			{
				units.Add(allUnits[i]);
			}
		}
	}

	private void FixedUpdate()
	{
		if (units == null)
		{
			return;
		}
		c += Time.deltaTime / time;
		float num = startRange * curve.Evaluate(c);
		for (int i = 0; i < units.Count; i++)
		{
			if (!(units[i] == null))
			{
				float num2 = Vector3.Distance(base.transform.position, units[i].data.mainRig.position);
				float num3 = rangeCurve.Evaluate(num2 / num);
				float num4 = dmgCurve.Evaluate(num2 / num);
				Vector3 normalized = (base.transform.position - units[i].data.mainRig.transform.position).normalized;
				units[i].data.mainRig.AddForce(force * num3 * normalized);
				units[i].data.mainRig.AddForce(num3 * unscalingForce * normalized, ForceMode.Acceleration);
				units[i].data.healthHandler.TakeDamage(dmg * num4 * Time.deltaTime, Vector3.up, null);
				if (num2 < num / 4f)
				{
					units[i].data.mainRig.velocity *= 0.5f;
				}
			}
		}
	}

	public override void OnEnterPlacementState()
	{
	}

	public override void OnEnterBattleState()
	{
		units = new List<Unit>();
		List<Unit> allUnits = m_teamSystem.GetAllUnits();
		for (int i = 0; i < allUnits.Count; i++)
		{
			units.Add(allUnits[i]);
		}
	}
}
