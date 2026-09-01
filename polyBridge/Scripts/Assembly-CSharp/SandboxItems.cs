using System.Collections.Generic;
using System.IO;
using Poly.Collide;
using UnityEngine;

public class SandboxItems
{
	public static List<SandboxItem> m_Items = new List<SandboxItem>();

	public static List<SandboxItem> m_Imposters = new List<SandboxItem>();

	public static SandboxItem m_NewUnPlacedItem;

	public static SandboxItem m_Hover;

	public static Vector2 m_NewUplacedItemStartMousePos;

	public static float DEFAULT_FLOATING_TEXT_YOFFSET = 0.75f;

	public static float DEFAULT_FLOATING_TEXT_Z = -5f;

	public static float MIN_X = -250f;

	public static float MAX_X = 250f;

	public static float MIN_Y = -250f;

	public static float MAX_Y = 250f;

	private static GameObject m_SandboxItemContainer;

	private static Dictionary<SandboxItemType, int> m_PrioritiesDictionary = new Dictionary<SandboxItemType, int>();

	private static List<SandboxItem> m_PrioritizedItemList = new List<SandboxItem>();

	private static List<SandboxItem> m_OverlappingFloatingTextItems = new List<SandboxItem>();

	private static PolygonShape m_CircleShapeForPointer;

	public static void Init()
	{
		int num = 1;
		m_PrioritiesDictionary.Add(SandboxItemType.ANCHOR, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.CHECKPOINT, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.VEHICLE_STOP_TRIGGER, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.VEHICLE, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.PLATFORM, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.RAMP, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.CUSTOM_SHAPE, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.PILLAR, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.DECOR, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.ROCK, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.FLYING_OBJECT, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.ZED_AXIS_VEHICLE, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.BUILD_ZONE, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.TERRAIN, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.WATER, num++);
		m_PrioritiesDictionary.Add(SandboxItemType.HYDRAULICS_PHASE, num++);
		m_CircleShapeForPointer = PolygonShape.FromCircle(Vector2.zero, 0.01f);
	}

	public static void UpdateManual()
	{
		if ((bool)m_NewUnPlacedItem)
		{
			MoveUnplacedItemWithPointer(m_NewUnPlacedItem);
			if (NewUnPlacedItemHasMoved() || m_NewUnPlacedItem.m_Type == SandboxItemType.ROCK || m_NewUnPlacedItem.m_Type == SandboxItemType.TERRAIN)
			{
				m_NewUnPlacedItem.gameObject.SetActive(value: true);
				m_NewUnPlacedItem.SetOutlineDirty(dirty: true);
			}
		}
		m_Hover = GetHoverItem();
		ProcessImposters();
	}

	public static void UpdateFloatingText()
	{
		foreach (SandboxItem item in m_Items)
		{
			item.UpdateFloatingText();
		}
	}

