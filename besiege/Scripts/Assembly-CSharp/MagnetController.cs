using System.Collections.Generic;
using UnityEngine;

public class MagnetController : SimBehaviour
{
	public float magnetForce = 10f;

	public Transform myTransform;

	public float myRadius = 10f;

	public List<Rigidbody> objsToAttract = new List<Rigidbody>();

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && (bool)other.attachedRigidbody)
		{
			objsToAttract.Add(other.attachedRigidbody);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && (bool)other.attachedRigidbody)
		{
			objsToAttract.Remove(other.attachedRigidbody);
		}
	}

	private void FixedUpdate()
	{
		if (base.SimPhysics && base.isSimulating)
		{
			for (int i = 0; i < objsToAttract.Count; i++)
			{
				objsToAttract[i].AddForce((myTransform.position - objsToAttract[i].transform.position).normalized * (myRadius - (myTransform.position - objsToAttract[i].transform.position).sqrMagnitude) * magnetForce);
			}
		}
	}
}
