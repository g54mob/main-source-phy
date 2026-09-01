using UnityEngine;

public class BlockJoint
{
	public enum Type
	{
		Config = 0,
		Hinge = 1,
		Fixed = 2
	}

	public enum Axis
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 3
	}

	public Type type;

	public Joint joint;

	public float breakForce;

	public float breakTorque;

	public bool hasJoint = true;

	public bool ballJoint;

	public bool canMove;

	public BlockBehaviour connectedBlock;

	public Vector3 anchor;

	public Vector3 axis;

	public Vector3 secondaryAxis;

	public Vector3 connectedAxis;

	public Vector3 connectedSecondaryAxis;

	public Vector3 connectedAnchor;

	public Axis freeAxis;

	private bool assignedProjection;

	public BlockJoint(BlockBehaviour block, Joint j)
	{
		joint = j;
		breakForce = j.breakForce;
		breakTorque = j.breakTorque;
		Transform transform = block.Rigidbody.transform;
		Transform transform2 = j.connectedBody.transform;
		switch (block.Prefab.Type)
		{
		case BlockType.Axle:
			if (breakTorque == 22001f)
			{
				j = transform2.GetComponent<ConfigurableJoint>();
				transform2 = j.connectedBody.transform;
				ballJoint = true;
			}
			break;
		case BlockType.BallJoint:
			ballJoint = true;
			break;
		case BlockType.Suspension:
		case BlockType.Piston:
		case BlockType.Slider:
			canMove = true;
			break;
		}
		axis = j.axis;
		secondaryAxis = axis;
		if (j is ConfigurableJoint)
		{
			type = Type.Config;
			ConfigurableJoint configurableJoint = j as ConfigurableJoint;
			secondaryAxis = configurableJoint.secondaryAxis;
			if (configurableJoint.angularXMotion != ConfigurableJointMotion.Locked)
			{
				freeAxis = Axis.X;
			}
			else if (configurableJoint.angularYMotion != ConfigurableJointMotion.Locked)
			{
				freeAxis = Axis.Y;
			}
			else if (configurableJoint.angularZMotion != ConfigurableJointMotion.Locked)
			{
				freeAxis = Axis.X;
				axis = Vector3.Cross(axis, secondaryAxis);
			}
		}
		else if (j is HingeJoint)
		{
			type = Type.Hinge;
		}
		else
		{
			type = Type.Fixed;
		}
		connectedAxis = transform2.InverseTransformDirection(transform.TransformDirection(axis));
		connectedSecondaryAxis = transform2.InverseTransformDirection(transform.TransformDirection(secondaryAxis));
		anchor = j.anchor;
		connectedAnchor = j.connectedAnchor;
		connectedBlock = transform2.GetComponent<BlockBehaviour>();
	}

	public void Swap(bool swapBody, float mass)
	{
		if (swapBody && type == Type.Config)
		{
			Rigidbody connectedBody = joint.connectedBody;
			if (mass < connectedBody.mass || (connectedBody.isKinematic && !connectedBody.gameObject.CompareTag("StayKinematic")))
			{
				(joint as ConfigurableJoint).swapBodies = true;
			}
		}
	}

	public void ResetBreak()
	{
		if (!(joint == null) && joint.breakForce != breakForce)
		{
			joint.breakForce = breakForce;
			joint.breakTorque = breakTorque;
			if (assignedProjection)
			{
				ConfigurableJoint configurableJoint = joint as ConfigurableJoint;
				configurableJoint.projectionMode = JointProjectionMode.None;
				assignedProjection = false;
			}
		}
	}

	public void SetInvincible()
	{
		if (joint == null || joint.breakForce == float.PositiveInfinity)
		{
			return;
		}
		joint.breakForce = float.PositiveInfinity;
		joint.breakTorque = float.PositiveInfinity;
		if (type == Type.Config)
		{
			ConfigurableJoint configurableJoint = joint as ConfigurableJoint;
			if (configurableJoint.projectionMode != JointProjectionMode.PositionAndRotation)
			{
				configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
				configurableJoint.projectionDistance = 0.5f;
				configurableJoint.projectionAngle = 180f;
				assignedProjection = true;
			}
		}
	}
}
