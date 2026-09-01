using System.Collections.Generic;
using UnityEngine;

public class ClipboardManager
{
	public static List<ClipboardJoint> m_Joints = new List<ClipboardJoint>();

	public static List<ClipboardEdge> m_Edges = new List<ClipboardEdge>();

	public static List<ClipboardBridgePillar> m_BridgePillars = new List<ClipboardBridgePillar>();

	private static GameObject m_ClipboardContainer;

	private static GameObject m_CanRotateOrFlipContainer;

	private static float m_TimeRotateHeldDown;

	private static float m_NextTickTime;

	private static float ROTATION_DEGREES_PER_SECOND = 45f;

	private static float ROTATE_REPEAT_DELAY_SECONDS = 0.3f;

	private static float ROTATE_REPEAT_SECONDS = 0.1f;

	private static float ROTATE_REPEAT_SECONDS_90 = 0.5f;

	private static bool m_IgnoreNextPaste;

	private static float m_AccumulatedRotation;

	private static Dictionary<string, ClipboardJoint> m_JointMap = new Dictionary<string, ClipboardJoint>();

	public static void Init()
	{
		m_ClipboardContainer = new GameObject("ClipboardContainer");
		m_CanRotateOrFlipContainer = new GameObject("CanRotateContainer");
		m_CanRotateOrFlipContainer.transform.SetParent(m_ClipboardContainer.transform);
		Object.DontDestroyOnLoad(m_ClipboardContainer);
	}

	public static bool IsEmpty()
	{
		if (m_Joints.Count == 0 && m_Edges.Count == 0)
		{
			return m_BridgePillars.Count == 0;
		}
		return false;
	}

	public static Quaternion GetClipboardRotation()
	{
		return m_CanRotateOrFlipContainer.transform.rotation;
	}

	public static void SetClipboardRotation(Quaternion rot)
	{
		m_CanRotateOrFlipContainer.transform.rotation = rot;
	}

	public static Vector3 GetClipboardContainerPos()
	{
		return m_ClipboardContainer.transform.position;
	}

	public static int GetBridgePillarCount()
	{
		return m_BridgePillars.Count;
	}

	public static bool ContainsBridgePillarSource(BridgePillar bridgePillar)
	{
		foreach (ClipboardBridgePillar bridgePillar2 in m_BridgePillars)
		{
			if (bridgePillar2.m_SourceBridgePillar.m_Guid == bridgePillar.m_Guid)
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdateManual()
	{
		if ((GameInput.JustPressed(BindingType.SELECT_INTERRUPT) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST)) && !CampaignTutorial.IsRunning())
		{
			ClearClipboard();
		}
		else if (ReadyToPaste())
		{
			UpdatePosition();
			UpdateBridgePillarColor();
			MaybePaste();
			MaybeRotate();
			if (m_BridgePillars.Count == 1)
			{
				BridgeJointPlacement.UpdatePlacementCrosshairs(m_BridgePillars[0].m_Joint.transform.position);
			}
		}
	}

	public static bool ReadyToPaste()
	{
		return !IsEmpty();
	}

	public static void ClearClipboard()
	{
		for (int i = 0; i < m_Edges.Count; i++)
		{
			m_Edges[i].gameObject.SetActive(value: false);
			Object.Destroy(m_Edges[i].gameObject);
		}
		for (int j = 0; j < m_Joints.Count; j++)
		{
			m_Joints[j].gameObject.SetActive(value: false);
			Object.Destroy(m_Joints[j].gameObject);
		}
		for (int k = 0; k < m_BridgePillars.Count; k++)
		{
			m_BridgePillars[k].gameObject.SetActive(value: false);
			Object.Destroy(m_BridgePillars[k].gameObject);
		}
		m_Edges.Clear();
		m_Joints.Clear();
		m_BridgePillars.Clear();
		m_JointMap.Clear();
		m_ClipboardContainer.transform.localScale = new Vector3(1f, 1f, 1f);
		m_CanRotateOrFlipContainer.transform.localScale = new Vector3(1f, 1f, 1f);
		m_CanRotateOrFlipContainer.transform.rotation = Quaternion.Euler(Vector3.zero);
		m_IgnoreNextPaste = false;
	}

	public static void StartRotate(float degrees)
	{
		m_TimeRotateHeldDown = 0f;
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			degrees *= 90f;
		}
		else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
		{
			degrees *= 0.1f;
		}
		Rotate(degrees);
	}

	public static bool SetContainerPosition(Vector3 pos)
	{
		if (Utils.ApproximatelyEquals(Utils.V3toV2(m_ClipboardContainer.transform.position), Utils.V3toV2(pos)))
		{
			return false;
		}
		m_ClipboardContainer.transform.position = pos;
		return true;
	}

	public static void ShiftContainerPosition(Vector2 v)
	{
		m_ClipboardContainer.transform.position += Utils.V2toV3(v);
	}

