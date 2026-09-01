using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class UnitEditorSpawnTestEnemies : MonoBehaviour
{
	private struct SpawnedUnit
	{
		public string name;

		public uint cost;

		public SpawnedUnit(string name, uint cost)
		{
			this.name = name;
			this.cost = cost;
		}
	}

	public Faction[] factionsToSpawn;

	public UnitBlueprint[] units;

	public List<GameObject> spawnedUnits;

	private void Start()
	{
		if (units.Length != 0)
		{
			return;
		}
		List<UnitBlueprint> list = new List<UnitBlueprint>();
		for (int i = 0; i < factionsToSpawn.Length; i++)
		{
			for (int j = 0; j < factionsToSpawn[i].Units.Length; j++)
			{
				list.Add(factionsToSpawn[i].Units[j]);
			}
		}
		list.Sort((UnitBlueprint unit01, UnitBlueprint unit02) => unit01.GetUnitCost().CompareTo(unit02.GetUnitCost()));
		units = list.ToArray();
	}

	private int GetMostExpensiveUnitAffordable(float money)
	{
		for (int i = 0; i < units.Length; i++)
		{
			if ((float)units[i].GetUnitCost() > money)
			{
				return i;
			}
		}
		return units.Length;
	}

	private UnitBlueprint GetRandomUnitFromRange(int max)
	{
		return units[Random.Range(0, max)];
	}

	public void Spawn(float money, Team team)
	{
		List<SpawnedUnit> list = new List<SpawnedUnit>();
		spawnedUnits = new List<GameObject>();
		float num = money;
		int num2 = 0;
		for (int i = 0; i < 100; i++)
		{
			int mostExpensiveUnitAffordable = GetMostExpensiveUnitAffordable(num);
			if (mostExpensiveUnitAffordable == -1)
			{
				break;
			}
			UnitBlueprint randomUnitFromRange = GetRandomUnitFromRange(mostExpensiveUnitAffordable);
			int a = Random.Range(1, 5);
			int b = Mathf.FloorToInt(num / (float)randomUnitFromRange.GetUnitCost());
			a = Mathf.Min(a, b);
			for (int j = 0; j < a; j++)
			{
				if (num2 == 30)
				{
					return;
				}
				num2++;
				list.Add(new SpawnedUnit(randomUnitFromRange.Name, randomUnitFromRange.GetUnitCost()));
				GameObject[] array = randomUnitFromRange.Spawn(base.transform.forward * Random.Range(10f, 20f + (float)num2) + base.transform.right * Random.Range(-num2, num2), Quaternion.identity, team);
				for (int k = 0; k < array.Length; k++)
				{
					spawnedUnits.Add(array[k]);
				}
				num -= (float)randomUnitFromRange.GetUnitCost();
			}
		}
	}

	public void Clear()
	{
		for (int i = 0; i < spawnedUnits.Count; i++)
		{
			Object.Destroy(spawnedUnits[i]);
		}
	}
}
