using System;
using System.Collections;
using UnityEngine;

public class SmallExplosion : SimBehaviour
{
	public float radius = 5f;

	public float power = 10f;

	public float upPower = 3f;

	public ParticleSystem[] particles;

	public RandomSoundController SFX;

	public Transform dustCraterQuad;

	public float randomDelay = 0.08f;

	public bool hasExploded;

	public Renderer thisRenderer;

	public Collider thisCollider;

	public Rigidbody thisRigidbody;

	public float ForceToBreak = 5f;

	private Vector3 explosionPos;

	private Collider[] colliders;

	private bool simPhys;

	private bool isSim;

	protected override void Start()
	{
		base.Start();
		simPhys = base.SimPhysics;
		isSim = base.isSimulating;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (simPhys && isSim && collision.relativeVelocity.magnitude > ForceToBreak)
		{
			StartCoroutine(Explode());
		}
	}

	private void ExplodeMessage()
	{
		StartCoroutine(Explode());
	}

	public IEnumerator Explode()
	{
		if (hasExploded)
		{
			yield break;
		}
		if (StatMaster.isMP && base.SimPhysics && !StatMaster.IsLevelEditorOnly)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.Explode);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		hasExploded = true;
		yield return new WaitForSeconds(UnityEngine.Random.Range(0f, randomDelay));
		ExplosionForce();
		PlayParticles();
		SFX.Play();
		DustCraterQuad();
		DisableComponents();
	}

	private void PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].transform.forward = Vector3.up;
			particles[i].Play();
		}
	}

	private void DisableComponents()
	{
		thisRenderer.enabled = false;
		thisCollider.enabled = false;
		if (thisRigidbody != null)
		{
			thisRigidbody.isKinematic = true;
		}
	}

	private void ExplosionForce()
	{
		if (!simPhys)
		{
			return;
		}
		explosionPos = base.transform.position;
		colliders = Physics.OverlapSphere(explosionPos, radius);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			Rigidbody attachedRigidbody = collider.attachedRigidbody;
			if (attachedRigidbody != null)
			{
				attachedRigidbody.AddExplosionForce(power, explosionPos, radius, upPower);
				if (attachedRigidbody.tag != "KeepConstraintsAlways")
				{
					SendExplodeMessages(attachedRigidbody, collider);
				}
			}
		}
	}

	private void DustCraterQuad()
	{
		if (!isSim)
		{
			return;
		}
		if (dustCraterQuad == null)
		{
			Debug.LogWarning("SmallExplosion doesn't have a dust crater quad!");
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
			dustCraterQuad.localEulerAngles = new Vector3(dustCraterQuad.localEulerAngles.x, dustCraterQuad.localEulerAngles.y, UnityEngine.Random.Range(0f, 360f));
		}
		else
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = false;
		}
	}

	private void SendExplodeMessages(Rigidbody obj, Collider coll)
	{
		if (simPhys)
		{
			obj.WakeUp();
			obj.constraints = RigidbodyConstraints.None;
			if (obj.gameObject.GetComponent<ExplodeMultiplier>() != null)
			{
				obj.gameObject.GetComponent<ExplodeMultiplier>().Explodey(power, explosionPos, radius, upPower);
			}
			if (obj.gameObject.GetComponent<SimpleBirdAI>() != null)
			{
				obj.gameObject.GetComponent<SimpleBirdAI>().Explode();
			}
			if (obj.gameObject.GetComponent<BleedOnJointBreak>() != null)
			{
				obj.gameObject.GetComponent<BleedOnJointBreak>().Killed(false);
			}
			if (obj.gameObject.GetComponent<BlockHealthBar>() != null)
			{
				obj.gameObject.GetComponent<BlockHealthBar>().DamageBlock(1f);
			}
			if (obj.gameObject.GetComponent<StructuralPhysTile>() != null && (explosionPos - obj.position).sqrMagnitude < 13f)
			{
				obj.gameObject.GetComponent<StructuralPhysTile>().DestroyTile(UnityEngine.Random.insideUnitSphere);
			}
			if (obj.gameObject.GetComponent<PhysNodeTile>() != null && (explosionPos - obj.position).sqrMagnitude < 13f)
			{
				obj.gameObject.GetComponent<PhysNodeTile>().BreakNode(coll, UnityEngine.Random.insideUnitSphere);
			}
			if (obj.gameObject.GetComponent<CastleWallBreak>() != null)
			{
				obj.gameObject.GetComponent<CastleWallBreak>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if (obj.gameObject.GetComponent<BreakOnForce>() != null)
			{
				obj.gameObject.GetComponent<BreakOnForce>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if (obj.gameObject.GetComponent<BreakOnForceNoSpawn>() != null)
			{
				obj.gameObject.GetComponent<BreakOnForceNoSpawn>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if (obj.gameObject.GetComponent<BreakOnForceNoScaling>() != null)
			{
				obj.gameObject.GetComponent<BreakOnForceNoScaling>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			InjuryController component = obj.gameObject.GetComponent<InjuryController>();
			if (component != null)
			{
				component.activeType = InjuryType.Fire;
				component.Kill();
			}
		}
	}
}
