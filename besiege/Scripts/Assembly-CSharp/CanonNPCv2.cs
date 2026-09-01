using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/AI/CanonNPC v2")]
public class CanonNPCv2 : SimBehaviour, IParticlePlay
{
	private enum SteeringState
	{
		Idle = 0,
		Random = 1,
		Aim = 2
	}

	public Rigidbody body;

	public AnimationCurve torqueScale;

	public bool useTorqueScale;

	public Transform SpawnPosition;

	public GameObject projectilePrefab;

	public Transform baseObject;

	public Transform barrel;

	public RandomSoundController rsp;

	public ParticleSystem particles;

	public float rotatedMeshCorrection = 90f;

	public Axes barrelAxis = Axes.z;

	public bool inverseAngle;

	public float maxAngle = 30f;

	public float minAngle = -20f;

	public float activationDistance = 50f;

	public float shootingDotP = 0.93f;

	public bool ignoreDotY;

	public int ammo = 3;

	public int numberOfVolleys = 1;

	public float volleyInterval = 0.1f;

	public float randomiseShots;

	public bool forceVolleys;

	public bool explode;

	public float power = 30f;

	public float prediction;

	private float shootDelay;

	public bool fireAfterStartDelay;

	public float startDelay = 5f;

	public float shootDelayMin = 2f;

	public float shootDelayMax = 6f;

	public float rotationSpeed = 1f;

	public Vector3 barrelForward = new Vector3(-1f, 0f, 0f);

	public Vector3 baseUp = new Vector3(0f, 1f, 0f);

	public float dropCompensationPer100mInDegrees = 10f;

	public float dropBias = 1f;

	public bool useForceRotation = true;

	public float maxTorque = 20f;

	public bool breakOnTilt = true;

	public bool disableOnTilt;

	public bool randomRotateOnIdle;

	public bool clearVelocityOnNew = true;

	public Collider[] projectileIgnore;

	public float startTimer;

	private float timer;

	private bool hasTarget;

	private Quaternion cannonRotation;

	private Vector3 direction;

	private BlockBehaviour target;

	private float activationDistanceSqr;

	private Transform bolt;

	private bool broken;

	[HideInInspector]
	public bool jointBroken;

	public float breakAngle = 0.707f;

	private int currentVolley;

	private float volleyTimer;

	private LayerMask mask;

	private bool useProjManager;

	private ushort playerID;

	private float minInterval = 6f;

	private float maxInterval = 14f;

	private float intervalTimer;

	private Vector3 randomDirection = Vector3.forward;

	private bool shoot;

	private SteeringState steering;

	private Vector3 correctedDir = Vector3.up;

	public bool Broken
	{
		set
		{
			broken = value;
		}
	}

	protected Vector3 spawnDir
	{
		get
		{
			return barrel.TransformDirection(barrelForward);
		}
	}

