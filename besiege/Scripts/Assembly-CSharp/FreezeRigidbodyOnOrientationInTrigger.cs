using System.Collections.Generic;
using UnityEngine;

public class FreezeRigidbodyOnOrientationInTrigger : MonoBehaviour
{
	public List<Rigidbody> targets = new List<Rigidbody>();

	private List<Rigidbody> frozen = new List<Rigidbody>();

	[Range(0f, 1f)]
	public float threshold = 0.99f;

	public Vector3 rigidbodyAxis = Vector3.up;

	public Vector3 direction = Vector3.forward;

	public bool globalDirection;

	public void OnTriggerStay(Collider col)
	{
		Rigidbody attachedRigidbody = col.attachedRigidbody;
		if ((bool)attachedRigidbody && targets.Contains(attachedRigidbody))
		{
			Vector3 lhs = ((!globalDirection) ? base.transform.TransformDirection(direction) : direction);
			Vector3 rhs = attachedRigidbody.transform.TransformDirection(rigidbodyAxis);
			float num = Mathf.Abs(Vector3.Dot(lhs, rhs));
			if (num > threshold)
			{
				targets.Remove(attachedRigidbody);
				frozen.Add(attachedRigidbody);
				Freeze(attachedRigidbody);
			}
		}
	}

	public void Freeze(Rigidbody r)
	{
		r.Sleep();
		r.isKinematic = true;
		WinCondition.currentObjsCompleted++;
	}
}
