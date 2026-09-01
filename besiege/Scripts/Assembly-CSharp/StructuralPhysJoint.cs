using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuralPhysJoint : SimBehaviour
{
	public List<Collider> colliders = new List<Collider>();

	public List<Collider> otherJoiners = new List<Collider>();

	public Rigidbody myRigidbody;

	public Rigidbody hitRigidbody;

	public ConfigurableJoint jointToCopy;

	public int breakForce = 200000;

	public int breakTorque = 12000;

	public bool useForGroundJoints;

	public bool jointMode;

	public int myId;

	public bool AddGroundJoint;

	public ConfigurableJoint jointAdded;

	public float searchDistance = 3f;

	private List<ConfigurableJoint> addedJoints = new List<ConfigurableJoint>();

	private bool isInitializing;

	private StructuralPhysTile ownPhysTile;

	private IEnumerator restoreJointsCoroutine;

	private Collider physNodeCollider;

	public bool debug;

	protected override void Start()
	{
		base.Start();
		bool levelEdit = StatMaster.Mode.levelEdit;
		if ((!levelEdit && !base.isSimulating) || (levelEdit && base.isSimulating && base.SimPhysics))
		{
			myId = UnityEngine.Random.Range(0, 10000000);
			restoreJointsCoroutine = RestoreJoints();
			StartCoroutine(restoreJointsCoroutine);
		}
	}

	public void Clear()
	{
		foreach (ConfigurableJoint addedJoint in addedJoints)
		{
			if (!(addedJoint == null))
			{
				UnityEngine.Object.Destroy(addedJoint);
			}
		}
		addedJoints.Clear();
		if ((bool)jointAdded)
		{
			UnityEngine.Object.Destroy(jointAdded);
		}
		jointAdded = null;
		hitRigidbody = null;
		if (isInitializing && restoreJointsCoroutine != null)
		{
			StopCoroutine(restoreJointsCoroutine);
		}
	}

	public void BurnJoint(float progress)
	{
		if ((bool)jointAdded)
		{
			jointAdded.breakForce = Mathf.Lerp(breakForce, 1f, progress);
			jointAdded.breakTorque = Mathf.Lerp(breakTorque, 1f, progress);
		}
	}

	public void ResetJoints()
	{
		Clear();
		StartCoroutine(RestoreJoints());
	}

	public void OnDisable()
	{
		StopAllCoroutines();
	}

	protected IEnumerator RestoreJoints()
	{
		isInitializing = true;
		myRigidbody.velocity = Vector3.zero;
		myRigidbody.useGravity = false;
		if (myRigidbody != null)
		{
			ownPhysTile = myRigidbody.GetComponent<StructuralPhysTile>();
			if (ownPhysTile != null)
			{
				ownPhysTile.myId = myId;
			}
		}
		SphereCastCheck();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		if (myRigidbody == null)
		{
			yield break;
		}
		if (!SetJointsRayCast(breakForce, breakTorque) && AddGroundJoint)
		{
			if (useForGroundJoints)
			{
				JoinToGroundRayCast(breakForce, breakTorque);
			}
			else
			{
				JoinToGroundRayCast(200000f, 40000f);
			}
		}
		int rand = base.transform.GetSiblingIndex() % 3;
		yield return new WaitForFixedUpdate();
		if (rand == 0)
		{
			myRigidbody.useGravity = true;
		}
		yield return new WaitForFixedUpdate();
		if (rand == 1)
		{
			myRigidbody.useGravity = true;
		}
		yield return new WaitForFixedUpdate();
		if (rand == 2)
		{
			myRigidbody.useGravity = true;
		}
		GetAttachedBlocksJoints();
		isInitializing = false;
	}

	private IEnumerator PulseTrigger()
	{
		Collider coll = GetComponent<Collider>();
		Rigidbody body = GetComponent<Rigidbody>();
		coll.enabled = false;
		body.WakeUp();
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		coll.enabled = true;
		body.WakeUp();
	}

	private void SphereCastCheck()
	{
		float num = 0.5f;
		float num2 = 1f;
		if (StatMaster.isMP)
		{
			Transform transform = basicInfo.gameObject.GetComponentInParent<GenericEntity>().transform;
			num2 = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
			if (num2 > 1f)
			{
				num *= num2;
			}
		}
		RaycastHit[] array = Physics.SphereCastAll(base.transform.position, num, base.transform.forward, searchDistance * num2);
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if (raycastHit.collider.gameObject.layer == 29)
			{
				AddGroundJoint = true;
				continue;
			}
			Rigidbody attachedRigidbody = raycastHit.collider.attachedRigidbody;
			if (!(attachedRigidbody != null) || !(attachedRigidbody != myRigidbody))
			{
				continue;
			}
			SimplePhysTile component = attachedRigidbody.GetComponent<SimplePhysTile>();
			if (component != null && component.enabled)
			{
				hitRigidbody = attachedRigidbody;
				continue;
			}
			PhysNodeTile component2 = attachedRigidbody.GetComponent<PhysNodeTile>();
			if (component2 != null)
			{
				physNodeCollider = raycastHit.collider;
				component2.onNodeBreak = (Action<PhysNodeTile, PhysNodeBase.PhysNode>)Delegate.Combine(component2.onNodeBreak, new Action<PhysNodeTile, PhysNodeBase.PhysNode>(OnNodeBreak));
				hitRigidbody = attachedRigidbody;
				continue;
			}
			ColorLevelBlock colorLevelBlock = ((!StatMaster.isMP) ? null : attachedRigidbody.GetComponentInParent<ColorLevelBlock>());
			if (StatMaster.isMP && (bool)colorLevelBlock)
			{
				hitRigidbody = attachedRigidbody;
			}
		}
	}

	private void OnNodeBreak(PhysNodeTile physTile, PhysNodeBase.PhysNode obj)
	{
		if (jointAdded != null && obj.collider == physNodeCollider)
		{
			UnityEngine.Object.Destroy(jointAdded);
			jointAdded = null;
		}
	}

	private void ResolveDoubleJoiners()
	{
		if (otherJoiners.Count > 0)
		{
			StructuralPhysJoint component = otherJoiners[0].GetComponent<StructuralPhysJoint>();
			if (component.myId > myId)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void RemoveDoubleJointsSphereCast(ConfigurableJoint[] configs)
	{
		foreach (ConfigurableJoint configurableJoint in configs)
		{
			if ((bool)configurableJoint && !(configurableJoint.connectedBody != myRigidbody) && myRigidbody != null)
			{
				StructuralPhysTile component = myRigidbody.GetComponent<StructuralPhysTile>();
				if (component != null && component.myId > myId && configurableJoint == jointAdded)
				{
					UnityEngine.Object.Destroy(jointAdded);
				}
			}
		}
	}

	private void GetAttachedBlocksJoints()
	{
		if (!(hitRigidbody == null))
		{
			ConfigurableJoint[] components = hitRigidbody.GetComponents<ConfigurableJoint>();
			RemoveDoubleJointsSphereCast(components);
		}
	}

	private void SetJoints(float bF, float bT)
	{
		if (colliders.Count >= 2)
		{
			ConfigurableJoint configurableJoint = colliders[0].attachedRigidbody.gameObject.AddComponent<ConfigurableJoint>();
			addedJoints.Add(configurableJoint);
			SetJointParams(configurableJoint);
			configurableJoint.connectedBody = colliders[1].attachedRigidbody;
			configurableJoint.breakForce = bF;
			configurableJoint.breakTorque = bT;
		}
	}

	private void JoinToGround(float bF, float bT)
	{
		ConfigurableJoint configurableJoint = colliders[0].attachedRigidbody.gameObject.AddComponent<ConfigurableJoint>();
		addedJoints.Add(configurableJoint);
		SetJointParams(configurableJoint);
		configurableJoint.connectedBody = null;
		configurableJoint.breakForce = bF;
		configurableJoint.breakTorque = bT;
	}

	protected bool SetJointsRayCast(float bF, float bT)
	{
		if (hitRigidbody == null)
		{
			return false;
		}
		jointAdded = myRigidbody.gameObject.AddComponent<ConfigurableJoint>();
		SetJointParams(jointAdded);
		jointAdded.connectedBody = hitRigidbody;
		if (hitRigidbody != null && hitRigidbody.inertiaTensor.sqrMagnitude > myRigidbody.inertiaTensor.sqrMagnitude)
		{
			jointAdded.swapBodies = true;
		}
		jointAdded.breakForce = bF;
		jointAdded.breakTorque = bT;
		return true;
	}

	private void JoinToGroundRayCast(float bF, float bT)
	{
		jointAdded = myRigidbody.gameObject.AddComponent<ConfigurableJoint>();
		SetJointParams(jointAdded);
		jointAdded.connectedBody = null;
		jointAdded.breakForce = bF;
		jointAdded.breakTorque = bT;
	}

	private void SetJointParams(ConfigurableJoint joint)
	{
		joint.axis = joint.transform.InverseTransformDirection(base.transform.forward);
		joint.secondaryAxis = joint.transform.InverseTransformDirection(base.transform.up);
		joint.anchor = joint.transform.InverseTransformPoint(base.transform.position);
		joint.autoConfigureConnectedAnchor = true;
		joint.xMotion = jointToCopy.xMotion;
		joint.yMotion = jointToCopy.yMotion;
		joint.zMotion = jointToCopy.zMotion;
		joint.angularXMotion = jointToCopy.angularXMotion;
		joint.angularYMotion = jointToCopy.angularYMotion;
		joint.angularZMotion = jointToCopy.angularZMotion;
		joint.projectionMode = jointToCopy.projectionMode;
		joint.projectionAngle = jointToCopy.projectionAngle;
		joint.projectionDistance = jointToCopy.projectionDistance;
		joint.enablePreprocessing = jointToCopy.enablePreprocessing;
	}
}
