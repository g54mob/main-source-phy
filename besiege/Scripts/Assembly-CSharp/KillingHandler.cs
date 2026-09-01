using System;
using System.Collections;
using Localisation;
using UnityEngine;

[AddComponentMenu("AI/KillingHandler")]
public class KillingHandler : MonoBehaviour, IExplosionEffect
{
	[Serializable]
	public class DamageAmount
	{
		[Tooltip("Miniman Velocity for this unit to take Damage through Collisions")]
		public float minimalVelocity = 100f;

		[Tooltip("Should this unit allways die from burning?")]
		public bool FireSureDeath = true;

		[Tooltip("Amount of Damage per Second from Fire")]
		public float fireDamage = 250f;

		[Tooltip("Fire Damage Multiplier")]
		public float FireScale = 1f;

		[Tooltip("Sharp Damage Multiplier")]
		public float SharpScale = 1f;

		[Tooltip("Sharp Vel Check Multiplier")]
		public float SharpMinVelScale = 1f;

		[Tooltip("Blunt Damage Multiplier")]
		public float BluntScale = 1f;

		[Tooltip("Blunt Vel Check Multiplier")]
		public float BluntMinVelScale = 1f;

		[Tooltip("Blunt Damage Multiplier While Vacuumed")]
		public float BluntVacuumeScale = 0.5f;

		[Tooltip("Percentage of projectiles deflected")]
		public float projectileDeflection = 0.25f;

		[Tooltip("Maximum damage dealt from a collision using AngularVel approximation")]
		public float maxDamageFromAngularVelCalc = 600f;
	}

	[Serializable]
	public class References
	{
		public EntityAI AiCode;

		public Renderer Renderer;

		public Texture2D[] BloodyTexture;

		public SetPoseForAI Poser;

		public GameObject GibPrefab;

		public GameObject corpseDust;

		[HideInInspector]
		public Transform PhysicsGoal;

		public RandomSoundController SoundController;

		public Rigidbody Rigidbody;

		public FireController fireControl;

		public ExplodeOnCollide bomb;

		[HideInInspector]
		public bool hasRenderer;
	}

	public class Explode
	{
		public float upAmountScaler = -0.5f;

		public float powerScaler = 2f;
	}

	public delegate void Del();

	public Transform[] destroyOnDie;

	[HideInInspector]
	public InjuryType activeType;

	public string[] bluntDeaths;

	public string[] fireDeaths;

	public string[] sharpDeaths;

	public bool updateBloodTextureOnHit;

	public bool causeOnScreenUpdate = true;

	public bool UseGibPrefab;

	public bool gibWhenSuffocateing;

	public bool canSuffocate = true;

	public float timeToSuffocate = 3f;

	public Del SuffocatingUpdate;

	private float resetSuffocateTime;

	private RealtimeUpdater RealtimeUpdaterCode;

	private string deathBy = "Heart Failure";

	private float drowningParticleRate = 0.2f;

	private float drowningParticleTimer;

	private LevelEntity levelEntity;

	private bool simPhys;

	[HideInInspector]
	public float maxHealth;

	private float fireDamageRatio;

	public Action<MonoBehaviour> OnDeath;

	public Action GettingGibbed;

	public DamageAmount damageAmount = new DamageAmount();

	public References my = new References();

	public Explode explode = new Explode();

	private bool gibbed;

