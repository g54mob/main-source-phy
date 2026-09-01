using System.Collections;
using UnityEngine;

public class CannonBallDamage : SimBehaviour
{
	public float velocitySqrThreshold = 650f;

	public GameObject explosionPrefab;

	public GameObject underwaterExplosionPrefab;

	public float healthDamageMultiplier = 1f;

	public float jointDamageMultiplier = 1f;

	public bool autoExplodeUnderwater;

	public float underwaterExplodeTimeout = 5f;

	public bool alwaysExplode;

	public bool freezeImpact;

	public bool attachImpact;

	private Rigidbody attachedRigidbody;

	private Rigidbody myRigidbody;

	private float lastSqrVelocity;

	private NetworkCannonball netCannon;

	private bool hasParentMachine;

	private ServerMachine parentMachine;

	private float underwaterTime;

	public bool playerProjectile;

	public ParticleSystem trail;

	private bool hasTrail;

	public float projectileScale;

	public bool isChain;

	public CannonChainBall chainScript;

	protected override void Start()
	{
		base.Start();
		if (base.SimPhysics)
		{
			netCannon = GetComponent<NetworkCannonball>();
			if (base.SimPhysics && myRigidbody == null)
			{
				myRigidbody = GetComponent<Rigidbody>();
			}
			hasTrail = trail != null;
			if (hasTrail)
			{
				ParticleSystemRenderer component = trail.GetComponent<ParticleSystemRenderer>();
				WaterFogController.AddEffectMat(component.sharedMaterial);
			}
		}
	}

	public void SetParentMachine(ServerMachine machine)
	{
		parentMachine = machine;
		hasParentMachine = true;
	}

	public void ResetParentMachine()
	{
		hasParentMachine = false;
	}

	protected void FixedUpdate()
	{
		if (HasBasicInfo && basicInfo.InWater)
		{
			if (hasTrail && trail.isPlaying)
			{
				trail.Stop();
			}
			if (autoExplodeUnderwater)
			{
				underwaterTime += Time.fixedDeltaTime;
				if (underwaterTime > underwaterExplodeTimeout)
				{
					Explode(base.transform.position, Quaternion.identity, true);
					autoExplodeUnderwater = false;
				}
			}
		}
		if (base.SimPhysics && myRigidbody != null)
		{
			lastSqrVelocity = myRigidbody.velocity.sqrMagnitude;
			if (hasTrail && lastSqrVelocity > 0f && lastSqrVelocity < 5f && trail.isPlaying)
			{
				trail.Stop();
			}
		}
	}

	public void Explode(Vector3 explosionPos, Quaternion explosionRot, bool ignoreGodToolCheck = false)
	{
		GameObject gameObject = Object.Instantiate((!HasBasicInfo || !basicInfo.InWater || !(underwaterExplosionPrefab != null)) ? explosionPrefab : underwaterExplosionPrefab, explosionPos, explosionRot, ReferenceMaster.physicsGoalInstance) as GameObject;
		gameObject.transform.parent = base.transform.parent;
		gameObject.transform.localScale *= projectileScale;
		if (!alwaysExplode && !ignoreGodToolCheck)
		{
			StatMaster.GodTools.HasBeenUsed = true;
		}
		if (StatMaster.isMP && !StatMaster.isLocalSim)
		{
			if (StatMaster.isHosting)
			{
				byte[] array = new byte[13];
				NetworkCompression.CompressPosition(explosionPos, array, 0);
				NetworkCompression.CompressRotation(explosionRot, array, 6);
				ProjectileManager.Instance.Despawn(netCannon, array);
			}
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
	}

	protected void OnCollisionEnter(Collision other)
	{
		if (!base.SimPhysics || !StatMaster.levelSimulating)
		{
			return;
		}
		if (isChain)
		{
			chainScript.InvokeCollapse();
		}
		bool flag = false;
		Machine machine = ((!StatMaster.isHosting || StatMaster.isLocalSim || !hasParentMachine) ? Machine.Active() : parentMachine);
		flag = machine != null;
		bool flag2 = flag && machine.ExplodingCannonballs;
		if (!alwaysExplode && playerProjectile && flag && other.transform.IsChildOf(machine.transform))
		{
			return;
		}
		if ((alwaysExplode || flag2) && !isChain)
		{
			StartCoroutine(ScheduleExplode(other.contacts[0].normal));
		}
		else
		{
			if (other.collider == null || other.collider.attachedRigidbody == null || lastSqrVelocity < velocitySqrThreshold / 2f)
			{
				return;
			}
			attachedRigidbody = other.collider.attachedRigidbody;
			bool flag3 = attachedRigidbody.gameObject.layer != 26;
			BlockBehaviour blockBehaviour = attachedRigidbody.GetComponent<BlockBehaviour>();
			if (!object.ReferenceEquals(blockBehaviour, null))
			{
				if (blockBehaviour.gotChildBlocks)
				{
					BlockBehaviour childBlockFromCollider = blockBehaviour.GetChildBlockFromCollider(other.collider);
					if (!object.ReferenceEquals(childBlockFromCollider, null))
					{
						blockBehaviour = childBlockFromCollider;
					}
				}
				if (!blockBehaviour.isDestroyed)
				{
					if (blockBehaviour.Prefab.hasHealthBar)
					{
						blockBehaviour.BlockHealth.DamageBlock((float)((lastSqrVelocity < velocitySqrThreshold) ? 1 : 2) * healthDamageMultiplier);
					}
					else if (ReduceBreakForceOnImpact.Used && blockBehaviour.Prefab.reduceBreakforce)
					{
						blockBehaviour.BreakOnImpact.ReduceJointBreakForce(lastSqrVelocity * jointDamageMultiplier);
					}
					if (freezeImpact && (bool)blockBehaviour.iceTag)
					{
						blockBehaviour.iceTag.Freeze();
					}
				}
				if (blockBehaviour.IsArmor)
				{
					flag3 = false;
				}
			}
			else
			{
				ConfigurableJoint component = attachedRigidbody.GetComponent<ConfigurableJoint>();
				if ((bool)component)
				{
					component.breakForce -= lastSqrVelocity * jointDamageMultiplier;
					component.breakTorque -= lastSqrVelocity * jointDamageMultiplier;
				}
			}
			if (flag3 && attachImpact && !attachedRigidbody.isKinematic)
			{
				FixedJoint fixedJoint = myRigidbody.gameObject.AddComponent<FixedJoint>();
				fixedJoint.autoConfigureConnectedAnchor = true;
				float breakForce = (fixedJoint.breakTorque = 10000f);
				fixedJoint.breakForce = breakForce;
				fixedJoint.connectedBody = attachedRigidbody;
			}
		}
	}

	protected IEnumerator ScheduleExplode(Vector3 normal)
	{
		yield return new WaitForSeconds(Random.Range(0f, 0.05f));
		Explode(base.transform.position, Quaternion.FromToRotation(Vector3.forward, normal));
	}
}
