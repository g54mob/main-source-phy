using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class RaycastTrail : MonoBehaviour, GameObjectPooling.IPoolable
{
	public LayerMask mask;

	[HideInInspector]
	public int ignoredFrames;

	public bool useSphereCastOnUnits;

	public float radius;

	public bool ignoreArmor;

	private Vector3 deltaPos;

	private Vector3 lastPos;

	private RaycastHit[] hits;

	private ProjectileHit projectileHit;

	private NativeArray<RaycastCommand> raycastCommands;

	private NativeArray<RaycastHit> raycastHits;

	private JobHandle jobHandle;

	private bool nativeArraysDisposed = true;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		projectileHit = GetComponent<ProjectileHit>();
		if (!IsManagedByPool)
		{
			InitializeOnSpawn();
		}
	}

	private void Update()
	{
		Check();
	}

	private void OnDestroy()
	{
		Release();
	}

	public void Initialize()
	{
		InitializeOnSpawn();
	}

	public void Reset()
	{
		lastPos = base.transform.position;
	}

	public void Release()
	{
		if (!nativeArraysDisposed)
		{
			jobHandle.Complete();
			raycastCommands.Dispose();
			raycastHits.Dispose();
			nativeArraysDisposed = true;
		}
	}

	private void InitializeOnSpawn()
	{
		raycastCommands = new NativeArray<RaycastCommand>(1, Allocator.Persistent);
		raycastHits = new NativeArray<RaycastHit>(1, Allocator.Persistent);
		nativeArraysDisposed = false;
		lastPos = base.transform.position;
		Check();
	}

	private void Check()
	{
		if (nativeArraysDisposed)
		{
			return;
		}
		if (ignoredFrames > 0)
		{
			ignoredFrames--;
			lastPos = base.transform.position;
			return;
		}
		jobHandle.Complete();
		RaycastHit sentHit = raycastHits[0];
		bool flag = sentHit.collider != null;
		if (useSphereCastOnUnits)
		{
			hits = Physics.SphereCastAll(lastPos, radius, deltaPos, Vector3.Distance(base.transform.position, lastPos), mask);
			if (hits.Length != 0)
			{
				for (int i = 0; i < hits.Length; i++)
				{
					projectileHit.Hit(hits[i]);
				}
			}
		}
		if (flag)
		{
			projectileHit.Hit(sentHit);
		}
		deltaPos = base.transform.position - lastPos;
		if (!nativeArraysDisposed)
		{
			raycastCommands[0] = new RaycastCommand(lastPos, deltaPos, Vector3.Distance(base.transform.position, lastPos), mask);
			jobHandle = RaycastCommand.ScheduleBatch(raycastCommands, raycastHits, 1);
		}
		lastPos = base.transform.position;
	}
}
