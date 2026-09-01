using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
	public float damagePerSecond = 50f;

	public float forcePerSecond;

	public Vector3 direction;

	private void OnTriggerStay(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if ((bool)attachedRigidbody && !(attachedRigidbody.transform.root == base.transform.root))
		{
			DataHandler componentInParent = attachedRigidbody.GetComponentInParent<DataHandler>();
			if ((bool)componentInParent && damagePerSecond != 0f)
			{
				componentInParent.healthHandler.TakeDamage(damagePerSecond * Time.fixedDeltaTime, Vector3.zero, null);
			}
			if (forcePerSecond != 0f)
			{
				attachedRigidbody.AddForce(base.transform.TransformDirection(direction) * forcePerSecond, ForceMode.Force);
			}
		}
	}
}
