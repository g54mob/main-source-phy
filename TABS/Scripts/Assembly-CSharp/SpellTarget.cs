using System;
using UnityEngine;

public class SpellTarget : TargetableEffect, GameObjectPooling.IPoolable
{
	[HideInInspector]
	public float distance = 5f;

	public bool setTargetOnStart;

	public Rigidbody rig;

	private TeamHolder teamHolder;

	private DataHandler spawnerData;

	private Vector3 targetPos;

	private Vector3 spawnPos;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	public override void DoEffect(Transform startPoint, Transform endPoint)
	{
	}

	public override void DoEffect(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig)
	{
		distance = Vector3.Distance(startPoint, endPoint);
		rig = targetRig;
	}

	public void GetTarget()
	{
		teamHolder = GetComponent<TeamHolder>();
		spawnerData = teamHolder.spawner.GetComponentInChildren<DataHandler>();
		targetPos = spawnerData.targetMainRig.position;
		spawnPos = base.transform.position;
		DoEffect(spawnPos, targetPos, spawnerData.targetMainRig);
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
		rig = null;
	}

	private void InitializeOnSpawn()
	{
		if (setTargetOnStart)
		{
			GetTarget();
		}
	}
}
