using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : MonoBehaviour
{
	private Stack<T> m_availableObjects = new Stack<T>();

	private List<T> m_usedObjects = new List<T>();

	private GameObject m_prefab;

	private Transform m_root;

	private bool m_deactivateOnRelease;

	private int testCounter = 1;

	public List<T> UsedObjects => m_usedObjects;

	public GameObjectPool(GameObject prefab, bool deactivateOnRelease, Transform root = null)
	{
		m_prefab = prefab;
		m_root = root;
		m_deactivateOnRelease = deactivateOnRelease;
	}

	public T GetObject()
	{
		if (m_availableObjects.Count > 0)
		{
			T val = m_availableObjects.Pop();
			m_usedObjects.Add(val);
			if (m_deactivateOnRelease)
			{
				val.gameObject.SetActive(value: true);
			}
			return val;
		}
		T component = Object.Instantiate(m_prefab, m_root).GetComponent<T>();
		if (component != null)
		{
			m_usedObjects.Add(component);
			if (m_deactivateOnRelease)
			{
				component.gameObject.SetActive(value: true);
			}
			return component;
		}
		return null;
	}

	public void ReleaseAll()
	{
		for (int i = 0; i < m_usedObjects.Count; i++)
		{
			T val = m_usedObjects[i];
			m_availableObjects.Push(val);
			if (m_deactivateOnRelease)
			{
				val.gameObject.SetActive(value: false);
			}
		}
		m_usedObjects.Clear();
	}

	public bool ReleaseObject(T obj)
	{
		bool num = m_usedObjects.Remove(obj);
		if (num)
		{
			m_availableObjects.Push(obj);
			if (m_deactivateOnRelease)
			{
				obj.gameObject.SetActive(value: false);
			}
		}
		return num;
	}

	public void ClearPool()
	{
		while (m_availableObjects.Count > 0)
		{
			Object.Destroy(m_availableObjects.Pop().gameObject);
		}
		for (int i = 0; i < m_usedObjects.Count; i++)
		{
			Object.Destroy(m_usedObjects[i].gameObject);
		}
		m_usedObjects.Clear();
	}
}
