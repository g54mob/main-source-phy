using System;
using UnityEngine;

[AddComponentMenu("Physics/AI/Ship Animation Path")]
public class ShipAnimationPath : MonoBehaviour
{
	public Animator anim;

	private int currentIndex;

	public Transform[] path = new Transform[0];

	public float speed = 200f;

	public AnimationCurve speedRamp = AnimationCurve.Linear(0f, 1f, 100f, 1f);

	public BasicInfo[] bInfo;

	public Transform looker;

	public float temporarilyStopSteeringDegree = -2f;

	public float waitOnStart = 1f;

	public float globalSpeed = 1f;

	public float turningSpeed = 1f;

	[HideInInspector]
	public float orgGlobalSpeed = 1f;

	public bool turnOnlyWithMovement;

	public float maxSpeedForTurn = 30f;

	public bool onlySteerInWater;

	public int minBodiesInWater = 2;

	public bool accelerateState;

	public float speedLerpDuration;

	public Joint[] destroyOnBreak = new Joint[0];

	public ParticleSystem[] movementParticles = new ParticleSystem[0];

	public Transform[] unparentOnPlay = new Transform[0];

	private Vector3 direction;

	private Vector3 direction2D;

	private Vector3 movementDir2D;

	private Vector3 zero = Vector3.zero;

	protected Vector3 centerOffset;

	private float previousSpeed;

	private float currentSpeed;

	private float targetSpeed;

	private float speedLerp;

	private float timeSinceStart;

	private bool inWater;

	public bool broken;

	public Vector3 Center
	{
		get
		{
			return bInfo[0].Rigidbody.transform.TransformPoint(centerOffset);
		}
	}

	private float CurrentSpeed
	{
		get
		{
			if (!accelerateState)
			{
				return speed * globalSpeed * speedRamp.Evaluate(timeSinceStart);
			}
			if (speedLerpDuration <= 0f)
			{
				return speed * globalSpeed * speedRamp.Evaluate(timeSinceStart);
			}
			if (targetSpeed != speed)
			{
				previousSpeed = currentSpeed;
				targetSpeed = speed;
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
			return currentSpeed * globalSpeed * speedRamp.Evaluate(timeSinceStart);
		}
	}

	private void Awake()
	{
		if (!StatMaster.levelSimulating)
		{
			for (int i = 0; i < unparentOnPlay.Length; i++)
			{
				unparentOnPlay[i].parent = base.transform.parent;
			}
			orgGlobalSpeed = globalSpeed;
		}
	}

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			if ((bool)anim)
			{
				anim.enabled = true;
			}
			GetCenterOffset();
		}
	}

	private Vector3 GetCenterOffset()
	{
		Vector3 position = zero;
		float num = 0f;
		for (int i = 0; i < bInfo.Length; i++)
		{
			Rigidbody rigidbody = bInfo[i].Rigidbody;
			position += rigidbody.worldCenterOfMass * rigidbody.mass;
			num += rigidbody.mass;
		}
		position /= num;
		return centerOffset = bInfo[0].Rigidbody.transform.InverseTransformPoint(position);
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || broken)
		{
			return;
		}
		Vector3 center = Center;
		Vector3 vector = CurrentTarget();
		int num = 0;
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && bInfo[i].InWater)
			{
				num++;
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
			timeSinceStart += Time.deltaTime;
			return;
		}
		float num2 = Vector3.Dot(looker.up, Vector3.up);
		HandleParticles();
		if (!(num2 < temporarilyStopSteeringDegree))
		{
			direction = vector - center;
			direction2D = new Vector3(direction.x, 0f, direction.z);
			movementDir2D = direction2D;
			Debug.DrawRay(center, movementDir2D, Color.yellow);
			DebugExtension.DebugWireSphere(vector, Color.yellow, 4f, 0f);
			timeSinceStart += Time.deltaTime;
		}
	}

	private Vector3 CurrentTarget()
	{
		Vector3 center = Center;
		center.y = 0f;
		Vector3 position = path[currentIndex].position;
		position.y = 0f;
		if ((position - center).sqrMagnitude < 16f && currentIndex + 1 < path.Length)
		{
			currentIndex++;
			position = path[currentIndex].position;
			position.y = 0f;
		}
		return position;
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && !(waitOnStart > 0f))
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
		else if (broken)
		{
			flag = false;
		}
		else if (onlySteerInWater && !inWater)
		{
			flag = false;
		}
		for (int i = 0; i < movementParticles.Length; i++)
		{
			if ((bool)movementParticles[i])
			{
				ParticleSystem.EmissionModule emission = movementParticles[i].emission;
				if (emission.enabled != flag)
				{
					emission.enabled = flag;
				}
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
				rigidbody.AddForce(force);
			}
		}
	}

	private void RunState()
	{
		if (!broken && inWater)
		{
			Pursuing(movementDir2D);
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

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		if (centerOffset == Vector3.zero)
		{
			GetCenterOffset();
		}
		Vector3 start = Center;
		for (int i = currentIndex; i < path.Length; i++)
		{
			Debug.DrawLine(start, path[i].position, Color.cyan);
			start = path[i].position;
		}
	}
}
