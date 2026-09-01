using UnityEngine;

[AddComponentMenu("Effects/WindParticles")]
public class WindParticles : MonoBehaviour
{
	private WindController windController;

	public ParticleSystem particleSystem;

	public ParticleSystem signal;

	private void Start()
	{
		particleSystem.Play();
		windController = GetComponent<WindController>();
	}

	private void Update()
	{
		bool flag = windController.windPower != new Vector3(0f, 0f, 0f);
		ParticleSystem.EmissionModule emission = signal.emission;
		emission.enabled = flag;
		emission = particleSystem.emission;
		emission.enabled = flag;
	}
}
