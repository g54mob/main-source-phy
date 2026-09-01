using System;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSave
{
	public static int CURRENT_VERSION = 18;

	public static bool m_IsDeserializing;

	private static readonly string THREE_WAY_SPLIT_JOINTS_ENABLED_KEY = "3way";

	public static BridgeSaveData Serialize()
	{
		BridgeSaveData bridgeSaveData = new BridgeSaveData();
		bridgeSaveData.m_Version = CURRENT_VERSION;
		bridgeSaveData.m_Anchors = BridgeJoints.SerializeAnchorsForBridgeSave();
		bridgeSaveData.m_BridgeJoints = BridgeJoints.SerializeNoAnchors();
		bridgeSaveData.m_BridgeEdges = BridgeEdges.Serialize();
		bridgeSaveData.m_BridgeSprings = BridgeSprings.Serialize();
		bridgeSaveData.m_Pistons = Pistons.Serialize();
		bridgeSaveData.m_HydraulicsController = HydraulicsController.Serialize();
		bridgeSaveData.m_BridgePillars = BridgePillars.Serialize();
		bridgeSaveData.m_BridgePillarAnchors = BridgePillars.SerializeAnchors();
		bridgeSaveData.m_BridgeEdgeColorsPermanent = new Dictionary<string, string>(BridgeEdges.m_BridgeEdgeColorsPermanent);
		if (SandboxSettings.m_ThreeWaySplitJointsEnabled)
		{
			bridgeSaveData.m_BridgeEdgeColorsPermanent.Add(THREE_WAY_SPLIT_JOINTS_ENABLED_KEY, "true");
		}
		return bridgeSaveData;
	}

	public static void Deserialize(BridgeSaveData saveData)
	{
		m_IsDeserializing = true;
		try
		{
			if (saveData.m_BridgeEdgeColorsPermanent != null && saveData.m_BridgeEdgeColorsPermanent.ContainsKey(THREE_WAY_SPLIT_JOINTS_ENABLED_KEY))
			{
				SandboxSettings.m_ThreeWaySplitJointsEnabled = true;
				saveData.m_BridgeEdgeColorsPermanent.Remove(THREE_WAY_SPLIT_JOINTS_ENABLED_KEY);
			}
			BridgeEdges.m_BridgeEdgeColorsPermanent = saveData.m_BridgeEdgeColorsPermanent;
			BridgeJoints.Deserialize(saveData.m_BridgePillarAnchors);
			BridgeJoints.Deserialize(saveData.m_BridgeJoints);
			SplitAnchors(saveData.m_Anchors);
			BridgeEdges.Deserialize(saveData.m_BridgeEdges);
			BridgeSprings.Deserialize(saveData.m_BridgeSprings);
			Pistons.Deserialize(saveData.m_Pistons);
			SanitizeHydraulics();
			HydraulicsController.Deserialize(saveData.m_Version, saveData.m_HydraulicsController);
			BridgePillars.Deserialize(saveData.m_BridgePillars);
			CreateSpringsForEdges();
			BridgeJoints.MakeDefaultColor();
			if ((GameStateManager.GetState() == GameState.BUILD && !GameStateBuild.m_CameraInTransition) || (GameStateManager.GetState() == GameState.SANDBOX && !GameStateSandbox.m_CameraInTransition))
			{
				BridgePillars.EnableOutlines();
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
		}
		m_IsDeserializing = false;
	}

	private static void SanitizeHydraulics()
	{
		for (int num = BridgeEdges.m_Edges.Count - 1; num >= 0; num--)
		{
			BridgeEdge bridgeEdge = BridgeEdges.m_Edges[num];
			if (bridgeEdge.IsPiston() && Pistons.GetPistonOnEdge(bridgeEdge) == null)
			{
				bridgeEdge.Destroy();
			}
			else if (Pistons.GetPistonOnEdge(bridgeEdge) != null && !bridgeEdge.IsPiston())
			{
				bridgeEdge.m_Material = BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.HYDRAULICS);
			}
		}
	}

	private static void CreateSpringsForEdges()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.IsSpring() && edge.m_SpringCoilVisualization == null)
			{
				BridgeSprings.CreateSpring(edge, 0.5f, Utils.GenerateUniqueId());
			}
		}
	}

	private static void SplitAnchors(List<BridgeJointProxy> bridgeAnchors)
	{
		foreach (BridgeJointProxy bridgeAnchor in bridgeAnchors)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(bridgeAnchor.m_Guid);
			if ((bool)bridgeJoint && bridgeAnchor.m_IsSplit)
			{
				bridgeJoint.Split();
				bridgeJoint.ResetJointSelectors();
				HydraulicsController.AddSplitJointToAllPhasesAcceptingNewAdditions(bridgeJoint);
			}
		}
	}

	public static byte[] SerializeBinary()
	{
		return Serialize().SerializeBinary();
	}

	public static BridgeSaveData DeserializeBinary(byte[] bytes)
	{
		BridgeSaveData bridgeSaveData = new BridgeSaveData();
		int offset = 0;
		bridgeSaveData.DeserializeBinary(bytes, ref offset);
		Deserialize(bridgeSaveData);
		return bridgeSaveData;
	}
}
