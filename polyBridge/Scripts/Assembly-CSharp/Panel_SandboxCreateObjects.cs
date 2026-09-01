using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxCreateObjects : MonoBehaviour
{
	public Sprite m_CustomShapeSquareIcon;

	public Sprite m_CustomShapeCircleIcon;

	public Sprite m_CustomShapeNgonIcon;

	[Header("Object Icons")]
	public Sprite m_AnchorIcon;

	public Sprite m_PlatformIcon;

	public Sprite m_RampIcon;

	public Sprite m_TerrainIcon;

	public Sprite m_PillarIcon;

	[Header("Rock Icons")]
	public Sprite m_RockAIcon;

	public Sprite m_RockBIcon;

	public Sprite m_RockCIcon;

	public Sprite m_RockDIcon;

	[Header("Hot Air Balloon Icons")]
	public Sprite m_BoxBalloonIcon;

	public Sprite m_RoundBalloonIcon;

	public Sprite m_TriangleBalloonIcon;

	[Header("Build Zone Icons")]
	public Sprite m_BuildZoneRectIcon;

	public Sprite m_BuildZoneTriangleIcon;

	[Header("Prefabs")]
	public GameObject m_AnchorPrefab;

	public GameObject m_PlatformPrefab;

	public GameObject m_RampPrefab;

	public GameObject m_TerrainPrefab;

	public GameObject m_PillarPrefab;

	public GameObject m_HotAirBalloonPrefab;

	public GameObject m_BuildZoneRectPrefab;

	public GameObject m_BuildZoneTrianglePrefab;

	[Header("Rollouts")]
	public Rollout m_ObjectsRollout;

	public Rollout m_StaticPropsRollout;

	public Rollout m_DynamicPropsRollout;

	public Rollout m_CustomShapesRollout;

	public Rollout m_MyCustomShapesRollout;

	public Rollout m_UgcCustomShapesRollout;

	public Rollout m_RegionsRollout;

	[Header("Grids")]
	public GameObject m_ObjectsGrid;

	public GameObject m_StaticPropsGrid;

	public GameObject m_DynamicPropsGrid;

	public GameObject m_CustomShapesGrid;

	public GameObject m_MyCustomShapesGrid;

	public GameObject m_UgcCustomShapesGrid;

	public GameObject m_RegionsGrid;

	[Header("Buttons")]
	public Button m_EditMyCustomShapes;

	[Header("Scrolling")]
	public ScrollRect m_ScrollRect;

	private void Start()
	{
		PopulateObjects();
		PopulateStaticProps();
		PopulateDynamicProps();
		PopulateCustomShapes();
		PopulateMyCustomShapes();
		PopulateRegions();
		m_EditMyCustomShapes.onClick.AddListener(OnEditMyCustomShapes);
	}

	private void Update()
	{
		m_ScrollRect.enabled = SandboxItems.m_NewUnPlacedItem == null;
		string searchFilter = GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.GetSearchFilter();
		if (string.IsNullOrEmpty(searchFilter) && !string.IsNullOrEmpty(Panel_SandboxTabs.m_LastFilter))
		{
			CollapseAllRollouts();
		}
		if (searchFilter != Panel_SandboxTabs.m_LastFilter)
		{
			Panel_SandboxTabs.FilterItems(m_ObjectsRollout, m_ObjectsGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_StaticPropsRollout, m_StaticPropsGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_DynamicPropsRollout, m_DynamicPropsGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_CustomShapesRollout, m_CustomShapesGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_MyCustomShapesRollout, m_MyCustomShapesGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_UgcCustomShapesRollout, m_UgcCustomShapesGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_RegionsRollout, m_RegionsGrid, searchFilter);
		}
		GameUI.m_Instance.m_SandboxCreateObjects.m_EditMyCustomShapes.gameObject.SetActive(Panel_SandboxTabs.GetNumberOfThumbnailsInGrid(m_MyCustomShapesGrid) > 0);
		Panel_SandboxTabs.m_LastFilter = searchFilter;
	}

	private void PopulateObjects()
	{
		Panel_SandboxTabs.AddPrefabToGrid(m_ObjectsGrid, m_AnchorPrefab, m_AnchorIcon, "TOOLTIP_ANCHOR", m_AnchorPrefab.name, string.Empty, SandboxItemType.ANCHOR, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_ObjectsGrid, m_PlatformPrefab, m_PlatformIcon, "TOOLTIP_PLATFORM", m_PlatformPrefab.name, string.Empty, SandboxItemType.PLATFORM, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_ObjectsGrid, m_RampPrefab, m_RampIcon, "TOOLTIP_RAMP", m_RampPrefab.name, string.Empty, SandboxItemType.RAMP, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_ObjectsGrid, m_TerrainPrefab, m_TerrainIcon, "TOOLTIP_TERRAIN", m_TerrainPrefab.name, string.Empty, SandboxItemType.TERRAIN, showName: true);
	}

	public void PopulateStaticProps()
	{
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_Rocks[0], m_RockAIcon, "TOOLTIP_ROCK1", Prefabs.m_Instance.m_Rocks[0].name, string.Empty, SandboxItemType.ROCK, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_Rocks[1], m_RockBIcon, "TOOLTIP_ROCK2", Prefabs.m_Instance.m_Rocks[1].name, string.Empty, SandboxItemType.ROCK, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_Rocks[2], m_RockCIcon, "TOOLTIP_ROCK3", Prefabs.m_Instance.m_Rocks[2].name, string.Empty, SandboxItemType.ROCK, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_Rocks[3], m_RockDIcon, "TOOLTIP_ROCK4", Prefabs.m_Instance.m_Rocks[3].name, string.Empty, SandboxItemType.ROCK, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_FlyingObjects[0], m_BoxBalloonIcon, "TOOLTIP_HOT_AIR_BALLOON2", Prefabs.m_Instance.m_FlyingObjects[0].name, string.Empty, SandboxItemType.FLYING_OBJECT, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_FlyingObjects[1], m_RoundBalloonIcon, "TOOLTIP_HOT_AIR_BALLOON3", Prefabs.m_Instance.m_FlyingObjects[1].name, string.Empty, SandboxItemType.FLYING_OBJECT, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, Prefabs.m_Instance.m_FlyingObjects[2], m_TriangleBalloonIcon, "TOOLTIP_HOT_AIR_BALLOON4", Prefabs.m_Instance.m_FlyingObjects[2].name, string.Empty, SandboxItemType.FLYING_OBJECT, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_StaticPropsGrid, m_PillarPrefab, m_PillarIcon, "TOOLTIP_PILLAR", m_PillarPrefab.name, string.Empty, SandboxItemType.PILLAR, showName: true);
	}

	public void PopulateDynamicProps()
	{
		foreach (KeyValuePair<string, CustomShapesLibrarySlot> dynamicPropSlot in CustomShapesLibrary.m_DynamicPropSlots)
		{
			Panel_SandboxTabs.AddPrefabToGrid(m_DynamicPropsGrid, null, (dynamicPropSlot.Value.m_Sprite != null) ? dynamicPropSlot.Value.m_Sprite : m_CustomShapeNgonIcon, dynamicPropSlot.Value.m_DisplayNamLocID, dynamicPropSlot.Value.m_FullyQualifiedPath, string.Empty, SandboxItemType.CUSTOM_SHAPE, showName: true);
		}
	}

	public void PopulateCustomShapes()
	{
		if (CustomShapesLibrary.m_GameSlots.ContainsKey(CustomShapes.CUSTOM_SHAPE_NAME_SQUARE))
		{
			CustomShapesLibrarySlot customShapesLibrarySlot = CustomShapesLibrary.m_GameSlots[CustomShapes.CUSTOM_SHAPE_NAME_SQUARE];
			Panel_SandboxTabs.AddPrefabToGrid(m_CustomShapesGrid, null, m_CustomShapeSquareIcon, "UI_CUSTOM_SHAPE_SQUARE", customShapesLibrarySlot.m_FullyQualifiedPath, string.Empty, SandboxItemType.CUSTOM_SHAPE, showName: true);
		}
		if (CustomShapesLibrary.m_GameSlots.ContainsKey(CustomShapes.CUSTOM_SHAPE_NAME_CIRCLE))
		{
			CustomShapesLibrarySlot customShapesLibrarySlot2 = CustomShapesLibrary.m_GameSlots[CustomShapes.CUSTOM_SHAPE_NAME_CIRCLE];
			Panel_SandboxTabs.AddPrefabToGrid(m_CustomShapesGrid, null, m_CustomShapeCircleIcon, "UI_CUSTOM_SHAPE_CIRCLE", customShapesLibrarySlot2.m_FullyQualifiedPath, string.Empty, SandboxItemType.CUSTOM_SHAPE, showName: true);
		}
		if (CustomShapesLibrary.m_GameSlots.ContainsKey(CustomShapes.CUSTOM_SHAPE_NAME_NGON))
		{
			CustomShapesLibrarySlot customShapesLibrarySlot3 = CustomShapesLibrary.m_GameSlots[CustomShapes.CUSTOM_SHAPE_NAME_NGON];
			Panel_SandboxTabs.AddPrefabToGrid(m_CustomShapesGrid, null, m_CustomShapeNgonIcon, "UI_CUSTOM_SHAPE_NGON", customShapesLibrarySlot3.m_FullyQualifiedPath, string.Empty, SandboxItemType.CUSTOM_SHAPE, showName: true);
		}
	}

	public void PopulateRegions()
	{
		Panel_SandboxTabs.AddPrefabToGrid(m_RegionsGrid, m_BuildZoneRectPrefab, m_BuildZoneRectIcon, "TOOLTIP_BUILD_ZONE_RECTANGLE", m_BuildZoneRectPrefab.name, string.Empty, SandboxItemType.BUILD_ZONE, showName: true);
		Panel_SandboxTabs.AddPrefabToGrid(m_RegionsGrid, m_BuildZoneTrianglePrefab, m_BuildZoneTriangleIcon, "TOOLTIP_BUILD_ZONE_TRIANGLE", m_BuildZoneTriangleIcon.name, string.Empty, SandboxItemType.BUILD_ZONE, showName: true);
	}

	public void RemoveMyCustomShape(string id)
	{
		if (Panel_SandboxTabs.PrefabExistsInGrid(m_MyCustomShapesGrid, id))
		{
			Panel_SandboxTabs.RemovePrefabInGrid(m_MyCustomShapesGrid, id);
		}
	}

	public void PopulateMyCustomShapes()
	{
		List<CustomShapesLibrarySlot> list = new List<CustomShapesLibrarySlot>();
		foreach (KeyValuePair<string, CustomShapesLibrarySlot> localSlot in CustomShapesLibrary.m_LocalSlots)
		{
			list.Add(localSlot.Value);
		}
		list.Sort(SortByCustomLibrarySlotName);
		foreach (CustomShapesLibrarySlot item in list)
		{
			Panel_SandboxTabs.AddPrefabToGrid(m_MyCustomShapesGrid, null, m_CustomShapeNgonIcon, item.m_DisplayNamLocID, item.m_FullyQualifiedPath, string.Empty, SandboxItemType.CUSTOM_SHAPE, showName: true);
		}
		int num = 0;
		foreach (CustomShapesLibrarySlot item2 in list)
		{
			SandboxThumbnail byId = SandboxThumbnails.GetById(item2.m_FullyQualifiedPath);
			if (byId != null)
			{
				byId.transform.SetSiblingIndex(num++);
			}
		}
	}

	public void PopulateUgcCustomShapes()
	{
		foreach (KeyValuePair<string, CustomShapesLibrarySlot> uGCSlot in CustomShapesLibrary.m_UGCSlots)
		{
			Panel_SandboxTabs.AddPrefabToGrid(m_UgcCustomShapesGrid, null, m_CustomShapeNgonIcon, uGCSlot.Value.m_DisplayNamLocID, uGCSlot.Value.m_FullyQualifiedPath, string.Empty, SandboxItemType.CUSTOM_SHAPE, showName: true);
		}
		m_UgcCustomShapesRollout.gameObject.SetActive(CustomShapesLibrary.m_UGCSlots.Count > 0);
	}

	public void ClearUGC()
	{
		foreach (Transform item in m_UgcCustomShapesGrid.transform)
		{
			if (item.GetComponent<SandboxThumbnail>() != null)
			{
				item.gameObject.SetActive(value: false);
			}
		}
		m_UgcCustomShapesRollout.gameObject.SetActive(value: false);
	}

	private void CollapseAllRollouts()
	{
		m_ObjectsRollout.SetState(RolloutState.COLLAPSED);
		m_DynamicPropsRollout.SetState(RolloutState.COLLAPSED);
		m_CustomShapesRollout.SetState(RolloutState.COLLAPSED);
		m_MyCustomShapesRollout.SetState(RolloutState.COLLAPSED);
		m_UgcCustomShapesRollout.SetState(RolloutState.COLLAPSED);
	}

	public void OnEditMyCustomShapes()
	{
		GameUI.m_Instance.m_CustomShapesLibrary.gameObject.SetActive(value: true);
	}

	private int SortByCustomLibrarySlotName(CustomShapesLibrarySlot a, CustomShapesLibrarySlot b)
	{
		return Localize.Get(a.m_DisplayNamLocID).ToLower().CompareTo(Localize.Get(b.m_DisplayNamLocID).ToLower());
	}
}
