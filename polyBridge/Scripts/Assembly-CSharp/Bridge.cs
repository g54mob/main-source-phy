using System.Collections.Generic;
using Poly.Base;
using Poly.Draw;
using Poly.Graphics;
using UnityEngine;

public class Bridge
{
	public static BridgeMaterialType m_BuildMaterialType;

	public static bool m_Simulating;

	public static float m_SimulationStartTime;

	public const float HALF_ROAD_HEIGHT = 0.1f;

	public static bool m_DebugVisualizePolygonShapesForVehicles;

	public static BridgeSaveData m_BridgeRestore;

	private static float m_NextRevealTime;

	private static float m_RevealInterval;

	private static int m_NextRevealIndex;

	private static List<BridgeEdge> m_RevealEdges = new List<BridgeEdge>();

	private static Vector3 m_PreviousErasePos;

	public static void Init()
	{
		m_BuildMaterialType = BridgeMaterialType.ROAD;
		BridgeSimSpeed.Init();
	}

	public static void Clear()
	{
		ClearWithoutUndoReset();
		BridgeUndo.Reset();
		BridgeRedo.Reset();
		m_Simulating = false;
	}

	public static void ClearWithoutUndoReset()
	{
		BridgeTrace.CancelFill();
		BridgeTrace.ClearTraceLine();
		CancelSelection();
		ClipboardManager.ClearClipboard();
		BridgePhysics.Reset();
		BridgeJoints.DestroyAllExceptLayoutAnchors();
		BridgeJoints.UnSplitAllJoints();
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
		BridgeEdges.DestroyAll();
		BridgeSprings.DestroyAll();
		Pistons.DestroyAll();
		BridgePillars.DestroyAll();
		HydraulicsController.Reset();
	}

	public static void ClearAndLoadforUndo(BridgeSaveData bridgeSaveData)
	{
		ClearWithoutUndoReset();
		Load(bridgeSaveData);
	}

	public static void ClearAndLoad(BridgeSaveData bridgeSaveData)
	{
		Clear();
		Load(bridgeSaveData);
	}

	public static void Load(BridgeSaveData bridgeSaveData)
	{
		GameUI.m_Instance.m_HydraulicsController.gameObject.SetActive(value: false);
		BridgeSave.Deserialize(bridgeSaveData);
		if (!SandboxSettings.m_ThreeWaySplitJointsEnabled)
		{
			SandboxSettings.m_ThreeWaySplitJointsEnabled = BridgeJoints.GetNumThreeWaySplitJoints() > 0;
		}
	}

	public static BridgeSaveData ClearAndLoadBinary(byte[] bytes)
	{
		Clear();
		BridgeCheat.Clear();
		GameUI.m_Instance.m_HydraulicsController.gameObject.SetActive(value: false);
		BridgeSaveData result = BridgeSave.DeserializeBinary(bytes);
		if (!SandboxSettings.m_ThreeWaySplitJointsEnabled)
		{
			SandboxSettings.m_ThreeWaySplitJointsEnabled = BridgeJoints.GetNumThreeWaySplitJoints() > 0;
		}
		return result;
	}

