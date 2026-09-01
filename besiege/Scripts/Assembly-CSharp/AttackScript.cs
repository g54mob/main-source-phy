using System;
using UnityEngine;

[AddComponentMenu("AI/AttackScript")]
public class AttackScript : MonoBehaviour
{
	public enum AttackMethod
	{
		Ray = 0,
		Trigger = 1,
		Capsule = 2
	}

	[Serializable]
	public class Range
	{
		public Transform projectileSpawnPos;

		public GameObject projectile;

		public NetworkProjectileType networkProjectileType;

		public float shootingForce = 20f;

		public float angleScale = 1f;

		public float minAngle = 45f;

		public float maxAngle = 75f;

		public float randomAimAmount = 0.01f;

		public bool prediction;

		public float predictionScalar = 0.5f;

		public float extraProjectiles = 4f;

		public float maxAngleCos = 0.4f;

		[HideInInspector]
		public float physG;

		[HideInInspector]
		public Vector3 projectileScale;

		[HideInInspector]
		public int poolAmount = 4;

		public void Init()
		{
			physG = Physics.gravity.magnitude;
		}
	}

	[Serializable]
	public class Projectile
	{
		public Rigidbody rigidbody;

		public ProjectileScript projectileScript;

		public Transform transform;

		public GameObject gameObject;

		public Collider collider;

		public Transform gyro;

		public Projectile(GameObject p)
		{
			NewProjectile(p);
		}

		public void NewProjectile(GameObject p)
		{
			gameObject = p;
			rigidbody = gameObject.GetComponent<Rigidbody>();
			projectileScript = gameObject.GetComponent<ProjectileScript>();
			transform = gameObject.transform;
			collider = gameObject.GetComponent<Collider>() ?? projectileScript.col;
			gyro = transform.GetChild(0).transform;
		}
	}

	public RandomSoundController randomSoundController;

	public Vector3 attackOffsetLocal = Vector3.zero;

	public float meleeAttackRange = 7f;

	public float pctDecreaseLookingUp = 1f;

	public float rangeExponent = 2f;

	public bool multiTargeting;

	public bool ignoreArmor;

	public float jointDamage = 0.75f;

	public AttackMethod attackMethod;

	public LayerMask raycastLayer;

	public AttackColliderList attackColliderList;

	public float blockDamageAmount = 1f;

	public float attackDamage = 30f;

	public float attackDelay = 1f;

	public ParticleSystem[] hitParticle;

	public ParticleSystem[] hitParticleBlock;

	public ParticleSystem[] hitParticleAI;

	public EntityAI aiCode;

	public float randomAttackTime = 0.5f;

	public float impactForceAddition = 10f;

	public float extraForcePerJoint = 300f;

	public InjuryType injuryType = InjuryType.Sharp;

	public SetPoseForAI Setpos;

	private float meleeAttackRangeExp;

	private float scaledMeleeAttackRange;

	private Vector3 attackPos = Vector3.zero;

	public bool freezingAttacks;

	private LevelEntity levelEntity;

	private bool isOfflineMode;

	private ushort playerID;

	public bool ranged;

	public Range range = new Range();

	private Projectile[] projectileList;

	private Projectile currentProjectile;

	private float attackTimer;

	private RaycastHit hit;

	public float attackBlockVolume = 0.1f;

