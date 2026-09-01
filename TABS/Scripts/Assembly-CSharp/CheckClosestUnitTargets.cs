using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI.Systems;
using Unity.Entities;
using UnityEngine;

public class CheckClosestUnitTargets : MonoBehaviour
{
	public bool useUnitTeam = true;

	private Unit unit;

	private Team team;

	private TeamHolder teamholder;

	private TeamSystem m_teamSystem;

	private List<Unit> orderedUnits;

	private void Start()
	{
		SetTeamSystem();
	}

	private List<Unit> GetUnitsAndSort()
	{
		team = GetTeam();
		List<Unit> teamUnits = m_teamSystem.GetTeamUnits((team == Team.Red) ? Team.Blue : Team.Red);
		teamUnits.Sort((Unit x, Unit y) => Vector3.Distance(base.transform.position, x.data.mainRig.position).CompareTo(Vector3.Distance(base.transform.position, y.data.mainRig.position)));
		return teamUnits;
	}

	public List<Unit> GetTargets(float? maxRange = 0f)
	{
		orderedUnits = new List<Unit>();
		orderedUnits.AddRange(GetUnitsAndSort());
		if (maxRange != 0f)
		{
			for (int i = 0; i < orderedUnits.Count; i++)
			{
				if (Vector3.Distance(base.transform.position, orderedUnits[i].data.mainRig.transform.position) > maxRange)
				{
					orderedUnits.RemoveRange(i, orderedUnits.Count - i);
					break;
				}
			}
		}
		return orderedUnits;
	}

	private Team GetTeam()
	{
		if (useUnitTeam)
		{
			unit = GetComponentInParent<Unit>();
			if ((bool)unit)
			{
				team = unit.Team;
			}
		}
		else
		{
			teamholder = base.transform.GetComponent<TeamHolder>();
			if ((bool)teamholder)
			{
				team = teamholder.team;
			}
		}
		return team;
	}

	private void SetTeamSystem()
	{
		m_teamSystem = World.Active.GetOrCreateManager<TeamSystem>();
	}
}
