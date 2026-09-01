using System;
using Landfall.TABS;
using UnityEngine;

public class DestroyObjects : MonoBehaviour, GameObjectPooling.IPoolable
{
	public GameObject[] objects;

	private Unit m_unit;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		m_unit = GetComponent<Unit>();
	}

	public void DoIt()
	{
		if (m_unit != null)
		{
			m_unit.data.Dead = true;
		}
		if (!IsManagedByPool)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				UnityEngine.Object.Destroy(objects[i]);
			}
		}
	}

	public void Initialize()
	{
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}
}
