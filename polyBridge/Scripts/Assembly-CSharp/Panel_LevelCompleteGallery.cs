using System;
using System.Collections.Generic;
using CloudinaryDotNet.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Panel_LevelCompleteGallery : MonoBehaviour
{
	[Header("Header")]
	public TextMeshProUGUI m_HeaderText;

	[Header("Body")]
	public GameObject m_GallerySlotSimplePrefab;

	public GameObject m_WaitAnimation;

	public GameObject m_Root;

	public TextMeshProUGUI m_NoReplaysText;

	[Header("Footer")]
	public Button m_ViewGalleryButton;

	[Header("Panel Animate")]
	public Button m_GalleryCollapseButton;

	public Image m_GalleryCollapseIcon;

	public PanelAnimate m_GalleryPanelAnimate;

	private GallerySlot[] m_Slots;

	private Dictionary<string, List<GalleryItem>> m_Cache = new Dictionary<string, List<GalleryItem>>();

	private string m_LevelId;

	public VideoPlayer m_VideoPlayer;

	private GallerySlot m_HoverSlot;

	private GallerySlot m_PreparingSlot;

	private bool m_SearchResultReady;

	private GallerySearchResult m_SearchResultToProcess;

	public readonly int NUM_GALLERY_SLOTS = 3;

	private static string m_LevelIdForLastCachedGalleryReplaysSearchResult;

	private static SearchResult m_CachedGalleryReplaysSearchResult;

	private void Start()
	{
		m_VideoPlayer.prepareCompleted += PrepareCompleted;
		m_ViewGalleryButton.onClick.AddListener(OnViewGallery);
		m_GalleryCollapseButton.onClick.AddListener(OnGalleryCollapse);
	}

	private void OnEnable()
	{
		if (m_Slots == null)
		{
			CreateSlots();
		}
	}

	private void Update()
	{
		if (m_SearchResultReady)
		{
			int num = 0;
			if (m_SearchResultToProcess != null && m_SearchResultToProcess.m_Resources.Count > 0)
			{
				num = ProcessSearchResult(m_SearchResultToProcess);
				m_SearchResultToProcess = null;
			}
			if (num == 0)
			{
				m_NoReplaysText.gameObject.SetActive(value: true);
				m_NoReplaysText.text = Localize.Get("UI_NO_REPLAYS_FOUND");
				HideSlots();
			}
			m_SearchResultReady = false;
			m_WaitAnimation.SetActive(value: false);
			m_Root.gameObject.SetActive(value: true);
		}
		if ((bool)m_HoverSlot && m_VideoPlayer.isPlaying && m_VideoPlayer.frame >= 0)
		{
			m_HoverSlot.SetProgress((float)(m_VideoPlayer.frame + 1) / (float)m_VideoPlayer.frameCount);
			m_HoverSlot.FrameIndex = m_VideoPlayer.frame;
		}
		GallerySlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].UpdateManual();
		}
	}

	public void Download(string levelId)
	{
		m_HeaderText.gameObject.SetActive(value: true);
		m_NoReplaysText.gameObject.SetActive(value: false);
		m_ViewGalleryButton.gameObject.SetActive(value: false);
		m_WaitAnimation.SetActive(value: false);
		m_LevelId = levelId;
		if (!Profiles.m_ActiveProfile.m_HideOtherPlayerSolutions)
		{
			Load(m_LevelId);
		}
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
		m_ViewGalleryButton.gameObject.SetActive(GameManager.GameModeIsCampaignOrWorkshop());
		m_GalleryPanelAnimate.ForceState(Profiles.m_ActiveProfile.m_HideOtherPlayerSolutions ? PanelAnimateState.OFF : PanelAnimateState.ON);
	}

	public void Close()
	{
		m_SearchResultReady = false;
		m_SearchResultToProcess = null;
		m_WaitAnimation.SetActive(value: false);
		m_VideoPlayer.Stop();
	}

	public void MaybeCacheGallerySearchResult(string levelId)
	{
		if (levelId.Length == 3 && !Game.IsCurrentLevelTutorial() && !m_Cache.ContainsKey(levelId) && m_LevelIdForLastCachedGalleryReplaysSearchResult != levelId)
		{
			SearchResult exampleReplays = GalleryCurate.GetExampleReplays(levelId, 3);
			if (exampleReplays != null)
			{
				m_CachedGalleryReplaysSearchResult = exampleReplays;
				m_LevelIdForLastCachedGalleryReplaysSearchResult = levelId;
			}
		}
	}

	private void Load(string levelId)
	{
		if (!string.IsNullOrEmpty(levelId))
		{
			if (m_Cache.ContainsKey(levelId))
			{
				LoadFromCache(levelId);
			}
			else
			{
				LoadAsync(levelId);
			}
		}
	}

	private void LoadFromCache(string levelId)
	{
		List<GalleryItem> list = m_Cache[levelId];
		for (int i = 0; i < Mathf.Min(list.Count, NUM_GALLERY_SLOTS); i++)
		{
			m_Slots[i].SetDisplayedItem(list[i]);
		}
		if (!AllSlotsHavePreviewLoaded())
		{
			m_WaitAnimation.SetActive(value: false);
			m_Root.gameObject.SetActive(value: false);
			m_NoReplaysText.gameObject.SetActive(value: true);
			m_NoReplaysText.text = (GameManager.IsSteamOffline() ? Localize.Get("UI_STEAM_OFFLINE") : Localize.Get("UI_NO_REPLAYS_FOUND"));
		}
	}

	private void CopySlotsToCache(string levelId)
	{
		if (!m_Cache.ContainsKey(levelId))
		{
			List<GalleryItem> list = new List<GalleryItem>();
			m_Cache.Add(levelId, list);
			GallerySlot[] slots = m_Slots;
			foreach (GallerySlot gallerySlot in slots)
			{
				list.Add(gallerySlot.GetGalleryItem);
			}
		}
	}

	private void LoadAsync(string levelId)
	{
		m_SearchResultToProcess = null;
		if (levelId.Length == 3)
		{
			if (levelId == m_LevelIdForLastCachedGalleryReplaysSearchResult)
			{
				SearchSuccess(m_CachedGalleryReplaysSearchResult, 0);
			}
			else
			{
				SearchSuccess(GalleryCurate.GetExampleReplays(levelId, 3), 0);
			}
		}
		else
		{
			CloudinaryManager.SearchAsyncExampleSolutions(SearchFail, SearchSuccess, NUM_GALLERY_SLOTS, SteamUtils.GetSteamId(), levelId, Profiles.m_ActiveProfile.m_CuratedReplays ? GalleryFilterParameters.CURATED_TAG : string.Empty, GalleryFilterParameters.FAIL_TAG + "," + GalleryFilterParameters.CHEAT_TAG + "," + GalleryFilterParameters.MOD_TAG);
		}
		m_WaitAnimation.SetActive(value: true);
		m_Root.gameObject.SetActive(value: false);
		m_NoReplaysText.gameObject.SetActive(value: false);
	}

	private void SearchSuccess(SearchResult searchResult, int pageIndex)
	{
		m_SearchResultReady = true;
		try
		{
			if (searchResult != null && searchResult.Resources != null && searchResult.Resources.Count > 0)
			{
				GallerySearchResultCache.m_TotalCount = searchResult.TotalCount;
				m_SearchResultToProcess = new GallerySearchResult(searchResult.Resources, string.Empty);
				m_SearchResultToProcess.RequestPreviewImages();
			}
		}
		catch (Exception ex)
		{
			Debug.Log("HANDLED: " + ex.Message);
			m_SearchResultToProcess = null;
		}
	}

	private void SearchFail(string errorMessage, int pageIndex)
	{
		m_SearchResultReady = true;
		m_SearchResultToProcess = null;
	}

	private int ProcessSearchResult(GallerySearchResult searchResult)
	{
		if (searchResult == null || searchResult.m_Resources.Count == 0)
		{
			return 0;
		}
		List<SearchResource> list = new List<SearchResource>();
		foreach (SearchResource resource in searchResult.m_Resources)
		{
			if (resource != null && GalleryMetaData.GetLevelID(resource.Context) == m_LevelId)
			{
				list.Add(resource);
			}
		}
		if (list.Count == 0)
		{
			return 0;
		}
		int num = Mathf.Min(NUM_GALLERY_SLOTS, list.Count);
		for (int i = 0; i < num; i++)
		{
			m_Slots[i].SetDisplayedItem(new GalleryItem(list[i]));
			m_Slots[i].gameObject.SetActive(value: true);
		}
		for (int j = num; j < NUM_GALLERY_SLOTS; j++)
		{
			m_Slots[j].gameObject.SetActive(value: false);
		}
		CopySlotsToCache(m_LevelId);
		return list.Count;
	}

	private bool AllSlotsHavePreviewLoaded()
	{
		GallerySlot[] slots = m_Slots;
		foreach (GallerySlot gallerySlot in slots)
		{
			gallerySlot.UpdateManual();
			if ((bool)gallerySlot && gallerySlot.GetGalleryItem != null && gallerySlot.GetGalleryItem.m_PreviewTexture == null)
			{
				return false;
			}
		}
		return true;
	}

	private void PrepareCompleted(VideoPlayer videoPlayer)
	{
		if (!(m_VideoPlayer.texture == null) && !(m_HoverSlot == null) && !(m_PreparingSlot != m_HoverSlot))
		{
			videoPlayer.isLooping = true;
			videoPlayer.targetTexture = m_PreparingSlot.RenderTexture;
			Graphics.Blit(m_PreparingSlot.m_RawImage.texture, m_PreparingSlot.RenderTexture);
			m_PreparingSlot.m_RawImage.texture = m_PreparingSlot.RenderTexture;
			videoPlayer.Play();
			m_PreparingSlot = null;
		}
	}

	public void OnHoverChange(GallerySlot slot, bool hover)
	{
		if (hover)
		{
			m_HoverSlot = slot;
			OnPlayThumbnail(slot);
			return;
		}
		m_HoverSlot = null;
		if (m_VideoPlayer.isPlaying)
		{
			m_VideoPlayer.Pause();
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

	private void OnViewGallery()
	{
		Gallery.LaunchForCurrentLevel();
		InterfaceAudio.Play("ui_window_open");
	}

	private void OnGalleryCollapse()
	{
		if (m_GalleryPanelAnimate.GetState() == PanelAnimateState.ON || m_GalleryPanelAnimate.GetState() == PanelAnimateState.ANIMATING_ON)
		{
			m_GalleryPanelAnimate.Play(on: false, GalleryPanelCollapsed);
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
		else
		{
			m_GalleryPanelAnimate.Play(on: true, GalleryPanelExpanded);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	private void GalleryPanelExpanded()
	{
		m_GalleryCollapseIcon.transform.localScale = new Vector3(1f, 1f, 1f);
		Profiles.m_ActiveProfile.m_HideOtherPlayerSolutions = false;
		Profiles.SaveActiveProfile();
		Load(m_LevelId);
	}

	private void GalleryPanelCollapsed()
	{
		m_GalleryCollapseIcon.transform.localScale = new Vector3(-1f, 1f, 1f);
		Profiles.m_ActiveProfile.m_HideOtherPlayerSolutions = true;
		Profiles.SaveActiveProfile();
	}

	private void CreateSlots()
	{
		m_Slots = new GallerySlot[NUM_GALLERY_SLOTS];
		for (int i = 0; i < NUM_GALLERY_SLOTS; i++)
		{
			GallerySlot component = UnityEngine.Object.Instantiate(m_GallerySlotSimplePrefab, m_Root.transform).GetComponent<GallerySlot>();
			component.gameObject.name = "Slot" + i;
			m_Slots[i] = component;
			component.m_OnHoverChangeCallback = (GallerySlot.OnHoverChangeDelegate)Delegate.Combine(component.m_OnHoverChangeCallback, new GallerySlot.OnHoverChangeDelegate(OnHoverChange));
		}
	}

	private void HideSlots()
	{
		GallerySlot[] slots = m_Slots;
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].gameObject.SetActive(value: false);
		}
	}
}
