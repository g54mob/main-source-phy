using UnityEngine;

public class ParticleLight : MonoBehaviour
{
	public ParticleSystem particleSystem;

	public Light light;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
		light = GetComponent<Light>();
	}

	private void FixedUpdate()
	{
		if ((double)particleSystem.time >= 1.5)
		{
			light.enabled = true;
		}
	}
}
