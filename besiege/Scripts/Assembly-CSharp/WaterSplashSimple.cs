using UnityEngine;

[AddComponentMenu("Effects/WaterSplashSimple")]
public class WaterSplashSimple : MonoBehaviour
{
	public string nameToCheck = "PoisonJug";

	public RandomSoundController sfxController;

	public ParticleSystem particle;

	protected void OnTriggerEnter(Collider other)
	{
		if ((bool)other.attachedRigidbody && other.attachedRigidbody.name == nameToCheck)
		{
			Splash();
		}
	}

	protected void Splash()
	{
		sfxController.Play();
		particle.Play();
	}
}