	protected void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		isOfflineMode = !StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim;
		playerID = (ushort)(StatMaster.isMP ? BesiegeNetworkManager.Instance.PlayerID : 0);
		if (randomSoundController == null)
		{
			randomSoundController = GetComponent<RandomSoundController>();
		}
		if (aiCode == null)
		{
			aiCode = GetComponent<EntityAI>();
		}
		levelEntity = aiCode.levelEntity;
		attackTimer += UnityEngine.Random.Range(0f, attackDelay);
		if (ranged)
		{
			if (isOfflineMode)
			{
				range.poolAmount = (int)(attackDelay * 60f / (range.projectile.GetComponent<ProjectileScript>().killTimer * 60f) + range.extraProjectiles);
				projectileList = new Projectile[range.poolAmount];
				for (int i = 0; i < projectileList.Length; i++)
				{
					GameObject p = UnityEngine.Object.Instantiate(range.projectile, range.projectileSpawnPos.position, range.projectileSpawnPos.rotation) as GameObject;
					projectileList[i] = new Projectile(p);
					Projectile projectile = projectileList[i];
					projectile.transform.parent = ReferenceMaster.physicsGoalInstance;
					projectile.projectileScript.attackDamage = attackDamage;
					projectile.projectileScript.blockDamageAmount = blockDamageAmount;
					projectile.projectileScript.impactForceMultiplier = impactForceAddition;
					projectile.gameObject.SetActive(false);
				}
			}
			range.projectileScale = aiCode.transform.localScale;
			range.Init();
		}
		if (multiTargeting && attackColliderList == null)
		{
			multiTargeting = false;
		}
		Vector3 localScale = base.transform.localScale;
		float num = ((!(localScale.x > localScale.z)) ? localScale.z : localScale.x);
		num = ((!(num > localScale.y)) ? localScale.y : num);
		scaledMeleeAttackRange = meleeAttackRange * num;
		meleeAttackRangeExp = scaledMeleeAttackRange * scaledMeleeAttackRange;
		if (attackColliderList != null)
		{
			attackMethod = AttackMethod.Trigger;
		}
	}

	public void Attack(EntityAI.Targeting target, float dist)
	{
		if (dist > aiCode.BehavioursMaxDistance)
		{
			return;
		}
		if (attackTimer > attackDelay)
		{
			Vector3 targetPosition = target.GetTargetPosition();
			if (ranged)
			{
				if (Mathf.Abs(targetPosition.y - base.transform.position.y) > aiCode.disposition.currentBehaviour.Radius || Vector3.Dot((targetPosition - base.transform.position).normalized, Vector3.down) > range.maxAngleCos)
				{
					return;
				}
				attackTimer = 0f;
				attackTimer -= UnityEngine.Random.Range(0f - randomAttackTime, randomAttackTime);
				if ((bool)Setpos)
				{
					if (!Setpos.StopScript)
					{
						StartCoroutine(Setpos.AttackAnim(target, dist, RangedAttack));
					}
				}
				else
				{
					RangedAttack(target);
				}
				return;
			}
			attackPos = base.transform.position + base.transform.rotation * attackOffsetLocal;
			Vector3 rhs = targetPosition - attackPos;
			float num = 1f - Mathf.Abs(Vector3.Dot(base.transform.up, rhs));
			if (rangeExponent != 1f)
			{
				num = Mathf.Pow(num, rangeExponent);
			}
			float num2 = meleeAttackRangeExp - meleeAttackRangeExp * (1f - num) * pctDecreaseLookingUp;
			if (rhs.sqrMagnitude > num2)
			{
				return;
			}
			attackTimer = 0f;
			attackTimer -= UnityEngine.Random.Range(0f - randomAttackTime, randomAttackTime);
			switch (attackMethod)
			{
			case AttackMethod.Trigger:
			{
				if (object.ReferenceEquals(attackColliderList, null))
				{
					break;
				}
				for (int i = 0; i < attackColliderList.attackColliderTargets.Count; i++)
				{
					EntityAI.Targeting targeting = attackColliderList.attackColliderTargets.Values[i];
					if (object.ReferenceEquals(targeting.trans, null))
					{
						attackColliderList.attackColliderTargets.RemoveAt(i);
						i--;
					}
					else if (targeting.isAI && targeting.AI.isDead)
					{
						attackColliderList.attackColliderTargets.RemoveAt(i);
						i--;
					}
					else if (targeting.isBlock && !object.ReferenceEquals(targeting.BlockHealth, null))
					{
						if (targeting.BlockHealth.health <= 0f)
						{
							attackColliderList.attackColliderTargets.RemoveAt(i);
							i--;
						}
						else if (targeting.Block.IsDestroyed)
						{
							attackColliderList.attackColliderTargets.RemoveAt(i);
							i--;
						}
					}
				}
				if (attackColliderList.attackColliderTargets.Count == 0)
				{
					break;
				}
				PlaySwingParticles();
				if ((bool)Setpos)
				{
					if (Setpos.AttackingPoses.Length > 1)
					{
						StartCoroutine(Setpos.AttackAnim(target, dist, TriggerAttack));
						break;
					}
					Setpos.AttackPose();
					TriggerAttack(target, dist);
				}
				else
				{
					TriggerAttack(target, dist);
				}
				break;
			}
			case AttackMethod.Capsule:
				PlaySwingParticles();
				if ((bool)Setpos)
				{
					if (Setpos.AttackingPoses.Length > 1)
					{
						StartCoroutine(Setpos.AttackAnim(target, dist, CapsuleAttack));
						break;
					}
					Setpos.AttackPose();
					CapsuleAttack(target, dist);
				}
				else
				{
					CapsuleAttack(target, dist);
				}
				break;
			case AttackMethod.Ray:
				PlaySwingParticles();
				if ((bool)Setpos)
				{
					if (Setpos.AttackingPoses.Length > 1)
					{
						StartCoroutine(Setpos.AttackAnim(target, dist, RayAttack));
						break;
					}
					Setpos.AttackPose();
					RayAttack(target, dist);
				}
				else
				{
					RayAttack(target, dist);
				}
				break;
			}
		}
		else
		{
			attackTimer += Time.deltaTime;
		}
	}

	private void TriggerAttack(EntityAI.Targeting target, float dist)
	{
		for (int i = 0; i < attackColliderList.attackColliderTargets.Count; i++)
		{
			EntityAI.Targeting targeting = attackColliderList.attackColliderTargets.Values[i];
			if (targeting.isAI)
			{
				MeleeAttack(targeting);
				if (!multiTargeting)
				{
					break;
				}
			}
			else
			{
				if (!targeting.isBlock)
				{
					continue;
				}
				if (multiTargeting)
				{
					if (targeting.isArmored && !ignoreArmor)
					{
						ForceFromHit(targeting);
						break;
					}
					MeleeAttack(targeting);
					continue;
				}
				if (targeting.isArmored && !ignoreArmor)
				{
					ForceFromHit(targeting);
					break;
				}
				if (target.trans == targeting.trans)
				{
					MeleeAttack(target);
				}
			}
		}
	}

	private void CapsuleAttack(EntityAI.Targeting target, float dist)
	{
		float radius = 0.75f;
		RaycastHit[] array = Physics.SphereCastAll(attackPos, radius, target.GetTargetPosition() - attackPos, scaledMeleeAttackRange, raycastLayer, QueryTriggerInteraction.Ignore);
		bool flag = false;
		if (array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				MeleeAttackFromRay(target, array[i], dist, flag);
				if (!flag)
				{
					flag = !ignoreArmor && target.isBlock && (target.Block.Prefab.isArmor || array[i].collider.gameObject.CompareTag("ArmourTag"));
				}
			}
		}
		else
		{
			randomSoundController.Play2(attackBlockVolume * 0.2f);
		}
	}

	private void RayAttack(EntityAI.Targeting target, float dist)
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(attackPos, target.GetTargetPosition() - attackPos, out hitInfo, scaledMeleeAttackRange))
		{
			MeleeAttackFromRay(target, hitInfo, dist);
		}
		else
		{
			randomSoundController.Play2(attackBlockVolume * 0.2f);
		}
	}

	public void PlaySwingParticles()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.AttackSwingParticles);
		}
		for (int i = 0; i < hitParticle.Length; i++)
		{
			if (hitParticle[i].isPlaying)
			{
				hitParticle[i].Stop();
			}
		}
		for (int j = 0; j < hitParticle.Length; j++)
		{
			if (!hitParticle[j].isPlaying)
			{
				hitParticle[j].Play();
			}
		}
	}

	private void MeleeAttackFromRay(EntityAI.Targeting target, RaycastHit hit, float distanceToTargetBlock, bool hitArmor = false)
	{
		Rigidbody rigidbody = hit.rigidbody;
		if (rigidbody == null)
		{
			randomSoundController.Play2(attackBlockVolume * 0.2f);
			return;
		}
		if (rigidbody != target.Rigidbody)
		{
			target = new EntityAI.Targeting(aiCode);
			target.NewTargetBlock(hit.collider.transform, rigidbody);
		}
		if (!target.isAI || target.AI.faction != aiCode.faction)
		{
			if (!ignoreArmor && (hitArmor || (target.isBlock && target.Block.Prefab.isArmor) || hit.collider.gameObject.CompareTag("ArmourTag") || CloserArmourPiece(target, hit, distanceToTargetBlock)))
			{
				ForceFromHit(target);
			}
			else
			{
				MeleeAttack(target);
			}
		}
	}

	public void MeleeAttack(EntityAI.Targeting target)
	{
		ForceFromHit(target);
		randomSoundController.SetMixer(aiCode.my.basicInfo.submergedPercent > 0.9f);
		if (target.isAI)
		{
			PlayHitParticles(AITargetType.Ai);
			target.AI.my.killingHandler.TakeDamage(attackDamage, injuryType);
			randomSoundController.Play();
		}
		else if (!object.ReferenceEquals(target.BlockHealth, null))
		{
			FreezeAttack(target);
			PlayHitParticles(AITargetType.Block);
			target.BlockHealth.DamageBlock(blockDamageAmount);
			randomSoundController.Play2(attackBlockVolume);
		}
		else
		{
			randomSoundController.Play2(attackBlockVolume * 0.2f);
		}
	}

	private void ForceFromHit(EntityAI.Targeting target)
	{
		if ((impactForceAddition < 5f && impactForceAddition > -5f) || !target.Rigidbody || (target.isBlock && target.Block.IsDestroyed) || target.Rigidbody.isKinematic)
		{
			return;
		}
		Vector3 vector = target.GetTargetPosition() - attackPos;
		float num = 0f;
		if (target.isBlock)
		{
			target.Block.CreateSimLists();
			int num2 = target.Block.jointsToMe.Count + target.Block.iJointTo.Count;
			if (target.isBlock && num2 > 0)
			{
				num += (float)num2 * extraForcePerJoint;
			}
		}
		target.Rigidbody.AddForce(vector.normalized * (impactForceAddition + num));
	}

	private bool CloserArmourPiece(EntityAI.Targeting target, RaycastHit hit, float distanceToTargetBlock)
	{
		if (!target.isBlock || !target.Block.gotChildBlocks || attackMethod != AttackMethod.Ray)
		{
			return false;
		}
		foreach (BlockBehaviour key in target.Block.parentedColliders.Keys)
		{
			if (key.IsArmor)
			{
				float sqrMagnitude = (key.GetCenter() - attackPos).sqrMagnitude;
				if (sqrMagnitude < distanceToTargetBlock)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void FreezeAttack(EntityAI.Targeting target)
	{
		if (!freezingAttacks)
		{
			return;
		}
		if (target.Block.gotChildBlocks)
		{
			target.Block.CreateSimLists();
			foreach (BlockBehaviour key in target.Block.parentedColliders.Keys)
			{
				if (key.Prefab.canFreeze)
				{
					key.iceTag.Freeze();
				}
			}
		}
		if (target.Block.Prefab.canFreeze)
		{
			target.Block.iceTag.Freeze();
		}
	}

	public void PlayHitParticles(AITargetType targetType)
	{
		if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.AttackHitParticles, (byte)targetType);
		}
		switch (targetType)
		{
		case AITargetType.Ai:
		{
			for (int j = 0; j < hitParticleAI.Length; j++)
			{
				hitParticleAI[j].Play();
			}
			break;
		}
		case AITargetType.Block:
		{
			for (int i = 0; i < hitParticleBlock.Length; i++)
			{
				hitParticleBlock[i].Play();
			}
			break;
		}
		}
	}

	private void RangedAttack(EntityAI.Targeting target)
	{
		RangedAttack(target, 0f);
	}

	private void RangedAttack(EntityAI.Targeting target, float dist)
	{
		if ((bool)target.trans && (target.isAI || target.isBlock))
		{
			Vector3 randomPos = UnityEngine.Random.insideUnitSphere * range.randomAimAmount;
			randomPos.y = 0f;
			randomPos += target.GetTargetPosition();
			if (range.prediction && (bool)target.Rigidbody)
			{
				randomPos += target.Rigidbody.velocity * range.predictionScalar;
			}
			RangedAttack(randomPos);
		}
	}

	private void RangedAttack(Vector3 randomPos)
	{
		float num = ElevationAngle(randomPos);
		float num2 = num + 40f;
		num2 = Mathf.Clamp(num2 * range.angleScale, range.minAngle, range.maxAngle);
		currentProjectile = GetProjectile();
		if (!object.ReferenceEquals(currentProjectile, null))
		{
			randomSoundController.SetMixer(aiCode.my.basicInfo.submergedPercent > 0.9f);
			randomSoundController.Play2(0.2f);
			Physics.IgnoreCollision(currentProjectile.collider, aiCode.my.Collider);
			if (aiCode.AllowedToModifyConstraints && aiCode.grounded)
			{
				aiCode.my.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			}
			currentProjectile.rigidbody.AddForce(BallisticVel(randomPos, num2) * range.shootingForce);
			currentProjectile.projectileScript.freezing = freezingAttacks;
		}
		if (aiCode.my.Rigidbody.constraints == RigidbodyConstraints.FreezeAll)
		{
			aiCode.my.Rigidbody.constraints = (RigidbodyConstraints)80;
		}
	}

	private Projectile GetProjectile()
	{
		if (StatMaster.levelSimulating && StatMaster.isHosting && !StatMaster.isLocalSim)
		{
			byte[] array = new byte[19];
			int num = 0;
			NetworkCompression.CompressPosition(range.projectileSpawnPos.position, array, num);
			num += 6;
			NetworkCompression.CompressRotation(range.projectileSpawnPos.rotation, array, num);
			num += 7;
			NetworkCompression.CompressVector(range.projectileScale, 0f, 100f, array, num);
			Transform transform = ProjectileManager.Instance.Spawn(range.networkProjectileType, 0u, playerID, array);
			Projectile projectile = new Projectile(transform.gameObject);
			projectile.projectileScript.attackDamage = attackDamage;
			projectile.projectileScript.blockDamageAmount = blockDamageAmount;
			projectile.projectileScript.impactForceMultiplier = impactForceAddition;
			projectile.projectileScript.SetScale(range.projectileScale);
			return projectile;
		}
		if (isOfflineMode)
		{
			for (int i = 0; i < projectileList.Length; i++)
			{
				Projectile projectile = projectileList[i];
				if ((bool)projectile.gameObject)
				{
					if (!projectile.gameObject.activeInHierarchy)
					{
						projectile.transform.parent = ReferenceMaster.physicsGoalInstance;
						projectile.transform.position = range.projectileSpawnPos.position;
						projectile.transform.rotation = range.projectileSpawnPos.rotation;
						projectile.projectileScript.SetScale(range.projectileScale);
						projectile.gameObject.SetActive(true);
						projectile.projectileScript.hasAttached = false;
						projectile.rigidbody.isKinematic = false;
						if ((bool)projectile.gyro)
						{
							projectile.gyro.localRotation = Quaternion.identity;
						}
						return projectile;
					}
				}
				else
				{
					GameObject p = UnityEngine.Object.Instantiate(range.projectile, range.projectileSpawnPos.position, range.projectileSpawnPos.rotation) as GameObject;
					projectile.NewProjectile(p);
					projectile.transform.parent = ReferenceMaster.physicsGoalInstance;
					projectile.projectileScript.attackDamage = attackDamage;
					projectile.projectileScript.blockDamageAmount = blockDamageAmount;
					projectile.projectileScript.impactForceMultiplier = impactForceAddition;
					projectile.gameObject.SetActive(false);
					i--;
				}
			}
		}
		return null;
	}

	private Vector3 BallisticVel(Vector3 target, float angle)
	{
		if (range.physG == 0f)
		{
			range.physG = Physics.gravity.magnitude;
		}
		Vector3 vector = target - currentProjectile.transform.position;
		float num = vector.y * 1.5f;
		vector.y = 0f;
		float magnitude = vector.magnitude;
		float num2 = angle * ((float)Math.PI / 180f);
		float num3 = Mathf.Tan(num2);
		vector.y = magnitude * num3;
		if (num <= 0f)
		{
			float num4 = num / 10f * 0.05f * -1f + 1.65f;
			num4 = ((num4 > 10f) ? 10f : ((!(num4 < 1f)) ? num4 : 1.000001f));
			float num5 = ((!(magnitude < 30f)) ? (Mathf.Log(magnitude, num4) / 10f) : 0.4f);
			magnitude += num * num5;
		}
		else
		{
			magnitude += num / num3;
		}
		float num6 = Mathf.Sqrt(magnitude * range.physG / Mathf.Sin(2f * num2));
		if (float.IsNaN(num6))
		{
			num6 = 10f;
		}
		return num6 * vector.normalized;
	}

	private float ElevationAngle(Vector3 target)
	{
		Vector3 vector = target - base.transform.position;
		float num = Vector3.Angle(to: new Vector3(vector.x, 0f, vector.z), from: vector);
		if (vector.y < 0f)
		{
			num = 0f - num;
		}
		return num;
	}

	public void ClearHiddenProjectiles()
	{
		if (!isOfflineMode)
		{
			return;
		}
		if (ranged)
		{
			for (int num = projectileList.Length - 1; num >= 0; num--)
			{
				if (projectileList[num] != null && (bool)projectileList[num].gameObject && !projectileList[num].gameObject.activeInHierarchy)
				{
					UnityEngine.Object.Destroy(projectileList[num].gameObject);
				}
			}
		}
		projectileList = new Projectile[0];
	}
}
