using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using Steamworks.Ugc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_Workshop : MonoBehaviour
{
	[Header("Panels")]
	public RectTransform m_RootPanel;

	public GameObject m_Ducking;

	public Panel_WorkshopItem m_WorkshopItemPanel;

	public Panel_WorkshopCampaign m_WorkshopCampaignPanel;

	public Panel_WorkshopSubmitMod m_SubmitModPanel;

	public Panel_WorkshopActiveMods m_ActiveModsPanel;

	public Panel_WorkshopLocalMods m_LocalModsPanel;

	[Header("Tab Buttons")]
	public SandboxTab m_LevelsTab;

	public SandboxTab m_CampaignsTab;

	public SandboxTab m_ModsTab;

	public SandboxTab m_ActiveModsTab;

	public SandboxTab m_LocalModsTab;

	[Header("Colors")]
	public Color m_TabActiveColor;

	public Color m_TabInActiveColor;

	[Header("Header")]
	public WorkshopFilterBar m_FilterBar;

	public TextMeshProUGUI m_TitleText;

	public TextMeshProUGUI m_OfflineText;

	public Button m_CancelButton;

	[Header("Body")]
	public GameObject m_LoadingScreen;

	public GameObject m_ZeroItemsMessage;

	public TextMeshProUGUI m_ZeroItemsMessageText;

	[Header("Footer")]
	public GameObject m_Footer;

	public TextMeshProUGUI m_PagesText;

	public Button m_PreviousPageButton;

	public Button m_NextPageButton;

	[Header("Refresh")]
	public Button m_RefreshButton;

	public TextMeshProUGUI m_RefreshText;

	public GameObject m_RefreshBusyAnimation;

	[Header("Slots")]
	public Texture2D m_DefaultSlotPreview;

	public RectTransform m_Content;

	public RectTransform m_ContentGrid;

	public GameObject m_WorkshopItemSlotPrefab;

	[NonSerialized]
	public WorkshopTab m_CurrentTab;

	private Dictionary<WorkshopTab, int> m_CurrentPageIndex = new Dictionary<WorkshopTab, int>();

	private Dictionary<WorkshopTab, WorkshopQueryFilter> m_LastQueryFilter = new Dictionary<WorkshopTab, WorkshopQueryFilter>();

	private WorkshopView m_WorkshopView;

	private WorkshopDownloadStatus m_WorkshopDownloadStatus;

	private WorkshopQueryFilter m_TempWorkshopQueryFilter = new WorkshopQueryFilter();

	private bool m_Offline;

	private bool m_SuppressMainMenuOnClose;

	private int m_NumPages;

	private Action<bool> m_NextSearchCompleteCallback;

	private readonly int NUM_SLOTS_PER_PAGE = 15;

	private WorkshopItemSlot[] m_Slots;

	private List<string> m_ActiveModListOnOpen = new List<string>();

	private string m_LastManualSearchText = string.Empty;

	private int m_RefreshCounter;

	public static Panel_Workshop m_Instance;

	private void Awake()
	{
		m_Instance = this;
		m_LocalModsTab.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		m_CancelButton.onClick.AddListener(OnCancel);
		m_LevelsTab.m_Button.onClick.AddListener(OnLevelsButton);
		m_CampaignsTab.m_Button.onClick.AddListener(OnCampaignsButton);
		m_ModsTab.m_Button.onClick.AddListener(OnModsButton);
		m_ActiveModsTab.m_Button.onClick.AddListener(OnActiveModsButton);
		m_LocalModsTab.m_Button.onClick.AddListener(OnLocalModsButton);
		m_RefreshButton.onClick.AddListener(OnForceRefresh);
		m_PreviousPageButton.onClick.AddListener(OnPreviousPage);
		m_NextPageButton.onClick.AddListener(OnNextPage);
		m_SubmitModPanel.gameObject.SetActive(value: false);
		m_WorkshopItemPanel.gameObject.SetActive(value: false);
		m_WorkshopCampaignPanel.gameObject.SetActive(value: false);
		m_FilterBar.m_TagsRoot.gameObject.SetActive(value: false);
	}

	private void OnEnable()
	{
		m_OfflineText.gameObject.SetActive(GameManager.IsSteamOffline());
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		m_LastManualSearchText = string.Empty;
	}

	private void Update()
	{
		ProcessInput();
		ProcessOffline();
		RefreshFooter();
		ProcessRefreshButton();
		if (TabHasFilterBar(m_CurrentTab) && m_WorkshopDownloadStatus == WorkshopDownloadStatus.IDLE)
		{
			WorkshopQueryFilters.UpdateQuery(m_TempWorkshopQueryFilter, m_FilterBar, m_CurrentTab);
			if (!m_LastQueryFilter[m_CurrentTab].Matches(m_TempWorkshopQueryFilter))
			{
				m_CurrentPageIndex[m_CurrentTab] = 0;
				RefreshTab(m_CurrentTab);
			}
			else if (m_FilterBar.DoesManualSearch() && m_LastManualSearchText != m_FilterBar.m_SearchText)
			{
				m_CurrentPageIndex[m_CurrentTab] = 0;
				DoManualSearch(m_FilterBar.m_SearchText);
			}
		}
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void DoManualSearch(string searchText)
	{
		RefreshTabForManualSearch(m_CurrentTab, searchText);
		m_LastManualSearchText = searchText;
	}

	public void UpdateForCurrentDevice()
	{
		m_RootPanel.anchoredPosition = new Vector2(0f, (Game.IsRunningOnSteamDeck() || GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? 7 : 0);
	}

	public void Open(WorkshopView workshopView)
	{
		Open(workshopView, string.Empty, null);
	}

	public void Open(WorkshopView workshopView, string levelID, Action<bool> searchCompleteCallback)
	{
		base.gameObject.SetActive(value: true);
		SetWorkshopView(workshopView);
		m_TitleText.text = ((workshopView == WorkshopView.LEVELS_AND_CAMPAIGNS) ? Localize.Get("MAINMENU_WORKSHOP") : Localize.Get("MAINMENU_MODS"));
		m_ActiveModListOnOpen.Clear();
		m_ActiveModListOnOpen.AddRange(Mods.GetActiveModDirectories());
		if (m_Slots == null)
		{
			CreateSlots();
			ResetCurrentPageIndex();
			m_LastQueryFilter.Add(WorkshopTab.LEVELS, new WorkshopQueryFilter());
			m_LastQueryFilter.Add(WorkshopTab.CAMPAIGNS, new WorkshopQueryFilter());
			m_LastQueryFilter.Add(WorkshopTab.MODS, new WorkshopQueryFilter());
		}
		m_FilterBar.OnEnableManual(workshopView);
		if (!string.IsNullOrEmpty(levelID))
		{
			SelectTabSilent(WorkshopTab.LEVELS, WorkshopSortOrder.MOST_RECENT, levelID);
			m_NextSearchCompleteCallback = searchCompleteCallback;
		}
		else if (!TabVisibileInView(m_CurrentTab, workshopView))
		{
			m_Content.gameObject.SetActive(value: false);
			m_Footer.SetActive(value: false);
			SelectTabSilent((workshopView == WorkshopView.LEVELS_AND_CAMPAIGNS) ? WorkshopTab.LEVELS : WorkshopTab.MODS, WorkshopSortOrder.NONE, string.Empty);
		}
		if (workshopView == WorkshopView.LEVELS_AND_CAMPAIGNS)
		{
			m_FilterBar.Update();
		}
	}

	public void OnForceRefresh()
	{
		if (m_RefreshBusyAnimation.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else if (m_CurrentTab == WorkshopTab.LEVELS && m_LastQueryFilter.ContainsKey(WorkshopTab.LEVELS))
		{
			m_LastQueryFilter[WorkshopTab.LEVELS].m_SortOrder = WorkshopSortOrder.NONE;
			FlushCache();
			ClearSlots();
		}
		else if (m_CurrentTab == WorkshopTab.CAMPAIGNS && m_LastQueryFilter.ContainsKey(WorkshopTab.CAMPAIGNS))
		{
			m_LastQueryFilter[WorkshopTab.CAMPAIGNS].m_SortOrder = WorkshopSortOrder.NONE;
			FlushCache();
			ClearSlots();
		}
		else if (m_CurrentTab == WorkshopTab.MODS && m_LastQueryFilter.ContainsKey(WorkshopTab.MODS))
		{
			m_LastQueryFilter[WorkshopTab.MODS].m_SortOrder = WorkshopSortOrder.NONE;
			FlushCache();
			ClearSlots();
		}
	}

	public WorkshopItemSlot GetSlot(string id)
	{
		WorkshopItemSlot[] slots = m_Slots;
		foreach (WorkshopItemSlot workshopItemSlot in slots)
		{
			if (workshopItemSlot.m_Item.GetId() == id)
			{
				return workshopItemSlot;
			}
		}
		return null;
	}

	private bool TabVisibileInView(WorkshopTab tab, WorkshopView view)
	{
		switch (view)
		{
		case WorkshopView.LEVELS_AND_CAMPAIGNS:
			if (tab != WorkshopTab.LEVELS)
			{
				return tab == WorkshopTab.CAMPAIGNS;
			}
			return true;
		case WorkshopView.MODS:
			if (tab != WorkshopTab.MODS && tab != WorkshopTab.SUBSCRIBED_MODS)
			{
				return tab == WorkshopTab.LOCAL_MODS;
			}
			return true;
		default:
			Debug.LogWarning("Unexpected view in TabVisibleInView(): " + view);
			return false;
		}
	}

	public void ClearActiveModIcons()
	{
		WorkshopItemSlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].m_ModActivatedIcon.gameObject.SetActive(value: false);
		}
	}

	private void FlushCache()
	{
		ResetCurrentPageIndex();
		WorkshopCaches.Clear(WorkshopTab.LEVELS);
		WorkshopCaches.Clear(WorkshopTab.CAMPAIGNS);
		WorkshopCaches.Clear(WorkshopTab.MODS);
	}

	private void ResetCurrentPageIndex()
	{
		m_CurrentPageIndex[WorkshopTab.LEVELS] = 0;
		m_CurrentPageIndex[WorkshopTab.CAMPAIGNS] = 0;
		m_CurrentPageIndex[WorkshopTab.MODS] = 0;
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	public async void Close(bool suppressMainMenu = false)
	{
		m_SuppressMainMenuOnClose = suppressMainMenu;
		m_FilterBar.m_TagsRoot.gameObject.SetActive(value: false);
		if (m_WorkshopView == WorkshopView.MODS)
		{
			if (Utils.CompareStringLists(m_ActiveModListOnOpen, Mods.GetActiveModDirectories()))
			{
				FinalizeClose();
				return;
			}
			Mods.ApplyActiveMods();
			PopUpMessage.DisplayLoading(Localize.Get("UI_MODS_PROCESSING"));
			await Task.Delay(400);
			for (int i = 0; i < 100; i++)
			{
				await Task.Delay(100);
				if (ModApi.GetNumModsLoadingAddressables() <= 0)
				{
					break;
				}
			}
			PopUpMessage.Close();
			FinalizeClose();
		}
		else
		{
			FinalizeClose();
		}
	}

	private void FinalizeClose()
	{
		base.gameObject.SetActive(value: false);
		if (GameStateManager.GetState() == GameState.MAIN_MENU && !m_SuppressMainMenuOnClose)
		{
			GameUI.m_Instance.m_MainMenuNew.Open();
		}
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
			return;
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
		{
			CycleToNextTab();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
		{
			CycleToPrevTab();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
		{
			CycleToNextPage();
		}
		if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT))
		{
			CycleToPrevPage();
		}
	}

	public void CycleToNextTab()
	{
		if (m_WorkshopView == WorkshopView.LEVELS_AND_CAMPAIGNS)
		{
			if (m_CurrentTab == WorkshopTab.LEVELS)
			{
				SelectTab(WorkshopTab.CAMPAIGNS);
			}
			else if (m_CurrentTab == WorkshopTab.CAMPAIGNS)
			{
				SelectTab(WorkshopTab.LEVELS);
			}
		}
		else if (m_WorkshopView == WorkshopView.MODS)
		{
			if (m_CurrentTab == WorkshopTab.MODS)
			{
				SelectTab(WorkshopTab.SUBSCRIBED_MODS);
			}
			else if (m_CurrentTab == WorkshopTab.SUBSCRIBED_MODS)
			{
				SelectTab(Game.IsRunningOnSteamDeck() ? WorkshopTab.MODS : WorkshopTab.LOCAL_MODS);
			}
			else if (m_CurrentTab == WorkshopTab.LOCAL_MODS)
			{
				SelectTab(WorkshopTab.MODS);
			}
		}
	}

	public void CycleToPrevTab()
	{
		if (m_WorkshopView == WorkshopView.LEVELS_AND_CAMPAIGNS)
		{
			if (m_CurrentTab == WorkshopTab.LEVELS)
			{
				SelectTab(WorkshopTab.CAMPAIGNS);
			}
			else if (m_CurrentTab == WorkshopTab.CAMPAIGNS)
			{
				SelectTab(WorkshopTab.LEVELS);
			}
		}
		else if (m_WorkshopView == WorkshopView.MODS)
		{
			if (m_CurrentTab == WorkshopTab.MODS)
			{
				SelectTab(Game.IsRunningOnSteamDeck() ? WorkshopTab.SUBSCRIBED_MODS : WorkshopTab.LOCAL_MODS);
			}
			else if (m_CurrentTab == WorkshopTab.SUBSCRIBED_MODS)
			{
				SelectTab(WorkshopTab.MODS);
			}
			else if (m_CurrentTab == WorkshopTab.LOCAL_MODS)
			{
				SelectTab(WorkshopTab.SUBSCRIBED_MODS);
			}
		}
	}

	public void CycleToNextPage()
	{
		if (m_CurrentPageIndex.ContainsKey(m_CurrentTab) && m_CurrentPageIndex[m_CurrentTab] == m_NumPages - 1)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			ExecuteEvents.Execute(m_NextPageButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	public void CycleToPrevPage()
	{
		if (m_CurrentPageIndex.ContainsKey(m_CurrentTab) && m_CurrentPageIndex[m_CurrentTab] == 0)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			ExecuteEvents.Execute(m_PreviousPageButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	public void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		GameUI.m_Instance.m_GamepadLegend.HideButtons();
		if (GameManager.IsSteamOffline())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
			return;
		}
		if (m_PagesText.gameObject.activeInHierarchy && !m_ZeroItemsMessage.activeInHierarchy && m_NumPages > 1)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"), GamepadButtonType.DPAD_HORIZONTAL, Localize.Get("UI_CHANGE_PAGE"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
		}
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private void ProcessOffline()
	{
		bool offline = m_Offline;
		m_Offline = GameManager.IsSteamOffline();
		m_OfflineText.gameObject.SetActive(GameManager.IsSteamOffline() && !TabSupportsOffline(m_CurrentTab));
		if (m_Offline != offline)
		{
			RefreshTab(m_CurrentTab);
		}
		if (GameManager.IsSteamOffline())
		{
			m_Content.gameObject.SetActive(value: false);
		}
	}

	private bool TabSupportsOffline(WorkshopTab tab)
	{
		if (tab != WorkshopTab.LOCAL_MODS)
		{
			return tab == WorkshopTab.SUBSCRIBED_MODS;
		}
		return true;
	}

	private void ShowLoadingScreen(bool on)
	{
		if (on)
		{
			m_LoadingScreen.gameObject.SetActive(value: true);
			m_Content.gameObject.SetActive(value: false);
			m_Footer.gameObject.SetActive(value: false);
			m_ZeroItemsMessage.SetActive(value: false);
		}
		else
		{
			m_Content.gameObject.SetActive(value: true);
			m_LoadingScreen.gameObject.SetActive(value: false);
		}
	}

	private void CreateSlots()
	{
		m_Slots = new WorkshopItemSlot[NUM_SLOTS_PER_PAGE];
		for (int i = 0; i < NUM_SLOTS_PER_PAGE; i++)
		{
			WorkshopItemSlot component = UnityEngine.Object.Instantiate(m_WorkshopItemSlotPrefab, m_ContentGrid.transform).GetComponent<WorkshopItemSlot>();
			component.gameObject.name = "Slot" + i;
			component.gameObject.SetActive(value: false);
			m_Slots[i] = component;
			component.m_OnHoverChangeCallback = (WorkshopItemSlot.OnHoverChangeDelegate)Delegate.Combine(component.m_OnHoverChangeCallback, new WorkshopItemSlot.OnHoverChangeDelegate(OnHoverChange));
		}
	}

	public void OnHoverChange(WorkshopItemSlot slot, bool hover)
	{
	}

	public void ForceRefreshCurrentTab()
	{
		WorkshopCaches.Clear(m_CurrentTab);
		RefreshTab(m_CurrentTab);
	}

	public bool IsOnModsTab()
	{
		return GameUI.m_Instance.m_Workshop.m_CurrentTab == WorkshopTab.MODS;
	}

	public bool IsOnLevelsTab()
	{
		return GameUI.m_Instance.m_Workshop.m_CurrentTab == WorkshopTab.LEVELS;
	}

	public bool IsOnCampaingsTab()
	{
		return GameUI.m_Instance.m_Workshop.m_CurrentTab == WorkshopTab.CAMPAIGNS;
	}

	public void GoToActiveMods()
	{
		SelectTabSilent(WorkshopTab.SUBSCRIBED_MODS, WorkshopSortOrder.NONE, string.Empty);
	}

	public void GoToLocalMods()
	{
		SelectTabSilent(WorkshopTab.LOCAL_MODS, WorkshopSortOrder.NONE, string.Empty);
	}

	public WorkshopView GetWorkshopView()
	{
		return m_WorkshopView;
	}

	private void RefreshFooter()
	{
		m_Footer.SetActive(!m_ZeroItemsMessage.activeInHierarchy && m_CurrentTab != WorkshopTab.LOCAL_MODS && m_CurrentTab != WorkshopTab.SUBSCRIBED_MODS);
		if (GameManager.IsSteamOffline())
		{
			m_Footer.SetActive(value: false);
		}
		m_PagesText.gameObject.SetActive(!m_LoadingScreen.gameObject.activeInHierarchy);
		if (m_Footer.gameObject.activeInHierarchy)
		{
			m_PagesText.text = $"{m_CurrentPageIndex[m_CurrentTab] + 1} / {m_NumPages}";
			m_PreviousPageButton.interactable = m_CurrentPageIndex[m_CurrentTab] > 0;
			m_NextPageButton.interactable = m_CurrentPageIndex[m_CurrentTab] < m_NumPages - 1;
		}
	}

	private void ProcessRefreshButton()
	{
		m_RefreshButton.gameObject.SetActive(m_Footer.gameObject.activeInHierarchy);
		m_RefreshText.gameObject.SetActive(m_RefreshCounter == 0);
		m_RefreshBusyAnimation.SetActive(m_RefreshCounter > 0);
	}

	private void SelectTab(WorkshopTab tab)
	{
		if (tab != m_CurrentTab)
		{
			SelectTabSilent(tab, WorkshopSortOrder.NONE, string.Empty);
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void SelectTabSilent(WorkshopTab tab, WorkshopSortOrder forcedSortOrder, string forcedSearchText)
	{
		m_LevelsTab.m_Background.color = ((tab == WorkshopTab.LEVELS) ? m_TabActiveColor : m_TabInActiveColor);
		m_CampaignsTab.m_Background.color = ((tab == WorkshopTab.CAMPAIGNS) ? m_TabActiveColor : m_TabInActiveColor);
		m_ModsTab.m_Background.color = ((tab == WorkshopTab.MODS) ? m_TabActiveColor : m_TabInActiveColor);
		m_ActiveModsTab.m_Background.color = ((tab == WorkshopTab.SUBSCRIBED_MODS) ? m_TabActiveColor : m_TabInActiveColor);
		m_LocalModsTab.m_Background.color = ((tab == WorkshopTab.LOCAL_MODS) ? m_TabActiveColor : m_TabInActiveColor);
		m_LevelsTab.m_BackgroundRectTransform.offsetMin = ((tab == WorkshopTab.LEVELS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_CampaignsTab.m_BackgroundRectTransform.offsetMin = ((tab == WorkshopTab.CAMPAIGNS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_ModsTab.m_BackgroundRectTransform.offsetMin = ((tab == WorkshopTab.MODS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_ActiveModsTab.m_BackgroundRectTransform.offsetMin = ((tab == WorkshopTab.SUBSCRIBED_MODS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_LocalModsTab.m_BackgroundRectTransform.offsetMin = ((tab == WorkshopTab.LOCAL_MODS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		bool active = TabHasFilterBar(tab);
		m_Content.gameObject.SetActive(active);
		m_FilterBar.gameObject.SetActive(active);
		m_FilterBar.m_TagsRoot.gameObject.SetActive(value: false);
		if (tab == WorkshopTab.SUBSCRIBED_MODS)
		{
			m_ActiveModsPanel.Open();
		}
		else
		{
			m_ActiveModsPanel.Close();
		}
		if (tab == WorkshopTab.LOCAL_MODS)
		{
			m_LocalModsPanel.Open();
		}
		else
		{
			m_LocalModsPanel.Close();
		}
		m_CurrentTab = tab;
		m_FilterBar.SetTagsDropdownVisibility();
		if (!string.IsNullOrEmpty(forcedSearchText))
		{
			m_FilterBar.ForceSearch(forcedSearchText);
		}
		if (forcedSortOrder != WorkshopSortOrder.NONE)
		{
			m_FilterBar.SetWorkshopSortOrder(forcedSortOrder);
		}
		else
		{
			switch (tab)
			{
			case WorkshopTab.LEVELS:
			case WorkshopTab.CAMPAIGNS:
				if (m_FilterBar.m_WorkshopSortOrder != Profiles.m_ActiveProfile.m_WorkshopItemsSortBy)
				{
					m_FilterBar.SetWorkshopSortOrder(Profiles.m_ActiveProfile.m_WorkshopItemsSortBy);
				}
				break;
			case WorkshopTab.MODS:
				if (m_FilterBar.m_WorkshopSortOrder != Profiles.m_ActiveProfile.m_WorkshopModItemsSortBy)
				{
					m_FilterBar.SetWorkshopSortOrder(Profiles.m_ActiveProfile.m_WorkshopModItemsSortBy);
				}
				break;
			}
		}
		RefreshTab(m_CurrentTab);
	}

	private bool TabHasFilterBar(WorkshopTab tab)
	{
		if (tab != WorkshopTab.LEVELS && tab != WorkshopTab.CAMPAIGNS)
		{
			return tab == WorkshopTab.MODS;
		}
		return true;
	}

	private void OnLevelsButton()
	{
		SelectTab(WorkshopTab.LEVELS);
	}

	private void OnCampaignsButton()
	{
		SelectTab(WorkshopTab.CAMPAIGNS);
	}

	private void OnModsButton()
	{
		SelectTab(WorkshopTab.MODS);
	}

	private void OnActiveModsButton()
	{
		SelectTab(WorkshopTab.SUBSCRIBED_MODS);
	}

	private void OnLocalModsButton()
	{
		SelectTab(WorkshopTab.LOCAL_MODS);
	}

	private void RefreshTabForManualSearch(WorkshopTab tab, string searchText)
	{
		if (tab != WorkshopTab.LOCAL_MODS && tab != WorkshopTab.SUBSCRIBED_MODS)
		{
			int num = m_CurrentPageIndex[tab] * NUM_SLOTS_PER_PAGE;
			WorkshopItem[] allFiltered = WorkshopCaches.GetAllFiltered(tab, searchText);
			if (allFiltered != null)
			{
				ClearSlots();
				int num2 = Mathf.Min(num + NUM_SLOTS_PER_PAGE, allFiltered.Length);
				PopulateSlots((allFiltered.Length > NUM_SLOTS_PER_PAGE) ? allFiltered[num..num2] : allFiltered);
				m_NumPages = CalculateNumPages(allFiltered.Length, NUM_SLOTS_PER_PAGE);
				ShowLoadingScreen(on: false);
			}
		}
	}

	private async void RefreshTab(WorkshopTab tab)
	{
		if (tab == WorkshopTab.LOCAL_MODS || tab == WorkshopTab.SUBSCRIBED_MODS || GameManager.IsSteamOffline())
		{
			return;
		}
		WorkshopQueryFilter queryFilter = WorkshopQueryFilters.AllocateNewQuery(m_FilterBar, tab);
		if (queryFilter == null)
		{
			return;
		}
		if (!WorkshopCaches.FilterMatches(tab, queryFilter))
		{
			WorkshopCaches.Clear(tab);
		}
		else
		{
			m_FilterBar.SetFilters(queryFilter);
		}
		ClearSlots();
		int num = m_CurrentPageIndex[tab] * NUM_SLOTS_PER_PAGE;
		int num2 = NUM_SLOTS_PER_PAGE;
		if (WorkshopCaches.GetTotalCount(tab) > 0 && num + num2 > WorkshopCaches.GetTotalCount(tab))
		{
			num2 = WorkshopCaches.GetTotalCount(tab) - num;
		}
		WorkshopItem[] array = WorkshopCaches.Get(tab, queryFilter, num, num2);
		if (array != null)
		{
			PopulateSlots(array);
			m_NumPages = CalculateNumPages(WorkshopCaches.GetTotalCount(tab), NUM_SLOTS_PER_PAGE);
			ShowLoadingScreen(on: false);
		}
		else
		{
			if (m_WorkshopDownloadStatus == WorkshopDownloadStatus.IN_PROGRESS)
			{
				Debug.LogWarning("Trying to query workshop items while a query is already in progress");
				return;
			}
			m_FilterBar.SetFilters(queryFilter);
			if (!m_FilterBar.m_TagsRoot.gameObject.activeInHierarchy)
			{
				ShowLoadingScreen(on: true);
			}
			m_WorkshopDownloadStatus = WorkshopDownloadStatus.IN_PROGRESS;
			m_RefreshCounter++;
			await QueryItems(tab, queryFilter, m_CurrentPageIndex[tab] * NUM_SLOTS_PER_PAGE, num2);
			m_RefreshCounter--;
			if (m_RefreshCounter < 0)
			{
				m_RefreshCounter = 0;
			}
			m_WorkshopDownloadStatus = WorkshopDownloadStatus.IDLE;
			m_LastQueryFilter[tab] = queryFilter;
			ShowLoadingScreen(on: false);
		}
		if (m_FilterBar.DoesManualSearch())
		{
			DoManualSearch(m_FilterBar.m_SearchText);
		}
	}

	private async Task QueryItems(WorkshopTab tab, WorkshopQueryFilter filter, int startIndex, int numItems)
	{
		int cachePageIndex = WorkshopCaches.GetCachePageIndexNeeded(tab, startIndex, numItems);
		bool success = PopulateResults(tab, cachePageIndex, startIndex, numItems, filter, await filter.CreateQuery(tab, cachePageIndex).GetPageAsync((filter.m_SortOrder == WorkshopSortOrder.MOST_RECENTLY_PLAYED) ? 1 : (cachePageIndex + 1)));
		if (filter.m_SortOrder == WorkshopSortOrder.MOST_RECENTLY_PLAYED || Workshop.IsUserUGCQuery(filter.m_SortOrder))
		{
			ResultPage? result;
			do
			{
				cachePageIndex++;
				result = await filter.CreateQuery(tab, cachePageIndex).GetPageAsync((filter.m_SortOrder == WorkshopSortOrder.MOST_RECENTLY_PLAYED) ? 1 : (cachePageIndex + 1));
			}
			while (PopulateCacheOnly(tab, cachePageIndex, filter, result) && result.Value.TotalCount >= WorkshopCaches.NUM_ITEMS_PER_PAGE);
		}
		if (filter.m_SortOrder == WorkshopSortOrder.MOST_RECENTLY_PLAYED)
		{
			WorkshopCaches.ForceUpdateTotalCount(tab);
		}
		m_NextSearchCompleteCallback?.Invoke(success);
		m_NextSearchCompleteCallback = null;
	}

	private bool PopulateResults(WorkshopTab tab, int cachePageIndex, int startIndex, int numItems, WorkshopQueryFilter filter, ResultPage? result)
	{
		if (!result.HasValue)
		{
			PopulateSlots(null);
			return false;
		}
		filter.m_SearchText.ToLower();
		List<WorkshopItem> list = new List<WorkshopItem>();
		foreach (Item entry in result.Value.Entries)
		{
			if (!FilterOutItem(entry, tab))
			{
				RequestPersonaInfo(entry);
				WorkshopItem item = new WorkshopItem(entry);
				list.Add(item);
			}
		}
		if (list.Count == 0)
		{
			PopulateSlots(null);
			return false;
		}
		WorkshopItem[] items = list.ToArray();
		WorkshopCaches.Add(tab, filter, cachePageIndex, items, result.Value.TotalCount);
		WorkshopItem[] array = WorkshopCaches.Get(tab, filter, startIndex, numItems);
		if (array == null)
		{
			PopulateSlots(null);
			return false;
		}
		PopulateSlots(array);
		m_NumPages = CalculateNumPages(WorkshopCaches.GetTotalCount(tab), NUM_SLOTS_PER_PAGE);
		return true;
	}

	private bool PopulateCacheOnly(WorkshopTab tab, int cachePageIndex, WorkshopQueryFilter filter, ResultPage? result)
	{
		if (!result.HasValue || result.Value.ResultCount == 0)
		{
			return false;
		}
		List<WorkshopItem> list = new List<WorkshopItem>();
		foreach (Item entry in result.Value.Entries)
		{
			if (!FilterOutItem(entry, tab))
			{
				RequestPersonaInfo(entry);
				WorkshopItem item = new WorkshopItem(entry);
				list.Add(item);
			}
		}
		if (list.Count == 0)
		{
			return false;
		}
		WorkshopItem[] items = list.ToArray();
		WorkshopCaches.Add(tab, filter, cachePageIndex, items, result.Value.TotalCount);
		return true;
	}

	private bool FilterOutItem(Item item, WorkshopTab tab)
	{
		if (item.Result != Result.OK)
		{
			return true;
		}
		if (item.HasTag(WorkshopTags.MOD_TAG) && tab != WorkshopTab.MODS)
		{
			return true;
		}
		if (item.HasTag(WorkshopTags.LEVEL_TAG) && tab != WorkshopTab.LEVELS)
		{
			return true;
		}
		if (item.HasTag(WorkshopTags.CAMPAIGN_TAG) && tab != WorkshopTab.CAMPAIGNS)
		{
			return true;
		}
		if ((uint)item.CreatorApp != (uint)SteamManager.m_AppId)
		{
			return true;
		}
		return false;
	}

	private void RequestPersonaInfo(Item item)
	{
		if (!Workshop.m_SteamIdsWithRequestedInfo.Contains(item.Owner.Id))
		{
			Workshop.m_SteamIdsWithRequestedInfo.Add(item.Owner.Id);
			if (!GameManager.IsSteamOffline())
			{
				SteamPersonas.RequestUserInfo(item.Owner.Id.ToString());
			}
		}
	}

	private int CalculateNumPages(int totalCount, int slotsPerPage)
	{
		return Mathf.CeilToInt((float)totalCount / (float)slotsPerPage);
	}

	private void PopulateSlots(WorkshopItem[] items)
	{
		if (items == null || items.Length == 0)
		{
			m_ZeroItemsMessage.SetActive(value: true);
			m_Footer.gameObject.SetActive(value: false);
			return;
		}
		m_ZeroItemsMessage.SetActive(value: false);
		int num = 0;
		foreach (WorkshopItem workshopItem in items)
		{
			m_Slots[num].gameObject.SetActive(value: true);
			m_Slots[num].m_RawImage.gameObject.SetActive(value: false);
			m_Slots[num].SetItem(workshopItem);
			workshopItem.DownloadPreviewFromSteam(OnDownloadPreviewComplete);
			num++;
		}
		for (int j = num; j < NUM_SLOTS_PER_PAGE; j++)
		{
			m_Slots[j].gameObject.SetActive(value: false);
		}
	}

	private void ClearSlots()
	{
		WorkshopItemSlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].gameObject.SetActive(value: false);
		}
	}

	private void OnDownloadPreviewComplete(WorkshopItem item)
	{
		WorkshopItemSlot[] slots = m_Slots;
		foreach (WorkshopItemSlot workshopItemSlot in slots)
		{
			if (workshopItemSlot.m_Item == item)
			{
				workshopItemSlot.SetImageTexture((item.m_PreviewTexture == null) ? m_DefaultSlotPreview : item.m_PreviewTexture);
				workshopItemSlot.m_RawImage.gameObject.SetActive(value: true);
				break;
			}
		}
	}

	private void OnPreviousPage()
	{
		if (m_LoadingScreen.gameObject.activeInHierarchy || m_CurrentPageIndex[m_CurrentTab] == 0)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		m_CurrentPageIndex[m_CurrentTab] -= 1;
		RefreshTab(m_CurrentTab);
	}

	private void OnNextPage()
	{
		if (m_LoadingScreen.gameObject.activeInHierarchy || m_CurrentPageIndex[m_CurrentTab] == m_NumPages - 1)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		m_CurrentPageIndex[m_CurrentTab] += 1;
		RefreshTab(m_CurrentTab);
	}

	private void SetWorkshopView(WorkshopView workshopView)
	{
		m_WorkshopView = workshopView;
		m_LevelsTab.gameObject.SetActive(workshopView == WorkshopView.LEVELS_AND_CAMPAIGNS);
		m_CampaignsTab.gameObject.SetActive(workshopView == WorkshopView.LEVELS_AND_CAMPAIGNS);
		m_ModsTab.gameObject.SetActive(workshopView == WorkshopView.MODS);
		m_ActiveModsTab.gameObject.SetActive(workshopView == WorkshopView.MODS);
		m_LocalModsTab.gameObject.SetActive(workshopView == WorkshopView.MODS && !Game.IsRunningOnSteamDeck());
	}
}