	public static ClipboardJoint AddJoint(Vector2 relativePos, BridgeJoint sourceJoint)
	{
		Transform transform = m_CanRotateOrFlipContainer.transform;
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && BridgeSelectionSet.ContainsPillar(bridgePillar))
			{
				BridgeJoint anchor = bridgePillar.GetAnchor();
				if (anchor.m_Guid == sourceJoint.m_Guid || BridgeEdges.AreJointsConnected(sourceJoint, anchor))
				{
					transform = m_ClipboardContainer.transform;
					break;
				}
			}
		}
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_ClipboardJoint, transform);
		gameObject.transform.localPosition = new Vector3(relativePos.x, relativePos.y, 0f);
		ClipboardJoint component = gameObject.GetComponent<ClipboardJoint>();
		component.m_IsSplit = sourceJoint.m_IsSplit;
		component.m_SourceBridgeJoint = sourceJoint;
		if (component.m_IsSplit)
		{
			component.DrawAsSplitJoint();
		}
		m_Joints.Add(component);
		if (!m_JointMap.ContainsKey(sourceJoint.m_Guid))
		{
			m_JointMap.Add(sourceJoint.m_Guid, component);
		}
		return component;
	}

	public static ClipboardEdge AddEdge(Vector2 relativePos, float angle, float length, BridgeEdge sourceEdge)
	{
		GameObject gameObject = Object.Instantiate(GetClipboardPrefabForEdge(sourceEdge.m_Material.m_MaterialType));
		Transform parent = m_CanRotateOrFlipContainer.transform;
		if (m_JointMap.ContainsKey(sourceEdge.m_JointA.m_Guid))
		{
			parent = m_JointMap[sourceEdge.m_JointA.m_Guid].transform.parent;
		}
		else if (m_JointMap.ContainsKey(sourceEdge.m_JointB.m_Guid))
		{
			parent = m_JointMap[sourceEdge.m_JointB.m_Guid].transform.parent;
		}
		else
		{
			Debug.LogWarning("Shouldn't get here");
		}
		gameObject.transform.SetParent(parent);
		gameObject.transform.localPosition = new Vector3(relativePos.x, relativePos.y, 0f);
		gameObject.transform.localEulerAngles = new Vector3(0f, 0f, angle);
		gameObject.transform.localScale = new Vector3(length, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		ClipboardEdge component = gameObject.GetComponent<ClipboardEdge>();
		component.m_SourceBridgeEdge = sourceEdge;
		m_Edges.Add(component);
		return component;
	}

	public static void AddBridgePillar(Vector2 relativePos, BridgePillar sourceBridgePillar)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_BridgePillarClipboard, m_ClipboardContainer.transform);
		gameObject.transform.localPosition = new Vector3(relativePos.x, relativePos.y, 0f);
		ClipboardBridgePillar component = gameObject.GetComponent<ClipboardBridgePillar>();
		component.m_SourceBridgePillar = sourceBridgePillar;
		component.SetTopHeightBasedOnTotalHeight(sourceBridgePillar.GetTotalHeight());
		component.m_Joint.gameObject.SetActive(value: false);
		m_BridgePillars.Add(component);
	}

	public static void FlipHorizontal()
	{
		m_ClipboardContainer.transform.localScale = new Vector3(0f - m_ClipboardContainer.transform.localScale.x, m_ClipboardContainer.transform.localScale.y, 1f);
		AlignClipboardAnchors();
	}

	public static void FlipVertical()
	{
		m_CanRotateOrFlipContainer.transform.localScale = new Vector3(m_CanRotateOrFlipContainer.transform.localScale.x, 0f - m_CanRotateOrFlipContainer.transform.localScale.y, 1f);
		AlignClipboardAnchors();
	}

	public static void IgnoreNextPaste()
	{
		m_IgnoreNextPaste = true;
	}

	public static float GetRotationDegrees()
	{
		return m_CanRotateOrFlipContainer.transform.rotation.eulerAngles.z;
	}

	public static void UpdateBridgePillarPolygonShapes()
	{
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			bridgePillar.UpdatePolygonShapes();
		}
	}

	public static void StartMovement(Vector3 pos)
	{
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			bridgePillar.m_StartMovementWorldPos = bridgePillar.transform.position;
			bridgePillar.m_StartMovementHeight = bridgePillar.GetTotalHeight();
		}
	}

	public static ClipboardJoint FindClipboardJointMatchingSource(string sourceGuid)
	{
		if (m_JointMap.ContainsKey(sourceGuid))
		{
			return m_JointMap[sourceGuid];
		}
		return null;
	}

	public static ClipboardBridgePillar FindClipboardBridgePillarMatchingSource(string sourceGuid)
	{
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_SourceBridgePillar.m_Guid == sourceGuid)
			{
				return bridgePillar;
			}
		}
		return null;
	}

	public static void ShowBridgePillarMarkers()
	{
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			BridgePillarDistanceMarkers.ShowMarkers(new Vector3(bridgePillar.transform.position.x, 1f, bridgePillar.transform.position.z));
		}
	}

	public static float GetCost()
	{
		float num = 0f;
		foreach (ClipboardEdge edge in m_Edges)
		{
			num += edge.m_SourceBridgeEdge.m_Material.m_PricePerMeter * edge.m_SourceBridgeEdge.GetLength();
		}
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			num += bridgePillar.m_SourceBridgePillar.Cost();
		}
		return num;
	}

	public static bool WillMergeWithJointGuid(string guid)
	{
		foreach (ClipboardJoint joint in m_Joints)
		{
			if (joint.m_MergeBridgeJoint != null && joint.m_MergeIcon.gameObject.activeInHierarchy && joint.m_MergeBridgeJoint.m_Guid == guid)
			{
				return true;
			}
		}
		return false;
	}

	private static void Paste()
	{
		BridgeActions.StartRecording();
		BridgeActions.SerializeBridgePre(BridgeSave.Serialize(), new ClipboardSaveData());
		PasteJoints();
		List<BridgePillar> list = PasteBridgePillars();
		List<BridgeEdge> list2 = PasteEdges();
		MaybeResetJointSelectors();
		CleanUpAfterPaste();
		AlignClipboardAnchors();
		if (list2.Count > 0 || list.Count > 0 || list.Count > 0)
		{
			BridgeActions.SerializeBridgePost(BridgeSave.Serialize(), new ClipboardSaveData());
			BridgeActions.FlushRecording();
			InterfaceAudio.Play("ui_build_paste");
		}
		else
		{
			BridgeActions.CancelRecording();
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private static bool BridgePillarWasPasted(BridgePillar bridgePillar)
	{
		foreach (ClipboardBridgePillar bridgePillar2 in m_BridgePillars)
		{
			if (bridgePillar2.m_PastedBridgePillar.m_Guid == bridgePillar.m_Guid)
			{
				return true;
			}
		}
		return false;
	}

	private static List<BridgeJoint> PasteJoints()
	{
		List<BridgeJoint> list = new List<BridgeJoint>();
		foreach (ClipboardJoint joint in m_Joints)
		{
			_ = joint.m_SourceBridgeJoint;
			ClipboardBridgePillar clipboardBridgePillar = joint.GetClipboardBridgePillar();
			if (!(clipboardBridgePillar != null) || BridgePillars.AllowedToPlaceClipboardBridgePillar(clipboardBridgePillar, Budget.m_PillarLeft) != PlacementReturnValue.SUCCESS)
			{
				BridgeJoint bridgeJoint = PasteJoint(joint);
				if (bridgeJoint != null)
				{
					list.Add(bridgeJoint);
				}
			}
		}
		return list;
	}

	private static BridgeJoint PasteJoint(ClipboardJoint clipboardJoint)
	{
		if (clipboardJoint.WillMerge())
		{
			clipboardJoint.m_PastedBridgeJoint = clipboardJoint.m_MergeBridgeJoint;
			if (clipboardJoint.m_IsSplit)
			{
				MaybeSplitPastedJoint(clipboardJoint.m_PastedBridgeJoint, clipboardJoint.m_SourceBridgeJoint);
			}
			if (clipboardJoint.m_MergeBridgeJoint.m_IsSplit && clipboardJoint.m_SourceBridgeJoint.m_IsSplit && IsFlippedHorizontally() && GetNumClipboardEdgesConnectedToJoint(clipboardJoint.m_SourceBridgeJoint) == 2)
			{
				clipboardJoint.m_ResetJointSelectorsAfterPaste = true;
			}
			return null;
		}
		if (!clipboardJoint.IsBad() && CanPasteJointAtPosition(clipboardJoint.transform.position))
		{
			Vector3 pos = new Vector3(clipboardJoint.transform.position.x, clipboardJoint.transform.position.y, clipboardJoint.m_SourceBridgeJoint.transform.position.z);
			clipboardJoint.m_PastedBridgeJoint = BridgeJoints.CreateJoint(pos, Utils.GenerateUniqueId());
			if ((bool)clipboardJoint.m_PastedBridgeJoint)
			{
				if (clipboardJoint.m_IsSplit)
				{
					MaybeSplitPastedJoint(clipboardJoint.m_PastedBridgeJoint, clipboardJoint.m_SourceBridgeJoint);
				}
				return clipboardJoint.m_PastedBridgeJoint;
			}
		}
		return null;
	}

	private static bool CanPasteJointAtPosition(Vector3 dest)
	{
		if (!WorldBounds.Contains(dest))
		{
			WorldBounds.ShowBriefly();
			return false;
		}
		if (BridgeJoints.JointOverlapsPosition(dest, GameSettings.NodeDiameter()))
		{
			return false;
		}
		if (BridgeJoints.NodeLocationOverlapsBlockingPolygonShape(dest))
		{
			return false;
		}
		if (!BuildZones.ContainsJoint(dest))
		{
			return false;
		}
		return true;
	}

	private static void MaybeSplitPastedJoint(BridgeJoint pastedJoint, BridgeJoint sourceJoint)
	{
		if ((bool)pastedJoint && !pastedJoint.m_IsSplit)
		{
			pastedJoint.Split();
			HydraulicsController.AddSplitJointToAllPhasesAcceptingNewAdditions(pastedJoint);
			if (sourceJoint.m_IsSplit)
			{
				HydraulicsController.CopySplitJointState(sourceJoint, pastedJoint);
			}
		}
	}

	private static BridgeEdge TryPasteEdge(BridgeJoint jointA, BridgeJoint jointB, BridgeMaterialType materialType)
	{
		if (!jointA || !jointB)
		{
			return null;
		}
		BridgeEdge edgeFromJoints = BridgeEdges.GetEdgeFromJoints(jointA, jointB);
		if (!edgeFromJoints && !BridgeJoints.JointsCanAddEdgeWithoutExceedingEdgeLimit(jointA, jointB))
		{
			BridgePlacement.DisplayPlacementFailureMessage(PlacementReturnValue.FAIL_EXCEEDS_MAX_EDGE_LIMIT_PER_NODE);
			return null;
		}
		if ((bool)edgeFromJoints && (!edgeFromJoints.IsLocked() || Game.InSandboxGodMode()))
		{
			Budget.AdjustBudgetForRemovedEdge(edgeFromJoints);
		}
		if (!BridgeEdges.CanFormEdgeBetweenJoints(edgeFromJoints, jointA, jointB, materialType))
		{
			return null;
		}
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(materialType);
		if (bridgeMaterial != null && BridgeEdges.EdgeLocationOverlapsBlockingPolygonShape(jointA.transform.position, jointB.transform.position, materialType, bridgeMaterial.m_EdgeMaterial.collisionRadius))
		{
			return null;
		}
		if ((bool)edgeFromJoints && (!edgeFromJoints.IsLocked() || Game.InSandboxGodMode()))
		{
			edgeFromJoints.ForceDisable();
			BridgeSelectionSet.DeSelectEdge(edgeFromJoints);
			BridgeActions.Delete(edgeFromJoints);
		}
		return BridgeEdges.CreateEdge(jointA, jointB, materialType, Utils.GenerateUniqueId(), null);
	}

	private static bool HasMaterialToPasteEdge(BridgeMaterialType materialType)
	{
		return Budget.HasMaterialLeft(materialType);
	}

	private static bool CanAffordToPasteEdge(BridgeJoint jointA, BridgeJoint jointB, BridgeMaterialType materialType)
	{
		return Budget.CanAffordEdge(Vector3.Distance(jointA.transform.position, jointB.transform.position), materialType);
	}

	private static List<BridgeEdge> PasteEdges()
	{
		int num = 0;
		List<BridgeEdge> list = new List<BridgeEdge>();
		foreach (ClipboardEdge edge in m_Edges)
		{
			BridgeEdge sourceBridgeEdge = edge.m_SourceBridgeEdge;
			BridgeJoint jointA = FindPastedJointMatchingSourceJoint(sourceBridgeEdge.m_JointA);
			BridgeJoint jointB = FindPastedJointMatchingSourceJoint(sourceBridgeEdge.m_JointB);
			BridgeEdge bridgeEdge = TryPasteEdge(jointA, jointB, edge.m_SourceBridgeEdge.m_Material.m_MaterialType);
			if ((bool)bridgeEdge)
			{
				if (edge.m_SourceBridgeEdge.IsPiston())
				{
					PasteHydraulics(bridgeEdge, edge.m_SourceBridgeEdge);
				}
				if (edge.m_SourceBridgeEdge.IsSpring())
				{
					PasteSpring(bridgeEdge, edge.m_SourceBridgeEdge);
				}
				MaybeCopyEdgeJointSelections(bridgeEdge, sourceBridgeEdge);
				if (Game.InSandboxGodMode())
				{
					bridgeEdge.SetPrebuiltState(sourceBridgeEdge.m_PrebuiltState);
				}
				list.Add(bridgeEdge);
				edge.m_PastedBridgeEdge = bridgeEdge;
				Budget.AdjustBudgetForAddedEdge(bridgeEdge);
				num++;
			}
		}
		BridgeJointSelectors.RefreshVisibility();
		return list;
	}

	private static void PasteHydraulics(BridgeEdge newEdge, BridgeEdge sourceEdge)
	{
		Piston pistonOnEdge = Pistons.GetPistonOnEdge(sourceEdge);
		if (!pistonOnEdge)
		{
			return;
		}
		Piston piston = Pistons.CreatePiston(newEdge.m_JointA, newEdge.m_JointB, pistonOnEdge.m_Slider.GetNormalizedValue(), Utils.GenerateUniqueId());
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

	private static void PasteSpring(BridgeEdge newEdge, BridgeEdge sourceEdge)
	{
		if ((bool)sourceEdge.m_SpringCoilVisualization)
		{
			BridgeSprings.CreateSpring(newEdge, sourceEdge.m_SpringCoilVisualization.m_Slider.GetNormalizedValue(), Utils.GenerateUniqueId());
		}
	}

	private static List<BridgePillar> PasteBridgePillars()
	{
		int num = 0;
		List<BridgePillar> list = new List<BridgePillar>();
		foreach (ClipboardBridgePillar bridgePillar2 in m_BridgePillars)
		{
			if (BridgePillars.AllowedToPlaceClipboardBridgePillar(bridgePillar2, Budget.m_PillarLeft) != PlacementReturnValue.SUCCESS)
			{
				continue;
			}
			ClipboardJoint clipboardJoint = FindClipboardJointMatchingSource(bridgePillar2.m_SourceBridgePillar.m_AnchorGuid);
			if (clipboardJoint == null)
			{
				continue;
			}
			BridgeJoint bridgeJoint = PasteJoint(clipboardJoint);
			if (clipboardJoint.m_PastedBridgeJoint == null)
			{
				continue;
			}
			clipboardJoint.m_PastedBridgeJoint.MakeAnchor();
			clipboardJoint.m_PastedBridgeJoint.gameObject.SetActive(value: false);
			BridgePillar bridgePillar = TryPasteBridgePillar(bridgePillar2, clipboardJoint.m_PastedBridgeJoint.m_Guid);
			if ((bool)bridgePillar)
			{
				if (Game.InSandboxGodMode())
				{
					bridgePillar.SetPrebuiltState(bridgePillar2.m_SourceBridgePillar.m_PrebuiltState);
				}
				bridgePillar.SetColor(BridgePillars.m_NormalColor);
				list.Add(bridgePillar);
				bridgePillar2.m_PastedBridgePillar = bridgePillar;
				Budget.AdjustBudgetForAddedBridgePillar(bridgePillar);
				num++;
				clipboardJoint.m_PastedBridgeJoint.gameObject.SetActive(value: true);
			}
			else if (bridgeJoint != null)
			{
				bridgeJoint.Destroy();
			}
			else if (clipboardJoint.m_PastedBridgeJoint != null)
			{
				clipboardJoint.m_PastedBridgeJoint.RevertAnchor();
				clipboardJoint.m_PastedBridgeJoint.gameObject.SetActive(value: true);
			}
		}
		return list;
	}

	private static void MaybeCopyEdgeJointSelections(BridgeEdge newEdge, BridgeEdge sourceEdge)
	{
		if (sourceEdge.m_JointA.m_IsSplit && newEdge.m_JointA.m_IsSplit)
		{
			newEdge.m_JointAPart = sourceEdge.m_JointAPart;
		}
		if (sourceEdge.m_JointB.m_IsSplit && newEdge.m_JointB.m_IsSplit)
		{
			newEdge.m_JointBPart = sourceEdge.m_JointBPart;
		}
		newEdge.RefreshJointSelectorNumbers();
	}

	private static BridgePillar TryPasteBridgePillar(ClipboardBridgePillar clipboardBridgePillar, string anchorGuid)
	{
		if (BridgePillars.AllowedToPlaceClipboardBridgePillar(clipboardBridgePillar, Budget.m_PillarLeft) != PlacementReturnValue.SUCCESS)
		{
			return null;
		}
		int remainingFromHardBudget = Budget.GetRemainingFromHardBudget();
		if ((float)Mathf.RoundToInt(clipboardBridgePillar.m_SourceBridgePillar.Cost() + 0.5f) > (float)remainingFromHardBudget)
		{
			return null;
		}
		return BridgePillars.Create(Prefabs.m_Instance.m_BridgePillar, clipboardBridgePillar.GetTotalHeight(), clipboardBridgePillar.transform.position, clipboardBridgePillar.transform.rotation, Utils.GenerateUniqueId().ToString(), anchorGuid);
	}

	private static void MaybeResetJointSelectors()
	{
		foreach (ClipboardJoint joint in m_Joints)
		{
			if (joint != null && joint.m_PastedBridgeJoint != null && joint.m_ResetJointSelectorsAfterPaste)
			{
				joint.m_PastedBridgeJoint.ResetJointSelectors();
			}
		}
	}

	private static void CleanUpAfterPaste()
	{
		BridgeJoints.DestroyOrphanedJoints();
		foreach (ClipboardJoint joint in m_Joints)
		{
			joint.m_PastedBridgeJoint = null;
		}
		foreach (ClipboardEdge edge in m_Edges)
		{
			edge.m_PastedBridgeEdge = null;
		}
	}

	private static BridgeJoint FindPastedJointMatchingSourceJoint(BridgeJoint source)
	{
		ClipboardJoint clipboardJoint = FindClipboardJointMatchingSource(source.m_Guid);
		if (!(clipboardJoint != null))
		{
			return null;
		}
		return clipboardJoint.m_PastedBridgeJoint;
	}

	private static ClipboardBridgePillar FindClipboardPillarWithSourceAnchor(string anchorGuid)
	{
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_SourceBridgePillar.m_AnchorGuid == anchorGuid)
			{
				return bridgePillar;
			}
		}
		return null;
	}

	private static void UpdatePosition()
	{
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
		if (m_BridgePillars.Count > 0 && worldPointFromScreenPos.y < 0f)
		{
			worldPointFromScreenPos.y = 0f;
		}
		if (SetContainerPosition(GameGrid.SnapPosToGrid(worldPointFromScreenPos)))
		{
			AlignClipboardAnchors();
		}
		ConstrainPositionForBridgePillars();
	}

	private static void ConstrainPositionForBridgePillars()
	{
		float num = 0f;
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			ClipboardJoint clipboardJoint = FindClipboardJointMatchingSource(bridgePillar.m_SourceBridgePillar.m_AnchorGuid);
			if (clipboardJoint != null && clipboardJoint.transform.position.y < BridgePillars.MIN_HEIGHT)
			{
				float num2 = BridgePillars.MIN_HEIGHT - clipboardJoint.transform.position.y;
				if (num2 > num)
				{
					num = num2;
				}
			}
		}
		if (num > 0f)
		{
			foreach (ClipboardJoint joint in m_Joints)
			{
				joint.transform.Translate(0f, num, 0f);
			}
			foreach (ClipboardEdge edge in m_Edges)
			{
				edge.UpdateTransform();
			}
		}
		foreach (ClipboardBridgePillar bridgePillar2 in m_BridgePillars)
		{
			ClipboardJoint clipboardJoint2 = FindClipboardJointMatchingSource(bridgePillar2.m_SourceBridgePillar.m_AnchorGuid);
			bridgePillar2.StickToGround();
			float topHeightBasedOnTotalHeight = Mathf.Clamp(GameGrid.RoundToNearestGridSquare(clipboardJoint2.transform.position.y), BridgePillars.MIN_HEIGHT, float.MaxValue);
			bridgePillar2.SetTopHeightBasedOnTotalHeight(topHeightBasedOnTotalHeight);
		}
	}

	private static void UpdateBridgePillarColor()
	{
		int num = Budget.m_PillarLeft;
		foreach (ClipboardBridgePillar bridgePillar in m_BridgePillars)
		{
			if (BridgePillars.AllowedToPlaceClipboardBridgePillar(bridgePillar, num) == PlacementReturnValue.SUCCESS)
			{
				bridgePillar.SetPlacementColor();
				num--;
			}
			else
			{
				bridgePillar.SetErrorColor();
			}
		}
	}

	private static void MaybePaste()
	{
		if (!GameInput.JustReleased(BindingType.DRAW_BUILD) || GameUI.IsPointerOverGameObject())
		{
			return;
		}
		if (CampaignTutorial.BlockPaste())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		if (!m_IgnoreNextPaste)
		{
			Paste();
		}
		m_IgnoreNextPaste = false;
	}

	private static void MaybeRotate()
	{
		if (GameInput.JustPressed(BindingType.ROTATE_CLIPBOARD_RIGHT) || GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
		{
			StartRotate(-1f);
		}
		if (GameInput.JustPressed(BindingType.ROTATE_CLIPBOARD_LEFT) || GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			StartRotate(1f);
		}
		if (GameInput.IsDown(BindingType.ROTATE_CLIPBOARD_RIGHT) || GamepadManager.ButtonIsDown(GamepadButtonType.SHOULDER_RIGHT))
		{
			ContinuousRotate(clockwise: true);
		}
		if (GameInput.IsDown(BindingType.ROTATE_CLIPBOARD_LEFT) || GamepadManager.ButtonIsDown(GamepadButtonType.SHOULDER_LEFT))
		{
			ContinuousRotate(clockwise: false);
		}
		if (GameInput.GetMouseButtonIsDown(0) && GameUI.m_Instance.m_Clipboard.m_RotateRightHover.m_IsHovering)
		{
			ContinuousRotate(clockwise: true);
		}
		if (GameInput.GetMouseButtonIsDown(0) && GameUI.m_Instance.m_Clipboard.m_RotateLeftHover.m_IsHovering)
		{
			ContinuousRotate(clockwise: false);
		}
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			m_TimeRotateHeldDown = 0f;
			m_NextTickTime = 0f;
			m_AccumulatedRotation = 0f;
		}
	}

	private static void ContinuousRotate(bool clockwise)
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			ContinuousRotatePerTick(clockwise, 90f);
		}
		else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
		{
			ContinuousRotatePerTick(clockwise, 0.1f);
		}
		else
		{
			ContinuousRotatePerFrame(clockwise);
		}
	}

	private static void ContinuousRotatePerTick(bool clockwise, float degrees)
	{
		float num = ROTATE_REPEAT_SECONDS;
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			num = ROTATE_REPEAT_SECONDS_90;
		}
		m_TimeRotateHeldDown += Time.unscaledDeltaTime;
		m_NextTickTime += Time.unscaledDeltaTime;
		if (m_TimeRotateHeldDown > ROTATE_REPEAT_DELAY_SECONDS)
		{
			m_NextTickTime += Time.unscaledDeltaTime;
			if (m_NextTickTime > num)
			{
				Rotate(clockwise ? (0f - degrees) : degrees);
				m_NextTickTime = 0f;
			}
		}
	}

	private static void ContinuousRotatePerFrame(bool clockwise)
	{
		m_TimeRotateHeldDown += Time.unscaledDeltaTime;
		if (m_TimeRotateHeldDown > ROTATE_REPEAT_DELAY_SECONDS)
		{
			float num = ROTATION_DEGREES_PER_SECOND * Time.unscaledDeltaTime;
			m_AccumulatedRotation += num;
			int num2 = Mathf.FloorToInt(m_AccumulatedRotation);
			if (num2 >= 1)
			{
				Rotate(clockwise ? (-num2) : num2);
				m_AccumulatedRotation -= num2;
			}
		}
	}

	public static float Rotate(float value)
	{
		float num = m_CanRotateOrFlipContainer.transform.rotation.eulerAngles.z + value;
		m_CanRotateOrFlipContainer.transform.rotation = Quaternion.Euler(0f, 0f, num);
		AlignClipboardAnchors();
		GameUI.m_Instance.m_Clipboard.m_DisplayRotationText = true;
		return num;
	}

	public static void AlignClipboardAnchors()
	{
		ClipboardJoint clipboardJoint = null;
		BridgeJoint bridgeJoint = null;
		float num = float.MaxValue;
		foreach (ClipboardJoint joint in m_Joints)
		{
			Vector3 vector = new Vector3(joint.transform.position.x, joint.transform.position.y, joint.m_SourceBridgeJoint.transform.position.z);
			if (BridgeJoints.SphereOverlapsJoint(vector, GameSettings.NodeRadius()))
			{
				joint.SetNormal();
				continue;
			}
			joint.SetBad();
			BridgeJoint bridgeJoint2 = BridgeJoints.FindClosestJoint(vector);
			if ((bool)bridgeJoint2)
			{
				float num2 = Vector2.Distance(bridgeJoint2.transform.position, joint.transform.position);
				if (num2 < num)
				{
					num = num2;
					bridgeJoint = bridgeJoint2;
					clipboardJoint = joint;
				}
			}
		}
		if (clipboardJoint != null)
		{
			TrySnapContainerAnchorToJoint(clipboardJoint, bridgeJoint);
		}
		foreach (ClipboardJoint joint2 in m_Joints)
		{
			ClipboardBridgePillar clipboardBridgePillar = joint2.GetClipboardBridgePillar();
			if (clipboardBridgePillar != null)
			{
				clipboardBridgePillar.UpdatePolygonShapes();
				BridgePillars.AllowedToPlaceClipboardBridgePillar(clipboardBridgePillar, Budget.m_PillarLeft);
			}
			BridgeJoint bridgeJoint3 = BridgeJoints.FindClosestJoint(joint2.transform.position);
			if ((bool)bridgeJoint3 && Vector2.Distance(bridgeJoint3.transform.position, joint2.transform.position) < GameSettings.NodeRadius())
			{
				if (bridgeJoint3.m_NoBuild && !Game.InSandboxGodMode())
				{
					joint2.SetBad();
				}
				else
				{
					joint2.SetMerge(bridgeJoint3);
				}
			}
			else if (!WorldBounds.Contains(joint2.transform.position))
			{
				WorldBounds.ShowBriefly();
				joint2.SetBad();
			}
			else if (BridgeJoints.NodeLocationOverlapsBlockingPolygonShape(joint2.transform.position) || !BuildZones.ContainsJoint(joint2.transform.position))
			{
				joint2.SetBad();
			}
		}
		foreach (ClipboardJoint joint3 in m_Joints)
		{
			foreach (ClipboardEdge item in GetClipboardEdgesConnectedToJoint(joint3.m_SourceBridgeJoint))
			{
				if (joint3.WillMerge())
				{
					MarkJointBadIfInvalidEdge(joint3, item.m_SourceBridgeEdge);
				}
			}
		}
	}

	private static List<ClipboardEdge> GetClipboardEdgesConnectedToJoint(BridgeJoint joint)
	{
		List<ClipboardEdge> list = new List<ClipboardEdge>();
		foreach (ClipboardEdge edge in m_Edges)
		{
			if (edge.m_SourceBridgeEdge.m_JointA == joint || edge.m_SourceBridgeEdge.m_JointB == joint)
			{
				list.Add(edge);
			}
		}
		return list;
	}

	private static int GetNumClipboardEdgesConnectedToJoint(BridgeJoint joint)
	{
		int num = 0;
		foreach (ClipboardEdge edge in m_Edges)
		{
			if (edge.m_SourceBridgeEdge.m_JointA == joint || edge.m_SourceBridgeEdge.m_JointB == joint)
			{
				num++;
			}
		}
		return num;
	}

	private static void MarkJointBadIfInvalidEdge(ClipboardJoint joint, BridgeEdge connectedEdge)
	{
		BridgeJoint bridgeJoint = ((connectedEdge.m_JointA == joint.m_SourceBridgeJoint) ? connectedEdge.m_JointB : connectedEdge.m_JointA);
		if (!(bridgeJoint != null))
		{
			return;
		}
		ClipboardJoint clipboardJoint = FindClipboardJointMatchingSource(bridgeJoint.m_Guid);
		if (!clipboardJoint)
		{
			return;
		}
		Vector3 vector = (clipboardJoint.WillMerge() ? clipboardJoint.m_MergeBridgeJoint.transform.position : clipboardJoint.transform.position);
		if (BridgeEdges.IsValidEdgeLength(Vector2.Distance(joint.m_MergeBridgeJoint.transform.position, vector), GameSettings.NodeDiameter(), connectedEdge.m_Material.m_MaxLength))
		{
			return;
		}
		if ((bool)clipboardJoint.m_MergeBridgeJoint)
		{
			float num = Vector2.Distance(joint.transform.position, joint.m_MergeBridgeJoint.transform.position);
			float num2 = Vector2.Distance(clipboardJoint.transform.position, clipboardJoint.m_MergeBridgeJoint.transform.position);
			if (num > num2)
			{
				joint.SetBad();
			}
			else
			{
				clipboardJoint.SetBad();
			}
		}
		else
		{
			joint.SetBad();
		}
	}

	private static void TrySnapContainerAnchorToJoint(ClipboardJoint joint, BridgeJoint bridgeJoint)
	{
		if (!(Vector2.Distance(joint.transform.position, bridgeJoint.transform.position) > GameSettings.NodeRadius()))
		{
			ShiftContainerPosition(Utils.V3toV2(bridgeJoint.transform.position) - Utils.V3toV2(joint.transform.position));
			joint.SetMerge(bridgeJoint);
		}
	}

	private static bool IsFlippedHorizontally()
	{
		return m_ClipboardContainer.transform.localScale.x < 0f;
	}

	public static GameObject GetClipboardPrefabForEdge(BridgeMaterialType bridgeMaterialType)
	{
		switch (bridgeMaterialType)
		{
		case BridgeMaterialType.CABLE:
			return Prefabs.m_Instance.m_CableTrussClipboard;
		case BridgeMaterialType.HYDRAULICS:
			return Prefabs.m_Instance.m_HydraulicsTrussClipboard;
		case BridgeMaterialType.REINFORCED_ROAD:
			return Prefabs.m_Instance.m_ReinforcedRoadClipboard;
		case BridgeMaterialType.ROAD:
			return Prefabs.m_Instance.m_RoadClipboard;
		case BridgeMaterialType.ROPE:
			return Prefabs.m_Instance.m_RopeTrussClipboard;
		case BridgeMaterialType.SPRING:
			return Prefabs.m_Instance.m_SpringTrussClipboard;
		case BridgeMaterialType.STEEL:
			return Prefabs.m_Instance.m_SteelTrussClipboard;
		case BridgeMaterialType.WOOD:
			return Prefabs.m_Instance.m_WoodTrussClipboard;
		default:
			Debug.LogErrorFormat("Unexpected material {0} in GetClipboardPrefabForEdge");
			return null;
		}
	}

	private static bool CanPasteClipboardJoint(ClipboardJoint joint)
	{
		if (!WorldBounds.Contains(joint.transform.position))
		{
			WorldBounds.ShowBriefly();
			return false;
		}
		if (BridgeJoints.JointOverlapsPosition(joint.transform.position, GameSettings.NodeDiameter()))
		{
			return false;
		}
		if (BridgeJoints.NodeLocationOverlapsBlockingPolygonShape(joint.transform.position))
		{
			return false;
		}
		if (BuildZones.ContainsJoint(joint.transform.position))
		{
			return false;
		}
		if (BridgePillarAnchorPasteBlockedByInvalidPillarPlacement(joint) && (!BridgePillars.IsBridgePillarAnchor(joint.m_SourceBridgeJoint.m_Guid) || !BridgeEdges.EdgeIsConnectedToJoint(joint.m_SourceBridgeJoint)))
		{
			return false;
		}
		return true;
	}

	private static bool BridgePillarAnchorPasteBlockedByInvalidPillarPlacement(ClipboardJoint joint)
	{
		ClipboardBridgePillar clipboardBridgePillar = FindClipboardPillarWithSourceAnchor(joint.m_SourceBridgeJoint.m_Guid);
		if (clipboardBridgePillar != null && BridgePillars.AllowedToPlaceClipboardBridgePillar(clipboardBridgePillar, Budget.m_PillarLeft) != PlacementReturnValue.SUCCESS)
		{
			return true;
		}
		return false;
	}
}
