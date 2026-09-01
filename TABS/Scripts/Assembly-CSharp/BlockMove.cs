using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class BlockMove : MonoBehaviour
{
	private const float LowFixedTimeStepAdjustmentFactor = 1.25f;

	public int projectilesPerBlock = 1;

	public float blockPower = 1f;

	public AnimationCurve blockCurve;

	public float blockMoveImpulse;

	public float blockAngle = 70f;

	public bool blockFriendlyProjectiles;

	public bool reflect = true;

	public bool destoryProjectile;

	public bool useLineEffect;

	public bool useWeaponPos = true;

	public bool switchTeam;

	private LineEffects lineEffect;

	public Transform t1;

	public Transform t2;

	private Transform weaponPos;

	public bool isActive = true;

	public UnityEvent enableEvent;

	public UnityEvent blockEvent;

	public UnityEvent disableEvent;

	public Rigidbody rig;

	public GameObject sliceEffect;

	public bool useSliceEffect = true;

	public GameObject effectToSpawn;

	public bool stopProjectile;

	public float goodReflectChance;

	public float timeBetweenMeleeBlocks = 0.4f;

	private DataHandler data;

	private Counter counter;

	private bool goodReflect;

	private WeaponHandler weaponHandler;

	private int currentProjectilesPerBlock = 1;

	private List<Rigidbody> acceptableRigs = new List<Rigidbody>();

	private Vector3 spawnRot;

	private Vector3 sliceDir;

	private bool effectSpawned;

	private FixedTimeStepService fixedTimeStepService;

	private UnitRig unitRig;

	private float forceCounter;

	public Action<GameObject, HitData> blockAction;

	private void Awake()
	{
		Transform root = base.transform.root;
		if (root != null)
		{
			data = root.GetComponentInChildren<DataHandler>();
			unitRig = root.GetComponentInChildren<UnitRig>();
		}
		if (data != null)
		{
			weaponHandler = data.GetComponentInParent<WeaponHandler>();
		}
		counter = GetComponent<Counter>();
		lineEffect = GetComponentInChildren<LineEffects>();
		if (!(unitRig != null))
		{
			return;
		}
		Transform target = null;
		if (unitRig.m_head != null)
		{
			target = unitRig.m_head.transform;
		}
		else if (unitRig != null)
		{
			target = unitRig.transform;
		}
		SurfaceBlock[] componentsInChildren = GetComponentsInChildren<SurfaceBlock>();
		if (componentsInChildren == null)
		{
			return;
		}
		SurfaceBlock[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			FollowTransform component = array[i].GetComponent<FollowTransform>();
			if (component != null)
			{
				component.target = target;
			}
		}
	}

	private void Start()
	{
		fixedTimeStepService = ServiceLocator.GetService<FixedTimeStepService>();
		currentProjectilesPerBlock = projectilesPerBlock;
	}

	public void SetEnabled()
	{
		isActive = true;
		enableEvent.Invoke();
	}

	public void SetDisabled()
	{
		isActive = false;
		disableEvent.Invoke();
	}

	private void GetAcceptableRigs()
	{
		if (!weaponHandler)
		{
			return;
		}
		MeleeWeapon meleeWeapon = null;
		MeleeWeapon meleeWeapon2 = null;
		if ((bool)weaponHandler.rightWeapon)
		{
			meleeWeapon = weaponHandler.rightWeapon.GetComponent<MeleeWeapon>();
		}
		if ((bool)weaponHandler.leftWeapon)
		{
			meleeWeapon2 = weaponHandler.leftWeapon.GetComponent<MeleeWeapon>();
		}
		if ((bool)meleeWeapon)
		{
			acceptableRigs.Add(meleeWeapon.rigidbody);
		}
		if ((bool)meleeWeapon2)
		{
			acceptableRigs.Add(meleeWeapon2.rigidbody);
		}
		if (!meleeWeapon2 && !meleeWeapon && acceptableRigs.Count == 0)
		{
			Rigidbody component = base.transform.root.GetComponentInChildren<HandLeft>().GetComponent<Rigidbody>();
			Rigidbody component2 = base.transform.root.GetComponentInChildren<HandLeft>().GetComponent<Rigidbody>();
			if ((bool)component)
			{
				acceptableRigs.Add(component);
			}
			if ((bool)component2)
			{
				acceptableRigs.Add(component2);
			}
		}
		if (acceptableRigs.Count == 0)
		{
			if ((bool)weaponHandler.rightWeapon && (bool)weaponHandler.rightWeapon.rigidbody)
			{
				acceptableRigs.Add(weaponHandler.rightWeapon.rigidbody);
			}
			if ((bool)weaponHandler.leftWeapon && (bool)weaponHandler.leftWeapon.rigidbody)
			{
				acceptableRigs.Add(weaponHandler.leftWeapon.rigidbody);
			}
		}
	}

	private void AssignRig()
	{
		if ((bool)weaponHandler)
		{
			if (acceptableRigs.Count == 0)
			{
				GetAcceptableRigs();
			}
			rig = acceptableRigs[UnityEngine.Random.Range(0, acceptableRigs.Count)];
		}
	}

	private void Update()
	{
		forceCounter += Time.deltaTime;
	}

	private void PlaySlice(Vector3 pos, Quaternion rot)
	{
		GameObject obj = UnityEngine.Object.Instantiate(sliceEffect, null);
		obj.transform.position = pos;
		obj.transform.rotation = rot;
		obj.GetComponent<CodeAnimation>()?.PlayIn();
		obj.AddComponent<RemoveAfterSeconds>().seconds = 0.5f;
	}

	public bool ProjectileBlock(GameObject projectile, HitData hit)
	{
		AssignRig();
		if ((bool)data && data.Dead)
		{
			return false;
		}
		if (!isActive)
		{
			return false;
		}
		if ((bool)data && Vector3.Angle(-data.characterForwardObject.forward, projectile.transform.forward) > blockAngle)
		{
			return false;
		}
		TeamHolder component = projectile.GetComponent<TeamHolder>();
		if ((bool)component && (bool)data && data.unit.Team == component.team)
		{
			return false;
		}
		ProjectileHit component2 = projectile.GetComponent<ProjectileHit>();
		if ((bool)component2)
		{
			if (component2.blockPoweredNeeded > blockPower)
			{
				return false;
			}
			if (component2.destroyOnRefect)
			{
				component2.DestroyAndSpawn(hit);
			}
		}
		if (counter != null)
		{
			if (counter.IsOnCooldown())
			{
				return false;
			}
			currentProjectilesPerBlock--;
			if (currentProjectilesPerBlock <= 0)
			{
				currentProjectilesPerBlock = projectilesPerBlock;
				counter.ResetCounter();
			}
		}
		MoveTransform component3 = projectile.GetComponent<MoveTransform>();
		if (goodReflectChance != 0f && goodReflectChance > (float)UnityEngine.Random.Range(0, 1))
		{
			goodReflect = true;
		}
		if (reflect)
		{
			switch (fixedTimeStepService.CurrentFixedTimeStep)
			{
			case FixedTimeStepService.FixedTimeStep.SixtyUpdates:
				component3.velocity = Vector3.Reflect(component3.velocity, hit.normal);
				break;
			case FixedTimeStepService.FixedTimeStep.ThirtyUpdates:
				component3.velocity = Vector3.Reflect(component3.velocity, hit.normal) * 1.25f;
				break;
			default:
				Debug.LogError("This FixedTimeStep has not been handled. This should not happen.Defaulting to 60 behaviour.");
				component3.velocity = Vector3.Reflect(component3.velocity, hit.normal);
				break;
			}
			if (!goodReflect)
			{
				component3.velocity *= UnityEngine.Random.Range(0.3f, 0.5f);
			}
		}
		projectile.GetComponent<RaycastTrail>().ignoredFrames = 3;
		if ((bool)data)
		{
			spawnRot = projectile.transform.position - data.mainRig.transform.position;
			sliceDir = Vector3.Cross((projectile.transform.position - rig.transform.position).normalized, data.characterForwardObject.forward).normalized;
		}
		else
		{
			spawnRot = projectile.transform.position - base.transform.position;
			sliceDir = Vector3.forward;
		}
		PlaySlice(projectile.transform.position, Quaternion.LookRotation(spawnRot, sliceDir));
		blockEvent.Invoke();
		if (lineEffect != null && useLineEffect)
		{
			if (useWeaponPos && (bool)weaponHandler)
			{
				t1.position = weaponHandler.rightWeapon.transform.position;
			}
			t2.position = hit.point;
			lineEffect.Play(t1, t2);
		}
		if ((bool)effectToSpawn)
		{
			if (destoryProjectile)
			{
				UnityEngine.Object.Instantiate(effectToSpawn, projectile.transform.position, projectile.transform.rotation);
				effectSpawned = true;
			}
			else
			{
				UnityEngine.Object.Instantiate(effectToSpawn, projectile.transform.position, projectile.transform.rotation);
				if (stopProjectile)
				{
					component3.rotationFollowVelocity = false;
					component3.velocity = Vector3.zero;
					component3.gravity = 0f;
					UnityEngine.Object.Destroy(projectile.GetComponent<RemoveAfterSeconds>());
				}
			}
		}
		if (forceCounter > 0.1f)
		{
			rig.AddForce((projectile.transform.position - rig.position).normalized * blockMoveImpulse, ForceMode.Acceleration);
			forceCounter = 0f;
		}
		if (switchTeam)
		{
			component.team = data.unit.Team;
		}
		if (!projectile.transform.GetChild(0).gameObject.GetComponent<ProjectileRotate>() && reflect)
		{
			if (!goodReflect)
			{
				ProjectileRotate projectileRotate = projectile.transform.GetChild(0).gameObject.AddComponent<ProjectileRotate>();
				projectileRotate.rotation = sliceDir * (0f - UnityEngine.Random.Range(1500f, 3000f));
				projectileRotate.self = false;
			}
			else
			{
				projectile.transform.rotation = Quaternion.LookRotation(projectile.GetComponent<TeamHolder>().spawner.transform.GetComponentInChildren<DataHandler>().mainRig.transform.position - base.transform.position);
			}
		}
		else if (destoryProjectile)
		{
			StartCoroutine(DestoryProjectile(component2, hit, 0f));
		}
		if (blockAction != null)
		{
			blockAction(projectile, hit);
		}
		return true;
	}

	private IEnumerator DestoryProjectile(ProjectileHit projHit, HitData hit, float seconds)
	{
		if (seconds > 0f)
		{
			yield return new WaitForSeconds(seconds);
		}
		projHit.DestroyAndSpawn(hit);
	}
}
