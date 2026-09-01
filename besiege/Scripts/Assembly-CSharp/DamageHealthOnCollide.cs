using UnityEngine;

public class DamageHealthOnCollide : SimBehaviour
{
	public float blockDamageAmount = 1f;

	private Rigidbody attachedRigidbody;

	private void OnCollisionEnter(Collision other)
	{
		if (base.isSimulating && (bool)other.collider.attachedRigidbody)
		{
			attachedRigidbody = other.collider.attachedRigidbody;
			BlockHealthBar component = attachedRigidbody.GetComponent<BlockHealthBar>();
			if (component != null)
			{
				component.DamageBlock(blockDamageAmount);
			}
		}
	}
}
