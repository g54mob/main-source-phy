using System;
using UnityEngine;

public class StructuralPhysTile : SimplePhysTile
{
	public int groupId;

	public float destroyThreshold = 100f;

	public Transform brokenBlock;

	public GameObject brokenChild;

	public Transform BrokenInstance;

	public int myId;

	public Transform skinnedMeshVis;

	public bool keepKinematic;

	public StructuralPhysJoint[] joints;

	public Renderer visCopyMaterialFrom;

	private CustomLevel level;

	private NetworkBlock netBlock;

	private bool CanDie = true;

	private float fullDT = 100f;

	private bool hasBody;

	private Rigidbody myBody;

	public override Vector3 Center()
	{
		if (base.SimPhysics)
		{
			return myBody.worldCenterOfMass;
		}
		return base.Center();
	}

	protected override void Awake()
	{
		base.Awake();
		BrokenInstance = null;
		joints = base.gameObject.GetComponentsInChildren<StructuralPhysJoint>(true);
		fullDT = destroyThreshold;
	}

	public void ResetJoints()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			joints[i].ResetJoints();
		}
	}

	public void ClearJoints()
	{
		for (int i = 0; i < joints.Length; i++)
		{
			joints[i].Clear();
		}
	}

	public void BurnJoints(float progress)
	{
		if (joints.Length == 0)
		{
			destroyThreshold = Mathf.Lerp(fullDT, 1f, progress);
		}
		for (int i = 0; i < joints.Length; i++)
		{
			joints[i].BurnJoint(progress);
		}
	}

	protected override void Start()
	{
		base.Start();
		myId = UnityEngine.Random.Range(0, 10000000);
		if (HasBasicInfo)
		{
			myBody = basicInfo.Rigidbody;
		}
		else
		{
			myBody = GetComponent<Rigidbody>();
		}
		if (!base.isSimulating || base.SimPhysics)
		{
			myBody.solverIterations = 22;
		}
		if (!base.isSimulating)
		{
			return;
		}
		hasBody = (HasBasicInfo && !basicInfo.noRigidbody) || (!HasBasicInfo && myBody != null);
		if (base.SimPhysics && hasBody)
		{
			if (!keepKinematic)
			{
				myBody.isKinematic = false;
			}
			myBody.Sleep();
		}
	}

	private void OnJointBreak()
	{
		if (base.SimPhysics && !myBody.isKinematic && base.gameObject.GetComponents<ConfigurableJoint>().Length < 2)
		{
			DestroyTile((HasBasicInfo && !basicInfo.noRigidbody) ? basicInfo.Rigidbody.velocity : ((HasBasicInfo || !(myBody != null)) ? Vector3.zero : myBody.velocity));
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		float sqrMagnitude = other.relativeVelocity.sqrMagnitude;
		if (!base.SimPhysics || !base.isSimulating || sqrMagnitude <= destroyThreshold || myBody.isKinematic)
		{
			return;
		}
		if ((bool)other.collider.attachedRigidbody)
		{
			TimedRocket component = other.collider.attachedRigidbody.GetComponent<TimedRocket>();
			if ((bool)component && component.hasFired && !component.hasExploded && sqrMagnitude <= destroyThreshold * 10f)
			{
				return;
			}
		}
		DestroyTile(other.relativeVelocity);
	}

	public void DestroyTile(Vector3 dir)
	{
		if (!base.enabled || !CanDie)
		{
			return;
		}
		CanDie = false;
		if (brokenBlock != null)
		{
			Transform transform = base.transform;
			BrokenInstance = UnityEngine.Object.Instantiate(brokenBlock, transform.position, transform.rotation) as Transform;
			BrokenInstance.parent = transform.parent;
			BrokenInstance.localScale = transform.localScale;
			BrokenInstance.localPosition = transform.localPosition;
			BrokenInstance.localRotation = transform.localRotation;
			if ((bool)visCopyMaterialFrom)
			{
				CopyMaterial component = BrokenInstance.GetComponent<CopyMaterial>();
				component.CopyMat(visCopyMaterialFrom);
			}
			bool flag = (HasBasicInfo && !basicInfo.noRigidbody) || (!HasBasicInfo && myBody != null);
			if (base.SimPhysics && flag)
			{
				myBody = ((!HasBasicInfo) ? myBody : basicInfo.Rigidbody);
				InheritForce component2 = BrokenInstance.GetComponent<InheritForce>();
				component2.forceToAdd = dir;
				component2.torqueToAdd = myBody.angularVelocity;
				component2.AddForce();
			}
		}
		bool flag2 = BrokenInstance != null;
		if (skinnedMeshVis != null)
		{
			skinnedMeshVis.parent = ReferenceMaster.physicsGoalInstance;
			skinnedMeshVis.localScale = Vector3.zero;
		}
		AddToPercentageBar();
		if (StatMaster.isHosting && base.SimPhysics)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.Break);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		if (flag2)
		{
			base.gameObject.SetActive(false);
			if (StatMaster.isMP && HasBasicInfo && base.SimPhysics)
			{
				Joint[] components = base.gameObject.GetComponents<Joint>();
				for (int i = 0; i < components.Length; i++)
				{
					UnityEngine.Object.Destroy(components[i]);
				}
				if (!basicInfo.noRigidbody)
				{
					basicInfo.noRigidbody = true;
					UnityEngine.Object.Destroy(basicInfo.Rigidbody);
				}
			}
		}
		else if ((bool)brokenChild)
		{
			Joint[] components2 = base.gameObject.GetComponents<Joint>();
			for (int j = 0; j < components2.Length; j++)
			{
				UnityEngine.Object.Destroy(components2[j]);
			}
			brokenChild.SetActive(true);
		}
		OnBreak();
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}
}
