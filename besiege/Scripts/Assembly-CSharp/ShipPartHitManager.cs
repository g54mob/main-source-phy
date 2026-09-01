using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartHitManager : BreakBase, IExplosionEffect
{
	private enum VictoryObjectiveType
	{
		Damage = 0,
		Sinking = 1,
		Points = 2
	}

	public bool isBottomPart;

	[Header("References")]
	public ShipDamageController controller;

	public MeshFilter mesh;

	public Mesh[] breakingStates = new Mesh[0];

	public GameObject breakPrefab;

	[HideInInspector]
	public Transform BrokenInstance;

	public Collider[] colliders;

	public GameObject[] destroyOnBreak;

	[Header("FX")]
	public AudioSource hitAudio;

	public ParticleSystem[] contactHitParticles;

	public ParticleSystem[] hitParticles;

	public GameObject jointBreakParticle;

	[Header("Joints")]
	private float[] jointForces;

	private float[] jointTorques;

	public List<Joint> joints = new List<Joint>();

	[HideInInspector]
	public List<Rigidbody> connectedBodies = new List<Rigidbody>();

	public List<Joint> jointsNoPart = new List<Joint>();

	[Header("Damage")]
	public bool nonCompartmentalizedSinking;

	public bool releaseAttachments;

	private float originalDensity;

	public float hitPoints = 10f;

	private float maxHits = 10f;

	private float waterTakenIn;

	private float sinkingDensity = 0.5f;

	public float forceToDamage;

	private float forceToDamageSqr;

	public bool partiallySinkBasedOnDamage;

	public float damagedDensity = 10f;

	public float brokenOffDensity = 10f;

	public float timeToSink = 1f;

	public float neighborBrokenDensity = 0.75f;

	public float damagedJointForce = 10000f;

	public float damagedJointTorque = 10000f;

	private float lerpTime;

	public bool sinking;

	private float rehitTimer;

	private int currentBreakState = -1;

	public ExplosiveProperty explosiveProperty = ExplosiveProperty.ForceBreaking;

	public bool useJointDamage;

	private int pointsAdded;

	[SerializeField]
	private int valuePoints = 5;

	private int pointsExpected;

	protected override void Awake()
	{
		forceToDamageSqr = forceToDamage * forceToDamage;
		maxHits = hitPoints;
		if (breakingStates.Length == 0)
		{
			breakingStates = new Mesh[1] { mesh.sharedMesh };
		}
		originalDensity = basicInfo.density;
		jointForces = new float[joints.Count];
		jointTorques = new float[joints.Count];
		for (int i = 0; i < jointForces.Length; i++)
		{
			float num = joints[i].breakForce;
			if (float.IsInfinity(num))
			{
				num = 100000f;
			}
			jointForces[i] = num;
			num = joints[i].breakTorque;
			if (float.IsInfinity(num))
			{
				num = 100000f;
			}
			jointTorques[i] = num;
		}
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		if (!basicInfo.isSimulating && !StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.Instance.objectiveObjectCount += valuePoints - 1;
		}
		connectedBodies.Clear();
		for (int i = 0; i < joints.Count; i++)
		{
			if (joints[i].connectedBody == basicInfo.Rigidbody)
			{
				connectedBodies.Add(joints[i].GetComponent<Rigidbody>());
			}
			else
			{
				connectedBodies.Add(joints[i].connectedBody);
			}
		}
	}

	private void Update()
	{
		rehitTimer -= Time.deltaTime;
	}

	public void OnJointBreak(float breakForce)
	{
		controller.OnPartJointBreak(this);
	}

	private void SpawnBrokeOffParticle()
	{
		if (jointBreakParticle != null)
		{
			UnityEngine.Object.Instantiate(jointBreakParticle, base.transform.position, base.transform.rotation, base.transform.parent);
		}
	}

	private void CleanJoints()
	{
		List<Joint> list = new List<Joint>(joints);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (list[num] == null)
			{
				joints.RemoveAt(num);
				connectedBodies.RemoveAt(num);
			}
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (!base.enabled)
		{
			return;
		}
		Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;
		bool flag = attachedRigidbody != null;
		if (flag && attachedRigidbody.CompareTag("CannonballBrittle") && !StatMaster.isMP)
		{
			UnityEngine.Object.Destroy(attachedRigidbody.gameObject);
		}
		else
		{
			if (!(rehitTimer <= 0f) || !(collision.relativeVelocity.sqrMagnitude > forceToDamageSqr))
			{
				return;
			}
			if (flag)
			{
				if (attachedRigidbody.CompareTag("Projectile"))
				{
					return;
				}
				if (attachedRigidbody.CompareTag("Cannonball"))
				{
					if (!StatMaster.isMP)
					{
						attachedRigidbody.gameObject.tag = "CannonballBrittle";
					}
				}
				else if (attachedRigidbody.CompareTag("DamageIgnored") || attachedRigidbody.CompareTag("Debris"))
				{
					return;
				}
			}
			for (int i = 0; i < contactHitParticles.Length; i++)
			{
				contactHitParticles[i].transform.position = collision.contacts[0].point;
				if (!contactHitParticles[i].isPlaying)
				{
					contactHitParticles[i].Play();
				}
			}
			ShipHit(1);
			rehitTimer = 0.05f;
		}
	}

	public void ShipPartialDamage(float damage)
	{
		hitPoints -= damage;
		CheckDamageState();
	}

	public void ShipHit(int damage)
	{
		if (!hitAudio.isPlaying)
		{
			hitAudio.Play();
		}
		hitPoints -= damage;
		CheckDamageState();
	}

	private void CheckDamageState()
	{
		CleanJoints();
		SetBreakingState();
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			VictoryObjectiveSet(VictoryObjectiveType.Damage, 0f);
		}
		if (nonCompartmentalizedSinking)
		{
			controller.SinkAll(false);
		}
		if (StatMaster.isHosting && basicInfo.SimPhysics)
		{
			if (basicInfo.NetBlock != null)
			{
				float value = 1f - hitPoints / maxHits;
				basicInfo.NetBlock.Event(NetworkEntity.EntityEvent.SetDamageLevel, (byte)(Mathf.Clamp01(value) * 255f));
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		if (joints.Count > 0)
		{
			if (hitPoints <= 0f)
			{
				BreakAllJoints();
				controller.OnPartJointBreak(this);
				BreakFully();
			}
		}
		else if (hitPoints <= 0f)
		{
			BreakFully();
		}
	}

	private IEnumerator AbsorbProjectile(Rigidbody r, Collider c, Vector3 oldVelocity)
	{
		for (int i = 0; i < colliders.Length; i++)
		{
			Physics.IgnoreCollision(c, colliders[i], true);
		}
		yield return new WaitForFixedUpdate();
		r.velocity = oldVelocity;
	}

	private void BreakAllJoints()
	{
		for (int i = 0; i < joints.Count; i++)
		{
			if (joints[i] != null && !string.IsNullOrEmpty(joints[i].gameObject.scene.name))
			{
				joints[i].breakForce = 0f;
				UnityEngine.Object.Destroy(joints[i]);
			}
		}
		joints.Clear();
		connectedBodies.Clear();
	}

	public void BreakFully()
	{
		if (!(breakPrefab != null))
		{
			return;
		}
		BrokenInstance = (UnityEngine.Object.Instantiate(breakPrefab, base.transform.position, base.transform.rotation, base.transform.parent) as GameObject).transform;
		for (int i = 0; i < destroyOnBreak.Length; i++)
		{
			if (destroyOnBreak[i] != null)
			{
				UnityEngine.Object.Destroy(destroyOnBreak[i]);
			}
		}
		if (StatMaster.isHosting && basicInfo.SimPhysics)
		{
			if (basicInfo.NetBlock != null)
			{
				basicInfo.NetBlock.Event(NetworkEntity.EntityEvent.Break);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		base.gameObject.SetActive(false);
		if (OnBreakTrigger != null)
		{
			OnBreakTrigger(this);
		}
	}

	public void SetWaterTaken(float pct)
	{
		waterTakenIn = pct;
		SetDensity(pct);
	}

	public void SetDensity(float pct)
	{
		if (!sinking)
		{
			float num = ((!partiallySinkBasedOnDamage) ? 1f : Mathf.Max(0f, hitPoints / maxHits));
			pct = Mathf.Min(waterTakenIn, num, pct);
			float b = Mathf.Lerp(damagedDensity, originalDensity, pct);
			basicInfo.density = Mathf.Max(sinkingDensity, b);
			VictoryObjectiveSet(VictoryObjectiveType.Sinking, basicInfo.density);
		}
	}

	public void ReleaseAttachments()
	{
		if (!releaseAttachments)
		{
			return;
		}
		for (int num = jointsNoPart.Count - 1; num >= 0; num--)
		{
			Joint joint = jointsNoPart[num];
			jointsNoPart.RemoveAt(num);
			if (joint != null)
			{
				joint.breakForce = 0f;
				joint.breakTorque = 0f;
			}
		}
	}

	private void SetBreakingState()
	{
		if (hitPoints > maxHits - 1f)
		{
			return;
		}
		for (int i = 0; i < hitParticles.Length; i++)
		{
			if (!hitParticles[i].isPlaying)
			{
				hitParticles[i].Play();
			}
		}
		int num = breakingStates.Length - 1;
		float num2 = Mathf.Max(0f, hitPoints / maxHits);
		int num3 = Mathf.FloorToInt((1f - num2) * (float)num);
		if (partiallySinkBasedOnDamage)
		{
			SetDensity(num2);
		}
		SetJointStrengths(num2);
		if (basicInfo.MeshRenderer.materials.Length > 1)
		{
			basicInfo.MeshRenderer.materials = new Material[1] { basicInfo.MeshRenderer.materials[0] };
		}
		if (currentBreakState != num3)
		{
			currentBreakState = num3;
			mesh.sharedMesh = breakingStates[num3];
		}
	}

	private void SetJointStrengths(float pct)
	{
		if (!useJointDamage)
		{
			return;
		}
		for (int i = 0; i < joints.Count; i++)
		{
			float num = Mathf.Lerp(damagedJointForce, jointForces[i], pct);
			Debug.Log("noo: " + num);
			if (joints[i].breakForce > num)
			{
				joints[i].breakForce = num;
			}
			num = Mathf.Lerp(damagedJointTorque, jointTorques[i], pct);
			if (joints[i].breakTorque > num)
			{
				joints[i].breakTorque = num;
			}
		}
	}

	public void Sink(bool completly = true)
	{
		if (completly)
		{
			nonCompartmentalizedSinking = false;
		}
		if (!sinking)
		{
			sinking = true;
			if (base.gameObject.activeSelf)
			{
				StartCoroutine(LerpDensity(completly));
			}
			ReleaseAttachments();
		}
	}

	private IEnumerator LerpDensity(bool completly)
	{
		float originalDensity = basicInfo.density;
		float max = maxHits + 0.1f;
		float hp = ((!completly) ? Mathf.Max(0f, hitPoints / max) : 0f);
		float sinkDensity = damagedDensity;
		if (hp > 0.999f)
		{
			sinking = false;
			yield break;
		}
		for (lerpTime = timeToSink; lerpTime > 0f; lerpTime -= Time.deltaTime)
		{
			hp = ((!completly) ? Mathf.Max(0f, hitPoints / max) : 0f);
			sinkDensity = ((!nonCompartmentalizedSinking) ? brokenOffDensity : Mathf.Lerp(damagedDensity, originalDensity, hp));
			float pct = lerpTime / timeToSink;
			basicInfo.density = Mathf.Lerp(sinkDensity, originalDensity, pct);
			sinkingDensity = basicInfo.density;
			VictoryObjectiveSet(VictoryObjectiveType.Sinking, basicInfo.density);
			yield return null;
		}
		basicInfo.density = sinkDensity;
		sinkingDensity = basicInfo.density;
		sinking = false;
	}

	public void BreakOff()
	{
		Vector3 center = controller.steering.Center;
		Vector3 vector = base.transform.position - center;
		vector = vector.normalized + new Vector3(0f, vector.y * 0.4f, 0f);
		basicInfo.Rigidbody.AddForce(vector * 5f, ForceMode.VelocityChange);
		timeToSink *= 0.25f;
		AffectNeighbors(neighborBrokenDensity);
		SpawnBrokeOffParticle();
	}

	public void AffectNeighbors(float affect = 0.5f)
	{
		ShipPartHitManager shipPartHitManager = null;
		for (int i = 0; i < connectedBodies.Count; i++)
		{
			if (!(connectedBodies[i] != null))
			{
				continue;
			}
			shipPartHitManager = connectedBodies[i].GetComponent<ShipPartHitManager>();
			if (shipPartHitManager != null)
			{
				Vector2 vector = new Vector2(base.transform.position.x - shipPartHitManager.transform.position.x, base.transform.position.z - shipPartHitManager.transform.position.z);
				float num = vector.magnitude / 20f;
				if (shipPartHitManager.partiallySinkBasedOnDamage)
				{
					shipPartHitManager.SetDensity(affect);
				}
				shipPartHitManager.timeToSink *= num;
			}
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.enabled || !basicInfo.isSimulating || !basicInfo.SimPhysics)
		{
			return false;
		}
		if ((mask & ReferenceMaster.EnumToInt((int)explosiveProperty)) != 0 && (mask & 1) != 0)
		{
			ShipHit(2);
			return true;
		}
		return false;
	}

	private void VictoryObjectiveSet(VictoryObjectiveType type, float value)
	{
		if (!StatMaster.isMP && basicInfo.isSimulating && basicInfo.SimPhysics && base.gameObject.CompareTag("ObjectiveObj"))
		{
			int num = 0;
			switch (type)
			{
			case VictoryObjectiveType.Sinking:
			{
				float num2 = ((!nonCompartmentalizedSinking) ? brokenOffDensity : damagedDensity);
				pointsExpected = Mathf.RoundToInt((value - originalDensity) / (num2 - originalDensity) * (float)valuePoints);
				num = pointsExpected - pointsAdded;
				break;
			}
			case VictoryObjectiveType.Points:
				num = Mathf.RoundToInt(value - (float)pointsAdded);
				break;
			case VictoryObjectiveType.Damage:
				pointsExpected = Mathf.RoundToInt((maxHits - hitPoints) / maxHits * (float)valuePoints);
				num = pointsExpected - pointsAdded;
				break;
			}
			if (num >= 0)
			{
				WinCondition.currentObjsCompleted += num;
				pointsAdded += num;
			}
		}
	}

	private void OnDisable()
	{
		VictoryObjectiveSet(VictoryObjectiveType.Points, valuePoints);
	}
}
