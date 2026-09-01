using System.Collections;
using UnityEngine;

public class GravityField : MonoBehaviour
{
	public float drag = 0.2f;

	public float force = 2.5f;

	public float upMulti = 1f;

	private float counter;

	private float slowDownTime = 0.3f;

	private void OnTriggerEnter(Collider other)
	{
		Rigidbody componentInParent = other.GetComponentInParent<Rigidbody>();
		if ((bool)componentInParent)
		{
			DataHandler componentInChildren = other.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)componentInChildren)
			{
				componentInChildren.fallTime = 5f;
			}
			componentInParent.useGravity = false;
			StartCoroutine(SlowDown(componentInParent));
			Vector3 vector = new Vector3(Random.Range(0f - force, force), Random.Range(0f - force, force * upMulti), Random.Range(0f - force, force));
			componentInParent.AddForce(vector * 10f, ForceMode.Acceleration);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Rigidbody componentInParent = other.GetComponentInParent<Rigidbody>();
		if ((bool)componentInParent)
		{
			DataHandler componentInChildren = other.transform.root.GetComponentInChildren<DataHandler>();
			if ((bool)componentInChildren)
			{
				componentInChildren.fallTime = 0.5f;
			}
			componentInParent.useGravity = true;
		}
	}

	private IEnumerator SlowDown(Rigidbody rig)
	{
		rig.velocity *= drag;
		yield return null;
	}
}
