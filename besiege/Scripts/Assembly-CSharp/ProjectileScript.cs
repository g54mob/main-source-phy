using System;
using System.Collections;
using InternalModding.Blocks;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	[Serializable]
	public class Rotation
	{
		[HideInInspector]
		public float dps;

		public Vector3 rotateAxis = -Vector3.right;

		public float degreesPerSecond = -270f;

		public float randomRotationVariation = 0.1f;
	}

	public static Action<ProjectileScript> onUpdateProjectile;

	[HideInInspector]
	public float attackDamage = 100f;

	public Rotation rotation = new Rotation();

	public bool deflectable;

	public bool freezing;

	public bool canAttach = true;

	public bool canExplode;

	public bool alwaysExplodes;

	public bool despawnOnCollision;

	public float killTimer = 1f;

	public bool useKillTimer = true;

	public bool despawnAfterEnable;

	public bool despawnInWater;

	public bool destroyOnDespawn;

	public float blockDamageAmount = 1f;

	public InjuryType injuryType = InjuryType.Sharp;

	public bool triggerProjectile;

	public ProjectileInfo projectileInfo;

	public GameObject explosionPrefab;

	public RandomSoundController randomSoundController;

	public FireController firecontrol;

	public float firetime = 2f;

	public float impactForceMultiplier = 10f;

	public Collider col;

	public Collider[] cols;

	public float colliderEnableTime;

	public bool disableCollider = true;

	public MeshRenderer visObj;

	public bool hasAttached;

	public float lookAtTorquePower = 100f;

	public bool enableRotation;

	public int ownerID;

	public Vector3 projectilePosition;

	public Transform gyro;

	private Rigidbody attachedRigidbody;

	protected Transform myTransform;

	private Vector3 worldUp;

	protected bool deflected;

	private float fTime = 2f;

	private float colliderTime;

	public bool zeroGUnderwater;

	private bool despawning;

	public void Awake()
	{
		if (myTransform == null)
		{
			myTransform = base.transform;
		}
		hasAttached = false;
		if (gyro == null)
		{
			gyro = base.transform.GetChild(0).transform;
		}
	}

	public void Start()
	{
		worldUp = Vector3.up;
	}

	public void OnEnable()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (StatMaster.isClient && StatMaster.Mode.LevelEditor.clientGlobalSim)
		{
			base.enabled = false;
			return;
		}
		if ((!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim) && !projectileInfo.noRigidbody)
		{
			projectileInfo.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
		deflected = false;
		if (enableRotation)
		{
			rotation.dps = rotation.degreesPerSecond * UnityEngine.Random.Range(1f - rotation.randomRotationVariation, 1f + rotation.randomRotationVariation);
		}
		if (object.ReferenceEquals(randomSoundController, null))
		{
			randomSoundController = base.gameObject.GetComponent<RandomSoundController>();
		}
		if (myTransform == null)
		{
			myTransform = base.transform;
		}
		hasAttached = false;
		if (gyro == null)
		{
			gyro = base.transform.GetChild(0).transform;
		}
		gyro.localRotation = Quaternion.identity;
		if (col == null)
		{
			col = cols[0];
		}
		else if (cols == null)
		{
			cols = new Collider[1] { col };
		}
		if (firecontrol != null && firecontrol.fireTagCode != null)
		{
			firecontrol.fireTagCode.burning = false;
			firecontrol.onFire = false;
			fTime = firetime;
			firecontrol.SetFireDuration(0f);
			firecontrol.igniteDelay = 0f;
			if (firecontrol.hasStartingAtributes)
			{
				firecontrol.SetBurnedLevel(0f);
			}
			firecontrol.StopParticles();
		}
		despawning = false;
		if (despawnAfterEnable)
		{
			Despawn();
		}
	}

	public void Update()
	{
		bool flag = !StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim;
		if (!StatMaster.levelSimulating || !flag)
		{
			return;
		}
		Rigidbody rigidbody = projectileInfo.Rigidbody;
		if (zeroGUnderwater && WaterController.Exist)
		{
			if (base.transform.position.y < WaterController.waterTransformHeight)
			{
				if (rigidbody.useGravity)
				{
					rigidbody.drag = 1f;
					rigidbody.useGravity = false;
				}
			}
			else if (!rigidbody.useGravity)
			{
				rigidbody.drag = 0f;
				rigidbody.useGravity = true;
			}
		}
		if (colliderTime < colliderEnableTime)
		{
			colliderTime += Time.deltaTime;
		}
		else if (!col.enabled && disableCollider)
		{
			projectileInfo.InWater = false;
			Collider[] array = cols;
			foreach (Collider collider in array)
			{
				collider.enabled = true;
			}
		}
		if (!projectileInfo.noRigidbody)
		{
			if (Time.timeScale <= 0.6f)
			{
				if (rigidbody.interpolation == RigidbodyInterpolation.None)
				{
					rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				}
			}
			else if (rigidbody.interpolation == RigidbodyInterpolation.Interpolate)
			{
				rigidbody.interpolation = RigidbodyInterpolation.None;
			}
		}
		if (hasAttached)
		{
			if (firecontrol != null && firecontrol.onFire)
			{
				if (fTime <= 0f)
				{
					if ((bool)firecontrol.fireTagCode)
					{
						firecontrol.fireTagCode.burning = false;
						firecontrol.onFire = false;
					}
				}
				else
				{
					fTime -= Time.deltaTime;
				}
			}
		}
		else if (StatMaster.isMP && triggerProjectile && onUpdateProjectile != null)
		{
			if (!projectileInfo.noRigidbody)
			{
				projectilePosition = rigidbody.position;
			}
			onUpdateProjectile(this);
		}
		if (despawnInWater && projectileInfo.InWater)
		{
			Despawn(false);
		}
	}

	public virtual void FixedUpdate()
	{
		if ((StatMaster.isMP && !StatMaster.isHosting && !StatMaster.isLocalSim) || !StatMaster.levelSimulating || (hasAttached && !deflected))
		{
			return;
		}
		Rigidbody rigidbody = projectileInfo.Rigidbody;
		if (projectileInfo.noRigidbody || myTransform == null)
		{
			Debug.LogError(string.Concat("Body or transform null in ProjectileScript.FixedUpdate! MyBody: ", rigidbody, " myTransform: ", myTransform));
		}
		else if (!projectileInfo.InWater)
		{
			if (!enableRotation)
			{
				rigidbody.AddTorque(Vector3.Cross(myTransform.forward, rigidbody.velocity) * lookAtTorquePower);
			}
			else
			{
				rigidbody.AddTorque(rotation.dps * Time.fixedDeltaTime * myTransform.TransformDirection(rotation.rotateAxis) * rigidbody.mass * 60f);
			}
		}
		else
		{
			rigidbody.AddTorque(Vector3.Cross(myTransform.forward, rigidbody.velocity) * lookAtTorquePower * 0.1f);
		}
	}

	protected bool IgnoreOwnerCollision(Collider other)
	{
		if (other.attachedRigidbody == null)
		{
			return false;
		}
		BasicInfo component = other.attachedRigidbody.GetComponent<BasicInfo>();
		if (component == null)
		{
			return false;
		}
		if (component.infoType != BasicInfo.BasicInfoType.Block || !component.HasParentMachine)
		{
			return false;
		}
		bool flag = ownerID == component.ParentMachine.PlayerID;
		bool flag2 = (component as BlockBehaviour).Prefab.Type == BlockType.Crossbow;
		bool flag3 = projectileInfo.projectileType == NetworkProjectileType.CrossbowArrow;
		if (flag && flag2 && flag3)
		{
			return true;
		}
		int projectileBlockType = SingleInstanceFindOnly<BlockLoader>.Instance.GetProjectileBlockType((int)projectileInfo.projectileType);
		if (flag && projectileBlockType == (component as BlockBehaviour).BlockID)
		{
			return true;
		}
		return false;
	}

	public void Explode(Vector3 explosionPos, Quaternion explosionRot)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(explosionPrefab, explosionPos, explosionRot) as GameObject;
		gameObject.transform.parent = base.transform.parent;
		if (!alwaysExplodes)
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
				ProjectileManager.Instance.Despawn(projectileInfo.netProjectile, array);
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (ValidCollisionOrTrigger(collision.collider))
		{
			StartCoroutine(HandleCollisionOrTrigger(collision.collider, collision.contacts[0].normal));
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (ValidCollisionOrTrigger(other) && projectileInfo.Rigidbody.velocity.sqrMagnitude > 2f)
		{
			StartCoroutine(HandleCollisionOrTrigger(other, other.transform.position - base.transform.position));
		}
	}

	private bool ValidCollisionOrTrigger(Collider other)
	{
		if ((StatMaster.isMP && !StatMaster.isHosting && !StatMaster.isLocalSim) || other.isTrigger || hasAttached || !visObj.enabled || !StatMaster.levelSimulating)
		{
			return false;
		}
		if (IgnoreOwnerCollision(other))
		{
			return false;
		}
		ProjectileScript componentInParent = other.GetComponentInParent<ProjectileScript>();
		if (componentInParent != null && componentInParent.projectileInfo.projectileType == projectileInfo.projectileType)
		{
			return false;
		}
		return true;
	}

	public IEnumerator HandleCollisionOrTrigger(Collider other, Vector3 collisionNormal)
	{
		hasAttached = true;
		if (canAttach)
		{
			attachedRigidbody = other.attachedRigidbody;
		}
		bool parentSet = false;
		randomSoundController.Play();
		if (attachedRigidbody != null)
		{
			ForceFromHit(attachedRigidbody);
			BasicInfo bInfo = attachedRigidbody.GetComponent<BasicInfo>();
			if (!object.ReferenceEquals(bInfo, null))
			{
				if (bInfo is BlockBehaviour)
				{
					BlockBehaviour block = bInfo as BlockBehaviour;
					if (freezing && !deflectable)
					{
						FreezeAttack(block);
					}
					if (block.isSimulating && block.Prefab.hasHealthBar)
					{
						if (block.gotChildBlocks)
						{
							BlockBehaviour childBlock = block.GetChildBlockFromCollider(other);
							if (childBlock != null)
							{
								if (childBlock.Prefab.hasHealthBar)
								{
									childBlock.BlockHealth.DamageBlock(blockDamageAmount);
								}
								if (canAttach)
								{
									base.transform.SetParent(childBlock.transform, true);
									parentSet = true;
								}
							}
						}
						else
						{
							block.BlockHealth.DamageBlock(blockDamageAmount);
						}
					}
					else if (deflectable)
					{
						Deflect();
						yield break;
					}
				}
				else if (bInfo is AIGenericEntity)
				{
					KillingHandler KH = (bInfo as AIGenericEntity).aiEntity.my.killingHandler;
					if (!object.ReferenceEquals(KH, null) && !KH.my.AiCode.isDead)
					{
						if (deflectable && KH.damageAmount.projectileDeflection > UnityEngine.Random.value)
						{
							Deflect();
							yield break;
						}
						KH.TakeDamage(attackDamage, injuryType);
						if (canAttach)
						{
							projectileInfo.Rigidbody.interpolation = RigidbodyInterpolation.None;
							base.transform.SetParent(KH.my.AiCode.my.VisObject, true);
							parentSet = true;
						}
					}
				}
				else if (bInfo is EnemyAISimple)
				{
					EnemyAISimple simpleAi = bInfo as EnemyAISimple;
					if (!simpleAi.isDead)
					{
						if (deflectable && simpleAi.projectileDeflection > UnityEngine.Random.value)
						{
							Deflect();
							yield break;
						}
						simpleAi.TakeDamage(attackDamage, injuryType);
						if (canAttach)
						{
							projectileInfo.Rigidbody.interpolation = RigidbodyInterpolation.None;
							base.transform.SetParent(simpleAi.visObject, true);
							parentSet = true;
						}
					}
				}
				else if (!StatMaster.isMP)
				{
					KillingHandler KH2 = attachedRigidbody.GetComponent<KillingHandler>();
					if (!object.ReferenceEquals(KH2, null) && !KH2.my.AiCode.isDead)
					{
						if (deflectable && KH2.damageAmount.projectileDeflection > UnityEngine.Random.value)
						{
							Deflect();
							yield break;
						}
						KH2.TakeDamage(attackDamage, injuryType);
						if (canAttach)
						{
							projectileInfo.Rigidbody.interpolation = RigidbodyInterpolation.None;
							base.transform.SetParent(KH2.my.AiCode.my.VisObject, true);
							parentSet = true;
						}
					}
				}
			}
			if (!parentSet)
			{
				int mask = 32;
				foreach (IExplosionEffect e in ReferenceMaster.GetInterfaces<IExplosionEffect>(attachedRigidbody.gameObject))
				{
					if (attachedRigidbody.gameObject.activeInHierarchy && e != null)
					{
						e.OnExplode(0f, 0f, 0f, Vector3.zero, 0f, mask, projectileInfo.InWater);
					}
				}
				if (canAttach)
				{
					projectileInfo.Rigidbody.interpolation = RigidbodyInterpolation.None;
					base.transform.parent = attachedRigidbody.transform;
				}
			}
			gyro.localRotation = base.transform.localRotation;
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = new Vector3(base.transform.localScale.x / base.transform.lossyScale.x, base.transform.localScale.y / base.transform.lossyScale.y, base.transform.localScale.z / base.transform.lossyScale.z);
		}
		if (canAttach)
		{
			projectileInfo.Rigidbody.isKinematic = true;
		}
		if ((canExplode && StatMaster.GodTools.ExplodingCannonballs) || alwaysExplodes)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.05f));
			Explode(base.transform.position, Quaternion.FromToRotation(Vector3.forward, collisionNormal));
		}
		else if (despawnOnCollision)
		{
			if (StatMaster.isMP && !StatMaster.isLocalSim)
			{
				if (StatMaster.isHosting)
				{
					ProjectileManager.Instance.Despawn(projectileInfo.netProjectile, null);
				}
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (base.gameObject.activeInHierarchy && useKillTimer)
		{
			Despawn();
		}
		else
		{
			DisableProjectile();
		}
	}

	protected void Deflect()
	{
		Rigidbody rigidbody = projectileInfo.Rigidbody;
		Vector3 velocity = rigidbody.velocity;
		Vector3 velocity2 = (worldUp + velocity.normalized * -0.25f) * 10f;
		rigidbody.velocity = velocity2;
		deflected = true;
		hasAttached = false;
		if (base.gameObject.activeInHierarchy && useKillTimer)
		{
			Despawn();
		}
		else
		{
			DisableProjectile();
		}
	}

	private void FreezeAttack(BlockBehaviour hitBlock)
	{
		if (hitBlock.gotChildBlocks)
		{
			hitBlock.CreateSimLists();
			foreach (BlockBehaviour key in hitBlock.parentedColliders.Keys)
			{
				if (key.Prefab.canFreeze)
				{
					key.iceTag.Freeze();
				}
			}
		}
		if (hitBlock.Prefab.canFreeze)
		{
			hitBlock.iceTag.Freeze();
		}
	}

	private void DisableProjectile()
	{
		if (canAttach && disableCollider)
		{
			Collider[] array = cols;
			foreach (Collider collider in array)
			{
				collider.enabled = false;
			}
		}
		deflected = false;
		ClearFire();
		base.enabled = false;
	}

	private void Despawn(bool disableCols = true)
	{
		if (despawning)
		{
			return;
		}
		despawning = true;
		if (disableCols && canAttach && disableCollider)
		{
			Collider[] array = cols;
			foreach (Collider collider in array)
			{
				collider.enabled = false;
			}
		}
		StartCoroutine(DespawnTimer());
	}

	private IEnumerator DespawnTimer()
	{
		yield return new WaitForSeconds(killTimer);
		if (StatMaster.isHosting && !StatMaster.isLocalSim)
		{
			NetworkProjectile netProjectile = projectileInfo.netProjectile;
			ProjectileManager.Instance.Despawn(netProjectile);
		}
		else
		{
			if (destroyOnDespawn)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				yield break;
			}
			base.gameObject.SetActive(false);
			projectileInfo.Rigidbody.isKinematic = true;
		}
		deflected = false;
		ClearFire();
		hasAttached = false;
	}

	private void ClearFire()
	{
		if (!object.ReferenceEquals(firecontrol, null) && firecontrol.onFire && firecontrol.gameObject.activeInHierarchy)
		{
			firecontrol.ImmediateStop();
			firecontrol.currentFireDuration = 0f;
			if (firecontrol.hasStartingAtributes)
			{
				firecontrol.SetBurnedLevel(0f);
			}
		}
	}

	protected void ForceFromHit(Rigidbody target)
	{
		target.AddForce(projectileInfo.Rigidbody.velocity * impactForceMultiplier);
	}

	private void OnDestroy()
	{
		if (StatMaster.isHosting && !StatMaster.isLocalSim)
		{
			NetworkProjectile netProjectile = projectileInfo.netProjectile;
			ProjectileManager.Instance.Despawn(netProjectile);
		}
	}

	public void SetScale(Vector3 scale)
	{
		gyro.localScale = scale;
	}
}
