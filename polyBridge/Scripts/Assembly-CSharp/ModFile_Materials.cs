public class ModFile_Materials
{
	public float m_RoadStrength;

	public float m_RoadCost;

	public float m_RoadMaxLength;

	public float m_ReinforcedRoadStrength;

	public float m_ReinforcedRoadCost;

	public float m_ReinforcedRoadMaxLength;

	public float m_WoodStrength;

	public float m_WoodCost;

	public float m_WoodMaxLength;

	public float m_SteelStrength;

	public float m_SteelCost;

	public float m_SteelMaxLength;

	public float m_RopeStrength;

	public float m_RopeCost;

	public float m_RopeMaxLength;

	public float m_CableStrength;

	public float m_CableCost;

	public float m_CableMaxLength;

	public float m_HydraulicsStrength;

	public float m_HydraulicsCost;

	public float m_HydraulicsMaxLength;

	public float m_SpringStrength;

	public float m_SpringCost;

	public float m_SpringMaxLength;

	public void ResetToDefaults()
	{
		m_RoadStrength = Prefabs.m_Instance.m_RoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_RoadCost = Prefabs.m_Instance.m_RoadMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_RoadMaxLength = Prefabs.m_Instance.m_RoadMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_ReinforcedRoadStrength = Prefabs.m_Instance.m_ReinforcedRoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_ReinforcedRoadCost = Prefabs.m_Instance.m_ReinforcedRoadMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_ReinforcedRoadMaxLength = Prefabs.m_Instance.m_ReinforcedRoadMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_WoodStrength = Prefabs.m_Instance.m_WoodMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_WoodCost = Prefabs.m_Instance.m_WoodMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_WoodMaxLength = Prefabs.m_Instance.m_WoodMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_SteelStrength = Prefabs.m_Instance.m_SteelMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_SteelCost = Prefabs.m_Instance.m_SteelMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_SteelMaxLength = Prefabs.m_Instance.m_SteelMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_RopeStrength = Prefabs.m_Instance.m_RopeMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_RopeCost = Prefabs.m_Instance.m_RopeMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_RopeMaxLength = Prefabs.m_Instance.m_RopeMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_CableStrength = Prefabs.m_Instance.m_CableMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_CableCost = Prefabs.m_Instance.m_CableMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_CableMaxLength = Prefabs.m_Instance.m_CableMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_HydraulicsStrength = Prefabs.m_Instance.m_HydraulicsMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_HydraulicsCost = Prefabs.m_Instance.m_HydraulicsMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_HydraulicsMaxLength = Prefabs.m_Instance.m_HydraulicsMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
		m_SpringStrength = Prefabs.m_Instance.m_SpringMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_SpringCost = Prefabs.m_Instance.m_SpringMaterial.GetComponent<BridgeMaterial>().m_PricePerMeter;
		m_SpringMaxLength = Prefabs.m_Instance.m_SpringMaterial.GetComponent<BridgeMaterial>().m_MaxLength;
	}
}
