using UnityEngine;

public class BasicBreakOnClick : ClickBehaviour
{
	public Transform[] visObjects;

	public ParticleSystem[] particles;

	public override void OnClicked()
	{
		Break();
	}

	private void Break()
	{
		PlayAllParticles();
		for (int i = 0; i < visObjects.Length; i++)
		{
			visObjects[i].gameObject.SetActive(false);
		}
	}

	private void PlayAllParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}
}
