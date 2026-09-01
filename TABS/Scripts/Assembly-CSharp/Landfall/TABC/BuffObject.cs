using System;
using UnityEngine;

namespace Landfall.TABC
{
	[Serializable]
	public class BuffObject
	{
		public enum SpawnOn
		{
			Self = 0,
			AllTeamMates = 1,
			AllEnemies = 2
		}

		public SpawnOn spawnOwn;

		public GameObject objectToSpawn;
	}
}
