using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;

[AddComponentMenu("Besiege/_BLOCKS/Blocks/Vaccume/Vacuum Controller")]
public class VacuumController : ExternalForce
{
	public VacuumBlock vacuumBlock;

	public float updateSpeed = 0.25f;

	public float maxAiVelocity = 5f;

	[HideInInspector]
	public bool isOff = true;

	[HideInInspector]
	public bool wasOff = true;

	private bool setupDone;

	private Transform machineTransform;

	private int blockCount;

	private Vector3 vacuumDir;

	private EntityAI ai;

	private ExternalForceObject EFO;

	[Header("Joint Variables")]
	[HideInInspector]
	public ConfigurableJoint joint;

	[HideInInspector]
	public bool isTouching;

	public float jointBreakForce = 100f;

	private float jointCD;

	private EntityAI jointedAI;

	private Rigidbody joinToRb;

	private bool jointExists;

	private bool createJoint;

	[Header("Detection")]
	public float angle = 0.65f;

	public float capsuleLength;

	public float capsuleRadius = 1f;

	[HideInInspector]
	public float additionalRadius;

	[HideInInspector]
	public float coneOffset;

	[HideInInspector]
	public float detectionLength;

	[HideInInspector]
	public float nozzleOffset = 1f;

	public Vector3 frontPoint;

	public LayerMask mask;

	private Collider[] overlapResults;

	private BasicInfo overlapInfo;

	private HashSet<BasicInfo> overlapSet = new HashSet<BasicInfo>();

	private Vector3 lastPoint;

	private Vector3 lastForward;

	private bool inverse;

	protected Vector3 GetFrontPoint
	{
		get
		{
			return base.transform.TransformPoint(frontPoint) + base.transform.up * coneOffset;
		}
	}

	protected Vector3 Nozzle
	{
		get
		{
			return lastPoint - lastForward * (nozzleOffset + 1f) * base.transform.localScale.y;
		}
	}

	public Vector3 Forward
	{
		get
		{
			return (!inverse) ? (-base.transform.up) : base.transform.up;
		}
	}

	public Vector3 EndPos
	{
		get
		{
			return GetFrontPoint + Forward * detectionLength;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && base.SimPhysics)
		{
			if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
			{
				base.enabled = false;
			}
			frontOfObject = FoO.y;
			machineTransform = base.transform.parent;
			jointBreakForce *= vacuumBlock.powerSlider.Value;
			StartCoroutine(Setup());
		}
	}

	private IEnumerator Setup()
	{
		yield return null;
		blockCount = ReferenceMaster.GetAllSimulationBlocks().Count;
		int size = (int)((blockCount <= 8) ? ((float)blockCount) : ((float)blockCount * 0.3f));
		EFOArray = new ExternalForceObject[size];
		worldMatrix = base.transform.worldToLocalMatrix;
		InvokeRepeating("UpdateVacuumTargets", updateSpeed, updateSpeed);
		setupDone = true;
	}

	public void UpdateRange(float value)
	{
		float num = 1f;
		float num2 = value * 0.5f + 0.5f;
		if (num2 < 0f)
		{
			inverse = true;
			lastForward = base.transform.up;
			num2 *= -1f;
		}
		else
		{
			inverse = false;
			lastForward = -base.transform.up;
		}
		nozzleOffset = Mathf.Max(1f, value) * 0.25f;
		additionalRadius = Mathf.Max(0f, value - 1f) * 0.05f;
		float num3 = capsuleRadius + additionalRadius;
		detectionLength = capsuleLength * num2 - num3 + num;
		if (detectionLength <= num3)
		{
			if (inverse)
			{
				coneOffset = 0f - num;
			}
			else
			{
				coneOffset = num3 - detectionLength;
			}
			detectionLength = num3 + 0.001f;
		}
		else if (inverse)
		{
			detectionLength = capsuleLength * num2 - num3;
			coneOffset = 0f - num;
		}
		else
		{
			coneOffset = 0f;
		}
		lastPoint = GetFrontPoint;
		Vector3 end = lastPoint + lastForward * detectionLength;
		angle = GetAngle(Nozzle, end);
		SingleInstance<Events>.Instance.CollidersChanged(vacuumBlock);
	}

	public void UpdateJoinTarget(Collider col)
	{
		if (col != null)
		{
			joinToRb = col.attachedRigidbody;
			createJoint = joinToRb != null;
		}
		else
		{
			createJoint = false;
		}
	}

