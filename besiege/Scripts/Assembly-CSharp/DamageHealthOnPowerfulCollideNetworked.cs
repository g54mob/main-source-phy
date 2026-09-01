using UnityEngine;

public class DamageHealthOnPowerfulCollideNetworked : SimBehaviour
{
	public float blockDamageAmount = 1f;

	public float velocitySqrThreshold = 650f;

	private Rigidbody attachedRigidbody;

	private Rigidbody myRigidbody;

	private float lastSqrVelocity;

	protected override void Start()
	{
		base.Start();
		if (base.SimPhysics && myRigidbody == null)
		{
			myRigidbody = GetComponent<Rigidbody>();
		}
	}

	private void FixedUpdate()
	{
		if (base.SimPhysics && myRigidbody != null)
		{
			lastSqrVelocity = myRigidbody.velocity.sqrMagnitude;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (base.SimPhysics && !(other.collider == null) && !(other.collider.attachedRigidbody == null) && !(lastSqrVelocity < velocitySqrThreshold / 2f) && base.isSimulating)
		{
			attachedRigidbody = other.collider.attachedRigidbody;
			BlockHealthBar component = attachedRigidbody.GetComponent<BlockHealthBar>();
			if (component != null)
			{
				component.DamageBlock((lastSqrVelocity < velocitySqrThreshold) ? 1 : 2);
			}
		}
	}
}
