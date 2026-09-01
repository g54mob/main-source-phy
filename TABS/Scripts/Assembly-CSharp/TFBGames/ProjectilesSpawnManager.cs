using System;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.RuntimeCleanup;
using UnityEngine;

namespace TFBGames
{
	public class ProjectilesSpawnManager : ServicePrefab
	{
		public delegate void SpawnedProjectileEventHandler(Projectile projectile, Unit unit, byte weaponIndex, Vector3 spawnDirection, Vector3 directionToTarget, Rigidbody targetRigidbody, Vector3 shootPositionForward, bool isSpawnedFromPrefabId);

		[SerializeField]
		protected Projectile[] m_projectilePrefabs;

		private readonly Dictionary<int, int> m_prefabsIndices = new Dictionary<int, int>();

		private RuntimeGarbageCollector m_runtimeGC;

		public event SpawnedProjectileEventHandler SpawnedProjectile;

		public event Action<Projectile> DestroyedProjectile;

		public override void OnAwake()
		{
			base.OnAwake();
			if (m_projectilePrefabs == null || m_projectilePrefabs.Length == 0)
			{
				return;
			}
			int i = 0;
			for (int num = m_projectilePrefabs.Length; i < num; i++)
			{
				Projectile projectile = m_projectilePrefabs[i];
				if (!(projectile == null))
				{
					try
					{
						m_prefabsIndices.Add(projectile.PrefabId, i);
					}
					catch (Exception ex)
					{
						Debug.LogErrorFormat("Failed to add the projectile prefab to the dictionary:\n{0}", ex);
					}
				}
			}
		}

		public override void OnStart()
		{
			base.OnStart();
			m_runtimeGC = ServiceLocator.GetService<RuntimeGarbageCollector>();
		}

		public int GetPrefabIndex(int prefabId)
		{
			if (m_prefabsIndices.TryGetValue(prefabId, out var value))
			{
				return value;
			}
			return -1;
		}

		public GameObject SpawnProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
		{
			Projectile projectile;
			return SpawnProjectile(prefab, position, rotation, null, 0, Vector3.zero, Vector3.zero, null, Vector3.zero, out projectile);
		}

		public GameObject SpawnProjectile(GameObject prefab, Vector3 position, Quaternion rotation, Unit unit, byte weaponIndex, Vector3 spawnDirection, Vector3 directionToTarget, Rigidbody targetRigidbody, Vector3 shootPositionForward, out Projectile projectile, bool isSpawnedFromPrefabId = false, byte? randomSeed = null)
		{
			GameObject gameObject;
			if ((bool)prefab.GetComponent<PooledProjectile>())
			{
				PooledProjectile component = (gameObject = ServiceLocator.GetService<GameObjectPooling>().GetPool(prefab).GetObject(position, rotation)).GetComponent<PooledProjectile>();
				m_runtimeGC.AddPoolable(component);
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
				m_runtimeGC.AddGameObject(gameObject);
			}
			if (gameObject == null)
			{
				projectile = null;
				return null;
			}
			projectile = gameObject.GetComponent<Projectile>();
			if (projectile == null)
			{
				Debug.LogErrorFormat("Projectile \"{0}\" does not have the \"{1}\" component.", gameObject.name, "Projectile");
				return gameObject;
			}
			projectile.DestroyedOrReturnedToPool += OnDestroyedOrReturnedToPool;
			if (randomSeed.HasValue)
			{
				projectile.RandomSeed = randomSeed;
			}
			else if (!isSpawnedFromPrefabId)
			{
				if (projectile.GetComponentInChildren<Compensation>() != null || projectile.GetComponentInChildren<MoveTransform>() != null)
				{
					projectile.RandomSeed = (byte)UnityEngine.Random.Range(0, 32);
				}
				else
				{
					projectile.RandomSeed = null;
				}
			}
			this.SpawnedProjectile?.Invoke(projectile, unit, weaponIndex, spawnDirection, directionToTarget, targetRigidbody, shootPositionForward, isSpawnedFromPrefabId);
			return gameObject;
		}

		public Projectile SpawnProjectile(int prefabIndex, Vector3 position, Quaternion rotation, Unit unit, byte weaponIndex, Vector3 spawnDirection, Vector3 directionToTarget, Rigidbody targetRigidbody, Vector3 shootPositionForward, byte? randomSeed)
		{
			if (m_projectilePrefabs == null || prefabIndex < 0 || prefabIndex >= m_projectilePrefabs.Length || m_projectilePrefabs[prefabIndex] == null)
			{
				return null;
			}
			Projectile projectile = m_projectilePrefabs[prefabIndex];
			Projectile projectile2;
			GameObject gameObject = SpawnProjectile(projectile.gameObject, position, rotation, unit, weaponIndex, spawnDirection, directionToTarget, targetRigidbody, shootPositionForward, out projectile2, isSpawnedFromPrefabId: true, randomSeed);
			if (!(gameObject != null))
			{
				return null;
			}
			return gameObject.GetComponent<Projectile>();
		}

		private void OnDestroyedOrReturnedToPool(Projectile projectile)
		{
			if (!(projectile == null))
			{
				projectile.DestroyedOrReturnedToPool -= OnDestroyedOrReturnedToPool;
				this.DestroyedProjectile?.Invoke(projectile);
			}
		}
	}
}
