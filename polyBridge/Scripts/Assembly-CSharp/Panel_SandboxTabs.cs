using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_SandboxTabs : MonoBehaviour
{
	[Header("Tabs")]
	public SandboxTab m_VehiclesTab;

	public SandboxTab m_ObjectsTab;

	public SandboxTab m_SettingsTab;

	public SandboxTab m_DecorTab;

	[Header("Search")]
	public TMP_InputField m_SearchInputField;

	public Button m_SearchInputFieldGamepadButton;

	public static string m_LastFilter;

	private SandboxTab m_ActiveTab;

	private SandboxTab m_RestoreTabFromDecor;

	private Vector3 FOCUS_BUTTON_SCALE = new Vector3(1f, 1f, 1f);

	private readonly int SEARCH_INPUT_FIELD_CHAR_LIMIT = 16;

	private void Start()
	{
		m_VehiclesTab.m_Button.onClick.AddListener(OnVehiclesButton);
		m_ObjectsTab.m_Button.onClick.AddListener(OnObjectsButton);
		m_SettingsTab.m_Button.onClick.AddListener(OnSettingsButton);
		m_DecorTab.m_Button.onClick.AddListener(OnDecorButton);
		m_SearchInputFieldGamepadButton.onClick.AddListener(OnSearchInputFieldGamepadButton);
		m_SearchInputField.placeholder.GetComponent<TextMeshProUGUI>().text = Localize.Get("UI_ENTER_TEXT");
		m_SearchInputField.characterLimit = SEARCH_INPUT_FIELD_CHAR_LIMIT;
		SetActiveTab(m_VehiclesTab);
	}

	private void Update()
	{
		ProcessInput();
	}

	private void ProcessInput()
	{
		if (ActivePanels.None())
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
			{
				CycleToNextTab();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
			{
				CycleToPrevTab();
			}
		}
	}

	private void CycleToNextTab()
	{
		if (m_ActiveTab == m_VehiclesTab)
		{
			ExecuteEvents.Execute(m_ObjectsTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ActiveTab == m_ObjectsTab)
		{
			ExecuteEvents.Execute(m_SettingsTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ActiveTab == m_SettingsTab)
		{
			ExecuteEvents.Execute(m_DecorTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ActiveTab == m_DecorTab)
		{
			ExecuteEvents.Execute(m_VehiclesTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToPrevTab()
	{
		if (m_ActiveTab == m_VehiclesTab)
		{
			ExecuteEvents.Execute(m_DecorTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ActiveTab == m_ObjectsTab)
		{
			ExecuteEvents.Execute(m_VehiclesTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ActiveTab == m_SettingsTab)
		{
			ExecuteEvents.Execute(m_ObjectsTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ActiveTab == m_DecorTab)
		{
			ExecuteEvents.Execute(m_SettingsTab.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	public void UpdateForCurrentDevice()
	{
		m_SearchInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_SearchInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public void OnObjectsButton()
	{
		if (!m_ObjectsTab.m_Page.activeInHierarchy && !CameraInterpolate.IsActive())
		{
			SetActiveTab(m_ObjectsTab);
			GameStateManager.SwitchToState(GameState.SANDBOX);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	public void OnVehiclesButton()
	{
		if (!m_VehiclesTab.m_Page.activeInHierarchy && !CameraInterpolate.IsActive())
		{
			SetActiveTab(m_VehiclesTab);
			GameStateManager.SwitchToState(GameState.SANDBOX);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	public void OnSettingsButton()
	{
		if (!m_SettingsTab.m_Page.activeInHierarchy && !CameraInterpolate.IsActive())
		{
			SetActiveTab(m_SettingsTab);
			GameStateManager.SwitchToState(GameState.SANDBOX);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	public void OnDecorButton()
	{
		if (!CameraInterpolate.IsActive() && (!m_DecorTab.m_Page.activeInHierarchy || GameStateManager.GetState() != GameState.DECOR))
		{
			m_RestoreTabFromDecor = m_ActiveTab;
			SetActiveTab(m_DecorTab);
			GameStateManager.SwitchToState(GameState.DECOR);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	public void SelectDefaultTab()
	{
		SetActiveTab((m_RestoreTabFromDecor != null) ? m_RestoreTabFromDecor : m_VehiclesTab);
	}

	public bool DecorIsActiveTab()
	{
		return m_ActiveTab == m_DecorTab;
	}

	public bool SearchInputFieldHasFocus()
	{
		if (m_SearchInputField.gameObject.activeInHierarchy)
		{
			return m_SearchInputField.isFocused;
		}
		return false;
	}

	public string GetSearchFilter()
	{
		return m_SearchInputField.text.ToLower();
	}

	public static void FilterItems(Rollout rollout, GameObject grid, string filter)
	{
		int num = 0;
		for (int i = 0; i < grid.transform.childCount; i++)
		{
			Transform child = grid.transform.GetChild(i);
			SandboxThumbnail component = child.GetComponent<SandboxThumbnail>();
			if (component != null)
			{
				if (component.PassesFilter(filter))
				{
					child.gameObject.SetActive(value: true);
					num++;
				}
				else
				{
					child.gameObject.SetActive(value: false);
				}
			}
		}
		if (!string.IsNullOrEmpty(filter) && filter != m_LastFilter)
		{
			rollout.SetState((num <= 0) ? RolloutState.COLLAPSED : RolloutState.EXPANDED);
		}
	}

	public static void AddAddressableToGrid(GameObject grid, string prefabAddress, Sprite icon, string locId, string id, string modId, SandboxItemType sandboxType, bool showName)
	{
		SandboxThumbnail sandboxThumbnail = SandboxThumbnails.GetById(id);
		if (sandboxThumbnail == null)
		{
			sandboxThumbnail = SandboxThumbnails.Create(id, null, locId, icon, grid.transform);
		}
		if (sandboxThumbnail != null)
		{
			sandboxThumbnail.SetSprite(icon);
			sandboxThumbnail.gameObject.SetActive(value: true);
			sandboxThumbnail.AddSandboxListener(sandboxType, null, prefabAddress, modId);
			if (!showName)
			{
				sandboxThumbnail.AddToolTip(locId);
				sandboxThumbnail.m_ThumbnailName.gameObject.SetActive(value: false);
			}
		}
	}

	public static bool PrefabExistsInGrid(GameObject grid, string id)
	{
		for (int i = 0; i < grid.transform.childCount; i++)
		{
			SandboxThumbnail component = grid.transform.GetChild(i).GetComponent<SandboxThumbnail>();
			if (component != null && component.m_ID == id)
			{
				return true;
			}
		}
		return false;
	}

	public static void RemovePrefabInGrid(GameObject grid, string id)
	{
		for (int i = 0; i < grid.transform.childCount; i++)
		{
			SandboxThumbnail component = grid.transform.GetChild(i).GetComponent<SandboxThumbnail>();
			if (component != null && component.m_ID == id)
			{
				Object.Destroy(component.gameObject);
			}
		}
	}

	public static void AddPrefabToGrid(GameObject grid, GameObject prefab, Sprite icon, string locId, string id, string modId, SandboxItemType sandboxType, bool showName)
	{
		SandboxThumbnail sandboxThumbnail = SandboxThumbnails.GetById(id);
		if (sandboxThumbnail == null)
		{
			sandboxThumbnail = SandboxThumbnails.Create(id, null, locId, icon, grid.transform);
		}
		if (sandboxThumbnail != null)
		{
			sandboxThumbnail.gameObject.SetActive(value: true);
			sandboxThumbnail.AddSandboxListener(sandboxType, prefab, string.Empty, modId);
			if (!showName)
			{
				sandboxThumbnail.AddToolTip(locId);
				sandboxThumbnail.m_ThumbnailName.gameObject.SetActive(value: false);
			}
		}
	}

	public static int GetNumberOfThumbnailsInGrid(GameObject grid)
	{
		int num = 0;
		for (int i = 0; i < grid.transform.childCount; i++)
		{
			if (grid.transform.GetChild(i).GetComponent<SandboxThumbnail>() != null)
			{
				num++;
			}
		}
		return num;
	}

	private void SetActiveTab(SandboxTab tab)
	{
		m_VehiclesTab.m_Page.SetActive(tab == m_VehiclesTab);
		m_ObjectsTab.m_Page.SetActive(tab == m_ObjectsTab);
		m_SettingsTab.m_Page.SetActive(tab == m_SettingsTab);
		m_DecorTab.m_Page.SetActive(tab == m_DecorTab);
		m_VehiclesTab.m_Icon.color = ((tab == m_VehiclesTab) ? GameUI.m_Instance.m_HighlightedIconColor : GameUI.m_Instance.m_DuckedIconColor);
		m_ObjectsTab.m_Icon.color = ((tab == m_ObjectsTab) ? GameUI.m_Instance.m_HighlightedIconColor : GameUI.m_Instance.m_DuckedIconColor);
		m_SettingsTab.m_Icon.color = ((tab == m_SettingsTab) ? GameUI.m_Instance.m_HighlightedIconColor : GameUI.m_Instance.m_DuckedIconColor);
		m_DecorTab.m_Icon.color = ((tab == m_DecorTab) ? GameUI.m_Instance.m_HighlightedIconColor : GameUI.m_Instance.m_DuckedIconColor);
		m_VehiclesTab.m_Icon.transform.localScale = ((tab == m_VehiclesTab) ? FOCUS_BUTTON_SCALE : Vector3.one);
		m_ObjectsTab.m_Icon.transform.localScale = ((tab == m_ObjectsTab) ? FOCUS_BUTTON_SCALE : Vector3.one);
		m_SettingsTab.m_Icon.transform.localScale = ((tab == m_SettingsTab) ? FOCUS_BUTTON_SCALE : Vector3.one);
		m_DecorTab.m_Icon.transform.localScale = ((tab == m_DecorTab) ? FOCUS_BUTTON_SCALE : Vector3.one);
		m_VehiclesTab.m_Background.color = ((tab == m_VehiclesTab) ? GameUI.m_Instance.m_TabActiveColor : GameUI.m_Instance.m_TabInActiveColor);
		m_ObjectsTab.m_Background.color = ((tab == m_ObjectsTab) ? GameUI.m_Instance.m_TabActiveColor : GameUI.m_Instance.m_TabInActiveColor);
		m_SettingsTab.m_Background.color = ((tab == m_SettingsTab) ? GameUI.m_Instance.m_TabActiveColor : GameUI.m_Instance.m_TabInActiveColor);
		m_DecorTab.m_Background.color = ((tab == m_DecorTab) ? GameUI.m_Instance.m_TabActiveColor : GameUI.m_Instance.m_TabInActiveColor);
		m_VehiclesTab.m_BackgroundRectTransform.offsetMin = ((tab == m_VehiclesTab) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_ObjectsTab.m_BackgroundRectTransform.offsetMin = ((tab == m_ObjectsTab) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_SettingsTab.m_BackgroundRectTransform.offsetMin = ((tab == m_SettingsTab) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_DecorTab.m_BackgroundRectTransform.offsetMin = ((tab == m_DecorTab) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_VehiclesTab.m_Outline.color = ((tab == m_VehiclesTab) ? GameUI.m_Instance.m_TabOutlineActiveColor : GameUI.m_Instance.m_TabOutlineInActiveColor);
		m_ObjectsTab.m_Outline.color = ((tab == m_ObjectsTab) ? GameUI.m_Instance.m_TabOutlineActiveColor : GameUI.m_Instance.m_TabOutlineInActiveColor);
		m_SettingsTab.m_Outline.color = ((tab == m_SettingsTab) ? GameUI.m_Instance.m_TabOutlineActiveColor : GameUI.m_Instance.m_TabOutlineInActiveColor);
		m_DecorTab.m_Outline.color = ((tab == m_DecorTab) ? GameUI.m_Instance.m_TabOutlineActiveColor : GameUI.m_Instance.m_TabOutlineInActiveColor);
		m_ActiveTab = tab;
		m_LastFilter = string.Empty;
	}

	private void OnSearchInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_SearchInputField.text, m_SearchInputField.characterLimit, string.Empty, multiline: false, OnSearchInputFieldEntered);
	}

	private void OnSearchInputFieldEntered(string text)
	{
		if (text != null)
		{
			m_SearchInputField.text = text;
		}
	}
}
