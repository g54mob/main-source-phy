using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExtraRipple : MonoBehaviour
{
	public ParticleSystem particles;

	private void Start()
	{
		RipplePostProcessing.Instance.AddExtraParticles(particles);
	}

	private void OnDestroy()
	{
		RipplePostProcessing.Instance.RemoveExtraParticles(particles);
	}
}
