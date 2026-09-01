using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class ConquestPlayer : MonoBehaviour
{
	public int gold = 100;

	public ConquestUnitWrapper[] startUnits;

	public Dictionary<UnitBlueprint, int> units = new Dictionary<UnitBlueprint, int>();

	private ConquestUserUI conquestUI;

	private void Awake()
	{
		for (int i = 0; i < startUnits.Length; i++)
		{
			units.Add(startUnits[i].unit, startUnits[i].number);
		}
		conquestUI = GetComponentInParent<ConquestUserUI>();
		conquestUI.PopulatePlayerArmyBar(units);
	}

	public bool TryToUpgradeUnit(UnitBlueprint from, UnitBlueprint to, bool justChecking = false)
	{
		float num = to.GetUnitCost() - from.GetUnitCost();
		if ((float)gold < num)
		{
			return false;
		}
		if (RemoveUnit(from, justChecking))
		{
			if (!justChecking)
			{
				gold -= (int)num;
				AddUnit(to);
			}
			return true;
		}
		return false;
	}

	public void AddUnit(UnitBlueprint unit)
	{
		conquestUI.PopulatePlayerArmyBar(units);
		if (units.ContainsKey(unit))
		{
			units[unit]++;
		}
		else
		{
			units.Add(unit, 1);
		}
	}

	public bool RemoveUnit(UnitBlueprint unit, bool justChecking = false)
	{
		conquestUI.PopulatePlayerArmyBar(units);
		if (units.ContainsKey(unit) && units[unit] > 0)
		{
			if (!justChecking)
			{
				units[unit]--;
			}
			return true;
		}
		return false;
	}
}