	public static void UpdateFloatingTextFocus()
	{
		if (GameStateManager.GetState() != GameState.BUILD || !(GameStateBuild.m_HoverSandboxItem != null) || !(GameStateBuild.m_HoverSandboxItem.m_Label != null) || GameStateBuild.m_HoverSandboxItem.gameObject.layer == Utils.RENDER_LAST_LAYER || !GameStateBuild.HoveringPastThreshold(GameStateBuild.m_HoverSandboxItem))
		{
			return;
		}
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition());
		Vector3 point = new Vector3(worldPointFromScreenPos.x, worldPointFromScreenPos.y, GameStateBuild.m_HoverSandboxItem.m_Label.m_BackgroundBoxCollider.bounds.center.z);
		if (!GameStateBuild.m_HoverSandboxItem.m_Label.m_BackgroundBoxCollider.bounds.Contains(point))
		{
			Utils.SetLayerRecursively(GameStateBuild.m_HoverSandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
		}
		Vehicle linkedVehicle = GameStateBuild.m_HoverSandboxItem.GetLinkedVehicle();
		if (!(linkedVehicle != null))
		{
			return;
		}
		VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.FindTriggerThatStopsVehicle(linkedVehicle.m_Guid);
		switch (GameStateBuild.m_HoverSandboxItem.m_Type)
		{
		case SandboxItemType.VEHICLE:
			if (vehicleStopTrigger != null)
			{
				Utils.SetLayerRecursively(vehicleStopTrigger.m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
			}
			{
				foreach (Checkpoint checkpoint in linkedVehicle.m_Checkpoints)
				{
					Utils.SetLayerRecursively(checkpoint.m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
				}
				break;
			}
		case SandboxItemType.CHECKPOINT:
			if (vehicleStopTrigger != null)
			{
				Utils.SetLayerRecursively(linkedVehicle.m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
			}
			Utils.SetLayerRecursively(vehicleStopTrigger.m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
			break;
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
			Utils.SetLayerRecursively(linkedVehicle.m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
			{
				foreach (Checkpoint checkpoint2 in linkedVehicle.m_Checkpoints)
				{
					Utils.SetLayerRecursively(checkpoint2.m_SandboxItem.m_Label.gameObject, Utils.RENDER_LAST_LAYER);
				}
				break;
			}
		case SandboxItemType.WATER:
			break;
		}
	}

	public static void DisableFloatingText()
	{
		foreach (SandboxItem item in m_Items)
		{
			item.DisableFloatingText();
		}
	}

	public static void SetNewUnPlacedItem(SandboxItem item)
	{
		m_NewUnPlacedItem = item;
		m_NewUplacedItemStartMousePos = GameInput.GetMousePosition();
		m_NewUnPlacedItem.gameObject.SetActive(value: false);
	}

	public static void PlaceNewItem(SandboxItem item)
	{
		if (!item)
		{
			return;
		}
		item.gameObject.SetActive(value: true);
		bool flag = !NewUnPlacedItemHasMoved() || GameUI.IsPointerOverGameObject();
		if (flag)
		{
			MaybePositionBetweenBookends(item);
		}
		PlayPlaceSound(item.m_Type);
		switch (item.m_Type)
		{
		case SandboxItemType.VEHICLE:
			PlaceNewVehicle(item.GetComponent<Vehicle>(), flag);
			break;
		case SandboxItemType.ZED_AXIS_VEHICLE:
			PlaceNewZedAxisVehicle(item.GetComponent<ZedAxisVehicle>(), flag);
			break;
		case SandboxItemType.TERRAIN:
		{
			TerrainIsland component = item.GetComponent<TerrainIsland>();
			if ((bool)component && component.m_TerrainIslandType == TerrainIslandType.Middle)
			{
				component.DisplayFullMesh(0.5f);
				component.m_RightEdgeWaterHeight = WaterBlocks.GetWaterHeightForTerrainRightEdge(component);
			}
			break;
		}
		case SandboxItemType.DECOR:
			PlaceNewDecor(item.GetComponent<Decor>());
			break;
		}
		if ((bool)item.m_Label)
		{
			item.SetFloatingTextToDefaultPosition();
			ResolveOverlappingFloatingText();
		}
		if (item.m_Type == SandboxItemType.ANCHOR)
		{
			BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
		}
		if (item.m_Type == SandboxItemType.CUSTOM_SHAPE)
		{
			CustomShape component2 = item.GetComponent<CustomShape>();
			if (component2.m_FullyQualifiedPath == Path.Combine(Application.streamingAssetsPath, CustomShapesLibrary.CUSTOM_SHAPE_LIBRARY_FOLDER, CustomShapes.CUSTOM_SHAPE_NAME_NGON))
			{
				GameUI.m_Instance.m_CustomShapeReset.gameObject.SetActive(value: true);
				GameUI.m_Instance.m_CustomShapeReset.m_CustomShape = component2;
			}
		}
		item.FinalizeMovement();
		if (item.m_Type == SandboxItemType.IMPOSTER)
		{
			CustomShape[] componentsInChildren = item.gameObject.GetComponentsInChildren<CustomShape>(includeInactive: true);
			if (componentsInChildren.Length != 0)
			{
				CustomShape[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].transform.SetParent(null);
				}
				item.gameObject.SetActive(value: false);
				Object.Destroy(item.gameObject);
				SandboxUndo.SnapShot();
			}
		}
		else
		{
			SandboxSelectionSet.CancelSelection();
			SandboxSelectionSet.SelectItem(item);
			GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
			EventEditor.m_PendingStage = null;
			SandboxUndo.SnapShot();
		}
	}

	public static SandboxItem AddSandboxItemComponent(GameObject gameObject, SandboxItemType itemType)
	{
		SandboxItem component = gameObject.GetComponent<SandboxItem>();
		if ((bool)component)
		{
			return component;
		}
		Transform parent = gameObject.transform.parent;
		component = gameObject.AddComponent<SandboxItem>();
		component.m_OriginalParent = parent;
		component.m_Type = itemType;
		return component;
	}

	public static void DeleteItem(GameObject gameObject)
	{
		SandboxItem component = gameObject.GetComponent<SandboxItem>();
		if ((bool)component)
		{
			m_Items.Remove(component);
			Object.Destroy(gameObject);
		}
	}

	public static void RemoveItem(GameObject gameObject)
	{
		SandboxItem component = gameObject.GetComponent<SandboxItem>();
		if ((bool)component)
		{
			m_Items.Remove(component);
			if (component.m_OriginalParent != null)
			{
				component.transform.parent = component.m_OriginalParent;
			}
		}
	}

	public static Transform GetSandboxContainerTransform()
	{
		if (!m_SandboxItemContainer)
		{
			m_SandboxItemContainer = new GameObject("CurrentLevel");
		}
		return m_SandboxItemContainer.transform;
	}

	public static SandboxItem TrySelectItem(Vector2 screenPos)
	{
		SandboxItem itemUnderPos = GetItemUnderPos(screenPos);
		if ((bool)itemUnderPos && itemUnderPos.gameObject.activeInHierarchy)
		{
			SandboxSelectionSet.StoreSelectionSetGuids();
			if (GameInput.MultiSelectIsDown())
			{
				if (SandboxSelectionSet.m_Items.Contains(itemUnderPos))
				{
					SandboxSelectionSet.DeSelectItem(itemUnderPos);
				}
				else
				{
					SandboxSelectionSet.SelectItem(itemUnderPos);
					MaybeSelectVehicleInEventEditor(itemUnderPos);
					if (itemUnderPos.m_Type == SandboxItemType.DECOR && GameUI.m_Instance.m_SandboxMultiSelect.gameObject.activeInHierarchy)
					{
						GameUI.m_Instance.m_SandboxMultiSelect.RefreshProperties();
					}
				}
			}
			else
			{
				SandboxSelectionSet.CancelSelection();
				SandboxSelectionSet.SelectItem(itemUnderPos);
				MaybeSelectVehicleInEventEditor(itemUnderPos);
			}
			if (!SandboxSelectionSet.SelectionSetMatchesStoredGuids())
			{
				SandboxUndo.SnapShot();
			}
			GameUI.m_Instance.m_SandboxMenu.MaybeActivateEditSubmenu();
			return itemUnderPos;
		}
		return null;
	}

	public static SandboxItem GetItemUnderPos(Vector3 screenPos)
	{
		m_PrioritizedItemList.Clear();
		int num = 0;
		num = ((GameStateManager.GetState() != GameState.DECOR) ? (Utils.SANDBOX_SELECT_MASK | Utils.SCENEGEO_LAYER_MASK | Utils.SCENEGEOSTATIC_LAYER_MASK | Utils.CUSTOM_SHAPE_LAYER_MASK | Utils.VEHICLE_LAYER_MASK | Utils.JOINT_HOTSPOT_LAYER_MASK | Utils.WATER_LAYER_MASK | Utils.PICKUP_BY_VEHICLE_LAYER_MASK | Utils.RENDER_LAST_LAYER_MASK) : Utils.DECOR_LAYER_MASK);
		if (GameUI.PointerOver(typeof(WaterRuler)))
		{
			m_PrioritizedItemList.Add(WaterBlocks.GetSandboxItem());
			return m_PrioritizedItemList[0];
		}
		Ray ray = Cameras.MainCamera().ScreenPointToRay(screenPos);
		int numHits = Physics.RaycastNonAlloc(ray, Utils.m_RaycastHits, float.MaxValue, num);
		AddHitsToPrioritizedItemList(Utils.m_RaycastHits, numHits);
		Vector2 vector = Cameras.MainCamera().ScreenToWorldPoint(screenPos);
		m_CircleShapeForPointer.verts[0] = vector;
		if (GameStateManager.GetState() != GameState.DECOR)
		{
			Vehicle closestThatOverlapPolygonShape = Vehicles.GetClosestThatOverlapPolygonShape(vector, m_CircleShapeForPointer);
			if ((bool)closestThatOverlapPolygonShape)
			{
				m_PrioritizedItemList.Add(closestThatOverlapPolygonShape.m_SandboxItem);
			}
			ZedAxisVehicle closestThatOverlapPolygonShape2 = ZedAxisVehicles.GetClosestThatOverlapPolygonShape(vector, m_CircleShapeForPointer);
			if ((bool)closestThatOverlapPolygonShape2)
			{
				m_PrioritizedItemList.Add(closestThatOverlapPolygonShape2.m_SandboxItem);
			}
			Rock closestThatOverlapPolygonShape3 = Rocks.GetClosestThatOverlapPolygonShape(vector, m_CircleShapeForPointer);
			if ((bool)closestThatOverlapPolygonShape3)
			{
				m_PrioritizedItemList.Add(closestThatOverlapPolygonShape3.m_SandboxItem);
			}
			FlyingObject closestThatOverlapPolygonShape4 = FlyingObjects.GetClosestThatOverlapPolygonShape(vector, m_CircleShapeForPointer);
			if ((bool)closestThatOverlapPolygonShape4)
			{
				m_PrioritizedItemList.Add(closestThatOverlapPolygonShape4.m_SandboxItem);
			}
			CustomShape closestThatOverlapsPolygonShape = CustomShapes.GetClosestThatOverlapsPolygonShape(vector);
			if ((bool)closestThatOverlapsPolygonShape)
			{
				m_PrioritizedItemList.Add(closestThatOverlapsPolygonShape.m_SandboxItem);
			}
			BuildZone closestThatContainPoint = BuildZones.GetClosestThatContainPoint(vector);
			if ((bool)closestThatContainPoint && !BuildZones.IsEditingBuildZone(closestThatContainPoint) && m_PrioritizedItemList.Count == 0)
			{
				m_PrioritizedItemList.Add(closestThatContainPoint.m_SandboxItem);
			}
			List<TerrainIsland> terrainsThatOverlapPolygonShape = TerrainIslands.GetTerrainsThatOverlapPolygonShape(m_CircleShapeForPointer);
			if (terrainsThatOverlapPolygonShape.Count > 0)
			{
				m_PrioritizedItemList.Add(terrainsThatOverlapPolygonShape[0].m_SandboxItem);
			}
			Collider closestRaycastHit = Utils.GetClosestRaycastHit(screenPos, Utils.JOINT_SELECTOR_LAYER_MASK);
			if ((bool)closestRaycastHit && (bool)closestRaycastHit.transform.parent.GetComponent<BuildZoneControlPoint>())
			{
				m_PrioritizedItemList.Clear();
			}
		}
		if (GameStateManager.GetState() == GameState.BUILD && !PrioritizedListContainsVehicle())
		{
			VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.CastRay(ray);
			if (vehicleStopTrigger != null)
			{
				m_PrioritizedItemList.Add(vehicleStopTrigger.m_SandboxItem);
			}
			Checkpoint checkpoint = Checkpoints.CastRay(ray);
			if (checkpoint != null)
			{
				m_PrioritizedItemList.Add(checkpoint.m_SandboxItem);
			}
		}
		if (m_PrioritizedItemList.Count == 0)
		{
			return null;
		}
		if (m_PrioritizedItemList.Count == 1)
		{
			return m_PrioritizedItemList[0];
		}
		m_PrioritizedItemList.Sort(SortBySandboxItemType);
		return m_PrioritizedItemList[0];
	}

	public static void ProcessImposters()
	{
		foreach (SandboxItem imposter in m_Imposters)
		{
			GameObject asyncPrefab = Prefabs.GetAsyncPrefab(imposter.m_LoadingAddressable);
			if (!string.IsNullOrEmpty(imposter.m_LoadingAddressable) && asyncPrefab == null)
			{
				continue;
			}
			switch (imposter.m_LoadingAddressableType)
			{
			case SandboxItemType.VEHICLE:
			{
				SandboxItem newItem = CreateVehicle(imposter.transform.position, asyncPrefab, imposter.m_LoadingAddressableModId);
				ReplaceImposter(imposter, newItem);
				break;
			}
			case SandboxItemType.ZED_AXIS_VEHICLE:
			{
				SandboxItem newItem3 = CreateZedAxisVehicle(imposter.transform.position, asyncPrefab, imposter.m_LoadingAddressableModId);
				ReplaceImposter(imposter, newItem3);
				break;
			}
			case SandboxItemType.DECOR:
			{
				SandboxItem newItem2 = CreateDecor(imposter.transform.position, asyncPrefab, imposter.m_LoadingAddressableId, imposter.m_LoadingAddressableModId);
				ReplaceImposter(imposter, newItem2);
				break;
			}
			case SandboxItemType.CUSTOM_SHAPE:
			{
				CustomShape[] componentsInChildren = imposter.GetComponentsInChildren<CustomShape>(includeInactive: true);
				foreach (CustomShape obj in componentsInChildren)
				{
					obj.GetComponent<SandboxItem>().SetOutlineDirty(dirty: true);
					obj.MarkAllAnchorOutlinesDirty();
				}
				break;
			}
			}
		}
	}

	private static void ReplaceImposter(SandboxItem imposterItem, SandboxItem newItem)
	{
		if (m_NewUnPlacedItem == imposterItem)
		{
			if (!NewUnPlacedItemHasMoved())
			{
				newItem.gameObject.SetActive(value: false);
			}
			m_NewUnPlacedItem = newItem;
		}
		else
		{
			PlaceNewItem(newItem);
		}
		imposterItem.gameObject.SetActive(value: false);
		Object.Destroy(imposterItem.gameObject);
	}

	public static void DestroyImposters()
	{
		foreach (SandboxItem imposter in m_Imposters)
		{
			imposter.gameObject.SetActive(value: false);
			Object.Destroy(imposter.gameObject);
		}
	}

	public static bool IsNewUnplacedItem(SandboxItem item)
	{
		return m_NewUnPlacedItem == item;
	}

	public static void EnableOutlines()
	{
		foreach (SandboxItem item in m_Items)
		{
			if (item.gameObject.activeInHierarchy)
			{
				item.m_OutlineGroup.EnableOutline();
			}
		}
	}

	public static void DisableOutlines()
	{
		foreach (SandboxItem item in m_Items)
		{
			if (item.gameObject.activeInHierarchy)
			{
				item.m_OutlineGroup.DisableOutline();
			}
		}
	}

	public static void ResolveOverlappingFloatingText()
	{
		m_OverlappingFloatingTextItems.Clear();
		foreach (SandboxItem item in m_Items)
		{
			if ((bool)item.m_Label && item.gameObject.activeInHierarchy && item.m_Label.m_BackgroundBoxCollider != null)
			{
				item.m_Label.gameObject.SetActive(value: false);
				m_OverlappingFloatingTextItems.Add(item);
				item.SetFloatingTextToDefaultPosition();
			}
		}
		Bounds a = default(Bounds);
		Bounds b = default(Bounds);
		for (int i = 0; i < m_OverlappingFloatingTextItems.Count; i++)
		{
			m_OverlappingFloatingTextItems[i].m_Label.gameObject.SetActive(value: true);
			a.center = m_OverlappingFloatingTextItems[i].m_Label.transform.position;
			a.size = m_OverlappingFloatingTextItems[i].m_Label.m_BackgroundBoxCollider.size / 1.7f;
			for (int j = 0; j < i; j++)
			{
				b.center = m_OverlappingFloatingTextItems[j].m_Label.transform.position;
				b.size = m_OverlappingFloatingTextItems[j].m_Label.m_BackgroundBoxCollider.size / 1.7f;
				while (Utils.BoundsIntersect2D(a, b))
				{
					m_OverlappingFloatingTextItems[i].m_Label.transform.Translate(0f, GameGrid.m_Spacing, 0f, Space.World);
					a.center = m_OverlappingFloatingTextItems[i].m_Label.transform.position;
					j = 0;
				}
			}
		}
	}

	public static SandboxItem FindByGuid(string guid)
	{
		foreach (SandboxItem item in m_Items)
		{
			if (item.m_UndoGuid == guid)
			{
				return item;
			}
		}
		return null;
	}

	public static void PlaceNewVehicle(Vehicle vehicle, bool useDefaultStartPos)
	{
		if (useDefaultStartPos)
		{
			PlaceVehicleAtDefaultStart(vehicle);
			vehicle.UpdatePolygonShapes();
			vehicle.EnableSpriteOutline();
			vehicle.ResolveOverlap();
			vehicle.m_SandboxItem.SetFloatingTextToDefaultPosition();
		}
		CreateGoalTriggerForVehicle(vehicle);
	}

	private static SandboxItem GetHoverItem()
	{
		if (GameUI.IsEditingCustomShapeOrRamp())
		{
			return null;
		}
		SandboxItem sandboxItem = ((GameUI.IsPointerOverGameObject() || (bool)m_NewUnPlacedItem || SandboxSelectionSet.SelectionFollowsMouse()) ? null : GetItemUnderPos(GameInput.GetMousePosition()));
		if ((bool)sandboxItem && sandboxItem.IsLocked())
		{
			return null;
		}
		return sandboxItem;
	}

	private static void MaybeSelectVehicleInEventEditor(SandboxItem item)
	{
		if ((bool)item && (item.m_Type == SandboxItemType.VEHICLE || item.m_Type == SandboxItemType.ZED_AXIS_VEHICLE))
		{
			EventEditor.SelectIconMatchingGameObject(item.gameObject);
		}
	}

	private static bool NewUnPlacedItemHasMoved()
	{
		Vector2 vector = Utils.V3toV2(GameInput.GetMousePosition()) - m_NewUplacedItemStartMousePos;
		if (!(Mathf.Abs(vector.x) > 1f))
		{
			return Mathf.Abs(vector.y) > 1.1f;
		}
		return true;
	}

	private static void PlaceNewZedAxisVehicle(ZedAxisVehicle vehicle, bool useDefaultStartPos)
	{
		if (useDefaultStartPos)
		{
			PlaceZedAxisVehicleAtDefaultStart(vehicle);
			vehicle.UpdatePolygonShapes();
		}
	}

	private static void PlaceNewDecor(Decor decor)
	{
		if (decor.m_AlignTopWithHighestTerrain && decor.m_MeshHeight > 0f)
		{
			float maxHeight = TerrainIslands.GetMaxHeight();
			float num = (Game.InDecorModeTopView() ? 0f : decor.m_DefaultZOffset);
			decor.transform.position = new Vector3(decor.transform.position.x, maxHeight - decor.m_MeshHeight, decor.transform.position.z + num);
		}
	}

	private static void AddHitsToPrioritizedItemList(RaycastHit[] hits, int numHits)
	{
		for (int i = 0; i < numHits; i++)
		{
			RaycastHit hit = hits[i];
			SandboxItem sandboxItem;
			if (HitIsForSandboxLabel(hit))
			{
				sandboxItem = hit.collider.transform.parent.parent.GetComponent<SandboxItem>();
			}
			else
			{
				sandboxItem = GetSandboxItemFromRaycastHit(hit);
				if (!sandboxItem || !sandboxItem.enabled)
				{
					continue;
				}
				if (sandboxItem.m_Type == SandboxItemType.ANCHOR)
				{
					BridgeJoint component = sandboxItem.GetComponent<BridgeJoint>();
					if ((bool)component && (component.isCustomShapeAnchor() || BridgePillars.IsBridgePillarAnchor(component.m_Guid) || !component.m_IsAnchor))
					{
						continue;
					}
				}
				if ((bool)sandboxItem.GetComponent<Vehicle>() || (bool)sandboxItem.GetComponent<Rock>() || (bool)sandboxItem.GetComponent<FlyingObject>() || (sandboxItem.m_Type == SandboxItemType.RAMP && !sandboxItem.GetComponent<Ramp>().PointOnRampSurface(new Vector3(hit.point.x, hit.point.y, 0f))))
				{
					continue;
				}
			}
			m_PrioritizedItemList.Add(sandboxItem);
		}
	}

	private static bool HitIsForSandboxLabel(RaycastHit hit)
	{
		if ((hit.collider.gameObject.layer == Utils.BUILD_ZONE_LAYER || hit.collider.gameObject.layer == Utils.RENDER_LAST_LAYER) && hit.collider.transform.parent != null && hit.collider.transform.parent.GetComponent<SandboxItemLabel>() != null && hit.collider.transform.parent.parent != null && hit.collider.transform.parent.parent.GetComponent<SandboxItem>() != null)
		{
			return true;
		}
		return false;
	}

	private static int SortBySandboxItemType(SandboxItem a, SandboxItem b)
	{
		return m_PrioritiesDictionary[a.m_Type].CompareTo(m_PrioritiesDictionary[b.m_Type]);
	}

	private static SandboxItem GetSandboxItemFromRaycastHit(RaycastHit hit)
	{
		SandboxItem sandboxItem = hit.transform.gameObject.GetComponent<SandboxItem>();
		Transform parent = hit.transform.parent;
		while (!sandboxItem && (bool)parent)
		{
			sandboxItem = hit.transform.parent.gameObject.GetComponentInParent<SandboxItem>(includeInactive: true);
			if ((bool)sandboxItem)
			{
				break;
			}
			parent = parent.transform.parent;
		}
		return sandboxItem;
	}

	private static void PlaceVehicleAtDefaultStart(Vehicle vehicle)
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!leftTerrain)
		{
			Debug.LogWarningFormat("Could not find left terrain for placing vehicle");
			return;
		}
		if (!leftTerrain.m_SpawnPoint)
		{
			Debug.LogWarningFormat("Left terrain requires a TerrainIslandSpawnPoint for default vehicle location");
		}
		vehicle.transform.position = vehicle.m_SandboxItem.SnapPosToGrid(leftTerrain.m_SpawnPoint.transform.position);
		vehicle.transform.rotation = leftTerrain.m_SpawnPoint.transform.rotation;
	}

	private static void PlaceZedAxisVehicleAtDefaultStart(ZedAxisVehicle vehicle)
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (!leftTerrain || !rightTerrain)
		{
			Debug.LogWarningFormat("Incorrectly configured terrain when trying to place Zed Axis Vehicle");
			return;
		}
		Vector3 vector = new Vector3((leftTerrain.transform.position.x + rightTerrain.transform.position.x) / 2f, 0f, 0f);
		float num = Mathf.Max(leftTerrain.m_SpawnPoint.transform.position.y, rightTerrain.m_SpawnPoint.transform.position.y);
		if (vehicle.GetVehicleType() == ZedAxisVehicleType.PLANE)
		{
			vehicle.transform.position = GameGrid.SnapPosToGridForced(vector + new Vector3(0f, num + vehicle.m_MeshRenderer.bounds.size.y / 2f, 0f));
			return;
		}
		WaterBlock waterBlockThatIntersectsVerticalLine = WaterBlocks.GetWaterBlockThatIntersectsVerticalLine(vector.x);
		vehicle.transform.position = ((waterBlockThatIntersectsVerticalLine != null) ? GameGrid.SnapPosToGridForced(vector + new Vector3(0f, waterBlockThatIntersectsVerticalLine.m_Height, 0f)) : GameGrid.SnapPosToGrid(vector + new Vector3(0f, 4f, 0f)));
	}

	public static VehicleStopTrigger CreateGoalTriggerForVehicle(Vehicle vehicle)
	{
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (!rightTerrain)
		{
			Debug.LogWarningFormat("Could not find right terrain for placing vehicle goal flag");
			return null;
		}
		TerrainIslandSpawnPoint spawnPoint = rightTerrain.m_SpawnPoint;
		if (!spawnPoint)
		{
			Debug.LogWarningFormat("Right terrain requires a TerrainIslandSpawnPoint for default vehicle goal location");
		}
		Vector3 worldPos = (spawnPoint ? spawnPoint.transform.position : rightTerrain.transform.position);
		VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.CreateTrigger(Prefabs.m_Instance.m_VictoryFlag, vehicle.GetFlagColor(), vehicle.m_SandboxItem.SnapPosToGrid(worldPos), Quaternion.identity);
		vehicleStopTrigger.ResolveOverlap();
		vehicleStopTrigger.m_VehicleGuid = vehicle.m_Guid;
		vehicleStopTrigger.m_SandboxItem.SetFloatingTextToDefaultPosition();
		return vehicleStopTrigger;
	}

	public static bool WaterIsSelected()
	{
		if (WaterBlocks.m_WaterBlocks.Count == 0)
		{
			return false;
		}
		return SandboxSelectionSet.IsSelected(WaterBlocks.m_WaterBlocks[0].m_SandboxItem);
	}

	public static Color GetDefaultOutlineColor(SandboxItem item)
	{
		if (item.m_Type != SandboxItemType.BUILD_ZONE)
		{
			return GameUI.m_Instance.m_OutlineColorSandbox;
		}
		return GameUI.m_Instance.m_OutlineBuildZoneColor;
	}

	public static void CancelNewUnplacedItem()
	{
		if (m_NewUnPlacedItem != null)
		{
			EventEditor.DestroyPendingStage();
			m_NewUnPlacedItem.gameObject.SetActive(value: false);
			Object.Destroy(m_NewUnPlacedItem.gameObject);
			m_NewUnPlacedItem = null;
			WaterBlocks.UpdateManual();
		}
	}

	public static void CancelMovementDueToModalMenuOpening()
	{
		CancelNewUnplacedItem();
		if (GameInput.GetMouseButtonIsDown(0))
		{
			SandboxSelectionSet.RevertSelectionSetToStartPositions();
			SandboxSelectionSet.CancelSelection();
		}
	}

	public static SandboxItem CreateVehicle(Vector3 pos, GameObject prefab, string modId)
	{
		Vehicle vehicle = Vehicles.CreateVehicle(prefab, modId, pos, Quaternion.identity, Utils.GenerateUniqueId());
		if (!vehicle)
		{
			return null;
		}
		SandboxItem component = vehicle.GetComponent<SandboxItem>();
		if (!component)
		{
			return null;
		}
		vehicle.ApplyRandomSkin();
		vehicle.DisableMeshRendering();
		vehicle.ShowCenterOfMassIcon(Sandbox.m_ShowVehicleCenterOfMass);
		EventEditor.CreatePendingStage(vehicle.gameObject, EventUnitType.VEHICLE);
		return component;
	}

	public static SandboxItem CreateZedAxisVehicle(Vector3 pos, GameObject prefab, string modId)
	{
		ZedAxisVehicle zedAxisVehicle = ZedAxisVehicles.CreateVehicle(prefab, modId, pos, Quaternion.identity, Utils.GenerateUniqueId());
		if (!zedAxisVehicle)
		{
			return null;
		}
		SandboxItem component = zedAxisVehicle.GetComponent<SandboxItem>();
		if (!component)
		{
			return null;
		}
		zedAxisVehicle.m_MeshRenderer.gameObject.SetActive(value: false);
		EventEditor.CreatePendingStage(zedAxisVehicle.gameObject, EventUnitType.ZED_AXIS_VEHICLE);
		return component;
	}

	public static SandboxItem CreateDecor(Vector3 pos, GameObject prefab, string id, string modId)
	{
		Decor decor = Decors.Create(prefab, id, modId, pos, Quaternion.identity);
		if (!decor)
		{
			return null;
		}
		return decor.GetComponent<SandboxItem>();
	}

	private static void MoveUnplacedItemWithPointer(SandboxItem item)
	{
		Vector3 position = item.transform.position;
		Vector3 worldPointFromScreenPos = Utils.GetWorldPointFromScreenPos(item.m_OffsetFromPointer + GameInput.GetMousePosition());
		bool flag = !SandboxInput.m_ForceIgnoreGrid;
		if (item.m_Type == SandboxItemType.TERRAIN || item.m_Type == SandboxItemType.ROCK)
		{
			worldPointFromScreenPos.y = position.y;
		}
		Vector3 vector = item.SnapPosToGrid(worldPointFromScreenPos);
		if (TargetPosIsValid(item, vector))
		{
			item.transform.position = (flag ? vector : worldPointFromScreenPos);
			if (flag && item.m_Type == SandboxItemType.CUSTOM_SHAPE)
			{
				item.transform.position += item.GetComponent<CustomShape>().m_GridOffset;
			}
			if (flag && item.m_Type == SandboxItemType.BUILD_ZONE)
			{
				item.transform.position += item.GetComponent<BuildZone>().m_GridOffset;
			}
		}
		if (item.m_Type == SandboxItemType.TERRAIN)
		{
			item.GetComponent<TerrainIsland>().SetHeight(GameGrid.RoundToNearestGridSquare(Cameras.MainCamera().ScreenToWorldPoint(GameInput.GetMousePosition()).y) + TerrainIslands.GRID_ALIGN_OFFSET);
		}
	}

	private static bool TargetPosIsValid(SandboxItem item, Vector3 pos)
	{
		BridgeJoint component = item.GetComponent<BridgeJoint>();
		if ((bool)component && BridgeJoints.AnchorOverlapsPosition(pos, component, 2f * GameGrid.m_Spacing))
		{
			return false;
		}
		return true;
	}

	private static bool ShouldUndoPlacementOnClick()
	{
		if (m_NewUnPlacedItem.m_Type == SandboxItemType.VEHICLE || m_NewUnPlacedItem.m_Type == SandboxItemType.ZED_AXIS_VEHICLE)
		{
			return false;
		}
		return !NewUnPlacedItemHasMoved();
	}

	private static void PlayPlaceSound(SandboxItemType type)
	{
		switch (type)
		{
		case SandboxItemType.TERRAIN:
			InterfaceAudio.Play("ui_build_terrain_place");
			break;
		case SandboxItemType.ANCHOR:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.VEHICLE:
			InterfaceAudio.Play("ui_build_vehicle_place");
			break;
		case SandboxItemType.VEHICLE_STOP_TRIGGER:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.WATER:
			InterfaceAudio.Play("ui_build_terrain_place");
			break;
		case SandboxItemType.CHECKPOINT:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.PLATFORM:
			InterfaceAudio.Play("ui_build_terrain_place");
			break;
		case SandboxItemType.RAMP:
			InterfaceAudio.Play("ui_build_terrain_place");
			break;
		case SandboxItemType.HYDRAULICS_PHASE:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.VEHICLE_RESTART_PHASE:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.FLYING_OBJECT:
			InterfaceAudio.Play("ui_build_terrain_place");
			break;
		case SandboxItemType.ROCK:
			InterfaceAudio.Play("ui_build_terrain_place");
			break;
		case SandboxItemType.ZED_AXIS_VEHICLE:
			InterfaceAudio.Play("ui_build_vehicle_place");
			break;
		case SandboxItemType.BUILD_ZONE:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.CUSTOM_SHAPE:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.PILLAR:
		case SandboxItemType.DECOR:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		default:
			InterfaceAudio.Play("ui_build_generic_place");
			break;
		case SandboxItemType.IMPOSTER:
			break;
		}
	}

	private static void MaybePositionBetweenBookends(SandboxItem item)
	{
		switch (item.m_Type)
		{
		case SandboxItemType.ANCHOR:
			item.transform.position = GetValidPositionForAnchor(item.GetComponent<BridgeJoint>());
			break;
		case SandboxItemType.PLATFORM:
		case SandboxItemType.RAMP:
		case SandboxItemType.FLYING_OBJECT:
		case SandboxItemType.CUSTOM_SHAPE:
		case SandboxItemType.BUILD_ZONE:
		case SandboxItemType.PILLAR:
		case SandboxItemType.DECOR:
			item.transform.position = GetPositionForCenterOfScreen();
			break;
		default:
		{
			Vector3 position = GameGrid.SnapPosToGridForced(TerrainIslands.GetAveragePositionOfBookendSpawnPoints());
			if (item.m_Type == SandboxItemType.ROCK || item.m_Type == SandboxItemType.TERRAIN)
			{
				item.transform.position = new Vector3(position.x, item.transform.position.y, item.transform.position.z);
			}
			else
			{
				item.transform.position = position;
			}
			break;
		}
		case SandboxItemType.VEHICLE:
		case SandboxItemType.ZED_AXIS_VEHICLE:
			break;
		}
		if (item.m_Type == SandboxItemType.DECOR)
		{
			item.GetComponent<Decor>();
			item.GetComponent<Decor>().AdjustPlacementPosition();
		}
	}

	private static Vector3 GetPositionForCenterOfScreen()
	{
		return GameGrid.SnapPosToGridForced(Utils.GetWorldPointFromScreenPos(new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f)));
	}

