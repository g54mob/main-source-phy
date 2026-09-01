using System.Collections.Generic;
using UnityEngine;

public class BridgePillarMovement
{
	public static bool m_IgnoreNextPlacement;

	private static bool m_MovingSelectionSet;

	private static Vector2 m_MouseWorldPosStart;

	public static void UpdateManual(Vector2 mouseScreenPos)
	{
		bool flag = false;
		if ((GameInput.IsDown(BindingType.DRAW_BUILD) || GameInput.IsDown(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonIsDown(GamepadButtonType.NORTH)) && GameToolMode.GetMode() == GameToolModeType.MOVE)
		{
			flag = true;
		}
		if (m_MovingSelectionSet && !flag)
		{
			EndMovement(mouseScreenPos);
		}
		if (!m_MovingSelectionSet)
		{
			return;
		}
		UpdateSelectionSetPosition(mouseScreenPos);
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			if (!bridgePillar.IsLocked())
			{
				BridgeJointPlacement.UpdatePlacementCrosshairs(bridgePillar.m_ClipboardJoint.transform.position);
				break;
			}
		}
	}

	public static void ProcessClick(Vector2 mouseScreenPos)
	{
		BridgeJoint bridgeJoint = null;
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(mouseScreenPos), out var hitInfo, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			bridgeJoint = hitInfo.transform.parent.GetComponent<BridgeJoint>();
		}
		if (!(bridgeJoint != null))
		{
			BridgePillar bridgePillarAtScreenPos = BridgePillars.GetBridgePillarAtScreenPos(GameInput.GetMousePosition());
			if ((bool)bridgePillarAtScreenPos && !bridgePillarAtScreenPos.IsLocked())
			{
				BridgeSelectionSet.SelectBridgePillar(bridgePillarAtScreenPos);
				StartMovement(GameInput.GetMousePosition());
			}
		}
	}

	public static void StartMovement(Vector2 mouseScreenPos)
	{
		if (BridgeSelectionSet.m_BridgePillars.Count == 0 || m_MovingSelectionSet)
		{
			return;
		}
		BridgeActions.StartRecording();
		BridgeActions.SerializeBridgePre(BridgeSave.Serialize(), null);
		m_MovingSelectionSet = true;
		m_IgnoreNextPlacement = true;
		m_MouseWorldPosStart = GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(mouseScreenPos));
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			if (bridgePillar.ConnectedToLockedEdges())
			{
				bridgePillar.DisconnectFromLockedEdges();
			}
			bridgePillar.m_StartMovementWorldPos = bridgePillar.transform.position;
			bridgePillar.m_StartMovementHeight = bridgePillar.GetTotalHeight();
			BridgeJoint anchor = bridgePillar.GetAnchor();
			if (anchor != null)
			{
				anchor.m_StartMovementWorldPos = anchor.transform.position;
			}
			if (bridgePillar.m_ClipboardJoint != null)
			{
				bridgePillar.m_ClipboardJoint.m_Icon.SetActive(value: false);
				bridgePillar.m_ClipboardJoint.m_AnchorIcon.SetActive(value: true);
				bridgePillar.UpdateClipboardJoint(active: true);
			}
			bridgePillar.SetMeshLayer(Utils.RENDER_LAST_LAYER);
		}
	}

	private static bool NoMovement(Vector2 mouseScreenPos)
	{
		return Mathf.Approximately(Vector3.Distance(GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(mouseScreenPos)), m_MouseWorldPosStart), 0f);
	}

	public static void EndMovement(Vector2 mouseScreenPos)
	{
		m_MovingSelectionSet = false;
		m_IgnoreNextPlacement = false;
		int num = 0;
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			if (!BridgePillars.AllowedToPlaceBridgePillar(bridgePillar) || bridgePillar.m_ClipboardJoint.IsBad())
			{
				bridgePillar.RevertToMovementStart();
			}
		}
		foreach (BridgePillar bridgePillar2 in BridgeSelectionSet.m_BridgePillars)
		{
			if (!BridgePillars.AllowedToPlaceBridgePillar(bridgePillar2) || bridgePillar2.m_ClipboardJoint.IsBad())
			{
				bridgePillar2.RevertToMovementStart();
			}
			else
			{
				num++;
				if (DeleteInvalidEdgesAttachedToBridgePillars(BridgeSelectionSet.m_BridgePillars).Count > 0)
				{
					BridgeEdges.UpdateManual();
					BridgeJoints.DestroyOrphanedJoints();
				}
			}
			BridgeJoint anchor = bridgePillar2.GetAnchor();
			anchor.gameObject.SetActive(value: false);
			BridgeJoint bridgeJoint = BridgeJoints.FindClosestJoint(anchor.transform.position);
			anchor.gameObject.SetActive(value: true);
			if ((bool)bridgeJoint && Vector2.Distance(bridgeJoint.transform.position, anchor.transform.position) < GameSettings.NodeRadius())
			{
				if (DeleteConnectedEdgesThatWillDuplicateAfterMerge(anchor, bridgeJoint).Count > 0)
				{
					BridgeEdges.UpdateManual();
					BridgeJoints.DestroyOrphanedJoints();
				}
				BridgeJoints.MergeIntoAnchor(anchor, bridgeJoint);
			}
			bridgePillar2.UpdateClipboardJoint(active: false);
			bridgePillar2.SetColor(BridgePillars.m_NormalColor);
			bridgePillar2.HideAnchorSprites(hide: false);
			bridgePillar2.SetMeshLayer(Utils.BRIDGE_PILLAR_LAYER);
		}
		if (Mathf.Approximately((GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(mouseScreenPos)) - Utils.V2toV3(m_MouseWorldPosStart)).magnitude, 0f))
		{
			BridgeActions.CancelRecording();
		}
		else
		{
			BridgeActions.SerializeBridgePost(BridgeSave.Serialize(), null);
			BridgeActions.FlushRecording();
		}
		if (num > 0)
		{
			InterfaceAudio.Play("ui_build_terrain_place");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
		BridgeSelectionSet.CancelSelection();
	}

	private static HashSet<BridgeEdge> DeleteConnectedEdgesThatWillDuplicateAfterMerge(BridgeJoint anchor, BridgeJoint mergeJoint)
	{
		HashSet<BridgeEdge> hashSet = new HashSet<BridgeEdge>();
		List<BridgeEdge> edgesConnectedToJoint = BridgeEdges.GetEdgesConnectedToJoint(mergeJoint);
		List<BridgeEdge> edgesConnectedToJoint2 = BridgeEdges.GetEdgesConnectedToJoint(anchor);
		foreach (BridgeEdge item in edgesConnectedToJoint)
		{
			BridgeJoint bridgeJoint = ((item.m_JointA == mergeJoint) ? item.m_JointB : item.m_JointA);
			foreach (BridgeEdge item2 in edgesConnectedToJoint2)
			{
				BridgeJoint bridgeJoint2 = ((item2.m_JointA == anchor) ? item2.m_JointB : item2.m_JointA);
				if (bridgeJoint.m_Guid == bridgeJoint2.m_Guid || bridgeJoint2.m_Guid == mergeJoint.m_Guid)
				{
					item2.ForceDisable();
					item2.SetStressColor(0f);
					hashSet.Add(item2);
				}
			}
		}
		return hashSet;
	}

	public static void CancelMovement()
	{
		if (!m_MovingSelectionSet)
		{
			return;
		}
		BridgeActions.CancelRecording();
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			bridgePillar.UpdateClipboardJoint(active: false);
			bridgePillar.RevertToMovementStart();
			bridgePillar.UpdatePolygonShapes();
			bridgePillar.SetColor(BridgePillars.m_NormalColor);
			bridgePillar.HideAnchorSprites(hide: false);
			bridgePillar.SetMeshLayer(Utils.BRIDGE_PILLAR_LAYER);
		}
		m_MovingSelectionSet = false;
		m_IgnoreNextPlacement = false;
	}

	public static bool IsMovingSelectionSet()
	{
		return m_MovingSelectionSet;
	}

	private static void UpdateSelectionSetPosition(Vector2 mouseScreenPos)
	{
		Vector2 vector = (Vector2)Utils.GetWorldPointFromScreenPos(mouseScreenPos) - m_MouseWorldPosStart;
		if (GameInput.IsDown(BindingType.HORIZONTAL_CONSTRAINT_UNIVERSAL))
		{
			vector.y = 0f;
		}
		if (GameInput.IsDown(BindingType.VERTICAL_CONSTRAINT_UNIVERSAL))
		{
			vector.x = 0f;
		}
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			if (bridgePillar.IsLocked())
			{
				continue;
			}
			bridgePillar.transform.position = GameGrid.SnapPosToGrid(new Vector3(bridgePillar.m_StartMovementWorldPos.x + vector.x, 0f, 0f));
			float num = GameGrid.RoundToNearestGridSquare(Mathf.Clamp(bridgePillar.m_StartMovementHeight + vector.y, BridgePillars.MIN_HEIGHT, GameGrid.RoundToNearestGridSquareForced(BridgePillars.GetMaxHeight())));
			bridgePillar.SetTopHeightBasedOnTotalHeight(num);
			BridgeJoint anchor = bridgePillar.GetAnchor();
			anchor.transform.position = GameGrid.SnapPosToGrid(new Vector3(anchor.m_StartMovementWorldPos.x + vector.x, num, anchor.m_StartMovementWorldPos.z));
			anchor.m_SandboxItem.SetOutlineDirty(dirty: true);
			anchor.gameObject.SetActive(value: false);
			BridgeJoint bridgeJoint = BridgeJoints.FindClosestJoint(anchor.transform.position);
			anchor.gameObject.SetActive(value: true);
			if ((bool)bridgeJoint && Vector2.Distance(bridgeJoint.transform.position, anchor.transform.position) < GameSettings.NodeRadius())
			{
				if (bridgeJoint.m_IsAnchor || bridgeJoint.transform.position.y < BridgePillars.MIN_HEIGHT + Mathf.Epsilon || bridgeJoint.transform.position.y > BridgePillars.GetMaxHeight() + Mathf.Epsilon)
				{
					bridgePillar.m_ClipboardJoint.SetBad();
				}
				else
				{
					Vector3 position = bridgePillar.GetAnchor().transform.position;
					bridgePillar.SnapAnchorToPos(bridgeJoint.transform.position);
					if (bridgePillar.HasIllegalPlacement())
					{
						bridgePillar.SnapAnchorToPos(position);
						bridgePillar.m_ClipboardJoint.SetBad();
					}
					else
					{
						bridgePillar.m_ClipboardJoint.SetMerge(bridgeJoint);
					}
				}
			}
			else
			{
				bridgePillar.m_ClipboardJoint.SetNormal();
			}
			bridgePillar.UpdateClipboardJoint(active: true);
		}
		foreach (BridgePillar bridgePillar2 in BridgeSelectionSet.m_BridgePillars)
		{
			if (!bridgePillar2.IsLocked())
			{
				if (BridgePillars.AllowedToPlaceBridgePillar(bridgePillar2) && !bridgePillar2.m_ClipboardJoint.IsBad())
				{
					bridgePillar2.SetColor(BridgePillars.m_NormalColor);
					continue;
				}
				bridgePillar2.SetColor(BridgePillars.m_ErrorColor);
				bridgePillar2.m_ClipboardJoint.SetBad();
			}
		}
	}

	private static HashSet<BridgeEdge> DeleteInvalidEdgesAttachedToBridgePillars(HashSet<BridgePillar> bridgePillars)
	{
		HashSet<BridgeEdge> hashSet = new HashSet<BridgeEdge>();
		foreach (BridgePillar bridgePillar in bridgePillars)
		{
			foreach (BridgeEdge item in BridgeEdges.GetEdgesConnectedToJoint(bridgePillar.GetAnchor()))
			{
				if (!item.IsValidLength())
				{
					item.ForceDisable();
					item.SetStressColor(0f);
					hashSet.Add(item);
				}
			}
		}
		return hashSet;
	}
}
