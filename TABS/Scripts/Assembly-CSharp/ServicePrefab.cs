using UnityEngine;

public abstract class ServicePrefab : MonoBehaviour, IService
{
	public ServicePrefab GetService()
	{
		return Object.Instantiate(this);
	}

	public virtual void OnRegister()
	{
	}

	public virtual void OnAwake()
	{
	}

	public virtual void OnStart()
	{
	}

	public virtual void OnUpdate()
	{
	}

	public virtual void OnFixedUpdate()
	{
	}

	public virtual void OnLateUpdate()
	{
	}

	public virtual void UnRegister()
	{
	}
}
