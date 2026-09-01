using System;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMagicZone : MonoBehaviour
{
	private class ParticleData
	{
		public ParticleSystem.Particle Particle;

		public float SpawnTime;

		public ParticleData()
		{
			Particle = default(ParticleSystem.Particle);
			Particle.randomSeed = (uint)Time.frameCount;
			SpawnTime = Time.time;
		}

		public bool UpdateLifetime()
		{
			Particle.lifetime = Mathf.Max(0f, Particle.startLifetime - (Time.time - SpawnTime));
			return Particle.lifetime > 0f;
		}

		public void Kill()
		{
			Particle.lifetime = 0f;
			ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
			{
				position = Particle.position,
				applyShapeToPosition = true
			};
			BreakParticle.Emit(emitParams, 20);
			BreakAudio.transform.position = Particle.position;
			BreakAudio.PlayOneShot(BreakAudio.clip);
		}
	}

	private const int MaxParticlesPerBody = 8;

	[Header("ice spawning")]
	public Rigidbody worldBody;

	public float radius = 40f;

	public LayerMask mask;

	public ParticleSystem[] particles;

	public AudioSource iceSFX;

	public float sfxVolume = 1f;

	public float audioFadeSpeed = 2f;

	public float freezeToFloorRange = 40f;

	public float floorFreezeHeight = 1f;

	public float timeToFreezeToFloor = 2f;

	public float breakForce = 3000f;

	public float breakTorque = 3000f;

	public float maxDrag = 60f;

	public float jointStrengthOverDistanceScale = 1f;

	private float overlapRadius = 40f;

	public Texture2D iceTex;

	private float introLerpSpeed = 4f;

	private float t;

	[Header("ice spawning")]
	public ParticleSystem iceSpawner;

	public ParticleSystem breakParticle;

	public AudioSource breakAudio;

	private Dictionary<Rigidbody, List<ParticleData>> iceParticles = new Dictionary<Rigidbody, List<ParticleData>>();

	private ParticleSystem.Particle[] temp;

	private ParticleSystem.Particle[] temp2;

	public Vector3 startSizeMin;

	public Vector3 startSizeMax;

	public Vector3 startRotationMin;

	public Vector3 startRotationMax;

	private static ParticleSystem BreakParticle;

	private static AudioSource BreakAudio;

	private Dictionary<Rigidbody, float> frozenBlocks = new Dictionary<Rigidbody, float>();

	private Dictionary<BlockBehaviour, ConfigurableJoint> frozenJoints = new Dictionary<BlockBehaviour, ConfigurableJoint>();

	private void Start()
	{
		BreakParticle = breakParticle;
		BreakAudio = breakAudio;
		frozenBlocks = new Dictionary<Rigidbody, float>();
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			Collider[] array = Physics.OverlapSphere(base.transform.position, overlapRadius, mask, QueryTriggerInteraction.Ignore);
			for (int i = 0; i < array.Length; i++)
			{
				OnOverlapEnter(array[i]);
			}
		}
	}

	private void OnOverlapEnter(Collider other)
	{
		if (!other.attachedRigidbody)
		{
			return;
		}
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		BlockBehaviour component = other.attachedRigidbody.gameObject.GetComponent<BlockBehaviour>();
		if (component == null)
		{
			return;
		}
		bool flag = false;
		if (!frozenBlocks.ContainsKey(attachedRigidbody))
		{
			frozenBlocks.Add(attachedRigidbody, Time.time + UnityEngine.Random.Range(0.5f, 1.5f));
		}
		else
		{
			if (!(frozenBlocks[attachedRigidbody] < Time.time))
			{
				return;
			}
			frozenBlocks[attachedRigidbody] = Time.time + UnityEngine.Random.Range(0.5f, 1.5f);
			flag = true;
			if (frozenJoints.ContainsKey(component))
			{
				ConfigurableJoint configurableJoint = frozenJoints[component];
				if (!(configurableJoint == null))
				{
					if (configurableJoint.breakForce < 20000f)
					{
						configurableJoint.breakForce *= 2f;
						configurableJoint.breakTorque *= 2f;
						FreezeParticle(attachedRigidbody);
					}
					return;
				}
				frozenJoints.Remove(component);
				foreach (ParticleData item in iceParticles[attachedRigidbody])
				{
					item.Kill();
				}
				iceParticles.Remove(attachedRigidbody);
			}
		}
		BlockVisualController visualController = component.VisualController;
		if (component.gotChildBlocks)
		{
			component.CreateSimLists();
			foreach (BlockBehaviour key in component.parentedColliders.Keys)
			{
				Freeze(key, key.GetCenter());
			}
		}
		if (Freeze(component, other.attachedRigidbody.worldCenterOfMass) && (bool)visualController)
		{
			t = ((!flag) ? 1f : 0.5f);
		}
	}

	private bool Freeze(BlockBehaviour block, Vector3 pos)
	{
		DebugExtension.DebugWireSphere(new Vector3(pos.x, block.LowestPoint, pos.z), Color.blue, 0.1f, 3f);
		if (block.LowestPoint < 0.25f)
		{
			ConfigurableJoint configurableJoint = worldBody.gameObject.AddComponent<ConfigurableJoint>();
			ConfigurableJointMotion configurableJointMotion = (configurableJoint.zMotion = ConfigurableJointMotion.Locked);
			configurableJointMotion = (configurableJoint.yMotion = configurableJointMotion);
			configurableJoint.xMotion = configurableJointMotion;
			configurableJointMotion = (configurableJoint.angularZMotion = ConfigurableJointMotion.Locked);
			configurableJointMotion = (configurableJoint.angularYMotion = configurableJointMotion);
			configurableJoint.angularXMotion = configurableJointMotion;
			Vector3 vector = pos - base.transform.position;
			float num = (1f - Mathf.Clamp01((vector.x * vector.x + vector.y * vector.y + vector.z * vector.z) / (overlapRadius * overlapRadius))) * jointStrengthOverDistanceScale + 1f;
			configurableJoint.breakForce = breakForce * num;
			configurableJoint.breakTorque = breakTorque * num;
			configurableJoint.anchor = pos;
			configurableJoint.autoConfigureConnectedAnchor = true;
			configurableJoint.connectedBody = block.Rigidbody;
			frozenJoints.Add(block, configurableJoint);
			FreezeParticle(block.Rigidbody);
		}
		else if (block.Prefab.canFreeze && block.iceTag.frozen)
		{
			return false;
		}
		if (block.Prefab.canFreeze)
		{
			block.iceTag.Freeze();
			PlayEffects(pos);
			return true;
		}
		return false;
	}

	private void FreezeParticle(Rigidbody r)
	{
		Vector3 worldCenterOfMass = r.worldCenterOfMass;
		worldCenterOfMass.y = 0.25f;
		if (!iceParticles.ContainsKey(r))
		{
			iceParticles.Add(r, new List<ParticleData>(8));
		}
		if (iceParticles[r].Count != 8)
		{
			ParticleData particleData = new ParticleData();
			particleData.Particle.lifetime = 1000f;
			particleData.Particle.startLifetime = 1000f;
			particleData.Particle.position = worldCenterOfMass;
			particleData.Particle.startSize3D = new Vector3(UnityEngine.Random.Range(startSizeMin.x, startSizeMax.x), UnityEngine.Random.Range(startSizeMin.y, startSizeMax.y), UnityEngine.Random.Range(startSizeMin.z, startSizeMax.z));
			particleData.Particle.rotation3D = new Vector3(UnityEngine.Random.Range(startRotationMin.x, startRotationMax.x), UnityEngine.Random.Range(startRotationMin.y, startRotationMax.y), UnityEngine.Random.Range(startRotationMin.z, startRotationMax.z));
			iceParticles[r].Add(particleData);
			RefreshIceParticles();
		}
	}

	private void RefreshIceParticles()
	{
		int num = 0;
		temp = new ParticleSystem.Particle[iceParticles.Count * 8];
		foreach (Rigidbody key in iceParticles.Keys)
		{
			List<ParticleData> list = iceParticles[key];
			int i;
			for (i = 0; i < list.Count; i++)
			{
				ParticleData particleData = list[i];
				if (particleData.UpdateLifetime())
				{
					temp[num++] = particleData.Particle;
					continue;
				}
				list.RemoveAt(i);
				i--;
			}
			if (i <= 0)
			{
				iceParticles.Remove(key);
			}
		}
		temp2 = new ParticleSystem.Particle[num];
		Array.Copy(temp, temp2, num);
		iceSpawner.SetParticles(temp2, num);
	}

	private void Update()
	{
		iceSFX.volume = Mathf.Lerp(iceSFX.volume, t * sfxVolume, introLerpSpeed * Time.deltaTime);
		t -= Time.deltaTime * audioFadeSpeed;
		t = Mathf.Clamp(t, 0f, 100000f);
	}

	private void PlayEffects(Vector3 pos)
	{
		ParticleSystem[] array = particles;
		foreach (ParticleSystem particleSystem in array)
		{
			particleSystem.transform.position = pos;
			particleSystem.Play();
		}
	}
}
