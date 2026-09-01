using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Modding
{
	public class GamePrefabs : MonoBehaviour
	{
		public enum ProjectileType
		{
			CannonBall = 0,
			CrossbowArrow = 1,
			BowArrow = 2
		}

		public enum ExplosionType
		{
			Large = 0,
			Small = 1,
			Firework = 2
		}

		[SerializeField]
		private List<GameObject> projectiles = new List<GameObject>();

		[SerializeField]
		private List<GameObject> explosions = new List<GameObject>();

		private static GamePrefabs _instance;

		public static GamePrefabs Instance
		{
			get
			{
				if (_instance == null)
				{
					Debug.LogError("GamePrefabs has no instance.");
					return null;
				}
				return _instance;
			}
		}

		public static GameObject InstantiateProjectile(ProjectileType type, [Optional] Vector3 position, [Optional] Quaternion rotation, Transform parent = null)
		{
			if (rotation == default(Quaternion))
			{
				rotation = Quaternion.identity;
			}
			return (GameObject)Object.Instantiate(Instance.projectiles[(int)type], position, rotation, parent);
		}

		public static GameObject InstantiateExplosion(ExplosionType type, [Optional] Vector3 position, [Optional] Quaternion rotation, Transform parent = null)
		{
			if (rotation == default(Quaternion))
			{
				rotation = Quaternion.identity;
			}
			return (GameObject)Object.Instantiate(Instance.explosions[(int)type], position, rotation, parent);
		}

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				_instance = this;
			}
		}
	}
}
