using System;
using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileHit : MonoBehaviour, GameObjectPooling.IPoolable, IValidatable
{
	public delegate void HitUnitEventHandler(ProjectileHit projectileHit, Unit unit);

	[SerializeField]
	private string soundRef = "";

	[SerializeField]
	private AudioPathData soundPathData;

	[HideInInspector]
	public bool done;

	public float setSpeedOnHit;

	public float damage = 55f;

	public float force = 200f;

	public float lowMassCap = 10f;

	public float shakeAmount = 1f;

	public float hitStop;

	public float blockPoweredNeeded;

	private ProjectileStick stick;

	private MoveTransform move;

	private RaycastTrail trail;

	public bool ignoreTeamMates;

	public ObjectToSpawn[] objectsToSpawn;

	[Tooltip("This event is fired when the object is released back into the pool.Use this to clean up parts of the state of the object if needed.")]
	[SerializeField]
	private UnityEvent releaseEvent;

	public HitEvents[] hitEvents;

	public HitEvents[] onUnitsEvent;

	public float onlyCallHitEventsOnRigsLessThan;

	private ProjectileHitEffect[] hitEffects;

	private List<Unit> unitsHit = new List<Unit>();

	private TeamHolder teamHolder;

	public bool alwaysHitTeamMates;

	private float counter;

	public int allowedHits = 5;

	public bool stopAditionalHitsOnArmor;

	private int startAllowedHits = 5;

	private bool spawned;

	public bool allowRepeat;

	public bool deltaTime;

	public bool destroyOnHit;

	public bool moveToHitPoint = true;

	public bool callEffectsBeforeDamage;

	public bool callEffectsOnArmor = true;

	public bool canHitOrgUnit;

	public bool stopOnMapHit;

	private Unit ownUnit;

	public bool destroyOnRefect;

	private Level level;

	private SoundPlayer soundPlayer;

	private HitData hit;

	public UnityEvent disableProjectileEvent;

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

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	public event HitUnitEventHandler HitUnit;

	internal void SwitchTarget(GameObject newHand)
	{
		hit.transform = newHand.transform;
		hit.rigidbody = hit.transform.GetComponent<Rigidbody>();
		hit.collider = hit.transform.GetComponentInChildren<Collider>();
	}

	public bool Validate()
	{
		return AudioPathData.ValidateAndAssignPathData(soundRef, ref soundPathData, base.gameObject);
	}

	public void Initialize()
	{
	}

	public void Reset()
	{
		if ((bool)move && (bool)stick)
		{
			move.enabled = true;
		}
		if ((bool)trail)
		{
			trail.enabled = true;
		}
		done = false;
		counter = 0f;
		allowedHits = startAllowedHits;
	}

	public void Release()
	{
		unitsHit.Clear();
		releaseEvent?.Invoke();
	}

	public void ResetTeam()
	{
		if ((bool)teamHolder)
		{
			teamHolder.spawner = null;
		}
	}

	private void Start()
	{
		soundPlayer = ServiceLocator.GetService<SoundPlayer>();
		startAllowedHits = allowedHits;
		lowMassCap *= 3f;
		force *= 3f;
		hitEffects = GetComponentsInChildren<ProjectileHitEffect>();
		move = GetComponent<MoveTransform>();
		teamHolder = GetComponent<TeamHolder>();
		trail = GetComponent<RaycastTrail>();
		stick = GetComponent<ProjectileStick>();
		level = GetComponentInParent<Level>();
		if ((bool)level)
		{
			force *= Mathf.Pow(level.level, 2f);
			lowMassCap *= Mathf.Pow(level.level, 2f);
			if (level.ignoreTeam)
			{
				ignoreTeamMates = level.ignoreTeam;
			}
		}
	}

	private void Update()
	{
		counter += Time.deltaTime;
	}

	public void Hit(RaycastHit sentHit, float multiplier = 1f)
	{
		hit = new HitData(sentHit.transform, sentHit.rigidbody, sentHit.collider, sentHit.normal, sentHit.point, sentHit.distance);
		if (done)
		{
			return;
		}
		Armor armor = null;
		if ((bool)hit.rigidbody)
		{
			armor = hit.rigidbody.GetComponent<Armor>();
		}
		if ((bool)hit.rigidbody && !armor && (bool)hit.rigidbody.GetComponent<Holdable>())
		{
			return;
		}
		if ((bool)armor)
		{
			force *= armor.projectileHitForceMultiplier;
		}
		Unit component = hit.transform.root.GetComponent<Unit>();
		if (((bool)teamHolder && (bool)teamHolder.spawner && hit.transform.root.gameObject == teamHolder.spawner && !canHitOrgUnit) || ((bool)move && (bool)component && (bool)teamHolder && teamHolder.team == component.Team && (counter * 0.01f * move.velocity.magnitude < 0.1f || ignoreTeamMates) && !alwaysHitTeamMates))
		{
			return;
		}
		ProjectileSurfaceEffect projectileSurfaceEffect = null;
		projectileSurfaceEffect = ((!hit.rigidbody) ? hit.collider.gameObject.GetComponentInChildren<ProjectileSurfaceEffect>() : hit.rigidbody.gameObject.GetComponentInChildren<ProjectileSurfaceEffect>());
		if ((bool)projectileSurfaceEffect && projectileSurfaceEffect.DoEffect(hit, base.gameObject))
		{
			return;
		}
		if ((bool)armor && stopAditionalHitsOnArmor)
		{
			allowedHits = 0;
		}
		if (stopOnMapHit && hit.transform.gameObject.layer == LayerMask.NameToLayer("Map"))
		{
			allowedHits = 0;
		}
		if (allowedHits < 1)
		{
			DisableProjectile();
		}
		else
		{
			allowedHits--;
		}
		if (allowedHits <= 0)
		{
			allowRepeat = false;
		}
		if ((bool)component && !deltaTime)
		{
			if (unitsHit.Contains(component))
			{
				return;
			}
			unitsHit.Add(component);
		}
		if (component != null)
		{
			this.HitUnit?.Invoke(this, component);
		}
		_ = (bool)hit.rigidbody;
		float num = (deltaTime ? (Time.deltaTime * 100f) : 1f);
		if (moveToHitPoint)
		{
			base.transform.position = hit.point;
		}
		DoVisualEffects(hit, num);
		if (!callEffectsBeforeDamage || (!callEffectsOnArmor && (bool)armor) || !DoHitEffects(hit))
		{
			DoGameplayEffects(hit, multiplier * num, component);
			if ((callEffectsBeforeDamage || (!callEffectsOnArmor && (bool)armor) || !DoHitEffects(hit)) && !allowRepeat)
			{
				DisableProjectile();
			}
		}
	}

	internal void DestroyAndSpawn(HitData hit)
	{
		for (int i = 0; i < objectsToSpawn.Length; i++)
		{
			Spawn(objectsToSpawn[i], hit);
		}
		if (IsManagedByPool)
		{
			ReleaseSelf?.Invoke();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void AddForceToTarget(Vector3 hitPoint, Rigidbody rig, float m = 1f)
	{
		WilhelmPhysicsFunctions.AddForceWithMinWeight(rig, base.transform.forward * force * m, hitPoint, ForceMode.Impulse, lowMassCap);
		rig.velocity *= 0.7f;
	}

	private bool DoHitEffects(HitData hit)
	{
		bool result = false;
		for (int i = 0; i < hitEffects.Length; i++)
		{
			if ((bool)hitEffects[i] && hitEffects[i].DoEffect(hit))
			{
				result = true;
			}
		}
		return result;
	}

	public void DisableProjectile()
	{
		if ((bool)move && (bool)stick)
		{
			move.enabled = false;
		}
		if ((bool)trail)
		{
			trail.enabled = false;
		}
		done = true;
		allowRepeat = false;
		if (destroyOnHit)
		{
			if (IsManagedByPool)
			{
				ReleaseSelf?.Invoke();
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			trail.enabled = false;
		}
		disableProjectileEvent?.Invoke();
	}

	public void EnableProjectile()
	{
		if ((bool)move)
		{
			move.enabled = true;
		}
		if ((bool)trail)
		{
			trail.enabled = true;
		}
		done = false;
	}

	private void DoVisualEffects(HitData hit, float m)
	{
		if ((bool)ScreenShake.Instance)
		{
			ScreenShake.Instance.AddForce(base.transform.forward * shakeAmount * m, base.transform.position);
		}
	}

	private void DoGameplayEffects(HitData hit, float m = 1f, Unit unit = null)
	{
		if ((bool)hit.rigidbody)
		{
			AddForceToTarget(hit.point, hit.rigidbody, m);
		}
		for (int i = 0; i < objectsToSpawn.Length; i++)
		{
			StartCoroutine(SpawnAfterDelay(objectsToSpawn[i], hit));
		}
		if (onlyCallHitEventsOnRigsLessThan == 0f || ((bool)hit.rigidbody && hit.rigidbody.mass < onlyCallHitEventsOnRigsLessThan))
		{
			for (int j = 0; j < hitEvents.Length; j++)
			{
				StartCoroutine(DelayHitEvent(hitEvents[j].eventDelay, hitEvents[j].hitEvent));
			}
		}
		if ((bool)unit)
		{
			for (int k = 0; k < onUnitsEvent.Length; k++)
			{
				StartCoroutine(DelayHitEvent(onUnitsEvent[k].eventDelay, onUnitsEvent[k].hitEvent));
			}
		}
		if ((bool)stick && (!hit.rigidbody || stick.minWeight < hit.rigidbody.mass))
		{
			DisableProjectile();
			stick.Stick(hit.transform, base.transform.forward + UnityEngine.Random.insideUnitSphere * 0.2f, hit.point, hit.rigidbody ? hit.rigidbody : null);
		}
		soundPlayer.PlaySoundEffectNonAlloc(soundPathData, 1f, base.transform.position, SoundEffectVariations.GetMaterialType(hit.transform.gameObject, hit.rigidbody));
		Damagable componentInParent = hit.transform.GetComponentInParent<Damagable>();
		if ((bool)componentInParent && (bool)hit.rigidbody)
		{
			if ((bool)teamHolder && (bool)teamHolder.spawner)
			{
				ownUnit = teamHolder.spawner.GetComponentInParent<Unit>();
			}
			float num = 1f;
			DamageMultiplier component = hit.rigidbody.GetComponent<DamageMultiplier>();
			if ((bool)component)
			{
				num = component.multiplier;
			}
			componentInParent.TakeDamage(damage * num * m, base.transform.forward, ownUnit);
		}
		if (setSpeedOnHit > 0f && (bool)move)
		{
			move.velocity = Vector3.ClampMagnitude(move.velocity, setSpeedOnHit);
		}
	}

	private IEnumerator SpawnAfterDelay(ObjectToSpawn objectToSpawn, HitData hit)
	{
		if (objectToSpawn.delay > 0f)
		{
			yield return new WaitForSeconds(objectToSpawn.delay);
		}
		Spawn(objectToSpawn, hit);
	}

	private void Spawn(ObjectToSpawn objectToSpawn, HitData hit)
	{
		Quaternion quaternion = Quaternion.identity;
		if (objectToSpawn.spawnRotation == ObjectToSpawn.SpawnRotation.normal)
		{
			quaternion = Quaternion.LookRotation(hit.normal);
		}
		if (objectToSpawn.spawnRotation == ObjectToSpawn.SpawnRotation.forward)
		{
			quaternion = base.transform.rotation;
		}
		if (objectToSpawn.spawnRotation == ObjectToSpawn.SpawnRotation.random)
		{
			quaternion = UnityEngine.Random.rotation;
		}
		GameObject gameObject = null;
		if (objectToSpawn.spawnOnce && spawned)
		{
			return;
		}
		if ((bool)objectToSpawn.objectToSpawn)
		{
			gameObject = UnityEngine.Object.Instantiate(objectToSpawn.objectToSpawn, base.transform.position, quaternion);
			if (objectToSpawn.childOfThis)
			{
				gameObject.transform.parent = base.transform;
			}
			if ((bool)level)
			{
				Level obj = gameObject.AddComponent<Level>();
				obj.levelMultiplier = level.levelMultiplier;
				obj.ignoreTeam = level.ignoreTeam;
			}
			if ((bool)this.teamHolder)
			{
				TeamHolder teamHolder = gameObject.AddComponent<TeamHolder>();
				teamHolder.team = this.teamHolder.team;
				if ((bool)this.teamHolder)
				{
					teamHolder.spawner = this.teamHolder.spawner;
				}
				else
				{
					teamHolder.spawner = base.gameObject;
				}
			}
			if (objectToSpawn.removeObjectsToSpawn)
			{
				ProjectileHit component = gameObject.GetComponent<ProjectileHit>();
				if ((bool)component)
				{
					component.objectsToSpawn = new ObjectToSpawn[0];
				}
			}
		}
		if (objectToSpawn.particleID != -1)
		{
			ServiceLocator.GetService<ParticlePlayer>().PlayEffect(objectToSpawn.particleID, base.transform.position, quaternion * Vector3.forward);
		}
		if (objectToSpawn.spawnOnce && !spawned)
		{
			spawned = true;
		}
		if (objectToSpawn.destroy)
		{
			if (IsManagedByPool)
			{
				ReleaseSelf?.Invoke();
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private IEnumerator DelayHitEvent(float time, UnityEvent theEvent)
	{
		yield return new WaitForSeconds(time);
		theEvent?.Invoke();
	}
}
