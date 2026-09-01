using UnityEngine;

public class Icicles : MonoBehaviour
{
	public ParticleSystem[] particles;

	public Renderer render;

	public GameObject breakEffect;

	private void Start()
	{
		if (!render)
		{
			render = GetComponentInChildren<Renderer>();
		}
	}

	public void Hit()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
		render.enabled = false;
		base.enabled = false;
	}

	public void FireKill()
	{
		Hit();
		if ((bool)breakEffect)
		{
			Object.Instantiate(breakEffect, base.transform.position, Quaternion.identity);
		}
	}
}
