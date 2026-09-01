using System.Collections.Generic;
using UnityEngine;

public class BridgeMaterials
{
	public static Dictionary<BridgeMaterialType, BridgeMaterial> m_BridgeMaterials = new Dictionary<BridgeMaterialType, BridgeMaterial>();

	public static float WOOD_REFERENCE_LENGTH = 2f;

	private static GameObject m_MaterialsParent;

	private static BridgeMaterial m_RoadMaterial;

	private static BridgeMaterial m_ReinforcedRoadMaterial;

	private static BridgeMaterial m_WoodMaterial;

	private static BridgeMaterial m_SteelMaterial;

	private static BridgeMaterial m_HydraulicsMaterial;

	private static BridgeMaterial m_RopeMaterial;

	private static BridgeMaterial m_CableMaterial;

	private static BridgeMaterial m_SpringMaterial;

	private static BridgeMaterial m_PillarMaterial;

	private static ModFile_Materials m_ModFileDefaults;

	public static string GetMaterialDisplayName(BridgeMaterialType materialType)
	{
		return materialType switch
		{
			BridgeMaterialType.CABLE => "Cable", 
			BridgeMaterialType.HYDRAULICS => "Hydraulics", 
			BridgeMaterialType.REINFORCED_ROAD => "Reinforced Road", 
			BridgeMaterialType.ROAD => "Road", 
			BridgeMaterialType.ROPE => "Rope", 
			BridgeMaterialType.SPRING => "Spring", 
			BridgeMaterialType.PILLAR => "Foundation", 
			BridgeMaterialType.STEEL => "Steel", 
			BridgeMaterialType.WOOD => "Wood", 
			_ => "Unknown Material", 
		};
	}

	public static string GetLocalizedMaterialDisplayName(BridgeMaterialType materialType)
	{
		return materialType switch
		{
			BridgeMaterialType.CABLE => Localize.Get("MATERIAL_CABLE"), 
			BridgeMaterialType.HYDRAULICS => Localize.Get("MATERIAL_HYDRAULIC"), 
			BridgeMaterialType.REINFORCED_ROAD => Localize.Get("MATERIAL_REINFORCED_ROAD"), 
			BridgeMaterialType.ROAD => Localize.Get("MATERIAL_ROAD"), 
			BridgeMaterialType.ROPE => Localize.Get("MATERIAL_ROPE"), 
			BridgeMaterialType.SPRING => Localize.Get("MATERIAL_SPRING"), 
			BridgeMaterialType.PILLAR => Localize.Get("MATERIAL_PILLAR"), 
			BridgeMaterialType.STEEL => Localize.Get("MATERIAL_STEEL"), 
			BridgeMaterialType.WOOD => Localize.Get("MATERIAL_WOOD"), 
			_ => string.Empty, 
		};
	}

	public static void Init()
	{
		m_MaterialsParent = new GameObject("Bridge Materials");
		Object.DontDestroyOnLoad(m_MaterialsParent);
		m_RoadMaterial = CreateMaterial(Prefabs.m_Instance.m_RoadMaterial);
		m_ReinforcedRoadMaterial = CreateMaterial(Prefabs.m_Instance.m_ReinforcedRoadMaterial);
		m_WoodMaterial = CreateMaterial(Prefabs.m_Instance.m_WoodMaterial);
		m_SteelMaterial = CreateMaterial(Prefabs.m_Instance.m_SteelMaterial);
		m_HydraulicsMaterial = CreateMaterial(Prefabs.m_Instance.m_HydraulicsMaterial);
		m_RopeMaterial = CreateMaterial(Prefabs.m_Instance.m_RopeMaterial);
		m_CableMaterial = CreateMaterial(Prefabs.m_Instance.m_CableMaterial);
		m_SpringMaterial = CreateMaterial(Prefabs.m_Instance.m_SpringMaterial);
		m_PillarMaterial = CreateMaterial(Prefabs.m_Instance.m_PillarMaterial);
		m_ModFileDefaults = new ModFile_Materials();
		m_ModFileDefaults.ResetToDefaults();
	}

	public static BridgeMaterial GetBridgeMaterial(BridgeMaterialType materialType)
	{
		if (!m_BridgeMaterials.ContainsKey(materialType))
		{
			Debug.LogWarningFormat("GetBridgeMaterial(): Trying to look up a material that isn't loaded: {0}", materialType.ToString());
			return null;
		}
		return m_BridgeMaterials[materialType];
	}

	public static Material GetMaterial(BridgeMaterialType materialType)
	{
		BridgeMaterial bridgeMaterial = GetBridgeMaterial(materialType);
		if (!(bridgeMaterial != null))
		{
			return null;
		}
		return bridgeMaterial.m_Material;
	}

