using System.Collections.Generic;
using Poly;
using Poly.Collide;
using UnityEngine;

public class BridgeSelectionSet
{
	public static HashSet<BridgeJoint> m_Joints = new HashSet<BridgeJoint>();

	public static HashSet<BridgeEdge> m_Edges = new HashSet<BridgeEdge>();

	public static HashSet<BridgePillar> m_BridgePillars = new HashSet<BridgePillar>();

	private static List<BridgeJoint> m_JointsRemovalList = new List<BridgeJoint>();

	public static bool IsEmpty()
	{
		if (m_Joints.Count == 0 && m_Edges.Count == 0)
		{
			return m_BridgePillars.Count == 0;
		}
		return false;
	}

	public static bool OnlyContainsJoints()
	{
		if (m_Joints.Count > 0 && m_Edges.Count == 0)
		{
			return m_BridgePillars.Count == 0;
		}
		return false;
	}

	public static bool BridgePillarCount()
	{
		return m_BridgePillars.Count > 0;
	}

	public static void SelectAllInPath(Vector3 start, Vector3 end)
	{
		SelectAllJointsInPath(start, end);
		SelectAllEdgesInPath(start, end);
		SelectAllBridgePillarsInPath(start, end);
	}

	public static void SelectAllInRect(Rect rect, bool invert)
	{
		SelectAllEdgesInRect(rect, invert);
		SelectAllJointsInRect(rect, invert);
		SelectAllBridgePillarsInRect(rect, invert);
	}

