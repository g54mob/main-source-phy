using UnityEngine;

public class PlayParticlesOnMoving : SimBehaviour
{
	public ParticleSystem particle;

	public float stopParticleDelay = 0.2f;

	public float minMovement = 10f;

	private Vector3 prevPos;

	private float timer;

	private float minParticles = 50f;

	private float dist;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			if (!particle)
			{
				base.enabled = false;
			}
			if (!particle.isPlaying)
			{
				particle.randomSeed = (uint)Random.Range(0f, 9999999f);
			}
			prevPos = base.transform.position;
		}
	}

	private void Update()
	{
		if (!base.isSimulating)
		{
			return;
		}
		dist = (prevPos - base.transform.position).sqrMagnitude;
		if (dist > minMovement)
		{
			if (particle.isStopped || !particle.isPlaying || ((float)particle.particleCount < minParticles && particle.isPlaying))
			{
				particle.Play();
				timer = 0f;
			}
			prevPos = base.transform.position;
		}
		else if (particle.isPlaying && timer >= stopParticleDelay)
		{
			particle.Stop();
			timer = 0f;
		}
		else
		{
			timer += Time.deltaTime;
		}
	}
}
