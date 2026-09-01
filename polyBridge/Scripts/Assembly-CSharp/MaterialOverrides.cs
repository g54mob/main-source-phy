using Poly.Physics;

public class MaterialOverrides
{
	public float m_NodeMass;

	public float m_RoadBaseMass;

	public float m_RoadMassPerMeter;

	public float m_RoadStrength;

	public float m_ReinforcedRoadBaseMass;

	public float m_ReinforcedRoadMassPerMeter;

	public float m_ReinforcedRoadStrength;

	public float m_WoodBaseMass;

	public float m_WoodMassPerMeter;

	public float m_WoodStrength;

	public float m_SteelBaseMass;

	public float m_SteelMassPerMeter;

	public float m_SteelStrength;

	public float m_HydraulicsBaseMass;

	public float m_HydraulicsMassPerMeter;

	public float m_HydraulicsStrength;

	public float m_RopeBaseMass;

	public float m_RopeMassPerMeter;

	public float m_RopeStrength;

	public float m_CableBaseMass;

	public float m_CableMassPerMeter;

	public float m_CableStrength;

	public float m_SpringBaseMass;

	public float m_SpringMassPerMeter;

	public float m_SpringStrength;

	public static MaterialOverrides m_Instance;

	public static void Init()
	{
		InitFromDefaults();
	}

	public void Reload()
	{
		Init();
	}

	public float GetMaterialBaseMass(BridgeMaterialType bridgeMaterialType)
	{
		return bridgeMaterialType switch
		{
			BridgeMaterialType.CABLE => m_CableBaseMass, 
			BridgeMaterialType.HYDRAULICS => m_HydraulicsBaseMass, 
			BridgeMaterialType.REINFORCED_ROAD => m_ReinforcedRoadBaseMass, 
			BridgeMaterialType.ROAD => m_RoadBaseMass, 
			BridgeMaterialType.ROPE => m_RopeBaseMass, 
			BridgeMaterialType.SPRING => m_SpringBaseMass, 
			BridgeMaterialType.STEEL => m_SteelBaseMass, 
			BridgeMaterialType.WOOD => m_WoodBaseMass, 
			_ => m_WoodBaseMass, 
		};
	}

	public float GetMaterialMassPerMeter(BridgeMaterialType bridgeMaterialType)
	{
		return bridgeMaterialType switch
		{
			BridgeMaterialType.CABLE => m_CableMassPerMeter, 
			BridgeMaterialType.HYDRAULICS => m_HydraulicsMassPerMeter, 
			BridgeMaterialType.REINFORCED_ROAD => m_ReinforcedRoadMassPerMeter, 
			BridgeMaterialType.ROAD => m_RoadMassPerMeter, 
			BridgeMaterialType.ROPE => m_RopeMassPerMeter, 
			BridgeMaterialType.SPRING => m_SpringMassPerMeter, 
			BridgeMaterialType.STEEL => m_SteelMassPerMeter, 
			BridgeMaterialType.WOOD => m_WoodMassPerMeter, 
			_ => m_WoodMassPerMeter, 
		};
	}

	public float GetMaterialStrength(BridgeMaterialType bridgeMaterialType)
	{
		return bridgeMaterialType switch
		{
			BridgeMaterialType.CABLE => m_CableStrength, 
			BridgeMaterialType.HYDRAULICS => m_HydraulicsStrength, 
			BridgeMaterialType.REINFORCED_ROAD => m_ReinforcedRoadStrength, 
			BridgeMaterialType.ROAD => m_RoadStrength, 
			BridgeMaterialType.ROPE => m_RopeStrength, 
			BridgeMaterialType.SPRING => m_SpringStrength, 
			BridgeMaterialType.STEEL => m_SteelStrength, 
			BridgeMaterialType.WOOD => m_WoodStrength, 
			_ => m_WoodStrength, 
		};
	}

	private static void InitFromDefaults()
	{
		m_Instance = new MaterialOverrides();
		m_Instance.m_NodeMass = Prefabs.m_Instance.m_PhysicsNode.GetComponent<Node>().define.mass;
		m_Instance.m_RoadBaseMass = Prefabs.m_Instance.m_RoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_RoadMassPerMeter = Prefabs.m_Instance.m_RoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_RoadStrength = Prefabs.m_Instance.m_RoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_ReinforcedRoadBaseMass = Prefabs.m_Instance.m_ReinforcedRoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_ReinforcedRoadMassPerMeter = Prefabs.m_Instance.m_ReinforcedRoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_ReinforcedRoadStrength = Prefabs.m_Instance.m_ReinforcedRoadMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_WoodBaseMass = Prefabs.m_Instance.m_WoodMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_WoodMassPerMeter = Prefabs.m_Instance.m_WoodMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_WoodStrength = Prefabs.m_Instance.m_WoodMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_SteelBaseMass = Prefabs.m_Instance.m_SteelMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_SteelMassPerMeter = Prefabs.m_Instance.m_SteelMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_SteelStrength = Prefabs.m_Instance.m_SteelMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_HydraulicsBaseMass = Prefabs.m_Instance.m_HydraulicsMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_HydraulicsMassPerMeter = Prefabs.m_Instance.m_HydraulicsMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_HydraulicsStrength = Prefabs.m_Instance.m_HydraulicsMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_RopeBaseMass = Prefabs.m_Instance.m_RopeMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_RopeMassPerMeter = Prefabs.m_Instance.m_RopeMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_RopeStrength = Prefabs.m_Instance.m_RopeMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_CableBaseMass = Prefabs.m_Instance.m_CableMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_CableMassPerMeter = Prefabs.m_Instance.m_CableMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_CableStrength = Prefabs.m_Instance.m_CableMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
		m_Instance.m_SpringBaseMass = Prefabs.m_Instance.m_SpringMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.baseMass;
		m_Instance.m_SpringMassPerMeter = Prefabs.m_Instance.m_SpringMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.massPerMeter;
		m_Instance.m_SpringStrength = Prefabs.m_Instance.m_SpringMaterial.GetComponent<BridgeMaterial>().m_EdgeMaterial.strength;
	}
}
