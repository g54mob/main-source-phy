using System.Collections.Generic;
using UnityEngine;

public class StopParticlesWhenMoved : MonoBehaviour
{
	public List<ParticleSystem> particles = new List<ParticleSystem>();

	public GameObject objToIgnore;

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.name != objToIgnore.name)
		{
			for (int i = 0; i < particles.Count; i++)
			{
				particles[i].Stop();
			}
			base.enabled = false;
		}
	}
}
