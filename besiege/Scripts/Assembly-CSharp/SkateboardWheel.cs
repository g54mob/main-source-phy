using System;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Skate Wheel")]
public class SkateboardWheel : CustomBodyBlock
{
	public MeshRenderer[] reparentOnJoint = new MeshRenderer[0];

	public MeshRenderer wheel;

	private MSlider tensionSlider;

	protected bool hasJoint = true;

	[HideInInspector]
	public bool dualBody;

	private Vector3 rot = Vector3.zero;

	public MSlider TensionSlider
	{
		get
		{
			return tensionSlider;
		}
	}

	protected override void Awake()
	{
		if (!isSimulating || SimPhysics)
		{
			tensionSlider = AddSlider(4590, "tightness", 1f, 0.1f, 1f, string.Empty);
		}
		base.Awake();
	}

	public override void StartPhysics(bool isKinematic)
	{
		base.StartPhysics(isKinematic);
		hasJoint = true;
		CheckForExtraSplits();
		if (dualBody)
		{
			SimReparent(this, BuildIndex);
		}
		if (!SimPhysics || blockJoint == null)
		{
			hasJoint = false;
			return;
		}
		if (blockJoint.connectedBody == null)
		{
			UnityEngine.Object.Destroy(blockJoint);
			hasJoint = false;
			return;
		}
		float num = tensionSlider.Value;
		if (num < 1f)
		{
			if (num < 0f || float.IsNaN(num))
			{
				num = 0f;
			}
			num *= num;
			ConfigurableJoint configurableJoint = blockJoint as ConfigurableJoint;
			configurableJoint.angularZMotion = ConfigurableJointMotion.Limited;
			SoftJointLimit angularZLimit = configurableJoint.angularZLimit;
			angularZLimit.limit = 3f;
			configurableJoint.angularZLimit = angularZLimit;
			SoftJointLimitSpring angularYZLimitSpring = configurableJoint.angularYZLimitSpring;
			angularYZLimitSpring.spring = 10000f * num;
			angularYZLimitSpring.damper = angularYZLimitSpring.spring * 0.01f;
			configurableJoint.angularYZLimitSpring = angularYZLimitSpring;
		}
	}

	public override int CheckJoints()
	{
		CheckForExtraSplits();
		return base.CheckJoints();
	}

	private void CheckForExtraSplits()
	{
		if (dualBody || jointsToMe.Count <= 0 || !(blockJoint != null))
		{
			return;
		}
		foreach (Joint item in jointsToMe)
		{
			if (item != null && item.connectedBody != null)
			{
				SplitWheelBody();
			}
		}
	}

	public void SimReparent(BlockBehaviour t, int index)
	{
		for (int i = 0; i < reparentOnJoint.Length; i++)
		{
			reparentOnJoint[i].transform.parent = t.transform;
			t.visAddedToMe.Add(reparentOnJoint[i]);
		}
		if (StatMaster.isMP && SimPhysics)
		{
			NetworkBlock netBlock = NetBlock;
			if (netBlock != null)
			{
				netBlock.Event(NetworkEntity.EntityEvent.ParentToBlock, (byte)index);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	public void ClientReparent(int index)
	{
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			return;
		}
		BlockBehaviour block;
		base.ParentMachine.GetBlockFromIndex(index, out block);
		block = block.SimBlock;
		if ((bool)block)
		{
			for (int i = 0; i < reparentOnJoint.Length; i++)
			{
				reparentOnJoint[i].transform.parent = block.transform;
				block.visAddedToMe.Add(reparentOnJoint[i]);
			}
			dualBody = block == this;
			if (dualBody)
			{
				_parentMachine.RegisterUpdate(this, false);
			}
			else
			{
				_parentMachine.UnregisterUpdate(this, false);
			}
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (dualBody && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			Vector3 rhs = base.transform.InverseTransformDirection(NetBlock.Velocity);
			rhs.x = 0f;
			Vector3 vector = Vector3.Cross(base.transform.InverseTransformDirection(Vector3.up), rhs);
			vector.y = (vector.z = 0f);
			rot += vector * Time.unscaledDeltaTime * (float)Math.PI * 2f * 40f;
			wheel.transform.localEulerAngles = rot;
		}
	}

	public void SplitWheelBody()
	{
		if (!dualBody && !(blockJoint == null))
		{
			GameObject gameObject = wheel.gameObject;
			gameObject.transform.parent = base.transform.parent;
			Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
			if (rigidbody == null)
			{
				rigidbody = gameObject.AddComponent<Rigidbody>();
			}
			float num = Rigidbody.mass * 0.5f;
			rigidbody.inertiaTensor = Rigidbody.inertiaTensor;
			Rigidbody rigidbody2 = rigidbody;
			float mass = num;
			Rigidbody.mass = mass;
			rigidbody2.mass = mass;
			rigidbody.drag = Rigidbody.drag;
			rigidbody.maxAngularVelocity = Rigidbody.maxAngularVelocity;
			rigidbody.isKinematic = false;
			rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			Rigidbody.inertiaTensor = CustomBodyBlock.ScaleInertia(base.transform.localScale, new Vector3(0.1f, 0.1f, 0.025f));
			Rigidbody.maxAngularVelocity = 7f;
			Rigidbody.centerOfMass = new Vector3(0f, 0f, -0.25f);
			ConfigurableJoint configurableJoint = blockJoint as ConfigurableJoint;
			configurableJoint.connectedBody = rigidbody;
			dualBody = true;
		}
	}
}