	public static void UpdateManual()
	{
		BridgePhysics.UpdateCurrentTime();
		if (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX)
		{
			BridgeJointPlacement.UpdateManual();
		}
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			BridgeTrace.UpdateManual();
		}
		if (!m_Simulating)
		{
			BridgeEdges.UpdateManual();
		}
		if (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX)
		{
			BridgeJoints.UpdateManualOutsideSim();
			BridgeEdges.UpdateManualOutsideSim();
		}
		else if (GameStateManager.GetState() == GameState.SIM || GameStateManager.GetState() == GameState.MAIN_MENU)
		{
			BridgeJoints.UpdateFlashingJoints();
		}
		BridgeJointMovement.UpdateManual();
		if (m_Simulating)
		{
			BridgeJoints.ApplySimulationResults_AndCacheSmoothNodePos();
			BridgeEdges.UpdateTransforms_InSimulation();
		}
		else if (m_DebugVisualizePolygonShapesForVehicles)
		{
			GlDrawer.Clear();
			Vehicles.Debug_VisualizePolygonShapes();
		}
		Pistons.UpdateManual();
		BridgeSprings.UpdateManual();
		BridgeRopes.UpdateManual();
		UpdateReveal();
	}

	public static void FixedUpdateManual()
	{
		BridgePhysics.UpdateCurrentTime();
		if (m_Simulating)
		{
			BridgePhysics.FixedUpdateManual();
			BridgeRopes.FixedUpdateManual();
			BridgeSprings.FixedUpdateManual();
		}
	}

	public static bool IsSimulating()
	{
		return m_Simulating;
	}

	public static void StartSimulation()
	{
		StressSamples.Reset();
		BridgePhysics.Reset();
		BridgePhysics.StartSimulation();
		TerrainIslands.AddToSimulation();
		BridgeJoints.AddToSimulation();
		BridgeEdges.AddToSimulation();
		BridgePillars.AddToSimulation();
		Platforms.AddToSimulation();
		Ramps.AddToSimulation();
		Rocks.AddToSimulation();
		FlyingObjects.AddToSimulation();
		if (!SandboxSettings.m_NoWater)
		{
			WaterBlocks.AddToSimulation();
		}
		CustomShapes.AddToSimulation();
		Vehicles.ResetCheckpoints();
		Vehicles.AddVisiblityBlock();
		Vehicles.EnablePhysics();
		ZedAxisVehicles.EnablePhysics();
		HydraulicsPhases.AddToHydraulicController();
		EventTimelines.StartSimulation();
		WaterBlocks.StartSimulation();
		BridgeSimSpeed.SetTimeScaleForSimulation();
		BridgeSimSpeed.SetPitchForSimulation();
		m_Simulating = true;
		if (GameStateManager.GetState() == GameState.SIM)
		{
			SingletonBehaviour<GpuInstancer>.instance?.Activate();
		}
	}

	public static void CancelSelection()
	{
		BridgeJointPlacement.CancelSelection();
		BridgeJointMovement.CancelSelection();
		BridgeSelectionSet.CancelSelection();
	}

	public static void ProcessBuildAction()
	{
		switch (GameToolMode.GetMode())
		{
		case GameToolModeType.MOVE:
			BridgeJointMovement.ProcessClick(GameInput.GetMousePosition());
			BridgePillarMovement.ProcessClick(GameInput.GetMousePosition());
			break;
		case GameToolModeType.BUILD:
			BridgeJointPlacement.ProcessClick(GameInput.GetMousePosition());
			break;
		}
	}

	public static void ProcessSelectAction()
	{
		if (BridgeJointSelectors.CycleUnderMouse(GameInput.GetMousePosition(), forward: false) || GameUI.m_Instance.m_HydraulicsController.gameObject.activeInHierarchy || CampaignTutorial.BlockJointEdgeSelection() || GameUI.IsPointerOverGameObject())
		{
			return;
		}
		if ((bool)BridgeJointPlacement.m_HoverJoint)
		{
			if (GameInput.MultiSelectIsDown())
			{
				if (BridgeSelectionSet.ContainsJoint(BridgeJointPlacement.m_HoverJoint))
				{
					BridgeSelectionSet.DeSelectJoint(BridgeJointPlacement.m_HoverJoint);
					return;
				}
				BridgeSelectionSet.SelectJointAndConnectedEdges(BridgeJointPlacement.m_HoverJoint);
				InterfaceAudio.Play("ui_build_select");
			}
			else
			{
				BridgeSelectionSet.CancelSelection();
				BridgeSelectionSet.SelectJointAndConnectedEdges(BridgeJointPlacement.m_HoverJoint);
				InterfaceAudio.Play("ui_build_select");
			}
		}
		else if (GameInput.MultiSelectIsDown())
		{
			if (!BridgeSelectionSet.TrySelectJoint(GameInput.GetMousePosition(), toggle: true))
			{
				BridgeSelectionSet.TrySelectEdge(GameInput.GetMousePosition(), toggle: true);
				BridgeSelectionSet.TrySelectBridgePillar(GameInput.GetMousePosition(), toggle: true);
			}
		}
		else
		{
			BridgeSelectionSet.CancelSelection();
			if (!BridgeSelectionSet.TrySelectJoint(GameInput.GetMousePosition(), toggle: true))
			{
				BridgeSelectionSet.TrySelectEdge(GameInput.GetMousePosition(), toggle: false);
				BridgeSelectionSet.TrySelectBridgePillar(GameInput.GetMousePosition(), toggle: false);
			}
		}
	}

	public static void HideAllUI()
	{
		BridgeJoints.HideAllUI();
		BridgeEdges.HideJointSelectorUI();
		BridgeEdges.EnableJointCaps();
		BridgeJoints.EnableSplitAnchorJointCaps();
		Pistons.HideAllUI();
		BridgeSprings.HideAllUI();
		Pistons.EnablePinions();
	}

	public static void UnHideAllUI()
	{
		BridgeJoints.UnHideAllUI();
		BridgeJoints.UnHideSplitUI();
		BridgeJoints.DisableJointCaps();
		BridgeJointSelectors.RefreshVisibility();
		Pistons.UnHideAllUI();
		BridgeSprings.UnHideAllUI();
		Pistons.DisablePinions();
	}

	public static void RefreshZoomDependentVisibility()
	{
		BridgeJointSelectors.RefreshVisibility();
		BridgeJoints.RefreshThreeWaySplitJointNumberVisibility();
	}

	public static GameObject GetPrefabFromBridgeMaterial(BridgeMaterialType material)
	{
		switch (material)
		{
		case BridgeMaterialType.ROAD:
			return Prefabs.m_Instance.m_Road;
		case BridgeMaterialType.REINFORCED_ROAD:
			return Prefabs.m_Instance.m_ReinforcedRoad;
		case BridgeMaterialType.WOOD:
			return Prefabs.m_Instance.m_WoodTruss;
		case BridgeMaterialType.STEEL:
			return Prefabs.m_Instance.m_SteelTruss;
		case BridgeMaterialType.HYDRAULICS:
			return Prefabs.m_Instance.m_HydraulicsTruss;
		case BridgeMaterialType.ROPE:
			return Prefabs.m_Instance.m_RopeTruss;
		case BridgeMaterialType.CABLE:
			return Prefabs.m_Instance.m_CableTruss;
		case BridgeMaterialType.BUNGINE_ROPE:
			return Prefabs.m_Instance.m_BungieRopeTruss;
		case BridgeMaterialType.SPRING:
			return Prefabs.m_Instance.m_SpringTruss;
		default:
			Debug.LogErrorFormat("Unsupport build material: {0}", m_BuildMaterialType);
			return null;
		}
	}

	public static bool IsSimulationPaused()
	{
		if (IsSimulating())
		{
			return Mathf.Approximately(Time.timeScale, 0f);
		}
		return false;
	}

	public static void RevertToSavedBridge(BridgeSaveData bridgeSaveData)
	{
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			shape.Restore();
		}
		BridgePhysics.Reset();
		BridgeJoints.DestroyAllExceptLayoutAnchors();
		SingletonBehaviour<GpuInstancer>.instance?.Reset();
		BridgeEdges.DestroyAll();
		BridgeSprings.DestroyAll();
		BridgePillars.DestroyAll();
		Pistons.DestroyAll();
		HydraulicsController.DestroyAll();
		BridgeSave.Deserialize(bridgeSaveData);
	}

	public static void Sanitize()
	{
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (joint.m_IsAnchor && joint.gameObject.activeInHierarchy)
			{
				BridgeJoints.DeleteInvalidAnchorEdges(joint);
			}
		}
	}

	public static void Hide()
	{
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			HideMeshes(joint.gameObject, hide: true);
			joint.m_FX.SetActive(value: false);
			joint.m_SnapToFX.SetActive(value: false);
		}
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			HideMeshes(edge.gameObject, hide: true);
			if (edge.IsPiston())
			{
				Piston pistonOnEdge = Pistons.GetPistonOnEdge(edge);
				if ((bool)pistonOnEdge)
				{
					HideMeshes(pistonOnEdge.gameObject, hide: true);
				}
			}
			if (edge.IsSpring())
			{
				HideSkinnedMeshes(edge.gameObject, hide: true);
			}
		}
	}

	public static void Reveal(float intervalSeconds)
	{
		m_RevealInterval = intervalSeconds;
		m_NextRevealTime = Time.unscaledTime + 0.2f;
		m_NextRevealIndex = 0;
		CreateRevealEdgesList();
	}

	public static bool HasPrebuilts()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.IsPrebuilt())
			{
				return true;
			}
		}
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.IsPrebuilt())
			{
				return true;
			}
		}
		return false;
	}

	public static void DestroyAllExceptPrebuilt()
	{
		BridgeEdges.DestroyAllExceptPrebuilt();
		BridgePillars.DestroyAllExceptPrebuilt();
		BridgeJoints.DestroyOrphanedJoints();
	}

	public static void InitPreviousErasePos(Vector2 mouseScreenPos)
	{
		m_PreviousErasePos = Utils.GetWorldPointFromScreenPos(mouseScreenPos);
	}

	public static void Erase(Vector2 mouseScreenPos)
	{
		BridgeSelectionSet.CancelSelection();
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(mouseScreenPos);
		if (Mathf.Approximately((worldPointFromScreenPos - m_PreviousErasePos).magnitude, 0f))
		{
			Vector3 worldPointFromScreenPos2 = Utils.GetWorldPointFromScreenPos(mouseScreenPos);
			Vector3 vector = worldPointFromScreenPos2 + new Vector3(0.01f, -0.01f, 0f);
			Vector2 vector2 = (worldPointFromScreenPos2 + vector) / 2f;
			Vector2 vector3 = new Vector3(Mathf.Abs(worldPointFromScreenPos2.x - vector.x), Mathf.Abs(worldPointFromScreenPos2.y - vector.y));
			BridgeSelectionSet.SelectAllInRect(new Rect(vector2 - vector3 / 2f, vector3), invert: false);
		}
		else
		{
			BridgeSelectionSet.SelectAllInPath(m_PreviousErasePos, worldPointFromScreenPos);
		}
		m_PreviousErasePos = worldPointFromScreenPos;
		float cost = BridgeSelectionSet.GetCost();
		BridgeSelectionSet.DeleteSelectionSet();
		if (BridgeSelectionSet.GetCost() < cost)
		{
			InterfaceAudio.Play("ui_pop");
		}
		BridgeSelectionSet.CancelSelection();
	}

	private static void UpdateReveal()
	{
		if (m_NextRevealIndex < m_RevealEdges.Count && !(Time.unscaledTime < m_NextRevealTime))
		{
			RevealEdge(m_RevealEdges[m_NextRevealIndex]);
			m_NextRevealTime = Time.unscaledTime + m_RevealInterval;
			m_NextRevealIndex++;
		}
	}

	private static void RevealEdge(BridgeEdge edge)
	{
		if (!edge.IsSpring())
		{
			HideMeshes(edge.gameObject, hide: false);
		}
		else
		{
			HideSkinnedMeshes(edge.gameObject, hide: false);
		}
		if (edge.IsPiston())
		{
			Piston pistonOnEdge = Pistons.GetPistonOnEdge(edge);
			if ((bool)pistonOnEdge)
			{
				HideMeshes(pistonOnEdge.gameObject, hide: false);
			}
		}
		HideMeshes(edge.m_JointA.gameObject, hide: false);
		HideMeshes(edge.m_JointB.gameObject, hide: false);
		edge.m_JointA.m_FX.SetActive(value: true);
		edge.m_JointB.m_FX.SetActive(value: true);
	}

	private static void CreateRevealEdgesList()
	{
		m_RevealEdges.Clear();
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				m_RevealEdges.Add(edge);
			}
		}
		m_RevealEdges.Sort(SortByX);
	}

	private static void HideMeshes(GameObject go, bool hide)
	{
		MeshRenderer[] componentsInChildren = go.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = !hide;
		}
	}

	private static void HideSkinnedMeshes(GameObject go, bool hide)
	{
		SkinnedMeshRenderer[] componentsInChildren = go.GetComponentsInChildren<SkinnedMeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = !hide;
		}
	}

	private static int SortByX(BridgeEdge A, BridgeEdge B)
	{
		float num = Mathf.Min(A.m_JointA.transform.position.x, A.m_JointB.transform.position.x);
		float value = Mathf.Min(B.m_JointA.transform.position.x, B.m_JointB.transform.position.x);
		return num.CompareTo(value);
	}
}
