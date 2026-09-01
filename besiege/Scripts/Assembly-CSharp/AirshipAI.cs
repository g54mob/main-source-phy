using System;
using UnityEngine;

[AddComponentMenu("Physics/AI/AirshipAI")]
public class AirshipAI : MonoBehaviour
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

	public BasicInfo bInfo;

	public Transform startingPoint;

	public float circleRadius = 3600f;

	public float crashDegree = 0.3f;

	private Vector3 direction;

	private Vector3 direction2D;

	private Vector3 returnDirection2D;

	private Transform target;

	private bool returned;

	public Disposition disposition = new Disposition();

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			if (startingPoint == null)
			{
				Debug.LogError("Starting point on AirshipAI is null!", base.gameObject);
				base.enabled = false;
			}
			for (int i = 0; i < disposition.behaviours.Length; i++)
			{
				disposition.behaviours[i].Initialize(i);
			}
			target = Machine.Active().SimulationBlocks[0].transform;
			bInfo.Rigidbody.AddForce(-base.transform.forward * disposition.currentBehaviour.parameters.Speed - bInfo.Rigidbody.velocity, ForceMode.Impulse);
		}
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && !(target == null))
		{
			direction = target.position - base.transform.position;
			direction2D = new Vector3(direction.x, 0f, direction.z);
			if (disposition.myState == EntityState.ReturnAndCircle)
			{
				returnDirection2D = startingPoint.position - base.transform.position;
				returnDirection2D = new Vector3(returnDirection2D.x, 0f, returnDirection2D.z);
			}
			CurrentDisposition();
			disposition.myState = disposition.currentBehaviour.state;
			if (Vector3.Dot(base.transform.up, Vector3.up) < crashDegree)
			{
				bInfo.Rigidbody.useGravity = true;
				Pursuing(-Vector3.up);
			}
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating)
		{
			RunState();
		}
	}

	private void Pursuing(Vector3 TargetDirection)
	{
		Vector3 right = base.transform.right;
		Vector3 forward = base.transform.forward;
		Vector3 up = base.transform.up;
		float num = Vector3.Dot(right, Vector3.up);
		float num2 = Vector3.Dot(TargetDirection.normalized, new Vector3(0f - right.x, 0f, 0f - right.z));
		Vector3 vector = new Vector3(0f - forward.x, (startingPoint.position - base.transform.position).normalized.y, 0f - forward.z);
		float num3 = Vector3.Dot(vector.normalized, up);
		Vector3 vector2 = forward * (0f - num) * (disposition.currentBehaviour.parameters.Speed / 10f);
		Vector3 vector3 = up * num2 * disposition.currentBehaviour.parameters.Speed;
		Vector3 vector4 = right * num3 * disposition.currentBehaviour.parameters.Speed;
		bInfo.Rigidbody.AddTorque(vector2 + vector3 + vector4);
		bInfo.Rigidbody.AddForce(-forward * disposition.currentBehaviour.parameters.Speed - bInfo.Rigidbody.velocity);
	}

	private void CircleStrafe()
	{
		Pursuing(Vector3.Cross(returnDirection2D.normalized, Vector3.up));
		if (returnDirection2D.sqrMagnitude > circleRadius)
		{
			bInfo.Rigidbody.AddForce(returnDirection2D.normalized * disposition.currentBehaviour.parameters.Speed);
		}
	}

	private void Stationary()
	{
		Vector3 right = base.transform.right;
		Vector3 forward = base.transform.forward;
		Vector3 up = base.transform.up;
		float num = Vector3.Dot(right, Vector3.up);
		float num2 = Vector3.Dot((-forward).normalized, new Vector3(0f - right.x, 0f, 0f - right.z));
		Vector3 vector = new Vector3(0f - forward.x, (startingPoint.position - base.transform.position).normalized.y, 0f - forward.z);
		float num3 = Vector3.Dot(vector.normalized, up);
		Vector3 vector2 = forward * (0f - num) * disposition.currentBehaviour.parameters.Speed;
		Vector3 vector3 = up * num2 * disposition.currentBehaviour.parameters.Speed;
		Vector3 vector4 = right * num3 * disposition.currentBehaviour.parameters.Speed;
		bInfo.Rigidbody.AddTorque(vector2 + vector3 + vector4);
	}

	private void RunState()
	{
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
		float sqrMagnitude = direction.sqrMagnitude;
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
}
