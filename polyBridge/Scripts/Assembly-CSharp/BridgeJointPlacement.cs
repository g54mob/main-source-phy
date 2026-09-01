using System;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BridgeJointPlacement
{
	public static bool m_IgnoreEdgePlacementRestrictions;

	public static BridgeJoint m_SelectedJoint;

	public static BridgeJoint m_HoverJoint;

	public static BridgeJoint m_SnapToJoint;

	private const float HOVER_THESHOLD_SECONDS = 0.1f;

	private static float m_HoverJointLingeredSeconds;

	private static VectorLine m_PlacementLine;

	private static VectorLine m_PlacementCrosshairs;

	private static GameObject m_PlacementDot;

	private static GameObject m_SnapTraceDot;

	private static BridgeEdge m_SnapTraceDotEdge;

	private static SpriteRenderer m_PlacementDotSpriteRenderer;

	private static float m_SelectionPressedSeconds;

	private static List<VectorLine> m_TriangulateLines = new List<VectorLine>();

	private static List<BridgeJoint> m_TriangulateJoints = new List<BridgeJoint>();

	private static BridgeJoint m_LastClickedJoint;

	private static float m_LastClickedJointTime;

	private const int MAX_SELECTION_CIRCLE_DOTS = 132;

	private static GameObject m_SelectionCircleDotsParent;

	private static float m_SelectionCircleDotsRadius;

	private static List<GameObject> m_SelectionCircleDots = new List<GameObject>();

	private static readonly float PLACEMENT_LINE_Z = -4f;

	private static readonly float PLACEMENT_DOT_Z = -4f;

	private static readonly float SNAP_MAX_DISTANCE = 1f;

	public static void Init()
	{
		CreatePlacementDot();
		CreateSnapDot();
		CreateSelectionCircleDots();
		if (Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_BuildToolTip.m_RectTransform.localScale = new Vector2(1.2f, 1.2f);
			GameUI.m_Instance.m_BuildToolTip.m_RectTransform.anchorMin = new Vector2(0.5f, 1f);
			GameUI.m_Instance.m_BuildToolTip.m_RectTransform.anchorMax = new Vector2(0.5f, 1f);
			GameUI.m_Instance.m_BuildToolTip.m_RectTransform.pivot = new Vector2(0.5f, 1f);
		}
	}

	public static void UpdateManual()
	{
		UpdateHoverJoint();
		UpdateSnapToJoint();
		UpdateSelectionCircleDots();
		if (!InPlacementMode() || Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR)
		{
			DisablePlacementUI();
			return;
		}
		Vector3 vector = new Vector3(m_SelectedJoint.transform.position.x, m_SelectedJoint.transform.position.y, PLACEMENT_LINE_Z);
		Vector3 lineEndPos = CalculateConstrainedEndPointNoGridSnap(vector);
		UpdateSnapTraceDot(vector, lineEndPos);
		if (!m_SnapTraceDot.activeInHierarchy)
		{
			lineEndPos = CalculateConstrainedEndPoint(vector);
		}
		UpdatePlacementLine(vector, lineEndPos);
		UpdatePlacementDot(vector, lineEndPos);
		UpdatePlacementCrosshairs(m_PlacementDot.transform.position);
		UpdateTriangulateLines();
		UpdatePlacementTooltip();
		MaybeAutoDraw();
		if (EdgeBisectDotActive())
		{
			m_PlacementDot.SetActive(value: false);
		}
		CheckForSwitchToMovement();
	}

	public static bool InPlacementMode()
	{
		if (BridgePillarPlacement.InPlacementMode() || Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR)
		{
			return false;
		}
		if (!m_SelectedJoint || BridgeTrace.IsTracingActive() || !m_SelectedJoint.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (GameToolMode.GetMode() != GameToolModeType.BUILD)
		{
			return false;
		}
		return true;
	}

	public static void OnLayoutLoaded()
	{
		if (m_PlacementLine != null)
		{
			VectorLine.Destroy(ref m_PlacementLine);
		}
		if (m_PlacementCrosshairs != null)
		{
			VectorLine.Destroy(ref m_PlacementCrosshairs);
		}
		m_PlacementLine = CreatePlacementLine();
		m_PlacementCrosshairs = CreatePlacementCrosshairs();
	}

	public static void ProcessClick(Vector2 mouseScreenPos)
	{
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_HydraulicsController.ProcessClick(mouseScreenPos);
		}
		else
		{
			if (ClipboardManager.ReadyToPaste() || (!InPlacementMode() && BridgeJointSelectors.CycleUnderMouse(mouseScreenPos, forward: true)))
			{
				return;
			}
			BridgePillar bridgePillar = BridgePillarPlacement.SelectedBridgePillarAtScreenPos(mouseScreenPos);
			if ((bool)bridgePillar)
			{
				if (!bridgePillar.IsLocked())
				{
					BridgePillarMovement.StartMovement(mouseScreenPos);
				}
				return;
			}
			PlacementReturnValue placementReturnValue = ProcessClickInternal(mouseScreenPos, preview: false);
			if (placementReturnValue != PlacementReturnValue.SUCCESS)
			{
				BridgePlacement.DisplayPlacementFailureMessage(placementReturnValue);
				if ((bool)m_SelectedJoint)
				{
					BridgePlacement.PlayFailPlacement(placementReturnValue);
				}
			}
			if (BridgeTrace.IsTracingActive())
			{
				BridgeTrace.ProcessSoftButtonDown();
			}
		}
	}

	public static void UpdatePlacementCrosshairs(Vector3 pos)
	{
		m_PlacementCrosshairs.active = true;
		m_PlacementCrosshairs.points3[0] = pos + new Vector3(0f, Mathf.Ceil((float)Screen.height / 2f), PLACEMENT_LINE_Z);
		m_PlacementCrosshairs.points3[1] = pos + new Vector3(0f, 0f - Mathf.Ceil((float)Screen.height / 2f), PLACEMENT_LINE_Z);
		m_PlacementCrosshairs.points3[2] = pos + new Vector3(Mathf.Ceil((float)Screen.width / 2f), 0f, PLACEMENT_LINE_Z);
		m_PlacementCrosshairs.points3[3] = pos + new Vector3(0f - Mathf.Ceil((float)Screen.width / 2f), 0f, PLACEMENT_LINE_Z);
		m_PlacementCrosshairs.SetWidth(GameUI.m_Instance.m_PlacementCrosshairsLineWidth / Cameras.MainCamera().orthographicSize);
	}

	public static void UpdatePlacementTooltip()
	{
		if (Profiles.m_ActiveProfile.m_DisableBuildDataTooltips || CampaignTutorial.IsRunning() || !GameUI.HudIsActive())
		{
			GameUI.m_Instance.m_BuildToolTip.Disable();
			return;
		}
		GameUI.m_Instance.m_BuildToolTip.ForceEnable();
		GameUI.m_Instance.m_BuildToolTip.Set(GetPlacementTooltipText(), null);
		if (Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_BuildToolTip.m_RectTransform.anchoredPosition = new Vector2(0f, (GameManager.GetGameMode() == GameMode.SANDBOX) ? (-110f) : (-55f));
		}
		else
		{
			GameUI.SetScreenPosClamped(GameUI.m_Instance.m_BuildToolTip.gameObject, GameInput.GetMousePosition(), 30f, GameUI.GetSecondaryBuildTooltipY());
		}
	}

	public static void ProcessDoubleClickOnJoint(BridgeJoint joint)
	{
		if (Game.IsCurrentLevelTutorial())
		{
			if (CampaignTutorial.CanSplitJoint(joint))
			{
				joint.Split();
				joint.ResetJointSelectors();
				HydraulicsController.AddSplitJointToAllPhasesAcceptingNewAdditions(joint);
				InterfaceAudio.Play("ui_build_splitJoint_create");
			}
		}
		else if (joint.m_IsSplit)
		{
			BridgeActions.StartRecording();
			BridgeActions.UnSplitJoint(joint);
			joint.UnSplit();
			BridgeActions.FlushRecording();
			InterfaceAudio.Play("ui_build_splitJoint_remove");
		}
		else if (HydraulicsPhases.m_Phases.Count > 0)
		{
			BridgeActions.StartRecording();
			joint.Split();
			joint.ResetJointSelectors();
			HydraulicsController.AddSplitJointToAllPhasesAcceptingNewAdditions(joint);
			BridgeActions.SplitJoint(joint);
			BridgeActions.FlushRecording();
			InterfaceAudio.Play("ui_build_splitJoint_create");
		}
		else
		{
			GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, Localize.Get("WARN_SPLIT_JOINT_CREATE"), ScreenMessage.DEFAULT_DURATION_SECONDS);
		}
	}

	public static void ClearDoubleClickTimer()
	{
		m_LastClickedJointTime = 0f;
	}

	public static void ClearTriangulateJoints()
	{
		m_TriangulateJoints.Clear();
	}

	public static float GetPlacementCost()
	{
		if (m_SelectedJoint == null)
		{
			return 0f;
		}
		float placementEdgeLength = GetPlacementEdgeLength();
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType);
		if (!(bridgeMaterial == null))
		{
			return bridgeMaterial.m_PricePerMeter * placementEdgeLength;
		}
		return 0f;
	}

	public static bool EdgeBisectDotActive()
	{
		if (m_SnapTraceDot.activeInHierarchy)
		{
			return m_SnapTraceDotEdge != null;
		}
		return false;
	}

	public static BridgeMaterialType GetMaterialTypeToBeBisected()
	{
		if (!(m_SnapTraceDotEdge != null))
		{
			return BridgeMaterialType.INVALID;
		}
		return m_SnapTraceDotEdge.m_Material.m_MaterialType;
	}

	private static float GetPlacementEdgeLength()
	{
		if (m_SelectedJoint == null)
		{
			return 0f;
		}
		float num = Vector3.Distance(m_SelectedJoint.transform.position, GetPlacementPos());
		for (int i = 0; i < m_TriangulateJoints.Count; i++)
		{
			BridgeJoint bridgeJoint = m_TriangulateJoints[i];
			num += Vector3.Distance(bridgeJoint.transform.position, GetPlacementDotPos());
		}
		return num;
	}

	private static float GetPlacementAngle()
	{
		Vector3 normalized = (GetPlacementPos() - m_SelectedJoint.transform.position).normalized;
		if (Vector3.Dot(Vector3.right, normalized) >= 0f)
		{
			return Mathf.Sign(Vector3.Dot(Vector3.up, normalized)) * Vector3.Angle(Vector3.right, normalized);
		}
		return Mathf.Sign(Vector3.Dot(Vector3.up, normalized)) * Vector3.Angle(-Vector3.right, normalized);
	}

	private static string GetPlacementTooltipText()
	{
		float placementEdgeLength = GetPlacementEdgeLength();
		float placementAngle = GetPlacementAngle();
		string text = Utils.FormatCashNoDollarSign(Mathf.RoundToInt(GetPlacementCost()));
		string text2 = Utils.FormatTwoDecimalPlaces(placementEdgeLength);
		string text3 = Utils.FormatAngle(placementAngle);
		string text4 = "<size=14><voffset=-0.20em>";
		string text5 = "<size=12></voffset>";
		string text6 = text4 + "<sprite name=tooltip_cost>" + text5;
		string text7 = text4 + "<sprite name=tooltip_ruler>" + text5;
		string text8 = text4 + "<sprite name=tooltip_angle>" + text5;
		string text9 = "   ";
		string text10 = string.Format(text6 + text + text9 + text7 + text2 + "m" + text9 + text8 + text3);
		Vector3 b = GameGrid.SnapPosToGrid(Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()));
		float num = Vector3.Distance(m_SelectedJoint.transform.position, b);
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType);
		if (bridgeMaterial != null && num > bridgeMaterial.m_MaxLength)
		{
			string text11 = Utils.FormatTwoDecimalPlaces(num);
			string text12 = text4 + "<sprite name=tooltip_distance>" + text5;
			text10 += string.Format(text9 + text12 + " " + text11 + "m");
		}
		return text10;
	}

	private static PlacementReturnValue ProcessClickInternal(Vector2 screenPos, bool preview)
	{
		if (BridgePillarPlacement.InPlacementMode() || Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR)
		{
			return PlacementReturnValue.FAIL;
		}
		BridgeJoint bridgeJoint = GetJointUnderRay(Cameras.MainCamera().ScreenPointToRay(screenPos));
		if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && !preview && bridgeJoint == null && GameStateBuild.SnapCursorEnabled())
		{
			bridgeJoint = SnapCursorToClosestNode();
		}
		BridgeJoint selectedJoint = m_SelectedJoint;
		bool flag = false;
		if (!preview && (bool)bridgeJoint)
		{
			if (bridgeJoint == m_LastClickedJoint && Time.realtimeSinceStartup - m_LastClickedJointTime < GameUI.DOUBLE_CLICK_THRESHOLD_SECONDS)
			{
				flag = true;
				m_LastClickedJoint = null;
				m_LastClickedJointTime = 0f;
			}
			else
			{
				m_LastClickedJoint = bridgeJoint;
				m_LastClickedJointTime = Time.realtimeSinceStartup;
			}
		}
		if ((bool)bridgeJoint && !selectedJoint)
		{
			if (!preview)
			{
				SelectJoint(bridgeJoint);
				if (flag)
				{
					ProcessDoubleClickOnJoint(bridgeJoint);
				}
			}
			return PlacementReturnValue.SUCCESS;
		}
		if ((bool)selectedJoint && bridgeJoint == selectedJoint)
		{
			float num = Vector2.Distance(m_SelectedJoint.transform.position, GameGrid.SnapPosToGrid(GetPlacementPos()));
			if (!(num > GameSettings.NodeDiameter()))
			{
				if (!preview && num < GameSettings.NodeDiameter())
				{
					DeSelectJoint(selectedJoint);
					if (flag)
					{
						ProcessDoubleClickOnJoint(bridgeJoint);
					}
				}
				if (!(num < GameSettings.NodeRadius()))
				{
					return PlacementReturnValue.FAIL;
				}
				return PlacementReturnValue.SUCCESS;
			}
			bridgeJoint = null;
		}
		if ((bool)selectedJoint && bridgeJoint != selectedJoint)
		{
			if (BridgeTrace.IsTracingActive())
			{
				BridgeTrace.AttachToJoint(bridgeJoint);
				return PlacementReturnValue.SUCCESS;
			}
			Vector3 placementPos = (bridgeJoint ? bridgeJoint.transform.position : GetPlacementPos());
			PlacementReturnValue placementReturnValue = AllowPlacement(selectedJoint, placementPos);
			if (placementReturnValue == PlacementReturnValue.SUCCESS)
			{
				return TryFormEdgeBetweenJoints(selectedJoint, bridgeJoint, GetPlacementPos(), Bridge.m_BuildMaterialType, preview);
			}
			return placementReturnValue;
		}
		if (!preview)
		{
			return PlacementReturnValue.FAIL;
		}
		return PlacementReturnValue.SUCCESS;
	}

	public static PlacementReturnValue TryFormEdgeBetweenJoints(BridgeJoint selectedJoint, BridgeJoint clickedJoint, Vector3 placementPos, BridgeMaterialType material, bool preview)
	{
		if (!clickedJoint || ((bool)clickedJoint && !BridgeEdges.CanFormEdgeBetweenJoints(null, selectedJoint, clickedJoint, material)))
		{
			bool num = BridgeEdges.IsValidEdgeLength(Vector2.Distance(selectedJoint.transform.position, placementPos), GameSettings.NodeDiameter(), BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType));
			BridgeJoint bridgeJoint = BridgeJoints.FindClosestJoint(placementPos);
			if (num)
			{
				if ((bool)bridgeJoint && Vector2.Distance(placementPos, bridgeJoint.transform.position) < GameSettings.NodeRadius())
				{
					clickedJoint = bridgeJoint;
				}
				else if (BridgeJoints.CanCreateJointAtPosition(placementPos, selectedJoint.transform.position, Bridge.m_BuildMaterialType) && Budget.CanAffordEdge(Vector3.Distance(selectedJoint.transform.position, placementPos), Bridge.m_BuildMaterialType))
				{
					if (Game.IsCurrentLevelTutorial() && !CampaignTutorial.CanPlaceJoint(selectedJoint, placementPos))
					{
						return PlacementReturnValue.FAIL;
					}
					if (!preview)
					{
						BridgeJoint bridgeJoint2 = BridgeJoints.CreateJoint(placementPos, Utils.GenerateUniqueId());
						if (bridgeJoint2 != null)
						{
							BridgeActions.StartRecording();
							BridgeActions.Create(bridgeJoint2);
							BridgeEdge bridgeEdge = BridgeEdges.CreateEdgeWithPistonOrSpring(selectedJoint, bridgeJoint2, Bridge.m_BuildMaterialType);
							if ((bool)bridgeEdge)
							{
								MaybeMakeEdgeSliderVisible(bridgeEdge);
								BridgeAudio.PlayCreateEdge(bridgeEdge.m_Material.m_MaterialType);
								BridgeActions.Create(bridgeEdge);
								if (m_SnapTraceDotEdge != null && m_SnapTraceDot.gameObject.activeInHierarchy && HasMaterialForSplit(m_SnapTraceDotEdge, Bridge.m_BuildMaterialType))
								{
									SplitEdgeWithNode(m_SnapTraceDotEdge, bridgeJoint2, selectedJoint);
								}
								Triangulate(bridgeJoint2, bridgeEdge.m_Material.m_MaterialType);
							}
							BridgeActions.FlushRecording();
							DeSelectJoint(selectedJoint);
							SelectJoint(bridgeJoint2);
						}
					}
					return PlacementReturnValue.SUCCESS;
				}
			}
		}
		if (!clickedJoint)
		{
			return PlacementReturnValue.FAIL_NODE_ILLEGAL_POSITION;
		}
		if (!BridgeJoints.JointsCanAddEdgeWithoutExceedingEdgeLimit(selectedJoint, clickedJoint))
		{
			return PlacementReturnValue.FAIL_EXCEEDS_MAX_EDGE_LIMIT_PER_NODE;
		}
		BridgeEdge edgeFromJoints = BridgeEdges.GetEdgeFromJoints(selectedJoint, clickedJoint);
		if (!preview && (bool)edgeFromJoints && edgeFromJoints.IsLocked())
		{
			DeSelectJoint(selectedJoint);
			SelectJoint(clickedJoint);
			BridgeAudio.PlayCreateEdge(edgeFromJoints.m_Material.m_MaterialType);
			return PlacementReturnValue.SUCCESS;
		}
		if (!BuildZones.ContainsJoint(clickedJoint.transform.position))
		{
			return PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE;
		}
		if (!BuildZones.ContainsEdge(clickedJoint.transform.position, selectedJoint.transform.position))
		{
			return PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE;
		}
		if ((bool)edgeFromJoints)
		{
			Budget.AdjustBudgetForRemovedEdge(edgeFromJoints);
		}
		if (BridgeEdges.CanFormEdgeBetweenJoints(edgeFromJoints, selectedJoint, clickedJoint, material))
		{
			if (!preview)
			{
				BridgeActions.StartRecording();
				if ((bool)edgeFromJoints)
				{
					edgeFromJoints.ForceDisable();
					BridgeSelectionSet.DeSelectEdge(edgeFromJoints);
					BridgeActions.Delete(edgeFromJoints);
				}
				BridgeEdge bridgeEdge2 = BridgeEdges.CreateEdgeWithPistonOrSpring(selectedJoint, clickedJoint, Bridge.m_BuildMaterialType);
				if ((bool)bridgeEdge2)
				{
					if ((bool)edgeFromJoints)
					{
						bridgeEdge2.m_JointAPart = ((edgeFromJoints.m_JointA == selectedJoint) ? edgeFromJoints.m_JointAPart : edgeFromJoints.m_JointBPart);
						bridgeEdge2.m_JointBPart = ((edgeFromJoints.m_JointB == clickedJoint) ? edgeFromJoints.m_JointBPart : edgeFromJoints.m_JointAPart);
						bridgeEdge2.RefreshJointSelectorNumbers();
					}
					MaybeMakeEdgeSliderVisible(bridgeEdge2);
					BridgeAudio.PlayCreateEdge(bridgeEdge2.m_Material.m_MaterialType);
					BridgeActions.Create(bridgeEdge2);
					Triangulate(clickedJoint, bridgeEdge2.m_Material.m_MaterialType);
					BridgeActions.FlushRecording();
				}
				DeSelectJoint(selectedJoint);
				if (!(bridgeEdge2 != null) || !bridgeEdge2.IsPiston() || !Game.IsCurrentLevelTutorial())
				{
					SelectJoint(clickedJoint);
				}
			}
			return PlacementReturnValue.SUCCESS;
		}
		return PlacementReturnValue.FAIL;
	}

	private static void SplitEdgeWithNode(BridgeEdge sourceEdge, BridgeJoint joint, BridgeJoint origin)
	{
		BridgeEdge bridgeEdge = null;
		BridgeEdge bridgeEdge2 = null;
		if (origin != sourceEdge.m_JointA)
		{
			bridgeEdge = BridgeEdges.CreateEdge(sourceEdge.m_JointA, joint, sourceEdge.m_Material.m_MaterialType, Utils.GenerateUniqueId(), null);
		}
		if (origin != sourceEdge.m_JointB)
		{
			bridgeEdge2 = BridgeEdges.CreateEdge(joint, sourceEdge.m_JointB, sourceEdge.m_Material.m_MaterialType, Utils.GenerateUniqueId(), null);
		}
		sourceEdge.ForceDisable();
		sourceEdge.SetStressColor(0f);
		if (bridgeEdge != null)
		{
			AddSpringOrPistonToEdge(sourceEdge, bridgeEdge);
			BridgeActions.Create(bridgeEdge);
		}
		if (bridgeEdge2 != null)
		{
			AddSpringOrPistonToEdge(sourceEdge, bridgeEdge2);
			BridgeActions.Create(bridgeEdge2);
		}
		BridgeActions.Delete(sourceEdge);
	}

	private static void AddSpringOrPistonToEdge(BridgeEdge sourceEdge, BridgeEdge targetEdge)
	{
		if (sourceEdge == null || targetEdge == null)
		{
			return;
		}
		if (targetEdge.IsSpring())
		{
			BridgeSprings.CreateSpring(targetEdge, sourceEdge.m_SpringCoilVisualization.m_Slider.GetNormalizedValue(), Utils.GenerateUniqueId());
		}
		if (!targetEdge.IsPiston())
		{
			return;
		}
		Piston pistonOnEdge = Pistons.GetPistonOnEdge(sourceEdge);
		Piston piston = Pistons.CreatePiston(targetEdge.m_JointA, targetEdge.m_JointB, pistonOnEdge.m_Slider.GetNormalizedValue(), Utils.GenerateUniqueId());
		if (!piston)
		{
			return;
		}
		foreach (HydraulicsControllerPhase controllerPhase in HydraulicsController.m_ControllerPhases)
		{
			if (controllerPhase.m_Pistons.Contains(pistonOnEdge))
			{
				controllerPhase.m_Pistons.Add(piston);
			}
		}
	}

	private static void MaybeMakeEdgeSliderVisible(BridgeEdge edge)
	{
		if (edge.IsPiston())
		{
			Pistons.GetPistonOnEdge(edge).m_Slider.SetVisibilityExpireTime();
		}
		if (edge.IsSpring())
		{
			edge.m_SpringCoilVisualization.m_Slider.SetVisibilityExpireTime();
		}
	}

	private static void CheckForSwitchToMovement()
	{
		if (!m_SelectedJoint)
		{
			return;
		}
		if (GameInput.IsDown(BindingType.DRAW_BUILD) && (!m_SelectedJoint.m_IsAnchor || BridgePillars.IsBridgePillarAnchor(m_SelectedJoint.m_Guid)) && m_HoverJoint == m_SelectedJoint)
		{
			m_SelectionPressedSeconds += Time.unscaledDeltaTime;
			if (m_SelectionPressedSeconds > 1f)
			{
				if (!BridgeEdges.LockedEdgesAreConnectedToJoint(m_SelectedJoint) || (Game.InSandboxGodMode() && !CampaignTutorial.BlockMoveAction() && !BridgeTrace.IsFilling()))
				{
					StartJointMovement(m_SelectedJoint);
					BridgeJointMovement.m_CancelMoveModeOnRelease = true;
					BridgeSelectionSet.CancelSelection();
				}
				else
				{
					BridgeEdges.DisplayLockIconForLockedEdgesConnectedToJoint(m_SelectedJoint, BridgeJointMovement.DISPLAY_LOCKED_ICON_SECONDS);
				}
			}
		}
		else
		{
			m_SelectionPressedSeconds = 0f;
		}
	}

	public static void SelectJoint(BridgeJoint joint)
	{
		joint.Select();
		m_SelectedJoint = joint;
		m_SelectionPressedSeconds = 0f;
	}

	public static void DeSelectJoint(BridgeJoint joint)
	{
		joint.DeSelect();
		m_SelectedJoint = null;
	}

	public static void CancelSelection()
	{
		if ((bool)m_SelectedJoint)
		{
			DeSelectJoint(m_SelectedJoint);
		}
		DisablePlacementUI();
	}

	private static bool CanCreateJointAtPos(Vector3 pos)
	{
		if (AllowPlacement(m_SelectedJoint, pos) != PlacementReturnValue.SUCCESS || !BuildZones.ContainsJoint(pos))
		{
			return false;
		}
		return ProcessClickInternal(GameInput.GetMousePosition(), preview: true) == PlacementReturnValue.SUCCESS;
	}

	private static VectorLine CreatePlacementLine()
	{
		VectorLine vectorLine = new VectorLine("PlacementLine", new List<Vector3>(), GameUI.m_Instance.m_PlacementLineTexture, 12f);
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.layer = Utils.DEFAULT_LAYER;
		vectorLine.textureScale = 1f;
		vectorLine.color = GameUI.PlacementLineColor();
		vectorLine.AddNormals();
		return vectorLine;
	}

	private static void CreatePlacementDot()
	{
		m_PlacementDot = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_PlacementDot);
		m_PlacementDot.hideFlags = HideFlags.HideInHierarchy;
		m_PlacementDotSpriteRenderer = m_PlacementDot.GetComponentInChildren<SpriteRenderer>();
		m_PlacementDot.SetActive(value: false);
		UnityEngine.Object.DontDestroyOnLoad(m_PlacementDot);
	}

	private static void CreateSnapDot()
	{
		m_SnapTraceDot = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SnapDot);
		m_SnapTraceDot.hideFlags = HideFlags.HideInHierarchy;
		m_SnapTraceDot.SetActive(value: false);
		UnityEngine.Object.DontDestroyOnLoad(m_SnapTraceDot);
	}

	private static VectorLine CreatePlacementCrosshairs()
	{
		VectorLine vectorLine = new VectorLine("PlacementCrosshairs", new List<Vector3>(), GameUI.m_Instance.m_PlacementCrosshairsLineWidth);
		vectorLine.rectTransform.gameObject.hideFlags = HideFlags.HideInHierarchy;
		vectorLine.Draw3DAuto();
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.points3.Add(Vector3.zero);
		vectorLine.layer = Utils.RENDER_LAST_LAYER;
		vectorLine.color = GameUI.m_Instance.m_PlacementCrosshairsColor;
		return vectorLine;
	}

	private static void CreateSelectionCircleDots()
	{
		m_SelectionCircleDotsParent = new GameObject("SelectionCircleDotsParent");
		m_SelectionCircleDotsParent.hideFlags = HideFlags.HideInHierarchy;
		UnityEngine.Object.DontDestroyOnLoad(m_SelectionCircleDotsParent);
		m_SelectionCircleDotsParent.SetActive(value: false);
		for (int i = 0; i < 132; i++)
		{
			m_SelectionCircleDots.Add(UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_SelectionCircleDot));
			m_SelectionCircleDots[i].transform.parent = m_SelectionCircleDotsParent.transform;
		}
	}

	private static void StartJointMovement(BridgeJoint selectedJoint)
	{
		CancelSelection();
		BridgeTrace.TurnOffTracing();
		if (BridgePillars.IsBridgePillarAnchor(selectedJoint.m_Guid))
		{
			BridgeSelectionSet.SelectBridgePillar(BridgePillars.GetBridgePillarWithAnchor(selectedJoint.m_Guid));
			BridgePillarMovement.StartMovement(GameInput.GetMousePosition());
		}
		else
		{
			BridgeJointMovement.SelectJoint(selectedJoint);
		}
		GameToolMode.SetMode(GameToolModeType.MOVE);
	}

	public static Vector3 GetPlacementPos()
	{
		if (m_SnapTraceDot.gameObject.activeInHierarchy)
		{
			return new Vector3(m_SnapTraceDot.transform.position.x, m_SnapTraceDot.transform.position.y, 0f);
		}
		return GetPlacementDotPos();
	}

	public static void ModForcePlacementPos(Vector3 placementPos)
	{
		placementPos.z = 0f;
		if (m_SnapTraceDot != null)
		{
			m_SnapTraceDot.transform.position = placementPos;
		}
		if (m_PlacementDot != null)
		{
			m_PlacementDot.transform.position = placementPos;
		}
	}

	public static Vector3 GetPlacementDotPosAfterClick()
	{
		BridgeJoint jointUnderRay = GetJointUnderRay(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()));
		if (!jointUnderRay)
		{
			return GetPlacementDotPos();
		}
		return jointUnderRay.transform.position;
	}

	public static Vector3 GetPlacementDotPos()
	{
		return new Vector3(m_PlacementDot.transform.position.x, m_PlacementDot.transform.position.y, 0f);
	}

	public static bool RoadMaterialWillOverdrawRoadMaterial(BridgeJoint A, BridgeJoint B, BridgeMaterialType materialType)
	{
		if (!A || !B)
		{
			return false;
		}
		if (!BridgeMaterials.IsRoadMaterial(materialType))
		{
			return false;
		}
		BridgeEdge edgeFromJoints = BridgeEdges.GetEdgeFromJoints(A, B);
		if ((bool)edgeFromJoints && BridgeMaterials.IsRoadMaterial(edgeFromJoints.m_Material.m_MaterialType))
		{
			return true;
		}
		return false;
	}

	public static bool WoodMaterialWillOverdrawRoadMaterial(BridgeJoint A, BridgeJoint B, BridgeMaterialType materialType)
	{
		if (!A || !B)
		{
			return false;
		}
		if (materialType != BridgeMaterialType.WOOD)
		{
			return false;
		}
		BridgeEdge edgeFromJoints = BridgeEdges.GetEdgeFromJoints(A, B);
		if ((bool)edgeFromJoints && BridgeMaterials.IsRoadMaterial(edgeFromJoints.m_Material.m_MaterialType))
		{
			return true;
		}
		return false;
	}

	public static PlacementReturnValue AllowPlacement(BridgeJoint startJoint, Vector3 placementPos)
	{
		if (m_IgnoreEdgePlacementRestrictions)
		{
			return PlacementReturnValue.SUCCESS;
		}
		if (!startJoint)
		{
			return PlacementReturnValue.FAIL;
		}
		if (BridgePillarPlacement.InPlacementMode() || Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR)
		{
			return PlacementReturnValue.FAIL;
		}
		if (!WorldBounds.Contains(placementPos))
		{
			WorldBounds.ShowBriefly();
			return PlacementReturnValue.FAIL_OUTSIDE_WORLD_BOUNDS;
		}
		if (!BridgeJoints.AnchorOverlapsPosition(placementPos, null, GameSettings.NodeDiameter()) && BridgeJoints.NodeLocationOverlapsBlockingPolygonShape(placementPos))
		{
			return PlacementReturnValue.FAIL_NODE_ILLEGAL_POSITION;
		}
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType);
		if (bridgeMaterial != null && BridgeEdges.EdgeLocationOverlapsBlockingPolygonShape(placementPos, startJoint.transform.position, Bridge.m_BuildMaterialType, bridgeMaterial.m_EdgeMaterial.collisionRadius))
		{
			return PlacementReturnValue.FAIL_EDGE_OVERLAPS_BLOCKING_SHAPE;
		}
		if (!BuildZones.ContainsJoint(startJoint.transform.position))
		{
			return PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE;
		}
		if (!BuildZones.ContainsEdge(placementPos, startJoint.transform.position))
		{
			return PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE;
		}
		if (!Game.InSandboxGodMode())
		{
			if (startJoint.m_NoBuild)
			{
				return PlacementReturnValue.FAIL_NO_BUILD_ANCHOR;
			}
			if (m_HoverJoint != null && m_HoverJoint.m_NoBuild)
			{
				return PlacementReturnValue.FAIL_NO_BUILD_ANCHOR;
			}
		}
		if (!Budget.CanAffordEdge(Vector3.Distance(startJoint.transform.position, placementPos), Bridge.m_BuildMaterialType))
		{
			return PlacementReturnValue.FAIL_CANNOT_AFFORD_COST;
		}
		if (!Budget.HasMaterialLeft(Bridge.m_BuildMaterialType))
		{
			bool num = BridgeEdges.IsValidEdgeLength(Vector2.Distance(startJoint.transform.position, GetPlacementPos()), GameSettings.NodeDiameter(), BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType));
			BridgeJoint bridgeJoint = BridgeJoints.FindClosestJoint(GetPlacementPos());
			if (num && (bool)bridgeJoint && Vector2.Distance(GetPlacementPos(), bridgeJoint.transform.position) < GameSettings.NodeRadius())
			{
				if (!RoadMaterialWillOverdrawRoadMaterial(startJoint, bridgeJoint, Bridge.m_BuildMaterialType))
				{
					return PlacementReturnValue.FAIL_NO_MATERIAL_LEFT;
				}
				return PlacementReturnValue.SUCCESS;
			}
			return PlacementReturnValue.FAIL_NO_MATERIAL_LEFT;
		}
		if (startJoint.HasMaxEdges())
		{
			return PlacementReturnValue.FAIL_EXCEEDS_MAX_EDGE_LIMIT_PER_NODE;
		}
		return PlacementReturnValue.SUCCESS;
	}

	public static void Triangulate(BridgeJoint source, BridgeMaterialType materialType)
	{
		if (source == null || !MaterialCanAutoTriangulate(materialType))
		{
			return;
		}
		Budget.UpdateManual();
		foreach (BridgeJoint triangulateJoint in m_TriangulateJoints)
		{
			if (!BridgeEdges.GetEdgeFromJoints(source, triangulateJoint) && BridgeEdges.CanFormEdgeBetweenJoints(null, source, triangulateJoint, materialType) && BridgeJoints.JointsCanAddEdgeWithoutExceedingEdgeLimit(source, triangulateJoint))
			{
				BridgeEdge bridgeEdge = BridgeEdges.CreateEdgeWithPistonOrSpring(source, triangulateJoint, materialType);
				if ((bool)bridgeEdge)
				{
					Budget.AdjustBudgetForAddedEdge(bridgeEdge);
					BridgeActions.Create(bridgeEdge);
				}
			}
		}
	}

	public static bool IsDrawing()
	{
		return m_PlacementDot.gameObject.activeInHierarchy;
	}

	public static bool SelectionCircleActive()
	{
		return m_SelectionCircleDotsParent.activeInHierarchy;
	}

	public static BridgeJoint SnapCursorToClosestNode()
	{
		if (m_SnapToJoint == null)
		{
			return null;
		}
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
		BridgeJoint bridgeJoint = BridgeJoints.FindClosestJointEx(worldPointFromScreenPos, IsDrawing() ? m_SelectedJoint : null);
		if ((bool)bridgeJoint && Vector2.Distance(bridgeJoint.transform.position, worldPointFromScreenPos) < SNAP_MAX_DISTANCE)
		{
			GameInput.SetVirtualMousePosition(Cameras.MainCamera().WorldToScreenPoint(bridgeJoint.transform.position));
			GamepadManager.m_VirtualMouseUI.SyncVirtualMouseToInput();
			InterfaceAudio.Play("ui_menuButton_hover");
			return bridgeJoint;
		}
		return null;
	}

	public static ClipboardJoint SnapCursorToClosestShadowNode()
	{
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
		ClipboardJoint clipboardJoint = BridgeShadow.FindClosestJoint(worldPointFromScreenPos);
		if ((bool)clipboardJoint && Vector2.Distance(clipboardJoint.transform.position, worldPointFromScreenPos) < SNAP_MAX_DISTANCE)
		{
			GameInput.SetVirtualMousePosition(Cameras.MainCamera().WorldToScreenPoint(clipboardJoint.transform.position));
			GamepadManager.m_VirtualMouseUI.SyncVirtualMouseToInput();
			InterfaceAudio.Play("ui_menuButton_hover");
			return clipboardJoint;
		}
		return null;
	}

	public static void UpdateHoverJoint()
	{
		BridgeJoint hoverJoint = m_HoverJoint;
		if ((bool)m_HoverJoint)
		{
			m_HoverJoint.EndHover();
			m_HoverJoint = null;
		}
		if ((BridgeTrace.m_ArcTracer.HandlesVisible() && BridgeTrace.m_ArcTracer.PointerOverArcHandle(GameInput.GetMousePosition()) != null) || ClipboardManager.ReadyToPaste() || GameToolMode.GetMode() == GameToolModeType.ERASE || GameToolMode.GetMode() == GameToolModeType.SELECT || (Pistons.MouseIsOverPistonSlider() && GameToolMode.GetMode() != GameToolModeType.MOVE && !m_SelectedJoint) || (BridgeSprings.MouseIsOverSpringSlider() && GameToolMode.GetMode() != GameToolModeType.MOVE && !m_SelectedJoint) || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy || GameUI.IsPointerOverGameObject() || (bool)Pistons.m_SliderFollowingMouse || (bool)BridgeSprings.m_SliderFollowingMouse || BridgePillarPlacement.InPlacementMode() || Bridge.m_BuildMaterialType == BridgeMaterialType.PILLAR)
		{
			return;
		}
		if (Physics.Raycast(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), out var hitInfo, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			BridgeJoint component = hitInfo.transform.parent.GetComponent<BridgeJoint>();
			if (!component)
			{
				return;
			}
			m_HoverJointLingeredSeconds += Time.unscaledDeltaTime;
			if (GameInput.GetActiveGameDevice() == GameDevice.Gamepad && !GamepadManager.CursorMovingSlowly())
			{
				m_HoverJointLingeredSeconds = 0f;
			}
			if (m_HoverJointLingeredSeconds > 0.1f)
			{
				component.StartHover();
				m_HoverJoint = component;
				if (hoverJoint != m_HoverJoint)
				{
					MaybePlayHoverAudio();
				}
			}
		}
		else
		{
			m_HoverJointLingeredSeconds = 0f;
		}
	}

	private static bool MaterialCanAutoTriangulate(BridgeMaterialType materialType)
	{
		if (materialType != BridgeMaterialType.WOOD && materialType != BridgeMaterialType.STEEL)
		{
			return materialType == BridgeMaterialType.SPRING;
		}
		return true;
	}

	private static void UpdatePlacementLine(Vector3 lineStartPos, Vector3 lineEndPos)
	{
		m_PlacementLine.active = true;
		m_PlacementLine.points3[0] = lineStartPos;
		m_PlacementLine.points3[1] = lineEndPos;
		m_PlacementLine.SetWidth(2f * GameUI.m_Instance.m_PlacementLineWidth / Cameras.MainCamera().orthographicSize);
		m_PlacementLine.textureOffset = (0f - GameUI.m_Instance.m_PlacementLineAnimSpeed) * Time.unscaledTime % 1f;
		m_PlacementLine.color = ((AllowPlacement(m_SelectedJoint, GetPlacementDotPos()) == PlacementReturnValue.SUCCESS && BuildZones.ContainsEdge(m_SelectedJoint.transform.position, GetPlacementDotPos())) ? GameUI.PlacementLineColor() : GameUI.PlacementLineErrorColor());
	}

	private static void UpdatePlacementDot(Vector3 lineStartPos, Vector3 lineEndPos)
	{
		m_PlacementDot.SetActive(value: true);
		m_PlacementDot.transform.position = new Vector3(lineEndPos.x, lineEndPos.y, PLACEMENT_DOT_Z);
		m_PlacementDotSpriteRenderer.color = ((AllowPlacement(m_SelectedJoint, GetPlacementDotPos()) == PlacementReturnValue.SUCCESS && BuildZones.ContainsEdge(m_SelectedJoint.transform.position, GetPlacementDotPos())) ? GameUI.PlacementLineColor() : GameUI.PlacementLineErrorColor());
	}

	private static void UpdateTriangulateLines()
	{
		DisableTriangulateLines();
		m_TriangulateJoints.Clear();
		if (!Profiles.m_ActiveProfile.m_AutoTriangulateEnabled || m_SelectedJoint == null || m_PlacementLine == null || ((bool)m_HoverJoint && PlacementDotIsOnJoint(m_HoverJoint, GameSettings.NodeRadius())) || !m_PlacementLine.active || !MaterialCanAutoTriangulate(Bridge.m_BuildMaterialType) || EdgeBisectDotActive() || AllowPlacement(m_SelectedJoint, GetPlacementDotPos()) != PlacementReturnValue.SUCCESS || !BuildZones.ContainsJoint(GetPlacementDotPos()) || PlacementDotIsOnAnyJoint(GameSettings.NodeDiameter()))
		{
			return;
		}
		float maxEdgeLength = BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType);
		CollectJointsForTriangulation(GetPlacementDotPos(), maxEdgeLength);
		if (m_TriangulateJoints.Count != 0)
		{
			int num = m_TriangulateJoints.Count - m_TriangulateLines.Count;
			for (int i = 0; i < num; i++)
			{
				VectorLine item = CreatePlacementLine();
				m_TriangulateLines.Add(item);
			}
			for (int j = 0; j < m_TriangulateJoints.Count; j++)
			{
				BridgeJoint bridgeJoint = m_TriangulateJoints[j];
				UpdateTriangulateLine(m_TriangulateLines[j], GetPlacementDotPos(), bridgeJoint.transform.position);
				m_TriangulateLines[j].active = true;
			}
		}
	}

	private static bool PlacementDotIsOnJoint(BridgeJoint joint, float threshold)
	{
		return Vector2.Distance(GetPlacementDotPos(), joint.transform.position) < threshold;
	}

	private static bool PlacementDotIsOnAnyJoint(float threshold)
	{
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && PlacementDotIsOnJoint(joint, threshold))
			{
				return true;
			}
		}
		return false;
	}

	private static void UpdateTriangulateLine(VectorLine line, Vector3 lineStartPos, Vector3 lineEndPos)
	{
		line.active = true;
		line.points3[0] = new Vector3(lineStartPos.x, lineStartPos.y, PLACEMENT_LINE_Z);
		line.points3[1] = new Vector3(lineEndPos.x, lineEndPos.y, PLACEMENT_LINE_Z);
		line.SetWidth(GameUI.m_Instance.m_PlacementLineWidth / Cameras.MainCamera().orthographicSize);
		line.textureOffset = (0f - GameUI.m_Instance.m_PlacementLineAnimSpeed) * Time.unscaledTime % 1f;
		line.color = GameUI.PlacementLineColor();
	}

	private static void CollectJointsForTriangulation(Vector3 pos, float range)
	{
		m_TriangulateJoints.Clear();
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy && !(joint == m_SelectedJoint) && BridgeEdges.IsValidEdgeLength(Vector2.Distance(pos, joint.transform.position), GameSettings.NodeDiameter(), range) && (!joint.m_NoBuild || Game.InSandboxGodMode()) && !BridgeEdges.LineSegmentCrossesSolidEdge(pos, joint.transform.position, m_HoverJoint, joint) && BuildZones.ContainsEdge(pos, joint.transform.position))
			{
				m_TriangulateJoints.Add(joint);
			}
		}
	}

	private static void DisableTriangulateLines()
	{
		foreach (VectorLine triangulateLine in m_TriangulateLines)
		{
			triangulateLine.active = false;
		}
	}

	private static void MaybeAutoDraw()
	{
		if (Profiles.m_ActiveProfile.m_AutoDrawEnabled && m_PlacementDot.gameObject.activeInHierarchy && (bool)m_SelectedJoint && (Vector2.Distance(Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()), m_SelectedJoint.transform.position) >= BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType) || (m_HoverJoint != null && m_HoverJoint != m_SelectedJoint)))
		{
			ProcessClickInternal(GameInput.GetMousePosition(), preview: false);
		}
	}

	private static void UpdateSnapTraceDot(Vector3 lineStartPos, Vector3 lineEndPos)
	{
		m_SnapTraceDot.SetActive(value: false);
		m_SnapTraceDotEdge = null;
		if (!BridgeTrace.IsTraceLinePlaced() && BridgeEdges.GetNumActiveEdges() == 0)
		{
			return;
		}
		float maxLength = BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType).m_MaxLength;
		Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		if (BridgeTrace.IsTraceLinePlaced())
		{
			vector = BridgeTrace.m_ArcTracer.ClosestPointOnLineTo(lineEndPos, lineStartPos, maxLength);
		}
		if (BridgeEdges.GetNumActiveEdges() > 0 && Profiles.m_ActiveProfile.m_EdgeBisectEnabled && !BridgeJoints.JointOverlapsPosition(lineEndPos, GameSettings.NodeDiameter()))
		{
			BridgeEdge closestEdgeToPos = BridgeEdges.GetClosestEdgeToPos(lineEndPos);
			if (closestEdgeToPos != null && !closestEdgeToPos.IsLocked() && !closestEdgeToPos.HasJoint(m_SelectedJoint) && HasMaterialForSplit(closestEdgeToPos, Bridge.m_BuildMaterialType))
			{
				Vector3 vector2 = Utils.NearestPointOnLineSegment(closestEdgeToPos.m_JointA.transform.position, closestEdgeToPos.m_JointB.transform.position, lineEndPos);
				float num = Vector2.Distance(vector2, lineEndPos);
				float num2 = Vector2.Distance(vector, lineEndPos);
				if (num < num2)
				{
					vector = vector2;
					m_SnapTraceDotEdge = closestEdgeToPos;
				}
			}
		}
		BridgeJoint bridgeJoint = BridgeJoints.FindClosestJoint(vector);
		if ((bool)bridgeJoint && Vector2.Distance(vector, bridgeJoint.transform.position) < GameSettings.NodeRadius() - 0.001f)
		{
			vector = bridgeJoint.transform.position;
		}
		if (!(Vector2.Distance(lineEndPos, vector) > (m_SnapTraceDotEdge ? GameSettings.NodeRadius() : GameSettings.NodeDiameter())) && BridgeEdges.IsValidEdgeLength(Vector2.Distance(lineStartPos, vector), GameSettings.NodeDiameter(), maxLength))
		{
			m_SnapTraceDot.transform.position = new Vector3(vector.x, vector.y, m_PlacementDot.transform.position.z);
			m_SnapTraceDot.SetActive(value: true);
			if (!CanCreateJointAtPos(new Vector3(vector.x, vector.y, m_PlacementDot.transform.position.z)))
			{
				m_SnapTraceDot.SetActive(value: false);
			}
		}
	}

	private static bool HasMaterialForSplit(BridgeEdge splitEdge, BridgeMaterialType newEdgeMaterial)
	{
		int materialLeft = Budget.GetMaterialLeft(splitEdge.m_Material.m_MaterialType);
		materialLeft--;
		if (newEdgeMaterial == splitEdge.m_Material.m_MaterialType)
		{
			materialLeft--;
		}
		return materialLeft >= 0;
	}

	private static bool DesiredEdgeLocationInvalid()
	{
		if (!BridgeMaterials.IsRoadMaterial(Bridge.m_BuildMaterialType))
		{
			return false;
		}
		float b = Vector2.Distance(GetPlacementPos(), m_SelectedJoint.transform.position);
		float maxDistance = Mathf.Min(BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType).m_MaxLength, b);
		Vector3 normalized = (GetPlacementPos() - m_SelectedJoint.transform.position).normalized;
		_ = m_SelectedJoint.transform.position;
		int layerMask = Utils.VEHICLE_LAYER_MASK | Utils.PICKUP_BY_VEHICLE_LAYER_MASK;
		if (Physics.SphereCast(m_SelectedJoint.transform.position, GameSettings.NodeRadius(), normalized, out var _, maxDistance, layerMask))
		{
			return true;
		}
		return false;
	}

	private static void DisablePlacementUI()
	{
		if (m_PlacementLine != null)
		{
			m_PlacementLine.active = false;
		}
		if (m_PlacementDot != null)
		{
			m_PlacementDot.SetActive(value: false);
		}
		if (m_SnapTraceDot != null)
		{
			m_SnapTraceDot.SetActive(value: false);
		}
		if (m_PlacementCrosshairs != null)
		{
			m_PlacementCrosshairs.active = false;
		}
		if (m_SelectionCircleDotsParent != null)
		{
			m_SelectionCircleDotsParent.SetActive(value: false);
		}
		DisableTriangulateLines();
		GameUI.m_Instance.m_BuildToolTip.Disable();
		GameUI.m_Instance.m_PointerToolTip.Disable();
	}

	private static Vector3 CalculateConstrainedEndPoint(Vector3 start)
	{
		Vector3 vector = ((m_HoverJoint != null) ? m_HoverJoint.transform.position : Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()));
		vector.z = start.z;
		float magnitude = (vector - start).magnitude;
		float maxEdgeLength = BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType);
		float num = Mathf.Min(magnitude, maxEdgeLength);
		Vector3 normalized = (vector - start).normalized;
		Vector3 vector2 = GameGrid.SnapPosToGrid(start + normalized * num);
		while ((vector2 - start).magnitude > maxEdgeLength + 0.0001f)
		{
			num -= 0.005f;
			normalized = (vector - start).normalized;
			vector2 = GameGrid.SnapPosToGrid(start + normalized * num);
		}
		return vector2;
	}

	private static Vector3 CalculateConstrainedEndPointNoGridSnap(Vector3 start)
	{
		Vector3 vector = ((m_HoverJoint != null) ? m_HoverJoint.transform.position : Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()));
		vector.z = start.z;
		float magnitude = (vector - start).magnitude;
		float maxEdgeLength = BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType);
		float num = Mathf.Min(magnitude, maxEdgeLength);
		Vector3 normalized = (vector - start).normalized;
		return start + normalized * num;
	}

	private static void UpdateSnapToJoint()
	{
		if ((bool)m_SnapToJoint)
		{
			m_SnapToJoint.EndSnapTo();
			m_SnapToJoint = null;
		}
		if (AllowedToShowSnapToJoint())
		{
			Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
			BridgeJoint bridgeJoint = BridgeJoints.FindClosestJointEx(worldPointFromScreenPos, IsDrawing() ? m_SelectedJoint : null);
			if ((bool)bridgeJoint && Vector2.Distance(bridgeJoint.transform.position, worldPointFromScreenPos) < SNAP_MAX_DISTANCE)
			{
				m_SnapToJoint = bridgeJoint;
				m_SnapToJoint.StartSnapTo();
				m_SnapToJoint.PointSnapToArrowAt(worldPointFromScreenPos);
			}
		}
	}

	private static bool AllowedToShowSnapToJoint()
	{
		if (!GameStateBuild.SnapCursorEnabled())
		{
			return false;
		}
		if (ClipboardManager.ReadyToPaste())
		{
			return false;
		}
		if (GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (HydraulicsPhases.m_Phases.Count > 0 && BridgeJointSelectors.JointSelectorIsUnderMouse())
		{
			return false;
		}
		if (Pistons.MouseIsOverPistonSlider() || (bool)Pistons.m_SliderFollowingMouse)
		{
			return false;
		}
		if (SandboxSettings.m_SpringAdjustmentsAllowed && (BridgeSprings.MouseIsOverSpringSlider() || (bool)BridgeSprings.m_SliderFollowingMouse))
		{
			return false;
		}
		if (!GameStateBuild.CanProcessBuildAction())
		{
			return false;
		}
		if (GroupSelect.IsActive())
		{
			return false;
		}
		if (GameToolMode.GetMode() == GameToolModeType.ERASE)
		{
			return false;
		}
		return true;
	}

	private static void MaybePlayHoverAudio()
	{
		if (BridgeTrace.IsTracingActive() && BridgeTrace.TracingFollowsMouse() && BridgeTrace.IsLongEnoughToPlace())
		{
			InterfaceAudio.Play("ui_build_hover");
			return;
		}
		Vector2 vector = GetPlacementDotPos();
		if ((bool)m_SelectedJoint && AllowPlacement(m_SelectedJoint, vector) == PlacementReturnValue.SUCCESS && m_SelectedJoint != m_HoverJoint && BridgeEdges.CanFormEdgeBetweenJoints(null, m_SelectedJoint, m_HoverJoint, Bridge.m_BuildMaterialType))
		{
			InterfaceAudio.Play("ui_build_hover");
		}
	}

	private static void UpdateSelectionCircleDots()
	{
		if (BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType) == null)
		{
			return;
		}
		if (!m_SelectedJoint || BridgeMaterials.GetBridgeMaterial(Bridge.m_BuildMaterialType).HasUnlimitedLength() || BridgeTrace.IsTracingActive())
		{
			m_SelectionCircleDotsParent.SetActive(value: false);
			return;
		}
		m_SelectionCircleDotsParent.transform.position = new Vector3(m_SelectedJoint.transform.position.x, m_SelectedJoint.transform.position.y, PLACEMENT_DOT_Z);
		float maxEdgeLength = BridgeMaterials.GetMaxEdgeLength(Bridge.m_BuildMaterialType);
		if (!m_SelectionCircleDotsParent.activeInHierarchy || !Mathf.Approximately(maxEdgeLength, m_SelectionCircleDotsRadius))
		{
			PositionSelectionCircleDots(maxEdgeLength);
			m_SelectionCircleDotsRadius = maxEdgeLength;
		}
		m_SelectionCircleDotsParent.SetActive(value: true);
		m_SelectionCircleDotsParent.transform.Rotate(0f, 0f, Time.unscaledDeltaTime * (0f - GameUI.m_Instance.m_SelectionCircleDotsRotateDegreesPerSecond), Space.Self);
	}

	private static void PositionSelectionCircleDots(float radius)
	{
		float num = 0.3f;
		float num2 = MathF.PI * 2f * radius;
		float num3 = 360f * (num / num2);
		int num4 = Mathf.RoundToInt(360f / num3);
		if (num4 > 132)
		{
			num4 = 132;
		}
		float num5 = 0f;
		for (int i = 0; i < num4; i++)
		{
			m_SelectionCircleDots[i].SetActive(value: true);
			m_SelectionCircleDots[i].transform.localPosition = new Vector3(radius * Mathf.Sin(MathF.PI / 180f * num5), radius * Mathf.Cos(MathF.PI / 180f * num5), 0f);
			num5 += num3;
		}
		for (int j = num4; j < 132; j++)
		{
			m_SelectionCircleDots[j].SetActive(value: false);
		}
	}

	private static BridgeJoint GetJointUnderRay(Ray ray)
	{
		BridgeJoint result = null;
		if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, Utils.JOINT_HOTSPOT_LAYER_MASK))
		{
			result = hitInfo.transform.parent.GetComponent<BridgeJoint>();
		}
		return result;
	}
}