	public static GameObject GetLinkPrefabFromMaterial(BridgeMaterialType materialType)
	{
		return materialType switch
		{
			BridgeMaterialType.ROPE => Prefabs.m_Instance.m_RopeLink, 
			BridgeMaterialType.CABLE => Prefabs.m_Instance.m_CableLink, 
			BridgeMaterialType.SPRING => Prefabs.m_Instance.m_SpringCoilLink, 
			_ => Prefabs.m_Instance.m_ErrorLink, 
		};
	}

	public static float GetMaxEdgeLength(BridgeMaterialType materialType)
	{
		if (!m_BridgeMaterials.ContainsKey(materialType))
		{
			Debug.LogWarningFormat("GetMaxEdgeLength(): Trying to look up a material that isn't loaded: {0}", materialType.ToString());
			return 0f;
		}
		return m_BridgeMaterials[materialType].m_MaxLength;
	}

	public static bool IsRoadMaterial(BridgeMaterialType materialType)
	{
		if (materialType != BridgeMaterialType.ROAD)
		{
			return materialType == BridgeMaterialType.REINFORCED_ROAD;
		}
		return true;
	}

	public static string LocalizeResources(string rawText)
	{
		return rawText.Replace("Road", Localize.Get("MATERIAL_ROAD")).Replace("Reinforced Road", Localize.Get("MATERIAL_REINFORCED_ROAD")).Replace("Wood", Localize.Get("MATERIAL_WOOD"))
			.Replace("Steel", Localize.Get("MATERIAL_STEEL"))
			.Replace("Hydraulic", Localize.Get("MATERIAL_HYDRAULIC"))
			.Replace("Rope", Localize.Get("MATERIAL_ROPE"))
			.Replace("Cable", Localize.Get("MATERIAL_CABLE"))
			.Replace("Spring", Localize.Get("MATERIAL_SPRING"))
			.Replace("Concrete Pillar", Localize.Get("MATERIAL_PILLAR"));
	}

	public static void UpdateActiveModFile(ModFile_Materials modFile)
	{
		if (modFile == null)
		{
			UpdateMaterialsFromModFile(m_ModFileDefaults);
		}
		else
		{
			UpdateMaterialsFromModFile(modFile);
		}
	}

	public static float GetRoadCollisionOffset()
	{
		return GetBridgeMaterial(BridgeMaterialType.ROAD).m_EdgeMaterial.collisionRadius + 0.0004f;
	}

	private static BridgeMaterial CreateMaterial(GameObject prefab)
	{
		GameObject gameObject = Object.Instantiate(prefab);
		if (gameObject == null)
		{
			return null;
		}
		BridgeMaterial component = gameObject.GetComponent<BridgeMaterial>();
		if (component == null)
		{
			return null;
		}
		component.name = prefab.name;
		component.transform.parent = m_MaterialsParent.transform;
		m_BridgeMaterials.Add(component.m_MaterialType, component);
		return component;
	}

	private static void UpdateMaterialsFromModFile(ModFile_Materials modFile)
	{
		MaterialOverrides.m_Instance.m_RoadStrength = modFile.m_RoadStrength;
		MaterialOverrides.m_Instance.m_ReinforcedRoadStrength = modFile.m_ReinforcedRoadStrength;
		MaterialOverrides.m_Instance.m_WoodStrength = modFile.m_WoodStrength;
		MaterialOverrides.m_Instance.m_SteelStrength = modFile.m_SteelStrength;
		MaterialOverrides.m_Instance.m_RopeStrength = modFile.m_RopeStrength;
		MaterialOverrides.m_Instance.m_CableStrength = modFile.m_CableStrength;
		MaterialOverrides.m_Instance.m_HydraulicsStrength = modFile.m_HydraulicsStrength;
		MaterialOverrides.m_Instance.m_SpringStrength = modFile.m_SpringStrength;
		m_RoadMaterial.m_PricePerMeter = modFile.m_RoadCost;
		m_ReinforcedRoadMaterial.m_PricePerMeter = modFile.m_ReinforcedRoadCost;
		m_WoodMaterial.m_PricePerMeter = modFile.m_WoodCost;
		m_SteelMaterial.m_PricePerMeter = modFile.m_SteelCost;
		m_RopeMaterial.m_PricePerMeter = modFile.m_RopeCost;
		m_CableMaterial.m_PricePerMeter = modFile.m_CableCost;
		m_HydraulicsMaterial.m_PricePerMeter = modFile.m_HydraulicsCost;
		m_SpringMaterial.m_PricePerMeter = modFile.m_SpringCost;
	}
}
