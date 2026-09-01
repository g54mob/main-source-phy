using System;
using UnityEngine;

public class ProjectileRotate : MonoBehaviour, GameObjectPooling.IPoolable
{
	public bool self = true;

	public Vector3 rotation;

	private ProjectileStick stick;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		stick = GetComponentInParent<ProjectileStick>();
	}

	private void Update()
	{
		if (!stick || !stick.target)
		{
			base.transform.Rotate(rotation * Time.deltaTime, self ? Space.Self : Space.World);
			return;
		}
		base.transform.localRotation = Quaternion.identity;
		if (!IsManagedByPool)
		{
			UnityEngine.Object.Destroy(this);
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
