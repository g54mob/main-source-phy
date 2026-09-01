using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	[Serializable]
	public class Board
	{
		[Serializable]
		public struct Unit
		{
			public GameObject unitObject;

			public UnitDataInstance unitDataInstance;

			public int2 pos;

			public UnitData unitData;

			public Unit(GameObject unitObject, UnitDataInstance unitDataInstance, int2 pos, UnitData unitData)
			{
				this.unitObject = unitObject;
				this.unitDataInstance = unitDataInstance;
				this.pos = pos;
				this.unitData = unitData;
			}
		}

		public List<Unit> Units = new List<Unit>();

		public void AddUnitToBoard(Unit newUnit)
		{
			Units.Add(newUnit);
		}
	}
}
