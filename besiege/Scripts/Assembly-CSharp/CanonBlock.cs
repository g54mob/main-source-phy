using System;
using System.Collections;
using System.Collections.Generic;
using Localisation;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/CanonBlock")]
public class CanonBlock : BlockBehaviour, IFireEffect
{
	public enum AmmoType
	{
		Round = 0,
		Chain = 1
	}

	public Transform[] boltObjects;

	public Transform selectedBoltObject;

	[HideInInspector]
	public Vector3 boltSpawnPos;

	[HideInInspector]
	public Quaternion boltSpawnRot;

	public bool shrapnel;

	public float boltSpeed = 100f;

	public float chainSpin = 10f;

	public float knockbackSpeed = 100f;

	public float interval = 0.05f;

	public float randomDelay = 0.1f;

	public ParticleSystem[] particles;

	public ParticleSystem fuseParticles;

	public bool stabilize = true;

	public AudioSource audioSource;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	public ReloadAnimation anim;

	protected float force = 1f;

	protected Quaternion lastRot;

	private List<Transform> spawnedProjectiles;

	private int ammo = 1;

	private AmmoType ammoType;

	private NetworkProjectileType networkProjectileType;

	private MKey shootKey;

	private MSlider strengthSlider;

	private MSlider projectileSlider;

	private MMenu ammunitionType;

	private bool useProjManager;

	private bool isShooting;

	protected float timeSinceLastShot;

	public int Ammo
	{
		get
		{
			return ammo;
		}
	}

	public MMenu AmmunitionType
	{
		get
		{
			return ammunitionType;
		}
	}

	public MSlider StrengthSlider
	{
		get
		{
			return strengthSlider;
		}
	}

	public MSlider ProjectileSlider
	{
		get
		{
			return projectileSlider;
		}
	}

	public MKey ShootKey
	{
		get
		{
			return shootKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		anim.Awake(this);
		shootKey = AddKey(2429, "shoot", ControlScheme.BlockControls.Cannons, 0, KeyCode.C);
		lastRot = base.transform.rotation;
		useProjManager = !shrapnel && StatMaster.isHosting && base.HasParentMachine && !base.ParentMachine.LocalSim;
		if (isSimulating)
		{
			mixer = audioSource.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
			if (!SimPhysics)
			{
				return;
			}
		}
		spawnedProjectiles = new List<Transform>();
		if (!shrapnel)
		{
			ammunitionType = AddMenu("Ammunition", 0, new List<string>
			{
				LocalisationManager.GetTranslation(5004),
				LocalisationManager.GetTranslation(5005)
			});
			ammunitionType.ValueChanged += OnAmmunitionChanged;
		}
		strengthSlider = AddSlider(2427, "strength", 1f, 0.1f, (!shrapnel) ? 3f : 4f, string.Empty);
		if (!shrapnel)
		{
			projectileSlider = AddSlider(4969, "projectile", 1f, 0.5f, 1.2f, string.Empty);
			if (!isSimulating)
			{
				projectileSlider.DisplayInMapper = StatMaster.advancedBuilding;
				ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Combine(ReferenceMaster.onAdvancedBuildingToggled, new Action(DisplayProjectileSlider));
			}
		}
	}

	public void DisplayProjectileSlider()
	{
		projectileSlider.DisplayInMapper = StatMaster.advancedBuilding;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (shootKey.IsPressed)
		{
			Shoot();
		}
		timeSinceLastShot += Time.deltaTime;
		lastRot = base.transform.rotation;
	}

	public override void EmulationUpdateBlock()
	{
		if (shootKey.EmulationPressed())
		{
			Shoot();
		}
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		if (isShooting)
		{
			return false;
		}
		Shoot();
		return (base.HasParentMachine && base.ParentMachine.InfiniteAmmoMode) || ammo > 0;
	}

	public void Shoot()
	{
		if (isShooting)
		{
			return;
		}
		if (SimPhysics)
		{
			force = strengthSlider.Value;
			if (float.IsNaN(force))
			{
				force = 0f;
			}
		}
		base.ParentMachine.hasFiredProjectiles = true;
		StartCoroutine(IEShoot());
	}

	public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock, bool playAnim = true)
	{
		if (type != ReloadAmmoType.All && type != ReloadAmmoType.Cannon)
		{
			return;
		}
		if (setAmmo)
		{
			if (eachBlock || units < 1)
			{
				ammo = units;
				units = 0;
			}
			else
			{
				units--;
				ammo = 1;
			}
			if (playAnim)
			{
				anim.AnimateReload((!shrapnel) ? ammo : ((ammo > 0) ? 3 : 0));
			}
			return;
		}
		int num = 0;
		if (eachBlock || units <= 1 - ammo)
		{
			ammo += units;
			num = units;
			units = 0;
		}
		else
		{
			num = 1 - ammo;
			units -= num;
			ammo = 1;
		}
		if (playAnim)
		{
			anim.AnimateReload((!shrapnel) ? num : ((ammo > 0 && num != 0) ? 3 : 0));
		}
	}

