using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopFilterBar : MonoBehaviour
{
	[Header("Filters")]
	public TMP_Dropdown m_SortByDropdown;

	public TMP_Dropdown m_OverTimePeriodDropdown;

	public TMP_Dropdown m_OverTimePeriodDropdownInactive;

	[Header("Tags")]
	public Transform m_TagsRoot;

	public Button m_TagsButton;

	public Button m_TagsButtonInactive;

	public GameObject m_TagTogglePrefab;

	public Transform m_TagToggleContent;

	public TextMeshProUGUI m_TagCaption;

	public TextMeshProUGUI m_TagCaptionInactive;

	public GameObject m_TagsDucking;

	[Header("Search")]
	public GameObject m_SearchBar;

	public GameObject m_SearchBarInactive;

	public TMP_InputField m_SearchInputField;

	public Button m_SearchInputFieldGamepadButton;

	public TMP_InputField m_SearchInputFieldInactive;

	[NonSerialized]
	public WorkshopSortOrder m_WorkshopSortOrder = WorkshopSortOrder.MOST_LIKED;

	[NonSerialized]
	public WorkshopOverTimePeriod m_WorkshopTimePeriod;

	[NonSerialized]
	public string m_SearchText = string.Empty;

	[NonSerialized]
	public ulong m_SteamIdForSearch;

	private ulong m_SteamIDOwner;

	private string m_SteamNameOwner;

	private readonly float AUTOMATIC_SEARCH_UPDATE_TIME_SECONDS = 1f;

	private float m_LastAutomaticSearchUpdateTime;

	private static readonly string WORKSHOP_TAGS_FILENAME = ".workshoptags";

	private WorkshopView m_InitiailizedForView = WorkshopView.NONE;

	private void Awake()
	{
		m_TagsButton.onClick.AddListener(OnTagsButton);
		m_SortByDropdown.onValueChanged.AddListener(delegate
		{
			OnSortByChanged();
		});
		m_SortByDropdown.alphaFadeSpeed = 0f;
		m_OverTimePeriodDropdown.onValueChanged.AddListener(delegate
		{
			OnOverTimePeriodChanged();
		});
		m_OverTimePeriodDropdown.alphaFadeSpeed = 0f;
		m_SearchInputField.onEndEdit.AddListener(delegate
		{
			OnSearch();
		});
		m_SearchInputFieldGamepadButton.onClick.AddListener(OnSearchInputFieldGamepadButton);
		CreateTagToggles();
		SetOverTimeDropdownVisibility();
		SetSearchBarVisibility();
		ClearSearch();
	}

	public void OnEnableManual(WorkshopView workshopView)
	{
		if (m_InitiailizedForView != workshopView)
		{
			LoadTags();
			PopulateSortByDropdown(string.Empty, workshopView);
			PopulateOverTimePeriodDropdown();
			m_SearchInputField.placeholder.GetComponent<TextMeshProUGUI>().text = Localize.Get("UI_WORKSHOP_SEARCH_PROMPT");
			m_SearchInputFieldInactive.placeholder.GetComponent<TextMeshProUGUI>().text = string.Empty;
			WorkshopTags.m_TagToggles[WorkshopTagType.SOLVED].m_Label.gameObject.AddComponent<ToolTipText>().m_RawLocalizationKey = "UI_WORKSHOPSUBMIT_AUTOPLAY_TOOLTIP";
			m_WorkshopSortOrder = ((workshopView == WorkshopView.MODS) ? Profiles.m_ActiveProfile.m_WorkshopModItemsSortBy : Profiles.m_ActiveProfile.m_WorkshopItemsSortBy);
			DropdownUtils.SelectItem(m_SortByDropdown, (int)m_WorkshopSortOrder);
			SetTagsDropdownVisibility();
			SetOverTimeDropdownVisibility();
			m_InitiailizedForView = workshopView;
		}
	}

	public void Update()
	{
		if (Input.anyKeyDown || GamepadManager.ButtonJustPressed(GamepadButtonType.SOUTH))
		{
			m_LastAutomaticSearchUpdateTime = Time.unscaledTime;
		}
		MaybeUpdateSearchText();
		UpdateTagsCaption();
		ProcessInput();
	}

	public void UpdateForCurrentDevice()
	{
		m_SearchInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_SearchInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public void EnableTagsForType(WorkshopTagMode toggleType)
	{
		for (int i = 0; i < m_TagToggleContent.childCount; i++)
		{
			WorkshopTagToggle component = m_TagToggleContent.GetChild(i).GetComponent<WorkshopTagToggle>();
			if (component != null)
			{
				component.gameObject.SetActive(component.m_ToggleMode == toggleType);
				component.m_Label.text = Localize.Get(component.m_LabelLocalizationKey);
			}
		}
	}

	public void ShowAllItemsForCreator(WorkshopItem ownerItem)
	{
		ClearSearch();
		m_SteamIDOwner = ownerItem.GetSteamId();
		m_SteamNameOwner = ownerItem.GetCreatorName();
		PopulateSortByDropdown(Localize.Get("UI_WORKSHOP_BY", m_SteamNameOwner), GameUI.m_Instance.m_Workshop.IsOnModsTab() ? WorkshopView.MODS : WorkshopView.LEVELS_AND_CAMPAIGNS);
		m_SortByDropdown.value = m_SortByDropdown.options.Count - 1;
	}

	public void SetFilters(WorkshopQueryFilter filter)
	{
		m_SortByDropdown.SetValueWithoutNotify((int)filter.m_SortOrder);
		m_OverTimePeriodDropdown.SetValueWithoutNotify((int)filter.m_OverTimePeriod);
	}

	public void SetWorkshopSortOrder(WorkshopSortOrder sortOrder)
	{
		m_WorkshopSortOrder = sortOrder;
		m_SortByDropdown.SetValueWithoutNotify((int)sortOrder);
		SetOverTimeDropdownVisibility();
		SetSearchBarVisibility();
	}

	public void ClearSearch()
	{
		m_SearchText = string.Empty;
		m_SearchInputField.text = string.Empty;
	}

	public void ForceSearch(string text)
	{
		m_SearchText = text;
		m_SearchInputField.text = text;
	}

	public void SetTagsDropdownVisibility()
	{
		m_TagsButton.gameObject.SetActive(m_WorkshopSortOrder != WorkshopSortOrder.MOST_RECENTLY_PLAYED);
		m_TagCaption.gameObject.SetActive(m_TagsButton.gameObject.activeInHierarchy);
		m_TagsButtonInactive.gameObject.SetActive(!m_TagsButton.gameObject.activeInHierarchy);
		m_TagCaptionInactive.gameObject.SetActive(!m_TagsButton.gameObject.activeInHierarchy);
	}

	public bool DoesManualSearch()
	{
		if (m_WorkshopSortOrder != WorkshopSortOrder.MOST_RECENTLY_PLAYED)
		{
			return Workshop.IsUserUGCQuery(m_WorkshopSortOrder);
		}
		return true;
	}

	private void PopulateSortByDropdown(string byCustom, WorkshopView workshopView)
	{
		List<string> list = new List<string>();
		list.Add(Localize.Get("UI_WORKSHOP_MOST_RECENT"));
		list.Add(Localize.Get("UI_WORKSHOP_MOST_ENDORSED"));
		list.Add(Localize.Get("UI_WORKSHOP_MOST_SUBSCRIBED"));
		list.Add(Localize.Get("UI_WORKSHOP_SUBSCRIBED_BY_ME"));
		list.Add(Localize.Get("UI_WORKSHOP_CREATED_BY_ME"));
		list.Add(Localize.Get("UI_WORKSHOP_FAVORITED_BY_ME"));
		list.Add(Localize.Get("UI_WORKSHOP_CREATED_BY_FRIENDS"));
		list.Add(Localize.Get("UI_WORKSHOP_FAVORITED_BY_FRIENDS"));
		if (workshopView == WorkshopView.LEVELS_AND_CAMPAIGNS)
		{
			list.Add(Localize.Get("UI_PLAYED_BY_ME"));
		}
		if (!string.IsNullOrEmpty(byCustom))
		{
			list.Add(byCustom);
		}
		m_SortByDropdown.ClearOptions();
		m_SortByDropdown.AddOptions(list);
	}

	private void PopulateOverTimePeriodDropdown()
	{
		List<string> list = new List<string>();
		list.Add(Localize.Get("UI_WORKSHOP_ALL_TIME"));
		list.Add(Localize.Get("UI_WORKSHOP_TODAY"));
		list.Add(Localize.Get("UI_WORKSHOP_PAST_WEEK"));
		list.Add(Localize.Get("UI_WORKSHOP_PAST_MONTH"));
		list.Add(Localize.Get("UI_WORKSHOP_PAST_YEAR"));
		m_OverTimePeriodDropdown.ClearOptions();
		m_OverTimePeriodDropdown.AddOptions(list);
		m_OverTimePeriodDropdownInactive.ClearOptions();
		m_OverTimePeriodDropdownInactive.AddOptions(list);
	}

	private void CreateTagToggles()
	{
		CreateTagToggle(WorkshopTagType.SOLVED, WorkshopTags.AUTOPLAY_TAG, "TAG_AUTOPLAY", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.UNBREAKABLE, WorkshopTags.UNBREAKABLE_TAG, "TAG_UNBREAKABLE", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.REQUIRES_MODS, WorkshopTags.REQUIRES_MODS, "TAG_REQUIRES_MODS", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.HYDRAULICS, WorkshopTags.HYDRAULICS_TAG, "TAG_HYDRAULICS", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.HYDRAULIC_CONTROLLER, WorkshopTags.HYDRAULIC_CONTROLLER_TAG, "TAG_HYDRAULIC_CONTROLLER", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.SPRINGS, WorkshopTags.SPRINGS_TAG, "TAG_SPRINGS", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.BUILD_REGIONS, WorkshopTags.BUILD_REGIONS_TAG, "TAG_BUILD_REGIONS", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.PREBUILDS, WorkshopTags.PREBUILDS_TAG, "TAG_PREBUILDS", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.CUSTOM_SHAPES, WorkshopTags.CUSTOM_SHAPES_TAG, "TAG_CUSTOM_SHAPES", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.ALLOWFEATURED_TAG, WorkshopTags.ALLOWFEATURED_TAG, "TAG_ALLOW_FEATURED", WorkshopTagMode.LEVEL);
		CreateTagToggle(WorkshopTagType.AFFECTS_GAMEPLAY, WorkshopTags.AFFECTS_GAMEPLAY_TAG, "TAG_AFFECTS_GAMEPLAY", WorkshopTagMode.MOD);
		CreateTagToggle(WorkshopTagType.LANGUAGE, WorkshopTags.LANGUAGE_TAG, "TAG_LANGUAGE", WorkshopTagMode.MOD);
		CreateTagToggle(WorkshopTagType.UGC_VEHICLES, WorkshopTags.UGC_VEHICLES_TAG, "TAG_UGC_VEHICLES", WorkshopTagMode.MOD);
		CreateTagToggle(WorkshopTagType.UGC_BOATS_PLANES, WorkshopTags.UGC_BOATS_PLANES_TAG, "TAG_UGC_BOATS_PLANES", WorkshopTagMode.MOD);
		CreateTagToggle(WorkshopTagType.UGC_DECOR, WorkshopTags.UGC_DECOR_TAG, "TAG_UGC_DECOR", WorkshopTagMode.MOD);
		CreateTagToggle(WorkshopTagType.UGC_CUSTOM_SHAPES, WorkshopTags.UGC_CUSTOM_SHAPES_TAG, "TAG_UGC_CUSTOM_SHAPES", WorkshopTagMode.MOD);
		WorkshopTags.m_TagToggles[WorkshopTagType.SOLVED].m_ExcludeToggle.isOn = true;
		WorkshopTags.m_TagToggles[WorkshopTagType.UNBREAKABLE].m_ExcludeToggle.isOn = true;
		WorkshopTags.m_TagToggles[WorkshopTagType.REQUIRES_MODS].m_ExcludeToggle.isOn = true;
		WorkshopTags.m_TagToggles[WorkshopTagType.AFFECTS_GAMEPLAY].m_ExcludeToggle.isOn = true;
		WorkshopTags.m_TagToggles[WorkshopTagType.SOLVED].m_Label.gameObject.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
	}

	private void CreateTagToggle(WorkshopTagType toggleType, string tagName, string localizationKey, WorkshopTagMode toggleMode)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(m_TagTogglePrefab, m_TagToggleContent);
		if (gameObject != null)
		{
			WorkshopTagToggle component = gameObject.GetComponent<WorkshopTagToggle>();
			component.m_TagType = toggleType;
			component.m_TagName = tagName;
			component.m_LabelLocalizationKey = localizationKey;
			component.m_Label.text = Localize.Get(localizationKey);
			component.m_ToggleMode = toggleMode;
			gameObject.SetActive(value: false);
			WorkshopTags.m_TagToggles.Add(toggleType, component);
		}
	}

	private void OnSortByChanged()
	{
		m_WorkshopSortOrder = (WorkshopSortOrder)m_SortByDropdown.value;
		if (m_WorkshopSortOrder == WorkshopSortOrder.BY_NAME)
		{
			m_SteamIdForSearch = m_SteamIDOwner;
		}
		else
		{
			m_SteamIdForSearch = 0uL;
		}
		if (GameUI.m_Instance.m_Workshop.GetWorkshopView() == WorkshopView.MODS)
		{
			if (Profiles.m_ActiveProfile.m_WorkshopModItemsSortBy != m_WorkshopSortOrder)
			{
				Profiles.m_ActiveProfile.m_WorkshopModItemsSortBy = ((m_WorkshopSortOrder == WorkshopSortOrder.BY_NAME) ? WorkshopSortOrder.MOST_LIKED : m_WorkshopSortOrder);
				Profiles.SaveActiveProfile();
			}
		}
		else if (Profiles.m_ActiveProfile.m_WorkshopItemsSortBy != m_WorkshopSortOrder)
		{
			Profiles.m_ActiveProfile.m_WorkshopItemsSortBy = ((m_WorkshopSortOrder == WorkshopSortOrder.BY_NAME) ? WorkshopSortOrder.MOST_LIKED : m_WorkshopSortOrder);
			Profiles.SaveActiveProfile();
		}
		SetOverTimeDropdownVisibility();
		SetTagsDropdownVisibility();
		SetSearchBarVisibility();
	}

	private void OnOverTimePeriodChanged()
	{
		m_WorkshopTimePeriod = (WorkshopOverTimePeriod)m_OverTimePeriodDropdown.value;
	}

	private void SetOverTimeDropdownVisibility()
	{
		m_OverTimePeriodDropdown.transform.parent.gameObject.SetActive(m_WorkshopSortOrder == WorkshopSortOrder.MOST_LIKED);
		m_OverTimePeriodDropdownInactive.transform.parent.gameObject.SetActive(m_WorkshopSortOrder != WorkshopSortOrder.MOST_LIKED);
		m_OverTimePeriodDropdownInactive.value = m_OverTimePeriodDropdown.value;
	}

	private void SetSearchBarVisibility()
	{
		bool flag = SearchEnabledForSortOrder(m_WorkshopSortOrder);
		m_SearchBar.gameObject.SetActive(flag);
		m_SearchBarInactive.gameObject.SetActive(!flag);
	}

	private bool SearchEnabledForSortOrder(WorkshopSortOrder sortOrder)
	{
		return true;
	}

	private void OnSearch()
	{
		m_SearchText = m_SearchInputField.text.Trim();
	}

	private void MaybeUpdateSearchText()
	{
		if (Time.unscaledTime - m_LastAutomaticSearchUpdateTime > AUTOMATIC_SEARCH_UPDATE_TIME_SECONDS)
		{
			OnSearch();
			m_LastAutomaticSearchUpdateTime = Time.unscaledTime;
		}
	}

	private void OnTagsButton()
	{
		if (!m_TagsRoot.gameObject.activeInHierarchy)
		{
			if (!GameManager.IsSteamOffline())
			{
				OpenTabsPanel();
			}
		}
		else
		{
			CloseTabsPanel();
		}
	}

	private void UpdateTagsCaption()
	{
		int numActiveTags = WorkshopTags.GetNumActiveTags(GetWorkshopTagMode());
		m_TagCaption.text = ((numActiveTags == 1) ? Localize.Get("UI_ONE_TAG_SELECTED") : Localize.Get("UI_TAG_CAPTION", numActiveTags.ToString()));
		m_TagCaptionInactive.text = Localize.Get("UI_TAG_CAPTION", "0");
	}

	private WorkshopTagMode GetWorkshopTagMode()
	{
		if (GameUI.m_Instance.m_Workshop.IsOnModsTab())
		{
			return WorkshopTagMode.MOD;
		}
		if (GameUI.m_Instance.m_Workshop.IsOnLevelsTab())
		{
			return WorkshopTagMode.LEVEL;
		}
		if (GameUI.m_Instance.m_Workshop.IsOnCampaingsTab())
		{
			return WorkshopTagMode.CAMPAIGN;
		}
		Debug.LogWarning("Unexpected current tab in GetWorkshopTagMode()");
		return WorkshopTagMode.LEVEL;
	}

	private void OpenTabsPanel()
	{
		ActivePanels.Add(m_TagsRoot.gameObject);
		m_TagsDucking.SetActive(value: true);
		m_TagsRoot.gameObject.SetActive(value: true);
		EnableTagsForType(GetWorkshopTagMode());
	}

	private void CloseTabsPanel()
	{
		ActivePanels.Remove(m_TagsRoot.gameObject);
		m_TagsRoot.gameObject.SetActive(value: false);
		m_TagsDucking.SetActive(value: false);
		SaveTags();
	}

	private void ProcessInput()
	{
		if (m_TagsRoot.gameObject.activeInHierarchy && !GameStateCommonInput.IgnoreKeyboardInputForPanel(m_TagsRoot.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				CloseTabsPanel();
			}
			else if (GameInput.GetMouseButtonJustPressed(0) && !GameUI.PointerOver(typeof(WorkshopTagPanel)))
			{
				CloseTabsPanel();
			}
		}
	}

	public void LoadTags()
	{
		string fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), WORKSHOP_TAGS_FILENAME);
		if (!Utils.FileExists(fullPath))
		{
			return;
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array == null || array.Length == 0)
		{
			return;
		}
		try
		{
			WorkshopFilterBarProxy workshopFilterBarProxy = SerializationUtility.DeserializeValue<WorkshopFilterBarProxy>(array, DataFormat.JSON);
			for (int i = 0; i < m_TagToggleContent.childCount; i++)
			{
				WorkshopTagToggle component = m_TagToggleContent.GetChild(i).GetComponent<WorkshopTagToggle>();
				if (component != null)
				{
					component.m_IncludeToggle.isOn = workshopFilterBarProxy.m_LevelIncludeTags.Contains(component.m_TagName) || workshopFilterBarProxy.m_ModIncludeTags.Contains(component.m_TagName);
					component.m_ExcludeToggle.isOn = workshopFilterBarProxy.m_LevelExcludeTags.Contains(component.m_TagName) || workshopFilterBarProxy.m_ModExcludeTags.Contains(component.m_TagName);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Excpetion parsing " + WORKSHOP_TAGS_FILENAME + ": " + ex.Message);
		}
	}

	public void SaveTags()
	{
		WorkshopFilterBarProxy workshopFilterBarProxy = new WorkshopFilterBarProxy();
		WorkshopTags.GetRequiredTags(WorkshopTagMode.LEVEL, workshopFilterBarProxy.m_LevelIncludeTags);
		WorkshopTags.GetRequiredTags(WorkshopTagMode.MOD, workshopFilterBarProxy.m_ModIncludeTags);
		WorkshopTags.GetExcludeTags(WorkshopTagMode.LEVEL, workshopFilterBarProxy.m_LevelExcludeTags);
		WorkshopTags.GetExcludeTags(WorkshopTagMode.MOD, workshopFilterBarProxy.m_ModExcludeTags);
		byte[] bytes = SerializationUtility.SerializeValue(workshopFilterBarProxy, DataFormat.JSON);
		Utils.WriteBytes(Path.Combine(Profiles.GetProfileRootDirectory(), WORKSHOP_TAGS_FILENAME), bytes);
	}

	private void OnSearchInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_SearchInputField.text, m_SearchInputField.characterLimit, string.Empty, multiline: false, OnSearchEntered);
	}

	private void OnSearchEntered(string text)
	{
		if (text != null)
		{
			m_SearchInputField.text = text;
		}
	}
}
