using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallExplosion : SimBehaviour, IExplosionEffect
{
	public float radius = 5f;

	public float power = 10f;

	public float upPower = 3f;

	public Vector3 originalScale = new Vector3(0.5f, 0.5f, 0.5f);

	public ParticleSystem[] particles;

	public RandomSoundController SFX;

	public Transform dustCraterQuad;

	public bool hasExploded;

	private Vector3 explosionPos;

	private Collider[] hitColliders;

	private Rigidbody colAttachedRigidbody;

	private List<Rigidbody> prevRigidbodies = new List<Rigidbody>();

	protected override void Start()
	{
		base.Start();
		power *= 2f;
		upPower *= 0.25f;
		Explode();
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & 0x20) != 0)
		{
			Explode();
			return true;
		}
		return false;
	}

	private void Explode()
	{
		if (!hasExploded)
		{
			StartCoroutine(ExplodeMessage());
		}
	}

	public IEnumerator ExplodeMessage()
	{
		if (!hasExploded)
		{
			ExplosionForce();
			PlayParticles();
			if (SFX != null)
			{
				SFX.Play();
			}
			DustCraterQuad();
			hasExploded = true;
			yield return new WaitForSeconds(4f);
			Object.Destroy(base.gameObject);
		}
	}

	private void PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Stop();
			particles[i].randomSeed = (uint)Random.Range(0, 9999999);
			particles[i].Play();
		}
	}

	private void ExplosionForce()
	{
		if (!base.SimPhysics)
		{
			return;
		}
		radius *= base.transform.localScale.x / originalScale.x;
		explosionPos = base.transform.position;
		hitColliders = Physics.OverlapSphere(explosionPos, radius);
		Collider[] array = hitColliders;
		foreach (Collider collider in array)
		{
			if (!collider.attachedRigidbody || prevRigidbodies.Contains(collider.attachedRigidbody) || collider.attachedRigidbody.gameObject.layer == 20 || collider.attachedRigidbody.gameObject.layer == 22 || !(collider.attachedRigidbody.tag != "KeepConstraintsAlways"))
			{
				continue;
			}
			colAttachedRigidbody = collider.attachedRigidbody;
			colAttachedRigidbody.WakeUp();
			colAttachedRigidbody.constraints = RigidbodyConstraints.None;
			colAttachedRigidbody.AddExplosionForce(power, explosionPos, radius, upPower);
			prevRigidbodies.Add(colAttachedRigidbody);
			int mask = 237;
			foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(colAttachedRigidbody.gameObject))
			{
				@interface.OnExplode(power, upPower, 0f, explosionPos, radius, mask, false);
			}
		}
	}

	private void DustCraterQuad()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (dustCraterQuad == null)
		{
			Debug.LogWarning("ControllableBomb doesn't have a dust crater quad!");
			return;
		}
		Vector3 position = base.transform.position;
		float floorHeight = SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
		if (StatMaster.ShowExplosionDecals && position.y < floorHeight + 5f)
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = true;
			dustCraterQuad.parent = ReferenceMaster.physicsGoalInstance;
			dustCraterQuad.position = new Vector3(position.x, floorHeight + 0.025f, position.z);
			dustCraterQuad.forward = Vector3.up;
			dustCraterQuad.localEulerAngles = new Vector3(dustCraterQuad.localEulerAngles.x, dustCraterQuad.localEulerAngles.y, Random.Range(0f, 360f));
		}
		else
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = false;
		}
	}
}
