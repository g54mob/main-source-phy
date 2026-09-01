using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class SnakeFoodSpawner : MonoBehaviour
	{
		public SnakeFood SnakeFoodPrefab;

		public int AmountOfFood;

		public Vector2 MinRandom;

		public Vector2 MaxRandom;

		protected List<SnakeFood> Foods;

		protected Camera _mainCamera;

		protected virtual void Start()
		{
		}

		public virtual Vector3 DetermineSpawnPosition()
		{
			return default(Vector3);
		}
	}
}
