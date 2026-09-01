using System.Collections.Generic;
using Poly;
using Poly.Base;
using Poly.Collide;
using Poly.Graphics;
using Poly.Physics;
using UnityEngine;

public class BridgeEdges
{
	public static float DEFAULT_SPRINGS_DISAPPEAR_ORTHOGRAPHIC_SIZE = 50f;

	public static float ERROR_HIGHLIGHT_SECONDS = 0.5f;

	public static List<BridgeEdge> m_Edges = new List<BridgeEdge>();

	public static Dictionary<string, string> m_BridgeEdgeColorsPermanent = new Dictionary<string, string>();

	public static string STRESS_COLOR_SHADER_ID = "_StressColor";

	public static string DESATURATE_SHADER_ID = "_Desaturate";

	public static float m_SpringsDisappearOrthographicSize = DEFAULT_SPRINGS_DISAPPEAR_ORTHOGRAPHIC_SIZE;

	private static Dictionary<string, BridgeEdge> m_EdgeDictionary = new Dictionary<string, BridgeEdge>();

	private static GameObject m_EdgesContainer;

	private static bool m_StressViewWasEnabled;

	private static readonly int MAX_RAYCAST_HITS = 32;

	private static RaycastHit2D[] m_RaycastHitsBuffer = new RaycastHit2D[MAX_RAYCAST_HITS];

