using System;
using System.Collections;
using UnityEngine;

public class ParticleOnTrigger : SimBehaviour
{
	public ParticleSystem particles;

	public bool dontTriggerIfPlaying;

	public bool randomiseParticleSeed;

	private bool canEmit;

	protected override void Start()
	{
		base.Start();
		StartCoroutine(BlockEmit());
	}

	protected IEnumerator BlockEmit()
	{
		canEmit = false;
		yield return new WaitForSeconds(0.2f);
		canEmit = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && canEmit)
		{
			PlayParticles();
		}
	}

	public void PlayParticles()
	{
		if (dontTriggerIfPlaying)
		{
			if (!particles.isPlaying)
			{
				particles.Play();
			}
		}
		else
		{
			if (randomiseParticleSeed)
			{
				particles.randomSeed = (uint)UnityEngine.Random.Range(0f, 4.2949673E+09f);
			}
			particles.Play();
		}
		if (StatMaster.isHosting && base.SimPhysics)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.ParticleOnTrigger);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}
}
