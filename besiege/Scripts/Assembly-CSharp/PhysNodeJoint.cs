using System;
using UnityEngine;

[AddComponentMenu("Destruction/Tiles/PhysNodeJoint")]
public class PhysNodeJoint : PhysNodeBase
{
	public Action<PhysNodeJoint> onNodeBreak;

	public PhysNode node;

	public JointNode jointNode;

	public ConfigurableJoint joint;

	protected override void Start()
	{
		jointNode.nodes = new PhysNode[1] { node };
		jointNode.joint = joint;
		base.Start();
	}

	protected override void OnDrawGizmosSelected()
	{
		DrawGizmos(jointNode);
	}

	protected override void OnTileBreak(PhysNodeTile nodeTile, PhysNode node)
	{
		if (jointNode.joint != null && jointNode.collider == node.collider)
		{
			OnTileBreak(jointNode, nodeTile, node);
		}
	}

	protected override void RegisterCallbacks()
	{
		RegisterCallbacks(jointNode);
	}

	protected override void CreateJoints()
	{
		ConfigurableJoint obj = jointNode.joint;
		Vector3 position = jointNode.orientation.position;
		Vector3 forward = jointNode.orientation.forward;
		ConnectResult connectResult;
		if (Connect(position, forward, out connectResult))
		{
			ConfigureNode(jointNode, connectResult);
		}
		else
		{
			UnityEngine.Object.Destroy(obj);
			jointNode.joint = null;
		}
		UnityEngine.Object.Destroy(jointNode.orientation.gameObject);
		jointNode.hasOrientation = false;
	}
}
