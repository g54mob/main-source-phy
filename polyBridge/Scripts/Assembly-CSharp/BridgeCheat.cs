using System.Collections.Generic;
using UnityEngine;

public class BridgeCheat : MonoBehaviour
{
	public static bool m_Cheated;

	public static CheatReason m_CheatReason;

	public static bool m_ForceUnlimitedBudget;

	public static bool m_ForceUnlimitedMaterial;

	public static bool m_AllowPhysicsCheatFlagsInSandbox;

	private static readonly float SAME_DISTANCE_THRESHOLD = 0.001f;

	private static readonly int MIN_COST_WHEN_USING_ROAD = 70;

	private static BridgeMaterialType m_InvalidLengthBridgeMaterialType;

	private static int m_OverHardCost;

	private static BridgeMaterialType m_InvalidBridgeMaterialType;

	private static Dictionary<BridgeMaterialType, int> m_NumMaterialsUsed = new Dictionary<BridgeMaterialType, int>();

	public static void Init()
	{
		InitNumMaterialsUsed();
	}

	public static void Clear()
	{
		m_Cheated = false;
		m_CheatReason = CheatReason.NONE;
		Budget.m_UsingForcedUnlimitedBudget = false;
		Budget.m_UsingForcedUnlimitedMaterial = false;
		if (Sandbox.m_CurrentLayoutData != null)
		{
			Budget.Deserialize(Sandbox.m_CurrentLayoutData.m_Budget);
			GameUI.m_Instance.m_BottomBar.SetMaterialIconsAlpha();
		}
	}

	public static void CheckForCheating(SandboxLayoutData layoutData, BridgeSaveData bridgeSaveData, string levelId)
	{
		m_Cheated = false;
		m_CheatReason = CheatReason.NONE;
		if (Budget.m_UsingForcedUnlimitedMaterial)
		{
			SetCheated(CheatReason.UNLIMITED_MATERIAL);
		}
		else if (Budget.m_UsingForcedUnlimitedBudget)
		{
			SetCheated(CheatReason.UNLIMITED_BUDGET);
		}
		else if (CostExceedsHardBudget())
		{
			SetCheated(CheatReason.COST_EXCEEDS_HARD_BUDGET);
			m_OverHardCost = GetCostOverHardBudget();
		}
		else if (InvalidEdges())
		{
			SetCheated(CheatReason.INVALID_EDGE_LENGTH);
			m_InvalidLengthBridgeMaterialType = GetInvalidEdgeMaterialType();
		}
		else if (InvalidBridgePillarHeight())
		{
			SetCheated(CheatReason.INVALID_BRIDGE_PILLAR_HEIGHT);
		}
		else if (WorkshopSubmit.BridgeHasIllegalNodePlacement())
		{
			SetCheated(CheatReason.ILLEGAL_NODE_PLACEMENT);
		}
		else if (BridgeJoints.GetNumSplitJoints() > 0 && HydraulicsPhases.m_Phases.Count == 0)
		{
			SetCheated(CheatReason.SPLIT_JOINTS_WITHOUT_HYDRAULICS_PHASE);
		}
		else if (Mods.m_IsUsingGameplayMod)
		{
			SetCheated(CheatReason.USING_GAMEPLAY_MOD);
		}
		else if (PrebuiltsModified(layoutData, bridgeSaveData, levelId))
		{
			SetCheated(CheatReason.PREBUILT_MATERIALS_MODIFIED);
		}
		else if (BuiltOutsizeBuildZone())
		{
			SetCheated(CheatReason.BUILT_OUTSIDE_BUILD_ZONES);
		}
		else if (!SandboxSettings.m_SpringAdjustmentsAllowed && BridgeSprings.SpringsHaveBeenAdjusted())
		{
			SetCheated(CheatReason.ILLEGAL_SPRING_ADJUSTMENTS);
		}
		else if (BridgePillars.IllegalFoundationPlacment())
		{
			SetCheated(CheatReason.ILLEGAL_FOUNDATION_PLACEMENT);
		}
		else
		{
			CheckForInvalidMaterials();
		}
	}

