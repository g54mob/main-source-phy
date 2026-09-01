using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/AI/AirshipMultiAI")]
public class AirshipMultiAI : MonoBehaviour
{
	public enum EntityState
	{
		Pursuing = 0,
		ReturnAndCircle = 1,
		Circeling = 2,
		Stationary = 3,
		Dead = 4
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

	public static List<Vector3> AirshipPositions = new List<Vector3>();

	[HideInInspector]
	[SerializeField]
	private int index = -1;

	public BasicInfo[] bInfo;

	public Transform startingPoint;

	private Vector3 startPosition;

	public Transform looker;

	public float circleRadius = 3600f;

	public float crashDegree = 0.3f;

	public bool avoidOtherShips;

	public float waitOnStart = 1f;

	public float speed = 1f;

	public float turning = 1f;

	public Joint[] destroyOnBreak = new Joint[0];

	private Vector3 direction;

	private Vector3 direction2D;

	private Vector3 returnDirection2D;

	private Transform target;

	private bool returned;

	private Vector3 zero = Vector3.zero;

	public Disposition disposition = new Disposition();

	protected Vector3 centerOffset;

	private float minDist = 12f;

	private float maxDist = 24f;

	public bool broken;

	protected Vector3 Center
	{
		get
		{
			return bInfo[0].Rigidbody.transform.TransformPoint(centerOffset);
		}
	}

	public Transform GetTarget()
	{
		if (StatMaster.isMP)
		{
			if (FactionsController.setupComplete)
			{
				int closestMachine = FactionsController.GetClosestMachine(looker.position);
				if (closestMachine != -1)
				{
					return ReferenceMaster.GetRandomBlock((uint)closestMachine).transform;
				}
			}
			return null;
		}
		List<BlockBehaviour> simulationBlocks = Machine.Active().SimulationBlocks;
		return simulationBlocks[UnityEngine.Random.Range(0, simulationBlocks.Count)].transform;
	}

	private void Awake()
	{
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			return;
		}
		if (!StatMaster.levelSimulating)
		{
			if (index == -1 && !StatMaster.isMP)
			{
				index = AirshipPositions.Count;
				AirshipPositions.Add(zero);
			}
			return;
		}
		if (StatMaster.isMP)
		{
			index = AirshipPositions.Count;
			AirshipPositions.Add(zero);
		}
		if ((bool)startingPoint)
		{
			startPosition = startingPoint.position;
		}
		else
		{
			startPosition = base.transform.position;
		}
	}

	private void Start()
	{
		if (!StatMaster.levelSimulating || (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim))
		{
			return;
		}
		Vector3 position = zero;
		float num = 0f;
		for (int i = 0; i < bInfo.Length; i++)
		{
			Rigidbody rigidbody = bInfo[i].Rigidbody;
			position += rigidbody.worldCenterOfMass * rigidbody.mass;
			num += rigidbody.mass;
		}
		position /= num;
		centerOffset = bInfo[0].Rigidbody.transform.InverseTransformPoint(position);
		AirshipPositions[index] = Center;
		if (!StatMaster.isMP && startingPoint == null)
		{
			Debug.LogError("Starting point on AirshipAI is null!", base.gameObject);
			base.enabled = false;
		}
		for (int j = 0; j < disposition.behaviours.Length; j++)
		{
			disposition.behaviours[j].Initialize(j);
		}
		target = GetTarget();
		for (int k = 0; k < bInfo.Length; k++)
		{
			if (!object.ReferenceEquals(bInfo[k], null) && !bInfo[k].noRigidbody)
			{
				bInfo[k].Rigidbody.AddForce(-looker.forward * disposition.currentBehaviour.parameters.Speed * speed - bInfo[k].Rigidbody.velocity, ForceMode.Impulse);
			}
		}
	}

