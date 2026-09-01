using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/AI/ShipMultibodyAI")]
public class ShipMultibodyAI : MonoBehaviour
{
	public enum EntityState
	{
		Pursuing = 0,
		ReturnAndCircle = 1,
		Circeling = 2,
		Stationary = 3,
		Dead = 4,
		PassMachine = 5,
		ReturnAndStationary = 6
	}

	[Serializable]
	public class Disposition
	{
		public EntityState myState = EntityState.Circeling;

		public bool SmartTargeting;

		public Behaviour[] behaviours;

		[HideInInspector]
		public Behaviour currentBehaviour;
	}

	[Serializable]
	public class Behaviour
	{
		[Serializable]
		public class Parameters
		{
			public float Speed;

			public float turningFactor = 1f;

			public int RandomizeSign()
			{
				if (UnityEngine.Random.value <= 0.5f)
				{
					return 1;
				}
				return -1;
			}
		}

		public EntityState state;

		public float Radius;

		[HideInInspector]
		public float RadiusSqr;

		public Parameters parameters = new Parameters();

		[HideInInspector]
		public int id = -1;

		public Behaviour()
		{
		}

		public Behaviour(float r, EntityState astate, float s, bool attackS)
		{
			Radius = r;
			parameters.Speed = s;
			state = astate;
		}

		public void Initialize(int i)
		{
			id = i;
			RadiusSqr = Radius * Radius;
		}
	}

	public Action Crash;

	public Action<float> TakeInWater;

	public static List<Vector3> AirshipPositions = new List<Vector3>();

	[SerializeField]
	[HideInInspector]
	private int index = -1;

	public BasicInfo[] bInfo;

	public Transform startingPoint;

	public Transform looker;

	public float circleRadius = 3600f;

	public float crashDegree = 0.3f;

	public float crashTime;

	private float maxCrashTime;

	public float temporarilyStopSteeringDegree = -2f;

	public bool avoidOtherShips;

	public float waitOnStart = 1f;

	public float globalSpeed = 1f;

	public float turningSpeed = 1f;

	[HideInInspector]
	public float orgGlobalSpeed = 1f;

	public bool turnOnlyWithMovement;

	public float maxSpeedForTurn = 30f;

	public float returnOffsetWeight;

	public bool onlySteerInWater;

	public int minBodiesInWater = 2;

	public float minDistanceToStartPoint;

	public float minDistanceToPassingMachine = 15f;

	public float avoidanceGradient = 1f;

	public float avoidancePrediction = 1f;

	public bool accelerateState;

	public float speedLerpDuration;

	public Joint[] destroyOnBreak = new Joint[0];

	public AudioSource[] audios = new AudioSource[0];

	public ParticleSystem[] movementParticles = new ParticleSystem[0];

	public Transform[] unparentOnPlay = new Transform[0];

	private Vector3 direction;

	private Vector3 direction2D;

	private Vector3 returnDir2D;

	private Vector3 movementDir2D;

	private Transform target;

	private bool returned;

	private Vector3 zero = Vector3.zero;

	public Disposition disposition = new Disposition();

	protected Vector3 centerOffset;

	private float previousSpeed;

	private float currentSpeed;

	private float targetSpeed;

	private float speedLerp;

	private bool inWater;

	private bool upsideDown;

	private float minDist = 12f;

	private float maxDist = 24f;

	private Vector3 offset = Vector3.zero;

	public bool broken;

	public Vector3 Center
	{
		get
		{
			if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
			{
				return bInfo[0].transform.TransformPoint(centerOffset);
			}
			return bInfo[0].Rigidbody.transform.TransformPoint(centerOffset);
		}
	}

	private float CurrentSpeed
	{
		get
		{
			if (!accelerateState)
			{
				return disposition.currentBehaviour.parameters.Speed * globalSpeed;
			}
			if (speedLerpDuration <= 0f)
			{
				return disposition.currentBehaviour.parameters.Speed * globalSpeed;
			}
			if (targetSpeed != disposition.currentBehaviour.parameters.Speed)
			{
				previousSpeed = currentSpeed;
				targetSpeed = disposition.currentBehaviour.parameters.Speed;
				speedLerp = 0f;
			}
			if (speedLerp < 1f)
			{
				speedLerp += Time.deltaTime / speedLerpDuration;
			}
			else
			{
				speedLerp = 1f;
			}
			currentSpeed = Mathf.Lerp(previousSpeed, targetSpeed, speedLerp);
			return currentSpeed * globalSpeed;
		}
	}

