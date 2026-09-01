using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Leaderboard : MonoBehaviour
{
	[Header("Loading")]
	public GameObject m_LoadingOverlay;

	public GameObject m_HideDuringLoad;

	public GameObject m_LeaderboardDividers;

	public TextMeshProUGUI m_FailedToLoadText;

	[Header("Tabs")]
	public SandboxTab m_OverallTab;

	public SandboxTab m_UnbreakingTab;

	public SandboxTab m_LowestStressTab;

	[Header("Colors")]
	public Color m_TabActiveColor;

	public Color m_TabInActiveColor;

	[Header("Leaderboard Buttons")]
	public TwoStateButton m_TopScoresButton;

	public TwoStateButton m_AroundYouScoresButton;

	public TwoStateButton m_FriendsScoresButton;

	public TwoStateButton m_ShowAllButton;

	public TwoStateButton m_ShowUnbreakingButton;

	public TwoStateButton m_ShowLowestStressButton;

	[Header("Slots")]
	public GameObject m_ScrollbarParent;

	public GameObject m_LeaderboardSlotPrefab;

	public GameObject m_Content;

	public GameObject m_Legend;

	public GameObject m_LowestStressLegend;

	[Header("Histogram")]
	public Panel_Histogram m_HistogramPanel;

	public TextMeshProUGUI m_NoHistogramText;

	public TextMeshProUGUI m_NotEnoughScoresText;

	public GameObject m_HistogramLoadingAnim;

	[NonSerialized]
	public LeaderboardFilterState m_CurrentFilterState = new LeaderboardFilterState();

	[NonSerialized]
	public LeaderboardFilterState m_PendingFilterState = new LeaderboardFilterState();

	private LeaderboardSlots m_LeaderboardSlots;

	private LeaderboardsView m_LeaderboardsView;

	private LeaderboardsFilter m_LeaderboardsFilter;

	private RectTransform m_ContentRectTransform;

	private int m_ContentAnchorY;

	private bool m_LoadingInProgress;

	private int m_LevelBudget;

	private string m_LevelID;

	private void Awake()
	{
		m_ContentRectTransform = m_Content.GetComponent<RectTransform>();
	}

	private void Start()
	{
		m_TopScoresButton.m_Button.onClick.AddListener(OnTopScores);
		m_AroundYouScoresButton.m_Button.onClick.AddListener(OnAroundYouScores);
		m_FriendsScoresButton.m_Button.onClick.AddListener(OnFriendsScores);
		m_ShowAllButton.m_Button.onClick.AddListener(OnShowAll);
		m_ShowUnbreakingButton.m_Button.onClick.AddListener(OnShowUnbreaking);
		m_ShowLowestStressButton.m_Button.onClick.AddListener(OnShowLowestStress);
	}

	private void Update()
	{
		if (m_HistogramLoadingAnim.gameObject.activeInHierarchy && LeaderboardBuckets.m_Levels.ContainsKey(m_LevelID))
		{
			m_HistogramLoadingAnim.gameObject.SetActive(value: false);
			EnableAndPopulateHistogram(m_LevelID);
		}
	}

	private void LateUpdate()
	{
		if (m_ContentAnchorY != 0)
		{
			m_ContentRectTransform.anchoredPosition = new Vector3(0f, m_ContentAnchorY);
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_Content.transform.parent.GetComponent<RectTransform>());
			m_ContentAnchorY = 0;
		}
	}

	public void Init()
	{
		base.gameObject.SetActive(value: true);
		m_Legend.SetActive(value: false);
		m_LowestStressLegend.SetActive(value: false);
		m_LoadingOverlay.SetActive(value: false);
		m_LeaderboardDividers.SetActive(value: true);
		m_FailedToLoadText.gameObject.SetActive(value: false);
		m_HideDuringLoad.SetActive(value: false);
		m_NoHistogramText.gameObject.SetActive(value: false);
		m_NotEnoughScoresText.gameObject.SetActive(value: false);
		m_HistogramLoadingAnim.SetActive(value: false);
		InitLeaderboardSlots();
	}

	public void OnRefresh(string levelId)
	{
		if (string.IsNullOrEmpty(levelId))
		{
			return;
		}
		if (m_LevelID != levelId)
		{
			m_LevelID = levelId;
			m_LevelBudget = GetLevelBudget(m_LevelID);
		}
		if (GameManager.IsSteamOffline())
		{
			OnLeaderboardDownloaded(null, null);
			ShowHistogram(levelId);
			return;
		}
		StartLoading();
		DestroyAllSlots();
		ShowHistogram(levelId);
		m_PendingFilterState.Set(m_LeaderboardsView, m_LeaderboardsFilter, levelId, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED);
		m_LoadingInProgress = true;
		m_LoadingOverlay.gameObject.SetActive(value: true);
		if (m_PendingFilterState.m_LeaderboardsView == LeaderboardsView.TOP_SCORES)
		{
			LeaderboardFilterState leaderboardFilterState = new LeaderboardFilterState();
			leaderboardFilterState.CopyFrom(m_PendingFilterState);
			leaderboardFilterState.m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
			SteamLeaderboardsDownload.DownloadLeaderboard(levelId, 0, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED, GameLeaderboards.NUM_AROUND_SCORES_ABOVE_DISPLAYED, GameLeaderboards.NUM_AROUND_SCORES_BELOW_DISPLAYED, leaderboardFilterState, null);
			SteamLeaderboardsDownload.DownloadLeaderboard(levelId, 200, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED, GameLeaderboards.NUM_AROUND_SCORES_ABOVE_DISPLAYED, GameLeaderboards.NUM_AROUND_SCORES_BELOW_DISPLAYED, m_PendingFilterState, OnLeaderboardDownloaded);
		}
		else
		{
			SteamLeaderboardsDownload.DownloadLeaderboard(levelId, 0, GameLeaderboards.NUM_TOP_SCORES_DISPLAYED, GameLeaderboards.NUM_AROUND_SCORES_ABOVE_DISPLAYED, GameLeaderboards.NUM_AROUND_SCORES_BELOW_DISPLAYED, m_PendingFilterState, OnLeaderboardDownloaded);
		}
	}

	public void StartLoading()
	{
		m_FailedToLoadText.gameObject.SetActive(value: false);
		m_HideDuringLoad.SetActive(value: false);
	}

	public void EndLoading()
	{
		m_HideDuringLoad.SetActive(value: true);
	}

	public void DestroyAllSlots()
	{
		if (m_LeaderboardSlots != null)
		{
			m_LeaderboardSlots.DestroyAll();
		}
	}

	public void PopulateLeaderboard(GameLeaderboard leaderboard, GameLeaderboardEntry[] scores, LeaderboardFilterState filter)
	{
		DestroyAllSlots();
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
		if (m_LeaderboardSlots.m_Slots.Count == 0)
		{
			m_FailedToLoadText.text = (GameManager.IsSteamOffline() ? GameManager.GetSteamOfflineMessage() : Localize.Get("UI_NO_LEADERBOARD_ENTRIES"));
			m_FailedToLoadText.gameObject.SetActive(value: true);
			m_LeaderboardDividers.SetActive(value: false);
		}
		else
		{
			m_FailedToLoadText.gameObject.SetActive(value: false);
			m_LeaderboardDividers.SetActive(value: true);
		}
		int num = (GameUI.m_Instance.m_WeeklyChallenges.gameObject.activeInHierarchy ? 21 : 12);
		if (filter.m_LeaderboardsView == LeaderboardsView.AROUND_YOU)
		{
			int max = scores.Length * 20;
			int playerScoreIndex = m_LeaderboardSlots.GetPlayerScoreIndex();
			m_ContentAnchorY = Mathf.Clamp(Mathf.RoundToInt((playerScoreIndex - num) * 20 + 100), 0, max);
			m_ContentRectTransform.anchoredPosition = new Vector2(0f, m_ContentAnchorY);
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

	public void OnLeaderboardDownloaded(GameLeaderboard leaderboard, GameLeaderboardEntry[] scores)
	{
		m_CurrentFilterState.CopyFrom(m_PendingFilterState);
		if (leaderboard == null || scores == null)
		{
			DestroyAllSlots();
			m_FailedToLoadText.text = (GameManager.IsSteamOffline() ? GameManager.GetSteamOfflineMessage() : Localize.Get("UI_NO_LEADERBOARD_ENTRIES"));
			m_FailedToLoadText.gameObject.SetActive(value: true);
			m_LeaderboardDividers.SetActive(value: false);
		}
		else
		{
			PopulateLeaderboard(leaderboard, scores, m_CurrentFilterState);
			m_LeaderboardDividers.SetActive(value: true);
			int num = m_LeaderboardSlots.GetPlayerScore();
			if (num == 0 && m_CurrentFilterState.m_LeaderboardsView == LeaderboardsView.TOP_SCORES)
			{
				num = GameLeaderboards.TryGetBestPlayerScore(m_CurrentFilterState);
			}
			if (LeaderboardBuckets.m_Levels.ContainsKey(m_CurrentFilterState.m_LevelId) && num > 0)
			{
				m_HistogramPanel.ShowPlayerScore(m_CurrentFilterState.m_LevelId, num, m_CurrentFilterState);
			}
		}
		FinalizeLeaderboardDisplay();
		m_LoadingInProgress = false;
	}

	public bool DownloadInProgress()
	{
		return m_LoadingInProgress;
	}

	public string CurrentLevelId()
	{
		return m_CurrentFilterState.m_LevelId;
	}

	public bool FiltersChanged()
	{
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

	private void InitLeaderboardSlots()
	{
		if (m_LeaderboardSlots == null)
		{
			m_LeaderboardSlots = new LeaderboardSlots(m_LeaderboardSlotPrefab, m_Content.transform);
			SelectView(Profiles.m_ActiveProfile.m_LeaderboardsViewExtended);
			SelectFilter(Profiles.m_ActiveProfile.m_LeaderboardsFilterExtended);
			m_CurrentFilterState.Reset();
			m_PendingFilterState.Reset();
		}
	}

	private void FinalizeLeaderboardDisplay()
	{
		m_LoadingOverlay.SetActive(value: false);
		m_HideDuringLoad.SetActive(value: true);
	}

	private void RefreshLegend()
	{
		if (m_LeaderboardSlots != null)
		{
			m_Legend.SetActive(m_LeaderboardsFilter == LeaderboardsFilter.ALL);
			m_LowestStressLegend.SetActive(m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS);
		}
	}

	private void OnTopScores()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectView(LeaderboardsView.TOP_SCORES);
		Profiles.m_ActiveProfile.m_LeaderboardsViewExtended = LeaderboardsView.TOP_SCORES;
		Profiles.SaveActiveProfile();
	}

	private void OnAroundYouScores()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectView(LeaderboardsView.AROUND_YOU);
		Profiles.m_ActiveProfile.m_LeaderboardsViewExtended = LeaderboardsView.AROUND_YOU;
		Profiles.SaveActiveProfile();
	}

	private void OnFriendsScores()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectView(LeaderboardsView.FRIENDS);
		Profiles.m_ActiveProfile.m_LeaderboardsViewExtended = LeaderboardsView.FRIENDS;
		Profiles.SaveActiveProfile();
	}

	private void OnShowAll()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectFilter(LeaderboardsFilter.ALL);
		Profiles.m_ActiveProfile.m_LeaderboardsFilterExtended = LeaderboardsFilter.ALL;
		Profiles.SaveActiveProfile();
	}

	private void OnShowUnbreaking()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectFilter(LeaderboardsFilter.UNBREAKING);
		Profiles.m_ActiveProfile.m_LeaderboardsFilterExtended = LeaderboardsFilter.UNBREAKING;
		Profiles.SaveActiveProfile();
	}

	private void OnShowLowestStress()
	{
		InterfaceAudio.Play("ui_menu_select");
		SelectFilter(LeaderboardsFilter.LOWEST_STRESS);
		Profiles.m_ActiveProfile.m_LeaderboardsFilterExtended = LeaderboardsFilter.LOWEST_STRESS;
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
		m_OverallTab.m_Background.color = ((filter == LeaderboardsFilter.ALL) ? m_TabActiveColor : m_TabInActiveColor);
		m_UnbreakingTab.m_Background.color = ((filter == LeaderboardsFilter.UNBREAKING) ? m_TabActiveColor : m_TabInActiveColor);
		m_LowestStressTab.m_Background.color = ((filter == LeaderboardsFilter.LOWEST_STRESS) ? m_TabActiveColor : m_TabInActiveColor);
		m_OverallTab.m_BackgroundRectTransform.offsetMin = ((filter == LeaderboardsFilter.ALL) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_UnbreakingTab.m_BackgroundRectTransform.offsetMin = ((filter == LeaderboardsFilter.UNBREAKING) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		m_LowestStressTab.m_BackgroundRectTransform.offsetMin = ((filter == LeaderboardsFilter.LOWEST_STRESS) ? new Vector2(2f, 0f) : new Vector2(2f, 2f));
		RefreshLegend();
	}

	public void ShowHistogram(string levelId)
	{
		m_NoHistogramText.gameObject.SetActive(value: false);
		m_NotEnoughScoresText.gameObject.SetActive(value: false);
		m_HistogramLoadingAnim.gameObject.SetActive(value: false);
		if (LeaderboardBuckets.m_Levels.ContainsKey(levelId))
		{
			EnableAndPopulateHistogram(levelId);
			return;
		}
		m_HistogramPanel.EnableHistogramUI(enable: false);
		if (WeeklyChallenges.IsAWeeklyChallenge(levelId))
		{
			m_HistogramLoadingAnim.gameObject.SetActive(value: true);
			LeaderboardBuckets.DownloadAsync(levelId + ".bin", delegate(string x, byte[] y)
			{
				HistogramDownloadComplete(x, y, levelId);
			});
		}
		else if (LeaderboardBuckets.m_Downloading)
		{
			m_HistogramLoadingAnim.gameObject.SetActive(value: true);
		}
		else
		{
			m_NoHistogramText.gameObject.SetActive(value: true);
		}
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
			m_NotEnoughScoresText.gameObject.SetActive(value: false);
			m_HistogramPanel.Populate(levelID, m_LeaderboardsFilter, m_LevelBudget);
		}
	}

	private void HistogramDownloadComplete(string filename, byte[] data, string levelID)
	{
		m_HistogramLoadingAnim.gameObject.SetActive(value: false);
		m_NoHistogramText.gameObject.SetActive(value: true);
		if (data == null)
		{
			string fullPath = Path.Combine(Application.persistentDataPath, LeaderboardBuckets.BUCKETS_DIRECTORYNAME, filename);
			if (Utils.FileExists(fullPath))
			{
				byte[] array = Utils.ReadAllBytes(fullPath);
				if (array != null && array.Length != 0)
				{
					LeaderboardBuckets.PopulateLeaderboardBuckets(array);
					EnableAndPopulateHistogram(levelID);
					m_NoHistogramText.gameObject.SetActive(value: false);
				}
			}
			return;
		}
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
		EnableAndPopulateHistogram(levelID);
		m_NoHistogramText.gameObject.SetActive(value: false);
	}

	private int GetLevelBudget(string levelId)
	{
		string empty = string.Empty;
		WorkshopItem weeklyChallengeByItemId = WeeklyChallenges.GetWeeklyChallengeByItemId(levelId);
		if (weeklyChallengeByItemId != null)
		{
			return WeeklyChallenges.GetBudgetFromEncodedDescription(weeklyChallengeByItemId.GetDescription());
		}
		empty = Campaign.GetLayoutFullPathFromId(levelId);
		if (string.IsNullOrEmpty(empty))
		{
			return 0;
		}
		return SandboxLayout.Load(empty)?.m_Budget.m_CashBudget ?? 0;
	}
}