	public IEnumerator IEShoot()
	{
		if (isShooting || !base.HasParentMachine)
		{
			yield break;
		}
		isShooting = true;
		if (SimPhysics)
		{
			while (!_parentMachine.isReady || Time.timeScale == 0f)
			{
				yield return null;
			}
			yield return new WaitForFixedUpdate();
		}
		if (!_parentMachine.InfiniteAmmoMode)
		{
			if (ammo == 0)
			{
				timeSinceLastShot = 0f;
				isShooting = false;
				yield break;
			}
		}
		else
		{
			StatMaster.GodTools.HasBeenUsed = true;
		}
		if (!shrapnel && _parentMachine.ExplodingCannonballs)
		{
			StatMaster.GodTools.HasBeenUsed = true;
		}
		ammo = ((ammo > 0) ? (ammo - 1) : 0);
		if ((bool)fuseParticles)
		{
			fuseParticles.Play();
		}
		float delay = 0f;
		Vector3 initialForward = -base.transform.up;
		Vector3 currentForward = initialForward;
		Vector3 predictedForward = initialForward;
		bool predictionIsBasicallyInitial = false;
		float remainingTime = 0f;
		float deadzoneInDegrees = 5f;
		float precision = Mathf.Cos((float)Math.PI / 180f * deadzoneInDegrees);
		if (SimPhysics)
		{
			float baseWait = ((!(timeSinceLastShot > interval)) ? (interval - timeSinceLastShot) : 0f);
			delay = UnityEngine.Random.Range(baseWait, baseWait + randomDelay - Time.fixedDeltaTime * 2f);
			if (stabilize)
			{
				predictionIsBasicallyInitial = true;
				Quaternion relativeRot = base.transform.rotation * Quaternion.Inverse(lastRot);
				for (float f = 0f; f < delay; f += Time.deltaTime)
				{
					predictedForward = relativeRot * predictedForward;
					float similarityToInitial = Vector3.Dot(predictedForward, initialForward);
					if (similarityToInitial < precision)
					{
						predictionIsBasicallyInitial = false;
					}
				}
			}
			for (float t = 0f; t < delay; t += Time.fixedDeltaTime)
			{
				currentForward = -base.transform.up;
				if (predictionIsBasicallyInitial)
				{
					float similarityToInitial2 = Vector3.Dot(currentForward, initialForward);
					if (similarityToInitial2 < precision)
					{
						remainingTime = delay - t;
						break;
					}
				}
				yield return new WaitForFixedUpdate();
			}
		}
		else if (base.HasParentMachine && _parentMachine.isLocalMachine)
		{
			yield return new WaitForSeconds((float)BesiegeNetworkManager.Instance.Ping / 1000f);
			yield return new WaitForFixedUpdate();
		}
		anim.StopReloadAnim();
		if ((bool)fuseParticles)
		{
			fuseParticles.Stop();
		}
		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
			particles[i].Play();
		}
		Vector3 globalSpawnPos = base.transform.TransformPoint(boltSpawnPos + Vector3.up * 0.3f);
		if (audioSource.gameObject.activeInHierarchy)
		{
			if (WaterController.Exist)
			{
				if (WaterController.IsUnderwater(globalSpawnPos))
				{
					audioSource.outputAudioMixerGroup = underwaterMixer;
				}
				else
				{
					audioSource.outputAudioMixerGroup = mixer;
				}
			}
			audioSource.pitch = 1f + UnityEngine.Random.Range(-0.05f, 0.05f);
			audioSource.Play();
		}
		if (StatMaster.Rules.DisableProjectiles)
		{
			yield break;
		}
		Transform bolt = null;
		if (SimPhysics)
		{
			currentForward = -base.transform.up;
			Vector3 projectileForward = (predictionIsBasicallyInitial ? initialForward : currentForward);
			Quaternion globalSpawnRot = base.transform.rotation * boltSpawnRot;
			float projSize = ((!shrapnel) ? Mathf.Clamp(ProjectileSlider.Value, 0.1f, 5f) : 1f);
			Vector3 globalSpawnScale = ((ammoType != AmmoType.Chain) ? (projSize * selectedBoltObject.localScale) : (projSize * Vector3.one));
			float mass = 2f;
			if (useProjManager)
			{
				byte[] spawnInfo = new byte[19];
				int offset = 0;
				NetworkCompression.CompressPosition(globalSpawnPos, spawnInfo, offset);
				offset += 6;
				NetworkCompression.CompressRotation(globalSpawnRot, spawnInfo, offset);
				offset += 7;
				NetworkCompression.CompressVector(globalSpawnScale, 0f, 100f, spawnInfo, offset);
				NetworkAddPiece addPiece = NetworkAddPiece.Instance;
				bolt = ProjectileManager.Instance.Spawn(networkProjectileType, addPiece.frame, _parentMachine.PlayerID, spawnInfo);
			}
			else
			{
				bolt = UnityEngine.Object.Instantiate(selectedBoltObject, globalSpawnPos, globalSpawnRot, ReferenceMaster.physicsGoalInstance) as Transform;
			}
			if (!shrapnel)
			{
				if (StatMaster.GodTools.ExplodingCannonballs)
				{
					CannonBallDamage cbd = bolt.GetComponent<CannonBallDamage>();
					if (cbd != null)
					{
						cbd.projectileScale = Mathf.Clamp(ProjectileSlider.Value, 0.1f, 5f);
					}
				}
				spawnedProjectiles.Add(bolt);
				Rigidbody boltRigidbody = bolt.GetComponent<Rigidbody>();
				Vector3 vel;
				if (predictionIsBasicallyInitial)
				{
					Vector3 projection = Vector3.Project(Rigidbody.velocity, initialForward);
					if (Vector3.Dot(projection, initialForward) < 0f)
					{
						projection = Rigidbody.velocity;
					}
					vel = projection;
				}
				else
				{
					vel = Rigidbody.GetPointVelocity(globalSpawnPos);
				}
				boltRigidbody.velocity = vel;
				MeshRenderer vis = bolt.GetComponent<MeshRenderer>();
				bolt.gameObject.layer = 8;
				vis.enabled = false;
				if (ammoType != AmmoType.Chain)
				{
					bolt.localScale = globalSpawnScale;
					boltRigidbody.mass = projSize * mass;
				}
				else
				{
					boltRigidbody.mass = projSize * mass * 0.75f + mass * 0.25f;
					boltRigidbody.GetComponent<CannonChainBall>().SetSize(projSize + UnityEngine.Random.Range(0f, 0.025f));
					boltRigidbody.AddRelativeTorque(Vector3.forward * force * chainSpin * ((!(UnityEngine.Random.value > 0.5f)) ? (-1f) : 1f), ForceMode.Force);
				}
				boltRigidbody.AddForce(projectileForward * 10f, ForceMode.VelocityChange);
				yield return new WaitForFixedUpdate();
				globalSpawnPos = base.transform.TransformPoint(boltSpawnPos);
				globalSpawnRot = base.transform.rotation * boltSpawnRot;
				boltRigidbody.MovePosition(globalSpawnPos);
				boltRigidbody.MoveRotation(globalSpawnRot);
				bolt.gameObject.layer = 0;
				vis.enabled = true;
				boltRigidbody.AddForce(projectileForward * (boltSpeed * force));
			}
			else
			{
				bolt.GetComponent<ShrapnelBoltControl>().Fire(0.1f + 1f * force - 0.1f * Mathf.Pow(force, 2f));
			}
			Rigidbody.AddForce(projectileForward * (0f - knockbackSpeed) * force);
		}
		if (remainingTime > 0f)
		{
			yield return new WaitForSeconds(remainingTime);
		}
		timeSinceLastShot = 0f;
		isShooting = false;
	}

	protected void OnAmmunitionChanged(int newState)
	{
		ammoType = (AmmoType)newState;
		if (boltObjects.Length < 2)
		{
			ammoType = AmmoType.Round;
		}
		switch (ammoType)
		{
		case AmmoType.Round:
			networkProjectileType = NetworkProjectileType.Cannon;
			selectedBoltObject = boltObjects[0];
			break;
		case AmmoType.Chain:
			networkProjectileType = NetworkProjectileType.ChainShot;
			selectedBoltObject = boltObjects[1];
			break;
		default:
			networkProjectileType = NetworkProjectileType.Cannon;
			selectedBoltObject = boltObjects[0];
			break;
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		StopAllCoroutines();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		ReferenceMaster.onAdvancedBuildingToggled = (Action)Delegate.Remove(ReferenceMaster.onAdvancedBuildingToggled, new Action(DisplayProjectileSlider));
		if (!SimPhysics)
		{
			return;
		}
		for (int i = 0; i < spawnedProjectiles.Count; i++)
		{
			Transform transform = spawnedProjectiles[i];
			if (transform == null)
			{
				continue;
			}
			GameObject gameObject = transform.gameObject;
			if (gameObject != null)
			{
				if (useProjManager)
				{
					ProjectileManager.Instance.Despawn(transform);
				}
				else
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
	}
}
