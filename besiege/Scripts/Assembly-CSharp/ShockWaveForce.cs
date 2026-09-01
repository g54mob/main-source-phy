using UnityEngine;

public class ShockWaveForce : MonoBehaviour
{
	public float radius = 10f;

	public float force = 5f;

	public ParticleSystem postShockFX;

	public Rigidbody rigidbody;

	private void ShockWave()
	{
		Vector3 position = base.transform.position;
		Collider[] array = Physics.OverlapSphere(base.transform.position, radius);
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			rigidbody = collider.GetComponent<Rigidbody>();
			if (rigidbody != null)
			{
				rigidbody.AddExplosionForce(force, position, radius);
			}
		}
	}

	private void Update()
	{
		if (postShockFX.isPlaying)
		{
			ShockWave();
		}
	}
}
