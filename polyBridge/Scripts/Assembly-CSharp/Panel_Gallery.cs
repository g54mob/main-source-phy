using System;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Panel_Gallery : MonoBehaviour
{
	public RectTransform m_RootPanel;

	public GameObject m_Ducking;

	[Header("Tab Buttons")]
	public SandboxTab m_AllTabButton;

	public SandboxTab m_FeaturedTabButton;

	[Header("Colors")]
	public Color m_TabActiveColor;

	public Color m_TabInActiveColor;

	[Header("Header")]
	public Button m_CancelButton;

	public TMP_Dropdown m_SortByDropdown;

	public TMP_Dropdown m_WorldNameDropdown;

	public TMP_Dropdown m_LevelNameDropdown;

	public TMP_Dropdown m_CreatedByDropdown;

	public GameObject m_CreatedByContainer;

	[Header("Toggles")]
	public Toggle m_UnderBudgetToggle;

	public Toggle m_UnbreakingToggle;

	public Toggle m_CuratedToggle;

	public Toggle m_ShowOnlyWinsToggle;

	public Toggle m_IncludeCheatsToggle;

	[Header("Body")]
	public GameObject m_LoadingScreen;

	public GameObject m_FailedLoadingContainer;

	public TextMeshProUGUI m_FailedLoadingText;

	public GameObject m_ZeroItemsMessage;

	public Button m_RetryButton;

	[Header("Footer")]
	public GameObject m_Footer;

	public TextMeshProUGUI m_PagesText;

	public Button m_PreviousPageButton;

	public Button m_NextPageButton;

	[Header("Slots")]
	public RectTransform m_Content;

	public RectTransform m_ContentGrid;

	public GameObject m_GallerySlotPrefab;

	[Header("Playback")]
	public VideoPlayer m_VideoPlayer;

	public Panel_GalleryVideo m_GalleryVideo;

	[NonSerialized]
	public bool m_ReturnToWorkshop;

	[NonSerialized]
	public string m_ReturnToWeekliesItemID;

	[NonSerialized]
	public bool m_ReturnToMainMenu;

	private GallerySlot[] m_Slots;

	private GallerySlot m_HoverSlot;

	private GallerySlot m_PreparingSlot;

	private GalleryCreatedBy m_CreatedByFilter;

	private GalleryFilterParameters m_GalleryFilterParameters = new GalleryFilterParameters();

	private GalleryFilterParameters m_LastGalleryFilterParameters = new GalleryFilterParameters();

	private Dictionary<int, string> m_LevelFilterMap = new Dictionary<int, string>();

	private Dictionary<string, int> m_LevelFilterReverseMap = new Dictionary<string, int>();

	private string m_RestoreLevelIDFilter;

	private bool m_Initialized;

	private int m_CurrentPageIndex;

	private bool m_ForceRefresh;

	private GalleryTab m_CurrentTab;

	private bool m_TabHasBeenSelected;

	private void Awake()
	{
		m_FeaturedTabButton.m_Button.onClick.AddListener(OnFeaturedTab);
		m_AllTabButton.m_Button.onClick.AddListener(OnAllTab);
		m_RetryButton.onClick.AddListener(OnRetry);
	}

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		if (!m_Initialized)
		{
			m_VideoPlayer.prepareCompleted += PrepareCompleted;
			m_CancelButton.onClick.AddListener(OnCancel);
			m_PreviousPageButton.onClick.AddListener(OnPreviousPage);
			m_NextPageButton.onClick.AddListener(OnNextPage);
			m_UnderBudgetToggle.onValueChanged.AddListener(delegate
			{
				OnUnderBudgetChanged();
			});
			m_UnbreakingToggle.onValueChanged.AddListener(delegate
			{
				OnUnbreakingChanged();
			});
			m_CuratedToggle.onValueChanged.AddListener(delegate
			{
				OnCuratedChanged();
			});
			m_ShowOnlyWinsToggle.onValueChanged.AddListener(delegate
			{
				OnShowWinsOnlyChanged();
			});
			m_IncludeCheatsToggle.onValueChanged.AddListener(delegate
			{
				OnIncludeCheatsChanged();
			});
			m_SortByDropdown.onValueChanged.AddListener(delegate
			{
				OnSortByChanged();
			});
			m_WorldNameDropdown.onValueChanged.AddListener(delegate
			{
				OnWorldNameChanged();
			});
			m_LevelNameDropdown.onValueChanged.AddListener(delegate
			{
				OnLevelNameChanged();
			});
			m_CreatedByDropdown.onValueChanged.AddListener(delegate
			{
				OnCreatedByChanged();
			});
			CampaignWorldDropdown.Populate(m_WorldNameDropdown, includeAll: true);
			PopulateSortByDropdown();
			PopulateCreatedByDropdown();
			m_SortByDropdown.alphaFadeSpeed = 0f;
			m_WorldNameDropdown.alphaFadeSpeed = 0f;
			m_LevelNameDropdown.alphaFadeSpeed = 0f;
			m_CreatedByDropdown.alphaFadeSpeed = 0f;
			m_CreatedByFilter = GalleryCreatedBy.COMMUNITY;
			m_UnderBudgetToggle.isOn = Profiles.m_ActiveProfile.m_GalleryUnderBudget;
			m_UnbreakingToggle.isOn = Profiles.m_ActiveProfile.m_GalleryUnbreaking;
			m_CuratedToggle.isOn = Profiles.m_ActiveProfile.m_CuratedReplays;
			m_ShowOnlyWinsToggle.isOn = Profiles.m_ActiveProfile.m_GalleryShowOnlyWins;
			m_IncludeCheatsToggle.isOn = Profiles.m_ActiveProfile.m_GalleryIncludeCheats;
			m_SortByDropdown.SetValueWithoutNotify((int)Profiles.m_ActiveProfile.m_GallerySortBy);
			PopulateSlots();
			m_LastGalleryFilterParameters.Set(0, "zzz", "zzz", "zzz", GallerySortBy.BUDGET_LOW, showOnlyFeatured: false, underBudgetOnly: false, unbreakingOnly: false, showOnlyWins: false, includeCheats: false, curated: false);
			m_Initialized = true;
		}
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		m_CreatedByContainer.gameObject.SetActive(!GameManager.IsSteamOffline());
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GalleryPreviewRequests.Clear();
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.m_Root.gameObject.SetActive(value: true);
		GameUI.m_Instance.m_Workshop.m_WorkshopCampaignPanel.m_Ducking.gameObject.SetActive(value: true);
		if (!GameUI.m_Instance.m_LevelComplete.gameObject.activeInHierarchy)
		{
			PreviewCache.FlushPreviewsOverCacheLimit();
		}
		if (m_HoverSlot != null)
		{
			OnHoverChange(m_HoverSlot, hover: false);
		}
	}

	private void OnDestroy()
	{
		DestroySlots();
	}

	private void Update()
	{
		GallerySlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].UpdateManual();
		}
		if (Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) == GallerySearchResultStatus.LOADING)
		{
			ShowLoadingScreen(loadingScreenVisible: true);
		}
		else if (Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) == GallerySearchResultStatus.CACHED)
		{
			Gallery.RequestPreviewsForPage(m_CurrentPageIndex);
			Gallery.RequestPreviewsForPage(m_CurrentPageIndex + 1);
			ShowLoadingScreen(loadingScreenVisible: false);
		}
		else if (Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) == GallerySearchResultStatus.ZERO_ITEMS)
		{
			m_LoadingScreen.SetActive(value: false);
			m_ZeroItemsMessage.SetActive(value: true);
			m_FailedLoadingContainer.SetActive(value: false);
		}
		else if (Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) == GallerySearchResultStatus.FAILED_LOAD)
		{
			m_LoadingScreen.SetActive(value: false);
			m_ZeroItemsMessage.SetActive(value: false);
			m_Content.gameObject.SetActive(value: false);
			m_Footer.gameObject.SetActive(value: false);
			m_FailedLoadingContainer.SetActive(value: true);
			m_FailedLoadingText.text = (GameManager.IsSteamOffline() ? Localize.Get("UI_STEAM_OFFLINE") : Localize.Get("WARN_GALLERY_DOWNLOAD_FAIL"));
		}
		if (m_GalleryVideo.gameObject.activeInHierarchy)
		{
			m_HoverSlot = null;
			return;
		}
		ProcessInput();
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
		if ((bool)m_HoverSlot && m_VideoPlayer.isPlaying && m_VideoPlayer.frame >= 0)
		{
			m_HoverSlot.SetProgress((float)(m_VideoPlayer.frame + 1) / (float)m_VideoPlayer.frameCount);
			m_HoverSlot.FrameIndex = m_VideoPlayer.frame;
		}
		if (GameInput.GetMouseButtonJustPressed(0) && m_HoverSlot != null && !GameUI.PointerOver(typeof(GallerySlotFooterButton)))
		{
			m_HoverSlot.SetHovered(hovered: false);
			m_VideoPlayer.Pause();
			m_GalleryVideo.Open(m_HoverSlot, m_CurrentPageIndex * Gallery.NUM_SLOTS_PER_PAGE + Array.IndexOf(m_Slots, m_HoverSlot), GetNumVideos());
			InterfaceAudio.Play("ui_window_open");
		}
		if (!Gallery.DownloadInProgress())
		{
			GalleryItemsFilterUpdate();
			if (m_LastGalleryFilterParameters.DifferentFrom(m_GalleryFilterParameters) || m_ForceRefresh)
			{
				Gallery.ClearCache();
				Gallery.ClearGallerySearchStatus();
				m_CurrentPageIndex = 0;
				m_GalleryFilterParameters.m_PageNum = m_CurrentPageIndex;
				ShowLoadingScreen(loadingScreenVisible: true);
				RefreshContent();
				m_ForceRefresh = false;
			}
		}
		m_CreatedByContainer.gameObject.SetActive(!GameManager.IsSteamOffline());
	}

	public void UpdateForCurrentDevice()
	{
		m_RootPanel.anchoredPosition = new Vector2(0f, (Game.IsRunningOnSteamDeck() || GameInput.GetActiveGameDevice() == GameDevice.Gamepad) ? 7 : 0);
	}

	public void ForceRefresh()
	{
		m_ForceRefresh = true;
	}

	public void SetOptions(GallerySortBy sortBy, bool unbreakingOnly, bool underBudgetOnly, bool winsOnly, bool allowCheats)
	{
		m_SortByDropdown.SetValueWithoutNotify((int)sortBy);
		m_IncludeCheatsToggle.isOn = allowCheats;
		m_ShowOnlyWinsToggle.isOn = winsOnly;
		m_UnbreakingToggle.isOn = unbreakingOnly;
		m_UnderBudgetToggle.isOn = underBudgetOnly;
	}

	public void OpenWorkshopItem(string title, string id)
	{
		PopulateLevelNameDropdown(title, id);
		CampaignWorldDropdown.Populate(m_WorldNameDropdown, includeAll: true);
		Open(id);
	}

	public void OpenCampaignLevel(CampaignLevel level)
	{
		PopulateLevelNameDropdown(string.Empty, string.Empty);
		CampaignWorldDropdown.Populate(m_WorldNameDropdown, includeAll: true);
		Open((level != null) ? level.m_Id : string.Empty);
	}

	public void OpenFromMainMenu(string worldID)
	{
		Init();
		PopulateLevelNameDropdown(string.Empty, string.Empty);
		CampaignWorldDropdown.Populate(m_WorldNameDropdown, includeAll: true);
		Open(string.Empty);
	}

	private void Open(string levelID)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			Init();
			base.gameObject.SetActive(value: true);
			m_GalleryVideo.gameObject.SetActive(value: false);
			if (!m_TabHasBeenSelected)
			{
				SelectTab(GalleryTab.ALL);
				m_TabHasBeenSelected = true;
			}
			m_LoadingScreen.SetActive(value: false);
			m_FailedLoadingContainer.SetActive(value: false);
			m_ZeroItemsMessage.SetActive(value: false);
			m_Content.gameObject.SetActive(value: false);
			m_Footer.SetActive(value: false);
			UpdateDropdownItemLoc();
			if (!string.IsNullOrEmpty(levelID))
			{
				m_RestoreLevelIDFilter = m_LevelNameDropdown.captionText.text;
				SetLevelFilterByID(levelID);
				SetCreatedByFilterToCommunity();
			}
			else
			{
				m_RestoreLevelIDFilter = string.Empty;
			}
			m_ReturnToWorkshop = false;
			m_ReturnToWeekliesItemID = string.Empty;
			m_ReturnToMainMenu = false;
			m_HoverSlot = null;
		}
	}

	public bool SetLevelFilterForSlot(GallerySlot slot)
	{
		if (slot == null || slot.GetGalleryItem == null)
		{
			return false;
		}
		string levelID = slot.GetGalleryItem.GetLevelID();
		if (string.IsNullOrEmpty(levelID))
		{
			return false;
		}
		if (!m_LevelFilterReverseMap.ContainsKey(levelID))
		{
			string levelNameFormatted = slot.GetGalleryItem.GetLevelNameFormatted();
			m_LevelNameDropdown.AddOptions(new List<string> { levelNameFormatted });
			m_LevelFilterMap.Add(m_LevelNameDropdown.options.Count - 1, levelID);
			m_LevelFilterReverseMap.Add(levelID, m_LevelNameDropdown.options.Count - 1);
		}
		m_LevelNameDropdown.value = m_LevelFilterReverseMap[levelID];
		m_LevelNameDropdown.captionText.text = m_LevelNameDropdown.options[m_LevelNameDropdown.value].text;
		m_CreatedByDropdown.value = 0;
		m_CreatedByDropdown.captionText.text = m_CreatedByDropdown.options[0].text;
		return true;
	}

	public void SetLevelFilterByID(string levelID)
	{
		if (m_LevelFilterReverseMap.ContainsKey(levelID))
		{
			m_LevelNameDropdown.value = m_LevelFilterReverseMap[levelID];
			m_LevelNameDropdown.captionText.text = m_LevelNameDropdown.options[m_LevelNameDropdown.value].text;
		}
	}

	public void CloseNoReturn()
	{
		if (base.gameObject.activeInHierarchy)
		{
			m_GalleryVideo.Close();
			base.gameObject.SetActive(value: false);
			m_VideoPlayer.Stop();
			if (!string.IsNullOrEmpty(m_RestoreLevelIDFilter))
			{
				SetLevelFilterByID(m_RestoreLevelIDFilter);
				m_RestoreLevelIDFilter = string.Empty;
			}
		}
	}

	public void Close()
	{
		if (base.gameObject.activeInHierarchy)
		{
			CloseNoReturn();
			if (m_ReturnToWorkshop && GameUI.m_Instance.m_Workshop.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_Workshop.m_RootPanel.gameObject.SetActive(value: true);
				GameUI.m_Instance.m_Workshop.m_Ducking.gameObject.SetActive(value: true);
				GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.gameObject.SetActive(value: true);
			}
			else if (!string.IsNullOrEmpty(m_ReturnToWeekliesItemID))
			{
				GameUI.m_Instance.m_WeeklyChallenges.Open(m_ReturnToWeekliesItemID);
			}
			else if (m_ReturnToMainMenu)
			{
				GameUI.m_Instance.m_MainMenuNew.Open();
			}
		}
	}

	private void ShowLoadingScreen(bool loadingScreenVisible)
	{
		if (loadingScreenVisible)
		{
			m_LoadingScreen.SetActive(value: true);
			m_Content.gameObject.SetActive(value: false);
			m_Footer.gameObject.SetActive(value: false);
			m_ZeroItemsMessage.SetActive(value: false);
			m_FailedLoadingContainer.SetActive(value: false);
		}
		else
		{
			m_LoadingScreen.SetActive(value: false);
			m_FailedLoadingContainer.SetActive(value: false);
			m_Content.gameObject.SetActive(value: true);
			m_Footer.gameObject.SetActive(value: true);
		}
	}

	public void OnPlayThumbnail(GallerySlot source)
	{
		if (!(source == null) && source.GetGalleryItem != null && !string.IsNullOrEmpty(source.GetGalleryItem.GetVideoUrl()))
		{
			m_VideoPlayer.url = source.GetGalleryItem.GetVideoUrl();
			m_VideoPlayer.frame = source.FrameIndex;
			m_VideoPlayer.Stop();
			m_VideoPlayer.Prepare();
			m_PreparingSlot = source;
		}
	}

	public void OnHoverChange(GallerySlot slot, bool hover)
	{
		if (slot != null && slot.GetGalleryItem == null)
		{
			slot = null;
		}
		if ((bool)slot)
		{
			slot.SetHovered(!m_GalleryVideo.gameObject.activeInHierarchy && hover);
		}
		if (m_GalleryVideo.gameObject.activeInHierarchy)
		{
			return;
		}
		if (hover && (bool)slot)
		{
			m_HoverSlot = slot;
			OnPlayThumbnail(slot);
			return;
		}
		if (m_VideoPlayer.isPlaying)
		{
			m_VideoPlayer.Pause();
			if (m_HoverSlot.PreviewTexture != null)
			{
				m_HoverSlot.m_RawImage.texture = m_HoverSlot.PreviewTexture;
				m_HoverSlot.SetProgress(0f);
			}
		}
		m_HoverSlot = null;
	}

	public GallerySlot GetSlotForIndex(int index)
	{
		if (index < 0 || index >= m_Slots.Length)
		{
			return null;
		}
		return m_Slots[index];
	}

	public bool OnFirstPage()
	{
		return m_CurrentPageIndex == 0;
	}

	public bool OnLastPage()
	{
		return m_CurrentPageIndex == GetNumPages() - 1;
	}

	public int GetCurrentPageNum()
	{
		return m_CurrentPageIndex;
	}

	public void DownloadAllItemsByOwnerAsync(string ownerId, string createdByName)
	{
		SetLevelFilterByValue(0);
		SetWorldFilterByName(Localize.Get("UI_ALL"));
		AddNameToCreatedByDropdown(createdByName);
		m_CurrentPageIndex = 0;
		if (!Gallery.m_NameOwnerIdMap.ContainsKey(createdByName))
		{
			Gallery.m_NameOwnerIdMap.Add(createdByName, ownerId);
		}
		else
		{
			Gallery.m_NameOwnerIdMap[createdByName] = ownerId;
		}
	}

	private void PrepareCompleted(VideoPlayer videoPlayer)
	{
		if (!(m_VideoPlayer.texture == null) && !(m_HoverSlot == null) && !(m_PreparingSlot != m_HoverSlot) && !(m_PreparingSlot.PreviewTexture == null))
		{
			videoPlayer.isLooping = true;
			videoPlayer.targetTexture = m_PreparingSlot.RenderTexture;
			Graphics.Blit(m_PreparingSlot.PreviewTexture, m_PreparingSlot.RenderTexture);
			m_PreparingSlot.m_RawImage.texture = m_PreparingSlot.RenderTexture;
			videoPlayer.Play();
			m_PreparingSlot = null;
		}
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnNextPage()
	{
		if (MoveToNextPage())
		{
			InterfaceAudio.Play("ui_menu_select");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void OnPreviousPage()
	{
		if (MoveToPreviousPage())
		{
			InterfaceAudio.Play("ui_menu_select");
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	public bool MoveToPreviousPage()
	{
		if (m_LoadingScreen.gameObject.activeInHierarchy || m_CurrentPageIndex == 0)
		{
			return false;
		}
		m_CurrentPageIndex--;
		GalleryItemsFilterUpdate();
		RefreshContent();
		return true;
	}

	public bool MoveToNextPage()
	{
		if (OnLastPage() || Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) != GallerySearchResultStatus.CACHED)
		{
			return false;
		}
		m_CurrentPageIndex++;
		GalleryItemsFilterUpdate();
		if ((m_CurrentPageIndex + 1) * Gallery.NUM_SLOTS_PER_PAGE < GallerySearchResultCache.m_TotalCount)
		{
			LoadPageInBackground(m_CurrentPageIndex + 1);
		}
		if (Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) == GallerySearchResultStatus.CACHED)
		{
			RefreshSlots();
		}
		RefreshFooter();
		return true;
	}

	public void Refresh(int pageIndex)
	{
		if (pageIndex == m_CurrentPageIndex)
		{
			ShowLoadingScreen(loadingScreenVisible: false);
			RefreshSlots();
			RefreshFooter();
		}
	}

	public string GetLevelID()
	{
		return m_LevelFilterMap[m_LevelNameDropdown.value];
	}

	private void GalleryItemsFilterUpdate()
	{
		string levelId = string.Empty;
		string worldId = string.Empty;
		if (m_LevelFilterMap.ContainsKey(m_LevelNameDropdown.value))
		{
			levelId = m_LevelFilterMap[m_LevelNameDropdown.value];
		}
		string worldName = ((m_WorldNameDropdown.value == 0) ? string.Empty : m_WorldNameDropdown.captionText.text);
		if (CampaignWorldDropdown.ContainsKey(worldName))
		{
			worldId = CampaignWorldDropdown.GetValue(worldName).m_Id;
		}
		string ownership = ResolveCreatedBy(m_CreatedByFilter);
		GallerySortBy value = (GallerySortBy)m_SortByDropdown.value;
		m_GalleryFilterParameters.Set(m_CurrentPageIndex + 1, worldId, levelId, ownership, value, m_CurrentTab == GalleryTab.FEATURED, m_UnderBudgetToggle.isOn, m_UnbreakingToggle.isOn, m_ShowOnlyWinsToggle.isOn, m_IncludeCheatsToggle.isOn, m_CuratedToggle.isOn);
	}

	private void DownloadItemsAsync(int pageIndex)
	{
		if (Gallery.GetGallerySearchResultStatus(pageIndex) == GallerySearchResultStatus.LOADING)
		{
			Debug.Log($"Aborting load for {pageIndex}");
			return;
		}
		Gallery.SetFilter(m_GalleryFilterParameters);
		Gallery.SetGallerySearchResultStatus(pageIndex, GallerySearchResultStatus.LOADING);
		m_LastGalleryFilterParameters.CopyFrom(m_GalleryFilterParameters);
		Gallery.DownloadAllAsync(pageIndex, Gallery.MAX_ENTRIES_FOR_SEARCH);
	}

	private string ResolveCreatedBy(GalleryCreatedBy createdBy)
	{
		switch (createdBy)
		{
		case GalleryCreatedBy.COMMUNITY:
			return string.Empty;
		case GalleryCreatedBy.FRIENDS:
			return GalleryFilterParameters.OWNERSHIP_FRIENDS_TAG;
		case GalleryCreatedBy.SOLO:
			return SteamUtils.GetSteamId();
		case GalleryCreatedBy.USER:
			if (!Gallery.m_NameOwnerIdMap.ContainsKey(m_CreatedByDropdown.captionText.text))
			{
				return string.Empty;
			}
			return Gallery.m_NameOwnerIdMap[m_CreatedByDropdown.captionText.text];
		default:
			Debug.LogWarningFormat("Unexpected GalleryCreatedBy value: {0}", createdBy.ToString());
			return string.Empty;
		}
	}

	private void PopulateSortByDropdown()
	{
		m_SortByDropdown.ClearOptions();
		List<string> list = new List<string>();
		list.Add(Localize.Get("UI_MOST_RECENT"));
		list.Add(Localize.Get("UI_LOWEST_BUDGET"));
		list.Add(Localize.Get("UI_HIGHEST_BUDGET"));
		m_SortByDropdown.AddOptions(list);
	}

	private void PopulateCreatedByDropdown()
	{
		m_CreatedByDropdown.ClearOptions();
		List<string> list = new List<string>();
		list.Add(Localize.Get("UI_COMMUNITY"));
		list.Add(Localize.Get("UI_FRIENDS"));
		list.Add(Localize.Get("UI_SELF"));
		m_CreatedByDropdown.AddOptions(list);
	}

	private void PopulateSlots()
	{
		m_Slots = new GallerySlot[Gallery.NUM_SLOTS_PER_PAGE];
		for (int i = 0; i < Gallery.NUM_SLOTS_PER_PAGE; i++)
		{
			GallerySlot component = UnityEngine.Object.Instantiate(m_GallerySlotPrefab, m_ContentGrid.transform).GetComponent<GallerySlot>();
			component.gameObject.name = "Slot" + i;
			m_Slots[i] = component;
			component.m_OnHoverChangeCallback = (GallerySlot.OnHoverChangeDelegate)Delegate.Combine(component.m_OnHoverChangeCallback, new GallerySlot.OnHoverChangeDelegate(OnHoverChange));
		}
	}

	private void DestroySlots()
	{
		for (int i = 0; i < Gallery.NUM_SLOTS_PER_PAGE; i++)
		{
			UnityEngine.Object.Destroy(m_Slots[i].gameObject);
		}
		m_Slots = null;
	}

	private void LoadPageInBackground(int pageIndex)
	{
		if (!GallerySearchResultCache.IsPageCached(pageIndex))
		{
			DownloadItemsAsync(pageIndex);
		}
	}

	private void RefreshContent()
	{
		if (!GallerySearchResultCache.IsPageCached(m_CurrentPageIndex))
		{
			ShowLoadingScreen(loadingScreenVisible: true);
			DownloadItemsAsync(m_CurrentPageIndex);
		}
		else
		{
			ShowLoadingScreen(loadingScreenVisible: false);
			RefreshSlots();
			RefreshFooter();
		}
	}

	private void RefreshSlots()
	{
		GallerySearchResult gallerySearchResult = GallerySearchResultCache.Get(m_CurrentPageIndex);
		bool flag = false;
		for (int i = 0; i < Gallery.NUM_SLOTS_PER_PAGE; i++)
		{
			if (gallerySearchResult != null && i < gallerySearchResult.m_Resources.Count)
			{
				m_Slots[i].SetDisplayedItem(new GalleryItem(gallerySearchResult.m_Resources[i]));
				if (!GameManager.IsSteamOffline())
				{
					SteamPersonas.RequestUserInfo(m_Slots[i].GetGalleryItem.GetOwnerId());
				}
				flag = true;
			}
			else
			{
				m_Slots[i].SetHidden();
			}
		}
		if (Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) != GallerySearchResultStatus.LOADING)
		{
			m_ZeroItemsMessage.SetActive(!flag);
		}
		else
		{
			m_ZeroItemsMessage.SetActive(value: false);
		}
		if (flag)
		{
			m_Content.gameObject.SetActive(value: true);
			m_Footer.gameObject.SetActive(value: true);
		}
	}

	private void RefreshFooter()
	{
		m_Footer.SetActive(Gallery.GetGallerySearchResultStatus(m_CurrentPageIndex) == GallerySearchResultStatus.CACHED);
		m_PagesText.text = $"{m_CurrentPageIndex + 1} / {GetNumPages()}";
		m_PreviousPageButton.interactable = m_CurrentPageIndex > 0;
		m_NextPageButton.interactable = !OnLastPage();
	}

	private bool NextCursorExists(int pageIndex)
	{
		GallerySearchResult gallerySearchResult = GallerySearchResultCache.Get(pageIndex);
		if (gallerySearchResult != null)
		{
			return !string.IsNullOrEmpty(gallerySearchResult.m_NextCursor);
		}
		return false;
	}

	private int GetNumPages()
	{
		int numVideos = GetNumVideos();
		if (numVideos == 0)
		{
			return 1;
		}
		return Mathf.CeilToInt((float)numVideos / (float)Gallery.NUM_SLOTS_PER_PAGE);
	}

	private int GetNumVideos()
	{
		return GallerySearchResultCache.m_TotalCount;
	}

	private void OnSortByChanged()
	{
		Profiles.m_ActiveProfile.m_GallerySortBy = (GallerySortBy)m_SortByDropdown.value;
		Profiles.SaveActiveProfile();
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnWorldNameChanged()
	{
		PopulateLevelNameDropdown(string.Empty, string.Empty);
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnLevelNameChanged()
	{
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnCreatedByChanged()
	{
		if (m_CreatedByDropdown.value != (int)m_CreatedByFilter)
		{
			m_CreatedByFilter = (GalleryCreatedBy)m_CreatedByDropdown.value;
		}
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnUnderBudgetChanged()
	{
		Profiles.m_ActiveProfile.m_GalleryUnderBudget = m_UnderBudgetToggle.isOn;
		Profiles.SaveActiveProfile();
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnUnbreakingChanged()
	{
		Profiles.m_ActiveProfile.m_GalleryUnbreaking = m_UnbreakingToggle.isOn;
		Profiles.SaveActiveProfile();
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnCuratedChanged()
	{
		Profiles.m_ActiveProfile.m_CuratedReplays = m_CuratedToggle.isOn;
		Profiles.SaveActiveProfile();
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnShowWinsOnlyChanged()
	{
		Profiles.m_ActiveProfile.m_GalleryShowOnlyWins = m_ShowOnlyWinsToggle.isOn;
		Profiles.SaveActiveProfile();
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void OnIncludeCheatsChanged()
	{
		Profiles.m_ActiveProfile.m_GalleryIncludeCheats = m_IncludeCheatsToggle.isOn;
		Profiles.SaveActiveProfile();
		if (GameUI.m_Instance.m_Gallery.gameObject.activeInHierarchy)
		{
			InterfaceAudio.Play("ui_menu_select");
		}
	}

	private void PopulateLevelNameDropdown(string extraTitle, string extraId)
	{
		m_LevelFilterMap.Clear();
		m_LevelFilterReverseMap.Clear();
		List<string> list = new List<string>();
		list.Add(Localize.Get("UI_ALL"));
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if ((campaignWorld.IsSecretWorld() && !GameManager.IsSecretWorldUnlocked()) || BlockedByWorldFilter(campaignWorld))
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!campaignLevel.IsTutorial())
				{
					string fullNameFormatted = campaignLevel.GetFullNameFormatted();
					list.Add(fullNameFormatted);
					m_LevelFilterMap.Add(list.IndexOf(fullNameFormatted), campaignLevel.m_Id);
					m_LevelFilterReverseMap.Add(campaignLevel.m_Id, list.IndexOf(fullNameFormatted));
				}
			}
		}
		if (!string.IsNullOrEmpty(extraTitle) && !string.IsNullOrEmpty(extraId))
		{
			if (m_LevelFilterReverseMap.ContainsKey(extraTitle))
			{
				Debug.LogWarning("Trying to add level with duplicated name to gallery level name dropdown: " + extraTitle);
			}
			else
			{
				list.Add(extraTitle);
				m_LevelFilterMap.Add(list.IndexOf(extraTitle), extraId);
				m_LevelFilterReverseMap.Add(extraId, list.IndexOf(extraTitle));
			}
		}
		m_LevelNameDropdown.ClearOptions();
		m_LevelNameDropdown.AddOptions(list);
	}

	private bool BlockedByWorldFilter(CampaignWorld world)
	{
		if (m_WorldNameDropdown.value != 0)
		{
			return m_WorldNameDropdown.value - 1 != Array.IndexOf(CampaignWorlds.m_Instance.m_Worlds, world);
		}
		return false;
	}

	private int SortByName(string a, string b)
	{
		return a.CompareTo(b);
	}

	private void SetLevelFilterByValue(int value)
	{
		if (value >= 0 && value < m_LevelNameDropdown.options.Count)
		{
			m_LevelNameDropdown.value = value;
			m_LevelNameDropdown.captionText.text = m_LevelNameDropdown.options[m_LevelNameDropdown.value].text;
		}
	}

	private void SetCreatedByFilterToCommunity()
	{
		m_CreatedByDropdown.value = 0;
		m_CreatedByDropdown.captionText.text = m_CreatedByDropdown.options[0].text;
		m_CreatedByFilter = GalleryCreatedBy.COMMUNITY;
	}

	private void SetWorldFilterByName(string name)
	{
		for (int i = 0; i < m_WorldNameDropdown.options.Count; i++)
		{
			if (m_WorldNameDropdown.options[i].text == name)
			{
				m_WorldNameDropdown.value = i;
				m_WorldNameDropdown.captionText.text = name;
				break;
			}
		}
	}

	private void AddNameToCreatedByDropdown(string name)
	{
		PopulateCreatedByDropdown();
		m_CreatedByDropdown.AddOptions(new List<string> { name });
		for (int i = 0; i < m_CreatedByDropdown.options.Count; i++)
		{
			if (m_CreatedByDropdown.options[i].text == name)
			{
				m_CreatedByDropdown.value = i;
				m_CreatedByDropdown.captionText.text = name;
				break;
			}
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
		if (Input.GetKeyDown(KeyCode.RightArrow) && m_NextPageButton.gameObject.activeInHierarchy && m_NextPageButton.interactable)
		{
			OnNextPage();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow) && m_PreviousPageButton.gameObject.activeInHierarchy && m_PreviousPageButton.interactable)
		{
			OnPreviousPage();
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

	public void CycleToNextPage()
	{
		if (OnLastPage())
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
		if (m_CurrentPageIndex == 0)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			ExecuteEvents.Execute(m_PreviousPageButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void UpdateDropdownItemLoc()
	{
		UpdateSortByLoc();
		UpdateCreatedByLoc();
	}

	private void UpdateSortByLoc()
	{
		m_SortByDropdown.options[0].text = Localize.Get("UI_MOST_RECENT");
		m_SortByDropdown.options[1].text = Localize.Get("UI_LOWEST_BUDGET");
		m_SortByDropdown.options[2].text = Localize.Get("UI_HIGHEST_BUDGET");
		m_SortByDropdown.captionText.text = m_SortByDropdown.options[m_SortByDropdown.value].text;
	}

	private void UpdateCreatedByLoc()
	{
		m_CreatedByDropdown.options[0].text = Localize.Get("UI_COMMUNITY");
		m_CreatedByDropdown.options[1].text = Localize.Get("UI_FRIENDS");
		m_CreatedByDropdown.options[2].text = Localize.Get("UI_SELF");
		m_CreatedByDropdown.captionText.text = m_CreatedByDropdown.options[m_CreatedByDropdown.value].text;
	}

	private void OnFeaturedTab()
	{
		if (m_CurrentTab != GalleryTab.FEATURED)
		{
			SelectTab(GalleryTab.FEATURED);
		}
	}

	private void OnAllTab()
	{
		if (m_CurrentTab != GalleryTab.ALL)
		{
			SelectTab(GalleryTab.ALL);
		}
	}

	private void OnRetry()
	{
		Gallery.ClearCache();
		m_CurrentPageIndex = 0;
		RefreshContent();
	}

	public void SelectTab(GalleryTab tab)
	{
		m_CurrentTab = tab;
		switch (m_CurrentTab)
		{
		case GalleryTab.ALL:
			AcvitateTabUI(m_AllTabButton);
			break;
		case GalleryTab.FEATURED:
			AcvitateTabUI(m_FeaturedTabButton);
			break;
		default:
			Debug.LogWarningFormat("Unrecognized tab in SelecTab: {0}", tab.ToString());
			break;
		}
	}

	private void AcvitateTabUI(SandboxTab tab)
	{
		m_AllTabButton.m_Background.color = ((tab == m_AllTabButton) ? m_TabActiveColor : m_TabInActiveColor);
		m_FeaturedTabButton.m_Background.color = ((tab == m_FeaturedTabButton) ? m_TabActiveColor : m_TabInActiveColor);
		m_AllTabButton.m_BackgroundRectTransform.offsetMin = ((tab == m_AllTabButton) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_FeaturedTabButton.m_BackgroundRectTransform.offsetMin = ((tab == m_FeaturedTabButton) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
	}

	private int GetNumVisibleSlots()
	{
		int num = 0;
		GallerySlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		return num;
	}

	private void ShowGamepadLegend()
	{
		if (m_PagesText.gameObject.activeInHierarchy && GetNumPages() > 1)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.DPAD_HORIZONTAL, Localize.Get("UI_CHANGE_PAGE"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		}
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}

	private async void AdminChangeTag(GalleryItem item, string url)
	{
		string id = m_HoverSlot.GetGalleryItem.GetId();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("public_id", LeaderboardReplay.AES_Encrypt(id));
		dictionary.Add("tag", GalleryFilterParameters.CURATED_TAG);
		try
		{
			FormUrlEncodedContent content = new FormUrlEncodedContent(dictionary);
			item.m_TagChangeInProgress = true;
			HttpResponseMessage obj = await Game.m_HttpClient.PostAsync(url, content);
			item.m_TagChangeInProgress = false;
			if (obj.IsSuccessStatusCode)
			{
				if (url == Game.ADMIN_ADD_TAG_URL)
				{
					item.SetCuratedTag();
				}
				else
				{
					item.ClearCuratedTag();
				}
			}
		}
		catch (Exception ex)
		{
			Debug.Log("AdminChangeTag failed due to exception: " + ex.Message);
		}
	}
}
