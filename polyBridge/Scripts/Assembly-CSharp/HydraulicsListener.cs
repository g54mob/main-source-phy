using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class HydraulicsListener : HydraulicListener
{
	private HashSet<Node> nodesMergedEarly = new HashSet<Node>();

	public override void OnNodeSplit(Node originalNode, Node additionalNewNode)
	{
		BridgeJoint bridgeJoint = (BridgeJoint)originalNode.userData;
		BridgeJoint bridgeJoint2 = BridgeJoints.CreateJoint(new Vector3(additionalNewNode.transform.position.x, additionalNewNode.transform.position.y, bridgeJoint.transform.position.z), Utils.GenerateUniqueId());
		if ((bool)bridgeJoint2)
		{
			bridgeJoint2.m_PhysicsNode = additionalNewNode;
			bridgeJoint2.m_FX.SetActive(value: false);
			bridgeJoint2.m_SnapToFX.SetActive(value: false);
			bridgeJoint2.m_Cap.SetActive(bridgeJoint.m_Cap.activeInHierarchy);
			bridgeJoint2.m_Cap.transform.localScale = new Vector3(0.999f, 0.999f, 1f);
			additionalNewNode.userData = bridgeJoint2;
		}
	}

	public override void OnNodeJoint__NeverTriggered(Node nodeAboutToBeRemoved, Node existingJointNode)
	{
		if (nodeAboutToBeRemoved.userData != null && existingJointNode.userData != null)
		{
			((BridgeJoint)nodeAboutToBeRemoved.userData).gameObject.SetActive(value: false);
		}
	}

	public override void OnEdgeReattached(Edge edge, Node oldEndpoint, Node newEndpoint)
	{
		if (oldEndpoint.userData == null || newEndpoint.userData == null || edge.userData == null)
		{
			return;
		}
		BridgeJoint bridgeJoint = (BridgeJoint)oldEndpoint.userData;
		BridgeJoint bridgeJoint2 = (BridgeJoint)newEndpoint.userData;
		BridgeEdge bridgeEdge = (BridgeEdge)edge.userData;
		if (bridgeEdge.m_JointA == bridgeJoint)
		{
			bridgeEdge.m_JointA.UnregisterEdgeFromCache(bridgeEdge);
			bridgeEdge.m_JointA = bridgeJoint2;
			bridgeEdge.m_JointA.RegisterEdgeInCache(bridgeEdge);
		}
		if (bridgeEdge.m_JointB == bridgeJoint)
		{
			bridgeEdge.m_JointB.UnregisterEdgeFromCache(bridgeEdge);
			bridgeEdge.m_JointB = bridgeJoint2;
			bridgeEdge.m_JointB.RegisterEdgeInCache(bridgeEdge);
		}
		if (bridgeJoint != null)
		{
			bridgeJoint.HideCapIfNoConnectedEdges();
			if (!bridgeJoint.m_Cap.activeInHierarchy)
			{
				bridgeJoint2.m_Cap.gameObject.transform.localScale = Vector3.one;
			}
		}
		if (bridgeEdge.IsPiston())
		{
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(bridgeEdge);
			if (pistonOnEdge.m_JointA == bridgeJoint)
			{
				pistonOnEdge.m_JointA = bridgeJoint2;
				if (!Pistons.PinionIsActiveOnJoint(bridgeJoint2))
				{
					pistonOnEdge.m_PinionA.SetActive(value: true);
					pistonOnEdge.m_PinionA.transform.localScale = bridgeJoint2.m_Cap.transform.localScale;
				}
			}
			if (pistonOnEdge.m_JointB == bridgeJoint)
			{
				pistonOnEdge.m_JointB = bridgeJoint2;
				if (!Pistons.PinionIsActiveOnJoint(bridgeJoint2))
				{
					pistonOnEdge.m_PinionB.SetActive(value: true);
					pistonOnEdge.m_PinionB.transform.localScale = bridgeJoint2.m_Cap.transform.localScale;
				}
			}
		}
		if (bridgeJoint.m_CapMeshTriple.activeInHierarchy && !bridgeJoint2.m_IsSplit)
		{
			switch ((bridgeJoint2 == bridgeEdge.m_JointA) ? bridgeEdge.m_JointAPart : bridgeEdge.m_JointBPart)
			{
			}
		}
	}

	public override void OnPhaseComplete(Node[] mergedNodes_duringLastPhaseOnly)
	{
		foreach (Node node in mergedNodes_duringLastPhaseOnly)
		{
			if (!nodesMergedEarly.Contains(node))
			{
				BridgeJoint bridgeJoint = (BridgeJoint)node.userData;
				if (bridgeJoint.gameObject.activeInHierarchy && !bridgeJoint.m_BridgeJointFlash.IsFlashing())
				{
					bridgeJoint.m_BridgeJointFlash.Flash();
				}
			}
		}
		nodesMergedEarly.Clear();
	}

	public override void OnNodesMergedEarly(Node nodeA, Node nodeB)
	{
		Node node = nodeA;
		for (int i = 0; i < 2; i++)
		{
			BridgeJoint bridgeJoint = (BridgeJoint)node.userData;
			if (bridgeJoint.gameObject.activeInHierarchy && !bridgeJoint.m_BridgeJointFlash.IsFlashing())
			{
				bridgeJoint.m_BridgeJointFlash.Flash();
				nodesMergedEarly.Add(node);
			}
			node = nodeB;
		}
	}

	public override void OnPhaseStart()
	{
		nodesMergedEarly.Clear();
	}

	public override void ClearAndReset()
	{
		nodesMergedEarly.Clear();
	}
}
