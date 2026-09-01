using System;
using UnityEngine;

[AddComponentMenu("Destruction/Tiles/PhysNodeBase")]
public abstract class PhysNodeBase : BreakBase
{
	[Serializable]
	public class PhysNode
	{
		public Collider collider;

		public bool active = true;

		public GameObject breakInto;

		[NonSerialized]
		public Transform breakInstance;

		public GameObject[] objectImSupporting = new GameObject[0];

		public Joint[] jointsImSupporting = new Joint[0];
	}

	[Serializable]
	public class JointNode
	{
		public Collider[] nodeColliders;

		public Transform orientation;

		[HideInInspector]
		public bool hasOrientation = true;

		[HideInInspector]
		public float nodeBreakForce;

		[HideInInspector]
		public float nodeBreakTorque;

		[NonSerialized]
		public PhysNode[] nodes;

		[HideInInspector]
		public Collider collider;

		[HideInInspector]
		public ConfigurableJoint joint;

		[HideInInspector]
		public int nodesInRange;
	}

	public class ConnectResult
	{
		public Collider collider;

		public Vector3 position;

		public bool groundJoint;

		public PhysNodeBase node;
	}

	public Rigidbody myBody;

	public float searchDistance = 0.25f;

	public float groundTorque = 40000f;

	protected float sphereCastRadius = 0.5f;

	protected float castRadiusSqr;

	protected override void Start()
	{
		base.Start();
		castRadiusSqr = sphereCastRadius * sphereCastRadius;
		if (!base.isSimulating)
		{
			CreateJoints();
		}
		else if (base.SimPhysics)
		{
			RegisterCallbacks();
			myBody.isKinematic = false;
			myBody.Sleep();
		}
	}

	protected abstract void OnDrawGizmosSelected();

	protected void DrawGizmos(JointNode jointNode)
	{
		if (jointNode == null)
		{
			return;
		}
		if (jointNode.joint != null)
		{
			if (jointNode.hasOrientation)
			{
				Vector3 forward = jointNode.orientation.forward;
				Vector3 localDirection = jointNode.joint.transform.InverseTransformDirection(forward);
				Vector3 anchor = jointNode.joint.anchor;
				for (int i = 0; i < jointNode.nodeColliders.Length; i++)
				{
					if (jointNode.nodes[i] != null && jointNode.nodes[i].active)
					{
						Vector3 nodePosition = GetNodePosition(jointNode, i, anchor, localDirection);
						Gizmos.color = ((!InRange(nodePosition, jointNode.joint.connectedBody)) ? Color.red : Color.green);
						Gizmos.DrawSphere(nodePosition, sphereCastRadius);
						Gizmos.DrawLine(nodePosition, nodePosition + forward);
					}
				}
			}
			else
			{
				Rigidbody connectedBody = jointNode.joint.connectedBody;
				Vector3 anchor;
				if (connectedBody != null)
				{
					Gizmos.color = Color.green;
					anchor = connectedBody.transform.TransformPoint(jointNode.joint.connectedAnchor);
				}
				else
				{
					Gizmos.color = Color.blue;
					anchor = jointNode.joint.connectedAnchor;
				}
				Gizmos.DrawSphere(anchor, sphereCastRadius);
			}
		}
		else if (jointNode.hasOrientation)
		{
			Vector3 position = jointNode.orientation.position;
			Vector3 forward2 = jointNode.orientation.forward;
			ConnectResult connectResult;
			if (Connect(position, forward2, out connectResult))
			{
				Gizmos.color = ((!connectResult.groundJoint) ? Color.green : Color.blue);
			}
			else
			{
				Gizmos.color = Color.red;
			}
			Gizmos.DrawWireSphere(position + forward2 * searchDistance, sphereCastRadius);
		}
	}

	protected Vector3 GetNodePosition(JointNode jointNode, int nodeIndex, Vector3 anchorPos, Vector3 localDirection)
	{
		Transform transform = jointNode.joint.transform;
		Collider collider = jointNode.nodes[nodeIndex].collider;
		Vector3 vector = transform.InverseTransformPoint(collider.bounds.center);
		Vector3 vector2 = Vector3.Project(anchorPos - vector, localDirection);
		return transform.TransformPoint(vector + vector2);
	}

	protected bool InRange(Vector3 nodePosition, Rigidbody body)
	{
		Vector3 vector = ((!(body == null)) ? (body.ClosestPointOnBounds(nodePosition) - nodePosition) : Vector3.zero);
		float num = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		return num < castRadiusSqr;
	}

