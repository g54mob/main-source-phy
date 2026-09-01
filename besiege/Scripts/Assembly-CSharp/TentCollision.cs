using System.Collections;
using UnityEngine;

public class TentCollision : BreakBase
{
	public float minForce = 100f;

	public SkinnedMeshRenderer mesh;

	public float collapseTime = 1f;

	public GameObject particleContainer;

	public FireController fireController;

	public RandomSoundController sfx;

	public Collider collapsedCollider;

	private Collider[] colliders;

	private ParticleSystem[] particles;

	private float time;

	private bool collapsed;

	public override Vector3 Center()
	{
		if (base.SimPhysics)
		{
			return mesh.bounds.center;
		}
		return base.Center();
	}

	protected override void Start()
	{
		base.Start();
		if (mesh == null)
		{
			mesh = GetComponent<SkinnedMeshRenderer>();
			if (mesh == null)
			{
				base.enabled = false;
			}
		}
		colliders = GetComponentsInChildren<Collider>();
		particles = particleContainer.GetComponentsInChildren<ParticleSystem>();
	}

	private void Update()
	{
		if (base.isSimulating && fireController.onFire && fireController.fireProgress > 0.95f && !collapsed)
		{
			StartCoroutine(Collapse());
		}
	}

	public void OnCollisionEnter(Collision other)
	{
		if (base.isSimulating && !(mesh.GetBlendShapeWeight(0) > 0f) && other.relativeVelocity.sqrMagnitude > minForce)
		{
			StartCoroutine(Collapse());
		}
	}

	private IEnumerator Collapse()
	{
		collapsed = true;
		PlayParticles();
		sfx.Play();
		while (time <= collapseTime)
		{
			time += Time.deltaTime;
			mesh.SetBlendShapeWeight(0, time / collapseTime * 100f);
			yield return null;
		}
		for (int i = 0; i < colliders.Length; i++)
		{
			colliders[i].enabled = false;
		}
		collapsedCollider.enabled = true;
		mesh.SetBlendShapeWeight(0, 100f);
		OnBreak();
		base.enabled = false;
	}

	private void PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}
}
