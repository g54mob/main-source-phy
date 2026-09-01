using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_LevelCompleteLeaderboard : MonoBehaviour
{
	public GameObject m_LeaderboardSlotPrefab;

	public GameObject m_Root;

	public GameObject m_Content;

	public GameObject m_WaitAnimation;

	public GameObject m_HideDuringLoad;

	public GameObject m_ScoresLoading;

	public GameObject m_HistoLoading;

	[Header("Histogram")]
	public Panel_Histogram m_HistogramPanel;

	public TextMeshProUGUI m_NoHistogramText;

	public TextMeshProUGUI m_NotEnoughScoresText;

	[Header("Leaderboard")]
	public TextMeshProUGUI m_NoLeaderboardsText;

	public TextMeshProUGUI m_NoLeaderboardsInWorkshopText;

	public GameObject m_ScrollbarParent;

	public GameObject m_LeaderboardDividers;

	public GameObject m_Legend;

	public GameObject m_LowStressLegend;

	[Header("Tabs")]
	public SandboxTab m_OverallTab;

	public SandboxTab m_UnbreakingTab;

	public SandboxTab m_LowestStressTab;

	[Header("Buttons")]
	public TwoStateButton m_TopScoresButton;

	public TwoStateButton m_AroundYouScoresButton;

	public TwoStateButton m_FriendsScoresButton;

	public TwoStateButton m_ShowAllButton;

	public TwoStateButton m_ShowUnbreakingButton;

	public TwoStateButton m_ShowLowestStressButton;

	[Header("Panel Animate")]
	public Button m_LeaderboardsCollapseButton;

	public Image m_LeaderboardsCollapseIcon;

	public PanelAnimate m_LeaderboardsPanelAnimate;

	[NonSerialized]
	public bool m_ForceRefresh;

	private LeaderboardFilterState m_CurrentFilterState = new LeaderboardFilterState();

	private LeaderboardFilterState m_PendingFilterState = new LeaderboardFilterState();

	private LeaderboardSlots m_LeaderboardSlots;

	private LeaderboardsView m_LeaderboardsView;

	private LeaderboardsFilter m_LeaderboardsFilter;

	private RectTransform m_ContentRectTransform;

	private int m_ContentAnchorY;

	private int m_NumRebuilds;

	private readonly int DOWNLOAD_DELAY_MS = 1000;

	private void Awake()
	{
		m_ContentRectTransform = m_Content.GetComponent<RectTransform>();
		m_NoHistogramText.gameObject.SetActive(value: false);
		m_NotEnoughScoresText.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		m_TopScoresButton.m_Button.onClick.AddListener(OnTopScores);
		m_AroundYouScoresButton.m_Button.onClick.AddListener(OnAroundYouScores);
		m_FriendsScoresButton.m_Button.onClick.AddListener(OnFriendsScores);
		m_ShowAllButton.m_Button.onClick.AddListener(OnShowAll);
		m_ShowUnbreakingButton.m_Button.onClick.AddListener(OnShowUnbreaking);
		m_ShowLowestStressButton.m_Button.onClick.AddListener(OnShowLowestStress);
		m_LeaderboardsCollapseButton.onClick.AddListener(OnLeaderboardsCollapse);
	}

	private void Update()
	{
		if (!IsDownloadingLeaderboard())
		{
			MaybeRefreshLeaderboard();
		}
		UpdateScoresLoadingDots();
	}

	private void LateUpdate()
	{
		if (m_ContentAnchorY != 0)
		{
			m_ContentRectTransform.anchoredPosition = new Vector3(0f, m_ContentAnchorY);
			LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.GetComponent<RectTransform>());
			m_NumRebuilds++;
			if (m_NumRebuilds == 2)
			{
				m_ContentAnchorY = 0;
			}
		}
	}

	public void Open()
	{
		base.gameObject.SetActive(value: true);
		if (m_LeaderboardSlots == null)
		{
			m_LeaderboardSlots = new LeaderboardSlots(m_LeaderboardSlotPrefab, m_Content.transform);
			SelectView(Profiles.m_ActiveProfile.m_LeaderboardsView);
			SelectFilter(Profiles.m_ActiveProfile.m_LeaderboardsFilter);
		}
		m_LeaderboardSlots.DestroyAll();
		m_Root.SetActive(value: false);
		m_NoLeaderboardsText.gameObject.SetActive(value: false);
		m_LeaderboardDividers.SetActive(value: true);
		m_Legend.SetActive(value: false);
		m_LowStressLegend.SetActive(value: false);
		m_ScoresLoading.SetActive(value: false);
		m_HistoLoading.SetActive(value: false);
		m_CurrentFilterState.Reset();
		m_PendingFilterState.Reset();
		m_LeaderboardsPanelAnimate.ForceState(Profiles.m_ActiveProfile.m_HideOtherLeaderboards ? PanelAnimateState.OFF : PanelAnimateState.ON);
		if (!GameLeaderboards.CurrentLevelAllowsLeaderboards() || GameManager.IsSteamOffline())
		{
			m_WaitAnimation.SetActive(value: false);
			string failText = GetFailText();
			DisableLeaderboard(failText);
		}
		else
		{
			m_WaitAnimation.SetActive(value: true);
		}
	}

	public void Close()
	{
		m_WaitAnimation.SetActive(value: false);
		if (m_LeaderboardSlots != null)
		{
			m_LeaderboardSlots.DestroyAll();
		}
	}

	public void ForceRefresh()
	{
		m_ForceRefresh = true;
	}

	public void OnRefresh()
	{
		m_NoHistogramText.gameObject.SetActive(value: false);
		m_NotEnoughScoresText.gameObject.SetActive(value: false);
		string levelId = Game.GetLevelId();
		if (LeaderboardBuckets.m_Levels.ContainsKey(levelId))
		{
			EnableAndPopulateHistogram(levelId);
		}
		else
		{
			m_HistogramPanel.EnableHistogramUI(enable: false);
			if (WeeklyChallenges.IsAWeeklyChallenge(levelId))
			{
				m_HistoLoading.gameObject.SetActive(value: true);
				LeaderboardBuckets.DownloadAsync(levelId + ".bin", HistogramDownloadComplete);
			}
			else
			{
				m_NoHistogramText.gameObject.SetActive(value: true);
			}
		}
		m_PendingFilterState.Set(m_LeaderboardsView, m_LeaderboardsFilter, levelId, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE);
		if (GameManager.IsSteamOffline())
		{
			OnLeaderboardDownloaded(null, null);
			return;
		}
		m_NoLeaderboardsText.gameObject.SetActive(value: false);
		m_LeaderboardDividers.SetActive(value: true);
		m_Content.gameObject.SetActive(value: false);
		if (!GameManager.IsSteamOffline())
		{
			int num = (m_WaitAnimation.activeInHierarchy ? DOWNLOAD_DELAY_MS : 0);
			m_LeaderboardSlots.DestroyAll();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content.GetComponent<RectTransform>());
			if (m_PendingFilterState.m_LeaderboardsView == LeaderboardsView.TOP_SCORES)
			{
				LeaderboardFilterState leaderboardFilterState = new LeaderboardFilterState();
				leaderboardFilterState.CopyFrom(m_PendingFilterState);
				leaderboardFilterState.m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
				SteamLeaderboardsDownload.DownloadLeaderboard(levelId, num, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE, GameLeaderboards.NUM_AROUND_SCORES_ABOVE_DISPLAYED_LEVEL_COMPLETE, GameLeaderboards.NUM_AROUND_SCORES_BELOW_DISPLAYED_LEVEL_COMPLETE, leaderboardFilterState, null);
				SteamLeaderboardsDownload.DownloadLeaderboard(levelId, num + 200, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE, GameLeaderboards.NUM_AROUND_SCORES_ABOVE_DISPLAYED_LEVEL_COMPLETE, GameLeaderboards.NUM_AROUND_SCORES_BELOW_DISPLAYED_LEVEL_COMPLETE, m_PendingFilterState, OnLeaderboardDownloaded);
			}
			else
			{
				SteamLeaderboardsDownload.DownloadLeaderboard(levelId, num, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE, GameLeaderboards.NUM_AROUND_SCORES_ABOVE_DISPLAYED_LEVEL_COMPLETE, GameLeaderboards.NUM_AROUND_SCORES_BELOW_DISPLAYED_LEVEL_COMPLETE, m_PendingFilterState, OnLeaderboardDownloaded);
			}
		}
	}

	public void OnLeaderboardDownloaded(GameLeaderboard leaderboard, GameLeaderboardEntry[] scores)
	{
		m_CurrentFilterState.CopyFrom(m_PendingFilterState);
		m_WaitAnimation.SetActive(value: false);
		if (scores == null)
		{
			DisableLeaderboard(GetFailText());
			int num = ((m_CurrentFilterState.m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS) ? GameLeaderboards.ConvertStressToScore(StressSamples.m_MaxStressNormalized) : GameStateSim.m_BudgetUsed);
			if (LeaderboardBuckets.m_Levels.ContainsKey(m_CurrentFilterState.m_LevelId) && num > 0)
			{
				m_HistogramPanel.ShowPlayerScore(m_CurrentFilterState.m_LevelId, num, m_CurrentFilterState);
			}
			return;
		}
		GameLeaderboardEntry[] array = new GameLeaderboardEntry[Mathf.Min(GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE, scores.Length)];
		Array.Copy(scores, 0, array, 0, Mathf.Min(GameLeaderboards.NUM_TOP_SCORES_DISPLAYED_LEVEL_COMPLETE, scores.Length));
		PopulateLeaderboard(leaderboard, array, m_CurrentFilterState);
		int num2 = m_LeaderboardSlots.GetPlayerScore();
		if (num2 == 0 && m_CurrentFilterState.m_LeaderboardsView == LeaderboardsView.TOP_SCORES)
		{
			num2 = GameLeaderboards.TryGetBestPlayerScore(m_CurrentFilterState);
		}
		if (LeaderboardBuckets.m_Levels.ContainsKey(m_CurrentFilterState.m_LevelId) && num2 > 0)
		{
			m_HistogramPanel.ShowPlayerScore(m_CurrentFilterState.m_LevelId, num2, m_CurrentFilterState);
		}
	}

	private void PopulateLeaderboard(GameLeaderboard leaderboard, GameLeaderboardEntry[] scores, LeaderboardFilterState filter)
	{
		m_LeaderboardSlots.DestroyAll();
		m_Root.SetActive(value: true);
		if (filter.m_LeaderboardsView == LeaderboardsView.FRIENDS)
		{
			m_LeaderboardSlots.AddFriendSlots(scores, filter);
		}
		else
		{
			m_LeaderboardSlots.AddSlots(scores, filter);
		}
		m_LeaderboardSlots.SetDefaultHighlightColors();
		m_LeaderboardSlots.HighlightPlayerSlot(filter);
		FinalizeLeaderboardDisplay();
		int num = 10;
		if (filter.m_LeaderboardsView == LeaderboardsView.AROUND_YOU)
		{
			int max = scores.Length * 20;
			int playerScoreIndex = m_LeaderboardSlots.GetPlayerScoreIndex();
			m_ContentAnchorY = Mathf.Clamp(Mathf.RoundToInt((playerScoreIndex - num) * 20 + 100), 0, max);
			m_ContentRectTransform.anchoredPosition = new Vector2(0f, m_ContentAnchorY);
			m_NumRebuilds = 0;
		}
		else
		{
			m_ContentRectTransform.anchoredPosition = new Vector2(0f, 0f);
		}
		if (m_ScrollbarParent != null)
		{
			m_ScrollbarParent.gameObject.SetActive(m_LeaderboardSlots.m_Slots.Count > num);
		}
	}

	private void FinalizeLeaderboardDisplay()
	{
		if (m_LeaderboardSlots.m_Slots.Count == 0)
		{
			m_NoLeaderboardsText.text = GetFailText();
			m_NoLeaderboardsText.gameObject.SetActive(value: true);
			m_LeaderboardDividers.SetActive(value: false);
			m_Content.gameObject.SetActive(value: false);
		}
		else
		{
			m_NoLeaderboardsText.gameObject.SetActive(value: false);
			m_LeaderboardDividers.SetActive(value: true);
			m_Content.gameObject.SetActive(value: true);
		}
		RefreshLegend();
		m_WaitAnimation.SetActive(value: false);
	}

	private void RefreshLegend()
	{
		if (m_LeaderboardSlots != null)
		{
			m_Legend.SetActive(m_LeaderboardsFilter == LeaderboardsFilter.ALL);
			m_LowStressLegend.SetActive(m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS);
		}
	}

	private void DisableLeaderboard(string failText)
	{
		m_Root.SetActive(value: true);
		m_Content.gameObject.SetActive(value: false);
		m_Legend.SetActive(value: false);
		m_LowStressLegend.SetActive(value: false);
		m_NoLeaderboardsText.gameObject.SetActive(value: true);
		m_LeaderboardDividers.SetActive(value: false);
		m_NoLeaderboardsText.text = failText;
	}

	private void MaybeRefreshLeaderboard()
	{
		if (ShouldRefreshLeaderboard() && GameLeaderboards.CurrentLevelAllowsLeaderboards() && !IsDownloadingLeaderboard())
		{
			OnRefresh();
			m_ForceRefresh = false;
		}
		if (m_ForceRefresh && !IsDownloadingLeaderboard())
		{
			m_WaitAnimation.gameObject.SetActive(value: false);
		}
	}

	private bool IsDownloadingLeaderboard()
	{
		return m_CurrentFilterState.IsDifferentThan(m_PendingFilterState);
	}

	private bool ShouldRefreshLeaderboard()
	{
		if (m_ForceRefresh)
		{
			return true;
		}
		if (m_LeaderboardsView != m_CurrentFilterState.m_LeaderboardsView)
		{
			return true;
		}
		if (m_LeaderboardsFilter != m_CurrentFilterState.m_LeaderboardsFilter)
		{
			return true;
		}
		return false;
	}

	private string GetFailText()
	{
		string result = Localize.Get("UI_NO_LEADERBOARD_ENTRIES");
		if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && !Workshop.m_LastPlayedWorkshopItem.IsFeatured())
		{
			result = Localize.Get("UI_LEADERBOARD_NOT_IN_WORKSHOP");
		}
		if (!GameManager.IsSteamOffline())
		{
			return result;
		}
		return GameManager.GetSteamOfflineMessage();
	}

	private void OnTopScores()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectView(LeaderboardsView.TOP_SCORES);
		Profiles.m_ActiveProfile.m_LeaderboardsView = LeaderboardsView.TOP_SCORES;
		Profiles.SaveActiveProfile();
	}

	private void OnAroundYouScores()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectView(LeaderboardsView.AROUND_YOU);
		Profiles.m_ActiveProfile.m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
		Profiles.SaveActiveProfile();
	}

	private void OnFriendsScores()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectView(LeaderboardsView.FRIENDS);
		Profiles.m_ActiveProfile.m_LeaderboardsView = LeaderboardsView.FRIENDS;
		Profiles.SaveActiveProfile();
	}

	private void OnShowAll()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectFilter(LeaderboardsFilter.ALL);
		Profiles.m_ActiveProfile.m_LeaderboardsFilter = LeaderboardsFilter.ALL;
		Profiles.SaveActiveProfile();
	}

	private void OnShowUnbreaking()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectFilter(LeaderboardsFilter.UNBREAKING);
		Profiles.m_ActiveProfile.m_LeaderboardsFilter = LeaderboardsFilter.UNBREAKING;
		Profiles.SaveActiveProfile();
	}

	private void OnShowLowestStress()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectFilter(LeaderboardsFilter.LOWEST_STRESS);
		Profiles.m_ActiveProfile.m_LeaderboardsFilter = LeaderboardsFilter.LOWEST_STRESS;
		Profiles.SaveActiveProfile();
	}

	private void SelectView(LeaderboardsView view)
	{
		m_LeaderboardsView = view;
		m_TopScoresButton.SetState(view == LeaderboardsView.TOP_SCORES);
		m_AroundYouScoresButton.SetState(view == LeaderboardsView.AROUND_YOU);
		m_FriendsScoresButton.SetState(view == LeaderboardsView.FRIENDS);
	}

	private void SelectFilter(LeaderboardsFilter filter)
	{
		m_LeaderboardsFilter = filter;
		m_ShowAllButton.SetState(filter == LeaderboardsFilter.ALL);
		m_ShowUnbreakingButton.SetState(filter == LeaderboardsFilter.UNBREAKING);
		m_ShowLowestStressButton.SetState(filter == LeaderboardsFilter.LOWEST_STRESS);
		m_OverallTab.m_BackgroundRectTransform.offsetMin = ((filter == LeaderboardsFilter.ALL) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_UnbreakingTab.m_BackgroundRectTransform.offsetMin = ((filter == LeaderboardsFilter.UNBREAKING) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_LowestStressTab.m_BackgroundRectTransform.offsetMin = ((filter == LeaderboardsFilter.LOWEST_STRESS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		RefreshLegend();
	}

	private void OnLeaderboardsCollapse()
	{
		if (m_LeaderboardsPanelAnimate.GetState() == PanelAnimateState.ON || m_LeaderboardsPanelAnimate.GetState() == PanelAnimateState.ANIMATING_ON)
		{
			m_LeaderboardsPanelAnimate.Play(on: false, LeaderboardsPanelCollapsed);
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
		else
		{
			m_LeaderboardsPanelAnimate.Play(on: true, LeaderboardsPanelExpanded);
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	private void LeaderboardsPanelExpanded()
	{
		m_LeaderboardsCollapseIcon.transform.localScale = new Vector3(-1f, 1f, 1f);
		Profiles.m_ActiveProfile.m_HideOtherLeaderboards = false;
		Profiles.SaveActiveProfile();
	}

	private void LeaderboardsPanelCollapsed()
	{
		m_LeaderboardsCollapseIcon.transform.localScale = new Vector3(1f, 1f, 1f);
		Profiles.m_ActiveProfile.m_HideOtherLeaderboards = true;
		Profiles.SaveActiveProfile();
	}

	private void EnableAndPopulateHistogram(string levelID)
	{
		if (m_HistogramPanel.GetNumScores(levelID, m_LeaderboardsFilter) < GameLeaderboards.MIN_SCORES_FOR_HISTOGRAM)
		{
			m_HistogramPanel.EnableHistogramUI(enable: false);
			m_NotEnoughScoresText.gameObject.SetActive(value: true);
		}
		else
		{
			m_HistogramPanel.EnableHistogramUI(enable: true);
			m_HistogramPanel.Populate(levelID, m_LeaderboardsFilter, GameStateSim.m_Budget);
		}
	}

	private void HistogramDownloadComplete(string filename, byte[] data)
	{
		m_HistoLoading.gameObject.SetActive(value: false);
		if (data == null)
		{
			string fullPath = Path.Combine(Application.persistentDataPath, LeaderboardBuckets.BUCKETS_DIRECTORYNAME, filename);
			if (Utils.FileExists(fullPath))
			{
				byte[] array = Utils.ReadAllBytes(fullPath);
				if (array != null && array.Length != 0)
				{
					LeaderboardBuckets.PopulateLeaderboardBuckets(array);
					EnableAndPopulateHistogram(m_CurrentFilterState.m_LevelId);
				}
			}
		}
		else
		{
			string text = Path.Combine(Application.persistentDataPath, LeaderboardBuckets.BUCKETS_DIRECTORYNAME);
			Utils.CreateDirectory(text);
			string text2 = Path.Combine(text, filename);
			try
			{
				File.WriteAllBytes(text2, data);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Failed to write: " + text2 + " due to " + ex.Message);
			}
			LeaderboardBuckets.PopulateLeaderboardBuckets(data);
			EnableAndPopulateHistogram(m_CurrentFilterState.m_LevelId);
		}
		m_NoHistogramText.gameObject.SetActive(!LeaderboardBuckets.m_Levels.ContainsKey(m_CurrentFilterState.m_LevelId));
	}

	private int GetLevelBudget(string levelId)
	{
		return SandboxLayout.Load(Campaign.GetLayoutFullPathFromId(levelId))?.m_Budget.m_CashBudget ?? 0;
	}

	private void UpdateScoresLoadingDots()
	{
		m_ScoresLoading.gameObject.SetActive(IsDownloadingLeaderboard() && !m_WaitAnimation.gameObject.activeInHierarchy);
		m_HideDuringLoad.gameObject.SetActive(!m_ScoresLoading.gameObject.activeInHierarchy);
	}
}
