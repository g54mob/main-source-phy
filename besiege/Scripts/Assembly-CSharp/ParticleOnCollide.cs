using System;
using UnityEngine;

public class ParticleOnCollide : SimBehaviour, TrophyIncrement
{
	public ParticleSystem particles;

	public ParticleSystem extraParticles;

	public float minImpact = 200f;

	public AudioClip[] randomSounds;

	public bool dontTriggerIfPlaying;

	private AudioSource audioSource;

	public bool checkIfHitByProjectile;

	private int childCount;

	public Action<MonoBehaviour> trophyIncrease { get; set; }

	protected override void Start()
	{
		base.Start();
		audioSource = GetComponent<AudioSource>();
		childCount = base.transform.childCount;
	}

	public void PlayParticles()
	{
		if (dontTriggerIfPlaying)
		{
			if (particles != null && !particles.isPlaying)
			{
				particles.randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				particles.Play();
			}
			if (extraParticles != null && !extraParticles.isPlaying)
			{
				extraParticles.randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				extraParticles.Play();
			}
		}
		else
		{
			if (particles != null)
			{
				particles.randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				particles.Play();
			}
			if (extraParticles != null)
			{
				extraParticles.randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				extraParticles.Play();
			}
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!base.isSimulating || other.relativeVelocity.sqrMagnitude <= minImpact)
		{
			return;
		}
		PlayEffect();
		if (StatMaster.isHosting && base.SimPhysics && !StatMaster.IsLevelEditorOnly)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.ParticleOnCollide);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	public void PlayEffect()
	{
		PlayParticles();
		if (audioSource != null && !audioSource.isPlaying)
		{
			if (randomSounds.Length > 0)
			{
				audioSource.clip = randomSounds[UnityEngine.Random.Range(0, randomSounds.Length)];
			}
			audioSource.Play();
		}
	}

	private void LateUpdate()
	{
		if (checkIfHitByProjectile && !StatMaster.isMP && base.transform.childCount > childCount)
		{
			PlayEffect();
			childCount = int.MaxValue;
			if (trophyIncrease != null)
			{
				trophyIncrease(this);
			}
		}
	}
}
