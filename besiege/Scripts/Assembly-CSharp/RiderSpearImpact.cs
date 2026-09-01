using UnityEngine;

public class RiderSpearImpact : MonoBehaviour
{
	public float force;

	private bool hasHit;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponentInParent<Machine>() != null && !hasHit)
		{
			Vector3 vector = other.transform.position - base.transform.position;
			other.attachedRigidbody.AddForce(vector * force * 0.1f, ForceMode.Impulse);
			hasHit = true;
		}
	}
}
