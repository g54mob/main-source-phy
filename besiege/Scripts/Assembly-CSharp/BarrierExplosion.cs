using UnityEngine;

public class BarrierExplosion : MonoBehaviour
{
	public Rigidbody[] rigidbodies;

	public float explosionForce;

	public float explosionRadius;

	private void Start()
	{
		Rigidbody[] array = rigidbodies;
		foreach (Rigidbody rigidbody in array)
		{
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.AddExplosionForce(explosionForce, base.transform.position, explosionRadius);
		}
	}
}
