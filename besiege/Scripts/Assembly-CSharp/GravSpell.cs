using System.Collections.Generic;
using UnityEngine;

public class GravSpell : MonoBehaviour
{
	public Vector3 windAmount;

	public List<Rigidbody> rigidbodys = new List<Rigidbody>();

	private void OnTriggerEnter(Collider other)
	{
		other.attachedRigidbody.WakeUp();
		if ((bool)other.GetComponent<Collider>().attachedRigidbody && !rigidbodys.Contains(other.GetComponent<Collider>().attachedRigidbody))
		{
			rigidbodys.Add(other.GetComponent<Collider>().attachedRigidbody);
		}
		for (int i = 0; i < rigidbodys.Count; i++)
		{
			rigidbodys[i].AddTorque(new Vector3(Random.Range(-6f, 6f), 0f, Random.Range(-6f, 6f)) * rigidbodys[i].mass);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((bool)other.GetComponent<Collider>().attachedRigidbody && rigidbodys.Contains(other.GetComponent<Collider>().attachedRigidbody))
		{
			rigidbodys.Remove(other.GetComponent<Collider>().attachedRigidbody);
		}
	}

	private void FixedUpdate()
	{
		for (int i = 0; i < rigidbodys.Count; i++)
		{
			rigidbodys[i].AddForce(windAmount * rigidbodys[i].mass);
		}
	}
}
