using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyRankingsRow : MonoBehaviour
{
	[Header("Fields")]
	public TextMeshProUGUI m_World;

	public TextMeshProUGUI m_LevelName;

	public TextMeshProUGUI m_Rank;

	public TextMeshProUGUI m_Score;

	public TextMeshProUGUI m_Percentile;

	[Header("Highlights")]
	public Image m_RowHighlight;

	public Image m_RankHighlight;

	public Image m_ScoreHighlight;

	public Image m_PercentileHighlight;

	[Header("Highlight Colors")]
	public Color m_RankHighlightEven;

	public Color m_RankHighlightOdd;

	[NonSerialized]
	public string m_WorldID;

	[NonSerialized]
	public string m_LevelID;

	[NonSerialized]
	public GameLeaderboardEntry[] m_Scores;

	[NonSerialized]
	public GameLeaderboardEntry[] m_ScoresUnbreaking;

	[NonSerialized]
	public GameLeaderboardEntry[] m_ScoresLowestStress;

	[NonSerialized]
	public bool m_ScoresRequested;

	[NonSerialized]
	public bool m_ScoresUnbreakingRequested;

	[NonSerialized]
	public bool m_ScoresLowestStressRequested;

	[NonSerialized]
	public int m_ScoreValue;

	[NonSerialized]
	public int m_RankValue;

	[NonSerialized]
	public int m_PercentileValue;

	public void MakeDynamicDataBlank()
	{
		m_Rank.text = string.Empty;
		m_Score.text = string.Empty;
		m_Percentile.text = string.Empty;
	}

	public void Reset()
	{
		m_Rank.text = string.Empty;
		m_Percentile.text = string.Empty;
		m_Scores = null;
		m_ScoresUnbreaking = null;
		m_ScoresLowestStress = null;
		m_ScoresRequested = false;
		m_ScoresUnbreakingRequested = false;
		m_ScoresLowestStressRequested = false;
	}

	public void SetScoreAndRank(int score, int rank, int percentile, bool isStress)
	{
		if (rank < 1)
		{
			m_Score.text = string.Empty;
			m_Rank.text = Localize.Get("UI_UNRANKED");
			m_Percentile.text = string.Empty;
		}
		else
		{
			m_Score.text = (isStress ? Utils.FormatStress((float)score / 100f) : Utils.FormatCash(score));
			m_Rank.text = Utils.FormatIntegerWithCommas(rank);
			m_Percentile.text = Utils.FormatPercentileAsTopBottom(percentile);
		}
		m_ScoreValue = score;
		m_RankValue = rank;
		m_PercentileValue = percentile;
	}

	public void Init(CampaignWorld world, CampaignLevel level)
	{
		m_World.text = level.m_NumberPrefix;
		m_LevelName.text = level.GetLocalizedDisplayNameWithoutPrefix();
		m_Rank.text = string.Empty;
		m_Score.text = string.Empty;
		m_Percentile.text = string.Empty;
		m_WorldID = world.m_Id;
		m_LevelID = level.m_Id;
	}

	public void Highlight(int index, MyRankingsSortType sortType)
	{
		if (index % 2 == 0)
		{
			m_RowHighlight.gameObject.SetActive(value: false);
		}
		else
		{
			m_RowHighlight.gameObject.SetActive(value: true);
		}
	}
}
