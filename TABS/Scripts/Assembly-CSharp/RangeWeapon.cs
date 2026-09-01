using System;
using System.Collections;
using BitCode.Extensions;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.RuntimeCleanup;
using Photon.Bolt;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RangeWeapon : Weapon, IValidatable
{
	public enum SpawnPosition
	{
		ShootPosition = 0,
		TargetPosition = 1,
		groundPosition = 2,
		ownTorso = 3
	}

	public enum SpawnRotation
	{
		TowardsTarget = 0,
		Up = 1,
		identity = 2,
		TowardsTargetWithoutY = 3,
		CharacterForward = 4
	}

	public enum RandomType
	{
		OnSphereRandom = 0,
		InSphereRandom = 1
	}

	public enum SoundTiming
	{
		Shoot = 0,
		AttackCall = 1
	}

	[SerializeField]
	private GameObject objectToSpawn;

	[SerializeField]
	private bool spawnedObjectsInheritLevelScriptValues;

	[Tooltip("Indicates if the weapons projectiles should be automatically pooled on LowEnd platforms.")]
	[SerializeField]
	private bool poolLowEndProjectiles;

	[Tooltip("The initial number of copies of the projectile to be added to the pool when created.")]
	[FormerlySerializedAs("pooledInstanceCount")]
	[SerializeField]
	private int numberOfInitialInstanceToAddToPool = 3;

	public bool rememberUnitThatSpawnedObject;

	public UnitBlueprint unitToSpawn;

	public float spawnDelay;

	public bool useSpawnDelayForEveryShot;

	public int magSize = 1;

	public float reloadTime;

	public int numberOfObjects = 1;

	public int maxProjectilesPerFrane;

	public bool reTargetPerShot;

	public float delayPerSpawn = 0.02f;

	public bool useRandomDelay;

	public float minRandom = 0.8f;

	public float maxRandom = 1.2f;

	public float shootRecoil;

	public float torsoRecoil;

	public float extraSpreadInMelee;

	public float extraCDInMelee;

	public float spread;

	public float projectileSpeedSpread;

	public float force;

	public float fallTime;

	public AnimationCurve shootHelpAngleCurve;

	private Level myLevel;

	public SpawnPosition spawnPosition;

	public SpawnRotation spawnRotation;

	public Vector3 positionOffset;

	public float randomPosition;

	public float characterForwardOffset;

	public float randomX = 1f;

	public float randomY = 1f;

	public float randomZ = 1f;

	public RandomType randomType;

	private Action shootAction;

	public float clampRange = float.PositiveInfinity;

	public bool parentToMe;

	public bool willCopy;

	private Transform shootPosition;

	private Rigidbody rig;

	public UnityEvent getAttackCallEvent;

	public UnityEvent shootEvent;

	public UnityEvent shootEndEvent;

	public UnityEvent lastShotEvent;

	[Space(30f)]
	[SerializeField]
	private string soundRef;

	[SerializeField]
	private AudioPathData soundPathData;

	private SoundPlayer soundPlayer;

	public SoundTiming soundTiming;

	public float soundDelay;

	[HideInInspector]
	public bool allowedToFire = true;

	private Action attackCallRecievedAction;

	private RuntimeGarbageCollector m_runtimeGC;

	private ProjectilesSpawnManager m_spawnManager;

	private FPSWeapon fpsWeapon;

	private int ammo;

	private float spawnDelayCounter;

	private bool reloading;

	public float charge = 1f;

	public GameObject ObjectToSpawn
	{
		get
		{
			return objectToSpawn;
		}
		set
		{
			objectToSpawn = value;
		}
	}

	public bool PoolLowEndProjectiles => poolLowEndProjectiles;

	public int NumberOfInitialInstanceToAddToPool => numberOfInitialInstanceToAddToPool;

	public string SoundRef
	{
		get
		{
			return soundRef;
		}
		set
		{
			soundRef = value;
			AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
		}
	}

	public bool Validate()
	{
		return AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
	}

	protected override void Start()
	{
		soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		ammo = magSize;
		myLevel = GetComponent<Level>();
		base.Start();
		shootPosition = GetComponentInChildren<ShootPosition>().transform;
		rig = GetComponentInParent<Rigidbody>();
		attackEffects = GetComponents<AttackEffect>();
		m_runtimeGC = ServiceLocator.GetService<RuntimeGarbageCollector>();
		fpsWeapon = GetComponent<FPSWeapon>();
		bool flag = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
		if (Bugs._DLC_ACTIVATED && !flag && ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("BUG_ATTACK_SPEED").currentValue == 1)
		{
			internalCooldown /= 30f;
		}
		m_spawnManager = ServiceLocator.GetService<ProjectilesSpawnManager>();
		if (rememberUnitThatSpawnedObject && ObjectToSpawn != null)
		{
			ProjectileHitAddEffect component = objectToSpawn.GetComponent<ProjectileHitAddEffect>();
			if ((bool)component)
			{
				component.unitThatSpawnedMe = base.transform.root.GetComponent<Unit>();
			}
		}
	}

	private bool IsPoolable(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		if (objectToSpawn.GetComponent<PooledProjectile>() == null)
		{
			return false;
		}
		return true;
	}

	public override void Attack(Vector3 position, Rigidbody targetRig, Vector3 forcedDirection, bool recursive = false)
	{
		if (ammo <= 0 && magSize > 1)
		{
			if (!reloading)
			{
				if (reloadTime == 0f)
				{
					ammo = magSize;
				}
				else
				{
					Reload();
				}
			}
			return;
		}
		attackCallRecievedAction?.Invoke();
		if (!allowedToFire)
		{
			return;
		}
		if (!recursive)
		{
			CheckIfAditionalAttacksAreNeeded(position, targetRig);
		}
		if (callAttackEffects)
		{
			for (int i = 0; i < attackEffects.Length; i++)
			{
				attackEffects[i].DoEffect(targetRig, forcedDirection);
			}
		}
		StartCoroutine(DelayShoot(position, targetRig, forcedDirection));
	}

	private void CheckIfAditionalAttacksAreNeeded(Vector3 position, Rigidbody targetRig)
	{
		if (internalCooldown < 0.034f)
		{
			StartCoroutine(DelayAttackCall(position, targetRig, UnityEngine.Random.Range(0.03f, 0.06f)));
			StartCoroutine(DelayAttackCall(position, targetRig, UnityEngine.Random.Range(0.06f, 0.09f)));
			StartCoroutine(DelayAttackCall(position, targetRig, UnityEngine.Random.Range(0.09f, 0.12f)));
		}
		else if (internalCooldown < 0.051f)
		{
			StartCoroutine(DelayAttackCall(position, targetRig, UnityEngine.Random.Range(0.06f, 0.9f)));
			StartCoroutine(DelayAttackCall(position, targetRig, UnityEngine.Random.Range(0.09f, 0.12f)));
		}
	}

	public int GetAmmo()
	{
		return ammo;
	}

	private IEnumerator DelayAttackCall(Vector3 position, Rigidbody targetRig, float delay)
	{
		float c = 0f;
		while (c < delay)
		{
			c += Time.deltaTime * attackSpeedM;
			yield return null;
		}
		Attack(position, targetRig, Vector3.zero, recursive: true);
	}

	private IEnumerator PlaySound()
	{
		float c = 0f;
		while (c < soundDelay)
		{
			c += Time.deltaTime * attackSpeedM;
			yield return null;
		}
		if (soundRef != string.Empty)
		{
			soundPlayer.PlaySoundEffectNonAlloc(soundPathData, 1f, base.transform.position);
		}
	}

	private IEnumerator DelayShoot(Vector3 position, Rigidbody targetRig, Vector3 forcedDirection)
	{
		bool flag = base.connectedData != null && base.connectedData.input != null && base.connectedData.input.IsRemotelyControlled;
		bool canSpawnObjects = !flag;
		if (soundTiming == SoundTiming.AttackCall)
		{
			StartCoroutine(PlaySound());
		}
		getAttackCallEvent.Invoke();
		while (spawnDelayCounter < spawnDelay)
		{
			spawnDelayCounter += Time.deltaTime * attackSpeedM;
			yield return null;
		}
		if (magSize > 1 && !useSpawnDelayForEveryShot)
		{
			ammo--;
		}
		else if (magSize > 1 && useSpawnDelayForEveryShot)
		{
			ammo--;
			spawnDelayCounter = 0f;
		}
		else
		{
			spawnDelayCounter = 0f;
		}
		if ((bool)base.connectedData && (bool)base.connectedData.targetMainRig && base.connectedData.unit.api.Active)
		{
			targetRig = base.connectedData.targetMainRig;
		}
		Vector3 vector = Vector3.zero;
		if ((bool)targetRig)
		{
			vector = targetRig.velocity;
		}
		vector.y = 0f;
		Vector3 directionToTarget = shootPosition.transform.forward;
		if ((bool)targetRig)
		{
			directionToTarget = (targetRig.position - shootPosition.position).normalized;
		}
		bool isBurst = false;
		int spawnedProjectilesThisFrame = 0;
		for (int i = 0; i < numberOfObjects; i++)
		{
			if ((!fpsWeapon || !fpsWeapon.isHeld) && (!base.connectedData || !base.connectedData.mainRig || !shootPosition || base.connectedData.Dead))
			{
				continue;
			}
			if (reTargetPerShot && (bool)base.connectedData && (bool)base.connectedData.mainRig)
			{
				targetRig = base.connectedData.targetMainRig;
			}
			if ((bool)targetRig)
			{
				directionToTarget = (targetRig.position - shootPosition.position).normalized;
			}
			if (clampRange != float.PositiveInfinity)
			{
				directionToTarget = Vector3.ClampMagnitude(directionToTarget, clampRange);
			}
			shootEvent.Invoke();
			Vector3 vector2 = GetSpawnPosition(targetRig);
			Vector3 spawnDirection = GetSpawnDirection(directionToTarget, targetRig, forcedDirection);
			if (ammo == 0)
			{
				lastShotEvent.Invoke();
			}
			GameObject gameObject = null;
			Projectile projectile = null;
			if ((bool)objectToSpawn && canSpawnObjects && m_spawnManager != null)
			{
				gameObject = m_spawnManager.SpawnProjectile(objectToSpawn, vector2, Quaternion.LookRotation(spawnDirection + 0.01f * spread * UnityEngine.Random.insideUnitSphere), (base.connectedData != null) ? base.connectedData.unit : null, base.WeaponIndex, spawnDirection, directionToTarget, targetRig, shootPosition.forward, out projectile);
				Level component = GetComponent<Level>();
				if (spawnedObjectsInheritLevelScriptValues && (bool)component)
				{
					Level orAddComponent = gameObject.GetOrAddComponent<Level>();
					orAddComponent.levelMultiplier = component.levelMultiplier;
					orAddComponent.level = component.level;
					orAddComponent.ignoreTeam = component.ignoreTeam;
				}
				if (parentToMe && gameObject != null)
				{
					gameObject.transform.SetParent(base.transform, worldPositionStays: true);
				}
				SetTeamHolder(gameObject, targetRig);
			}
			if ((bool)unitToSpawn && canSpawnObjects)
			{
				Vector3 forward = shootPosition.forward;
				forward.y = 0f;
				Team team = ((!holdable) ? base.transform.root.GetComponent<Unit>().Team : holdable.holderData.GetComponentInParent<Unit>().Team);
				gameObject = unitToSpawn.Spawn(shootPosition.position, Quaternion.LookRotation(forward), team)[0];
				m_runtimeGC.AddGameObject(gameObject);
				if (fallTime != 0f)
				{
					gameObject.GetComponent<Unit>().data.fallTime = fallTime;
				}
			}
			if (gameObject != null)
			{
				byte? randomSeed = ((projectile != null) ? projectile.RandomSeed : ((byte?)null));
				SetProjectileStats(gameObject, spawnDirection, directionToTarget, targetRig, shootPosition.forward, (targetRig != null) ? targetRig.position : Vector3.zero, (targetRig != null) ? targetRig.velocity : Vector3.zero, randomSeed);
			}
			if (willCopy && (bool)gameObject)
			{
				gameObject.AddComponent<ProjectileCopy>().selfCopy = GetComponent<MeleeWeaponCopySelf>();
			}
			if (i == numberOfObjects - 1)
			{
				shootEndEvent.Invoke();
			}
			SetTargetableEffects(gameObject, vector2, targetRig);
			if ((bool)rig && shootRecoil != 0f)
			{
				rig.AddForce(-shootPosition.forward * (FixedTimeStepService.SmallForceCoefficient * shootRecoil / attackSpeedM), ForceMode.VelocityChange);
			}
			if ((bool)holdable && holdable.holderData != null && torsoRecoil != 0f)
			{
				holdable.holderData.mainRig.AddForce(-shootPosition.forward * (FixedTimeStepService.SmallForceCoefficient * torsoRecoil / attackSpeedM), ForceMode.VelocityChange);
			}
			spawnedProjectilesThisFrame++;
			if (maxProjectilesPerFrane != 0 && spawnedProjectilesThisFrame >= maxProjectilesPerFrane)
			{
				isBurst = true;
				if (soundTiming == SoundTiming.Shoot)
				{
					StartCoroutine(PlaySound());
				}
				if (delayPerSpawn > 0f)
				{
					float seconds = ((!useRandomDelay) ? delayPerSpawn : UnityEngine.Random.Range(delayPerSpawn * minRandom, delayPerSpawn * maxRandom));
					spawnedProjectilesThisFrame = 0;
					yield return new WaitForSeconds(seconds);
				}
			}
		}
		if (!isBurst)
		{
			StartCoroutine(PlaySound());
		}
		if (shootAction != null)
		{
			shootAction();
		}
	}

	private IEnumerator ReloadCounter()
	{
		reloading = true;
		yield return new WaitForSeconds(reloadTime);
		reloading = false;
		ammo = magSize;
		internalCounter = 10000f;
	}

	public void Reload()
	{
		StartCoroutine(ReloadCounter());
	}

	public void SetTeamHolder(GameObject spawnedObject, Rigidbody targetRig)
	{
		if ((bool)base.connectedData)
		{
			TeamHolder orAddComponent = spawnedObject.GetOrAddComponent<TeamHolder>();
			orAddComponent.team = base.connectedData.team;
			orAddComponent.spawner = base.transform.root.gameObject;
			if ((bool)shootPosition)
			{
				orAddComponent.spawnerWeapon = shootPosition.gameObject;
			}
			orAddComponent.target = targetRig;
		}
	}

	public void SetProjectileStats(GameObject spawnedObject, Vector3 spawnDir, Vector3 directionToTarget, Rigidbody targetRig, Vector3 shootPositionForward, Vector3 targetRigPosition, Vector3 targetRigVelocity, byte? randomSeed = null)
	{
		Transform[] componentsInChildren = spawnedObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (force != 0f)
			{
				Rigidbody component = componentsInChildren[i].GetComponent<Rigidbody>();
				if ((bool)component)
				{
					component.AddForce(spawnDir * force, ForceMode.VelocityChange);
					if ((bool)myLevel)
					{
						component.mass *= Mathf.Pow(myLevel.level, 1.5f);
					}
				}
			}
			Compensation componentInChildren = componentsInChildren[i].GetComponentInChildren<Compensation>();
			if ((bool)componentInChildren && (bool)targetRig)
			{
				if (randomSeed.HasValue)
				{
					SetRandomSeed(randomSeed.Value);
					randomSeed++;
				}
				componentInChildren.transform.rotation = Quaternion.LookRotation(componentInChildren.GetCompensation(targetRigPosition, targetRigVelocity, shootHelpAngleCurve.Evaluate(Vector3.Angle(directionToTarget, shootPositionForward))) + 0.01f * spread * UnityEngine.Random.insideUnitSphere);
				if (extraSpreadInMelee != 0f && (bool)base.connectedData && base.connectedData.distanceToTarget < 5f)
				{
					internalCounter = 0f - extraCDInMelee;
					if (randomSeed.HasValue)
					{
						SetRandomSeed(randomSeed.Value);
						randomSeed++;
					}
					componentInChildren.transform.Rotate(extraSpreadInMelee * UnityEngine.Random.insideUnitSphere);
				}
			}
			MoveTransform componentInChildren2 = spawnedObject.GetComponentInChildren<MoveTransform>();
			if ((bool)componentInChildren2)
			{
				componentInChildren2.projectileSpeedSpread = projectileSpeedSpread;
				if ((bool)componentInChildren && componentInChildren.forwardCompensation > 0f && (bool)targetRig)
				{
					componentInChildren2.selfImpulse.z += Mathf.Pow(Mathf.Clamp(Vector3.Distance(targetRigPosition, base.transform.position), 0f, componentInChildren.clampDistance), componentInChildren.rangePow) * componentInChildren.forwardCompensation;
				}
			}
			AddForce component2 = spawnedObject.GetComponent<AddForce>();
			if ((bool)component2 && (bool)componentInChildren && componentInChildren.forwardCompensation > 0f && (bool)targetRig)
			{
				component2.force.z += Mathf.Pow(Mathf.Clamp(Vector3.Distance(targetRigPosition, base.transform.position), 0f, componentInChildren.clampDistance), componentInChildren.rangePow) * componentInChildren.forwardCompensation;
			}
			ProjectileHit componentInChildren3 = componentsInChildren[i].GetComponentInChildren<ProjectileHit>();
			if ((bool)componentInChildren3)
			{
				componentInChildren3.damage *= levelMultiplier;
				if ((bool)base.connectedData && base.connectedData.input.hasControl)
				{
					componentInChildren3.alwaysHitTeamMates = true;
				}
				if ((bool)myLevel)
				{
					componentInChildren3.ignoreTeamMates = myLevel.ignoreTeam;
				}
			}
			CollisionWeapon componentInChildren4 = componentsInChildren[i].GetComponentInChildren<CollisionWeapon>();
			if ((bool)componentInChildren4)
			{
				componentInChildren4.damage *= levelMultiplier;
			}
			if (charge != 0f)
			{
				if ((bool)componentInChildren2)
				{
					componentInChildren2.selfImpulse *= charge;
					componentInChildren2.worldImpulse.y = 0f;
				}
				if ((bool)componentInChildren3)
				{
					componentInChildren3.damage *= charge;
					componentInChildren3.force *= charge;
				}
			}
		}
		if (randomSeed.HasValue)
		{
			RestoreRandomSeed();
		}
		Level level = spawnedObject.GetComponent<Level>();
		if (level == null)
		{
			level = spawnedObject.AddComponent<Level>();
		}
		level.levelMultiplier = levelMultiplier;
		if ((bool)myLevel)
		{
			level.ignoreTeam = myLevel.ignoreTeam;
			level.level = myLevel.level;
		}
	}

	public void SetTargetableEffects(GameObject spawnedObject, Vector3 spawnPos, Rigidbody targetRig)
	{
		if ((bool)targetRig && !(spawnedObject == null))
		{
			Vector3 endPoint = targetRig.position;
			if (clampRange != float.PositiveInfinity)
			{
				Vector3 vector = targetRig.position - spawnPos;
				vector = Vector3.ClampMagnitude(vector, clampRange);
				endPoint = spawnPos + vector;
			}
			TargetableEffect[] components = spawnedObject.GetComponents<TargetableEffect>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].damageMultiplier = levelMultiplier;
				components[i].DoEffect(shootPosition, targetRig.transform);
				components[i].DoEffect(spawnPos, endPoint, targetRig);
			}
		}
	}

	private Vector3 GetSpawnPosition(Rigidbody targetRig)
	{
		Vector3 result = Vector3.zero;
		if (spawnPosition == SpawnPosition.TargetPosition && (bool)targetRig)
		{
			result = targetRig.position;
		}
		if (spawnPosition == SpawnPosition.ShootPosition)
		{
			result = shootPosition.position;
		}
		if (spawnPosition == SpawnPosition.groundPosition)
		{
			result = base.connectedData.groundPosition;
		}
		if (spawnPosition == SpawnPosition.ownTorso)
		{
			result = base.connectedData.torso.position;
		}
		result += positionOffset;
		Vector3 vector = ((randomType == RandomType.InSphereRandom) ? (UnityEngine.Random.insideUnitSphere * randomPosition) : (UnityEngine.Random.onUnitSphere * randomPosition));
		vector.x *= randomX;
		vector.y *= randomY;
		vector.z *= randomZ;
		result += vector;
		if ((bool)base.connectedData)
		{
			result += base.connectedData.characterForwardObject.forward * characterForwardOffset;
		}
		return result;
	}

	private Vector3 GetSpawnDirection(Vector3 directionToTarget, Rigidbody targetRig, Vector3 forcedDirection)
	{
		Vector3 result = Vector3.Lerp(directionToTarget, shootPosition.forward, shootHelpAngleCurve.Evaluate(Vector3.Angle(directionToTarget, shootPosition.forward))).normalized;
		if ((bool)base.connectedData && base.connectedData.input.hasControl)
		{
			if (possessionAimingMode == PossessionAimingMode.Forward)
			{
				result = shootPosition.forward;
			}
			else if (!targetRig)
			{
				result = forcedDirection;
			}
		}
		return result;
	}

	public void AddShootAction(Action action)
	{
		shootAction = (Action)Delegate.Combine(shootAction, action);
	}

	public void AddAttackCallRecievedAction(Action action)
	{
		attackCallRecievedAction = (Action)Delegate.Combine(attackCallRecievedAction, action);
	}

	private void SetRandomSeed(int seed)
	{
		if (BoltNetwork.IsRunning)
		{
			UnityEngine.Random.InitState(seed);
		}
	}

	private void RestoreRandomSeed()
	{
		if (BoltNetwork.IsRunning)
		{
			UnityEngine.Random.InitState(Environment.TickCount);
		}
	}
}
