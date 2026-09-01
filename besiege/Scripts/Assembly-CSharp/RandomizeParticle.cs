using UnityEngine;

public class RandomizeParticle : MonoBehaviour
{
	public ParticleSystem[] particles;

	private void Awake()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			bool isPlaying = particles[i].isPlaying;
			particles[i].Stop();
			particles[i].randomSeed = (uint)Random.Range(0, 9999999);
			if (isPlaying)
			{
				particles[i].Play();
			}
		}
	}
}
