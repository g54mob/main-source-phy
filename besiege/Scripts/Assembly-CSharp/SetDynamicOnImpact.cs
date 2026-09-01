using UnityEngine;

public class SetDynamicOnImpact : SimBehaviour
{
	public Rigidbody body;

	public bool useSleep = true;

	public bool alsoOnFire = true;

	[SerializeField]
	private float breakVelocity = 30f;

	protected float breakForceSqr;

	private Vector3 startPos;

	private Quaternion startRot;

	protected override void Awake()
	{
		breakForceSqr = breakVelocity * breakVelocity;
		startPos = base.transform.position;
		startRot = base.transform.rotation;
		base.Awake();
	}

	protected void LateUpdate()
	{
		if (base.isSimulating)
		{
			if ((base.transform.position - startPos).sqrMagnitude > 0.01f || Quaternion.Angle(base.transform.rotation, startRot) > 5f)
			{
				Release();
			}
			if (useSleep && !body.IsSleeping())
			{
				body.Sleep();
			}
		}
	}

	protected void OnCollisionEnter(Collision col)
	{
		if (base.isSimulating && (useSleep || col.relativeVelocity.sqrMagnitude > breakForceSqr))
		{
			Release();
		}
	}

	public void FireKill()
	{
		if (alsoOnFire)
		{
			Release();
		}
	}

	public void Release()
	{
		body.useGravity = true;
		body.isKinematic = false;
		useSleep = false;
		Object.Destroy(this);
	}
}
