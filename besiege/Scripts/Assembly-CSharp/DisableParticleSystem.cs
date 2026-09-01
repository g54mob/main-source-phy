using UnityEngine;

public class DisableParticleSystem : MonoBehaviour
{
	public ParticleSystem particleSystem;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (particleSystem.time >= particleSystem.duration)
		{
			base.gameObject.SetActive(false);
		}
	}
}