	protected void Start()
	{
		simPhys = !StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim;
		if (StatMaster.levelSimulating)
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			materialPropertyBlock.SetFloat("_BloodAmount", 0f);
			if (my.Renderer != null)
			{
				my.hasRenderer = true;
				my.Renderer.SetPropertyBlock(materialPropertyBlock);
			}
			my.PhysicsGoal = ReferenceMaster.physicsGoalInstance;
		}
		if (my.AiCode == null)
		{
			my.AiCode = GetComponent<EntityAI>();
		}
		levelEntity = my.AiCode.levelEntity;
		maxHealth = my.AiCode.health;
		if (!my.fireControl)
		{
			my.fireControl = base.transform.GetComponentInChildren<FireController>();
		}
		RealtimeUpdaterCode = RealtimeUpdater.Instance;
		resetSuffocateTime = timeToSuffocate;
		switch (my.AiCode.subAIType)
		{
		case AIType.Fish:
			SuffocatingUpdate = FishInAirUpdate;
			break;
		case AIType.LandBased:
		case AIType.Bird:
			SuffocatingUpdate = DrowningUpdate;
			break;
		}
		drowningParticleTimer = UnityEngine.Random.Range(0f, drowningParticleRate);
	}

	protected void Update()
	{
		if (!StatMaster.levelSimulating || (StatMaster.isClient && !StatMaster.isLocalSim) || my.AiCode.isDead)
		{
			return;
		}
		SuffocatingUpdate();
		if (my.fireControl.onFire)
		{
			if (damageAmount.FireSureDeath)
			{
				fireDamageRatio = (maxHealth + 10f) / my.fireControl.fullFireDuration * Time.deltaTime;
				TakeDamage(fireDamageRatio, InjuryType.Fire);
			}
			else
			{
				TakeDamage(damageAmount.fireDamage * Time.deltaTime, InjuryType.Fire);
			}
		}
		if (damageAmount.FireSureDeath && my.fireControl.fireProgress >= 1f)
		{
			TakeDamage(maxHealth, InjuryType.Fire);
		}
	}

	private void DrowningUpdate()
	{
		if (canSuffocate && (my.AiCode.disposition.myState == EntityAI.EntityState.Suffocating || (my.AiCode.disposition.myState == EntityAI.EntityState.Grabbed && my.AiCode.my.basicInfo.InWater)))
		{
			timeToSuffocate -= Time.deltaTime;
			PlaySuffocateingSounds();
			DrowningParticlesUpdate();
			if (timeToSuffocate <= 0f)
			{
				UseGibPrefab = gibWhenSuffocateing;
				activeType = InjuryType.Suffocateing;
				KillMe(false);
			}
		}
		else if (timeToSuffocate < resetSuffocateTime)
		{
			timeToSuffocate = resetSuffocateTime;
		}
	}

	private void FishInAirUpdate()
	{
		if (canSuffocate && (my.AiCode.disposition.myState == EntityAI.EntityState.Suffocating || !my.AiCode.my.basicInfo.InWater))
		{
			timeToSuffocate -= Time.deltaTime;
			if (timeToSuffocate <= 0f)
			{
				UseGibPrefab = gibWhenSuffocateing;
				activeType = InjuryType.Suffocateing;
				KillMe(false);
			}
		}
		else if (timeToSuffocate < resetSuffocateTime)
		{
			timeToSuffocate = resetSuffocateTime;
		}
	}

	private void DrowningParticlesUpdate()
	{
		if (drowningParticleTimer <= 0f)
		{
			drowningParticleTimer = drowningParticleRate;
			if (my.AiCode.my.basicInfo.submergedPercent != 1f)
			{
				GlobalParticles.EmitParticleBursts(0, base.transform.position);
			}
			else
			{
				GlobalParticles.EmitParticleAmount(3, base.transform.position, 1);
			}
		}
		else
		{
			drowningParticleTimer -= Time.deltaTime;
		}
	}

	protected void OnCollisionEnter(Collision collision)
	{
		if (!StatMaster.levelSimulating || object.ReferenceEquals(my.AiCode, null) || !base.enabled || !StatMaster.levelSimulating)
		{
			return;
		}
		if (StatMaster.isMP)
		{
			AIGenericEntity aiGenEntity = my.AiCode.my.aiGenEntity;
			if (!aiGenEntity.SimPhysics)
			{
				return;
			}
		}
		float num = collision.relativeVelocity.sqrMagnitude;
		if (collision.collider.CompareTag("DamageIgnored"))
		{
			num *= 0.1f;
		}
		float num2 = num;
		if (!my.AiCode.isDead)
		{
			activeType = ((collision.collider.gameObject.layer == 26) ? InjuryType.Sharp : InjuryType.Blunt);
			float num3 = num2 * ((activeType != InjuryType.Sharp) ? damageAmount.BluntMinVelScale : damageAmount.SharpMinVelScale);
			if (my.AiCode is EntityAIFish && num3 <= damageAmount.minimalVelocity && collision.rigidbody != null)
			{
				Vector3 zero = Vector3.zero;
				for (int i = 0; i < collision.contacts.Length; i++)
				{
					zero += collision.rigidbody.GetRelativePointVelocity(collision.contacts[i].point);
				}
				float num4 = Mathf.Min((zero / collision.contacts.Length * 0.2f).sqrMagnitude, damageAmount.maxDamageFromAngularVelCalc);
				num2 = ((!(num > num4)) ? num4 : num);
				num3 = num2 * ((activeType != InjuryType.Sharp) ? damageAmount.BluntMinVelScale : damageAmount.SharpMinVelScale);
			}
			if (!(num3 > damageAmount.minimalVelocity))
			{
				return;
			}
			TakeDamage(num2, activeType);
			if (!(my.AiCode.health < 1f))
			{
				return;
			}
			if (collision.collider.gameObject.layer == 26)
			{
				if (num2 > damageAmount.minimalVelocity * 4f)
				{
					KillUnit(false, InjuryType.Crushed);
				}
				else
				{
					KillUnit(false, InjuryType.Sharp);
				}
				BloodParticle();
			}
			else
			{
				Vector3 vector = new Vector3(collision.relativeVelocity.x * (float)((collision.relativeVelocity.x > 0f) ? 1 : (-1)), collision.relativeVelocity.y * (float)((collision.relativeVelocity.y > 0f) ? 1 : (-1)), collision.relativeVelocity.z * (float)((collision.relativeVelocity.z > 0f) ? 1 : (-1)));
				if (vector.y > vector.x && vector.y > vector.z && vector.y > 100f)
				{
					KillUnit(false, InjuryType.Crushed);
					BloodParticle();
				}
				else
				{
					my.AiCode.onDeath.LeapAmount = ((!(collision.relativeVelocity.sqrMagnitude * 4f > my.AiCode.onDeath.MaxLeapAmount)) ? (collision.relativeVelocity.sqrMagnitude * 4f) : my.AiCode.onDeath.MaxLeapAmount);
					KillUnit(true, activeType);
				}
			}
			BleedOnObject(collision.collider);
		}
		else if (activeType == InjuryType.Sharp && num2 > damageAmount.minimalVelocity * 4f)
		{
			Gib();
			BloodParticle();
		}
		else if (num2 > maxHealth * 0.6f)
		{
			BloodParticle();
			BleedOnObject(collision.collider);
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		AIGenericEntity aiGenEntity = my.AiCode.my.aiGenEntity;
		if ((StatMaster.isMP && (!aiGenEntity.isSimulating || !aiGenEntity.SimPhysics)) || my.AiCode.isDead || !base.enabled)
		{
			return false;
		}
		bool result = false;
		if ((mask & 4) != 0)
		{
			my.Rigidbody.AddExplosionForce(0f - power, explosionPos, radius, upPower);
			my.Rigidbody.AddExplosionForce(power * 0.4f, explosionPos, radius * 1.25f, upPower * 80f);
			if (my.AiCode.my.basicInfo.InWater)
			{
				float magnitude = (explosionPos - base.transform.position).magnitude;
				TakeDamage(power * (float)((!(magnitude < radius / 4f)) ? 1 : 3) * 2f, InjuryType.Blunt);
			}
			else
			{
				TakeDamage(my.Rigidbody.GetPointVelocity(my.Rigidbody.position).sqrMagnitude * 1100f, InjuryType.Blunt);
			}
			result = true;
		}
		my.AiCode.FallOver(false);
		return result;
	}

	public void TakeDamage(float damage, InjuryType injuryType)
	{
		switch (injuryType)
		{
		case InjuryType.Sharp:
			my.AiCode.health -= damage * damageAmount.SharpScale;
			BloodOnHit();
			break;
		case InjuryType.Blunt:
			if ((bool)my.AiCode.my.aiGenEntity && my.AiCode.my.aiGenEntity.BeingVacuumed)
			{
				my.AiCode.health -= damage * damageAmount.BluntVacuumeScale;
			}
			else
			{
				my.AiCode.health -= damage * damageAmount.BluntScale;
			}
			break;
		case InjuryType.Fire:
			my.AiCode.health -= damage * damageAmount.FireScale;
			break;
		}
		if (my.AiCode.health < 1f)
		{
			KillUnit(false, injuryType);
		}
		else if (updateBloodTextureOnHit && (injuryType == InjuryType.Sharp || injuryType == InjuryType.Blunt))
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			materialPropertyBlock.SetFloat("_BloodAmount", 1f - my.AiCode.health / maxHealth);
			if (my.hasRenderer)
			{
				my.Renderer.SetPropertyBlock(materialPropertyBlock);
			}
		}
	}

	public void BloodOnHit()
	{
		if (my.AiCode.my.basicInfo.InWater)
		{
			return;
		}
		if (StatMaster.isMP && StatMaster.isHosting && !StatMaster.isLocalSim && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.BloodBurstHit);
		}
		if (!NetworkBlock.applyingState)
		{
			if (OptionsMaster.BesiegeConfig.BloodEnabled)
			{
				GlobalParticles.EmitParticleBursts(11, base.transform.position);
			}
			else
			{
				GlobalParticles.EmitParticleBursts(13, base.transform.position);
			}
		}
	}

	public void KillUnit(bool jump, InjuryType type)
	{
		if (!my.AiCode.isDead)
		{
			activeType = type;
			Killed(jump);
			if (StatMaster.isMP && simPhys && levelEntity != null)
			{
				levelEntity.Event(NetworkEntity.EntityEvent.AIKilled, (byte)type);
			}
		}
	}

	public void Killed(bool jump)
	{
		if (my.AiCode.isDead)
		{
			return;
		}
		my.SoundController.Play();
		my.AiCode.isDead = true;
		if (StatMaster.GodTools.GravityDisabled)
		{
			jump = false;
		}
		switch (activeType)
		{
		case InjuryType.Blunt:
		case InjuryType.Sharp:
		case InjuryType.Crushed:
			BloodParticle();
			break;
		}
		KillMe(jump);
		if (!StatMaster.isMP || simPhys)
		{
			my.Rigidbody.AddForceAtPosition(base.transform.forward - Vector3.up, base.transform.up);
			if (!UseGibPrefab)
			{
				base.gameObject.tag = "Untagged";
			}
		}
	}

	public void KillMe(bool jump)
	{
		if (activeType != InjuryType.Fire && activeType != InjuryType.Suffocateing)
		{
			BloodTextureSwap();
		}
		else if (activeType == InjuryType.Crushed)
		{
			activeType = InjuryType.Blunt;
			UseGibPrefab = true;
		}
		AddToPercentageBar();
		DestroyOnDie();
		if ((bool)my.AiCode)
		{
			if (jump)
			{
				my.AiCode.Die();
			}
			else
			{
				my.AiCode.DieNoJump();
			}
		}
		if (UseGibPrefab)
		{
			Gib();
			return;
		}
		if ((bool)my.Poser)
		{
			my.Poser.KillPose();
		}
		if (!StatMaster.isMP || simPhys)
		{
			my.Rigidbody.ResetCenterOfMass();
		}
	}

	public void Gib()
	{
		if (gibbed || !UseGibPrefab)
		{
			return;
		}
		if (GettingGibbed != null)
		{
			GettingGibbed();
		}
		if (!object.ReferenceEquals(my.bomb, null) && !my.bomb.hasExploded)
		{
			my.bomb.Explodey();
		}
		if (!gibbed && !NetworkBlock.applyingState)
		{
			if (OptionsMaster.BesiegeConfig.BloodEnabled)
			{
				if (my.GibPrefab == null)
				{
					Debug.LogError("[KillingHandler] Gib Using fallback gib for " + Machine.GetObjectPath(base.gameObject) + " since the gib prefab is null!");
					my.GibPrefab = ReferenceMaster.Instance.goreGib;
				}
				gibbed = true;
				if (!StatMaster.isMP)
				{
					GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(my.GibPrefab, base.transform.position, base.transform.rotation, my.PhysicsGoal);
					gameObject.transform.localScale = base.transform.localScale;
				}
				else if (StatMaster.isMP && levelEntity != null)
				{
					GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(my.GibPrefab, base.transform.position, base.transform.rotation, my.PhysicsGoal);
					gameObject2.transform.localScale = base.transform.localScale;
					levelEntity.BreakIntoChildren(gameObject2.transform);
				}
			}
			else if ((bool)my.corpseDust)
			{
				gibbed = true;
				UnityEngine.Object.Instantiate(my.corpseDust, base.transform.position, base.transform.rotation, my.PhysicsGoal);
			}
		}
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(DelayedDeactivate());
		}
	}

	private IEnumerator DelayedDeactivate()
	{
		yield return null;
		base.gameObject.SetActive(false);
	}

	public void FireKill()
	{
		Kill();
	}

	public void Kill()
	{
		if (!object.ReferenceEquals(OnDeath, null))
		{
			OnDeath(this);
		}
		CauseOfDeath(activeType);
		if (RealtimeUpdaterCode == null)
		{
			RealtimeUpdaterCode = RealtimeUpdater.Instance;
		}
		if (!StatMaster.isMP && causeOnScreenUpdate && RealtimeUpdaterCode != null)
		{
			if (my.AiCode.deathDiscription != string.Empty)
			{
				deathBy = my.AiCode.deathDiscription;
			}
			if (string.IsNullOrEmpty(deathBy))
			{
				RealtimeUpdaterCode.AddBox(my.AiCode.nickname, my.AiCode.fullName, activeType);
			}
			else
			{
				RealtimeUpdaterCode.AddBox(my.AiCode.nickname, my.AiCode.fullName, activeType, deathBy);
			}
		}
	}

	protected void DestroyOnDie()
	{
		for (int i = 0; i < destroyOnDie.Length; i++)
		{
			UnityEngine.Object.Destroy(destroyOnDie[i].gameObject);
		}
	}

	protected void CauseOfDeath(InjuryType death)
	{
		if (LocalisationManager.UsingDefault())
		{
			switch (death)
			{
			case InjuryType.Sharp:
				RandomSharp();
				break;
			case InjuryType.Blunt:
				RandomBlunt();
				break;
			case InjuryType.Fire:
				RandomFire();
				break;
			default:
				deathBy = string.Empty;
				break;
			}
		}
		else
		{
			deathBy = string.Empty;
		}
	}

	public void BleedOnObject(Collider other)
	{
		if (OptionsMaster.BesiegeConfig.BloodEnabled && (bool)other.attachedRigidbody)
		{
			BlockBehaviour component = other.attachedRigidbody.GetComponent<BlockBehaviour>();
			if (component != null)
			{
				component.BloodSplatter();
			}
		}
	}

	public void BloodParticle()
	{
		if (StatMaster.isMP && StatMaster.isHosting && !StatMaster.isLocalSim && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.BloodParticle);
		}
		if (NetworkBlock.applyingState)
		{
			return;
		}
		if (OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			if (my.AiCode.my.basicInfo.InWater && my.AiCode.my.basicInfo.submergedPercent > 0.45f)
			{
				GlobalParticles.EmitParticleBursts(6, base.transform.position);
			}
			else
			{
				GlobalParticles.EmitParticleBursts(5, base.transform.position);
			}
		}
		else
		{
			GlobalParticles.EmitParticleBursts(12, base.transform.position);
		}
	}

	public void BloodTextureSwap()
	{
		if (!OptionsMaster.BesiegeConfig.BloodEnabled || !my.hasRenderer)
		{
			return;
		}
		if (my.BloodyTexture.Length > 0)
		{
			Material[] materials = my.Renderer.materials;
			for (int i = 0; i < materials.Length; i++)
			{
				if ((bool)my.BloodyTexture[i])
				{
					materials[i].mainTexture = my.BloodyTexture[i];
				}
			}
		}
		else
		{
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			materialPropertyBlock.SetColor("_BloodColor", StatMaster.BloodColor);
			materialPropertyBlock.SetFloat("_BloodAmount", 1f);
			my.Renderer.SetPropertyBlock(materialPropertyBlock);
		}
	}

	protected void PlaySuffocateingSounds()
	{
		if (!my.SoundController.audioSource.isPlaying && my.AiCode.my.basicInfo.submergedPercent < 1f)
		{
			my.SoundController.Play3(0.2f);
		}
	}

	protected void RandomSharp()
	{
		int num = 0;
		num = Mathf.RoundToInt(UnityEngine.Random.Range(0, sharpDeaths.Length - 1));
		deathBy = sharpDeaths[num];
	}

	protected void RandomBlunt()
	{
		int num = 0;
		num = Mathf.RoundToInt(UnityEngine.Random.Range(0, bluntDeaths.Length - 1));
		deathBy = bluntDeaths[num];
	}

	protected void RandomFire()
	{
		int num = 0;
		num = Mathf.RoundToInt(UnityEngine.Random.Range(0, fireDeaths.Length - 1));
		deathBy = fireDeaths[num];
	}

	public void AddToPercentageBar()
	{
		if (base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted += my.AiCode.victoryValue;
		}
		Kill();
	}
}