	public static void DestroyAll()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			edge.Destroy();
		}
		m_Edges.Clear();
		m_EdgeDictionary.Clear();
		m_BridgeEdgeColorsPermanent.Clear();
	}

	public static BridgeEdge FindByGuid(string guid)
	{
		if (string.IsNullOrEmpty(guid))
		{
			return null;
		}
		if (!m_EdgeDictionary.ContainsKey(guid))
		{
			return null;
		}
		return m_EdgeDictionary[guid];
	}

	public static Dictionary<string, BridgeEdge> GetEdgeDictionary()
	{
		return m_EdgeDictionary;
	}

	public static void RemoveFromDictionary(BridgeEdge edge)
	{
		if (edge.m_Guid != null && m_EdgeDictionary.ContainsKey(edge.m_Guid) && m_EdgeDictionary[edge.m_Guid] == edge)
		{
			m_EdgeDictionary.Remove(edge.m_Guid);
			if (m_BridgeEdgeColorsPermanent.ContainsKey(edge.m_Guid))
			{
				m_BridgeEdgeColorsPermanent.Remove(edge.m_Guid);
			}
		}
	}

	public static void UpdateStressColor()
	{
		if (Profiles.m_ActiveProfile.m_StressViewEnabled && GameStateManager.GetState() == GameState.SIM)
		{
			SetStressColor();
			m_StressViewWasEnabled = true;
		}
		else if (m_StressViewWasEnabled)
		{
			SetOriginalColor();
			m_StressViewWasEnabled = false;
		}
	}

	public static void UpdateManual()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			edge.UpdateManual();
		}
	}

	public static void UpdateManualOutsideSim()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				edge.UpdateManualOutsideSim();
			}
		}
		HighlightEdgesWhenMouseOverJointSelector();
	}

	public static void UpdateTransforms()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				edge._UpdateTransform();
			}
		}
	}

	private static List<BridgeEdge> GetNotInstancedEdges()
	{
		if (!GpuInstancer.isActivatedAndInstancing)
		{
			return m_Edges;
		}
		return SingletonBehaviour<GpuInstancer>.instance.notInstancedEdges;
	}

	public static void UpdateTransforms_InSimulation()
	{
		foreach (BridgeEdge notInstancedEdge in GetNotInstancedEdges())
		{
			if (!notInstancedEdge.m_ForceDisabled)
			{
				notInstancedEdge._UpdateTransform();
			}
		}
	}

	public static void ClampJointSelectorsToTwoWay()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				edge.ClampJointSelectorsToTwoWay();
			}
		}
	}

	public static List<BridgeEdgeProxy> Serialize()
	{
		List<BridgeEdgeProxy> list = new List<BridgeEdgeProxy>();
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				list.Add(new BridgeEdgeProxy(edge));
			}
		}
		return list;
	}

	public static void Deserialize(List<BridgeEdgeProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (BridgeEdgeProxy proxy in proxies)
		{
			CreateEdgeFromProxy(proxy);
		}
	}

	public static BridgeEdge CreateEdgeFromProxy(BridgeEdgeProxy proxy)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(proxy.m_NodeA_Guid);
		BridgeJoint bridgeJoint2 = BridgeJoints.FindByGuid(proxy.m_NodeB_Guid);
		if ((bool)bridgeJoint && (bool)bridgeJoint2)
		{
			BridgeEdge bridgeEdge = CreateEdge(bridgeJoint, bridgeJoint2, proxy.m_Material, string.IsNullOrEmpty(proxy.m_Guid) ? Utils.GenerateUniqueId() : proxy.m_Guid, null);
			if ((bool)bridgeEdge)
			{
				bridgeEdge.m_JointAPart = proxy.m_JointAPart;
				bridgeEdge.m_JointBPart = proxy.m_JointBPart;
				bridgeEdge.RefreshJointSelectorNumbers();
				bridgeEdge.SetPrebuiltState(proxy.m_BridgePrebuiltState);
				if (!string.IsNullOrEmpty(bridgeEdge.m_Guid) && m_BridgeEdgeColorsPermanent.ContainsKey(bridgeEdge.m_Guid))
				{
					bridgeEdge.SetOverrideColorPermanent(m_BridgeEdgeColorsPermanent[bridgeEdge.m_Guid]);
				}
				return bridgeEdge;
			}
		}
		return null;
	}

	public static HashSet<BridgeEdge> CollectEdgesInRect(Rect rect)
	{
		Vector3 center = new Vector3(rect.center.x, rect.center.y, 0f);
		Vector3 halfExtents = new Vector3(rect.width / 2f, rect.height / 2f, 10f);
		Collider[] array = Physics.OverlapBox(center, halfExtents, Quaternion.identity, Utils.EDGE_LAYER_MASK);
		HashSet<BridgeEdge> hashSet = new HashSet<BridgeEdge>();
		Collider[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			BridgeEdge component = array2[i].transform.parent.GetComponent<BridgeEdge>();
			if ((bool)component)
			{
				hashSet.Add(component);
			}
		}
		return hashSet;
	}

	public static BridgeEdge FindByPhysicsEdge(Edge physicsEdge)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && edge.m_PhysicsEdge == physicsEdge)
			{
				return edge;
			}
		}
		return null;
	}

	public static BridgeEdge FindEnabledEdgeByJointGuids(string A, string B, BridgeMaterialType materialType)
	{
		BridgeJoint a = BridgeJoints.FindByGuid(A);
		BridgeJoint b = BridgeJoints.FindByGuid(B);
		return FindEnabledEdgeByJoints(a, b, materialType);
	}

	public static BridgeEdge FindDisabledEdgeByJointGuids(string A, string B, BridgeMaterialType materialType)
	{
		BridgeJoint a = BridgeJoints.FindByGuid(A);
		BridgeJoint b = BridgeJoints.FindByGuid(B);
		return FindDisabledEdgeByJoints(a, b, materialType);
	}

	public static BridgeEdge FindEnabledEdgeByJoints(BridgeJoint A, BridgeJoint B, BridgeMaterialType materialType)
	{
		if (!A || !B)
		{
			return null;
		}
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && edge.m_Material.m_MaterialType == materialType && edge.MatchesJoints(A, B))
			{
				return edge;
			}
		}
		return null;
	}

	public static BridgeEdge FindDisabledEdgeByJoints(BridgeJoint A, BridgeJoint B, BridgeMaterialType materialType)
	{
		if (!A || !B)
		{
			return null;
		}
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!edge.gameObject.activeInHierarchy && edge.m_Material.m_MaterialType == materialType && edge.MatchesJoints(A, B))
			{
				return edge;
			}
		}
		return null;
	}

	public static BridgeEdge GetEdgeFromJoints(BridgeJoint jointA, BridgeJoint jointB)
	{
		if (!jointA || !jointB)
		{
			return null;
		}
		return jointA.GetEdgeConnectingTo(jointB);
	}

	public static BridgeEdge CreateEdge(BridgeJoint jointA, BridgeJoint jointB, BridgeMaterialType materialType, string guid, Edge physicsEdge_onlyUsedWhenBreakingEdgesInSimulation)
	{
		GameObject gameObject = Object.Instantiate(Bridge.GetPrefabFromBridgeMaterial(materialType));
		if (!gameObject)
		{
			return null;
		}
		BridgeEdge component = gameObject.GetComponent<BridgeEdge>();
		if (!component)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(guid))
		{
			component.m_Guid = guid;
			m_EdgeDictionary.Add(guid, component);
		}
		component.transform.parent = GetEdgesContainerTransform();
		component.m_JointA = jointA;
		component.m_JointB = jointB;
		component.m_StartSimJointA = jointA;
		component.m_StartSimJointB = jointB;
		component.m_Material = BridgeMaterials.GetBridgeMaterial(materialType);
		component.UpdateTransform();
		component.m_JointAPart = component.CalculateJointPart(component.m_JointA);
		component.m_JointBPart = component.CalculateJointPart(component.m_JointB);
		component.m_PhysicsEdge = physicsEdge_onlyUsedWhenBreakingEdgesInSimulation;
		component.MatchTilingWithLength(materialType, Vector3.Distance(jointA.transform.position, jointB.transform.position));
		component.MaybeSetRopeCableTiling();
		if (!Bridge.IsSimulating())
		{
			component.MaybeCreateJointSelectors();
		}
		jointA.RegisterEdgeInCache(component);
		jointB.RegisterEdgeInCache(component);
		return component;
	}

	public static BridgeEdge CreateEdgeWithPistonOrSpring(BridgeJoint jointA, BridgeJoint jointB, BridgeMaterialType materialType)
	{
		BridgeEdge bridgeEdge = CreateEdge(jointA, jointB, materialType, Utils.GenerateUniqueId(), null);
		if ((bool)bridgeEdge && bridgeEdge.IsPiston())
		{
			Piston piston = Pistons.CreatePiston(bridgeEdge.m_JointA, bridgeEdge.m_JointB, 0f, Utils.GenerateUniqueId());
			if ((bool)piston)
			{
				HydraulicsController.AddPistonToAllPhasesAcceptingNewAdditions(piston);
			}
		}
		if ((bool)bridgeEdge && bridgeEdge.IsSpring())
		{
			BridgeSprings.CreateSpring(bridgeEdge, 0.5f, Utils.GenerateUniqueId());
		}
		if (Game.IsCurrentLevelTutorial())
		{
			CampaignTutorial.CreatedNewEdge(jointA, jointB, materialType);
		}
		return bridgeEdge;
	}

	public static void EnableJointCaps()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				bool num = edge.ShouldShowJointCaps();
				if (num || edge.m_JointA.m_IsAnchor)
				{
					edge.m_JointA.m_Cap.SetActive(value: true);
				}
				if (num || edge.m_JointB.m_IsAnchor)
				{
					edge.m_JointB.m_Cap.SetActive(value: true);
				}
			}
		}
	}

	public static void DestroyInactiveEdges()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!edge.gameObject.activeInHierarchy)
			{
				Object.Destroy(edge.gameObject);
			}
		}
	}

	public static void DestroyAllExceptPrebuilt()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!edge.IsPrebuilt())
			{
				edge.gameObject.SetActive(value: false);
				Object.Destroy(edge.gameObject);
			}
		}
	}

	public static void ForceDisableAllExceptPrebuilt()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!edge.IsPrebuilt())
			{
				edge.ForceDisable();
			}
		}
	}

	public static void UndoForceDisabled()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (!edge.IsPrebuilt() && edge.m_ForceDisabled)
			{
				edge.ForceEnable();
			}
		}
	}

	public static void DestroyEdgesConnectedToJoint(BridgeJoint joint)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.m_JointA == joint || edge.m_JointB == joint)
			{
				Object.Destroy(edge.gameObject);
			}
		}
	}

	public static void AddToSimulation()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				BridgePhysics.AddEdge(edge);
			}
		}
	}

	public static bool EdgeIsConnectedToJoint(BridgeJoint joint)
	{
		return 0 < joint.GetNumConnectedEdges();
	}

	public static List<BridgeEdge> GetEdgesConnectedToJoint(BridgeJoint joint)
	{
		return joint.GetConnectedEdgesCopy();
	}

	public static bool LockedEdgesAreConnectedToJoint(BridgeJoint joint)
	{
		foreach (BridgeEdge item in joint.GetConnectedEdgesCopy())
		{
			if (item.IsLocked())
			{
				return true;
			}
		}
		return false;
	}

	public static void DisplayLockIconForLockedEdgesConnectedToJoint(BridgeJoint joint, float seconds)
	{
		foreach (BridgeEdge item in joint.GetConnectedEdgesCopy())
		{
			if (item.IsLocked())
			{
				item.ShowLockIconForSeconds(seconds);
			}
		}
	}

	public static int GetNumEdgesConnectedToJoint(BridgeJoint joint)
	{
		return joint.GetNumConnectedEdges();
	}

	public static bool LineSegmentCrossesSolidEdge(Vector3 start, Vector3 end, BridgeJoint fromJoint, BridgeJoint toJoint)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !(edge.m_JointA == fromJoint) && !(edge.m_JointB == fromJoint) && !(edge.m_JointA == toJoint) && !(edge.m_JointB == toJoint) && edge.m_Material.m_MaterialType != BridgeMaterialType.ROPE && edge.m_Material.m_MaterialType != BridgeMaterialType.CABLE && Utils.LineIntersectsLine(start, end, edge.m_JointA.transform.position, edge.m_JointB.transform.position))
			{
				return true;
			}
		}
		return false;
	}

	public static bool CanFormEdgeBetweenJoints(BridgeEdge existingEdge, BridgeJoint A, BridgeJoint B, BridgeMaterialType materialType)
	{
		if (!A || !B)
		{
			return false;
		}
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (!Game.InSandboxGodMode() && (A.m_NoBuild || B.m_NoBuild))
		{
			return false;
		}
		if ((bool)existingEdge && existingEdge.IsLocked() && !Game.InSandboxGodMode())
		{
			return false;
		}
		if (!BuildZones.ContainsEdge(A.transform.position, B.transform.position))
		{
			return false;
		}
		if (!Budget.HasMaterialLeft(materialType))
		{
			return BridgeJointPlacement.RoadMaterialWillOverdrawRoadMaterial(A, B, Bridge.m_BuildMaterialType);
		}
		float length = Vector3.Distance(A.transform.position, B.transform.position);
		if (!Budget.CanAffordEdge(length, materialType))
		{
			return false;
		}
		BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(materialType);
		if (bridgeMaterial != null && EdgeLocationOverlapsBlockingPolygonShape(A.transform.position, B.transform.position, materialType, bridgeMaterial.m_EdgeMaterial.collisionRadius))
		{
			return false;
		}
		if (A.HasMaxEdges() || B.HasMaxEdges())
		{
			return false;
		}
		if (Game.IsCurrentLevelTutorial() && BridgeJointPlacement.WoodMaterialWillOverdrawRoadMaterial(A, B, Bridge.m_BuildMaterialType))
		{
			return false;
		}
		return IsValidEdgeLength(length, GameSettings.NodeDiameter(), BridgeMaterials.GetMaxEdgeLength(materialType));
	}

	public static bool IsValidEdgeLength(float length, float min, float max)
	{
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (Utils.ApproximatelyEquals(length, max))
		{
			return true;
		}
		if (Utils.ApproximatelyEquals(length, min))
		{
			return true;
		}
		if (length <= max)
		{
			return length >= min;
		}
		return false;
	}

	public static void HideJointSelectorUI()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if ((bool)edge.m_JointSelectorA)
			{
				edge.m_JointSelectorA.gameObject.SetActive(value: false);
			}
			if ((bool)edge.m_JointSelectorB)
			{
				edge.m_JointSelectorB.gameObject.SetActive(value: false);
			}
		}
	}

	public static void DebugDisplayStrengths()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && (bool)edge.m_PhysicsEdge)
			{
				Debug.LogFormat("{0} : {1}", edge.name, edge.m_PhysicsEdge.material.strength);
			}
		}
	}

	public static void SetDefaultColors()
	{
		if (Profiles.m_ActiveProfile.m_StressViewEnabled && GameStateManager.GetState() == GameState.SIM)
		{
			foreach (BridgeEdge notInstancedEdge in GetNotInstancedEdges())
			{
				notInstancedEdge.SetStressColor(0.01f);
			}
			return;
		}
		foreach (BridgeEdge notInstancedEdge2 in GetNotInstancedEdges())
		{
			notInstancedEdge2.SetStressColor(0f);
		}
	}

	public static void SetOriginalColor()
	{
		foreach (BridgeEdge notInstancedEdge in GetNotInstancedEdges())
		{
			notInstancedEdge.SetStressColor(0f);
		}
	}

	public static void InitFX()
	{
		bool flag = GameStateManager.GetState() == GameState.SANDBOX;
		bool flag2 = GameStateManager.GetState() == GameState.SANDBOX;
		foreach (BridgeEdge edge in m_Edges)
		{
			edge.SetStressColor(0f);
			edge.m_LockFX.SetActive(edge.IsLocked() && flag);
			edge.m_SoftLockFX.SetActive(edge.IsSoftLocked() && flag2);
			edge.UnHighlight();
		}
	}

	public static bool EdgeLocationOverlapsBlockingPolygonShape(Vector3 A, Vector3 B, BridgeMaterialType materialType, float collisionRadius)
	{
		if (Vector3.Distance(A, B) < GameSettings.NodeDiameter())
		{
			return false;
		}
		PolygonShape polygonShape = PolygonShape.FromSegment((Vec2)A, (Vec2)B, collisionRadius);
		bool flag = BridgeMaterials.IsRoadMaterial(materialType);
		if (flag && Vehicles.OverlapsPolygonShape(polygonShape))
		{
			return true;
		}
		if (flag && CustomShapes.OverlapsPolygonShapeBlockingRoad(polygonShape))
		{
			return true;
		}
		return false;
	}

	public static bool AtLeastOneActiveEdge()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}

	public static int GetNumActiveEdges()
	{
		int num = 0;
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		return num;
	}

	public static void SetHydraulicsSleeveColor(Color c)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.m_HydraulicEdgeVisualization != null)
			{
				edge.m_HydraulicEdgeVisualization.SetColor(c);
			}
		}
	}

	public static void SetHydraulicsPistonColor(Color c)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.m_HydraulicEdgeVisualization != null)
			{
				edge.m_MeshRenderer.material.color = c;
			}
		}
	}

	public static void SetSpringColorCoil(Color c)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.m_Material.m_MaterialType == BridgeMaterialType.SPRING && edge.m_SpringCoilVisualization != null)
			{
				edge.m_SpringCoilVisualization.m_FrontLink.m_MeshRenderer.material.color = c;
				edge.m_SpringCoilVisualization.m_BackLink.m_MeshRenderer.material.color = c;
			}
		}
	}

	public static void SetSpringColorInterior(Color c)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.m_Material.m_MaterialType == BridgeMaterialType.SPRING)
			{
				edge.m_MeshRenderer.material.color = c;
			}
		}
	}

	public static BridgeEdge GetEdgeUnderRay(Ray ray)
	{
		BridgeEdge result = null;
		if (Physics.Raycast(ray, out var hitInfo, float.MaxValue, Utils.EDGE_LAYER_MASK))
		{
			result = hitInfo.transform.parent.GetComponent<BridgeEdge>();
		}
		return result;
	}

	public static bool EdgesAreDuplicated(BridgeEdge a, BridgeEdge b)
	{
		if (a.m_JointA == b.m_JointA && a.m_JointB == b.m_JointB)
		{
			return true;
		}
		if (a.m_JointA == b.m_JointB && a.m_JointB == b.m_JointA)
		{
			return true;
		}
		return false;
	}

	public static void ForceStressVisualizationRefresh()
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			edge.ForceStressVisualizationRefresh();
		}
	}

	public static void MarkPrebuiltEdgesToExcludeFromMaxStressCalculation()
	{
		if (BuildZones.m_BuildZones.Count == 0)
		{
			return;
		}
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.IsLocked())
			{
				edge.m_ExcludeFromMaxStressCalculation = !BuildZones.ContainsPoint(edge.m_JointA.transform.position) && !BuildZones.ContainsPoint(edge.m_JointB.transform.position);
			}
		}
	}

	public static BridgeEdge GetLockIconUnderMouseSkipJointSelectorCheck()
	{
		int rayIntersectionNonAlloc = Physics2D.GetRayIntersectionNonAlloc(Cameras.MainCamera().ScreenPointToRay(GameInput.GetMousePosition()), m_RaycastHitsBuffer, float.PositiveInfinity, Utils.EDGE_LAYER_MASK);
		for (int i = 0; i < rayIntersectionNonAlloc; i++)
		{
			RaycastHit2D raycastHit2D = m_RaycastHitsBuffer[i];
			if (raycastHit2D.collider != null)
			{
				SpriteRenderer component = raycastHit2D.collider.GetComponent<SpriteRenderer>();
				if ((bool)component && component.gameObject.activeInHierarchy)
				{
					return raycastHit2D.collider.transform.parent.transform.parent.gameObject.GetComponent<BridgeEdge>();
				}
			}
		}
		return null;
	}

	public static float Cost()
	{
		float num = 0f;
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && !edge.IsLocked())
			{
				num += edge.m_Material.m_PricePerMeter * edge.GetLength();
			}
		}
		return num;
	}

	public static float Mass()
	{
		float num = 0f;
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				num += edge.Mass();
			}
		}
		return num;
	}

	public static float Mass(HashSet<BridgeEdge> edges)
	{
		float num = 0f;
		foreach (BridgeEdge edge in edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				num += edge.Mass();
			}
		}
		return num;
	}

	public static BridgeEdge GetClosestEdgeToPos(Vector3 pos)
	{
		float num = float.MaxValue;
		BridgeEdge result = null;
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				Vector3 vector = Utils.NearestPointOnLineSegment(edge.m_JointA.transform.position, edge.m_JointB.transform.position, pos);
				float num2 = Vector2.Distance(pos, vector);
				if (num2 < num)
				{
					num = num2;
					result = edge;
				}
			}
		}
		return result;
	}

	public static bool EdgeExistsWithNodePositions(Vector3 A, Vector3 B, BridgeMaterialType bridgeMaterialType)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && edge.m_Material.m_MaterialType == bridgeMaterialType && edge.NodeExistsAtPosition(A) && edge.NodeExistsAtPosition(B))
			{
				return true;
			}
		}
		return false;
	}

	public static BridgeEdge EdgeExistsWithNodePositions(Vector3 A, Vector3 B)
	{
		foreach (BridgeEdge edge in m_Edges)
		{
			if (edge.gameObject.activeInHierarchy && edge.NodeExistsAtPosition(A) && edge.NodeExistsAtPosition(B))
			{
				return edge;
			}
		}
		return null;
	}

	public static void SetPermanentEdgeColor(BridgeEdge edge, string colorHexCode)
	{
		if (!(edge == null))
		{
			if (m_BridgeEdgeColorsPermanent.ContainsKey(edge.m_Guid))
			{
				m_BridgeEdgeColorsPermanent[edge.m_Guid] = colorHexCode;
			}
			else
			{
				m_BridgeEdgeColorsPermanent.Add(edge.m_Guid, colorHexCode);
			}
		}
	}

	public static void RemovePermanentEdgeColor(BridgeEdge edge)
	{
		if (!(edge == null) && m_BridgeEdgeColorsPermanent.ContainsKey(edge.m_Guid))
		{
			m_BridgeEdgeColorsPermanent.Remove(edge.m_Guid);
		}
	}

	private static void SetStressColor()
	{
		if (Bridge.IsSimulating())
		{
			foreach (BridgeEdge edge in m_Edges)
			{
				if ((bool)edge.m_PhysicsEdge && (bool)edge.m_PhysicsEdge.handle && !edge.m_IsBroken && !edge.m_IsDebris)
				{
					edge.SetStressColor(Mathf.Max(0.01f, Mathf.Clamp01(edge.m_PhysicsEdge.smoothedStressNormalized)));
				}
				else
				{
					edge.SetStressColor(0f);
				}
			}
			return;
		}
		foreach (BridgeEdge edge2 in m_Edges)
		{
			edge2.SetStressColor(0.01f);
		}
	}

	private static Transform GetEdgesContainerTransform()
	{
		if (!m_EdgesContainer)
		{
			m_EdgesContainer = new GameObject("CreatedEdges");
		}
		return m_EdgesContainer.transform;
	}

	private static void HighlightEdgesWhenMouseOverJointSelector()
	{
		Collider closestRaycastHit = Utils.GetClosestRaycastHit(GameInput.GetMousePosition(), Utils.JOINT_SELECTOR_LAYER_MASK);
		if ((bool)closestRaycastHit)
		{
			BridgeJointSelector component = closestRaycastHit.transform.GetComponent<BridgeJointSelector>();
			if ((bool)component && (bool)component.m_Edge && !Pistons.m_SliderFollowingMouse && !BridgeSprings.m_SliderFollowingMouse)
			{
				component.m_Edge.m_MouseHoveringOverJointSelector = true;
			}
		}
	}

	public static bool AreJointsConnected(BridgeJoint start, BridgeJoint end, List<BridgeEdge> edges = null)
	{
		if (start == end)
		{
			return true;
		}
		Queue<BridgeJoint> queue = new Queue<BridgeJoint>();
		HashSet<BridgeJoint> hashSet = new HashSet<BridgeJoint>();
		HashSet<BridgeEdge> hashSet2 = ((edges != null) ? new HashSet<BridgeEdge>(edges) : null);
		int num = 10000;
		queue.Enqueue(start);
		hashSet.Add(start);
		while (0 < queue.Count && 0 < num)
		{
			num--;
			BridgeJoint bridgeJoint = queue.Dequeue();
			foreach (BridgeEdge item in bridgeJoint.m_ConnectedEdgesCache)
			{
				if (hashSet2 == null || hashSet2.Contains(item))
				{
					BridgeJoint bridgeJoint2 = ((item.m_JointA == bridgeJoint) ? item.m_JointB : item.m_JointA);
					if (!hashSet.Contains(bridgeJoint2))
					{
						queue.Enqueue(bridgeJoint2);
						hashSet.Add(bridgeJoint2);
					}
					if (bridgeJoint2 == end)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
