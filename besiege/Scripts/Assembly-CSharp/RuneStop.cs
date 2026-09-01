using UnityEngine;

public class RuneStop : MonoBehaviour
{
	private ParticleSystem particle;

	public float lifespan = 3.5f;

	private void Start()
	{
		particle = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (particle.time >= lifespan)
		{
			particle.Pause();
		}
	}
}
