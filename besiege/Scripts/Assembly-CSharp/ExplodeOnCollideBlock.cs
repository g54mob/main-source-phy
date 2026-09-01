using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/ExplodeOnCollideBlock")]
public class ExplodeOnCollideBlock : BlockBehaviour, IExplosionEffect, IFireEffect
{
	public int version = 1;

	public SphereCollider col;

	public float radius = 5f;

	public float power = 10f;

	public float torquePower = 1000f;

	public float upPower = 6f;

	public Transform explosionEffect;

	public Transform underwaterEffect;

	public bool hasExploded;

	public bool explodeOnCollision;

	public float collideCutoff = 10f;

	public Transform parentObj;

	public LayerMask layerMask = -14230267;

	private Vector3 explosionPos;

	private Collider[] colliders;

	private Collider[] myColliders;

	private HashSet<int> prevRigidbodies = new HashSet<int>();

	private Rigidbody hitAttachedRigidbody;

	protected override void Start()
	{
		base.Start();
		power *= 2f;
		upPower *= 0.25f;
		if (isSimulating && explosionEffect != null)
		{
			Transform obj = explosionEffect;
			Transform physicsGoalInstance = ReferenceMaster.physicsGoalInstance;
			underwaterEffect.parent = physicsGoalInstance;
			obj.parent = physicsGoalInstance;
			explosionEffect.transform.rotation = Quaternion.identity;
			underwaterEffect.transform.rotation = Quaternion.identity;
			Transform obj2 = explosionEffect.transform;
			Vector3 localScale = Vector3.one;
			underwaterEffect.transform.localScale = localScale;
			obj2.localScale = localScale;
		}
		myColliders = GetComponentsInChildren<Collider>();
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		Explodey();
		return !hasExploded && !StatMaster.Rules.DisableExplosions;
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!isSimulating || !SimPhysics || hasExploded)
		{
			return false;
		}
		if ((float)(mask & 0x20) != 0f)
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
		if (SimPhysics)
		{
			for (int i = 0; i < myColliders.Length; i++)
			{
				myColliders[i].enabled = false;
			}
		}
		hasExploded = true;
		Vector3 position = base.transform.position;
		if (StatMaster.isMP)
		{
			NetworkBlock netBlock = NetBlock;
			if (netBlock != null)
			{
				if (SimPhysics)
				{
					netBlock.Event(NetworkEntity.EntityEvent.Explode);
				}
				else
				{
					position = netBlock.Position;
				}
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		Vector3 vector = position + base.transform.forward;
		if ((base.InWater && !StatMaster.GodTools.GravityDisabled) || (WaterController.Exist && WaterController.IsUnderwater(vector)))
		{
			underwaterEffect.transform.position = vector;
			underwaterEffect.gameObject.SetActive(true);
			base.gameObject.SetActive(false);
			return;
		}
		explosionEffect.transform.position = vector;
		explosionEffect.gameObject.SetActive(true);
		if (SimPhysics)
		{
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			Rigidbody rigidbody = Rigidbody;
			rigidbody.isKinematic = true;
			int instanceID = rigidbody.GetInstanceID();
			position = base.transform.position;
			colliders = Physics.OverlapSphere(position, radius, layerMask);
			int mask = 253;
			Collider[] array = colliders;
			foreach (Collider collider in array)
			{
				int num4 = 0;
				hitAttachedRigidbody = collider.attachedRigidbody;
				if (hitAttachedRigidbody == null)
				{
					continue;
				}
				num4 = hitAttachedRigidbody.GetInstanceID();
				if (num4 == instanceID || prevRigidbodies.Contains(num4) || hitAttachedRigidbody.CompareTag("KeepConstraintsAlways"))
				{
					continue;
				}
				GameObject gameObject = hitAttachedRigidbody.gameObject;
				if (gameObject.name != "Rocket")
				{
					hitAttachedRigidbody.constraints = RigidbodyConstraints.None;
					float num5 = power;
					float num6 = upPower;
					if (gameObject.CompareTag("Debris"))
					{
						InheritExplosion componentInParent = gameObject.GetComponentInParent<InheritExplosion>();
						if ((bool)componentInParent)
						{
							num5 *= componentInParent.forceScaler;
							num6 *= componentInParent.upScaler;
						}
					}
					hitAttachedRigidbody.AddExplosionForce(num5 * num, position, radius, num6 * num2);
					hitAttachedRigidbody.AddRelativeTorque(UnityEngine.Random.insideUnitSphere.normalized * torquePower * num3);
				}
				foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(gameObject))
				{
					@interface.OnExplode(power, upPower, torquePower, position, radius, mask, base.InWater);
				}
				prevRigidbodies.Add(num4);
			}
		}
		prevRigidbodies.Clear();
		base.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!explodeOnCollision && SimPhysics && !hasExploded && isSimulating && other.gameObject.layer != 2 && other.gameObject.layer != 27)
		{
			Explodey();
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (explodeOnCollision && SimPhysics && !hasExploded && isSimulating && other.collider.gameObject.layer != 2 && other.relativeVelocity.sqrMagnitude > ((!other.collider.gameObject.CompareTag("BombSlick")) ? collideCutoff : (collideCutoff * 3f)))
		{
			Explodey();
		}
	}

	public override void OnSave(XDataHolder data)
	{
		data.Write("bmt-version", version);
		base.OnSave(data);
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating && !SimPhysics)
		{
			return;
		}
		if (!data.HasKey("bmt-version"))
		{
			if (data.WasLoadedFromFile)
			{
				version = 0;
				data.Write("bmt-version", version);
			}
		}
		else
		{
			version = data.ReadInt("bmt-version");
		}
		SetVersion();
	}

	public void SetVersion()
	{
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			if (version == 0)
			{
				col.center = new Vector3(0.01116767f, 0.005472944f, 0.9955841f);
				col.radius = 0.759653f;
				Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
			}
			else
			{
				col.center = new Vector3(0f, 0f, 1f);
				col.radius = 0.75f;
				Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
		}
	}
}
