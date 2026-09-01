using System.Collections;
using Landfall.TABS;
using Landfall.TABS.AI;
using UnityEngine;

namespace TFBGames
{
	public class CopySelfLimitedPool : MonoBehaviour
	{
		[Tooltip("Sets the total number of copies that a unit, and its copies, can spawn in total.")]
		[SerializeField]
		private int maxAllowedCopies = 10;

		private Unit originalUnit;

		private GameObject[] unitCopies;

		private GameObject poolParent;

		private int currentPooledUnitIndex = -1;

		private Vector3 copySpawnStartPosition = new Vector3(1000f, 1000f, 1000f);

		private INetworkService networkService;

		private void OnDestroy()
		{
			if (poolParent != null)
			{
				Object.Destroy(poolParent);
			}
		}

		public void CreatePool(Unit originalUnit)
		{
			this.originalUnit = originalUnit;
			StartCoroutine(CreateCopyPool());
		}

		private IEnumerator CreateCopyPool()
		{
			poolParent = new GameObject("CopySelfLimitedPool");
			poolParent.SetActive(value: false);
			unitCopies = new GameObject[maxAllowedCopies];
			Transform originalUnitTransform = originalUnit.transform;
			short poolId = GetPoolId();
			for (int i = 0; i < maxAllowedCopies; i++)
			{
				GameObject unitCopy = originalUnit.unitBlueprint.Spawn(copySpawnStartPosition, originalUnitTransform.rotation, originalUnit.Team, 1f, new UnitPoolInfo(i, poolId))[0];
				UnitAPI copyAPI = unitCopy.GetComponentInChildren<UnitAPI>();
				copyAPI.forceSupressFromWinCondition = true;
				unitCopies[i] = unitCopy;
				MeleeWeaponCopySelf[] componentsInChildren = unitCopy.GetComponentsInChildren<MeleeWeaponCopySelf>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].RegisterUnitWithPool(this);
				}
				yield return null;
				copyAPI.SetIsInPool();
				unitCopy.transform.SetParent(poolParent.transform);
			}
		}

		public GameObject GetNextUnitFromPool(Vector3 spawnPosition, Quaternion spawnRotation)
		{
			currentPooledUnitIndex++;
			if (currentPooledUnitIndex >= maxAllowedCopies)
			{
				return null;
			}
			GameObject gameObject = unitCopies[currentPooledUnitIndex];
			Unit component = gameObject.GetComponent<Unit>();
			if (component == null)
			{
				Debug.LogError("Trying to get a GameObject from the pool that isn't a Unit.");
				return null;
			}
			GetNetworkService();
			if (networkService.IsRunning && component.PoolInfo.HasValue && component.PoolInfo.Value.HasNetworkError)
			{
				Debug.LogError("Trying to get a Unit from the pool, but there was a network error that prevents it being used.");
				return null;
			}
			InitializeUnitFromPool(component, spawnPosition, spawnRotation);
			return gameObject;
		}

		public void InitializeUnitFromPool(Unit unit, Vector3 spawnPosition, Quaternion spawnRotation)
		{
			unit.transform.SetParent(null);
			unit.transform.SetPositionAndRotation(spawnPosition, spawnRotation);
			GetNetworkService();
			if (networkService.IsRunning)
			{
				unit.SetEnableSyncTransforms(enable: true);
			}
		}

		private short GetPoolId()
		{
			GetNetworkService();
			if (networkService == null || !networkService.IsRunning)
			{
				return 0;
			}
			if (networkService.IsServer)
			{
				if (!originalUnit.OriginatesFromClient)
				{
					return originalUnit.InstanceId;
				}
				return originalUnit.RemoteInstanceId;
			}
			if (networkService.IsClient)
			{
				if (originalUnit.NetworkId == 0L)
				{
					return originalUnit.InstanceId;
				}
				return originalUnit.RemoteInstanceId;
			}
			return 0;
		}

		private void GetNetworkService()
		{
			if (networkService == null)
			{
				networkService = ServiceLocator.GetService<INetworkService>();
			}
		}
	}
}
