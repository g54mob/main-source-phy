using System;
using UnityEngine;
using UnityEngine.Events;

public class PooledProjectile : MonoBehaviour, GameObjectPooling.IPoolable
{
	public UnityEvent resetEvent;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public event Action<PooledProjectile> SpawnedFromPool;

	public event Action<PooledProjectile> ReturnedToPool;

	public void Initialize()
	{
	}

	public void Reset()
	{
		if (resetEvent != null)
		{
			resetEvent.Invoke();
		}
		this.SpawnedFromPool?.Invoke(this);
	}

	public void Release()
	{
		this.ReturnedToPool?.Invoke(this);
	}
}
