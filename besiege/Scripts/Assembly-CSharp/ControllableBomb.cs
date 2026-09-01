using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/ControllableBomb")]
public class ControllableBomb : BlockBehaviour, IExplosionEffect, IFireEffect
{
	public int version = 1;

	public SphereCollider col;

	public float radius = 5f;

	public float power = 10f;

	public float upPower = 3f;

	public LayerMask mask;

	public AudioSource fuseSfx;

	public ParticleSystem fuseParticles;

	public ParticleSystem[] particles;

	public ParticleSystem[] particlesUnderwater;

	public RandomSoundController SFX;

	public Transform dustCraterQuad;

	public float randomDelay = 0.08f;

	public float collisionExplodeThreshold = 200f;

	public bool hasIgnited;

	public bool hasExploded;

	public Renderer thisRenderer;

	public Collider thisCollider;

	private Vector3 explosionPos;

	private Collider[] hitColliders;

	private Rigidbody colAttachedRigidbody;

	private HashSet<Rigidbody> prevRigidbodies = new HashSet<Rigidbody>();

	private CustomLevel level;

	private static Material[] particleMats;

	private static bool hasMatsController;

	private MKey detonateKey;

	private MSlider fuseDelay;

	private float fuse;

	public MKey DetonateKey
	{
		get
		{
			return detonateKey;
		}
	}