	public static void CheckForInvalidMaterials()
	{
		ResetNumMaterialsUsed();
		CollectNumMaterialsUsed();
		int num = m_NumMaterialsUsed[BridgeMaterialType.ROAD] + m_NumMaterialsUsed[BridgeMaterialType.REINFORCED_ROAD];
		if (m_NumMaterialsUsed[BridgeMaterialType.WOOD] > 0 && !Budget.m_AllowWood)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.WOOD;
		}
		else if (m_NumMaterialsUsed[BridgeMaterialType.STEEL] > 0 && !Budget.m_AllowSteel)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.STEEL;
		}
		else if (m_NumMaterialsUsed[BridgeMaterialType.HYDRAULICS] > 0 && !Budget.m_AllowHydraulic)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.HYDRAULICS;
		}
		else if (m_NumMaterialsUsed[BridgeMaterialType.ROPE] > 0 && !Budget.m_AllowRope)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.ROPE;
		}
		else if (m_NumMaterialsUsed[BridgeMaterialType.CABLE] > 0 && !Budget.m_AllowCable)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.CABLE;
		}
		else if (m_NumMaterialsUsed[BridgeMaterialType.SPRING] > 0 && !Budget.m_AllowSpring)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.SPRING;
		}
		else if (m_NumMaterialsUsed[BridgeMaterialType.PILLAR] > 0 && !Budget.m_AllowPillar)
		{
			SetCheated(CheatReason.MATERIAL_NOT_ALLOWED);
			m_InvalidBridgeMaterialType = BridgeMaterialType.PILLAR;
		}
		else if (Budget.m_RoadBudget != Budget.UNLIMITED_MATERIAL_BUDGET && num > Budget.m_RoadBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.ROAD;
		}
		else if (Budget.m_WoodBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.WOOD] > Budget.m_WoodBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.WOOD;
		}
		else if (Budget.m_SteelBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.STEEL] > Budget.m_SteelBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.STEEL;
		}
		else if (Budget.m_HydraulicBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.HYDRAULICS] > Budget.m_HydraulicBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.HYDRAULICS;
		}
		else if (Budget.m_RopeBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.ROPE] > Budget.m_RopeBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.ROPE;
		}
		else if (Budget.m_CableBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.CABLE] > Budget.m_CableBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.CABLE;
		}
		else if (Budget.m_SpringBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.SPRING] > Budget.m_SpringBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.SPRING;
		}
		else if (Budget.m_PillarBudget != Budget.UNLIMITED_MATERIAL_BUDGET && m_NumMaterialsUsed[BridgeMaterialType.PILLAR] > Budget.m_PillarBudget)
		{
			SetCheated(CheatReason.MATERIAL_BEYOND_BUDGET);
			m_InvalidBridgeMaterialType = BridgeMaterialType.PILLAR;
		}
	}

	public static string GetLocalizedCheatReason()
	{
		switch (m_CheatReason)
		{
		case CheatReason.NONE:
			return string.Empty;
		case CheatReason.UNLIMITED_MATERIAL:
			return Localize.Get("UI_UNLIMITED_MATERIAL");
		case CheatReason.UNLIMITED_BUDGET:
			return Localize.Get("UI_UNLIMITED_BUDGET");
		case CheatReason.COST_EXCEEDS_HARD_BUDGET:
		{
			if (Mathf.Approximately(Budget.m_CashBudget, 0f))
			{
				return Localize.Get("UI_OVERBUDGET");
			}
			float num = (float)m_OverHardCost / (float)Budget.m_CashBudget;
			return string.Format(Localize.Get("UI_OVERBUDGET"), Mathf.CeilToInt(num * 100f));
		}
		case CheatReason.INVALID_EDGE_LENGTH:
			return string.Format(Localize.Get("UI_ILLEGAL_MATERIAL_LENGTH"), BridgeMaterials.GetLocalizedMaterialDisplayName(m_InvalidLengthBridgeMaterialType));
		case CheatReason.INVALID_BRIDGE_PILLAR_HEIGHT:
			return Localize.Get("UI_ILLEGAL_FOUNDATION_HEIGHT");
		case CheatReason.ILLEGAL_NODE_PLACEMENT:
			return Localize.Get("WARN_WORKSHOP_ILLEGAL_NODES");
		case CheatReason.SPLIT_JOINTS_WITHOUT_HYDRAULICS_PHASE:
			return Localize.Get("UI_ILLEGAL_SPLIT_NODES");
		case CheatReason.USING_GAMEPLAY_MOD:
			return Localize.Get("UI_GAMEPLAY_MOD_DETECTED");
		case CheatReason.MATERIAL_NOT_ALLOWED:
			return string.Format(Localize.Get("UI_MATERIAL_NOT_ALLOWED"), BridgeMaterials.GetLocalizedMaterialDisplayName(m_InvalidBridgeMaterialType));
		case CheatReason.MATERIAL_BEYOND_BUDGET:
			return string.Format(Localize.Get("UI_MATERIAL_BEYOND_BUDGET"), BridgeMaterials.GetLocalizedMaterialDisplayName(m_InvalidBridgeMaterialType));
		case CheatReason.PREBUILT_MATERIALS_MODIFIED:
			return Localize.Get("UI_MODIFIED_PREBUILT_MATERIALS");
		case CheatReason.BUILT_OUTSIDE_BUILD_ZONES:
			return Localize.Get("UI_PLACEMENT_OUTSIDE_BUILD_ZONE");
		case CheatReason.ILLEGAL_SPRING_ADJUSTMENTS:
			return Localize.Get("UI_ILLEGAL_SPRING_ADJUSTMENTS");
		case CheatReason.ILLEGAL_FOUNDATION_PLACEMENT:
			return Localize.Get("UI_ILLEGAL_FOUNDATION_PLACEMENT");
		case CheatReason.VEHICLE_JOINT_BROKEN:
			return Localize.Get("UI_VEHICLE_JOINT_BROKEN");
		case CheatReason.VEHICLE_HIGH_ACCELERATION:
			return Localize.Get("UI_VEHICLE_HIGH_ACCELERATION");
		default:
			Debug.LogWarning("Unexpected Cheat Reason: " + m_Cheated);
			return string.Empty;
		}
	}

	public static bool BridgeCostValid(int cost)
	{
		if (cost == 0)
		{
			return false;
		}
		bool flag = false;
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && edge.IsRoad())
			{
				flag = true;
				break;
			}
		}
		if (flag && cost < MIN_COST_WHEN_USING_ROAD)
		{
			return false;
		}
		return true;
	}

	private static void InitNumMaterialsUsed()
	{
		m_NumMaterialsUsed.Add(BridgeMaterialType.ROAD, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.REINFORCED_ROAD, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.WOOD, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.STEEL, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.HYDRAULICS, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.ROPE, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.CABLE, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.BUNGINE_ROPE, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.SPRING, 0);
		m_NumMaterialsUsed.Add(BridgeMaterialType.PILLAR, 0);
	}

	private static void ResetNumMaterialsUsed()
	{
		m_NumMaterialsUsed[BridgeMaterialType.ROAD] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.REINFORCED_ROAD] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.WOOD] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.STEEL] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.HYDRAULICS] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.ROPE] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.CABLE] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.BUNGINE_ROPE] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.SPRING] = 0;
		m_NumMaterialsUsed[BridgeMaterialType.PILLAR] = 0;
	}

	private static void CollectNumMaterialsUsed()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsLocked())
			{
				m_NumMaterialsUsed[edge.m_Material.m_MaterialType]++;
			}
		}
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && !bridgePillar.IsLocked())
			{
				m_NumMaterialsUsed[BridgeMaterialType.PILLAR]++;
			}
		}
	}

	internal static void SetCheated(CheatReason cheatReason)
	{
		m_Cheated = true;
		m_CheatReason = cheatReason;
	}

	private static bool CostExceedsHardBudget()
	{
		return GetCostOverHardBudget() > 0;
	}

	private static int GetCostOverHardBudget()
	{
		int num = Mathf.RoundToInt(Budget.CalculateBridgeCost());
		int hardBudgetLimit = Budget.GetHardBudgetLimit();
		return num - hardBudgetLimit;
	}

	private static bool InvalidEdges()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsValidLength())
			{
				return true;
			}
		}
		return false;
	}

	private static bool InvalidBridgePillarHeight()
	{
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && !bridgePillar.IsValidHeight())
			{
				return true;
			}
		}
		return false;
	}

	private static BridgeMaterialType GetInvalidEdgeMaterialType()
	{
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsValidLength())
			{
				return edge.m_Material.m_MaterialType;
			}
		}
		return BridgeMaterialType.INVALID;
	}

	private static bool PrebuiltsModified(SandboxLayoutData layoutData, BridgeSaveData bridgeSaveData, string levelId)
	{
		if (layoutData == null || layoutData.m_Bridge == null || bridgeSaveData == null)
		{
			return false;
		}
		if (layoutData.m_Bridge.HasHardPrebuilts())
		{
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.ROAD) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.ROAD))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.WOOD) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.WOOD))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.STEEL) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.STEEL))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.HYDRAULICS) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.HYDRAULICS))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.ROPE) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.ROPE))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.CABLE) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.CABLE))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.SPRING) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.SPRING))
			{
				return true;
			}
			if (layoutData.m_Bridge.GetNumHardPrebuiltMaterials(BridgeMaterialType.PILLAR) != bridgeSaveData.GetNumHardPrebuiltMaterials(BridgeMaterialType.PILLAR))
			{
				return true;
			}
		}
		foreach (BridgeEdgeProxy bridgeEdge2 in layoutData.m_Bridge.m_BridgeEdges)
		{
			if (bridgeEdge2.m_BridgePrebuiltState != PrebuiltState.HARD_LOCKED)
			{
				continue;
			}
			BridgeEdgeProxy bridgeEdge = bridgeSaveData.GetBridgeEdge(bridgeEdge2.m_NodeA_Guid, bridgeEdge2.m_NodeB_Guid);
			if (bridgeEdge == null)
			{
				continue;
			}
			if (bridgeEdge.m_BridgePrebuiltState != bridgeEdge2.m_BridgePrebuiltState)
			{
				return true;
			}
			if (bridgeEdge.m_Material != bridgeEdge2.m_Material)
			{
				return true;
			}
			float num = Vector3.Distance(bridgeSaveData.GetNodePosition(bridgeEdge.m_NodeA_Guid), layoutData.m_Bridge.GetNodePosition(bridgeEdge.m_NodeA_Guid));
			float num2 = Vector3.Distance(bridgeSaveData.GetNodePosition(bridgeEdge.m_NodeB_Guid), layoutData.m_Bridge.GetNodePosition(bridgeEdge.m_NodeB_Guid));
			if (levelId == "079")
			{
				if (num > SAME_DISTANCE_THRESHOLD)
				{
					BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(bridgeEdge.m_NodeA_Guid);
					if (bridgeJoint != null)
					{
						bridgeJoint.transform.position = layoutData.m_Bridge.GetNodePosition(bridgeEdge.m_NodeA_Guid);
					}
				}
				if (num2 > SAME_DISTANCE_THRESHOLD)
				{
					BridgeJoint bridgeJoint2 = BridgeJoints.FindByGuid(bridgeEdge.m_NodeB_Guid);
					if (bridgeJoint2 != null)
					{
						bridgeJoint2.transform.position = layoutData.m_Bridge.GetNodePosition(bridgeEdge.m_NodeB_Guid);
					}
				}
			}
			else
			{
				float num3 = GameGrid.m_Spacing + SAME_DISTANCE_THRESHOLD;
				if (num > num3 || num2 > num3)
				{
					return true;
				}
			}
		}
		foreach (BridgePillarProxy bridgePillar2 in layoutData.m_Bridge.m_BridgePillars)
		{
			if (bridgePillar2.m_BridgePrebuiltState == PrebuiltState.HARD_LOCKED)
			{
				BridgePillarProxy bridgePillar = bridgeSaveData.GetBridgePillar(bridgePillar2.m_Guid);
				if (bridgePillar == null)
				{
					return true;
				}
				if (bridgePillar.m_BridgePrebuiltState != bridgePillar2.m_BridgePrebuiltState)
				{
					return true;
				}
				if (Vector3.Distance(bridgePillar.m_Pos, bridgePillar2.m_Pos) > SAME_DISTANCE_THRESHOLD)
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool BuiltOutsizeBuildZone()
	{
		if (BuildZones.m_BuildZones.Count == 0)
		{
			return false;
		}
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsPrebuilt() && !BuildZones.ContainsEdge(edge.m_JointA.transform.position, edge.m_JointB.transform.position))
			{
				return true;
			}
		}
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && !bridgePillar.IsPrebuilt() && !BuildZones.ContainsBridgePillar(new Vector3(bridgePillar.transform.position.x, bridgePillar.transform.position.z + bridgePillar.GetTotalHeight(), bridgePillar.transform.position.z), bridgePillar.m_Outline.m_VectorLine))
			{
				return true;
			}
		}
		return false;
	}
}
