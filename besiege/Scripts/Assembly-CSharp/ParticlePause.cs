using UnityEngine;

public class ParticlePause : MonoBehaviour
{
	public float waitFor = 1f;

	public ParticleSystem system;

	private float time;

	private void Start()
	{
		system.Play();
		system.Pause();
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (time > waitFor)
		{
			system.Play();
			base.enabled = false;
		}
	}
}