	public MSlider FuseDelay
	{
		get
		{
			return fuseDelay;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (!isSimulating || SimPhysics)
		{
			detonateKey = AddKey(2493, "detonate", ControlScheme.BlockControls.Grenade, 0, KeyCode.K);
			fuseDelay = AddSlider(3781, "fusedelay", 0f, 0f, 10f, string.Empty, "s");
			fuseDelay.logScaling = true;
			fuseDelay.ValueChanged += SetFuse;
			power *= 2f;
			upPower *= 0.25f;
		}
	}

	public void SetFuse(float f)
	{
		fuse = f;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates((!isSimulating) ? Prefab.RegisterSimUpdate : SimPhysics, Prefab.RegisterSimFixedUpdate, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if ((!isSimulating || SimPhysics) && detonateKey.IsPressed)
		{
			InvokeExplode();
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (detonateKey.EmulationPressed())
		{
			InvokeExplode();
		}
	}

	protected override void OnDestroy()
	{
		if (hasExploded && WaterController.Exist)
		{
			hasMatsController = false;
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Remove(WaterFogController.UnderwaterToggled, new Action<bool>(SetUnderwater));
		}
		base.OnDestroy();
	}

	public void SetUnderwater(bool under)
	{
		for (int i = 0; i < particles.Length; i++)
		{
			particleMats[i].renderQueue = (under ? 2999 : 3001);
		}
	}

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		if (hasIgnited)
		{
			fuse -= Time.fixedDeltaTime * Mathf.Lerp(20f, 50f, t.lastIntensity);
		}
		else
		{
			InvokeExplode();
		}
		return !hasExploded && !StatMaster.Rules.DisableExplosions;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (SimPhysics && hasIgnited && !hasExploded && isSimulating && other.collider.gameObject.layer != 2 && other.relativeVelocity.sqrMagnitude > collisionExplodeThreshold)
		{
			fuse = 0f;
			ExplodeMessage();
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!isSimulating || !SimPhysics)
		{
			return false;
		}
		if ((mask & 0x20) != 0)
		{
			fuse = 0f;
			InvokeExplode();
			return true;
		}
		return false;
	}

	private void InvokeExplode()
	{
		if (!hasIgnited)
		{
			hasIgnited = true;
			StartCoroutine(Explode());
		}
	}

	private IEnumerator Explode()
	{
		if (hasExploded)
		{
			yield break;
		}
		fuseSfx.Play();
		fuseParticles.Play();
		if (fuse > 0f)
		{
			float r = UnityEngine.Random.Range(0f, 0.15f);
			for (float t = 0f; t < fuse + r; t += Time.fixedDeltaTime)
			{
				yield return new WaitForFixedUpdate();
			}
		}
		else if (!float.IsNaN(fuse))
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, randomDelay));
		}
		fuseSfx.Stop();
		fuseParticles.Stop();
		ExplodeMessage();
	}

	public void ExplodeMessage()
	{
		if (hasExploded || StatMaster.Rules.DisableExplosions)
		{
			return;
		}
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
					base.transform.position = netBlock.Position;
				}
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		ExplosionForce();
		PlayParticles();
		SFX.Play();
		DustCraterQuad();
		DisableComponents();
		hasExploded = true;
	}

	private void PlayParticles()
	{
		base.InWater = WaterController.IsUnderwater(GetCenter());
		if (base.InWater)
		{
			for (int i = 0; i < particlesUnderwater.Length; i++)
			{
				particlesUnderwater[i].transform.rotation = UnityEngine.Random.rotation;
				particlesUnderwater[i].Stop();
				particlesUnderwater[i].randomSeed = (uint)UnityEngine.Random.Range(0f, 9999999f);
				particlesUnderwater[i].Play();
			}
			return;
		}
		bool exist = WaterController.Exist;
		bool flag = false;
		if (exist && !hasMatsController)
		{
			flag = true;
			hasMatsController = true;
			WaterFogController.UnderwaterToggled = (Action<bool>)Delegate.Combine(WaterFogController.UnderwaterToggled, new Action<bool>(SetUnderwater));
			particleMats = new Material[particles.Length];
		}
		for (int j = 0; j < particles.Length; j++)
		{
			if (exist)
			{
				ParticleSystemRenderer component = particles[j].GetComponent<ParticleSystemRenderer>();
				if (flag)
				{
					particleMats[j] = component.material;
					particleMats[j].renderQueue = ((!WaterFogController.overWater) ? 2999 : 3001);
				}
				component.sharedMaterial = particleMats[j];
			}
			particles[j].transform.forward = Vector3.up;
			particles[j].Stop();
			particles[j].randomSeed = (uint)UnityEngine.Random.Range(0f, 9999999f);
			particles[j].Play();
		}
	}

	private void DisableComponents()
	{
		thisRenderer.enabled = false;
		UnityEngine.Object.Destroy(thisCollider);
		DestroyRigidbody();
	}

	private void ExplosionForce()
	{
		if (!SimPhysics)
		{
			return;
		}
		explosionPos = base.transform.position;
		hitColliders = Physics.OverlapSphere(explosionPos, radius, mask);
		int num = 237;
		Collider[] array = hitColliders;
		foreach (Collider collider in array)
		{
			colAttachedRigidbody = collider.attachedRigidbody;
			if (!colAttachedRigidbody || !(colAttachedRigidbody != Rigidbody) || prevRigidbodies.Contains(colAttachedRigidbody) || !(colAttachedRigidbody.tag != "KeepConstraintsAlways"))
			{
				continue;
			}
			colAttachedRigidbody.WakeUp();
			colAttachedRigidbody.constraints = RigidbodyConstraints.None;
			colAttachedRigidbody.AddExplosionForce(power, explosionPos, radius, upPower);
			prevRigidbodies.Add(colAttachedRigidbody);
			foreach (IExplosionEffect @interface in ReferenceMaster.GetInterfaces<IExplosionEffect>(colAttachedRigidbody.gameObject))
			{
				@interface.OnExplode(power, upPower, 0f, explosionPos, radius, num, base.InWater);
			}
		}
	}

	private void DustCraterQuad()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (dustCraterQuad == null)
		{
			Debug.LogWarning("ControllableBomb doesn't have a dust crater quad!");
			return;
		}
		Vector3 position = base.transform.position;
		float floorHeight = SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
		if (StatMaster.ShowExplosionDecals && position.y < floorHeight + 5f)
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = true;
			dustCraterQuad.parent = ReferenceMaster.physicsGoalInstance;
			dustCraterQuad.position = new Vector3(position.x, floorHeight + 0.025f, position.z);
			dustCraterQuad.forward = Vector3.up;
			dustCraterQuad.localEulerAngles = new Vector3(dustCraterQuad.localEulerAngles.x, dustCraterQuad.localEulerAngles.y, UnityEngine.Random.Range(0f, 360f));
		}
		else
		{
			dustCraterQuad.GetComponent<Renderer>().enabled = false;
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
				col.center = new Vector3(0.01116767f, 0.0055f, 0.7045408f);
				col.radius = 0.6077224f;
				Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
			}
			else
			{
				col.center = new Vector3(0f, 0f, 0.7f);
				col.radius = 0.6f;
				Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			}
		}
	}
}
