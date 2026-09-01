using System.Collections;
using UnityEngine;

public class CanonBlgFly : MonoBehaviour
{
	public float ReloadTime = 5f;

	public float ShootSpeed = 10000f;

	public Transform ShootPos;

	public Rigidbody CanonBall;

	public ParticleSystem ShootParticle;

	public ParticleSystem DustParticle;

	public ParticleSystem ShrapnelParticle;

	public float knockBackForce = 15000f;

	public float torqueToAdd = 15000f;

	public float upAmount = 10f;

	public RandomSoundController sfx;

	public Light lightSource;

	public float damageRadius = 8f;

	public float lightLerpSpeed;

	public FreighterAI FreighterAi;

	private bool Shooting;

	private void OnTriggerEnter(Collider other)
	{
		if (StatMaster.levelSimulating && (bool)other.attachedRigidbody && other.attachedRigidbody.GetComponent<BlockBehaviour>() != null && !Shooting && !FreighterAi.machineBroken)
		{
			StartCoroutine(Shotgun(other.attachedRigidbody));
		}
	}

	private IEnumerator Shotgun(Rigidbody obj)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			yield break;
		}
		Shooting = true;
		ShootParticle.Play();
		DustParticle.Play();
		ShrapnelParticle.Play();
		sfx.Play();
		StartCoroutine(LerpLight());
		yield return new WaitForSeconds(0.05f);
		Collider[] colliders = Physics.OverlapSphere(ShootPos.position, damageRadius);
		Collider[] array = colliders;
		foreach (Collider hit in array)
		{
			if (!hit || !hit.attachedRigidbody)
			{
				continue;
			}
			BlockBehaviour block = hit.attachedRigidbody.GetComponent<BlockBehaviour>();
			if (block != null)
			{
				hit.attachedRigidbody.AddForce((ShootPos.forward + Random.insideUnitSphere * 2f) * knockBackForce * Random.Range(0.2f, 1f) + Vector3.up * upAmount);
				hit.attachedRigidbody.AddTorque(Random.insideUnitSphere.normalized * torqueToAdd);
				if (block is SqrBalloonController)
				{
					(block as SqrBalloonController).Pop();
				}
			}
		}
		yield return new WaitForSeconds(ReloadTime);
		Shooting = false;
	}

	private IEnumerator LerpLight()
	{
		float cTime = 0f;
		float rate = 1f / lightLerpSpeed;
		lightSource.enabled = true;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			lightSource.intensity = Mathf.Lerp(8f, 0f, cTime);
			yield return null;
		}
		lightSource.enabled = false;
	}
}