	public static bool TrySelectJoint(Vector2 screenPos, bool toggle)
	{
		BridgeJoint bridgeJoint = null;
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(screenPos), out var hitInfo, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			bridgeJoint = hitInfo.transform.parent.GetComponent<BridgeJoint>();
		}
		if (toggle && (bool)bridgeJoint && m_Joints.Contains(bridgeJoint))
		{
			DeSelectJoint(bridgeJoint);
			return true;
		}
		if ((bool)bridgeJoint)
		{
			if (GameStateManager.GetState() == GameState.SANDBOX)
			{
				SelectJointAndConnectedEdges(bridgeJoint);
			}
			else
			{
				SelectJoint(bridgeJoint);
			}
			InterfaceAudio.Play("ui_build_select");
			return true;
		}
		return false;
	}

	public static void TrySelectEdge(Vector2 screenPos, bool toggle)
	{
		BridgeEdge bridgeEdge = null;
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(screenPos), out var hitInfo, float.MaxValue, Utils.EDGE_LAYER_MASK))
		{
			bridgeEdge = hitInfo.transform.parent.GetComponent<BridgeEdge>();
		}
		if (toggle && (bool)bridgeEdge && m_Edges.Contains(bridgeEdge))
		{
			DeSelectEdge(bridgeEdge);
		}
		else if ((bool)bridgeEdge)
		{
			SelectEdge(bridgeEdge);
			InterfaceAudio.Play("ui_build_select");
		}
	}

	public static void TrySelectBridgePillar(Vector2 screenPos, bool toggle)
	{
		BridgePillar bridgePillar = null;
		Ray ray = Cameras.MainCamera().ScreenPointToRay(screenPos);
		if (!Physics.Raycast(ray, out var _, float.MaxValue, Utils.EDGE_LAYER_MASK))
		{
			if (Physics.Raycast(ray, out var hitInfo2, float.MaxValue, Utils.BRIDGE_PILLAR_LAYER_MASK) && hitInfo2.transform.gameObject.layer == Utils.BRIDGE_PILLAR_LAYER)
			{
				bridgePillar = hitInfo2.transform.GetComponentInParent<BridgePillar>();
			}
			if (toggle && (bool)bridgePillar && m_BridgePillars.Contains(bridgePillar))
			{
				DeSelectBridgePillar(bridgePillar);
			}
			else if ((bool)bridgePillar)
			{
				SelectBridgePillar(bridgePillar);
				InterfaceAudio.Play("ui_build_select");
			}
		}
	}

	public static void DeleteSelectionSet()
	{
		List<BridgeJoint> list = DeleteJointsInSelectionSet();
		HashSet<BridgeEdge> hashSet = DeleteEdgesInSelectionSet();
		HashSet<BridgeJoint> hashSet2 = UnAnchorBridgePillarAnchorsInSelectionSet();
		HashSet<BridgePillar> hashSet3 = DeleteBridgePillarsInSelectionSet();
		foreach (BridgeJoint item in list)
		{
			int numConnectedEdges = item.GetNumConnectedEdges();
			for (int i = 0; i < numConnectedEdges; i++)
			{
				BridgeEdge connecteEdge = item.GetConnecteEdge(i);
				if (connecteEdge != null && !hashSet.Contains(connecteEdge))
				{
					hashSet.Add(connecteEdge);
				}
			}
		}
		BridgeEdges.UpdateManual();
		List<BridgeJoint> orphanedJoints = BridgeJoints.GetOrphanedJoints();
		foreach (BridgeJoint item2 in orphanedJoints)
		{
			item2.gameObject.SetActive(value: false);
		}
		list.AddRange(orphanedJoints);
		foreach (BridgePillar item3 in hashSet3)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(item3.m_AnchorGuid);
			if (!list.Contains(bridgeJoint) && !BridgeEdges.EdgeIsConnectedToJoint(bridgeJoint))
			{
				bridgeJoint.gameObject.SetActive(value: false);
				list.Add(bridgeJoint);
			}
			else if (!hashSet2.Contains(bridgeJoint))
			{
				hashSet2.Add(bridgeJoint);
				bridgeJoint.RevertAnchor();
				bridgeJoint.MakeDefaultColor();
			}
		}
		if (hashSet.Count > 0 || list.Count > 0 || hashSet3.Count > 0 || hashSet2.Count > 0)
		{
			if (!BridgeActions.IsRecording())
			{
				BridgeActions.StartRecording();
			}
			BridgeActions.UnMakeAnchors(hashSet2);
			BridgeActions.Delete(hashSet);
			BridgeActions.Delete(list);
			BridgeActions.Delete(hashSet3);
			BridgeJointMovement.CancelSelection();
		}
		CancelSelection();
	}

	public static void CopySelectionSet()
	{
		ClipboardManager.ClearClipboard();
		Vector3 vector = GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()));
		ClipboardManager.SetContainerPosition(vector);
		ClipboardManager.AlignClipboardAnchors();
		Vector2 center = CalculateSelectSetCenter();
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (ContainsPillar(bridgePillar))
			{
				BridgeJoint anchor = bridgePillar.GetAnchor();
				if (anchor != null && !ContainsJoint(anchor))
				{
					AddJoint(anchor);
				}
			}
		}
		CopyJointsInSelectionSet(center);
		CopyEdgesInSelectionSet(center);
		CopyBridgePillarsInSelectionSet(center);
		GameToolMode.SetMode(GameToolModeType.BUILD);
		ClipboardManager.StartMovement(vector);
	}

	private static void CopyJointsInSelectionSet(Vector2 center)
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			if ((joint.m_IsAnchor || BridgePillars.IsBridgePillarAnchor(joint.m_Guid) || !JointIsOrphanInSelectionSet(joint)) && (!joint.m_IsAnchor || BridgePillars.IsBridgePillarAnchor(joint.m_Guid) || BridgeEdges.EdgeIsConnectedToJoint(joint)))
			{
				ClipboardManager.AddJoint(Utils.V3toV2(joint.transform.position) - center, joint);
			}
		}
	}

	private static void CopyEdgesInSelectionSet(Vector2 center)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!m_Joints.Contains(edge.m_JointA))
			{
				ClipboardManager.AddJoint(Utils.V3toV2(edge.m_JointA.transform.position) - center, edge.m_JointA);
			}
			if (!m_Joints.Contains(edge.m_JointB))
			{
				ClipboardManager.AddJoint(Utils.V3toV2(edge.m_JointB.transform.position) - center, edge.m_JointB);
			}
			float z = edge.transform.localEulerAngles.z;
			float length = edge.GetLength();
			ClipboardEdge clipboardEdge = ClipboardManager.AddEdge(Utils.V3toV2(edge.transform.position) - center, z, length, edge);
			clipboardEdge.m_JointA = ClipboardManager.FindClipboardJointMatchingSource(edge.m_JointA.m_Guid);
			clipboardEdge.m_JointB = ClipboardManager.FindClipboardJointMatchingSource(edge.m_JointB.m_Guid);
		}
	}

	private static void CopyBridgePillarsInSelectionSet(Vector2 center)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			ClipboardManager.AddBridgePillar(Utils.V3toV2(bridgePillar.transform.position) - center, bridgePillar);
		}
	}

	public static void CutSelectionSet()
	{
		BridgePillarMovement.CancelMovement();
		CopySelectionSet();
		DeleteSelectionSet();
		BridgeActions.FlushRecording();
	}

	public static void SelectJointAndConnectedEdges(BridgeJoint joint)
	{
		SelectJoint(joint);
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(joint))
		{
			if (item.gameObject.activeInHierarchy)
			{
				AddEdge(item);
			}
		}
	}

	public static void SelectJoint(BridgeJoint joint)
	{
		joint.Select();
		AddJoint(joint);
	}

	public static void DeSelectJoint(BridgeJoint joint)
	{
		joint.DeSelect();
		RemoveJoint(joint);
	}

	public static void DeSelectAllJoints()
	{
		foreach (BridgeJoint joint in m_Joints)
		{
			joint.DeSelect();
		}
		m_Joints.Clear();
	}

	public static void RemoveAnchorsFromSelectionSet()
	{
		m_JointsRemovalList.Clear();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (joint.m_IsAnchor)
			{
				m_JointsRemovalList.Add(joint);
			}
		}
		foreach (BridgeJoint jointsRemoval in m_JointsRemovalList)
		{
			m_Joints.Remove(jointsRemoval);
		}
	}

	public static void DeSelectAllEdges()
	{
		m_Edges.Clear();
	}

	public static void DeSelectAllBridgePillars()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			bridgePillar.DeSelect();
		}
		m_BridgePillars.Clear();
	}

	public static void SelectEdge(BridgeEdge edge)
	{
		AddEdge(edge);
	}

	public static void DeSelectEdge(BridgeEdge edge)
	{
		RemoveEdge(edge);
	}

	public static void SelectBridgePillar(BridgePillar bridgePillar)
	{
		bridgePillar.Select();
		if (!m_BridgePillars.Contains(bridgePillar))
		{
			m_BridgePillars.Add(bridgePillar);
			BridgeJoint anchor = bridgePillar.GetAnchor();
			if (anchor != null)
			{
				anchor.Select();
				AddJoint(anchor);
			}
		}
	}

	public static void DeSelectBridgePillar(BridgePillar bridgePillar)
	{
		bridgePillar.DeSelect();
		if (m_BridgePillars.Contains(bridgePillar))
		{
			m_BridgePillars.Remove(bridgePillar);
			BridgeJoint anchor = bridgePillar.GetAnchor();
			if (anchor != null)
			{
				anchor.DeSelect();
				RemoveJoint(anchor);
			}
		}
	}

	public static void CancelSelection()
	{
		DeSelectAllJoints();
		DeSelectAllEdges();
		if (BridgePillarMovement.IsMovingSelectionSet())
		{
			BridgePillarMovement.CancelMovement();
		}
		DeSelectAllBridgePillars();
	}

	public static bool ContainsJoint(BridgeJoint joint)
	{
		return m_Joints.Contains(joint);
	}

	public static bool ContainsEdge(BridgeEdge edge)
	{
		return m_Edges.Contains(edge);
	}

	public static bool ContainsPillar(BridgePillar pillar)
	{
		return m_BridgePillars.Contains(pillar);
	}

	public static float GetCost()
	{
		float num = 0f;
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsLocked())
			{
				num += edge.m_Material.m_PricePerMeter * edge.GetLength();
			}
		}
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && !bridgePillar.IsLocked())
			{
				num += bridgePillar.Cost();
			}
		}
		return num;
	}

	public static float GetMass()
	{
		float num = BridgeJoints.Mass(m_Joints);
		float num2 = BridgeEdges.Mass(m_Edges);
		return num + num2;
	}

	private static void AddJoint(BridgeJoint joint)
	{
		if (!m_Joints.Contains(joint))
		{
			joint.Select();
			m_Joints.Add(joint);
		}
	}

	private static void AddEdge(BridgeEdge edge)
	{
		if (!m_Edges.Contains(edge))
		{
			m_Edges.Add(edge);
		}
	}

	private static void RemoveJoint(BridgeJoint joint)
	{
		if (m_Joints.Contains(joint))
		{
			m_Joints.Remove(joint);
		}
	}

	private static void RemoveEdge(BridgeEdge edge)
	{
		if (m_Edges.Contains(edge))
		{
			m_Edges.Remove(edge);
		}
	}

	private static Vector2 CalculateSelectSetCenter()
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = 0;
		foreach (BridgeJoint joint in m_Joints)
		{
			num += joint.transform.position.x;
			num2 += joint.transform.position.y;
			num3++;
		}
		foreach (BridgeEdge edge in m_Edges)
		{
			num += edge.m_JointA.transform.position.x;
			num2 += edge.m_JointA.transform.position.y;
			num += edge.m_JointB.transform.position.x;
			num2 += edge.m_JointB.transform.position.y;
			num3 += 2;
		}
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			num += bridgePillar.transform.position.x;
			num2 += bridgePillar.transform.position.y + bridgePillar.GetTotalHeight();
			num3++;
		}
		return Utils.V3toV2(GameGrid.SnapPosToGrid(new Vector3(num / (float)num3, num2 / (float)num3, Cameras.MainCamera().transform.position.z + 1f)));
	}

	private static void SelectAllJointsInPath(Vector3 start, Vector3 end)
	{
		HashSet<BridgeJoint> hashSet = new HashSet<BridgeJoint>();
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && Utils.LineSegmentIntersectsSphere(start, end, joint.transform.position, GameSettings.NodeRadius()))
			{
				hashSet.Add(joint);
			}
		}
		foreach (BridgeJoint item in hashSet)
		{
			SelectJointAndConnectedEdges(item);
		}
	}

	private static void SelectAllEdgesInPath(Vector3 start, Vector3 end)
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && Utils.LineSegmentsIntersect(start, end, edge.m_JointA.transform.position, edge.m_JointB.transform.position))
			{
				SelectEdge(edge);
			}
		}
	}

	private static void SelectAllBridgePillarsInPath(Vector3 start, Vector3 end)
	{
		if (BridgePillars.GetNumActivePillars() == 0)
		{
			return;
		}
		float radius = GameSettings.NodeRadius() - 0.001f;
		PolygonShape shape = PolygonShape.FromSegment((Vec2)end, (Vec2)start, radius);
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && bridgePillar.OverlapsPolygonShape(shape))
			{
				SelectBridgePillar(bridgePillar);
			}
		}
	}

	private static void SelectAllJointsInRect(Rect rect, bool invert)
	{
		HashSet<BridgeJoint> hashSet = new HashSet<BridgeJoint>(m_Joints);
		foreach (BridgeJoint item in BridgeJoints.CollectJointsInRect(rect))
		{
			if (invert && hashSet.Contains(item))
			{
				DeSelectJoint(item);
			}
			else
			{
				SelectJointAndConnectedEdges(item);
			}
		}
	}

	private static void SelectAllEdgesInRect(Rect rect, bool invert)
	{
		HashSet<BridgeEdge> hashSet = new HashSet<BridgeEdge>(m_Edges);
		foreach (BridgeEdge item in BridgeEdges.CollectEdgesInRect(rect))
		{
			if (invert && hashSet.Contains(item))
			{
				DeSelectEdge(item);
			}
			else
			{
				SelectEdge(item);
			}
		}
	}

	private static void SelectAllBridgePillarsInRect(Rect rect, bool invert)
	{
		HashSet<BridgePillar> hashSet = new HashSet<BridgePillar>(m_BridgePillars);
		foreach (BridgePillar item in BridgePillars.CollectInRect(rect))
		{
			if (invert && hashSet.Contains(item))
			{
				DeSelectBridgePillar(item);
			}
			else
			{
				SelectBridgePillar(item);
			}
		}
	}

	private static List<BridgeJoint> DeleteJointsInSelectionSet()
	{
		List<BridgeJoint> list = new List<BridgeJoint>();
		foreach (BridgeJoint joint in m_Joints)
		{
			if (BridgePillars.IsBridgePillarAnchor(joint.m_Guid))
			{
				if (BridgeEdges.EdgeIsConnectedToJoint(joint))
				{
					continue;
				}
				BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(joint.m_Guid);
				if (((bool)bridgePillarWithAnchor && !m_BridgePillars.Contains(bridgePillarWithAnchor)) || ((bool)bridgePillarWithAnchor && bridgePillarWithAnchor.IsLocked() && !Game.InSandboxGodMode()))
				{
					continue;
				}
			}
			if ((!joint.m_IsAnchor || BridgePillars.IsBridgePillarAnchor(joint.m_Guid)) && (!BridgeEdges.LockedEdgesAreConnectedToJoint(joint) || Game.InSandboxGodMode()))
			{
				joint.gameObject.SetActive(value: false);
				list.Add(joint);
			}
		}
		return list;
	}

	private static HashSet<BridgeEdge> DeleteEdgesInSelectionSet()
	{
		HashSet<BridgeEdge> hashSet = new HashSet<BridgeEdge>();
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!edge.IsLocked() || Game.InSandboxGodMode())
			{
				edge.ForceDisable();
				edge.SetStressColor(0f);
				hashSet.Add(edge);
			}
		}
		return hashSet;
	}

	private static HashSet<BridgeJoint> UnAnchorBridgePillarAnchorsInSelectionSet()
	{
		HashSet<BridgeJoint> hashSet = new HashSet<BridgeJoint>();
		foreach (BridgeJoint joint in m_Joints)
		{
			BridgePillar bridgePillarWithAnchor = BridgePillars.GetBridgePillarWithAnchor(joint.m_Guid);
			if (bridgePillarWithAnchor != null && !bridgePillarWithAnchor.IsLocked() && ContainsPillar(bridgePillarWithAnchor) && BridgeEdges.EdgeIsConnectedToJoint(joint))
			{
				hashSet.Add(joint);
				joint.RevertAnchor();
				joint.MakeDefaultColor();
			}
		}
		return hashSet;
	}

	private static HashSet<BridgePillar> DeleteBridgePillarsInSelectionSet()
	{
		HashSet<BridgePillar> hashSet = new HashSet<BridgePillar>();
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (!bridgePillar.IsLocked() || Game.InSandboxGodMode())
			{
				bridgePillar.gameObject.SetActive(value: false);
				hashSet.Add(bridgePillar);
			}
		}
		return hashSet;
	}

	private static bool JointIsOrphanInSelectionSet(BridgeJoint joint)
	{
		foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(joint))
		{
			if (m_Edges.Contains(item))
			{
				return false;
			}
		}
		return true;
	}
}
