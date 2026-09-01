using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoreMountains.Tools
{
	public abstract class MMObjectPooler : MonoBehaviour
	{
		public static MMObjectPooler Instance;

		public bool MutualizeWaitingPools;

		public bool NestWaitingPool;

		[MMCondition("NestWaitingPool", true)]
		public bool NestUnderThis;

		protected GameObject _waitingPool;

		protected MMObjectPool _objectPool;

		protected const int _initialPoolsListCapacity = 5;

		protected bool _onSceneLoadedRegistered;

		public static List<MMObjectPool> _pools;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		protected static void InitializeStatics()
		{
		}

		public static void AddPool(MMObjectPool pool)
		{
		}

		public static void RemovePool(MMObjectPool pool)
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual bool CreateWaitingPool()
		{
			return false;
		}

		public virtual MMObjectPool ExistingPool(string poolName)
		{
			return null;
		}

		protected virtual void ApplyNesting()
		{
		}

		protected virtual string DetermineObjectPoolName()
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

		public virtual void DestroyObjectPool()
		{
		}

		protected virtual void OnEnable()
		{
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
		}

		private void OnDestroy()
		{
		}
	}
}