	private void Update()
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return;
		}
		if (jointExists)
		{
			jointExists = joint != null && joint.connectedBody != null;
			if (!jointExists)
			{
				createJoint = false;
			}
		}
		if (isOff)
		{
			if (jointExists)
			{
				PostJointBreak(0f);
			}
		}
		else if (!jointExists && jointCD > 0f)
		{
			jointCD -= Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (!base.SimPhysics || !base.isSimulating || isOff || jointExists || createJoint || !setupDone)
		{
			return;
		}
		for (int i = 0; i < ExternalForceObjectCount; i++)
		{
			EFO = EFOArray[i];
			if (object.ReferenceEquals(EFO, null))
			{
				break;
			}
			if (!EFO.basicInfo.isDestroyed && !EFO.basicInfo.noRigidbody)
			{
				EFO.basicInfo.Hover(EFO.antiGravity);
				EFO.basicInfo.Rigidbody.AddForceAtPosition(EFO.force, EFO.closestPoint, EFO.forceMode);
				vacuumBlock.Rigidbody.AddForce(-EFO.force);
			}
		}
	}

	public void CalculateForcePositions()
	{
		if (!base.SimPhysics || isOff)
		{
			return;
		}
		Vector3 up = base.transform.up;
		for (int i = 0; i < ExternalForceObjectCount; i++)
		{
			EFO = EFOArray[i];
			if (object.ReferenceEquals(EFO, null))
			{
				break;
			}
			if (EFO.basicInfo.isDestroyed || EFO.basicInfo.noRigidbody)
			{
				continue;
			}
			isTouching = createJoint && object.ReferenceEquals(EFO.basicInfo.Rigidbody, joinToRb);
			EFO.basicInfo.BeingVacuumed = isTouching || !createJoint;
			if (createJoint && !isTouching)
			{
				continue;
			}
			Rigidbody rigidbody = EFO.basicInfo.Rigidbody;
			bool hasAiScript = EFO.basicInfo.hasAiScript;
			bool flag = EFO.basicInfo is EnemyAISimple;
			if (hasAiScript)
			{
				ai = EFO.basicInfo.aiEntity;
			}
			EnemyAISimple enemyAISimple = null;
			if (flag)
			{
				enemyAISimple = EFO.basicInfo as EnemyAISimple;
			}
			Vector3 vector = base.transform.TransformPoint(frontPoint - Vector3.up * 0.5f);
			if (jointCD <= 0f && isTouching && !jointExists && (!flag || (flag && enemyAISimple.isDead)))
			{
				AddJoint(rigidbody);
				if (hasAiScript)
				{
					ai.Grabbed(this);
					jointedAI = ai;
				}
			}
			if ((hasAiScript || flag) && rigidbody.velocity.magnitude > maxAiVelocity / rigidbody.mass && Vector3.Dot(vector - rigidbody.position, rigidbody.velocity) > 0f)
			{
				EFO.force = Vector3.zero;
				continue;
			}
			EFO.closestPoint = ((!isTouching || hasAiScript || flag) ? EFO.basicInfo.Rigidbody.worldCenterOfMass : EFO.basicInfo.UpdatedBounds.ClosestPoint(vector));
			Vector3 v = vector - EFO.closestPoint;
			Debug.DrawLine(EFO.closestPoint, vector, Color.yellow, Time.fixedDeltaTime, false);
			float num = v.magnitude;
			if (num < 1f)
			{
				num = 1f;
			}
			EFO.antiGravity = ((!(rigidbody.mass > vacuumBlock.Rigidbody.mass)) ? rigidbody.mass : vacuumBlock.Rigidbody.mass) / num;
			vacuumDir = ((!(num > 1f)) ? up : NormalizeVector(num, v));
			vacuumBlock.vacuumPower = vacuumDir * (vacuumBlock.vacuumForce * vacuumBlock.powerSlider.Value * 1.5f / Mathf.Pow(num, 1.5f));
			EFO.force = vacuumBlock.vacuumPower;
		}
	}

	private void AddJoint(Rigidbody rb)
	{
		joint = base.gameObject.AddComponent<ConfigurableJoint>();
		jointExists = true;
		joint.axis = new Vector3(1f, joint.axis.y, joint.axis.z);
		joint.angularXMotion = ConfigurableJointMotion.Locked;
		joint.angularYMotion = ConfigurableJointMotion.Locked;
		joint.angularZMotion = ConfigurableJointMotion.Locked;
		joint.xMotion = ConfigurableJointMotion.Locked;
		joint.yMotion = ConfigurableJointMotion.Locked;
		joint.zMotion = ConfigurableJointMotion.Locked;
		joint.breakForce = jointBreakForce;
		joint.connectedBody = rb;
		rb.gameObject.SendMessage("Joined", joint, SendMessageOptions.DontRequireReceiver);
	}

	public void ScheduleJointBreak()
	{
		if (jointExists)
		{
			ConfigurableJoint configurableJoint = joint;
			float num = 0f;
			joint.breakTorque = num;
			configurableJoint.breakForce = num;
		}
	}

	public void OnJointBreak(float force)
	{
		PostJointBreak(0.5f);
	}

	protected void PostJointBreak(float jointcd)
	{
		if (base.SimPhysics && !(joint == null))
		{
			jointExists = false;
			createJoint = false;
			if (joint.connectedBody != null)
			{
				joint.connectedBody.gameObject.SendMessage("Disjoined", joint, SendMessageOptions.DontRequireReceiver);
			}
			jointCD = jointcd;
			if (!object.ReferenceEquals(jointedAI, null))
			{
				jointedAI.StopBeingGrabbed();
				jointedAI = null;
			}
		}
	}

	protected void UpdateVacuumTargets()
	{
		if (base.isSimulating && !isOff)
		{
			worldMatrix = base.transform.worldToLocalMatrix;
			ExternalForceObjectCount = 0;
			OverlapApproach();
		}
	}

	protected override void CheckIfFormost(Vector3 pos, BasicInfo basic)
	{
		Vector3 pos2 = worldMatrix.MultiplyPoint3x4(pos);
		base.AddEFOVelSpace(pos2, basic, ForceMode.Force, 1f);
	}

	internal override void AddEFOVelSpace(Vector3 pos, BasicInfo basic, ForceMode forceMode, float powerScale)
	{
		Vector3 pos2 = worldMatrix.MultiplyPoint3x4(pos);
		base.AddEFOVelSpace(pos2, basic, forceMode, powerScale);
	}

	private void OverlapApproach()
	{
		lastForward = Forward;
		lastPoint = GetFrontPoint;
		float num = capsuleRadius + additionalRadius;
		overlapResults = Physics.OverlapCapsule(lastPoint + lastForward * num, lastPoint + lastForward * detectionLength, num, mask, QueryTriggerInteraction.Ignore);
		SortOverlapResults();
		foreach (BasicInfo item in overlapSet)
		{
			processBasicInfo3(item);
		}
	}

	public float GetAngle(Vector3 start, Vector3 end)
	{
		Vector3 vector = end - start;
		Vector3 normalized = Vector3.Slerp(vector, -vector, 0.5f).normalized;
		float num = capsuleRadius + additionalRadius;
		Vector3 vector2 = vector + normalized * num;
		return Vector3.Dot(vector.normalized, vector2.normalized);
	}

	private void SortOverlapResults()
	{
		overlapSet.Clear();
		Vector3 nozzle = Nozzle;
		for (int i = 0; i < overlapResults.Length; i++)
		{
			Rigidbody attachedRigidbody = overlapResults[i].attachedRigidbody;
			if (attachedRigidbody == null || attachedRigidbody.isKinematic)
			{
				continue;
			}
			float num = Vector3.Dot(lastForward, (attachedRigidbody.worldCenterOfMass - nozzle).normalized);
			if (!(num < angle))
			{
				overlapInfo = attachedRigidbody.GetComponent<BasicInfo>();
				if (overlapInfo != null && !overlapSet.Contains(overlapInfo))
				{
					overlapSet.Add(overlapInfo);
				}
			}
		}
	}

	private void processBasicInfo3(BasicInfo bInfo)
	{
		bInfo.BeingVacuumed = false;
		if (!ValidateEFO(bInfo))
		{
			return;
		}
		BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
		if (object.ReferenceEquals(blockBehaviour, null))
		{
			Vector3 worldCenterOfMass = bInfo.Rigidbody.worldCenterOfMass;
			CheckIfFormost(worldCenterOfMass, bInfo);
		}
		else if (machineTransform == null)
		{
			Debug.LogError("ERROR! Machine Transform is null for " + Machine.GetObjectPath(base.gameObject) + "Please notify the devs!");
		}
		else
		{
			if (bInfo.transform.IsChildOf(machineTransform) && vacuumBlock.ClusterIndex == blockBehaviour.ClusterIndex && vacuumBlock.ClusterIndex != -1)
			{
				return;
			}
			int iD = blockBehaviour.Prefab.ID;
			switch (iD)
			{
			case -1:
			case 7:
			case 9:
			case 45:
			case 57:
			case 58:
				return;
			}
			Vector3 worldCenterOfMass = bInfo.Rigidbody.worldCenterOfMass;
			switch (iD)
			{
			case 1:
			case 41:
			case 63:
				if (blockBehaviour.MeshRenderer.enabled)
				{
					AddEFOVelSpace(worldCenterOfMass, bInfo, ForceMode.Force, 2f);
					return;
				}
				break;
			case 25:
			case 34:
				AddEFOVelSpace(worldCenterOfMass, bInfo, ForceMode.Force, 2f);
				return;
			}
			CheckIfFormost(worldCenterOfMass, bInfo);
		}
	}
}
