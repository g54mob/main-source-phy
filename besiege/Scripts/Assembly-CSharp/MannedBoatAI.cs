using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Physics/AI/MannedBoatAI")]
public class MannedBoatAI : MonoBehaviour
{
	public static List<MannedBoatAI> boats = new List<MannedBoatAI>();

	[Header("AI")]
	public EntityAI skipper;

	public EntityAI[] ais;

	public Mesh poseOnRemove;

	public GameObject effectOnRemove;

	[Header("Boat")]
	public Transform looker;

	public Joint mainJoint;

	public BasicInfo[] bInfo;

	public bool turnOnlyWithMovement;

	public float maxSpeedForTurn = 5f;

	public float speed = 15f;

	public float sinkDensity = 4f;

	public bool separate = true;

	[HideInInspector]
	public float leftDist;

	[HideInInspector]
	public float rightDist;

	[HideInInspector]
	public float frontDist;

	private static MannedBoatsManager boatsManager;

	protected Vector3 centerOffset;

	private SetPoseForAI poser;

	private BlockBehaviour target;

	private Transform targetTransform;

	private Vector3 direction;

	private Vector3 direction2D;

	private bool moveable = true;

	private bool added;

	private bool hasSkipper;

	private float minRange
	{
		get
		{
			return boatsManager.minRange;
		}
	}

	private float maxRange
	{
		get
		{
			return boatsManager.maxRange;
		}
	}

	private float aggroRange
	{
		get
		{
			return boatsManager.aggroRange;
		}
	}

	public Vector3 Center
	{
		get
		{
			return looker.TransformPoint(centerOffset);
		}
	}

	public bool InWater
	{
		get
		{
			return base.enabled && bInfo[0].InWater;
		}
	}

	private void Start()
	{
		if (StatMaster.levelSimulating && (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim))
		{
			if (boatsManager == null)
			{
				boatsManager = MannedBoatsManager.instance ?? ReferenceMaster.physicsGoalInstance.gameObject.AddComponent<MannedBoatsManager>();
			}
			added = true;
			boats.Add(this);
			CalculateCenter();
			hasSkipper = skipper != null;
			if (hasSkipper)
			{
				poser = skipper.GetComponent<SetPoseForAI>();
			}
			target = ReferenceMaster.GetRandomBlock(Machine.Active().PlayerID);
			if (target != null)
			{
				targetTransform = target.transform;
			}
			for (int i = 0; i < bInfo.Length; i++)
			{
				BasicInfo obj = bInfo[i];
				obj.CallBackOnDisable = (Action)Delegate.Combine(obj.CallBackOnDisable, new Action(SinkBoat));
			}
		}
	}

	public void Move()
	{
		if (Vector3.Dot(Vector3.up, looker.up) < 0.4f || (bInfo.Length > 1 && mainJoint == null) || looker.position.y < WaterController.waterTransformHeight - 3f)
		{
			for (int i = 0; i < ais.Length; i++)
			{
				EntityAI entityAI = ais[i];
				if (entityAI.my.basicInfo.isKinematic)
				{
					entityAI.SetDynamic();
				}
			}
			MainAIDetach();
			base.enabled = false;
			return;
		}
		if (hasSkipper && !skipper.my.basicInfo.isKinematic)
		{
			MainAIDetach();
			moveable = false;
			return;
		}
		if (targetTransform == null)
		{
			target = Machine.Active().GetRandomBlock();
			if (target != null)
			{
				targetTransform = target.transform;
			}
		}
		if (targetTransform != null)
		{
			direction = targetTransform.position - Center;
		}
		direction2D = new Vector3(direction.x, 0f, direction.z);
		if (separate)
		{
			Separate();
		}
		if (hasSkipper)
		{
			poser.updatePoses = direction2D.sqrMagnitude > minRange * 2f;
		}
	}

	private void Separate()
	{
		float magnitude = direction2D.magnitude;
		direction2D /= magnitude;
		float num = 70f;
		if (rightDist < num)
		{
			direction2D -= looker.right * (num - rightDist) / (num * 1.5f);
		}
		if (leftDist < num)
		{
			direction2D += looker.right * (num - leftDist) / (num * 1.5f);
		}
		num *= 2f;
		num -= 20f;
		if (frontDist < 20f)
		{
			direction2D = Vector3.zero;
		}
		else if (frontDist < num)
		{
			direction2D = direction2D.normalized * (frontDist - 20f) / num;
			direction2D *= magnitude;
		}
		else
		{
			direction2D = direction2D.normalized * magnitude;
		}
	}

