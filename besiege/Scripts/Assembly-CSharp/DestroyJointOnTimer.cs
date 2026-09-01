using System.Collections;
using UnityEngine;

public class DestroyJointOnTimer : SimBehaviour
{
	public float maxRandomTimer = 3f;

	public ParticleSystem[] particles;

	protected override void Start()
	{
		base.Start();
		StartCoroutine(StartDestroy());
	}

	private IEnumerator StartDestroy()
	{
		yield return new WaitForSeconds(Random.Range(0f, maxRandomTimer));
		if (base.isSimulating)
		{
			Object.Destroy(base.gameObject.GetComponent<HingeJoint>());
			PlayParticles();
		}
	}

	private void PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}
}
