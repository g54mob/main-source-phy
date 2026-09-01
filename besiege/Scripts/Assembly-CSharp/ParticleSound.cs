using UnityEngine;

public class ParticleSound : MonoBehaviour
{
	public AudioSource audio;

	public ParticleSystem particleSystem;

	private void Start()
	{
		audio = GetComponent<AudioSource>();
		particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (particleSystem.time > 0f && !audio.isPlaying)
		{
			audio.Play();
		}
	}
}