	private void MainAIDetach()
	{
		if (hasSkipper && (bool)poseOnRemove)
		{
			skipper.looking.Focus = EntityAI.FocusOn.Target;
			poser.updatePoses = true;
			poser.StandingPoses = new Mesh[1] { poseOnRemove };
			if (skipper.disposition.myState != EntityAI.EntityState.Suffocating)
			{
				poser.ChangeMesh(EntityAI.EntityState.Fallen);
			}
		}
		if ((bool)effectOnRemove)
		{
			effectOnRemove.SetActive(true);
		}
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim) && moveable && InWater && direction2D.sqrMagnitude > minRange)
		{
			Pursuing(direction2D);
		}
	}

	private void CalculateCenter()
	{
		Vector3 zero = Vector3.zero;
		float num = 0f;
		for (int i = 0; i < bInfo.Length; i++)
		{
			Rigidbody rigidbody = bInfo[i].Rigidbody;
			zero += rigidbody.worldCenterOfMass * rigidbody.mass;
			num += rigidbody.mass;
		}
		zero /= num;
		centerOffset = looker.InverseTransformPoint(zero);
	}

	private void Pursuing(Vector3 TargetDirection, float turningSpeed = 0.35f)
	{
		if (!looker)
		{
			return;
		}
		float sqrMagnitude = TargetDirection.sqrMagnitude;
		float num = speed * Mathf.InverseLerp(minRange, maxRange, sqrMagnitude);
		if (sqrMagnitude > aggroRange)
		{
			num = 0f;
		}
		Vector3 normalized = TargetDirection.normalized;
		Vector3 right = looker.right;
		Vector3 forward = looker.forward;
		Vector3 vector = forward * num;
		float num2 = ((!(Vector3.Dot(normalized, right) > Vector3.Dot(normalized, -right))) ? 1f : (-1f));
		float value = Vector3.Angle(normalized, forward);
		value = Mathf.Clamp(value, 0f, 45f) * num2;
		if (bInfo.Length == 1)
		{
			SteerSingle(TargetDirection, turningSpeed);
			return;
		}
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (!object.ReferenceEquals(bInfo[i], null) && !bInfo[i].noRigidbody)
			{
				Rigidbody rigidbody = bInfo[i].Rigidbody;
				float num3 = rigidbody.mass / 10f;
				Vector3 vector2 = rigidbody.worldCenterOfMass - Center;
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
				vector4 *= num * turningSpeed * num6;
				Vector3 force = (vector + vector4) * num3 - rigidbody.velocity;
				rigidbody.AddForce(force, ForceMode.Acceleration);
			}
		}
	}

	public void SteerSingle(Vector3 TargetDirection, float turningSpeed = 0.35f)
	{
		Rigidbody rigidbody = bInfo[0].Rigidbody;
		float num = rigidbody.mass / 20f;
		float sqrMagnitude = TargetDirection.sqrMagnitude;
		float num2 = speed * Mathf.InverseLerp(minRange, maxRange, sqrMagnitude);
		if (sqrMagnitude > aggroRange)
		{
			num2 = 0f;
		}
		Vector3 velocity = rigidbody.velocity;
		Vector3 forward = looker.forward;
		Vector3 normalized = TargetDirection.normalized;
		forward.y = (normalized.y = 0f);
		Vector3 vector = Vector3.Cross(forward, normalized);
		float num3 = 1f;
		if (turnOnlyWithMovement)
		{
			num3 = Mathf.InverseLerp(0f, maxSpeedForTurn, Vector3.Project(velocity, forward).magnitude);
		}
		Vector3 vector2 = forward * num2;
		Vector3 force = vector2 * num - velocity;
		rigidbody.AddForce(force, ForceMode.Acceleration);
		rigidbody.AddTorque(vector * num * turningSpeed * num3 * 17f, ForceMode.Acceleration);
	}

	private void SinkBoat()
	{
		for (int i = 0; i < bInfo.Length; i++)
		{
			if (bInfo[i] != null && !bInfo[i].isDestroyed)
			{
				bInfo[i].density = sinkDensity;
			}
		}
	}

	private void OnDisable()
	{
		if (added)
		{
			boats.Remove(this);
			added = false;
		}
		if ((!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim) && StatMaster.levelSimulating)
		{
			for (int i = 0; i < bInfo.Length; i++)
			{
				BasicInfo obj = bInfo[i];
				obj.CallBackOnDisable = (Action)Delegate.Remove(obj.CallBackOnDisable, new Action(SinkBoat));
			}
		}
	}
}
