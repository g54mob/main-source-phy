using System;
using Landfall.TABS;
using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "NewSimulatedUnitBlueprint", menuName = "Landfall/TABC/Simulated Unit Blueprint", order = 999999998)]
	public class SimulatedUnitBlueprint : ScriptableObject
	{
		public int numberOfUnits = 1;

		public int cost = 2;

		public Alliance[] alliances;

		[Space(20f)]
		public float damagePerLevel = 0.3f;

		public float attackSpeedPerLevel = 0.25f;

		public float hpPerLevel = 400f;

		public float hpMultiplierPerLevel = 0.5f;

		public UnitBlueprint unitBlueprint;

		public string ID;

		public GameObject m_Prefab;

		public Guid Guid
		{
			get
			{
				return Guid.Parse(ID);
			}
			set
			{
				ID = value.ToString();
			}
		}

		public void NewID()
		{
			Guid = Guid.NewGuid();
		}
	}
}
