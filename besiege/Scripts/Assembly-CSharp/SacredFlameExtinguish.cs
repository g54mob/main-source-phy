using System.Collections;
using UnityEngine;

public class SacredFlameExtinguish : MonoBehaviour
{
	public RandomSoundController sfx;

	public ParticleSystem[] particles;

	public ParticleSystem[] disableParticles;

	public Light pointLight;

	public AudioSource flameSFX;

	public bool isDead;

	public float radius = 5f;

	public float power = 10f;

	public float upPower = 3f;

	public bool explode = true;

	public Transform explosionSphere;

	public float explosionSphereRadius = 4f;

	public float completionDelay = 0.5f;

	public Transform graveStoneParent;

	public float graveBreakTimer = 0.15f;

	private Vector3 explosionPos;

	private Collider[] colliders;

	private Rigidbody colAttachedRigidbody;

	private void Doused()
	{
		if (!isDead)
		{
			isDead = true;
			sfx.Play();
			flameSFX.Stop();
			if (graveStoneParent != null)
			{
				StartCoroutine(ExplodeGraves());
			}
			StartCoroutine(AddToPercentageBar());
			if (explode)
			{
				ExplosionForce();
			}
			PlayParticles();
			DisableParticles();
			pointLight.enabled = false;
		}
	}

	private IEnumerator AddToPercentageBar()
	{
		yield return new WaitForSeconds(completionDelay);
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}

	private void PlayParticles()
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].Play();
		}
	}

	private void DisableParticles()
	{
		for (int i = 0; i < disableParticles.Length; i++)
		{
			disableParticles[i].Stop();
		}
	}

	private IEnumerator ExplodeGraves()
	{
		yield return new WaitForSeconds(0.4f);
		for (int i = 0; i < graveStoneParent.childCount; i++)
		{
			StartCoroutine(ExplodeSingleGrave(graveStoneParent.GetChild(i), (float)i * graveBreakTimer));
		}
	}

	private IEnumerator ExplodeSingleGrave(Transform obj, float timer)
	{
		yield return new WaitForSeconds(timer);
		obj.GetComponent<BreakOnForce>().ExternalBreak();
	}

	private void ExplosionForce()
	{
		explosionPos = base.transform.position;
		colliders = Physics.OverlapSphere(explosionPos, radius);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if ((bool)collider.attachedRigidbody)
			{
				colAttachedRigidbody = collider.attachedRigidbody;
				colAttachedRigidbody.AddExplosionForce(power, explosionPos + Vector3.right * 5f, radius, upPower);
				SendExplodeMessages(colAttachedRigidbody);
			}
		}
	}

	private void SendExplodeMessages(Rigidbody obj)
	{
		if (!StatMaster.isClient)
		{
			obj.WakeUp();
			obj.constraints = RigidbodyConstraints.None;
			if ((bool)obj.gameObject.GetComponent<ExplodeMultiplier>())
			{
				obj.gameObject.GetComponent<ExplodeMultiplier>().Explodey(power, explosionPos, radius, upPower);
			}
			if ((bool)obj.gameObject.GetComponent<SimpleBirdAI>())
			{
				obj.gameObject.GetComponent<SimpleBirdAI>().Explode();
			}
			if ((bool)obj.gameObject.GetComponent<ExplodeOnCollide>())
			{
				obj.gameObject.GetComponent<ExplodeOnCollide>().Explodey();
			}
			if ((bool)obj.gameObject.GetComponent<BleedOnJointBreak>())
			{
				obj.gameObject.GetComponent<BleedOnJointBreak>().Killed(false);
			}
			if ((bool)obj.gameObject.GetComponent<BlockHealthBar>())
			{
				obj.gameObject.GetComponent<BlockHealthBar>().DamageBlock(1f);
			}
			if ((bool)obj.gameObject.GetComponent<CastleWallBreak>())
			{
				obj.gameObject.GetComponent<CastleWallBreak>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if ((bool)obj.gameObject.GetComponent<BreakOnForce>())
			{
				obj.gameObject.GetComponent<BreakOnForce>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if ((bool)obj.gameObject.GetComponent<BreakOnForceNoSpawn>())
			{
				obj.gameObject.GetComponent<BreakOnForceNoSpawn>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if ((bool)obj.gameObject.GetComponent<BreakOnForceNoScaling>())
			{
				obj.gameObject.GetComponent<BreakOnForceNoScaling>().BreakExplosion(power, explosionPos, radius, upPower);
			}
			if ((bool)obj.gameObject.GetComponent<InjuryController>())
			{
				obj.gameObject.GetComponent<InjuryController>().activeType = InjuryType.Fire;
				obj.gameObject.GetComponent<InjuryController>().Kill();
			}
		}
	}
}