	private static Vector3 GetValidPositionForAnchor(BridgeJoint anchor)
	{
		Vector3 positionForCenterOfScreen;
		for (positionForCenterOfScreen = GetPositionForCenterOfScreen(); BridgeJoints.AnchorOverlapsPosition(positionForCenterOfScreen, anchor, 2f * GameGrid.m_Spacing); positionForCenterOfScreen += new Vector3(GameGrid.m_Spacing, 0f, 0f))
		{
		}
		return positionForCenterOfScreen;
	}

	private static void CreateMiddleTerrainAnchors(TerrainIsland terrain)
	{
		float y = terrain.GetHeight() - 0.1f;
		BridgeJoint bridgeJoint = BridgeJoints.CreateAnchor(new Vector3(GameGrid.RoundToNearestGridSquare(terrain.transform.position.x - terrain.m_BoxCollider.bounds.size.x / 2f), y, 0f), Utils.GenerateUniqueId());
		bridgeJoint.m_SandboxItem = AddSandboxItemComponent(bridgeJoint.gameObject, SandboxItemType.ANCHOR);
		BridgeJoint bridgeJoint2 = BridgeJoints.CreateAnchor(new Vector3(GameGrid.RoundToNearestGridSquare(terrain.transform.position.x + terrain.m_BoxCollider.bounds.size.x / 2f), y, 0f), Utils.GenerateUniqueId());
		bridgeJoint2.m_SandboxItem = AddSandboxItemComponent(bridgeJoint2.gameObject, SandboxItemType.ANCHOR);
		BridgeJoints.ResolveOverlappingAnchors(Vector3.up);
	}

	private static bool PrioritizedListContainsVehicle()
	{
		foreach (SandboxItem prioritizedItem in m_PrioritizedItemList)
		{
			if (prioritizedItem.m_Type == SandboxItemType.VEHICLE)
			{
				return true;
			}
		}
		return false;
	}
}
