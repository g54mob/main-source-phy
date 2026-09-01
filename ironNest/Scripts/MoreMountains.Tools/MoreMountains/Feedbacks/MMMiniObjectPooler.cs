using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMMiniObjectPooler : MonoBehaviour
	{
		public GameObject GameObjectToPool;

		public int PoolSize;

		public bool PoolCanExpand;

		public bool MutualizeWaitingPools;

		public bool NestWaitingPool;

		protected GameObject _waitingPool;

		protected MMMiniObjectPool _objectPool;

		protected const int _initialPoolsListCapacity = 5;

		private static List<MMMiniObjectPool> _pools;

		public static void AddPool(MMMiniObjectPool pool)
		{
		}

		public static void RemovePool(MMMiniObjectPool pool)
		{
		}

		protected virtual void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public virtual MMMiniObjectPool ExistingPool(string poolName)
		{
			return null;
		}

		protected virtual void CreateWaitingPool()
		{
		}

		public static string DetermineObjectPoolName(GameObject gameObjectToPool)
		{
			return null;
		}

		public virtual void FillObjectPool()
		{
		}

		public virtual GameObject GetPooledGameObject()
		{
			return null;
		}

		protected virtual GameObject AddOneObjectToThePool()
		{
			return null;
		}

		public virtual void DestroyObjectPool()
		{
		}
	}
}
