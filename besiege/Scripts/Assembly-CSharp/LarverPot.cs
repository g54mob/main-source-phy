using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class LarverPot : MonoBehaviour
{
	public LayerMask layerMasky;

	public float duration = 0.3f;

	public Transform ojbToRotat;

	[FormerlySerializedAs("endRotation")]
	public Transform start;

	[FormerlySerializedAs("startRotation")]
	public Transform end;

	public bool hasPhysParticle;

	public ParticleSystem physicsParticles;

	public ParticleSystem Particles;

	public Transform[] sphereCastPositions;

	public ParticleCollisionCallback[] particleHooks;

	public Behaviour[] toEnable;

	public Light[] lights;

	public float flameRange = 12f;

	public float sphereCastRadius = 2f;

	private bool used;

	private float lastUsed;

	private void Start()
	{
		if (!StatMaster.isClient && StatMaster.levelSimulating)
		{
			for (int i = 0; i < particleHooks.Length; i++)
			{
				ParticleCollisionCallback obj = particleHooks[i];
				obj.callback = (Action<BasicInfo>)Delegate.Combine(obj.callback, new Action<BasicInfo>(Ignite));
			}
		}
	}

	protected void FixedUpdate()
	{
		if (hasPhysParticle)
		{
			physicsParticles.Simulate(Time.fixedDeltaTime, false, false, true);
		}
		if (StatMaster.isClient || !StatMaster.levelSimulating)
		{
			return;
		}
		Collider[] array = Physics.OverlapBox(base.transform.position, base.transform.lossyScale * 0.5f, base.transform.rotation, layerMasky, QueryTriggerInteraction.Ignore);
		foreach (Collider collider in array)
		{
			if ((bool)collider.attachedRigidbody && (bool)collider.attachedRigidbody.GetComponent<BasicInfo>())
			{
				TriggerPot(true);
				lastUsed = Time.fixedTime;
				break;
			}
		}
		if (Time.fixedTime > lastUsed + 1f)
		{
			TriggerPot(false);
		}
		if (used && sphereCastPositions.Length > 0)
		{
			FlamethrowerSphereCast(sphereCastPositions[0]);
		}
	}

	protected void FlamethrowerSphereCast(Transform obj)
	{
		RaycastHit hitInfo;
		if (Physics.SphereCast(obj.position, sphereCastRadius, obj.forward, out hitInfo, flameRange, layerMasky))
		{
			Debug.DrawLine(obj.position, hitInfo.point);
			if ((bool)hitInfo.collider.attachedRigidbody && (bool)hitInfo.collider.attachedRigidbody.GetComponent<FireTag>())
			{
				hitInfo.collider.attachedRigidbody.GetComponent<FireTag>().Ignite(1f);
			}
		}
	}

	protected void Ignite(BasicInfo info)
	{
		BasicInfo.BasicInfoType infoType = info.infoType;
		if (infoType == BasicInfo.BasicInfoType.Block)
		{
			BlockBehaviour blockBehaviour = info as BlockBehaviour;
			if (blockBehaviour.CanBurn)
			{
				blockBehaviour.fireTag.Ignite(1f);
			}
		}
		else
		{
			FireTag component = info.GetComponent<FireTag>();
			if (component != null)
			{
				component.Ignite(1f);
			}
		}
	}

	public void TriggerPot(bool enabled)
	{
		if (used != enabled)
		{
			used = enabled;
			StopAllCoroutines();
			StartCoroutine(Rotate(enabled));
		}
	}

	private IEnumerator Rotate(bool enabled)
	{
		float cTime = 0f;
		float rate = 1f / duration;
		if (!enabled)
		{
			Particles.Stop();
		}
		else
		{
			for (int i = 0; i < toEnable.Length; i++)
			{
				toEnable[i].enabled = true;
			}
		}
		Quaternion start = ojbToRotat.localRotation;
		Quaternion target = ((!enabled) ? this.start.localRotation : end.localRotation);
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			ojbToRotat.localRotation = Quaternion.Lerp(start, target, cTime);
			for (int j = 0; j < lights.Length; j++)
			{
				lights[j].intensity = ((!enabled) ? (1f - cTime) : cTime) * 1.5f;
			}
			yield return null;
		}
		ojbToRotat.localRotation = target;
		if (enabled)
		{
			Particles.Play();
			yield break;
		}
		for (int k = 0; k < toEnable.Length; k++)
		{
			toEnable[k].enabled = false;
		}
	}
}
