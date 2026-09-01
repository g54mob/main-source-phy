using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxEditTerrain : MonoBehaviour
{
	public SandboxPropertiesHeader m_SandboxPropertiesHeader;

	[Header("Nudge")]
	public Panel_SandboxNudge m_SandboxNudge;

	[Header("Input Fields")]
	public SandboxInputField m_InputFieldPosX;

	[Header("Buttons")]
	public Button m_Duplicate;

	public Button m_Delete;

	[Header("Sliders")]
	public SandboxTapeSlider m_SliderStretch;

	[Header("Toggles")]
	public Toggle m_LockPositionToggle;

	public Toggle m_HiddenToggle;

	[Header("Style")]
	public GameObject m_StylePanel;

	public SandboxStylePicker m_StylePicker;

	private TerrainIsland m_LastRefreshedTerrain;

	private float NUM_SECONDS_TO_DISPLAY_VARIANT = 2f;

	private PointerEvents m_LockPositionTogglePointerEvents;

	private PointerEvents m_HiddenTogglePointerEvents;

	private bool m_SkipInputFieldUpdateFromSlider;

	private void Awake()
	{
		m_LockPositionTogglePointerEvents = m_LockPositionToggle.GetComponent<PointerEvents>();
		m_LockPositionTogglePointerEvents.RegisterOnClickedDelegate(OnLockPositionToggle);
		m_HiddenTogglePointerEvents = m_HiddenToggle.GetComponent<PointerEvents>();
		m_HiddenTogglePointerEvents.RegisterOnClickedDelegate(OnHiddenToggle);
		m_SliderStretch.SetRange(TerrainIslands.MIN_HEIGHT_SLIDER, TerrainIslands.MAX_HEIGHT_SLIDER, GameGrid.m_Spacing);
		m_SliderStretch.SetCallback(HeightSliderChanged);
	}

	private void Update()
	{
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if ((bool)selectedTerrain && selectedTerrain != m_LastRefreshedTerrain)
		{
			RefreshProperties(selectedTerrain);
			StyleInitialize(selectedTerrain);
		}
		ProcessInput();
	}

	private void OnEnable()
	{
		m_Duplicate.onClick.AddListener(OnDuplicate);
		m_Delete.onClick.AddListener(OnDelete);
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if ((bool)selectedTerrain)
		{
			float defaultTerrainHeight = Theme.m_Instance.m_ThemeStub.m_DefaultTerrainHeight;
			if (!Mathf.Approximately(defaultTerrainHeight, 0f))
			{
				float min = Mathf.Max(TerrainIslands.MIN_HEIGHT, Theme.m_Instance.m_ThemeStub.m_DefaultTerrainHeight - (float)Mathf.RoundToInt(defaultTerrainHeight) * 0.6f);
				float max = Mathf.Min(TerrainIslands.MAX_HEIGHT, Theme.m_Instance.m_ThemeStub.m_DefaultTerrainHeight + (float)Mathf.RoundToInt(defaultTerrainHeight) * 0.6f);
				m_SliderStretch.SetRange(min, max, GameGrid.m_Spacing);
			}
			else
			{
				m_SliderStretch.SetRange(TerrainIslands.MIN_HEIGHT_SLIDER, TerrainIslands.MAX_HEIGHT_SLIDER, GameGrid.m_Spacing);
			}
			RefreshProperties(selectedTerrain);
			StyleInitialize(selectedTerrain);
		}
	}

	private void OnDisable()
	{
		m_LastRefreshedTerrain = null;
		m_Duplicate.onClick.RemoveAllListeners();
		m_Delete.onClick.RemoveAllListeners();
	}

	public void UpdateForCurrentDevice()
	{
		m_SandboxNudge.UpdateForCurrentDevice();
	}

	public void SkipInputFieldUpdateFromSlider()
	{
		m_SkipInputFieldUpdateFromSlider = true;
	}

	public void ForceRefresh()
	{
		m_LastRefreshedTerrain = null;
	}

	public void RefreshProperties(TerrainIsland terrain)
	{
		RefreshPosition(terrain);
		RefreshButtons(terrain);
		RefreshToggles(terrain);
		RefreshSliders(terrain);
		m_SandboxPropertiesHeader.gameObject.SetActive(terrain.m_TerrainIslandType == TerrainIslandType.Middle);
		m_LastRefreshedTerrain = terrain;
	}

	public void RefreshPosition(TerrainIsland terrainIsland)
	{
		m_InputFieldPosX.m_InputField.text = Utils.FormatThreeDecimalPlaces(terrainIsland.transform.position.x);
	}

	private void RefreshButtons(TerrainIsland terrain)
	{
		if ((bool)terrain && terrain.m_TerrainIslandType == TerrainIslandType.Middle)
		{
			m_Duplicate.gameObject.SetActive(value: true);
			m_Delete.gameObject.SetActive(value: true);
		}
		else
		{
			m_Duplicate.gameObject.SetActive(value: false);
			m_Delete.gameObject.SetActive(value: false);
		}
	}

	private void RefreshToggles(TerrainIsland terrain)
	{
		m_LockPositionToggle.isOn = terrain.m_LockPosition;
		m_HiddenToggle.isOn = terrain.m_Hidden;
		m_HiddenToggle.transform.parent.gameObject.SetActive(terrain.m_TerrainIslandType == TerrainIslandType.Bookend);
	}

	private void RefreshSliders(TerrainIsland terrain)
	{
		m_SliderStretch.SetValue(terrain.GetHeight());
		m_SliderStretch.m_SandboxInputField.m_InputField.text = terrain.FormatHeight();
	}

	private void OnPickStyle(int index)
	{
		InterfaceAudio.Play("ui_menu_select");
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if ((bool)selectedTerrain)
		{
			switch (selectedTerrain.m_TerrainIslandType)
			{
			case TerrainIslandType.Middle:
				PickMiddleIslandStyle(selectedTerrain, index);
				break;
			case TerrainIslandType.Bookend:
				PickBookendStyle(selectedTerrain, index);
				break;
			default:
				Debug.LogWarningFormat("Unexpected terrain island type {0}", selectedTerrain.m_TerrainIslandType.ToString());
				break;
			}
		}
	}

	private void PickMiddleIslandStyle(TerrainIsland currentTerrain, int index)
	{
		TerrainIsland terrainIsland = currentTerrain.Duplicate(Theme.m_Instance.GetTerrainIslandPrefab(TerrainIslandType.Middle, index), Vector3.zero);
		if ((bool)terrainIsland)
		{
			SandboxSelectionSet.ForceSelection(terrainIsland.m_SandboxItem);
			terrainIsland.DisplayFullMesh(NUM_SECONDS_TO_DISPLAY_VARIANT);
			TerrainIslands.DestroyTerrain(currentTerrain);
			SandboxUndo.SnapShot();
		}
	}

	private void PickBookendStyle(TerrainIsland currentTerrain, int index)
	{
		TerrainIsland terrainIsland = currentTerrain.Duplicate(Theme.m_Instance.GetTerrainIslandPrefab(TerrainIslandType.Bookend, index), Vector3.zero);
		if ((bool)terrainIsland)
		{
			SandboxSelectionSet.ForceSelection(terrainIsland.m_SandboxItem);
			terrainIsland.DisplayFullMesh(NUM_SECONDS_TO_DISPLAY_VARIANT);
			TerrainIslands.DestroyTerrain(currentTerrain);
			SandboxUndo.SnapShot();
		}
	}

	private void OnDuplicate()
	{
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if (!selectedTerrain)
		{
			return;
		}
		int terrainPrefabIndex = Theme.m_Instance.GetTerrainPrefabIndex(selectedTerrain.m_TerrainIslandType, selectedTerrain.name);
		if (terrainPrefabIndex != -1)
		{
			TerrainIsland terrainIsland = selectedTerrain.Duplicate(Theme.m_Instance.GetTerrainIslandPrefab(selectedTerrain.m_TerrainIslandType, terrainPrefabIndex), new Vector3(selectedTerrain.m_BoxCollider.size.x, 0f, 0f));
			if ((bool)terrainIsland)
			{
				InterfaceAudio.Play("ui_build_terrain_place");
				SandboxSelectionSet.ForceSelection(terrainIsland.m_SandboxItem);
				SandboxUndo.SnapShot();
			}
		}
	}

	private void OnDelete()
	{
		if ((bool)SandboxSelectionSet.GetSelectedTerrain())
		{
			InterfaceAudio.Play("ui_build_delete");
			SandboxSelectionSet.Delete();
		}
	}

	private void OnLockPositionToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if ((bool)selectedTerrain)
		{
			selectedTerrain.m_LockPosition = m_LockPositionToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void OnHiddenToggle()
	{
		InterfaceAudio.Play("ui_settings_toggle");
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if ((bool)selectedTerrain)
		{
			selectedTerrain.m_Hidden = m_HiddenToggle.isOn;
			SandboxUndo.SnapShot();
		}
	}

	private void HeightSliderChanged(float height)
	{
		if (m_SkipInputFieldUpdateFromSlider)
		{
			m_SkipInputFieldUpdateFromSlider = false;
			return;
		}
		TerrainIsland selectedTerrain = SandboxSelectionSet.GetSelectedTerrain();
		if ((bool)selectedTerrain)
		{
			height = Mathf.Clamp(height, TerrainIslands.MIN_HEIGHT, TerrainIslands.MAX_HEIGHT);
			float heightAdded = selectedTerrain.m_HeightAdded;
			selectedTerrain.SetHeight(height);
			float num = selectedTerrain.m_HeightAdded - heightAdded;
			if (selectedTerrain.m_OverlappingAnchors.Count > 0)
			{
				BridgeJoints.ResolveOverlappingAnchors((num > 0f) ? Vector3.up : Vector3.down);
			}
			m_SliderStretch.m_SandboxInputField.m_InputField.text = selectedTerrain.FormatHeight();
		}
	}

	private void StyleInitialize(TerrainIsland terrain)
	{
		int numTerrainIslandPrefabs = Theme.m_Instance.GetNumTerrainIslandPrefabs(terrain.m_TerrainIslandType);
		m_StylePanel.SetActive(numTerrainIslandPrefabs > 1);
		if (numTerrainIslandPrefabs > 1)
		{
			m_StylePicker.CreateButtons(numTerrainIslandPrefabs, OnPickStyle);
			for (int i = 0; i < numTerrainIslandPrefabs; i++)
			{
				m_StylePicker.SetButtonText(i, (i + 1).ToString());
			}
			int terrainPrefabIndex = Theme.m_Instance.GetTerrainPrefabIndex(terrain.m_TerrainIslandType, terrain.name);
			m_StylePicker.Select(terrainPrefabIndex);
		}
	}

	private void ProcessInput()
	{
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			ExecuteEvents.Execute(m_Delete.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			ExecuteEvents.Execute(m_Duplicate.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}
}
