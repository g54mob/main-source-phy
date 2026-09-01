using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SandboxCreateVehicles : MonoBehaviour
{
	[Header("Rollouts")]
	public Rollout m_RolloutVehicles;

	public Rollout m_RolloutBoats;

	public Rollout m_RolloutPlanes;

	public Rollout m_RolloutUGCVehicles;

	public Rollout m_RolloutUGCBoats;

	public Rollout m_RolloutUGCPlanes;

	public Rollout m_RolloutLegacy;

	[Header("Grids")]
	public GameObject m_GridObjectVehicles;

	public GameObject m_GridObjectBoats;

	public GameObject m_GridObjectPlanes;

	public GameObject m_GridObjectUGCVehicles;

	public GameObject m_GridObjectUGCBoats;

	public GameObject m_GridObjectUGCPlanes;

	public GameObject m_GridObjectLegacy;

	[Header("Scrolling")]
	public ScrollRect m_ScrollRect;

	public void Start()
	{
		PopulateVehicles();
		PopulateZVehicles();
		m_RolloutLegacy.gameObject.SetActive(value: false);
	}

	public void Update()
	{
		m_ScrollRect.enabled = SandboxItems.m_NewUnPlacedItem == null;
		string searchFilter = GameUI.m_Instance.m_SandboxMenu.m_SandboxTabsPanel.GetSearchFilter();
		if (string.IsNullOrEmpty(searchFilter) && !string.IsNullOrEmpty(Panel_SandboxTabs.m_LastFilter))
		{
			CollapseAllRollouts();
		}
		if (searchFilter != Panel_SandboxTabs.m_LastFilter)
		{
			Panel_SandboxTabs.FilterItems(m_RolloutVehicles, m_GridObjectVehicles, searchFilter);
			Panel_SandboxTabs.FilterItems(m_RolloutBoats, m_GridObjectBoats, searchFilter);
			Panel_SandboxTabs.FilterItems(m_RolloutPlanes, m_GridObjectPlanes, searchFilter);
			Panel_SandboxTabs.FilterItems(m_RolloutUGCVehicles, m_GridObjectUGCVehicles, searchFilter);
			Panel_SandboxTabs.FilterItems(m_RolloutUGCBoats, m_GridObjectUGCBoats, searchFilter);
			Panel_SandboxTabs.FilterItems(m_RolloutUGCPlanes, m_GridObjectUGCPlanes, searchFilter);
			Panel_SandboxTabs.m_LastFilter = searchFilter;
		}
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_menubar_gen_off");
		base.gameObject.SetActive(value: false);
	}

	public void ClearUGC()
	{
		foreach (Transform item in m_GridObjectUGCVehicles.transform)
		{
			if (item.GetComponent<SandboxThumbnail>() != null)
			{
				item.gameObject.SetActive(value: false);
			}
		}
		foreach (Transform item2 in m_GridObjectUGCBoats.transform)
		{
			if (item2.GetComponent<SandboxThumbnail>() != null)
			{
				item2.gameObject.SetActive(value: false);
			}
		}
		foreach (Transform item3 in m_GridObjectUGCPlanes.transform)
		{
			if (item3.GetComponent<SandboxThumbnail>() != null)
			{
				item3.gameObject.SetActive(value: false);
			}
		}
		m_RolloutUGCVehicles.gameObject.SetActive(value: false);
		m_RolloutUGCBoats.gameObject.SetActive(value: false);
		m_RolloutUGCPlanes.gameObject.SetActive(value: false);
	}

	public void AddZedAxisUGC(ZedAxisVehicleStub stub, string modId)
	{
		GameObject grid = ((stub.m_Type == ZedAxisVehicleType.PLANE) ? m_GridObjectUGCPlanes : m_GridObjectUGCBoats);
		if (stub.m_Type == ZedAxisVehicleType.BOAT)
		{
			m_RolloutUGCBoats.gameObject.SetActive(value: true);
		}
		if (stub.m_Type == ZedAxisVehicleType.PLANE)
		{
			m_RolloutUGCPlanes.gameObject.SetActive(value: true);
		}
		Panel_SandboxTabs.AddAddressableToGrid(grid, stub.m_PrefabAddress, stub.m_Icon, stub.m_DisplayNameLocID, stub.m_PrefabAddress, modId, SandboxItemType.ZED_AXIS_VEHICLE, showName: true);
	}

	public void AddVehicleUGC(VehicleStub stub, string modId)
	{
		m_RolloutUGCVehicles.gameObject.SetActive(value: true);
		Panel_SandboxTabs.AddAddressableToGrid(m_GridObjectUGCVehicles, stub.m_PrefabAddress, stub.m_Icon, stub.m_DisplayNameLocID, stub.m_PrefabAddress, modId, SandboxItemType.VEHICLE, showName: true);
	}

	private void CollapseAllRollouts()
	{
		m_RolloutVehicles.SetState(RolloutState.COLLAPSED);
		m_RolloutPlanes.SetState(RolloutState.COLLAPSED);
		m_RolloutBoats.SetState(RolloutState.COLLAPSED);
		m_RolloutUGCVehicles.SetState(RolloutState.COLLAPSED);
		m_RolloutUGCBoats.SetState(RolloutState.COLLAPSED);
		m_RolloutUGCPlanes.SetState(RolloutState.COLLAPSED);
	}

	private void PopulateVehicles()
	{
		List<VehicleStub> list = new List<VehicleStub>();
		foreach (KeyValuePair<string, VehicleStub> item in VehicleStubs.m_StubsDict)
		{
			if (!item.Value.m_UGC)
			{
				list.Add(item.Value);
			}
		}
		list.Sort(SortByMass);
		foreach (VehicleStub item2 in list)
		{
			Panel_SandboxTabs.AddAddressableToGrid(item2.m_Legacy ? m_GridObjectLegacy : m_GridObjectVehicles, item2.m_PrefabAddress, item2.m_Icon, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.VEHICLE, showName: true);
		}
	}

	private void PopulateZVehicles()
	{
		List<ZedAxisVehicleStub> list = new List<ZedAxisVehicleStub>();
		foreach (KeyValuePair<string, ZedAxisVehicleStub> item in ZedAxisVehicleStubs.m_StubsDict)
		{
			if (!item.Value.m_UGC)
			{
				list.Add(item.Value);
			}
		}
		list.Sort(SortByMass);
		foreach (ZedAxisVehicleStub item2 in list)
		{
			GameObject gameObject = ((item2.m_Type == ZedAxisVehicleType.PLANE) ? m_GridObjectPlanes : m_GridObjectBoats);
			Panel_SandboxTabs.AddAddressableToGrid(item2.m_Legacy ? m_GridObjectLegacy : gameObject, item2.m_PrefabAddress, item2.m_Icon, item2.m_DisplayNameLocID, item2.m_PrefabAddress, string.Empty, SandboxItemType.ZED_AXIS_VEHICLE, showName: true);
		}
	}

	private int SortByMass(VehicleStub a, VehicleStub b)
	{
		return a.m_Mass.CompareTo(b.m_Mass);
	}

	private int SortByMass(ZedAxisVehicleStub a, ZedAxisVehicleStub b)
	{
		return a.m_Mass.CompareTo(b.m_Mass);
	}
}
