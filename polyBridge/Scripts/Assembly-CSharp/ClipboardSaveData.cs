using System.Collections.Generic;
using UnityEngine;

public class ClipboardSaveData
{
	public HashSet<ClipboardJointProxy> m_Joints;

	public HashSet<string> m_EdgeGUIDs;

	public HashSet<string> m_PillarGUIDs;

	public List<BridgeJointProxy> m_CutJoints;

	public List<BridgeEdgeProxy> m_CutEdges;

	public List<BridgeSpringProxy> m_CutSprings;

	public List<PistonProxy> m_CutPistons;

	public List<BridgePillarProxy> m_CutPillars;

	public ClipboardSaveData()
	{
		m_Joints = new HashSet<ClipboardJointProxy>();
		m_EdgeGUIDs = new HashSet<string>();
		m_PillarGUIDs = new HashSet<string>();
		m_CutJoints = new List<BridgeJointProxy>();
		m_CutEdges = new List<BridgeEdgeProxy>();
		m_CutSprings = new List<BridgeSpringProxy>();
		m_CutPistons = new List<PistonProxy>();
		m_CutPillars = new List<BridgePillarProxy>();
		foreach (ClipboardJoint joint in ClipboardManager.m_Joints)
		{
			if (!(joint.m_SourceBridgeJoint == null))
			{
				if (joint.m_SourceBridgeJoint.gameObject.activeInHierarchy)
				{
					m_Joints.Add(new ClipboardJointProxy(joint));
				}
				else
				{
					m_CutJoints.Add(new BridgeJointProxy(joint.m_SourceBridgeJoint));
				}
			}
		}
		foreach (ClipboardEdge edge in ClipboardManager.m_Edges)
		{
			if (edge.m_SourceBridgeEdge == null)
			{
				continue;
			}
			if (edge.m_SourceBridgeEdge.gameObject.activeInHierarchy)
			{
				m_EdgeGUIDs.Add(edge.m_SourceBridgeEdge.m_Guid);
				continue;
			}
			m_CutEdges.Add(new BridgeEdgeProxy(edge.m_SourceBridgeEdge));
			if (edge.m_SourceBridgeEdge.IsSpring())
			{
				BridgeSpring springCoilVisualization = edge.m_SourceBridgeEdge.m_SpringCoilVisualization;
				if (springCoilVisualization != null)
				{
					m_CutSprings.Add(new BridgeSpringProxy(springCoilVisualization));
				}
			}
			if (edge.m_SourceBridgeEdge.IsPiston())
			{
				Piston pistonOnEdge = Pistons.GetPistonOnEdge(edge.m_SourceBridgeEdge);
				if (pistonOnEdge != null)
				{
					m_CutPistons.Add(new PistonProxy(pistonOnEdge));
				}
			}
		}
		foreach (ClipboardBridgePillar bridgePillar in ClipboardManager.m_BridgePillars)
		{
			if (!(bridgePillar.m_SourceBridgePillar == null))
			{
				if (bridgePillar.m_SourceBridgePillar.gameObject.activeInHierarchy)
				{
					m_PillarGUIDs.Add(bridgePillar.m_SourceBridgePillar.m_Guid);
				}
				else
				{
					m_CutPillars.Add(new BridgePillarProxy(bridgePillar.m_SourceBridgePillar));
				}
			}
		}
	}

