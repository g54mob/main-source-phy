using System;
using System.Collections.Generic;
using Steamworks;
using Steamworks.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel_MyRankings : MonoBehaviour
{
	[Header("Tabs")]
	public SandboxTab m_OverallTab;

	public SandboxTab m_UnbreakingTab;

	public SandboxTab m_LowestStressTab;

	[Header("Colors")]
	public UnityEngine.Color m_TabActiveColor;

	public UnityEngine.Color m_TabInActiveColor;

	public UnityEngine.Color m_SortActiveColor;

	public UnityEngine.Color m_SortInActiveColor;

	[Header("Buttons")]
	public Button m_CancelButton;

	public Button m_RefreshButton;

	public TextMeshProUGUI m_RefreshText;

	public GameObject m_RefreshBusyAnimation;

	[Header("Sort Bar")]
	public Button m_RankSortButton;

	public Button m_ScoreSortButton;

	public Button m_PercentileSortButton;

	public TextMeshProUGUI m_RankText;

	public TextMeshProUGUI m_ScoreText;

	public TextMeshProUGUI m_PercentileText;

	public UnityEngine.UI.Image m_RankArrow;

	public UnityEngine.UI.Image m_ScoreArrow;

	public UnityEngine.UI.Image m_PercentileArrow;

	[Header("Filters")]
	public TMP_Dropdown m_WorldNameDropdown;

	public TwoStateButton m_ShowAllButton;

	public TwoStateButton m_ShowUnbreakingButton;

	public TwoStateButton m_ShowLowestStressButton;

	[Header("Content")]
	public Transform m_RowsParent;

	[Header("Prefabs")]
	public GameObject m_MyRankingsRowPrefab;

	[Header("Sum")]
	public RectTransform m_SumRectTransform;

	public TextMeshProUGUI m_SumText;

	private LeaderboardsFilter m_LeaderboardsFilter;

	private string m_WorldFilter;

	private MyRankingsSortType m_SortType;

	private static Dictionary<string, MyRankingsRow> m_SlotDict = new Dictionary<string, MyRankingsRow>();

	private static int m_RefreshCounter;

	private void Start()
	{
		m_CancelButton.onClick.AddListener(Close);
		m_RefreshButton.onClick.AddListener(OnForceRefresh);
		m_ShowAllButton.m_Button.onClick.AddListener(OnShowAll);
		m_ShowUnbreakingButton.m_Button.onClick.AddListener(OnShowUnbreaking);
		m_ShowLowestStressButton.m_Button.onClick.AddListener(OnShowLowestStress);
		m_RankSortButton.onClick.AddListener(OnRankSort);
		m_ScoreSortButton.onClick.AddListener(OnScoreSort);
		m_PercentileSortButton.onClick.AddListener(OnPercentileSort);
		m_WorldNameDropdown.onValueChanged.AddListener(delegate
		{
			OnWorldNameChanged();
		});
		m_WorldNameDropdown.alphaFadeSpeed = 0f;
	}

	private void OnEnable()
	{
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
	}

	private void OnDisable()
	{
		ActivePanels.Remove(base.gameObject);
		GameUI.m_Instance.m_Campaign.m_Root.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		ProcessInput();
		m_RefreshText.gameObject.SetActive(m_RefreshCounter == 0);
		m_RefreshBusyAnimation.SetActive(m_RefreshCounter > 0);
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
	}

	public void Open(string worldID)
	{
		base.gameObject.SetActive(value: true);
		m_WorldFilter = worldID;
		if (m_SlotDict.Count == 0)
		{
			SelectFilter(LeaderboardsFilter.ALL);
			CreateRows();
			CampaignWorldDropdown.Populate(m_WorldNameDropdown, includeAll: true);
		}
		CampaignWorldDropdown.Select(m_WorldNameDropdown, worldID);
		OnRefreshSlots(m_LeaderboardsFilter, forceRefresh: false);
	}

	private void Close()
	{
		InterfaceAudio.Play("ui_menu_cancel");
		base.gameObject.SetActive(value: false);
	}

	private void UpdateSlot(string levelId, int score, int rank, int rankPercentile)
	{
		if (m_SlotDict.ContainsKey(levelId))
		{
			m_SlotDict[levelId].SetScoreAndRank(score, rank, rankPercentile, m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS);
		}
	}

	private void FilterSlotsByWorld(string worldID)
	{
		foreach (KeyValuePair<string, MyRankingsRow> item in m_SlotDict)
		{
			item.Value.gameObject.SetActive(value: false);
		}
		int num = 0;
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (worldID != CampaignWorlds.WORLD_ID_ALL && campaignWorld.m_Id != worldID)
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!campaignLevel.IsTutorial() && m_SlotDict.ContainsKey(campaignLevel.m_Id))
				{
					m_SlotDict[campaignLevel.m_Id].gameObject.SetActive(value: true);
					m_SlotDict[campaignLevel.m_Id].MakeDynamicDataBlank();
					num++;
				}
			}
		}
		float y = -30f * (float)Mathf.Min(num, 12);
		m_SumRectTransform.anchoredPosition = new Vector2(m_SumRectTransform.anchoredPosition.x, y);
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				Close();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_RIGHT))
			{
				CycleToNextLeaderboardType();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.SHOULDER_LEFT))
			{
				CycleToPrevLeaderboardType();
			}
		}
	}

	private void CycleToNextLeaderboardType()
	{
		if (m_ShowAllButton.IsOn())
		{
			ExecuteEvents.Execute(m_ShowUnbreakingButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ShowUnbreakingButton.IsOn())
		{
			ExecuteEvents.Execute(m_ShowLowestStressButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ShowLowestStressButton.IsOn())
		{
			ExecuteEvents.Execute(m_ShowAllButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void CycleToPrevLeaderboardType()
	{
		if (m_ShowAllButton.IsOn())
		{
			ExecuteEvents.Execute(m_ShowLowestStressButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ShowLowestStressButton.IsOn())
		{
			ExecuteEvents.Execute(m_ShowUnbreakingButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
		else if (m_ShowUnbreakingButton.IsOn())
		{
			ExecuteEvents.Execute(m_ShowAllButton.m_Button.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void OnForceRefresh()
	{
		if (m_RefreshBusyAnimation.activeInHierarchy)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			OnRefreshSlots(m_LeaderboardsFilter, forceRefresh: true);
		}
	}

	private void OnShowAll()
	{
		InterfaceAudio.Play("ui_menu_select");
		if (m_LeaderboardsFilter != LeaderboardsFilter.ALL)
		{
			SelectFilter(LeaderboardsFilter.ALL);
			OnRefreshSlots(m_LeaderboardsFilter, forceRefresh: false);
		}
	}

	private void OnShowUnbreaking()
	{
		InterfaceAudio.Play("ui_menu_select");
		if (m_LeaderboardsFilter != LeaderboardsFilter.UNBREAKING)
		{
			SelectFilter(LeaderboardsFilter.UNBREAKING);
			OnRefreshSlots(m_LeaderboardsFilter, forceRefresh: false);
		}
	}

	private void OnShowLowestStress()
	{
		InterfaceAudio.Play("ui_menu_select");
		if (m_LeaderboardsFilter != LeaderboardsFilter.LOWEST_STRESS)
		{
			SelectFilter(LeaderboardsFilter.LOWEST_STRESS);
			OnRefreshSlots(m_LeaderboardsFilter, forceRefresh: false);
		}
	}

	private void UpdateSorting(MyRankingsSortType sortType)
	{
		List<MyRankingsRow> rowsListFromDict = GetRowsListFromDict(m_SlotDict);
		SortRows(rowsListFromDict, sortType);
	}

	private void OnRankSort()
	{
		List<MyRankingsRow> rowsListFromDict = GetRowsListFromDict(m_SlotDict);
		if (m_SortType == MyRankingsSortType.NONE)
		{
			m_SortType = MyRankingsSortType.RANK_ASCENDING;
		}
		else if (m_SortType == MyRankingsSortType.RANK_ASCENDING)
		{
			m_SortType = MyRankingsSortType.RANK_DESCENDING;
		}
		else if (m_SortType == MyRankingsSortType.RANK_DESCENDING)
		{
			m_SortType = MyRankingsSortType.NONE;
		}
		else
		{
			m_SortType = MyRankingsSortType.RANK_ASCENDING;
		}
		SortRows(rowsListFromDict, m_SortType);
	}

	private void OnScoreSort()
	{
		List<MyRankingsRow> rowsListFromDict = GetRowsListFromDict(m_SlotDict);
		if (m_SortType == MyRankingsSortType.NONE)
		{
			m_SortType = MyRankingsSortType.SCORE_ASCENDING;
		}
		else if (m_SortType == MyRankingsSortType.SCORE_ASCENDING)
		{
			m_SortType = MyRankingsSortType.SCORE_DESCENDING;
		}
		else if (m_SortType == MyRankingsSortType.SCORE_DESCENDING)
		{
			m_SortType = MyRankingsSortType.NONE;
		}
		else
		{
			m_SortType = MyRankingsSortType.SCORE_ASCENDING;
		}
		SortRows(rowsListFromDict, m_SortType);
	}

	private void OnPercentileSort()
	{
		List<MyRankingsRow> rowsListFromDict = GetRowsListFromDict(m_SlotDict);
		if (m_SortType == MyRankingsSortType.NONE)
		{
			m_SortType = MyRankingsSortType.PERCENTILE_DESCENDING;
		}
		else if (m_SortType == MyRankingsSortType.PERCENTILE_ASCENDING)
		{
			m_SortType = MyRankingsSortType.NONE;
		}
		else if (m_SortType == MyRankingsSortType.PERCENTILE_DESCENDING)
		{
			m_SortType = MyRankingsSortType.PERCENTILE_ASCENDING;
		}
		else
		{
			m_SortType = MyRankingsSortType.PERCENTILE_DESCENDING;
		}
		SortRows(rowsListFromDict, m_SortType);
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
	}

	private async void OnRefreshSlots(LeaderboardsFilter filter, bool forceRefresh)
	{
		m_RefreshCounter++;
		FilterSlotsByWorld(m_WorldFilter);
		int rowIndex = 0;
		int sum = 0;
		UpdateSumText(sum);
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (m_WorldFilter != CampaignWorlds.WORLD_ID_ALL && campaignWorld.m_Id != m_WorldFilter)
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (filter != m_LeaderboardsFilter)
				{
					break;
				}
				if (campaignLevel.IsTutorial() || !m_SlotDict.ContainsKey(campaignLevel.m_Id))
				{
					continue;
				}
				string leaderboardKey = SteamLeaderboards.GetLeaderboardKey(campaignLevel.m_Id, m_LeaderboardsFilter);
				if (string.IsNullOrEmpty(leaderboardKey))
				{
					continue;
				}
				MyRankingsRow row = m_SlotDict[campaignLevel.m_Id];
				row.Highlight(rowIndex++, m_SortType);
				if (forceRefresh)
				{
					row.Reset();
				}
				if (!SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
				{
					Leaderboard? leaderboard = await SteamUserStats.FindLeaderboardAsync(leaderboardKey);
					if (leaderboard.HasValue)
					{
						try
						{
							SteamLeaderboards.m_Leaderboards.Add(leaderboardKey, leaderboard.Value);
						}
						catch (Exception arg)
						{
							Debug.LogWarning($"Caught exception '{arg}' adding leaderboard ID {leaderboardKey} to SteamLeaderboards.m_Leaderboards");
						}
					}
					else
					{
						UpdateSlot(row.m_LevelID, 0, 0, 0);
						UpdateSorting(m_SortType);
					}
				}
				Leaderboard leaderboard2;
				if (filter == LeaderboardsFilter.ALL && SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
				{
					if (row.m_Scores != null)
					{
						int rankPercentile = GameLeaderboards.ComputePlayerPercentile(new GameLeaderboard(SteamLeaderboards.m_Leaderboards[leaderboardKey]), row.m_Scores);
						UpdateSlot(row.m_LevelID, row.m_Scores[0].GetScore(), row.m_Scores[0].GetGlobalRank(), rankPercentile);
						sum += row.m_Scores[0].GetScore();
						UpdateSumText(sum);
						UpdateSorting(m_SortType);
					}
					else
					{
						leaderboard2 = SteamLeaderboards.m_Leaderboards[leaderboardKey];
						if (!row.m_ScoresRequested)
						{
							row.m_ScoresRequested = true;
							row.m_Scores = GameLeaderboards.CreateGameLeaderboardEntries(await leaderboard2.GetScoresAroundUserAsync(0, 0));
						}
						if (filter != m_LeaderboardsFilter)
						{
							break;
						}
						if (row.m_Scores == null || row.m_Scores.Length == 0)
						{
							UpdateSlot(row.m_LevelID, 0, 0, 0);
							UpdateSorting(m_SortType);
						}
						else
						{
							int rankPercentile2 = GameLeaderboards.ComputePlayerPercentile(new GameLeaderboard(leaderboard2), row.m_Scores);
							UpdateSlot(row.m_LevelID, row.m_Scores[0].GetScore(), row.m_Scores[0].GetGlobalRank(), rankPercentile2);
							sum += row.m_Scores[0].GetScore();
							UpdateSumText(sum);
							UpdateSorting(m_SortType);
						}
					}
				}
				if (filter == LeaderboardsFilter.UNBREAKING && SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
				{
					if (row.m_ScoresUnbreaking != null)
					{
						int rankPercentile3 = GameLeaderboards.ComputePlayerPercentile(new GameLeaderboard(SteamLeaderboards.m_Leaderboards[leaderboardKey]), row.m_ScoresUnbreaking);
						UpdateSlot(row.m_LevelID, row.m_ScoresUnbreaking[0].GetScore(), row.m_ScoresUnbreaking[0].GetGlobalRank(), rankPercentile3);
						sum += row.m_ScoresUnbreaking[0].GetScore();
						UpdateSumText(sum);
						UpdateSorting(m_SortType);
					}
					else
					{
						leaderboard2 = SteamLeaderboards.m_Leaderboards[leaderboardKey];
						if (!row.m_ScoresUnbreakingRequested)
						{
							row.m_ScoresUnbreakingRequested = true;
							row.m_ScoresUnbreaking = GameLeaderboards.CreateGameLeaderboardEntries(await leaderboard2.GetScoresAroundUserAsync(0, 0));
						}
						if (filter != m_LeaderboardsFilter)
						{
							break;
						}
						if (row.m_ScoresUnbreaking == null || row.m_ScoresUnbreaking.Length == 0)
						{
							UpdateSlot(row.m_LevelID, 0, 0, 0);
							UpdateSorting(m_SortType);
						}
						else
						{
							int rankPercentile4 = GameLeaderboards.ComputePlayerPercentile(new GameLeaderboard(leaderboard2), row.m_ScoresUnbreaking);
							UpdateSlot(row.m_LevelID, row.m_ScoresUnbreaking[0].GetScore(), row.m_ScoresUnbreaking[0].GetGlobalRank(), rankPercentile4);
							sum += row.m_ScoresUnbreaking[0].GetScore();
							UpdateSumText(sum);
							UpdateSorting(m_SortType);
						}
					}
				}
				if (filter != LeaderboardsFilter.LOWEST_STRESS || !SteamLeaderboards.m_Leaderboards.ContainsKey(leaderboardKey))
				{
					continue;
				}
				if (row.m_ScoresLowestStress != null)
				{
					int rankPercentile5 = GameLeaderboards.ComputePlayerPercentile(new GameLeaderboard(SteamLeaderboards.m_Leaderboards[leaderboardKey]), row.m_ScoresLowestStress);
					UpdateSlot(row.m_LevelID, row.m_ScoresLowestStress[0].GetScore(), row.m_ScoresLowestStress[0].GetGlobalRank(), rankPercentile5);
					sum += row.m_ScoresLowestStress[0].GetScore();
					UpdateSumText(sum);
					UpdateSorting(m_SortType);
					continue;
				}
				leaderboard2 = SteamLeaderboards.m_Leaderboards[leaderboardKey];
				if (!row.m_ScoresLowestStressRequested)
				{
					row.m_ScoresLowestStressRequested = true;
					row.m_ScoresLowestStress = GameLeaderboards.CreateGameLeaderboardEntries(await leaderboard2.GetScoresAroundUserAsync(0, 0));
				}
				if (filter != m_LeaderboardsFilter)
				{
					break;
				}
				if (row.m_ScoresLowestStress == null || row.m_ScoresLowestStress.Length == 0)
				{
					UpdateSlot(row.m_LevelID, 0, 0, 0);
					UpdateSorting(m_SortType);
					continue;
				}
				int rankPercentile6 = GameLeaderboards.ComputePlayerPercentile(new GameLeaderboard(leaderboard2), row.m_ScoresLowestStress);
				UpdateSlot(row.m_LevelID, row.m_ScoresLowestStress[0].GetScore(), row.m_ScoresLowestStress[0].GetGlobalRank(), rankPercentile6);
				sum += row.m_ScoresLowestStress[0].GetScore();
				UpdateSumText(sum);
				UpdateSorting(m_SortType);
			}
		}
		m_RefreshCounter--;
		if (m_RefreshCounter < 0)
		{
			m_RefreshCounter = 0;
		}
	}

	private void CreateRows()
	{
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!campaignLevel.IsTutorial())
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(m_MyRankingsRowPrefab, m_RowsParent);
					MyRankingsRow component = gameObject.GetComponent<MyRankingsRow>();
					component.Init(campaignWorld, campaignLevel);
					component.gameObject.SetActive(value: false);
					m_SlotDict.Add(campaignLevel.m_Id, gameObject.GetComponent<MyRankingsRow>());
				}
			}
		}
	}

	private bool RowIsPopulated(MyRankingsRow row, LeaderboardsFilter filter)
	{
		if (filter == LeaderboardsFilter.ALL && row.m_ScoresRequested)
		{
			return true;
		}
		if (filter == LeaderboardsFilter.UNBREAKING && row.m_ScoresUnbreakingRequested)
		{
			return true;
		}
		if (filter == LeaderboardsFilter.LOWEST_STRESS && row.m_ScoresLowestStressRequested)
		{
			return true;
		}
		return false;
	}

	private void OnWorldNameChanged()
	{
		if (m_WorldNameDropdown.value == 0)
		{
			m_WorldFilter = CampaignWorlds.WORLD_ID_ALL;
		}
		else
		{
			m_WorldFilter = CampaignWorldDropdown.GetValue(m_WorldNameDropdown.captionText.text).m_Id;
		}
		OnRefreshSlots(m_LeaderboardsFilter, forceRefresh: false);
	}

	private void UpdateSumText(int sum)
	{
		m_SumText.text = ((m_LeaderboardsFilter == LeaderboardsFilter.LOWEST_STRESS) ? Utils.FormatStress((float)sum / 100f) : Utils.FormatCash(sum));
	}

	private int SortByScoreAscending(MyRankingsRow a, MyRankingsRow b)
	{
		if (a.m_RankValue == 0 && b.m_RankValue == 0)
		{
			return 0;
		}
		if (a.m_RankValue > 0 && b.m_RankValue == 0)
		{
			return -1;
		}
		if (a.m_RankValue == 0 && b.m_RankValue > 0)
		{
			return 1;
		}
		return a.m_ScoreValue.CompareTo(b.m_ScoreValue);
	}

	private int SortByScoreDescending(MyRankingsRow a, MyRankingsRow b)
	{
		if (a.m_RankValue == 0 && b.m_RankValue == 0)
		{
			return 0;
		}
		if (a.m_RankValue > 0 && b.m_RankValue == 0)
		{
			return -1;
		}
		if (a.m_RankValue == 0 && b.m_RankValue > 0)
		{
			return 1;
		}
		return b.m_ScoreValue.CompareTo(a.m_ScoreValue);
	}

	private int SortByRankAscending(MyRankingsRow a, MyRankingsRow b)
	{
		if (a.m_RankValue == 0 && b.m_RankValue == 0)
		{
			return 0;
		}
		if (a.m_RankValue > 0 && b.m_RankValue == 0)
		{
			return -1;
		}
		if (a.m_RankValue == 0 && b.m_RankValue > 0)
		{
			return 1;
		}
		return a.m_RankValue.CompareTo(b.m_RankValue);
	}

	private int SortByRankDescending(MyRankingsRow a, MyRankingsRow b)
	{
		if (a.m_RankValue == 0 && b.m_RankValue == 0)
		{
			return 0;
		}
		if (a.m_RankValue > 0 && b.m_RankValue == 0)
		{
			return -1;
		}
		if (a.m_RankValue == 0 && b.m_RankValue > 0)
		{
			return 1;
		}
		return b.m_RankValue.CompareTo(a.m_RankValue);
	}

	private int SortByPercentileAscending(MyRankingsRow a, MyRankingsRow b)
	{
		if (a.m_RankValue == 0 && b.m_RankValue == 0)
		{
			return 0;
		}
		if (a.m_RankValue > 0 && b.m_RankValue == 0)
		{
			return -1;
		}
		if (a.m_RankValue == 0 && b.m_RankValue > 0)
		{
			return 1;
		}
		return a.m_PercentileValue.CompareTo(b.m_PercentileValue);
	}

	private int SortByPercentileDescending(MyRankingsRow a, MyRankingsRow b)
	{
		if (a.m_RankValue == 0 && b.m_RankValue == 0)
		{
			return 0;
		}
		if (a.m_RankValue > 0 && b.m_RankValue == 0)
		{
			return -1;
		}
		if (a.m_RankValue == 0 && b.m_RankValue > 0)
		{
			return 1;
		}
		return b.m_PercentileValue.CompareTo(a.m_PercentileValue);
	}

	private void SortByCampaignOrder(List<MyRankingsRow> rows)
	{
		rows.Clear();
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (m_WorldFilter != CampaignWorlds.WORLD_ID_ALL && campaignWorld.m_Id != m_WorldFilter)
			{
				continue;
			}
			CampaignLevel[] levels = campaignWorld.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				if (!campaignLevel.IsTutorial() && m_SlotDict.ContainsKey(campaignLevel.m_Id))
				{
					rows.Add(m_SlotDict[campaignLevel.m_Id]);
				}
			}
		}
	}

	private void SortRows(List<MyRankingsRow> rows, MyRankingsSortType sortType)
	{
		SortByCampaignOrder(rows);
		switch (sortType)
		{
		case MyRankingsSortType.PERCENTILE_ASCENDING:
			rows.Sort(SortByPercentileAscending);
			break;
		case MyRankingsSortType.PERCENTILE_DESCENDING:
			rows.Sort(SortByPercentileDescending);
			break;
		case MyRankingsSortType.RANK_ASCENDING:
			rows.Sort(SortByRankAscending);
			break;
		case MyRankingsSortType.RANK_DESCENDING:
			rows.Sort(SortByRankDescending);
			break;
		case MyRankingsSortType.SCORE_ASCENDING:
			rows.Sort(SortByScoreAscending);
			break;
		case MyRankingsSortType.SCORE_DESCENDING:
			rows.Sort(SortByScoreDescending);
			break;
		default:
			Debug.LogWarning($"Unexpected sort type: '{sortType}'");
			break;
		case MyRankingsSortType.NONE:
			break;
		}
		HighlightForSortType(sortType);
		for (int i = 0; i < rows.Count; i++)
		{
			rows[i].transform.SetSiblingIndex(i);
			rows[i].Highlight(i, sortType);
		}
	}

	private void HighlightForSortType(MyRankingsSortType sortType)
	{
		m_RankArrow.gameObject.SetActive(value: false);
		m_ScoreArrow.gameObject.SetActive(value: false);
		m_PercentileArrow.gameObject.SetActive(value: false);
		m_RankSortButton.image.color = m_SortInActiveColor;
		m_ScoreSortButton.image.color = m_SortInActiveColor;
		m_PercentileSortButton.image.color = m_SortInActiveColor;
		m_RankText.color = UnityEngine.Color.white;
		m_ScoreText.color = UnityEngine.Color.white;
		m_PercentileText.color = UnityEngine.Color.white;
		m_RankArrow.color = UnityEngine.Color.white;
		m_ScoreArrow.color = UnityEngine.Color.white;
		m_PercentileArrow.color = UnityEngine.Color.white;
		switch (sortType)
		{
		case MyRankingsSortType.RANK_ASCENDING:
			m_RankArrow.gameObject.SetActive(value: true);
			m_RankArrow.transform.localScale = Vector3.one;
			m_RankArrow.color = UnityEngine.Color.black;
			m_RankText.color = UnityEngine.Color.black;
			m_RankSortButton.image.color = m_SortActiveColor;
			break;
		case MyRankingsSortType.RANK_DESCENDING:
			m_RankArrow.gameObject.SetActive(value: true);
			m_RankArrow.transform.localScale = new Vector3(1f, -1f, 1f);
			m_RankArrow.color = UnityEngine.Color.black;
			m_RankText.color = UnityEngine.Color.black;
			m_RankSortButton.image.color = m_SortActiveColor;
			break;
		case MyRankingsSortType.SCORE_ASCENDING:
			m_ScoreArrow.gameObject.SetActive(value: true);
			m_ScoreArrow.transform.localScale = Vector3.one;
			m_ScoreArrow.color = UnityEngine.Color.black;
			m_ScoreText.color = UnityEngine.Color.black;
			m_ScoreSortButton.image.color = m_SortActiveColor;
			break;
		case MyRankingsSortType.SCORE_DESCENDING:
			m_ScoreArrow.gameObject.SetActive(value: true);
			m_ScoreArrow.transform.localScale = new Vector3(1f, -1f, 1f);
			m_ScoreArrow.color = UnityEngine.Color.black;
			m_ScoreText.color = UnityEngine.Color.black;
			m_ScoreSortButton.image.color = m_SortActiveColor;
			break;
		case MyRankingsSortType.PERCENTILE_ASCENDING:
			m_PercentileArrow.gameObject.SetActive(value: true);
			m_PercentileArrow.transform.localScale = Vector3.one;
			m_PercentileArrow.color = UnityEngine.Color.black;
			m_PercentileText.color = UnityEngine.Color.black;
			m_PercentileSortButton.image.color = m_SortActiveColor;
			break;
		case MyRankingsSortType.PERCENTILE_DESCENDING:
			m_PercentileArrow.gameObject.SetActive(value: true);
			m_PercentileArrow.transform.localScale = new Vector3(1f, -1f, 1f);
			m_PercentileArrow.color = UnityEngine.Color.black;
			m_PercentileText.color = UnityEngine.Color.black;
			m_PercentileSortButton.image.color = m_SortActiveColor;
			break;
		default:
			Debug.LogWarning($"Unexpected sort type: '{sortType}'");
			break;
		case MyRankingsSortType.NONE:
			break;
		}
	}

	private List<MyRankingsRow> GetRowsListFromDict(Dictionary<string, MyRankingsRow> dict)
	{
		List<MyRankingsRow> list = new List<MyRankingsRow>();
		foreach (KeyValuePair<string, MyRankingsRow> item in dict)
		{
			if (item.Value.gameObject.activeInHierarchy)
			{
				list.Add(item.Value);
			}
		}
		return list;
	}

	private void ShowGamepadLegend()
	{
		GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.SHOULDER_LEFT, GamepadButtonType.SHOULDER_RIGHT, Localize.Get("KEY_TAB"));
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
