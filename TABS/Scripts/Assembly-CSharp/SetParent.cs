using System;
using Landfall.TABS;
using UnityEngine;

public class SetParent : MonoBehaviour, GameObjectPooling.IPoolable
{
	public bool OnAwake = true;

	public Transform targetParent;

	public bool setSpawnerAsParentOnStart;

	public bool setSpawnerWeaponAsParentOnStart;

	[HideInInspector]
	public Transform parentBefore;

	private TeamHolder th;

	private bool wasReparented;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public bool DelayReparenting { get; private set; }

	private void Awake()
	{
		parentBefore = base.transform.parent;
		if (OnAwake && !DelayReparenting)
		{
			Doit();
		}
	}

	private void Start()
	{
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void OnDestroy()
	{
	}

	public void Doit()
	{
		if (targetParent == null)
		{
			Unit componentInParent = GetComponentInParent<Unit>();
			if (componentInParent != null)
			{
				Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
				if (componentsInChildren != null && componentsInChildren.Length != 0)
				{
					componentInParent.AddRenderersToShowHide(componentsInChildren, componentInParent.IsSpawnedInBlindPlacement);
				}
			}
		}
		base.transform.parent = targetParent;
		wasReparented = true;
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
	}

	public void Release()
	{
	}

	private void InitializeOnSpawn()
	{
		if (!setSpawnerAsParentOnStart && !setSpawnerWeaponAsParentOnStart)
		{
			return;
		}
		th = GetComponent<TeamHolder>();
		if ((bool)th)
		{
			if (setSpawnerAsParentOnStart)
			{
				targetParent = th.spawner.transform;
			}
			if (setSpawnerWeaponAsParentOnStart)
			{
				targetParent = th.spawnerWeapon.transform;
			}
			Doit();
		}
	}
}