	protected virtual int ConfigureNode(JointNode jointNode, ConnectResult connectResult)
	{
		jointNode.collider = connectResult.collider;
		jointNode.joint.anchor = jointNode.joint.transform.InverseTransformPoint(connectResult.position);
		if (!connectResult.groundJoint)
		{
			jointNode.joint.connectedBody = connectResult.collider.attachedRigidbody;
		}
		else
		{
			jointNode.joint.breakTorque = groundTorque;
		}
		int num = 0;
		for (int i = 0; i < jointNode.nodes.Length; i++)
		{
			if (jointNode.nodes[i] != null && jointNode.nodes[i].active)
			{
				num++;
			}
		}
		return num;
	}

	protected bool Connect(Vector3 position, Vector3 fwd, out ConnectResult connectResult)
	{
		connectResult = new ConnectResult
		{
			collider = null,
			node = null,
			groundJoint = false
		};
		RaycastHit[] array = Physics.SphereCastAll(position, sphereCastRadius, fwd, searchDistance);
		int num = LayerMask.NameToLayer("Floor");
		Transform parent = myBody.transform;
		for (int i = 0; i < array.Length; i++)
		{
			Collider collider = array[i].collider;
			if (collider.transform.IsChildOf(parent))
			{
				continue;
			}
			bool groundJoint = false;
			PhysNodeBase node = null;
			if (collider.gameObject.layer == num)
			{
				groundJoint = true;
			}
			else
			{
				Rigidbody attachedRigidbody = collider.attachedRigidbody;
				if (attachedRigidbody == null)
				{
					continue;
				}
				node = collider.GetComponentInParent<PhysNodeBase>();
				if (ContainsSelf(node))
				{
					continue;
				}
				BasicInfo componentInChildren = attachedRigidbody.GetComponentInChildren<BasicInfo>();
				if ((bool)componentInChildren)
				{
					BasicInfo.BasicInfoType infoType = componentInChildren.infoType;
					if (infoType != BasicInfo.BasicInfoType.None)
					{
						if (infoType == BasicInfo.BasicInfoType.Block)
						{
							continue;
						}
					}
					else if (componentInChildren.hasAiScript)
					{
						continue;
					}
				}
				if (attachedRigidbody.gameObject.CompareTag("Debris") || attachedRigidbody.gameObject.CompareTag("Projectile") || (bool)attachedRigidbody.GetComponent<EntityAI>())
				{
					continue;
				}
			}
			connectResult.collider = collider;
			connectResult.position = position;
			connectResult.groundJoint = groundJoint;
			connectResult.node = node;
			return true;
		}
		return false;
	}

	protected void RegisterCallbacks(JointNode jointNode)
	{
		if (jointNode.joint == null || jointNode.joint.connectedBody == null)
		{
			return;
		}
		PhysNodeBase componentInParent = jointNode.collider.GetComponentInParent<PhysNodeBase>();
		if (componentInParent != null)
		{
			if (componentInParent is PhysNodeTile)
			{
				PhysNodeTile obj = componentInParent as PhysNodeTile;
				obj.onNodeBreak = (Action<PhysNodeTile, PhysNode>)Delegate.Combine(obj.onNodeBreak, new Action<PhysNodeTile, PhysNode>(OnTileBreak));
			}
			else if (componentInParent is PhysNodeJoint)
			{
				PhysNodeJoint obj2 = componentInParent as PhysNodeJoint;
				obj2.onNodeBreak = (Action<PhysNodeJoint>)Delegate.Combine(obj2.onNodeBreak, new Action<PhysNodeJoint>(OnNodeBreak));
			}
		}
	}

	protected virtual void OnNodeBreak(PhysNodeJoint nodeJoint)
	{
	}

	protected virtual void OnTileBreak(PhysNodeTile nodeTile, PhysNode node)
	{
	}

	protected void OnTileBreak(JointNode jointNode, PhysNodeTile nodeTile, PhysNode node)
	{
		ConfigurableJoint joint = jointNode.joint;
		float num = 0f;
		jointNode.joint.breakTorque = num;
		joint.breakForce = num;
		jointNode.joint = null;
		nodeTile.onNodeBreak = (Action<PhysNodeTile, PhysNode>)Delegate.Remove(nodeTile.onNodeBreak, new Action<PhysNodeTile, PhysNode>(OnTileBreak));
	}

	protected abstract void CreateJoints();

	protected abstract void RegisterCallbacks();

	protected bool ContainsSelf(PhysNodeBase node)
	{
		if (node != null)
		{
			if (node is PhysNodeTile)
			{
				PhysNodeTile physNodeTile = node as PhysNodeTile;
				for (int i = 0; i < physNodeTile.jointNodes.Length; i++)
				{
					if (physNodeTile.jointNodes[i].joint != null && physNodeTile.jointNodes[i].joint.connectedBody == myBody)
					{
						return true;
					}
				}
			}
			else if (node is PhysNodeJoint)
			{
				PhysNodeJoint physNodeJoint = node as PhysNodeJoint;
				return physNodeJoint.jointNode.joint != null && physNodeJoint.jointNode.joint.connectedBody == myBody;
			}
		}
		return false;
	}
}
