using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/MyWindZone")]
public class MyWindZone : MonoBehaviour
{
	public Vector3 windAmount;

	public bool localSpace;

	public bool addRandomTorque;

	public float randomTorqueAmount = 100f;

	public HashSet<Rigidbody> bodies = new HashSet<Rigidbody>();

	private void OnTriggerStay(Collider other)
	{
		if (StatMaster.levelSimulating && (bool)other.attachedRigidbody && !bodies.Contains(other.attachedRigidbody))
		{
			if (localSpace)
			{
				other.attachedRigidbody.AddForce(base.transform.TransformDirection(windAmount));
			}
			else
			{
				other.attachedRigidbody.AddForce(windAmount);
			}
			if (addRandomTorque)
			{
				other.attachedRigidbody.AddTorque(Random.insideUnitSphere * randomTorqueAmount);
			}
			bodies.Add(other.attachedRigidbody);
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			bodies.Clear();
		}
	}
}