	private void OnEnable()
	{
		if (StatMaster.isMP && !StatMaster.levelSimulating)
		{
			AirshipPositions.Clear();
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || broken || (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim))
		{
			return;
		}
		if (waitOnStart > 0f)
		{
			waitOnStart -= Time.deltaTime;
			return;
		}
		if (target == null)
		{
			target = GetTarget();
			return;
		}
		Vector3 center = Center;
		AirshipPositions[index] = center;
		direction = target.position - center;
		direction2D = new Vector3(direction.x, 0f, direction.z);
		if (disposition.myState == EntityState.ReturnAndCircle)
		{
			returnDirection2D = startPosition - center;
			returnDirection2D = new Vector3(returnDirection2D.x, 0f, returnDirection2D.z);
		}
		Debug.DrawLine(center, target.position, Color.Lerp(Color.red, Color.yellow, (direction2D.magnitude - 20f) / 100f));
		CurrentDisposition();
		disposition.myState = disposition.currentBehaviour.state;
		if (!(Vector3.Dot(looker.up, Vector3.up) < crashDegree))
		{
			return;
		}
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && !bInfo[i].noRigidbody)
			{
				bInfo[i].Rigidbody.useGravity = true;
				bInfo[i].Rigidbody.ResetCenterOfMass();
			}
		}
		Pursuing(-Vector3.up);
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim))
		{
			RunState();
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
		float num = disposition.currentBehaviour.parameters.Speed * speed;
		Vector3 vector = forward * num;
		float num2 = ((!(Vector3.Dot(normalized, right) > Vector3.Dot(normalized, -right))) ? 1f : (-1f));
		float value = Vector3.Angle(normalized, forward);
		value = Mathf.Clamp(value, 0f, 45f) * num2;
		Vector3 center = Center;
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && !bInfo[i].noRigidbody)
			{
				Rigidbody rigidbody = bInfo[i].Rigidbody;
				float num3 = rigidbody.mass / 10f;
				Vector3 vector2 = rigidbody.transform.position - center;
				float f = value * ((float)Math.PI / 180f);
				float num4 = Mathf.Sin(f);
				float num5 = Mathf.Cos(f);
				float x = vector2.x * num5 - vector2.z * num4;
				float z = vector2.x * num4 + vector2.z * num5;
				Vector3 vector3 = new Vector3(x, vector2.y, z);
				Vector3 vector4 = vector3 - vector2;
				vector4 *= num * turningSpeed * turning;
				Vector3 force = (vector + vector4) * num3 - rigidbody.velocity;
				if (avoidOtherShips)
				{
					Vector3 vector5 = CollisionAvoidance(force.normalized, force.magnitude, 25f);
					force += vector5;
					Debug.DrawRay(rigidbody.transform.position, vector5, (Color.red + Color.yellow) * 0.5f);
				}
				Vector3 worldCenterOfMass = rigidbody.worldCenterOfMass;
				worldCenterOfMass.y = center.y;
				rigidbody.AddForceAtPosition(force, worldCenterOfMass);
				Debug.DrawLine(rigidbody.worldCenterOfMass, center, Color.blue);
			}
		}
	}

	private void CircleStrafe()
	{
		Pursuing(Vector3.Cross(returnDirection2D.normalized, Vector3.up), 0.45f);
		float num = returnDirection2D.sqrMagnitude - circleRadius;
		float num2 = Mathf.Abs(num);
		Vector3 center = Center;
		if (!(num2 > 0.5f))
		{
			return;
		}
		float num3 = 100f;
		Vector3 force = returnDirection2D.normalized * Mathf.Clamp(num, 0f - num3, num3) * disposition.currentBehaviour.parameters.Speed * speed / 1000f;
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && !bInfo[i].noRigidbody)
			{
				Vector3 worldCenterOfMass = bInfo[i].Rigidbody.worldCenterOfMass;
				worldCenterOfMass.y = center.y;
				bInfo[i].Rigidbody.AddForceAtPosition(force, worldCenterOfMass);
			}
		}
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
		Vector3 torque = up * num * disposition.currentBehaviour.parameters.Speed * speed;
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
		if (broken)
		{
			return;
		}
		switch (disposition.myState)
		{
		case EntityState.Pursuing:
			Pursuing(direction2D);
			returned = false;
			break;
		case EntityState.Circeling:
			CircleStrafe();
			break;
		case EntityState.ReturnAndCircle:
			if (returnDirection2D.sqrMagnitude > circleRadius && !returned)
			{
				Pursuing(returnDirection2D);
				break;
			}
			returned = true;
			CircleStrafe();
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
			Debug.Log("missing idel behaviour");
		}
	}

	public void Break()
	{
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			return;
		}
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
	}
}
