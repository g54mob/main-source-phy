using UnityEngine;

public class CastleFloorBreak : BreakOnForce
{
	public Ray ray;

	public RaycastHit hit;

	public Rigidbody myRigidbody;

	public int groundedCounter;

	public float range = 2.3f;

	public Transform brokenPrefab;

	public float childForceMultiplier = 200f;

	public bool hasExploded;

	protected override void Awake()
	{
		BreakInto = brokenPrefab;
	}

	protected override void Start()
	{
		Init();
		if (!StatMaster.levelSimulating)
		{
			RayCheck();
		}
	}

	private void RayCheck()
	{
		RayCheck(base.transform.up);
		RayCheck(-base.transform.up);
		RayCheck(base.transform.right);
		RayCheck(-base.transform.right);
	}

	private void RayCheck(Vector3 direction)
	{
		if (!Physics.Raycast(base.transform.position, direction, out hit, range))
		{
			return;
		}
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			CastleWallBreak component = attachedRigidbody.GetComponent<CastleWallBreak>();
			if (component != null)
			{
				component.otherObjsSendBreakMssg.Add(base.transform);
			}
		}
	}

	protected override void OnCollisionEnter(Collision collision)
	{
	}

	protected override void SetParent(Transform breakObj)
	{
		breakObj.parent = base.transform.parent;
		breakObj.localScale = base.transform.localScale;
	}

	public override Transform BreakObj()
	{
		if (myRigidbody != null)
		{
			myRigidbody.isKinematic = false;
			myRigidbody.WakeUp();
		}
		return base.BreakObj();
	}

	public void BreakFull()
	{
		BreakObj();
	}
}