	private void Awake()
	{
		if (!StatMaster.levelSimulating && (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim))
		{
			for (int i = 0; i < unparentOnPlay.Length; i++)
			{
				unparentOnPlay[i].parent = base.transform.parent;
			}
			orgGlobalSpeed = globalSpeed;
			if (index == -1)
			{
				index = AirshipPositions.Count;
				AirshipPositions.Add(zero);
			}
		}
	}

	private void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		Vector3 position = zero;
		float num = 0f;
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			for (int i = 0; i < bInfo.Length; i++)
			{
				position += bInfo[i].transform.position;
				num += 1f;
			}
			position /= num;
			centerOffset = bInfo[0].transform.InverseTransformPoint(position);
			return;
		}
		for (int j = 0; j < bInfo.Length; j++)
		{
			Rigidbody rigidbody = bInfo[j].Rigidbody;
			position += rigidbody.worldCenterOfMass * rigidbody.mass;
			num += rigidbody.mass;
		}
		position /= num;
		centerOffset = bInfo[0].Rigidbody.transform.InverseTransformPoint(position);
		maxCrashTime = crashTime;
		if (index != -1)
		{
			AirshipPositions[index] = Center;
		}
		if (startingPoint == null)
		{
			if (StatMaster.isMP)
			{
				startingPoint = new GameObject("starting point").transform;
				startingPoint.parent = SingleInstanceFindOnly<AddPiece>.Instance.PhysicsGoalObject;
			}
			else
			{
				startingPoint = base.transform;
			}
		}
		for (int k = 0; k < disposition.behaviours.Length; k++)
		{
			disposition.behaviours[k].Initialize(k);
		}
		if (!StatMaster.isMP)
		{
			GetNewTarget();
		}
		for (int l = 0; l < bInfo.Length; l++)
		{
			if (!object.ReferenceEquals(bInfo[l], null) && !bInfo[l].noRigidbody)
			{
				bInfo[l].Rigidbody.AddForce(-looker.forward * disposition.currentBehaviour.parameters.Speed * globalSpeed - bInfo[l].Rigidbody.velocity, ForceMode.Impulse);
			}
		}
	}

	private void GetNewTarget()
	{
		if (StatMaster.isMP)
		{
			if (FactionsController.setupComplete)
			{
				int closestMachine = FactionsController.GetClosestMachine(Center);
				if (closestMachine != -1)
				{
					target = ReferenceMaster.GetRandomBlock((uint)closestMachine).transform;
				}
			}
		}
		else
		{
			target = Machine.Active().SimulationBlocks[0].transform;
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || broken)
		{
			return;
		}
		MoveSounds();
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			return;
		}
		bool flag = true;
		int num = 0;
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null))
			{
				if (bInfo[i].InWater)
				{
					num++;
				}
				if (bInfo[i].submergedPercent < 1f)
				{
					flag = false;
				}
			}
		}
		inWater = num >= minBodiesInWater;
		if (onlySteerInWater && !inWater)
		{
			inWater = false;
			HandleParticles();
			return;
		}
		if (waitOnStart > 0f)
		{
			waitOnStart -= Time.deltaTime;
			return;
		}
		float num2 = Vector3.Dot(looker.up, Vector3.up);
		if (num2 < crashDegree || flag)
		{
			if (crashTime > 0f)
			{
				num2 = (num2 - 1f) * -1f;
				num2 -= (crashDegree - 1f) * -0.5f;
				num2 = Mathf.Max(0f, num2);
				crashTime -= Time.deltaTime * Mathf.Max((!flag) ? 0f : 3f, num2);
				if (crashTime <= 0f)
				{
					crashTime = 0f;
				}
				float obj = crashTime / maxCrashTime;
				if (TakeInWater != null)
				{
					TakeInWater(obj);
				}
			}
			upsideDown = true;
		}
		else
		{
			upsideDown = false;
		}
		HandleParticles();
		if (crashTime == 0f)
		{
			if (Crash != null)
			{
				Crash();
			}
			else
			{
				Break();
			}
		}
		if (upsideDown)
		{
			return;
		}
		if (crashTime < maxCrashTime)
		{
			crashTime += Time.deltaTime;
			if (crashTime > maxCrashTime)
			{
				crashTime = maxCrashTime;
			}
			float obj2 = crashTime / maxCrashTime;
			if (TakeInWater != null)
			{
				TakeInWater(obj2);
			}
		}
		if (num2 < temporarilyStopSteeringDegree)
		{
			return;
		}
		Vector3 center = Center;
		AirshipPositions[index] = center;
		if (target == null || direction2D.sqrMagnitude > circleRadius)
		{
			ResetDisposition();
			GetNewTarget();
			if (target == null)
			{
				direction = Vector3.zero;
			}
			else
			{
				direction = target.position - center;
			}
		}
		else
		{
			CurrentDisposition();
			direction = target.position - center;
		}
		direction2D = new Vector3(direction.x, 0f, direction.z);
		movementDir2D = direction2D;
		disposition.myState = disposition.currentBehaviour.state;
		if (disposition.myState == EntityState.PassMachine)
		{
			offset = Vector3.Cross(direction2D.normalized, Vector3.up);
			float x = looker.transform.InverseTransformDirection(direction2D.normalized).x;
			if (x < 0f)
			{
				offset *= -1f;
			}
			offset = (offset - direction2D.normalized) / 2f;
			offset *= minDistanceToPassingMachine;
			movementDir2D = direction2D + offset;
			returnDir2D = startingPoint.position - center;
			returnDir2D = new Vector3(returnDir2D.x, 0f, returnDir2D.z);
		}
		if (disposition.myState == EntityState.ReturnAndCircle)
		{
			returnDir2D = startingPoint.position - center;
			returnDir2D = new Vector3(returnDir2D.x, 0f, returnDir2D.z);
			movementDir2D = returnDir2D;
		}
		else if (minDistanceToStartPoint > 0f)
		{
			BasicInfo basicInfo = bInfo[0];
			returnDir2D = startingPoint.position - (center + basicInfo.Rigidbody.velocity * avoidancePrediction);
			returnDir2D = new Vector3(returnDir2D.x, 0f, returnDir2D.z);
			float num3 = Vector3.Dot(looker.transform.forward, returnDir2D.normalized);
			float value = (returnDir2D.magnitude - minDistanceToStartPoint) / avoidanceGradient;
			float t = (1f - Mathf.Clamp01(value)) * Mathf.Clamp01((num3 + 1f) * 0.8f);
			Vector3 vector = Vector3.Lerp(movementDir2D, -returnDir2D, t);
			movementDir2D = vector;
		}
	}

	private void MoveSounds()
	{
		for (int i = 0; i < audios.Length; i++)
		{
			audios[i].transform.position = Center;
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim))
		{
			RunState();
		}
	}

	public void HandleParticles()
	{
		bool flag = movementDir2D.sqrMagnitude > 3f;
		if (CurrentSpeed == 0f)
		{
			flag = false;
		}
		else if (upsideDown || broken)
		{
			flag = false;
		}
		else if (onlySteerInWater && !inWater)
		{
			flag = false;
		}
		for (int i = 0; i < movementParticles.Length; i++)
		{
			ParticleSystem.EmissionModule emission = movementParticles[i].emission;
			if (emission.enabled != flag)
			{
				emission.enabled = flag;
			}
		}
	}

	private void Pursuing(Vector3 TargetDirection, float turningSpeed = 0.35f)
	{
		if (!looker)
		{
			return;
		}
		Vector3 normalized = TargetDirection.normalized;
		Vector3 right = looker.right;
		Vector3 forward = looker.forward;
		float num = CurrentSpeed;
		if (num == 0f)
		{
			return;
		}
		Vector3 vector = forward * num;
		float num2 = ((!(Vector3.Dot(normalized, right) > Vector3.Dot(normalized, -right))) ? 1f : (-1f));
		float value = Vector3.Angle(normalized, forward);
		value = Mathf.Clamp(value, 0f, 45f) * num2;
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && !bInfo[i].noRigidbody)
			{
				Rigidbody rigidbody = bInfo[i].Rigidbody;
				float num3 = rigidbody.mass / 10f;
				Vector3 vector2 = rigidbody.transform.position - Center;
				float f = value * ((float)Math.PI / 180f);
				float num4 = Mathf.Sin(f);
				float num5 = Mathf.Cos(f);
				float x = vector2.x * num5 - vector2.z * num4;
				float z = vector2.x * num4 + vector2.z * num5;
				Vector3 vector3 = new Vector3(x, vector2.y, z);
				Vector3 vector4 = vector3 - vector2;
				float num6 = 1f;
				if (turnOnlyWithMovement)
				{
					num6 = Mathf.InverseLerp(0f, maxSpeedForTurn, Vector3.Project(rigidbody.velocity, rigidbody.transform.forward).magnitude);
				}
				vector4 *= num / globalSpeed * turningSpeed * num6 * this.turningSpeed;
				Vector3 force = (vector + vector4) * num3 - rigidbody.velocity;
				if (avoidOtherShips)
				{
					Vector3 vector5 = CollisionAvoidance(force.normalized, force.magnitude, 25f);
					force += vector5;
				}
				rigidbody.AddForce(force);
			}
		}
	}

	private void IdleCircle()
	{
		float num = (returnDir2D.magnitude - Mathf.Sqrt(circleRadius)) * 0.02f;
		Vector3 a = Vector3.Cross(returnDir2D, Vector3.up);
		a = ((!(num < 0f)) ? Vector3.Lerp(a, returnDir2D, num) : Vector3.Lerp(a, -returnDir2D, 0f - num));
		Pursuing(a, 0.45f * disposition.currentBehaviour.parameters.turningFactor);
	}

	private Vector3 CollisionAvoidance(Vector3 forward, float distanceAhead, float force)
	{
		Vector3 vector = AirshipPositions[index];
		Vector3 vector2 = vector + forward * distanceAhead;
		Vector3 vector3 = ClosestOtherAirship(vector);
		Vector3 result = zero;
		if (vector3 != zero)
		{
			result.x = vector2.x - vector3.x;
			result.z = vector2.z - vector3.z;
			if (result.sqrMagnitude < maxDist * maxDist)
			{
				float magnitude = result.magnitude;
				if (magnitude < minDist)
				{
					magnitude = minDist;
				}
				float num = Mathf.InverseLerp(minDist, maxDist, magnitude);
				num = 1f - num;
				result = result.normalized * num * force;
			}
			else
			{
				result = zero;
			}
		}
		return result;
	}

	private Vector3 ClosestOtherAirship(Vector3 toPosition)
	{
		float num = float.MaxValue;
		Vector3 result = zero;
		for (int i = 0; i < AirshipPositions.Count; i++)
		{
			if (i != index)
			{
				float sqrMagnitude = (AirshipPositions[i] - AirshipPositions[index]).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = AirshipPositions[i];
					num = sqrMagnitude;
				}
			}
		}
		return result;
	}

	private void Stationary()
	{
		if (!looker)
		{
			return;
		}
		Vector3 right = looker.right;
		Vector3 forward = looker.forward;
		Vector3 up = looker.up;
		float num = Vector3.Dot((-forward).normalized, new Vector3(right.x, 0f, right.z));
		Vector3 torque = up * num * CurrentSpeed;
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && !bInfo[i].noRigidbody)
			{
				bInfo[i].Rigidbody.AddTorque(torque);
			}
		}
	}

	private void RunState()
	{
		if (broken || !inWater || upsideDown)
		{
			return;
		}
		switch (disposition.myState)
		{
		case EntityState.Pursuing:
			if (returnDir2D.sqrMagnitude < circleRadius + 25f && Vector3.Dot(movementDir2D, returnDir2D) > 0f)
			{
				IdleCircle();
				break;
			}
			Pursuing(movementDir2D, 0.35f * disposition.currentBehaviour.parameters.turningFactor);
			returned = false;
			break;
		case EntityState.Circeling:
			IdleCircle();
			break;
		case EntityState.ReturnAndCircle:
			if (movementDir2D.sqrMagnitude > circleRadius && !returned)
			{
				if (!Mathf.Approximately(returnOffsetWeight, 0f))
				{
					offset = Vector3.Cross(movementDir2D.normalized, Vector3.up) * Mathf.Sqrt(circleRadius) * returnOffsetWeight;
				}
				Pursuing(movementDir2D + offset, 0.35f * disposition.currentBehaviour.parameters.turningFactor);
			}
			else
			{
				returned = true;
				IdleCircle();
			}
			break;
		case EntityState.ReturnAndStationary:
			if (movementDir2D.sqrMagnitude > circleRadius && !returned)
			{
				if (!Mathf.Approximately(returnOffsetWeight, 0f))
				{
					offset = Vector3.Cross(movementDir2D.normalized, Vector3.up) * Mathf.Sqrt(circleRadius) * returnOffsetWeight;
				}
				Pursuing(movementDir2D + offset, 0.35f * disposition.currentBehaviour.parameters.turningFactor);
			}
			else
			{
				returned = true;
				Stationary();
			}
			break;
		case EntityState.PassMachine:
			Pursuing(movementDir2D + offset, 0.35f * disposition.currentBehaviour.parameters.turningFactor);
			returned = false;
			break;
		case EntityState.Stationary:
			Stationary();
			break;
		case EntityState.Dead:
			break;
		}
	}

	public float GetBehaviourMaxRad()
	{
		float num = float.MinValue;
		for (int i = 0; i < disposition.behaviours.Length; i++)
		{
			if (disposition.behaviours[i].Radius > num)
			{
				num = disposition.behaviours[i].Radius;
			}
		}
		return Mathf.Clamp(num * num, float.MinValue, float.MaxValue);
	}

	private void CurrentDisposition()
	{
		float num = float.MaxValue;
		int num2 = -1;
		float sqrMagnitude = direction2D.sqrMagnitude;
		for (int i = 0; i < disposition.behaviours.Length; i++)
		{
			Behaviour behaviour = disposition.behaviours[i];
			float num3 = sqrMagnitude - behaviour.RadiusSqr;
			if ((disposition.currentBehaviour.id == behaviour.id || !(num3 < 10f) || !(num3 > -10f)) && (num3 <= 0f || (disposition.currentBehaviour.id != behaviour.id && num3 < 10f)) && num >= behaviour.Radius)
			{
				num = behaviour.Radius;
				num2 = i;
			}
		}
		if (num2 != -1)
		{
			disposition.currentBehaviour = disposition.behaviours[num2];
		}
		else
		{
			Debug.Log("missing idle behaviour");
		}
	}

	private void ResetDisposition()
	{
		if (disposition.behaviours.Length > 0)
		{
			disposition.currentBehaviour = disposition.behaviours[0];
		}
		else
		{
			Debug.Log("missing idle behaviour");
		}
	}

	public void Break()
	{
		if (!broken)
		{
			for (int i = 0; i < destroyOnBreak.Length; i++)
			{
				if ((bool)destroyOnBreak[i])
				{
					UnityEngine.Object.Destroy(destroyOnBreak[i]);
				}
			}
		}
		broken = true;
		AirshipPositions[index] = zero;
		for (int j = 0; j < bInfo.Length; j++)
		{
			if (!object.ReferenceEquals(bInfo[j], null) && !bInfo[j].noRigidbody)
			{
				Rigidbody rigidbody = bInfo[j].Rigidbody;
				rigidbody.useGravity = true;
				rigidbody.ResetCenterOfMass();
			}
		}
		HandleParticles();
	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		if ((bool)startingPoint)
		{
			DebugExtension.DrawCircle(startingPoint.position, Color.magenta, Mathf.Sqrt(circleRadius));
			DebugExtension.DrawCircle(startingPoint.position, Color.red, minDistanceToStartPoint);
		}
	}
}
