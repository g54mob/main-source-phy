using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BridgeJointMovement
{
	public static BridgeJoint m_SelectedJoint;

	public static bool m_CancelMoveModeOnRelease;

	private static List<BridgeEdge> m_ConstrainedEdges = new List<BridgeEdge>();

	private static Vector2 m_OffsetFromPointer;

	private static List<VectorLine> m_Arcs = new List<VectorLine>();

	public static float DISPLAY_LOCKED_ICON_SECONDS = 2f;

	public static void Init()
	{
	}

	public static void UpdateManual()
	{
		if (!m_SelectedJoint)
		{
			return;
		}
		if (GameInput.IsDown(BindingType.DRAW_BUILD) || GameInput.IsDown(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonIsDown(GamepadButtonType.NORTH))
		{
			MoveSelectedJoint(m_SelectedJoint, GameInput.GetMousePosition());
			BridgeJointPlacement.UpdatePlacementCrosshairs(m_SelectedJoint.transform.position);
			if (!Mathf.Approximately((m_SelectedJoint.transform.position - m_SelectedJoint.m_BuildPos).magnitude, 0f))
			{
				m_SelectedJoint.TryRecreateSpringVisualizationForAttachedEdges();
			}
			UpdateArcs();
		}
		else
		{
			FinalizeMovement();
		}
	}

	public static void FinalizeMovement()
	{
		Vector3 translation = m_SelectedJoint.transform.position - m_SelectedJoint.m_BuildPos;
		if (!Mathf.Approximately(translation.magnitude, 0f))
		{
			m_SelectedJoint.m_BuildPos = m_SelectedJoint.transform.position;
			BridgeActions.StartRecording();
			BridgeActions.Translate(m_SelectedJoint, translation);
			BridgeActions.FlushRecording();
			m_SelectedJoint.TryRecreateSpringVisualizationForAttachedEdges();
		}
		CancelSelection();
	}

	public static void ProcessClick(Vector2 mouseScreenPos)
	{
		BridgeJoint bridgeJoint = null;
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(mouseScreenPos), out var hitInfo, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			bridgeJoint = hitInfo.transform.parent.GetComponent<BridgeJoint>();
		}
		if (!bridgeJoint || (bridgeJoint.m_IsAnchor && !BridgePillars.IsBridgePillarAnchor(bridgeJoint.m_Guid)) || (bool)m_SelectedJoint || CampaignTutorial.BlockMoveAction() || BridgeTrace.IsFilling())
		{
			return;
		}
		if (!BridgeEdges.LockedEdgesAreConnectedToJoint(bridgeJoint) || Game.InSandboxGodMode())
		{
			if (BridgePillars.IsBridgePillarAnchor(bridgeJoint.m_Guid))
			{
				BridgeSelectionSet.SelectBridgePillar(BridgePillars.GetBridgePillarWithAnchor(bridgeJoint.m_Guid));
				BridgePillarMovement.StartMovement(GameInput.GetMousePosition());
			}
			else
			{
				SelectJoint(bridgeJoint);
				m_CancelMoveModeOnRelease = false;
			}
			BridgeTrace.TurnOffTracing();
		}
		else
		{
			BridgeEdges.DisplayLockIconForLockedEdgesConnectedToJoint(bridgeJoint, DISPLAY_LOCKED_ICON_SECONDS);
		}
	}

	public static void SelectJoint(BridgeJoint joint)
	{
		if (!CampaignTutorial.BlockMoveJoint(joint))
		{
			joint.Select();
			joint.m_MoveStartPos = joint.transform.position;
			m_SelectedJoint = joint;
			Vector2 vector = (Vector2)Cameras.MainCamera().WorldToScreenPoint(joint.transform.position) - Utils.V3toV2(GameInput.GetMousePosition());
			m_OffsetFromPointer = new Vector2(vector.x, vector.y);
			if (!Game.IsCurrentLevelTutorial())
			{
				CreateMovementBoundary();
			}
		}
	}

	public static void CancelSelection()
	{
		if ((bool)m_SelectedJoint)
		{
			m_SelectedJoint.DeSelect();
		}
		m_SelectedJoint = null;
		m_CancelMoveModeOnRelease = false;
		DisableArcs();
	}

	public static bool ModMoveJointToPointLegally(BridgeJoint moveJoint, Vector3 targetPos)
	{
		List<BridgeEdge> edgesConnectedToJoint = BridgeEdges.GetEdgesConnectedToJoint(moveJoint);
		moveJoint.m_MoveStartPos = moveJoint.transform.position;
		moveJoint.transform.position = targetPos;
		RefreshEdgeTransforms(edgesConnectedToJoint);
		Budget.UpdateBridgeCost();
		if (!IsJointAtInvalidLocation(moveJoint, edgesConnectedToJoint) && AllEdgesValidLength(edgesConnectedToJoint) && Budget.CanAffordToBuild())
		{
			return true;
		}
		moveJoint.transform.position = moveJoint.m_MoveStartPos;
		RefreshEdgeTransforms(edgesConnectedToJoint);
		Budget.UpdateBridgeCost();
		return false;
	}

	private static void MoveSelectedJoint(BridgeJoint moveJoint, Vector2 mouseScreenPos)
	{
		List<BridgeEdge> edgesConnectedToJoint = BridgeEdges.GetEdgesConnectedToJoint(moveJoint);
		Vector3 vector = CalculateTargetPos(GameGrid.SnapPosToGrid(Utils.V2toV3(Utils.GetWorldPointFromScreenPos(mouseScreenPos + m_OffsetFromPointer))), moveJoint, edgesConnectedToJoint);
		if (GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL))
		{
			vector = new Vector3(vector.x, moveJoint.transform.position.y, vector.z);
		}
		if (GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
		{
			vector = new Vector3(moveJoint.transform.position.x, vector.y, vector.z);
		}
		Vector3 vector2 = vector - moveJoint.transform.position;
		Vector3 normalized = vector2.normalized;
		moveJoint.m_MoveStartPos = moveJoint.transform.position;
		float num = 0.01f;
		float num2 = 0f;
		float magnitude = vector2.magnitude;
		for (float num3 = 0f; num3 < magnitude; num3 += num)
		{
			moveJoint.transform.position = GameGrid.SnapPosToGrid(moveJoint.m_MoveStartPos + normalized * num3);
			RefreshEdgeTransforms(edgesConnectedToJoint);
			Budget.UpdateBridgeCost();
			if (!IsJointAtInvalidLocation(moveJoint, edgesConnectedToJoint) && AllEdgesValidLength(edgesConnectedToJoint) && Budget.CanAffordToBuild())
			{
				num2 = num3;
			}
		}
		if (Mathf.Approximately(num2, 0f))
		{
			moveJoint.transform.position = moveJoint.m_MoveStartPos;
			RefreshEdgeTransforms(edgesConnectedToJoint);
			Budget.UpdateBridgeCost();
			return;
		}
		moveJoint.transform.position = GameGrid.SnapPosToGrid(moveJoint.m_MoveStartPos + normalized * num2);
		RefreshEdgeTransforms(edgesConnectedToJoint);
		Budget.UpdateBridgeCost();
		foreach (BridgeEdge item in edgesConnectedToJoint)
		{
			item.UpdateJointSelectors();
			item.ResolveJointSelectorOverlap();
			item.MaybeSetRopeCableTiling();
		}
		GameStateBuild.ClearFirstBreakAttachedToJoint(moveJoint.m_Guid);
	}

	private static Vector3 CalculateTargetPos(Vector3 mouseWorldPos, BridgeJoint joint, List<BridgeEdge> edges)
	{
		if (PosInsideBoundary(mouseWorldPos, joint, edges))
		{
			return mouseWorldPos;
		}
		List<Vector3> list = new List<Vector3>();
		foreach (BridgeEdge edge in edges)
		{
			if (edge.m_Material.m_MaterialType != BridgeMaterialType.ROPE && edge.m_Material.m_MaterialType != BridgeMaterialType.CABLE)
			{
				BridgeJoint bridgeJoint = ((edge.m_JointA == joint) ? edge.m_JointB : edge.m_JointA);
				Vector3 normalized = (mouseWorldPos - bridgeJoint.transform.position).normalized;
				Vector3 vector = bridgeJoint.transform.position + normalized * edge.GetMaxLength();
				Vector3 normalized2 = (bridgeJoint.transform.position - mouseWorldPos).normalized;
				Vector3 normalized3 = (joint.transform.position - mouseWorldPos).normalized;
				if (!(Vector3.Dot(normalized2, normalized3) < 0f) && PosSatisfiesAllContraints(vector, bridgeJoint, edges))
				{
					list.Add(vector);
				}
			}
		}
		if (list.Count == 0)
		{
			return mouseWorldPos;
		}
		Vector3 result = list[0];
		float num = float.MaxValue;
		foreach (Vector3 item in list)
		{
			float num2 = Vector3.Distance(mouseWorldPos, item);
			if (num2 < num)
			{
				num = num2;
				result = item;
			}
		}
		return result;
	}

	private static bool PosInsideBoundary(Vector3 pos, BridgeJoint joint, List<BridgeEdge> edges)
	{
		foreach (BridgeEdge edge in edges)
		{
			BridgeJoint anchor = ((edge.m_JointA == m_SelectedJoint) ? edge.m_JointB : edge.m_JointA);
			if (!PosSatisfiesContraint(pos, anchor, edge.GetMaxLength()))
			{
				return false;
			}
		}
		return true;
	}

	private static bool PosSatisfiesAllContraints(Vector3 pos, BridgeJoint anchor, List<BridgeEdge> edges)
	{
		foreach (BridgeEdge edge in edges)
		{
			BridgeJoint bridgeJoint = ((edge.m_JointA == m_SelectedJoint) ? edge.m_JointB : edge.m_JointA);
			if (!(bridgeJoint == anchor) && !PosSatisfiesContraint(pos, bridgeJoint, edge.GetMaxLength()))
			{
				return false;
			}
		}
		return true;
	}

	private static bool PosSatisfiesContraint(Vector3 pos, BridgeJoint anchor, float radius)
	{
		return Vector3.Distance(pos, anchor.transform.position) < radius;
	}

	private static bool AllEdgesValidLength(List<BridgeEdge> edges)
	{
		foreach (BridgeEdge edge in edges)
		{
			if (!EdgeIsValidLength(edge))
			{
				return false;
			}
		}
		return true;
	}

	private static void RefreshEdgeTransforms(List<BridgeEdge> edges)
	{
		foreach (BridgeEdge edge in edges)
		{
			edge.UpdateTransform();
		}
	}

	private static bool IsJointAtInvalidLocation(BridgeJoint joint, List<BridgeEdge> connectedEdges)
	{
		if (!WorldBounds.Contains(joint.transform.position))
		{
			WorldBounds.ShowBriefly();
			return true;
		}
		if (BridgeJoints.JointOverlapsOtherJoints(joint, 0f, GameSettings.NodeDiameter()))
		{
			return true;
		}
		if (BridgeJoints.NodeLocationOverlapsBlockingPolygonShape(joint.transform.position) || !BuildZones.ContainsJoint(joint.transform.position))
		{
			return true;
		}
		foreach (BridgeEdge connectedEdge in connectedEdges)
		{
			BridgeJoint bridgeJoint = ((joint == connectedEdge.m_JointA) ? connectedEdge.m_JointB : connectedEdge.m_JointA);
			if (BridgeEdges.EdgeLocationOverlapsBlockingPolygonShape(joint.transform.position, bridgeJoint.transform.position, connectedEdge.m_Material.m_MaterialType, connectedEdge.m_Material.m_EdgeMaterial.collisionRadius))
			{
				return true;
			}
			if (!BuildZones.ContainsEdge(joint.transform.position, bridgeJoint.transform.position))
			{
				return true;
			}
		}
		return false;
	}

	private static void CreateMovementBoundary()
	{
		for (int i = 0; i < m_Arcs.Count; i++)
		{
			VectorLine line = m_Arcs[i];
			if (line != null)
			{
				VectorLine.Destroy(ref line);
			}
		}
		m_Arcs.Clear();
		List<BridgeEdge> edgesConnectedToJoint = BridgeEdges.GetEdgesConnectedToJoint(m_SelectedJoint);
		foreach (BridgeEdge item in edgesConnectedToJoint)
		{
			if (item.m_Material.m_MaterialType != BridgeMaterialType.ROPE && item.m_Material.m_MaterialType != BridgeMaterialType.CABLE)
			{
				CreateArcs((item.m_JointA == m_SelectedJoint) ? item.m_JointB : item.m_JointA, item.GetMaxLength(), edgesConnectedToJoint);
			}
		}
	}

	private static void CreateArcs(BridgeJoint anchor, float radius, List<BridgeEdge> constraints)
	{
		float num = 0.05f;
		float num2 = MathF.PI * 2f * radius;
		float num3 = 360f * (num / num2);
		int num4 = Mathf.RoundToInt(360f / num3);
		bool flag = false;
		float num5 = 0f;
		float startDegrees = 0f;
		for (int i = 0; i < num4; i++)
		{
			if (PosSatisfiesAllContraints(anchor.transform.position + new Vector3(radius * Mathf.Sin(MathF.PI / 180f * num5), radius * Mathf.Cos(MathF.PI / 180f * num5), 0f), anchor, constraints))
			{
				if (!flag)
				{
					flag = true;
					startDegrees = num5;
				}
			}
			else if (flag)
			{
				flag = false;
				VectorLine vectorLine = CreateArc();
				vectorLine.MakeArc(anchor.transform.position + new Vector3(0f, 0f, -10f), -Vector3.forward, radius, radius, startDegrees, num5);
				vectorLine.active = true;
			}
			num5 += num3;
		}
		if (flag)
		{
			VectorLine vectorLine2 = CreateArc();
			vectorLine2.MakeArc(anchor.transform.position + new Vector3(0f, 0f, -10f), -Vector3.forward, radius, radius, startDegrees, Mathf.Min(num5, 360f));
			vectorLine2.active = true;
		}
	}

	private static VectorLine CreateArc()
	{
		VectorLine vectorLine = new VectorLine("arc", new List<Vector3>(128), GameUI.m_Instance.m_PlacementLineTexture, 6f);
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.layer = Utils.FOREGROUND_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = GameUI.PlacementLineColor();
		vectorLine.AddNormals();
		vectorLine.SetWidth(GameUI.m_Instance.m_PlacementLineWidth / Cameras.MainCamera().orthographicSize);
		vectorLine.Draw3DAuto();
		m_Arcs.Add(vectorLine);
		return vectorLine;
	}

	private static void UpdateArcs()
	{
		foreach (VectorLine arc in m_Arcs)
		{
			arc.SetWidth(GameUI.m_Instance.m_PlacementLineWidth / Cameras.MainCamera().orthographicSize);
		}
	}

	private static void DisableArcs()
	{
		foreach (VectorLine arc in m_Arcs)
		{
			arc.active = false;
		}
	}

	private static bool EdgeIsValidLength(BridgeEdge edge)
	{
		float length = edge.GetLength();
		float num = GameSettings.NodeDiameter();
		float maxLength = edge.GetMaxLength();
		if (length < maxLength + 1E-06f)
		{
			return length > num - 1E-06f;
		}
		return false;
	}
}
