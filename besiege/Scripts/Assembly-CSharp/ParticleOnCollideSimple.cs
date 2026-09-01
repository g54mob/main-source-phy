using UnityEngine;

public class ParticleOnCollideSimple : MonoBehaviour
{
	public ParticleSystem particles;

	private void OnCollisionEnter()
	{
		particles.Play();
	}
}
