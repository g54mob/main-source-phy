using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardSlot : MonoBehaviour
{
	public Image m_Highlight;

	public TextMeshProUGUI m_RankText;

	public TextMeshProUGUI m_OwnedBy;

	public TextMeshProUGUI m_ScoreText;

	public Image m_BreakIcon;

	public Button m_PlayButton;

	[NonSerialized]
	public int m_Rank;

	[NonSerialized]
	public int m_Score;

	[NonSerialized]
	public string m_OwnerId;

	[NonSerialized]
	public string m_LevelId;

	[NonSerialized]
	public string m_LeaderboardKey;

	[NonSerialized]
	public string m_DisplayName;

	private RectTransform m_OwnedByRectTransform;

	public void Awake()
	{
	}

	public void Populate(LeaderboardFilterState filter, int rank, string steamId, string displayName, int score, bool didBreak)
	{
		m_RankText.text = GameLeaderboards.FormatRank(rank);
		GameUI.SetAndEnableText(m_OwnedBy, displayName);
		m_ScoreText.text = GameLeaderboards.FormatScore(score, filter);
		m_BreakIcon.gameObject.SetActive(didBreak);
		m_Rank = rank;
		m_Score = score;
		m_OwnerId = steamId;
		m_LevelId = filter.m_LevelId;
		m_DisplayName = displayName;
		m_LeaderboardKey = GameLeaderboards.GetLeadboardKey(filter);
	}

	private void OnPlay()
	{
		LeaderboardReplay.Run(m_LevelId, m_OwnerId, m_DisplayName, m_LeaderboardKey, m_Score);
	}
}
