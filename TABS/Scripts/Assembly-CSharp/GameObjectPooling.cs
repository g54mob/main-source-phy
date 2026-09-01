using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPooling : IService
{
	public interface IPoolable
	{
		bool IsManagedByPool { get; set; }

		Action ReleaseSelf { get; set; }

		void Initialize();

		void Reset();

		void Release();
	}

	public class PooledInstance
	{
		public GameObject m_gameObject;

		public IPoolable[] m_pooledComponents;

		public Coroutine m_delayedInitialize;

		public void RegisterRelease(ObjectPool pool)
		{
			for (int i = 0; i < m_pooledComponents.Length; i++)
			{
				m_pooledComponents[i].ReleaseSelf = delegate
				{
					pool.ReleaseObject(this);
				};
			}
		}

		public void Initialize()
		{
			for (int i = 0; i < m_pooledComponents.Length; i++)
			{
				m_pooledComponents[i].Initialize();
			}
		}

		public void Reset()
		{
			for (int i = 0; i < m_pooledComponents.Length; i++)
			{
				m_pooledComponents[i].Reset();
			}
		}

		public void Release()
		{
			for (int i = 0; i < m_pooledComponents.Length; i++)
			{
				m_pooledComponents[i].Release();
			}
			if (m_delayedInitialize != null)
			{
				_coroutineProxy.StopCoroutine(m_delayedInitialize);
			}
		}
	}

	public class ObjectPool
	{
		private Stack<PooledInstance> m_availableObjects = new Stack<PooledInstance>();

		private List<PooledInstance> m_usedObjects = new List<PooledInstance>();

		private int m_numLeftToSpawn;

		private Coroutine m_spawningCoroutine;

		private GameObject m_prefab;

		private Transform m_objectRoot;

		private int testCounter = 1;

		public ObjectPool(GameObject prefab, int numSpawn)
		{
			GameObject gameObject = new GameObject("pool_" + prefab.name);
			m_objectRoot = gameObject.transform;
			m_objectRoot.gameObject.SetActive(value: false);
			m_prefab = prefab;
			SpawnInstances(numSpawn);
		}

		public void SpawnInstances(int numSpawn)
		{
			m_numLeftToSpawn += numSpawn;
			if (m_spawningCoroutine == null)
			{
				m_spawningCoroutine = _coroutineProxy.StartCoroutine(SpawnInstances());
			}
		}

		private IEnumerator SpawnInstances()
		{
			yield return null;
			while (m_numLeftToSpawn > 0)
			{
				CreateInstance(addToAvailable: true);
				m_numLeftToSpawn--;
				yield return null;
			}
			m_spawningCoroutine = null;
		}

		private PooledInstance CreateInstance(bool addToAvailable)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(m_prefab);
			PooledInstance pooledInstance = new PooledInstance();
			pooledInstance.m_gameObject = gameObject;
			pooledInstance.m_pooledComponents = gameObject.GetComponentsInChildren<IPoolable>();
			for (int i = 0; i < pooledInstance.m_pooledComponents.Length; i++)
			{
				pooledInstance.m_pooledComponents[i].IsManagedByPool = true;
			}
			pooledInstance.RegisterRelease(this);
			if (addToAvailable)
			{
				gameObject.transform.SetParent(m_objectRoot);
				m_availableObjects.Push(pooledInstance);
			}
			return pooledInstance;
		}

		public GameObject GetObject(Vector3 position, Quaternion rotation)
		{
			if (m_availableObjects.Count > 0)
			{
				PooledInstance pooledInstance = m_availableObjects.Pop();
				pooledInstance.m_gameObject.transform.SetParent(null);
				pooledInstance.m_gameObject.transform.position = position;
				pooledInstance.m_gameObject.transform.rotation = rotation;
				pooledInstance.m_gameObject.transform.localScale = m_prefab.transform.localScale;
				pooledInstance.Reset();
				pooledInstance.m_delayedInitialize = _coroutineProxy.StartCoroutine(DelayedInitialize(pooledInstance));
				m_usedObjects.Add(pooledInstance);
				return pooledInstance.m_gameObject;
			}
			if (m_numLeftToSpawn > 0)
			{
				m_numLeftToSpawn--;
			}
			PooledInstance pooledInstance2 = CreateInstance(addToAvailable: false);
			pooledInstance2.m_gameObject.transform.position = position;
			pooledInstance2.m_gameObject.transform.rotation = rotation;
			m_usedObjects.Add(pooledInstance2);
			return pooledInstance2.m_gameObject;
		}

		public bool ReleaseObject(PooledInstance obj)
		{
			bool num = m_usedObjects.Remove(obj);
			if (num)
			{
				obj.Release();
				obj.m_gameObject.transform.SetParent(m_objectRoot);
				m_availableObjects.Push(obj);
			}
			return num;
		}

		public void ReleaseAll()
		{
			for (int i = 0; i < m_usedObjects.Count; i++)
			{
				PooledInstance pooledInstance = m_usedObjects[i];
				if (pooledInstance != null)
				{
					pooledInstance.Release();
					if (pooledInstance.m_gameObject == null || pooledInstance.m_gameObject.transform == null)
					{
						Debug.LogError("Pooled object gameobject is null");
						continue;
					}
					pooledInstance.m_gameObject.transform.SetParent(m_objectRoot);
					m_availableObjects.Push(pooledInstance);
				}
			}
			m_usedObjects.Clear();
		}

		private IEnumerator DelayedInitialize(PooledInstance obj)
		{
			yield return new WaitForEndOfFrame();
			obj.Initialize();
			obj.m_delayedInitialize = null;
		}

		public void ClearAvailable(int numToClear)
		{
			m_numLeftToSpawn -= numToClear;
			numToClear = 0;
			if (m_numLeftToSpawn < 0)
			{
				numToClear -= m_numLeftToSpawn;
				m_numLeftToSpawn = 0;
			}
			int num = 0;
			while (m_availableObjects.Count > 0 && num < numToClear)
			{
				UnityEngine.Object.Destroy(m_availableObjects.Pop().m_gameObject);
				num++;
			}
			if (m_spawningCoroutine != null)
			{
				_coroutineProxy.StopCoroutine(m_spawningCoroutine);
			}
			m_spawningCoroutine = null;
		}

		public void ClearPool()
		{
			ReleaseAll();
			if (m_objectRoot != null)
			{
				UnityEngine.Object.Destroy(m_objectRoot.gameObject);
			}
			m_availableObjects.Clear();
			for (int i = 0; i < m_usedObjects.Count; i++)
			{
				if (m_usedObjects[i].m_gameObject != null)
				{
					UnityEngine.Object.Destroy(m_usedObjects[i].m_gameObject);
				}
			}
			m_usedObjects.Clear();
			if (m_spawningCoroutine != null)
			{
				_coroutineProxy.StopCoroutine(m_spawningCoroutine);
			}
			m_spawningCoroutine = null;
		}
	}

	private static CoroutineProxy _coroutineProxy;

	private Dictionary<GameObject, ObjectPool> m_pools = new Dictionary<GameObject, ObjectPool>();

	public ObjectPool GetPool(GameObject prefab, int numSpawn = 0)
	{
		if (m_pools.ContainsKey(prefab))
		{
			ObjectPool objectPool = m_pools[prefab];
			objectPool.SpawnInstances(numSpawn);
			return objectPool;
		}
		ObjectPool objectPool2 = new ObjectPool(prefab, numSpawn);
		m_pools.Add(prefab, objectPool2);
		return objectPool2;
	}

	public void ClearPools()
	{
		foreach (KeyValuePair<GameObject, ObjectPool> pool in m_pools)
		{
			pool.Value.ClearPool();
		}
		m_pools.Clear();
	}

	public void OnRegister()
	{
		_coroutineProxy = new GameObject("CoroutineProxy").AddComponent<CoroutineProxy>();
		UnityEngine.Object.DontDestroyOnLoad(_coroutineProxy.gameObject);
	}

	public void OnAwake()
	{
	}

	public void OnStart()
	{
	}

	public void OnUpdate()
	{
	}

	public void OnFixedUpdate()
	{
	}

	public void OnLateUpdate()
	{
	}

	public void UnRegister()
	{
	}
}