	protected override void Start()
	{
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			base.Start();
			activationDistanceSqr = activationDistance * activationDistance;
			useProjManager = StatMaster.levelSimulating && StatMaster.isHosting && !StatMaster.isLocalSim && StatMaster.isMP;
			playerID = (ushort)(StatMaster.isMP ? BesiegeNetworkManager.Instance.PlayerID : 0);
			if (!fireAfterStartDelay)
			{
				shootDelay = UnityEngine.Random.Range(shootDelayMin, shootDelayMax);
			}
			else
			{
				shootDelay = UnityEngine.Random.Range(0f, 0.15f);
			}
			randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.35f, 0f), UnityEngine.Random.Range(-1f, 1f)).normalized;
			intervalTimer = UnityEngine.Random.Range(minInterval, maxInterval);
			if (base.isSimulating)
			{
				mask = AddPiece.CreateLayerMask(SingleInstanceFindOnly<AddPiece>.Instance.layerMasky, 4, 24, 28, 29);
			}
			if (startTimer != 0f)
			{
				timer = startTimer;
				shoot = true;
			}
		}
	}

	private void RandomisedRotation()
	{
		if (!randomRotateOnIdle)
		{
			return;
		}
		if (Vector3.Dot(body.transform.forward, correctedDir) > 0.95f)
		{
			intervalTimer -= Time.deltaTime;
			if (intervalTimer < 0f)
			{
				randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.35f, 0f), UnityEngine.Random.Range(-1f, 1f)).normalized;
				intervalTimer = UnityEngine.Random.Range(minInterval, maxInterval);
			}
		}
		Aim(randomDirection, 0.2f);
		steering = SteeringState.Random;
	}

	private void Update()
	{
		steering = SteeringState.Idle;
		if ((StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim) || !base.isSimulating || ammo <= 0 || (broken && !disableOnTilt))
		{
			return;
		}
		if (breakOnTilt && Vector3.Dot(body.transform.TransformDirection(baseUp), Vector3.up) < breakAngle)
		{
			broken = true;
			return;
		}
		if (disableOnTilt)
		{
			if (!broken && Vector3.Dot(body.transform.TransformDirection(baseUp), Vector3.up) < breakAngle)
			{
				broken = true;
				return;
			}
			if (broken)
			{
				broken = false;
			}
		}
		if (jointBroken)
		{
			return;
		}
		if (timer < shootDelay && ammo > 0)
		{
			timer += Time.deltaTime;
		}
		bool flag = false;
		if (startDelay > 0f)
		{
			startDelay -= Time.deltaTime;
			flag = true;
		}
		if (!hasTarget || target.isDestroyed)
		{
			GetTarget();
			return;
		}
		if (hasTarget && target.Prefab.hasHealthBar && target.BlockHealth.health == 0f && !GetTarget() && !shoot)
		{
			RandomisedRotation();
			return;
		}
		direction = GetDir(base.transform.position);
		if (direction.sqrMagnitude > activationDistanceSqr)
		{
			RandomisedRotation();
			hasTarget = false;
			return;
		}
		Aim(direction);
		if (flag)
		{
			return;
		}
		if (timer >= shootDelay)
		{
			Vector3 vector = direction;
			Vector3 vector2 = spawnDir;
			Vector3 dir = GetDir(base.transform.position + body.velocity * Time.deltaTime);
			if (ignoreDotY)
			{
				vector.y = 0f;
				vector2.y = 0f;
				dir.y = 0f;
			}
			vector2 = vector2.normalized;
			float num = Vector3.Dot(vector.normalized, vector2);
			if (num < Vector3.Dot(dir.normalized, vector2) && !shoot)
			{
				Debug.DrawRay(SpawnPosition.position, vector2 * 5f, Color.red);
				return;
			}
			if ((num > shootingDotP && CanSee(target.transform)) || (forceVolleys && currentVolley > 0))
			{
				if (currentVolley < numberOfVolleys)
				{
					if (currentVolley == 0 || volleyTimer > volleyInterval)
					{
						currentVolley++;
						volleyTimer = 0f;
						shoot = true;
					}
				}
				else if (currentVolley >= numberOfVolleys)
				{
					currentVolley = 0;
					timer = 0f;
					shootDelay = UnityEngine.Random.Range(shootDelayMin, shootDelayMax);
				}
			}
		}
		if (shoot)
		{
			ammo--;
			Shoot();
			shoot = false;
		}
		volleyTimer += Time.deltaTime;
	}

	private void FixedUpdate()
	{
		switch (steering)
		{
		case SteeringState.Idle:
			break;
		case SteeringState.Random:
			Rotate(maxTorque * 0.5f, 0.2f);
			break;
		case SteeringState.Aim:
			Rotate(maxTorque);
			break;
		}
	}

	private Vector3 GetDir(Vector3 start)
	{
		if (prediction > 0f && !target.noRigidbody)
		{
			return target.transform.position + target.Rigidbody.velocity * prediction - start;
		}
		return target.transform.position - start;
	}

	private bool CanSee(Transform target)
	{
		RaycastHit hitInfo;
		return Physics.Raycast(SpawnPosition.position, (target.position - SpawnPosition.position).normalized, out hitInfo, activationDistance, mask) && !IsStaticEnvironment(hitInfo.collider.transform);
	}

	private bool IsStaticEnvironment(Transform t)
	{
		if (StatMaster.isMP)
		{
			foreach (KeyValuePair<uint, List<BlockBehaviour>> simulationBlock in ReferenceMaster.SimulationBlocks)
			{
				if (simulationBlock.Value.Count > 0 && t.root == simulationBlock.Value[0].transform.root)
				{
					return false;
				}
			}
		}
		else if (t.root == Machine.Active().transform)
		{
			return false;
		}
		if ((bool)t.parent)
		{
			if (t.parent.name == "Knights")
			{
				return false;
			}
			if ((bool)t.parent.parent && t.parent.parent.name == "Knights")
			{
				return false;
			}
		}
		return true;
	}

	private void Rotate(float maxTorque, float speed = 1f)
	{
		if (hasTarget && useForceRotation)
		{
			speed = rotationSpeed * speed;
			float num = Vector3.Angle(body.transform.forward, correctedDir);
			Vector3 normalized = Vector3.Cross(body.transform.forward, correctedDir).normalized;
			float num2 = num * speed * 5f;
			if (useTorqueScale)
			{
				num2 = torqueScale.Evaluate(num2 / maxTorque) * maxTorque;
			}
			Vector3 vector = normalized * num2;
			vector = Vector3.ClampMagnitude(vector, maxTorque);
			if (clearVelocityOnNew)
			{
				vector -= body.angularVelocity;
			}
			body.AddTorque(vector, ForceMode.Acceleration);
		}
	}

	private void Aim(Vector3 direction, float speed = 1f)
	{
		if (hasTarget)
		{
			steering = SteeringState.Aim;
			speed = rotationSpeed * speed;
			Vector3 normalized = direction.normalized;
			cannonRotation.SetLookRotation(normalized, base.transform.up);
			Vector3 vector = baseObject.InverseTransformDirection(normalized);
			Vector3 normalized2 = new Vector3(vector.x, 0f, vector.z).normalized;
			correctedDir = baseObject.TransformDirection(normalized2);
			if (!useForceRotation)
			{
				Vector3 eulerAngles = Quaternion.LookRotation(normalized2, Vector3.up).eulerAngles;
				baseObject.localRotation = Quaternion.Lerp(baseObject.localRotation, Quaternion.Euler(new Vector3(0f, eulerAngles.y + rotatedMeshCorrection, 0f)), Time.deltaTime * speed);
			}
			float num = Vector3.Angle(vector, normalized2);
			float num2 = ((!(Vector3.Dot(baseObject.up, normalized) > 0f)) ? (-1f) : 1f);
			num *= num2;
			float num3 = 5f;
			float num4 = direction.magnitude - num3;
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			float f = num * ((float)Math.PI / 180f);
			float num5 = dropCompensationPer100mInDegrees * Mathf.Cos(Mathf.Pow(f, dropBias)) * (num4 / 100f) * Vector3.Dot(baseObject.up, Vector3.up);
			num += num5;
			num = Mathf.Clamp((!inverseAngle) ? num : (0f - num), minAngle, maxAngle);
			Vector3 euler = barrel.localEulerAngles;
			switch (barrelAxis)
			{
			case Axes.x:
				euler = new Vector3(num, 0f, 0f);
				break;
			case Axes.y:
				euler = new Vector3(0f, num, 0f);
				break;
			case Axes.z:
				euler = new Vector3(0f, 0f, num);
				break;
			default:
				Debug.LogError("Use one of the local axis for the barrel rotation");
				break;
			}
			barrel.localRotation = Quaternion.Lerp(barrel.localRotation, Quaternion.Euler(euler), Time.deltaTime * speed * 2f);
		}
	}

	private void Shoot()
	{
		Vector3 vector = Mathf.Min(SpawnPosition.lossyScale.x, SpawnPosition.lossyScale.y) * projectilePrefab.transform.localScale;
		if (useProjManager)
		{
			byte[] array = new byte[19];
			int num = 0;
			NetworkCompression.CompressPosition(SpawnPosition.position, array, num);
			num += 6;
			NetworkCompression.CompressRotation(SpawnPosition.rotation, array, num);
			num += 7;
			NetworkCompression.CompressVector(vector, 0f, 100f, array, num);
			bolt = ProjectileManager.Instance.Spawn(NetworkProjectileType.Cannon, NetworkAddPiece.Instance.frame, playerID, array, explode);
			bolt.localScale = vector;
		}
		else
		{
			bolt = (UnityEngine.Object.Instantiate(projectilePrefab, SpawnPosition.position, SpawnPosition.rotation, ReferenceMaster.physicsGoalInstance) as GameObject).transform;
		}
		float num2 = 1f;
		if (numberOfVolleys > 1)
		{
			float num3 = numberOfVolleys - 1;
			float t = (num3 - (float)currentVolley) / (1f * num3);
			float num4 = Mathf.Floor((float)numberOfVolleys / 2f) * 0.2f;
			num2 = Mathf.Lerp(1f - num4, 1f + num4, t);
		}
		Rigidbody component = bolt.GetComponent<Rigidbody>();
		Collider component2 = bolt.GetComponent<Collider>();
		if (useProjManager)
		{
			Vector3 velocity = (component.angularVelocity = Vector3.zero);
			component.velocity = velocity;
		}
		else
		{
			for (int i = 0; i < projectileIgnore.Length; i++)
			{
				Physics.IgnoreCollision(projectileIgnore[i], component2);
			}
		}
		Vector3 vector2 = spawnDir + randomiseShots * UnityEngine.Random.onUnitSphere;
		component.AddForce(vector2 * power * num2);
		component.AddTorque(vector2 * 500f);
		PlayParticles();
	}

	public bool PlayParticles()
	{
		if (particles != null)
		{
			particles.Play();
		}
		if (rsp != null)
		{
			rsp.Play();
		}
		if (StatMaster.isHosting && base.SimPhysics)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.CannonParticles);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		return true;
	}

	private bool GetTarget()
	{
		int closestMachine = FactionsController.GetClosestMachine(base.transform.position);
		if (closestMachine != -1)
		{
			BlockBehaviour randomIntactBlock = ReferenceMaster.GetRandomIntactBlock((uint)closestMachine);
			if (object.ReferenceEquals(randomIntactBlock, null) || randomIntactBlock.IsDestroyed)
			{
				return false;
			}
			target = randomIntactBlock;
			hasTarget = true;
			return true;
		}
		return false;
	}

	private void OnDrawGizmosSelected()
	{
		DebugExtension.DebugCircle(base.transform.position, Color.magenta, activationDistance, 0f);
		DebugExtension.DebugCone(SpawnPosition.position, SpawnPosition.forward * activationDistance * 0.5f, Color.yellow, Mathf.Acos(shootingDotP) * 57.29578f, 0f);
	}
}
