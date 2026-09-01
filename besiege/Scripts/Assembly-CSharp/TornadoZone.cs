using System.Collections.Generic;
using UnityEngine;

public class TornadoZone : SimBehaviour
{
	public Vector3 windAmount;

	public Vector3 torqueAmount;

	private List<Rigidbody> rigidbodies = new List<Rigidbody>();

	protected override void Start()
	{
		base.Start();
		windAmount *= 2f;
		torqueAmount *= 2f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && (bool)other.attachedRigidbody)
		{
			other.attachedRigidbody.WakeUp();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (base.SimPhysics && base.isSimulating)
		{
			Rigidbody attachedRigidbody = other.attachedRigidbody;
			if (attachedRigidbody != null && !rigidbodies.Contains(attachedRigidbody))
			{
				attachedRigidbody.AddForce(windAmount);
				attachedRigidbody.AddTorque(torqueAmount);
				rigidbodies.Add(attachedRigidbody);
			}
		}
	}

	private void FixedUpdate()
	{
		if (base.SimPhysics && base.isSimulating)
		{
			rigidbodies.Clear();
		}
	}
}
