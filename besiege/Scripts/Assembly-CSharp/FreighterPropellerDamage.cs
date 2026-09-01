using UnityEngine;

[AddComponentMenu("Destruction/Freighter Propeller Damage")]
public class FreighterPropellerDamage : MonoBehaviour
{
	public float knockBackForce = 1000f;

	public float torqueForce = 1000f;

	private void OnCollisionEnter(Collision other)
	{
		if ((bool)other.collider.attachedRigidbody)
		{
			other.collider.attachedRigidbody.AddForce((base.transform.position - other.collider.transform.position).normalized * knockBackForce);
			other.collider.attachedRigidbody.AddTorque(torqueForce * Vector3.up);
		}
	}
}
