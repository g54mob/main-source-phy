using System;
using System.Collections;
using UnityEngine;

public class SimpleBirdAI : BreakBase, IExplosionEffect
{
	public Transform gibParticles;

	public Transform dustParticles;

	public SineBob sineBobCode;

	public float plummetSpeed = 20f;

	public float impactForce = 20f;

	public bool popped;

	private float thresh;

	public Action onFireKill;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && HasBasicInfo)
		{
			InvokeRepeating("RareUpdate", UnityEngine.Random.Range(0.5f, 2f), 0.2f);
		}
	}

	private void RareUpdate()
	{
		if (thresh == 0f)
		{
			thresh = Mathf.Pow(base.transform.localPosition.magnitude + 80f, 2f);
		}
		if (basicInfo.hasBeenHovered && basicInfo.lastHoverPct <= 0f && base.transform.localPosition.sqrMagnitude > thresh)
		{
			WindKill();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && !popped && !other.isTrigger)
		{
			BleedOnObject(other);
			Pop();
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if ((mask & 0x40) != 0)
		{
			Explode();
			return true;
		}
		return false;
	}

	public void Explode()
	{
		Pop();
	}

	public void Pop()
	{
		popped = true;
		AddToPercentageBar();
		if (OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			UnityEngine.Object.Instantiate(gibParticles, base.transform.position, base.transform.rotation);
		}
		else
		{
			UnityEngine.Object.Instantiate(dustParticles, base.transform.position, base.transform.rotation);
		}
		OnBreak();
		base.gameObject.SetActive(false);
	}

	private void PushObject(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			attachedRigidbody.AddForce((base.transform.position - attachedRigidbody.position).normalized * (0f - impactForce));
		}
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}

	private IEnumerator FireKill()
	{
		sineBobCode.enabled = false;
		base.transform.parent = null;
		if (!StatMaster.isMP)
		{
			AchievementHelper.Increment(16, 1);
		}
		while (!popped)
		{
			base.transform.Translate((-Vector3.forward - Vector3.up) * Time.deltaTime * plummetSpeed);
			yield return null;
		}
	}

	private void WindKill()
	{
		if (!HasBasicInfo || !basicInfo.noRigidbody)
		{
			sineBobCode.enabled = false;
			basicInfo.Rigidbody.useGravity = true;
		}
	}

	private void BleedOnObject(Collider other)
	{
		if (!OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			return;
		}
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody != null)
		{
			BlockBehaviour component = attachedRigidbody.GetComponent<BlockBehaviour>();
			if (component != null)
			{
				component.BloodSplatter();
			}
		}
	}
}
