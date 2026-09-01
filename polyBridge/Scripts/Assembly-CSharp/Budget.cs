using UnityEngine;

public class Budget
{
	public static int MIN_CASH_BUDGET = 0;

	public static int MAX_CASH_BUDGET = 9999999;

	public static int MAX_MATERIAL_BUDGET = 99;

	public static int UNLIMITED_CASH_BUDGET = MAX_CASH_BUDGET + 1;

	public static int UNLIMITED_MATERIAL_BUDGET = MAX_MATERIAL_BUDGET + 1;

	public static int MIN_ROAD_BUDGET = 0;

	public static float m_BridgeCost;

	public static int m_CashBudget;

	public static int m_RoadBudget;

	public static int m_RoadLeft;

	public static int m_WoodBudget;

	public static int m_WoodLeft;

	public static int m_SteelBudget;

	public static int m_SteelLeft;

	public static int m_HydraulicBudget;

	public static int m_HydraulicLeft;

	public static int m_RopeBudget;

	public static int m_RopeLeft;

	public static int m_CableBudget;

	public static int m_CableLeft;

	public static int m_BungieRopeBudget;

	public static int m_BungieRopeLeft;

	public static int m_SpringBudget;

	public static int m_SpringLeft;

	public static int m_PillarBudget;

	public static int m_PillarLeft;

	public static bool m_AllowWood;

	public static bool m_AllowSteel;

	public static bool m_AllowHydraulic;

	public static bool m_AllowRope;

	public static bool m_AllowCable;

	public static bool m_AllowSpring;

	public static bool m_AllowPillar;

	public static bool m_UsingForcedUnlimitedBudget;

	public static bool m_UsingForcedUnlimitedMaterial;

	private static float m_SoftBudgetMultiplier = 2f;

	public static void Init()
	{
		m_CashBudget = UNLIMITED_CASH_BUDGET;
		m_RoadBudget = UNLIMITED_MATERIAL_BUDGET;
		m_WoodBudget = UNLIMITED_MATERIAL_BUDGET;
		m_SteelBudget = UNLIMITED_MATERIAL_BUDGET;
		m_HydraulicBudget = UNLIMITED_MATERIAL_BUDGET;
		m_RopeBudget = UNLIMITED_MATERIAL_BUDGET;
		m_CableBudget = UNLIMITED_MATERIAL_BUDGET;
		m_BungieRopeBudget = UNLIMITED_MATERIAL_BUDGET;
		m_SpringBudget = UNLIMITED_MATERIAL_BUDGET;
		m_PillarBudget = UNLIMITED_MATERIAL_BUDGET;
		m_RoadLeft = m_RoadBudget;
		m_WoodLeft = m_WoodBudget;
		m_SteelLeft = m_SteelBudget;
		m_HydraulicLeft = m_HydraulicBudget;
		m_RopeLeft = m_RopeBudget;
		m_CableLeft = m_CableBudget;
		m_BungieRopeLeft = m_BungieRopeBudget;
		m_SpringLeft = m_SpringBudget;
		m_AllowWood = true;
		m_AllowSteel = true;
		m_AllowHydraulic = true;
		m_AllowRope = true;
		m_AllowCable = true;
		m_AllowSpring = true;
		m_AllowPillar = true;
	}

	public static void UpdateManual()
	{
		UpdateBridgeCost();
		UpdateRemainingMaterials();
	}

