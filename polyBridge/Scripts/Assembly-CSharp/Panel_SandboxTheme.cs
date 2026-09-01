using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_SandboxTheme : MonoBehaviour
{
	public TMP_Dropdown m_Dropdown;

	private Dictionary<string, string> m_DropdownMap = new Dictionary<string, string>();

	private string m_PreloadingThemeId;

	private string m_PreviousThemeId;

	private void Awake()
	{
		m_Dropdown.onValueChanged.AddListener(delegate
		{
			OnThemeChanged();
		});
		m_Dropdown.alphaFadeSpeed = 0f;
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		PopulateThemes();
		if (Theme.m_Instance != null)
		{
			string localizedDisplayName = Theme.m_Instance.GetLocalizedDisplayName();
			DropdownUtils.SelectItem(m_Dropdown, localizedDisplayName);
		}
	}

	private void OnDisable()
	{
		if (GameUI.IsScreenDucked())
		{
			GameUI.UnDuckScreen();
		}
	}

	private void Update()
	{
	}

	public void Close()
	{
		InterfaceAudio.Play("ui_menubar_gen_off");
		base.gameObject.SetActive(value: false);
	}

	public void PopulateThemes()
	{
		m_Dropdown.ClearOptions();
		m_DropdownMap.Clear();
		List<string> list = new List<string>();
		if (ThemeStubs.m_Instance != null)
		{
			ThemePreloadStub[] themePreloadStubs = ThemeStubs.m_Instance.m_ThemePreloadStubs;
			foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
			{
				if (!themePreloadStub.m_ExcludeInRelease)
				{
					string text = Localize.Get(themePreloadStub.m_DisplayNameLocID);
					if (!list.Contains(text))
					{
						list.Add(text);
						m_DropdownMap.Add(text, themePreloadStub.m_ID);
					}
				}
			}
		}
		list.Sort();
		m_Dropdown.AddOptions(list);
	}

	private void OnThemeChanged()
	{
		string text = m_Dropdown.captionText.text;
		if (!m_DropdownMap.ContainsKey(text))
		{
			Debug.LogWarning("Could not find '" + text + "' in m_DropdownMap");
			return;
		}
		string themeId = m_DropdownMap[text];
		ThemeSelectedCallback(themeId);
	}

	private void ThemeSelectedCallback(string themeId)
	{
		m_PreviousThemeId = Theme.m_Instance.m_ThemeStub.m_ID;
		m_PreloadingThemeId = themeId;
		string addressableNameForId = ThemeStubs.m_Instance.GetAddressableNameForId(themeId);
		if (Prefabs.AsyncPrefabExists(addressableNameForId))
		{
			ThemePreloadedCallback(addressableNameForId, string.Empty, success: true);
			return;
		}
		Prefabs.m_Instance.PreloadSingleTheme(addressableNameForId, string.Empty, ThemePreloadedCallback);
		GameUI.DuckScreen();
	}

	private void ThemePreloadedCallback(string addressableName, string instanceID, bool success)
	{
		if (GameUI.IsScreenDucked())
		{
			GameUI.UnDuckScreen();
		}
		ThemeStub stubFromId = ThemeStubs.m_Instance.GetStubFromId(m_PreloadingThemeId);
		if (stubFromId != null && Theme.m_Instance.m_ThemeStub.m_ID != m_PreloadingThemeId)
		{
			SandboxLayoutData layoutData = SandboxLayout.SerializeToProxies();
			SandboxSelectionSet.CancelSelection();
			Sandbox.Clear();
			Sandbox.Load(stubFromId.m_ID, layoutData, loadBridge: true);
			SandboxUndo.Clear();
			SandboxUndo.SnapShot();
			TerrainIslands.ShrinkForSandboxMode(shrink: true);
			TerrainIslands.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
			Prefabs.ReleaseAsset(ThemeStubs.m_Instance.GetAddressableNameForId(m_PreviousThemeId));
		}
	}

	private void StickVehiclesToTerrain()
	{
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.SnapToTerrainSurface();
				VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.FindTriggerThatStopsVehicle(vehicle.m_Guid);
				if (vehicleStopTrigger != null)
				{
					vehicleStopTrigger.SnapToTerrainSurface();
					vehicle.UpdatePolygonShapes();
				}
			}
		}
	}
}