	public void RestoreToClipboard(Quaternion clipboardRot)
	{
		BridgeSelectionSet.CancelSelection();
		foreach (ClipboardJointProxy joint in m_Joints)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(joint.m_Guid);
			if (bridgeJoint != null)
			{
				BridgeSelectionSet.SelectJoint(bridgeJoint);
			}
		}
		foreach (string edgeGUID in m_EdgeGUIDs)
		{
			BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(edgeGUID);
			if (bridgeEdge != null)
			{
				BridgeSelectionSet.SelectEdge(bridgeEdge);
			}
		}
		foreach (string pillarGUID in m_PillarGUIDs)
		{
			BridgePillar bridgePillar = BridgePillars.FindByGuid(pillarGUID);
			if (bridgePillar != null)
			{
				BridgeSelectionSet.SelectBridgePillar(bridgePillar);
			}
		}
		HashSet<BridgeJoint> hashSet = new HashSet<BridgeJoint>();
		foreach (BridgeJointProxy cutJoint in m_CutJoints)
		{
			BridgeJoint bridgeJoint2 = BridgeJoints.CreateJointFromProxy(cutJoint);
			if (bridgeJoint2 != null)
			{
				BridgeSelectionSet.SelectJoint(bridgeJoint2);
				hashSet.Add(bridgeJoint2);
			}
		}
		HashSet<BridgeEdge> hashSet2 = new HashSet<BridgeEdge>();
		foreach (BridgeEdgeProxy cutEdge in m_CutEdges)
		{
			BridgeEdge bridgeEdge2 = BridgeEdges.CreateEdgeFromProxy(cutEdge);
			if (bridgeEdge2 != null)
			{
				BridgeSelectionSet.SelectEdge(bridgeEdge2);
				hashSet2.Add(bridgeEdge2);
			}
		}
		HashSet<BridgeSpring> hashSet3 = new HashSet<BridgeSpring>();
		foreach (BridgeSpringProxy cutSpring in m_CutSprings)
		{
			BridgeSpring bridgeSpring = BridgeSprings.CreateSpringFromProxy(cutSpring);
			if (bridgeSpring != null)
			{
				hashSet3.Add(bridgeSpring);
			}
		}
		HashSet<Piston> hashSet4 = new HashSet<Piston>();
		foreach (PistonProxy cutPiston in m_CutPistons)
		{
			Piston piston = Pistons.CreatePistonFromProxy(cutPiston);
			if (piston != null)
			{
				hashSet4.Add(piston);
			}
		}
		HashSet<BridgePillar> hashSet5 = new HashSet<BridgePillar>();
		foreach (BridgePillarProxy cutPillar in m_CutPillars)
		{
			BridgePillar bridgePillar2 = BridgePillars.CreateBridgePillarFromProxy(cutPillar);
			if (cutPillar != null)
			{
				BridgeSelectionSet.SelectBridgePillar(bridgePillar2);
				hashSet5.Add(bridgePillar2);
			}
		}
		BridgeSelectionSet.CopySelectionSet();
		BridgeSelectionSet.CancelSelection();
		foreach (BridgeJoint item in hashSet)
		{
			item.gameObject.SetActive(value: false);
		}
		foreach (BridgeEdge item2 in hashSet2)
		{
			item2.ForceDisable();
		}
		foreach (BridgeSpring item3 in hashSet3)
		{
			item3.gameObject.SetActive(value: false);
		}
		foreach (Piston item4 in hashSet4)
		{
			item4.gameObject.SetActive(value: false);
		}
		foreach (BridgePillar item5 in hashSet5)
		{
			item5.gameObject.SetActive(value: false);
		}
		foreach (ClipboardJoint joint2 in ClipboardManager.m_Joints)
		{
			foreach (ClipboardJointProxy joint3 in m_Joints)
			{
				if (joint2.m_SourceBridgeJoint.m_Guid == joint3.m_Guid)
				{
					joint2.m_IsSplit = joint3.m_IsSplit;
					joint2.m_ResetJointSelectorsAfterPaste = joint3.m_m_ResetJointSelectorsAfterPaste;
					joint2.transform.localPosition = joint3.m_LocalPos;
					joint2.SetNormal();
					if (joint2.m_IsSplit)
					{
						joint2.DrawAsSplitJoint();
					}
					else
					{
						joint2.DrawAsNonSplitJoint();
					}
				}
			}
		}
		foreach (ClipboardEdge edge in ClipboardManager.m_Edges)
		{
			edge.UpdateTransform();
		}
		foreach (ClipboardBridgePillar bridgePillar3 in ClipboardManager.m_BridgePillars)
		{
			bridgePillar3.UpdateAnchorIcon();
		}
		ClipboardManager.SetClipboardRotation(clipboardRot);
	}
}
