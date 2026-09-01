using UnityEngine;

public class SetRigidbodyCenter : MonoBehaviour
{
	public Rigidbody body;

	protected void Awake()
	{
		body.centerOfMass = body.transform.InverseTransformPoint(base.transform.position);
	}
}
