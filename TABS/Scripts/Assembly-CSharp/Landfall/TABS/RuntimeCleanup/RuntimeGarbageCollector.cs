using System;
using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABS.RuntimeCleanup
{
	public class RuntimeGarbageCollector : ServicePrefab
	{
		private GameObject[] m_trackedObjects = new GameObject[100];

		private GameObjectPooling.IPoolable[] m_trackedPooledObjects = new GameObjectPooling.IPoolable[100];

		private int m_objectCount;

		private int m_pooledObjectCount;

		private bool m_doFlush;

		private GameStateListenerSurrogate m_stateListenerSurrogate;

		public void AddGameObject(GameObject go)
		{
			m_objectCount++;
			if (m_objectCount >= m_trackedObjects.Length)
			{
				Array.Resize(ref m_trackedObjects, m_trackedObjects.Length + 100);
			}
			m_trackedObjects[m_objectCount - 1] = go;
		}

		public void AddPoolable(GameObjectPooling.IPoolable poolable)
		{
			m_pooledObjectCount++;
			if (m_pooledObjectCount >= m_trackedPooledObjects.Length)
			{
				Array.Resize(ref m_trackedPooledObjects, m_trackedPooledObjects.Length + 100);
			}
			m_trackedPooledObjects[m_pooledObjectCount - 1] = poolable;
		}

		public void AddGameObjects(GameObject[] gos)
		{
			int num = gos.Length;
			for (int i = 0; i < num; i++)
			{
				AddGameObject(gos[i]);
			}
		}

		private void RemoveElement(ref GameObject[] behaviours, ref int elementCount, GameObject behaviour)
		{
			for (int i = 0; i < elementCount; i++)
			{
				if (behaviours[i] == behaviour)
				{
					behaviours[i] = behaviours[elementCount - 1];
					behaviours[elementCount - 1] = null;
					elementCount--;
					break;
				}
			}
		}

		public void ForceFlushGC()
		{
			m_doFlush = true;
		}

		public override void OnLateUpdate()
		{
			if (m_doFlush)
			{
				Debug.Log("-- Flushing RuntimeGC --");
				InternalFlush();
				InternalFlushPooled();
				m_doFlush = false;
			}
		}

		private void InternalFlush()
		{
			for (int i = 0; i < m_objectCount; i++)
			{
				if (m_trackedObjects[i] != null)
				{
					if (m_trackedObjects[i] == null)
					{
						Debug.LogError("Cannot Garbage Collect, object is already null!");
					}
					else
					{
						UnityEngine.Object.Destroy(m_trackedObjects[i]);
					}
				}
			}
			m_objectCount = 0;
			m_trackedObjects = new GameObject[100];
		}

		private void InternalFlushPooled()
		{
			for (int i = 0; i < m_pooledObjectCount; i++)
			{
				if (m_trackedPooledObjects[i] == null || m_trackedPooledObjects[i].ReleaseSelf == null)
				{
					Debug.LogError("Tracked Pooled Object should not be null!");
				}
				else
				{
					m_trackedPooledObjects[i].ReleaseSelf();
				}
			}
			m_pooledObjectCount = 0;
			m_trackedPooledObjects = new GameObjectPooling.IPoolable[100];
		}

		public override void OnAwake()
		{
		}

		public override void UnRegister()
		{
		}
	}
}
