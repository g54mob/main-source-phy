using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollide : BreakBase, IExplosionEffect, IFireEffect
{
	public float radius = 5f;

	public float power = 10f;

	public float torquePower = 1000f;

	public float upPower = 6f;

	public Renderer bombVis;

	public Transform explosionEffectPrefab;

	public bool hasExploded;

	public bool explodeOnCollision;

	public float collideCutoff = 10f;

	public Transform parentObj;

	public Transform scaleRoot;

	public Collider IgnoreCollider;

	[HideInInspector]
	public int explisionMask;

	public bool ScaleExplosion;

	private float scale = 1f;

	public Transform underwaterEffect;

	private Rigidbody prevRigidbody;

	private Vector3 explosionPos;

	private Collider[] colliders;

	private Rigidbody hitAttachedRigidbody;

	private List<Rigidbody> prevRigidbodies = new List<Rigidbody>();

	protected override void Start()
	{
		base.Start();
		power *= 2f;
		upPower *= 0.25f;
		if (IgnoreCollider != null)
		{
			Physics.IgnoreCollision(IgnoreCollider, GetComponent<Collider>());
		}
		if (ScaleExplosion)
		{
			Vector3 localScale = scaleRoot.localScale;
			scale = Mathf.Max(localScale.x, localScale.y, localScale.z);
		}
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		Explodey();
		return !hasExploded && !StatMaster.Rules.DisableExplosions;
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & 0x20) != 0)
		{
			Explodey();
			return true;
		}
		return false;
	}

	public void Explodey()
	{
		if (hasExploded || StatMaster.Rules.DisableExplosions)
		{
			return;
		}
		Vector3 position = base.transform.position;
		NetworkBlock networkBlock = null;
		bool flag = false;
		if (StatMaster.isMP)
		{
			networkBlock = base.NetBlock;
			flag = networkBlock != null;
		}
		if (HasBasicInfo)
		{
			Vector3 pos = position + base.transform.forward;
			if ((basicInfo.InWater && !StatMaster.GodTools.GravityDisabled) || (WaterController.Exist && WaterController.IsUnderwater(pos)))
			{
				SpawnEffect(underwaterEffect, position);
				parentObj.gameObject.SetActive(false);
				SendExplodeEvent(flag, networkBlock);
				return;
			}
		}
		bool flag2 = true;
		if (base.SimPhysics)
		{
			Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
			component.isKinematic = true;
			hasExploded = true;
			position = base.transform.position;
			colliders = Physics.OverlapSphere(position, radius * scale);
			Collider[] array = colliders;
			foreach (Collider collider in array)
			{
				if (collider == null)
				{
					continue;
				}
				if (collider.attachedRigidbody != null)
				{
					hitAttachedRigidbody = collider.attachedRigidbody;
				}
				if (collider.transform.parent != null)
				{
					Rigidbody component2 = collider.transform.parent.GetComponent<Rigidbody>();
					if (component2 != null)
					{
						component2.WakeUp();
					}
				}
				if (!(hitAttachedRigidbody != component))
				{
					continue;
				}
				if (hitAttachedRigidbody != null && !prevRigidbodies.Contains(hitAttachedRigidbody) && hitAttachedRigidbody.gameObject.layer != 20 && hitAttachedRigidbody.gameObject.layer != 22 && hitAttachedRigidbody.tag != "KeepConstraintsAlways")
				{
					if (!(hitAttachedRigidbody.GetComponent<TimedRocket>() != null))
					{
						hitAttachedRigidbody.WakeUp();
						hitAttachedRigidbody.constraints = RigidbodyConstraints.None;
						float num = power;
						float num2 = upPower;
						if (hitAttachedRigidbody.gameObject.tag == "Debris")
						{
							InheritExplosion componentInParent = hitAttachedRigidbody.GetComponentInParent<InheritExplosion>();
							if ((bool)componentInParent)
							{
								num *= componentInParent.forceScaler;
								num2 *= componentInParent.upScaler;
							}
						}
						hitAttachedRigidbody.AddExplosionForce(num, position, radius * scale, num2);
						hitAttachedRigidbody.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * torquePower);
					}
					foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(hitAttachedRigidbody.gameObject))
					{
						if (hitAttachedRigidbody.gameObject.activeInHierarchy)
						{
							@interface.OnExplode(power, upPower, torquePower, position, radius * scale, explisionMask, HasBasicInfo && basicInfo.InWater);
						}
					}
					prevRigidbodies.Add(hitAttachedRigidbody);
				}
				else
				{
					if (!collider.transform.parent)
					{
						continue;
					}
					Rigidbody component3 = collider.transform.parent.GetComponent<Rigidbody>();
					if (!(component3 != null) || !(component3 != component) || prevRigidbodies.Contains(component3))
					{
						continue;
					}
					if (component3.gameObject.tag == "Debris")
					{
						InheritExplosion componentInParent2 = component3.GetComponentInParent<InheritExplosion>();
						if ((bool)componentInParent2)
						{
							power *= componentInParent2.forceScaler;
							upPower *= componentInParent2.upScaler;
						}
					}
					component3.AddExplosionForce(power, position, radius * scale, upPower);
					component3.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * torquePower);
					prevRigidbodies.Add(component3);
				}
			}
		}
		else if (flag)
		{
			flag2 = !NetworkBlock.applyingState;
		}
		if (flag2)
		{
			GameObject gameObject = SpawnEffect(explosionEffectPrefab, position);
			gameObject.GetComponent<ExplosionEffect>().startSize = Vector3.one * Mathf.Sqrt(scale);
		}
		OnBreak();
		parentObj.gameObject.SetActive(false);
		SendExplodeEvent(flag, networkBlock);
	}

	private GameObject SpawnEffect(Transform prefab, Vector3 pos)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab.gameObject, pos, Quaternion.identity, ReferenceMaster.physicsGoalInstance) as GameObject;
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		if (ScaleExplosion)
		{
			gameObject.transform.localScale = gameObject.transform.localScale * Mathf.Sqrt(scale);
		}
		return gameObject;
	}

	private void SendExplodeEvent(bool hasNetBlock, NetworkBlock netBlock)
	{
		if (hasNetBlock)
		{
			if (base.SimPhysics)
			{
				netBlock.Event(NetworkEntity.EntityEvent.Explode);
			}
			else
			{
				explosionPos = netBlock.Position;
			}
		}
		else if (StatMaster.isMP)
		{
			Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && !explodeOnCollision && !hasExploded && other.gameObject.layer != 2 && other.gameObject.layer != 27)
		{
			Explodey();
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (base.SimPhysics && base.isSimulating && explodeOnCollision && !hasExploded && !other.collider.CompareTag("IgnoreBreakCollision") && other.collider.gameObject.layer != 2 && other.relativeVelocity.sqrMagnitude > collideCutoff)
		{
			Explodey();
		}
	}

	private void DisableOnExplode(bool toggle)
	{
		GetComponent<Collider>().enabled = toggle;
		bombVis.enabled = toggle;
	}
}
