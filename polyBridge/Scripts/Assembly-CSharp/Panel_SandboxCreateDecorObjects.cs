using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxCreateDecorObjects : MonoBehaviour
{
	[Header("Rollouts")]
	public Rollout m_RocksDecorRollout;

	public Rollout m_VikingDecorRollout;

	public Rollout m_ToxicDecorRollout;

	public Rollout m_SkyscraperDecorRollout;

	public Rollout m_NewDesertDecorRollout;

	public Rollout m_WallDecorRollout;

	public Rollout m_AlpineDecorRollout;

	public Rollout m_TropicsDecorRollout;

	public Rollout m_GlacierDecorRollout;

	public Rollout m_SantoriniDecorRollout;

	public Rollout m_SandboxDecorRollout;

	public Rollout m_PyramidsDecorRollout;

	public Rollout m_CyberDecorRollout;

	public Rollout m_DesktopDecorRollout;

	public Rollout m_TurnpikeDecorRollout;

	public Rollout m_MiscDecorRollout;

	public Rollout m_UgcDecorRollout;

	[Header("Grids")]
	public GameObject m_RocksDecorGrid;

	public GameObject m_VikingDecorGrid;

	public GameObject m_ToxicDecorGrid;

	public GameObject m_SkyscraperDecorGrid;

	public GameObject m_NewDesertDecorGrid;

	public GameObject m_WallDecorGrid;

	public GameObject m_AlpineDecorGrid;

	public GameObject m_TropicsDecorGrid;

	public GameObject m_GlacierDecorGrid;

	public GameObject m_SantoriniDecorGrid;

	public GameObject m_SandboxDecorGrid;

	public GameObject m_PyramidsDecorGrid;

	public GameObject m_CyberDecorGrid;

	public GameObject m_DesktopDecorGrid;

	public GameObject m_TurnpikeDecorGrid;

	public GameObject m_MiscDecorGrid;

	public GameObject m_UgcDecorGrid;

	[Header("Scrolling")]
	public ScrollRect m_ScrollRect;

	private void Start()
	{
		PopulateDecor();
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
			Panel_SandboxTabs.FilterItems(m_RocksDecorRollout, m_RocksDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_VikingDecorRollout, m_VikingDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_ToxicDecorRollout, m_ToxicDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_SkyscraperDecorRollout, m_SkyscraperDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_NewDesertDecorRollout, m_NewDesertDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_WallDecorRollout, m_WallDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_AlpineDecorRollout, m_AlpineDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_TropicsDecorRollout, m_TropicsDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_GlacierDecorRollout, m_GlacierDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_SantoriniDecorRollout, m_SantoriniDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_SandboxDecorRollout, m_SandboxDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_PyramidsDecorRollout, m_PyramidsDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_CyberDecorRollout, m_CyberDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_DesktopDecorRollout, m_DesktopDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_TurnpikeDecorRollout, m_TurnpikeDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_MiscDecorRollout, m_MiscDecorGrid, searchFilter);
			Panel_SandboxTabs.FilterItems(m_UgcDecorRollout, m_UgcDecorGrid, searchFilter);
		}
		Panel_SandboxTabs.m_LastFilter = searchFilter;
	}

	public void PopulateDecor()
	{
		List<DecorStub> list = new List<DecorStub>();
		foreach (KeyValuePair<string, DecorStub> item in DecorStubs.m_DecorStubsDict)
		{
			if (string.IsNullOrEmpty(item.Value.m_ModId))
			{
				list.Add(item.Value);
			}
		}
		list.Sort(SortByName);
		foreach (DecorStub item2 in list)
		{
			if (string.IsNullOrEmpty(item2.m_ModId))
			{
				switch (item2.m_Category)
				{
				case DecorCatgory.ROCKS:
					Panel_SandboxTabs.AddAddressableToGrid(m_RocksDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.VIKING:
					Panel_SandboxTabs.AddAddressableToGrid(m_VikingDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.TOXIC:
					Panel_SandboxTabs.AddAddressableToGrid(m_ToxicDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.SKYSCRAPER:
					Panel_SandboxTabs.AddAddressableToGrid(m_SkyscraperDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.NEW_DESERT:
					Panel_SandboxTabs.AddAddressableToGrid(m_NewDesertDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.WALL:
					Panel_SandboxTabs.AddAddressableToGrid(m_WallDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.ALPINE:
					Panel_SandboxTabs.AddAddressableToGrid(m_AlpineDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.TROPICS:
					Panel_SandboxTabs.AddAddressableToGrid(m_TropicsDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.GLACIER:
					Panel_SandboxTabs.AddAddressableToGrid(m_GlacierDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.SANTORINI:
					Panel_SandboxTabs.AddAddressableToGrid(m_SantoriniDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.SANDBOX:
					Panel_SandboxTabs.AddAddressableToGrid(m_SandboxDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.PYRAMIDS:
					Panel_SandboxTabs.AddAddressableToGrid(m_PyramidsDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.CYBER:
					Panel_SandboxTabs.AddAddressableToGrid(m_CyberDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.DESKTOP:
					Panel_SandboxTabs.AddAddressableToGrid(m_DesktopDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.TURNPIKE:
					Panel_SandboxTabs.AddAddressableToGrid(m_TurnpikeDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				case DecorCatgory.MISC:
					Panel_SandboxTabs.AddAddressableToGrid(m_MiscDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				default:
					Panel_SandboxTabs.AddAddressableToGrid(m_MiscDecorGrid, item2.m_PrefabAddress, item2.m_Sprite, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.DECOR, showName: true);
					break;
				}
			}
		}
	}

	public void AddDecorUGC(DecorStub stub, string modId)
	{
		m_UgcDecorRollout.gameObject.SetActive(value: true);
		Panel_SandboxTabs.AddAddressableToGrid(m_UgcDecorGrid, stub.m_PrefabAddress, stub.m_Sprite, stub.m_DisplayNameLocID, stub.m_PrefabAddress, stub.m_ModId, SandboxItemType.DECOR, showName: true);
	}

	public void ClearUGC()
	{
		foreach (Transform item in m_UgcDecorGrid.transform)
		{
			if (item.GetComponent<SandboxThumbnail>() != null)
			{
				item.gameObject.SetActive(value: false);
			}
		}
		m_UgcDecorRollout.gameObject.SetActive(value: false);
	}

	private void CollapseAllRollouts()
	{
		m_RocksDecorRollout.SetState(RolloutState.COLLAPSED);
		m_VikingDecorRollout.SetState(RolloutState.COLLAPSED);
		m_ToxicDecorRollout.SetState(RolloutState.COLLAPSED);
		m_SkyscraperDecorRollout.SetState(RolloutState.COLLAPSED);
		m_NewDesertDecorRollout.SetState(RolloutState.COLLAPSED);
		m_WallDecorRollout.SetState(RolloutState.COLLAPSED);
		m_AlpineDecorRollout.SetState(RolloutState.COLLAPSED);
		m_RocksDecorRollout.SetState(RolloutState.COLLAPSED);
		m_TropicsDecorRollout.SetState(RolloutState.COLLAPSED);
		m_GlacierDecorRollout.SetState(RolloutState.COLLAPSED);
		m_SantoriniDecorRollout.SetState(RolloutState.COLLAPSED);
		m_SandboxDecorRollout.SetState(RolloutState.COLLAPSED);
		m_PyramidsDecorRollout.SetState(RolloutState.COLLAPSED);
		m_CyberDecorRollout.SetState(RolloutState.COLLAPSED);
		m_DesktopDecorRollout.SetState(RolloutState.COLLAPSED);
		m_TurnpikeDecorRollout.SetState(RolloutState.COLLAPSED);
		m_UgcDecorRollout.SetState(RolloutState.COLLAPSED);
	}

	private int SortByName(DecorStub a, DecorStub b)
	{
		return Localize.Get(a.m_DisplayNameLocID).CompareTo(Localize.Get(b.m_DisplayNameLocID));
	}
}
