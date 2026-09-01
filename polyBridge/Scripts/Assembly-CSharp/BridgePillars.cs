using System.Collections.Generic;
using Poly;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class BridgePillars
{
	public static List<BridgePillar> m_BridgePillars = new List<BridgePillar>();

	public static float BASE_COST = 13000f;

	public static float MESH_TOP_HEIGHT = 1f;

	public static float MESH_BASE_HEIGHT = 0.75f;

	public static float MESH_CAP_HEIGHT = 1f;

	public static float MIN_HEIGHT = MESH_BASE_HEIGHT + MESH_CAP_HEIGHT;

	public static float MAX_HEIGHT = 10.006f;

	public static float BRIDGE_PILLAR_OUTLINE_Z = -4f;

	public static float Y_THRESHOLD_FOR_ADJUSTABLE_SPLINE_POINTS = 1.5f;

	public static float TOP_SCALE_ADJUSTMENT = 1f;

	public static Color m_NormalColor = new Color(1f, 1f, 1f, 1f);

	public static Color m_PlacementColor = new Color(0.5f, 0.5f, 0.5f, 20f / 51f);

	public static Color m_ErrorColor = new Color(0.75f, 0f, 0f, 20f / 51f);

	public static string BASE_COLOR_SHADER_ID = "_BaseColor";

	private static GameObject m_FoundationsContainer;

	public static BridgePillar Create(GameObject prefab, float height, Vector3 pos, Quaternion rot, string guid, string anchorGuid)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		BridgePillar component = gameObject.GetComponent<BridgePillar>();
		if (!component)
		{
			return null;
		}
		component.transform.parent = GetFoundationsContainerTransform();
		component.name = prefab.name;
		component.m_Guid = guid;
		component.m_AnchorGuid = ((!string.IsNullOrEmpty(anchorGuid)) ? anchorGuid : component.CreateAnchor()?.m_Guid);
		component.SetTopHeightBasedOnTotalHeight(height);
		component.SetColor(m_NormalColor);
		m_BridgePillars.Add(component);
		return component;
	}

	public static void UpdateManual()
	{
		BridgePillarDistanceMarkers.Show(BridgeSelectionSet.m_BridgePillars);
	}

	public static BridgePillar FindByGuid(string guid)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_Guid == guid)
			{
				return bridgePillar;
			}
		}
		return null;
	}

	public static void DestroyAll()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			bridgePillar.Destroy();
		}
		m_BridgePillars.Clear();
	}

	public static void DestroyAllExceptPrebuilt()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (!bridgePillar.IsPrebuilt())
			{
				bridgePillar.Destroy();
			}
		}
	}

	public static void ForceDisableAllExceptPrebuilt()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (!bridgePillar.IsPrebuilt())
			{
				bridgePillar.m_ForceDisabled = true;
				bridgePillar.gameObject.SetActive(value: false);
			}
		}
	}

	public static void UndoForceDisabled()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (!bridgePillar.IsPrebuilt() && bridgePillar.m_ForceDisabled)
			{
				bridgePillar.m_ForceDisabled = false;
				bridgePillar.gameObject.SetActive(value: true);
			}
		}
	}

	public static void AddToSimulation()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				PlaceableCollisionInfo componentInChildren = bridgePillar.GetComponentInChildren<PlaceableCollisionInfo>();
				if ((bool)componentInChildren)
				{
					componentInChildren.OnAddedToWorld();
				}
			}
		}
	}

	public static BridgePillar OverlapsPolygonShape(PolygonShape shape)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && bridgePillar.OverlapsPolygonShape(shape))
			{
				return bridgePillar;
			}
		}
		return null;
	}

	public static void UpdatePolygonShapes()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				bridgePillar.UpdatePolygonShapes();
			}
		}
	}

	public static BridgePillar GetClosestThatOverlapPolygonShape(Vector2 pos, PolygonShape shape)
	{
		BridgePillar result = null;
		float num = float.MaxValue;
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && bridgePillar.OverlapsPolygonShape(shape))
			{
				float num2 = Vector2.Distance(pos, bridgePillar.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = bridgePillar;
				}
			}
		}
		return result;
	}

	public static List<BridgeJointProxy> SerializeAnchors()
	{
		List<BridgeJointProxy> list = new List<BridgeJointProxy>();
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(bridgePillar.m_AnchorGuid);
				if ((bool)bridgeJoint && bridgeJoint.gameObject.activeInHierarchy)
				{
					BridgeJointProxy item = new BridgeJointProxy(bridgeJoint);
					list.Add(item);
				}
			}
		}
		return list;
	}

	public static List<BridgePillarProxy> Serialize()
	{
		List<BridgePillarProxy> list = new List<BridgePillarProxy>();
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				list.Add(new BridgePillarProxy(bridgePillar));
			}
		}
		return list;
	}

	public static void Deserialize(List<BridgePillarProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (BridgePillarProxy proxy in proxies)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(proxy.m_AnchorGuid);
			if (!(bridgeJoint == null) && bridgeJoint.m_IsAnchor)
			{
				CreateBridgePillarFromProxy(proxy);
			}
		}
	}

	public static BridgePillar CreateBridgePillarFromProxy(BridgePillarProxy proxy)
	{
		if (!Prefabs.m_PrefabsDict.ContainsKey(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		GameObject prefab = Prefabs.m_PrefabsDict[proxy.m_PrefabName];
		proxy.m_Height = Mathf.Clamp(proxy.m_Height, MIN_HEIGHT, GetMaxHeight());
		BridgePillar bridgePillar = Create(prefab, proxy.m_Height, proxy.m_Pos, Quaternion.identity, proxy.m_Guid, proxy.m_AnchorGuid);
		if ((bool)bridgePillar)
		{
			ApplyProxyToBridgePillar(bridgePillar, proxy);
		}
		return bridgePillar;
	}

	public static void ApplyProxyToBridgePillar(BridgePillar bridgePillar, BridgePillarProxy proxy)
	{
		bridgePillar.transform.position = proxy.m_Pos;
		bridgePillar.SetPrebuiltState(proxy.m_BridgePrebuiltState);
		bridgePillar.SetTopHeightBasedOnTotalHeight(proxy.m_Height);
		bridgePillar.UpdatePolygonShapes();
	}

	public static float Cost()
	{
		float num = 0f;
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && !bridgePillar.IsLocked())
			{
				num += bridgePillar.Cost();
			}
		}
		return num;
	}

	public static bool IsBridgePillarAnchor(string anchorGuid)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && bridgePillar.m_AnchorGuid == anchorGuid)
			{
				return true;
			}
		}
		return false;
	}

	public static BridgePillar GetBridgePillarWithAnchor(string anchorGuid)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && bridgePillar.m_AnchorGuid == anchorGuid)
			{
				return bridgePillar;
			}
		}
		return null;
	}

	public static BridgePillar GetActiveOrInactiveBridgePillarWithAnchor(string anchorGuid)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.m_AnchorGuid == anchorGuid)
			{
				return bridgePillar;
			}
		}
		return null;
	}

	public static int GetNumActivePillars()
	{
		int num = 0;
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		return num;
	}

	public static int GetNumActivePillarsNotLocked()
	{
		int num = 0;
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && !bridgePillar.IsLocked())
			{
				num++;
			}
		}
		return num;
	}

	public static void EnableOutlines()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				bridgePillar.EnableOutline(enable: true);
			}
		}
	}

	public static void DisableOutlines()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			bridgePillar.EnableOutline(enable: false);
		}
	}

	public static HashSet<BridgePillar> CollectInRect(Rect rect)
	{
		HashSet<BridgePillar> hashSet = new HashSet<BridgePillar>();
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy && bridgePillar.OverlapsRect(rect))
			{
				hashSet.Add(bridgePillar);
			}
		}
		return hashSet;
	}

	public static bool AllowedToPlaceBridgePillar(BridgePillar bridgePillar)
	{
		if (CollidesWithOtherBridgePillar(bridgePillar, bridgePillar.m_PolygonShapes))
		{
			return false;
		}
		if (Budget.GetRemainingFromHardBudget() < 0)
		{
			return false;
		}
		return AllowedToPlace(bridgePillar.transform.position, bridgePillar.GetAnchor(), bridgePillar.GetTotalHeight(), bridgePillar.m_PolygonShapes, bridgePillar.m_Outline) == PlacementReturnValue.SUCCESS;
	}

	public static PlacementReturnValue AllowedToPlaceClipboardBridgePillar(ClipboardBridgePillar clipboardBridgePillar, int pillarsLeft)
	{
		if (pillarsLeft < 1 && !Game.InSandboxGodMode())
		{
			return PlacementReturnValue.FAIL_NO_MATERIAL_LEFT;
		}
		if (CollidesWithOtherBridgePillar(null, clipboardBridgePillar.m_PolygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		float totalHeight = clipboardBridgePillar.GetTotalHeight();
		if (!CanAffordHeight(totalHeight))
		{
			return PlacementReturnValue.FAIL_CANNOT_AFFORD_COST;
		}
		return AllowedToPlace(clipboardBridgePillar.transform.position, null, totalHeight, clipboardBridgePillar.m_PolygonShapes, clipboardBridgePillar.m_Outline);
	}

	public static float CalculateHighestRoundHeightCanAfford()
	{
		if (!Budget.CanAffordCost(BASE_COST))
		{
			return MIN_HEIGHT;
		}
		float num = ((float)Budget.GetRemainingFromHardBudget() - BASE_COST) / BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.PILLAR).m_PricePerMeter;
		int num2 = Mathf.FloorToInt(MESH_BASE_HEIGHT + num);
		return Mathf.Min(GetMaxHeight(), num2);
	}

	public static bool CanAffordHeight(float height)
	{
		return Budget.CanAffordCost(CalculateCostFromHeight(height));
	}

	public static float CalculateCostFromHeight(float height)
	{
		return BASE_COST + Mathf.Max(0f, height - MIN_HEIGHT) * BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.PILLAR).m_PricePerMeter;
	}

	public static PlacementReturnValue AllowedToPlace(Vector3 pos, BridgeJoint anchor, float height, List<PolygonShape> polygonShapes, Outline outline)
	{
		Bounds pillarBounds = new Bounds(pos + new Vector3(0f, height / 2f, 0f), new Vector2(1.43f, height));
		if (height > GetMaxHeight())
		{
			return PlacementReturnValue.FAIL_PILLAR_EXCEEDS_MAX_HEIGHT;
		}
		if (OutsideBookends(pos))
		{
			return PlacementReturnValue.FAIL_PILLAR_NOT_BETWEEN_ISLANDS;
		}
		if (CollidesWithTerrain(polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithCustomShape(pillarBounds, polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithRock(pillarBounds, polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithFlyingObject(pillarBounds, polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithZedAxisVehicle(pillarBounds, polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithVehicle(pillarBounds, polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithPlatforms(pillarBounds))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		if (CollidesWithRamps(pillarBounds))
		{
			return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
		}
		Vector3 vector = pos + new Vector3(0f, height, 0f);
		if (CollidesWithNodes(vector, polygonShapes))
		{
			return PlacementReturnValue.FAIL_PILLAR_ANCHOR_ILLEGAL_LOCATION;
		}
		if (anchor != null && BridgeJoints.JointOverlapsOtherJoints(anchor, GameSettings.NodeRadius(), GameSettings.NodeDiameter()))
		{
			return PlacementReturnValue.FAIL_PILLAR_ANCHOR_ILLEGAL_LOCATION;
		}
		if (anchor == null && BridgeJoints.SphereOverlapsOtherJoints(vector, GameSettings.NodeRadius(), GameSettings.NodeDiameter()))
		{
			return PlacementReturnValue.FAIL_PILLAR_ANCHOR_ILLEGAL_LOCATION;
		}
		if (!BuildZones.ContainsBridgePillar(new Vector3(pos.x, pos.y + height, pos.z), outline.m_VectorLine))
		{
			return PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE;
		}
		if (anchor != null)
		{
			int numConnectedEdges = anchor.GetNumConnectedEdges();
			for (int i = 0; i < numConnectedEdges; i++)
			{
				BridgeEdge connecteEdge = anchor.GetConnecteEdge(i);
				BridgeJoint bridgeJoint = ((anchor == connecteEdge.m_JointA) ? connecteEdge.m_JointB : connecteEdge.m_JointA);
				if (BridgeEdges.EdgeLocationOverlapsBlockingPolygonShape(anchor.transform.position, bridgeJoint.transform.position, connecteEdge.m_Material.m_MaterialType, connecteEdge.m_Material.m_EdgeMaterial.collisionRadius))
				{
					return PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE;
				}
				if (!BuildZones.ContainsEdge(anchor.transform.position, bridgeJoint.transform.position))
				{
					return PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE;
				}
			}
		}
		if (anchor != null)
		{
			anchor.gameObject.SetActive(value: false);
		}
		BridgeJoint bridgeJoint2 = BridgeJoints.FindClosestJoint(vector);
		if (anchor != null)
		{
			anchor.gameObject.SetActive(value: true);
		}
		if ((bool)bridgeJoint2 && bridgeJoint2.m_IsAnchor && Vector2.Distance(bridgeJoint2.transform.position, vector) < GameSettings.NodeRadius())
		{
			return PlacementReturnValue.FAIL_PILLAR_ANCHOR_ILLEGAL_LOCATION;
		}
		return PlacementReturnValue.SUCCESS;
	}

	public static bool CollidesWithOtherBridgePillar(BridgePillar self, List<PolygonShape> polygonShapes)
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (!bridgePillar.gameObject.activeInHierarchy || !(bridgePillar != self))
			{
				continue;
			}
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (bridgePillar.OverlapsPolygonShape(polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithTerrain(List<PolygonShape> polygonShapes)
	{
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			if (!terrain.gameObject.activeInHierarchy)
			{
				continue;
			}
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (terrain.OverlapsPolygonShape(polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithCustomShape(Bounds pillarBounds, List<PolygonShape> polygonShapes)
	{
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			if (!shape.gameObject.activeInHierarchy || !shape.m_MeshRenderer.bounds.Intersects(pillarBounds))
			{
				continue;
			}
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (shape.OverlapsPolygonShape(polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithRock(Bounds pillarBounds, List<PolygonShape> polygonShapes)
	{
		foreach (Rock rock in Rocks.m_Rocks)
		{
			if (!rock.gameObject.activeInHierarchy || !rock.m_MeshRenderer.bounds.Intersects(pillarBounds))
			{
				continue;
			}
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (rock.OverlapsPolygonShape(polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithFlyingObject(Bounds pillarBounds, List<PolygonShape> polygonShapes)
	{
		foreach (FlyingObject flyingObject in FlyingObjects.m_FlyingObjects)
		{
			if (!flyingObject.gameObject.activeInHierarchy || !flyingObject.m_MeshRenderer.bounds.Intersects(pillarBounds))
			{
				continue;
			}
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (flyingObject.OverlapsPolygonShape(polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithZedAxisVehicle(Bounds pillarBounds, List<PolygonShape> polygonShapes)
	{
		foreach (ZedAxisVehicle vehicle in ZedAxisVehicles.m_Vehicles)
		{
			if (!vehicle.gameObject.activeInHierarchy)
			{
				continue;
			}
			Vector3 center = new Vector3(vehicle.m_BoxCollider.bounds.center.x, vehicle.m_BoxCollider.bounds.center.y, 0f);
			Bounds bounds = new Bounds(center, vehicle.m_BoxCollider.bounds.size);
			if (!bounds.Intersects(pillarBounds))
			{
				continue;
			}
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (vehicle.OverlapsPolygonShape(polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithVehicle(Bounds pillarBounds, List<PolygonShape> polygonShapes)
	{
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (!vehicle.gameObject.activeInHierarchy)
			{
				continue;
			}
			Renderer[] renderers = vehicle.m_Renderers;
			for (int i = 0; i < renderers.Length; i++)
			{
				if (!renderers[i].bounds.Intersects(pillarBounds))
				{
					continue;
				}
				foreach (PolygonShape polygonShape in polygonShapes)
				{
					if (vehicle.OverlapsPolygonShape(polygonShape))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private static bool CollidesWithPlatforms(Bounds pillarBounds)
	{
		foreach (Platform platform in Platforms.m_Platforms)
		{
			if (platform.gameObject.activeInHierarchy)
			{
				Bounds bounds = new Bounds(platform.transform.position, new Vector2(platform.m_Width, 0.25f));
				if (pillarBounds.Intersects(bounds))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool CollidesWithRamps(Bounds pillarBounds)
	{
		foreach (Ramp ramp in Ramps.m_Ramps)
		{
			if (!ramp.gameObject.activeInHierarchy || !ramp.m_Bounds.Intersects(pillarBounds))
			{
				continue;
			}
			foreach (MeshRenderer plank in ramp.m_Planks)
			{
				if (plank.bounds.Intersects(pillarBounds))
				{
					return true;
				}
			}
		}
		return false;
	}

	private static bool OutsideBookends(Vector3 pos)
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (leftTerrain != null && !leftTerrain.m_Hidden && pos.x < leftTerrain.transform.position.x)
		{
			return true;
		}
		if (rightTerrain != null && !rightTerrain.m_Hidden && pos.x > rightTerrain.transform.position.x)
		{
			return true;
		}
		return false;
	}

	private static bool CollidesWithNodes(Vector3 anchorPos, List<PolygonShape> polygonShapes)
	{
		float radius = GameSettings.NodeRadius() - 0.001f;
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if (!joint.gameObject.activeInHierarchy || IsBridgePillarAnchor(joint.m_Guid) || Vector2.Distance(anchorPos, joint.transform.position) < GameSettings.NodeRadius())
			{
				continue;
			}
			PolygonShape testShape = PolygonShape.FromCircle((Vec2)joint.transform.position, radius);
			foreach (PolygonShape polygonShape in polygonShapes)
			{
				if (Utils.PolygonShapeOverlapsShape(testShape, polygonShape))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static void InitFX()
	{
		bool flag = GameStateManager.GetState() == GameState.SANDBOX;
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			bridgePillar.m_LockIcon.SetActive(bridgePillar.IsLocked() && flag);
			bridgePillar.DeSelect();
		}
	}

	public static void SelectAll()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				BridgeSelectionSet.SelectBridgePillar(bridgePillar);
			}
		}
	}

	public static float GetMaxHeight()
	{
		if (!SandboxSettings.m_UnlimitedHeightFoundations && !Profiles.m_ActiveProfile.m_GodMode)
		{
			return MAX_HEIGHT;
		}
		return TerrainIslands.MAX_HEIGHT;
	}

	public static BridgePillar GetBridgePillarAtScreenPos(Vector2 screenPos)
	{
		float radius = GameSettings.NodeRadius() - 0.001f;
		return OverlapsPolygonShape(PolygonShape.FromCircle((Vec2)Utils.GetWorldPointFromScreenPos(screenPos), radius));
	}

	public static bool IllegalFoundationPlacment()
	{
		foreach (BridgePillar bridgePillar in m_BridgePillars)
		{
			if (bridgePillar.HasIllegalPlacement())
			{
				return true;
			}
		}
		return false;
	}

	private static Transform GetFoundationsContainerTransform()
	{
		if (!m_FoundationsContainer)
		{
			m_FoundationsContainer = new GameObject("CreatedFoundations");
		}
		return m_FoundationsContainer.transform;
	}
}
