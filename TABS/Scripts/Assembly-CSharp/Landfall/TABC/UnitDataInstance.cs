using System;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	[Serializable]
	public class UnitDataInstance
	{
		public SimulatedUnitBlueprint unit;

		public GameObject unitObject;

		public bool ownedByPlayer;

		public int2 boardPos;

		public int level = 1;

		public UnitDataInstance(SimulatedUnitBlueprint unit, GameObject unitObject, bool ownedByPlayer, int2 boardPos, int level = 1)
		{
			this.unit = unit;
			this.unitObject = unitObject;
			this.ownedByPlayer = ownedByPlayer;
			this.boardPos = boardPos;
			this.level = level;
		}

		public GameObject Spawn()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(unit.m_Prefab);
			UnitData component = gameObject.GetComponent<UnitData>();
			component.dataInstance.ownedByPlayer = ownedByPlayer;
			component.dataInstance.unitObject = unitObject;
			component.dataInstance.boardPos = boardPos;
			component.dataInstance.unit = unit;
			return gameObject;
		}
	}
}
