using System;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Water/VFX/AlignParticleRotation")]
public class AlignParticleRotation : MonoBehaviour
{
	[HideInInspector]
	public ParticleSystem particle;

	private void Awake()
	{
		particle = base.gameObject.GetComponent<ParticleSystem>();
	}

	private void LateUpdate()
	{
		if (Application.isPlaying || particle != null)
		{
			particle.startRotation3D = base.transform.eulerAngles * ((float)Math.PI / 180f);
		}
	}
}
