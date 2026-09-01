using System.Collections.Generic;
using UnityEngine;

public class CastleWallBreak : BreakOnForce
{
	public Ray ray;

	public RaycastHit hit;

	public int groundedCounter;

	public float range = 2.3f;

	public Transform brokenPrefab;

	public float childForceMultiplier = 200f;

	public bool shouldRaycast = true;

	public bool hasExploded;

	public List<Transform> otherObjsSendBreakMssg = new List<Transform>();

	protected override void Awake()
	{
		base.Awake();
		BreakInto = brokenPrefab;
		explosiveProperty = ExplosiveProperty.DestroyBrick;
	}

	private void RayCheck()
	{
		if (!Physics.Raycast(base.transform.position, Vector3.up, out hit, range))
		{
			return;
		}
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			CastleWallBreak component = attachedRigidbody.GetComponent<CastleWallBreak>();
			if (component != null)
			{
				component.BreakFull();
			}
		}
	}

	protected override void OnCollisionEnter(Collision collision)
	{
	}

	public override Transform BreakObj()
	{
		if (shouldRaycast)
		{
			RayCheck();
		}
		if (base.SimPhysics && myBody != null)
		{
			myBody.isKinematic = false;
			myBody.WakeUp();
		}
		BreakOthers();
		return base.BreakObj();
	}

	private void BreakOthers()
	{
		for (int i = 0; i < otherObjsSendBreakMssg.Count; i++)
		{
			otherObjsSendBreakMssg[i].SendMessage("BreakFull");
		}
	}

	private void BreakFull()
	{
		BreakObj();
	}
}
