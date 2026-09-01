using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "UnitDatabse", menuName = "Landfall/TABC/UnitDatabase", order = 999999999)]
	public class UnitDatabase : SerializedScriptableObject
	{
		[Serializable]
		public struct Entry
		{
			public SimulatedUnitBlueprint blueprint;
		}

		public Entry[] units;

		public SimulatedUnitBlueprint GetUnitFromID(Guid ID)
		{
			for (int i = 0; i < units.Length; i++)
			{
				if (units[i].blueprint.Guid == ID)
				{
					return units[i].blueprint;
				}
			}
			return null;
		}

		public SimulatedUnitBlueprint GetRandomUnit()
		{
			int num = UnityEngine.Random.Range(0, units.Length);
			return units[num].blueprint;
		}
	}
}
