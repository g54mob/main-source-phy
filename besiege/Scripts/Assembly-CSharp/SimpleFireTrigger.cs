using UnityEngine;

public class SimpleFireTrigger : MonoBehaviour
{
	public Rigidbody rigidbodyToIgnore;

	public ParticleSystem fireParticles;

	private void OnTriggerEnter(Collider other)
	{
		if (base.enabled && (bool)other.attachedRigidbody && other.attachedRigidbody != rigidbodyToIgnore && (bool)other.attachedRigidbody.GetComponent<FireTag>())
		{
			other.attachedRigidbody.GetComponent<FireTag>().Ignite();
		}
	}

	public void Doused()
	{
		base.enabled = false;
		fireParticles.Stop();
	}
}
