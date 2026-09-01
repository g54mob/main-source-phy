using UnityEngine;

public class IceProjectile : ProjectileScript
{
	public float speed = 2f;

	public bool disabledGravity;

	public GameObject explosionEffect;

	public float explosionRadius = 2f;

	private float radiusSquared;

	public override void FixedUpdate()
	{
		if ((!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim) && StatMaster.levelSimulating && (!hasAttached || deflected))
		{
			Rigidbody rigidbody = projectileInfo.Rigidbody;
			if (projectileInfo.noRigidbody || myTransform == null)
			{
				Debug.LogError(string.Concat("Body or transform null in ProjectileScript.FixedUpdate! MyBody: ", rigidbody, " myTransform: ", myTransform));
			}
			else
			{
				rigidbody.MovePosition(myTransform.position + myTransform.forward * speed);
			}
		}
	}

	public override void OnTriggerEnter(Collider other)
	{
		if ((!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim) && !other.isTrigger && !hasAttached && visObj.enabled && StatMaster.levelSimulating && !IgnoreOwnerCollision(other))
		{
			radiusSquared = explosionRadius * explosionRadius;
			Object.Instantiate(explosionEffect, base.transform.position, Quaternion.identity, base.transform.parent);
			OnExplode();
		}
	}

	private void OnExplode()
	{
		for (int i = 0; i < ReferenceMaster.ExternalForceObjectsArray.Length; i++)
		{
			AffectObject(ReferenceMaster.ExternalForceObjectsArray[i]);
		}
		for (int i = 0; i < ReferenceMaster.ExternalForceTemp.Count; i++)
		{
			AffectObject(ReferenceMaster.ExternalForceTemp[i]);
		}
		base.gameObject.SetActive(false);
	}

	private void AffectObject(BasicInfo bInfo)
	{
		float sqrMagnitude = (bInfo.transform.position - base.transform.position).sqrMagnitude;
		if (sqrMagnitude > radiusSquared)
		{
			return;
		}
		if (disabledGravity)
		{
			bInfo.Rigidbody.useGravity = false;
		}
		ForceFromHit(bInfo.Rigidbody);
		if (bInfo is BlockBehaviour)
		{
			BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
			if (blockBehaviour.isSimulating && blockBehaviour.Prefab.hasHealthBar)
			{
				if (blockBehaviour.gotChildBlocks)
				{
					foreach (BlockBehaviour key in blockBehaviour.parentedColliders.Keys)
					{
						if (key.Prefab.hasHealthBar)
						{
							key.BlockHealth.DamageBlock(blockDamageAmount);
							if (key.Prefab.canFreeze)
							{
								key.iceTag.Freeze();
							}
						}
					}
				}
				else
				{
					blockBehaviour.BlockHealth.DamageBlock(blockDamageAmount);
				}
				if (blockBehaviour.Prefab.canFreeze)
				{
					blockBehaviour.iceTag.Freeze();
				}
			}
			else if (deflectable)
			{
				Deflect();
				return;
			}
		}
		else if (bInfo is AIGenericEntity)
		{
			KillingHandler killingHandler = (bInfo as AIGenericEntity).aiEntity.my.killingHandler;
			if (!object.ReferenceEquals(killingHandler, null) && !killingHandler.my.AiCode.isDead)
			{
				if (deflectable && killingHandler.damageAmount.projectileDeflection > Random.value)
				{
					Deflect();
					return;
				}
				killingHandler.TakeDamage(attackDamage, injuryType);
			}
		}
		else if (bInfo is EnemyAISimple)
		{
			EnemyAISimple enemyAISimple = bInfo as EnemyAISimple;
			if (!enemyAISimple.isDead)
			{
				if (deflectable && enemyAISimple.projectileDeflection > Random.value)
				{
					Deflect();
					return;
				}
				enemyAISimple.TakeDamage(attackDamage, injuryType);
			}
		}
		else if (!StatMaster.isMP)
		{
			KillingHandler component = bInfo.Rigidbody.GetComponent<KillingHandler>();
			if (!object.ReferenceEquals(component, null) && !component.my.AiCode.isDead)
			{
				if (deflectable && component.damageAmount.projectileDeflection > Random.value)
				{
					Deflect();
					return;
				}
				component.TakeDamage(attackDamage, injuryType);
			}
		}
		int mask = 32;
		foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(bInfo.Rigidbody.gameObject))
		{
			if (bInfo.Rigidbody.gameObject.activeInHierarchy && @interface != null)
			{
				@interface.OnExplode(0f, 0f, 0f, Vector3.zero, 0f, mask, projectileInfo.InWater);
			}
		}
	}

	protected bool ValidateBasicInfo(BasicInfo b)
	{
		if (object.ReferenceEquals(b, null) || b.isDestroyed || !b.isSimulating || b.noRigidbody || b.isKinematic)
		{
			return false;
		}
		if (b.transform == null)
		{
			Debug.LogError("ERROR! Transform null for Please notify the devs!");
			return false;
		}
		if (b.Rigidbody == null)
		{
			Debug.LogError("ERROR! Rigidbody null for" + b.transform.name + "Please notify the devs!");
			return false;
		}
		return true;
	}
}