	public static bool HasMaterialLeft(BridgeMaterialType materialType)
	{
		if (Game.InSandboxGodMode())
		{
			return true;
		}
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
			return m_RoadLeft > 0;
		case BridgeMaterialType.REINFORCED_ROAD:
			return m_RoadLeft > 0;
		case BridgeMaterialType.WOOD:
			return m_WoodLeft > 0;
		case BridgeMaterialType.STEEL:
			return m_SteelLeft > 0;
		case BridgeMaterialType.HYDRAULICS:
			return m_HydraulicLeft > 0;
		case BridgeMaterialType.ROPE:
			return m_RopeLeft > 0;
		case BridgeMaterialType.CABLE:
			return m_CableLeft > 0;
		case BridgeMaterialType.BUNGINE_ROPE:
			return m_BungieRopeLeft > 0;
		case BridgeMaterialType.SPRING:
			return m_SpringLeft > 0;
		case BridgeMaterialType.PILLAR:
			return m_PillarLeft > 0;
		default:
			Debug.LogWarningFormat("Unsuppported material {0}", materialType.ToString());
			return false;
		}
	}

	public static int GetMaterialLeft(BridgeMaterialType materialType)
	{
		if (Game.InSandboxGodMode())
		{
			return 1000;
		}
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
			return m_RoadLeft;
		case BridgeMaterialType.REINFORCED_ROAD:
			return m_RoadLeft;
		case BridgeMaterialType.WOOD:
			return m_WoodLeft;
		case BridgeMaterialType.STEEL:
			return m_SteelLeft;
		case BridgeMaterialType.HYDRAULICS:
			return m_HydraulicLeft;
		case BridgeMaterialType.ROPE:
			return m_RopeLeft;
		case BridgeMaterialType.CABLE:
			return m_CableLeft;
		case BridgeMaterialType.BUNGINE_ROPE:
			return m_BungieRopeLeft;
		case BridgeMaterialType.SPRING:
			return m_SpringLeft;
		case BridgeMaterialType.PILLAR:
			return m_PillarLeft;
		default:
			Debug.LogWarningFormat("Unsuppported material {0}", materialType.ToString());
			return 0;
		}
	}

	public static bool HasZeroBudget(BridgeMaterialType materialType)
	{
		if (Game.InSandboxGodMode())
		{
			return false;
		}
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
		case BridgeMaterialType.REINFORCED_ROAD:
			return m_RoadBudget == 0;
		case BridgeMaterialType.WOOD:
			return m_WoodBudget == 0;
		case BridgeMaterialType.STEEL:
			return m_SteelBudget == 0;
		case BridgeMaterialType.HYDRAULICS:
			return m_HydraulicBudget == 0;
		case BridgeMaterialType.ROPE:
			return m_RopeBudget == 0;
		case BridgeMaterialType.CABLE:
			return m_CableBudget == 0;
		case BridgeMaterialType.BUNGINE_ROPE:
			return m_BungieRopeBudget == 0;
		case BridgeMaterialType.SPRING:
			return m_SpringBudget == 0;
		case BridgeMaterialType.PILLAR:
			return m_PillarBudget == 0;
		default:
			Debug.LogWarningFormat("Unsuppported material {0}", materialType.ToString());
			return true;
		}
	}

	public static BridgeMaterialType GetFirstNegativeMaterial()
	{
		if (m_RoadLeft < 0)
		{
			return BridgeMaterialType.ROAD;
		}
		if (m_AllowWood && m_WoodLeft < 0)
		{
			return BridgeMaterialType.WOOD;
		}
		if (m_AllowSteel && m_SteelLeft < 0)
		{
			return BridgeMaterialType.STEEL;
		}
		if (m_AllowHydraulic && m_HydraulicLeft < 0)
		{
			return BridgeMaterialType.HYDRAULICS;
		}
		if (m_AllowRope && m_RopeLeft < 0)
		{
			return BridgeMaterialType.ROPE;
		}
		if (m_AllowCable && m_CableLeft < 0)
		{
			return BridgeMaterialType.CABLE;
		}
		if (m_AllowSpring && m_SpringLeft < 0)
		{
			return BridgeMaterialType.SPRING;
		}
		if (m_AllowPillar && m_PillarLeft < 0)
		{
			return BridgeMaterialType.PILLAR;
		}
		return BridgeMaterialType.INVALID;
	}

	public static bool CanAffordEdge(float length, BridgeMaterialType materialType)
	{
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (Game.InSandboxGodMode())
		{
			return true;
		}
		if (m_CashBudget == UNLIMITED_CASH_BUDGET)
		{
			return true;
		}
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(materialType);
		if (bridgeMaterial == null)
		{
			return false;
		}
		return CanAffordCost(bridgeMaterial.m_PricePerMeter * length);
	}

	public static bool CanAffordCost(float cost)
	{
		if (Game.InSandboxGodMode())
		{
			return true;
		}
		if (m_CashBudget == UNLIMITED_CASH_BUDGET)
		{
			return true;
		}
		return Mathf.RoundToInt(m_BridgeCost + cost) <= Mathf.RoundToInt(m_SoftBudgetMultiplier * (float)m_CashBudget);
	}

	public static void AdjustBudgetForAddedEdge(BridgeEdge edge)
	{
		m_BridgeCost += edge.m_Material.m_PricePerMeter * edge.GetLength();
		switch (edge.m_Material.m_MaterialType)
		{
		case BridgeMaterialType.ROAD:
		case BridgeMaterialType.REINFORCED_ROAD:
			m_RoadLeft = ((m_RoadBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_RoadLeft - 1));
			break;
		case BridgeMaterialType.WOOD:
			m_WoodLeft = ((m_WoodBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_WoodLeft - 1));
			break;
		case BridgeMaterialType.STEEL:
			m_SteelLeft = ((m_SteelBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_SteelLeft - 1));
			break;
		case BridgeMaterialType.HYDRAULICS:
			m_HydraulicLeft = ((m_HydraulicBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_HydraulicLeft - 1));
			break;
		case BridgeMaterialType.ROPE:
			m_RopeLeft = ((m_RopeBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_RopeLeft - 1));
			break;
		case BridgeMaterialType.CABLE:
			m_CableLeft = ((m_CableBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_CableLeft - 1));
			break;
		case BridgeMaterialType.BUNGINE_ROPE:
			m_BungieRopeLeft = ((m_BungieRopeBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_BungieRopeLeft - 1));
			break;
		case BridgeMaterialType.SPRING:
			m_SpringLeft = ((m_SpringBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_SpringLeft - 1));
			break;
		}
		GameUI.m_Instance.m_BottomBar.RefreshLimits();
		GameUI.m_Instance.m_BottomBar.m_PanelResizeHorizontal.ForceUpdate();
	}

	public static void AdjustBudgetForRemovedEdge(BridgeEdge edge)
	{
		m_BridgeCost -= edge.m_Material.m_PricePerMeter * edge.GetLength();
		switch (edge.m_Material.m_MaterialType)
		{
		case BridgeMaterialType.ROAD:
		case BridgeMaterialType.REINFORCED_ROAD:
			m_RoadLeft = ((m_RoadBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_RoadLeft + 1));
			break;
		case BridgeMaterialType.WOOD:
			m_WoodLeft = ((m_WoodBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_WoodLeft + 1));
			break;
		case BridgeMaterialType.STEEL:
			m_SteelLeft = ((m_SteelBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_SteelLeft + 1));
			break;
		case BridgeMaterialType.HYDRAULICS:
			m_HydraulicLeft = ((m_HydraulicBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_HydraulicLeft + 1));
			break;
		case BridgeMaterialType.ROPE:
			m_RopeLeft = ((m_RopeBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_RopeLeft + 1));
			break;
		case BridgeMaterialType.CABLE:
			m_CableLeft = ((m_CableBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_CableLeft + 1));
			break;
		case BridgeMaterialType.BUNGINE_ROPE:
			m_BungieRopeLeft = ((m_BungieRopeBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_BungieRopeLeft + 1));
			break;
		case BridgeMaterialType.SPRING:
			m_SpringLeft = ((m_SpringBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_SpringLeft + 1));
			break;
		}
		GameUI.m_Instance.m_BottomBar.RefreshLimits();
		GameUI.m_Instance.m_BottomBar.m_PanelResizeHorizontal.ForceUpdate();
	}

	public static void AdjustBudgetForAddedBridgePillar(BridgePillar bridgePillar)
	{
		m_BridgeCost += bridgePillar.Cost();
		m_PillarLeft = ((m_PillarBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_PillarLeft - 1));
	}

	public static void AdjustBudgetForRemovedBridgePillar(BridgePillar bridgePillar)
	{
		m_BridgeCost -= bridgePillar.Cost();
		m_PillarLeft = ((m_PillarBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_PillarLeft + 1));
	}

	public static bool CanAffordToBuild()
	{
		return CanAffordCost(0f);
	}

	public static int GetHardBudgetLimit()
	{
		return Mathf.RoundToInt((float)m_CashBudget * m_SoftBudgetMultiplier);
	}

	public static int GetRemainingFromHardBudget()
	{
		return GetHardBudgetLimit() - Mathf.RoundToInt(m_BridgeCost);
	}

	public static bool IsUnderBudget(float cost)
	{
		if (Mathf.RoundToInt(cost) > m_CashBudget)
		{
			return m_CashBudget == UNLIMITED_CASH_BUDGET;
		}
		return true;
	}

	public static Color GetBridgeCostTextColor()
	{
		if (IsUnderBudget(m_BridgeCost))
		{
			return GameUI.BudgetTextGreen();
		}
		return GameUI.BudgetTextRed();
	}

	public static int GetPercentagePointsOverBudget()
	{
		return Mathf.CeilToInt(100f * (float)(Mathf.RoundToInt(m_BridgeCost) - m_CashBudget) / (float)m_CashBudget);
	}

	public static BudgetProxy Serialize()
	{
		return new BudgetProxy();
	}

	public static void Deserialize(BudgetProxy proxy)
	{
		if (proxy != null)
		{
			m_CashBudget = (BridgeCheat.m_ForceUnlimitedBudget ? UNLIMITED_CASH_BUDGET : proxy.m_CashBudget);
			m_RoadBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_RoadBudget);
			m_WoodBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_WoodBudget);
			m_SteelBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_SteelBudget);
			m_HydraulicBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_HydraulicBudget);
			m_RopeBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_RopeBudget);
			m_CableBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_CableBudget);
			m_BungieRopeBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_BungieRopeBudget);
			m_SpringBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_SpringBudget);
			m_PillarBudget = (BridgeCheat.m_ForceUnlimitedMaterial ? UNLIMITED_MATERIAL_BUDGET : proxy.m_PillarBudget);
			m_AllowWood = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowWood;
			m_AllowSteel = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowSteel;
			m_AllowHydraulic = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowHydraulic;
			m_AllowRope = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowRope;
			m_AllowCable = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowCable;
			m_AllowSpring = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowSpring;
			m_AllowPillar = BridgeCheat.m_ForceUnlimitedMaterial || proxy.m_AllowPillar;
			m_UsingForcedUnlimitedBudget = BridgeCheat.m_ForceUnlimitedBudget;
			m_UsingForcedUnlimitedMaterial = BridgeCheat.m_ForceUnlimitedMaterial;
			if (m_UsingForcedUnlimitedMaterial)
			{
				BridgeCheat.m_Cheated = true;
				BridgeCheat.m_CheatReason = CheatReason.UNLIMITED_MATERIAL;
			}
			if (m_UsingForcedUnlimitedBudget)
			{
				BridgeCheat.m_Cheated = true;
				BridgeCheat.m_CheatReason = CheatReason.UNLIMITED_BUDGET;
			}
		}
	}

	public static void MaybeApplyForcedBudgets(bool unlimitedBudget, bool unlimitedMaterial)
	{
		m_BridgeCost = CalculateBridgeCost();
		if (unlimitedBudget && !CanAffordToBuild())
		{
			m_UsingForcedUnlimitedBudget = true;
			ApplyForceUnlimitedBudget();
		}
		if (unlimitedMaterial)
		{
			BridgeCheat.CheckForInvalidMaterials();
			if (BridgeCheat.m_CheatReason == CheatReason.MATERIAL_NOT_ALLOWED || BridgeCheat.m_CheatReason == CheatReason.MATERIAL_BEYOND_BUDGET)
			{
				m_UsingForcedUnlimitedMaterial = true;
				ApplyForceUnlimitedMaterial();
				GameUI.m_Instance.m_BottomBar.SetMaterialIconsAlpha();
			}
		}
		if (m_UsingForcedUnlimitedMaterial)
		{
			BridgeCheat.m_Cheated = true;
			BridgeCheat.m_CheatReason = CheatReason.UNLIMITED_MATERIAL;
		}
		if (m_UsingForcedUnlimitedBudget)
		{
			BridgeCheat.m_Cheated = true;
			BridgeCheat.m_CheatReason = CheatReason.UNLIMITED_BUDGET;
		}
	}

	public static float CalculateBridgeCost()
	{
		return BridgeEdges.Cost() + BridgePillars.Cost();
	}

	public static void UpdateBridgeCost()
	{
		m_BridgeCost = CalculateBridgeCost();
	}

	public static int GetDollarsOverNoBuildBudget()
	{
		return Mathf.RoundToInt(m_BridgeCost - m_SoftBudgetMultiplier * (float)m_CashBudget);
	}

	public static string BuildDisplayListOfResources()
	{
		string text = string.Empty;
		if (m_AllowWood && m_WoodBudget > 0)
		{
			text += BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.WOOD);
		}
		if (m_AllowSteel && m_SteelBudget > 0)
		{
			string materialDisplayName = BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.STEEL);
			text += (string.IsNullOrEmpty(text) ? materialDisplayName : (", " + materialDisplayName));
		}
		if (m_AllowHydraulic && m_HydraulicBudget > 0)
		{
			string materialDisplayName2 = BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.HYDRAULICS);
			text += (string.IsNullOrEmpty(text) ? materialDisplayName2 : (", " + materialDisplayName2));
		}
		if (m_AllowRope && m_RopeBudget > 0)
		{
			string materialDisplayName3 = BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.ROPE);
			text += (string.IsNullOrEmpty(text) ? materialDisplayName3 : (", " + materialDisplayName3));
		}
		if (m_AllowCable && m_CableBudget > 0)
		{
			string materialDisplayName4 = BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.CABLE);
			text += (string.IsNullOrEmpty(text) ? materialDisplayName4 : (", " + materialDisplayName4));
		}
		if (m_AllowSpring && m_SpringBudget > 0)
		{
			string materialDisplayName5 = BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.SPRING);
			text += (string.IsNullOrEmpty(text) ? materialDisplayName5 : (", " + materialDisplayName5));
		}
		if (m_AllowPillar && m_PillarBudget > 0)
		{
			string materialDisplayName6 = BridgeMaterials.GetMaterialDisplayName(BridgeMaterialType.PILLAR);
			text += (string.IsNullOrEmpty(text) ? materialDisplayName6 : (", " + materialDisplayName6));
		}
		if (!string.IsNullOrEmpty(text))
		{
			return text;
		}
		return "None";
	}

	private static void UpdateRemainingMaterials()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int numActivePillarsNotLocked = BridgePillars.GetNumActivePillarsNotLocked();
		foreach (BridgeEdge edge in BridgeEdges.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsLocked())
			{
				switch (edge.m_Material.m_MaterialType)
				{
				case BridgeMaterialType.ROAD:
				case BridgeMaterialType.REINFORCED_ROAD:
					num++;
					break;
				case BridgeMaterialType.WOOD:
					num2++;
					break;
				case BridgeMaterialType.STEEL:
					num3++;
					break;
				case BridgeMaterialType.HYDRAULICS:
					num4++;
					break;
				case BridgeMaterialType.ROPE:
					num5++;
					break;
				case BridgeMaterialType.CABLE:
					num6++;
					break;
				case BridgeMaterialType.BUNGINE_ROPE:
					num7++;
					break;
				case BridgeMaterialType.SPRING:
					num8++;
					break;
				}
			}
		}
		m_RoadLeft = ((m_RoadBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_RoadBudget - num));
		m_WoodLeft = ((m_WoodBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_WoodBudget - num2));
		m_SteelLeft = ((m_SteelBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_SteelBudget - num3));
		m_HydraulicLeft = ((m_HydraulicBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_HydraulicBudget - num4));
		m_RopeLeft = ((m_RopeBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_RopeBudget - num5));
		m_CableLeft = ((m_CableBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_CableBudget - num6));
		m_BungieRopeLeft = ((m_BungieRopeBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_BungieRopeBudget - num7));
		m_SpringLeft = ((m_SpringBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_SpringBudget - num8));
		m_PillarLeft = ((m_PillarBudget == UNLIMITED_MATERIAL_BUDGET) ? UNLIMITED_MATERIAL_BUDGET : (m_PillarBudget - numActivePillarsNotLocked));
	}

	private static void ApplyForceUnlimitedBudget()
	{
		m_CashBudget = UNLIMITED_CASH_BUDGET;
	}

	private static void ApplyForceUnlimitedMaterial()
	{
		m_AllowWood = true;
		m_AllowSteel = true;
		m_AllowHydraulic = true;
		m_AllowRope = true;
		m_AllowCable = true;
		m_AllowSpring = true;
		m_AllowPillar = true;
		m_RoadBudget = UNLIMITED_MATERIAL_BUDGET;
		m_WoodBudget = UNLIMITED_MATERIAL_BUDGET;
		m_SteelBudget = UNLIMITED_MATERIAL_BUDGET;
		m_HydraulicBudget = UNLIMITED_MATERIAL_BUDGET;
		m_RopeBudget = UNLIMITED_MATERIAL_BUDGET;
		m_CableBudget = UNLIMITED_MATERIAL_BUDGET;
		m_BungieRopeBudget = UNLIMITED_MATERIAL_BUDGET;
		m_SpringBudget = UNLIMITED_MATERIAL_BUDGET;
		m_PillarBudget = UNLIMITED_MATERIAL_BUDGET;
	}
}
