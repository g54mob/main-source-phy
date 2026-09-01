using Poly.Base;
using Poly.Graphics;
using Poly.Physics;
using UnityEngine;

public class BridgePhysics
{
	public static float PgToKg = 4f;

	public static float KgToPg = 1f / PgToKg;

	public static readonly float NODE_MASS_KG = 0.25f;

	private static GameObject m_BridgePhysicsContainer;

	public static void Reset()
	{
		Main.m_Instance.m_World.autoPlay = false;
		Main.m_Instance.m_World.Clear();
	}

	public static void UpdateCurrentTime()
	{
		Main.m_Instance.m_World.UpdateCurrentFractionOfFixedFrame();
	}

	public static void StartSimulation()
	{
		Main.m_Instance.m_World.bounds = WorldBounds.m_Bounds;
		Main.m_Instance.m_World.bounds.Expand(20f);
		Main.m_Instance.m_World.areEdgesBreakable = !SandboxSettings.m_Unbreakable;
		Main.m_Instance.m_World.autoPlay = false;
		Main.m_Instance.m_World.splitNodePartPrefab = Prefabs.m_Instance.m_PhysicsNode.GetComponent<Node>();
	}

	public static void FixedUpdateManual()
	{
		Main.m_Instance.m_World.FixedUpdate_Manual();
		SingletonBehaviour<GpuInstancer>.instance?.ScanForNewEdges();
	}

	public static void AddNode(BridgeJoint bridgeJoint)
	{
		Node component = Object.Instantiate(Prefabs.m_Instance.m_PhysicsNode).GetComponent<Node>();
		if ((bool)component)
		{
			component.gameObject.name = "Node";
			component.define.isKinematic = bridgeJoint.m_IsAnchor;
			component.define.mass = MaterialOverrides.m_Instance.m_NodeMass;
			component.define.mass = Mathf.Max(component.define.mass, 1E-06f);
			component.shapeDefine.collisionGroup = ((!bridgeJoint.m_IsAnchor) ? CollisionGroup.Bridge : CollisionGroup.Fixed);
			component.shapeDefine.enableCollision = !bridgeJoint.m_IsAnchor;
			component.shapeDefine.layer = Layer.Bridge;
			component.transform.position = bridgeJoint.transform.position;
			component.transform.parent = GetBridgePhysicsContainerTransform();
			component.userData = bridgeJoint;
			component.willSplit = bridgeJoint.m_IsSplit;
			component.ReInit();
			component.handle.isAnchor = bridgeJoint.m_IsAnchor;
			bridgeJoint.SetPhysicsNode(component);
		}
	}

	public static void AddEdge(BridgeEdge bridgeEdge)
	{
		Edge component = Object.Instantiate(Prefabs.m_Instance.m_PhysicsEdge).GetComponent<Edge>();
		if ((bool)component)
		{
			Node physicsNode = bridgeEdge.m_JointA.GetPhysicsNode();
			Node physicsNode2 = bridgeEdge.m_JointB.GetPhysicsNode();
			component.transform.parent = physicsNode.transform.parent;
			component.gameObject.name = "Edge";
			component.material = bridgeEdge.m_Material.m_EdgeMaterial;
			component.material.baseMass = MaterialOverrides.m_Instance.GetMaterialBaseMass(bridgeEdge.m_Material.m_MaterialType);
			component.material.massPerMeter = MaterialOverrides.m_Instance.GetMaterialMassPerMeter(bridgeEdge.m_Material.m_MaterialType);
			component.material.strength = MaterialOverrides.m_Instance.GetMaterialStrength(bridgeEdge.m_Material.m_MaterialType);
			component.collisionGroup = CollisionGroup.Bridge;
			component.layer = Layer.Bridge;
			component.userData = bridgeEdge;
			if (bridgeEdge.m_Material.m_MaterialType == BridgeMaterialType.HYDRAULICS)
			{
				AddHydraulics(bridgeEdge, component);
			}
			if (component.material.isRope)
			{
				AddRope(bridgeEdge, component);
			}
			component.InitBeforeStart(physicsNode, physicsNode2);
			component.SetPartOnNode_Once(physicsNode, (bridgeEdge.m_JointA.m_IsSplit && bridgeEdge.m_JointAPart != SplitJointPart.A) ? ((bridgeEdge.m_JointAPart == SplitJointPart.B) ? Part.B : Part.C) : Part.A);
			component.SetPartOnNode_Once(physicsNode2, (bridgeEdge.m_JointB.m_IsSplit && bridgeEdge.m_JointBPart != SplitJointPart.A) ? ((bridgeEdge.m_JointBPart == SplitJointPart.B) ? Part.B : Part.C) : Part.A);
			if (component.material.isSpring)
			{
				component.freeLengthOverride = Vector3.Distance(physicsNode.transform.position, physicsNode2.transform.position) * bridgeEdge.m_SpringCoilVisualization.m_FreeLengthOverrideMultiplier;
			}
			component.excludeFromMaxStressCalculation = bridgeEdge.m_ExcludeFromMaxStressCalculation;
			bridgeEdge.m_PhysicsEdge = component;
		}
	}

	private static void AddHydraulics(BridgeEdge bridgeEdge, Edge edge)
	{
		Piston pistonOnEdge = Pistons.GetPistonOnEdge(bridgeEdge);
		if ((bool)pistonOnEdge)
		{
			edge.enableHydraulics = true;
			edge.hydraulicsDefine.maxSpeed = Pistons.MAX_SPEED;
			edge.hydraulicsDefine.acceleration = Pistons.MAX_ACCELERATION;
			edge.hydraulicsDefine.targetLengthFractionDelta = (float)Mathf.RoundToInt((pistonOnEdge.GetTargetLengthScale() - 1f) * 100f) / 100f;
		}
	}

	private static void AddRope(BridgeEdge bridgeEdge, Edge edge)
	{
		Object.Instantiate(Prefabs.m_Instance.m_PhysicsRope, edge.transform);
		bridgeEdge.m_MeshRenderer.gameObject.SetActive(value: false);
	}

	private static Transform GetBridgePhysicsContainerTransform()
	{
		if (!m_BridgePhysicsContainer)
		{
			m_BridgePhysicsContainer = new GameObject("SimBridge");
		}
		return m_BridgePhysicsContainer.transform;
	}
}
