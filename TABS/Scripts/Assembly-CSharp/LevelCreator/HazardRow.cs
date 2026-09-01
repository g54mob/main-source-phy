using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class HazardRow : DataTableRow
	{
		public string name;

		public GameObject hazardObject;

		public Sprite icon;

		public float cooldown = 0.3f;

		[Space]
		public int burstCount = 1;

		public float burstDelayMin = 0.1f;

		public float burstDelayMax = 0.1f;

		public float burstRadius = 10f;

		public string GetRowName()
		{
			return name;
		}
	}
}
