using TMPro;
using UnityEngine;

public class Panel_Histogram : MonoBehaviour
{
	public GameObject m_HistogramRoot;

	[Header("Bars")]
	public Panel_HistogramBar[] m_Bars;

	public ToolTip m_BarTooltip;

	public int m_BarHeight;

	[Header("Placement Bar")]
	public RectTransform m_YouBar;

	public GameObject m_YouRight;

	public GameObject m_YouLeft;

	public TextMeshProUGUI m_YouRightPercentileText;

	public TextMeshProUGUI m_YouLeftPercentileText;

	[Header("Footer")]
	public TextMeshProUGUI m_StartBucketScore;

	public TextMeshProUGUI m_EndBucketScore;

	[Header("Stress Colors")]
	public Color m_EvenColor;

	public Color m_EvenColorHighlight;

	public Color m_OddColor;

	public Color m_OddColorHighlight;

	[Header("Under Budget Colors")]
	public Color m_EvenColorUnderBudget;

	public Color m_EvenColorHighlightUnderBudget;

	public Color m_OddColorUnderBudget;

	public Color m_OddColorHighlightUnderBudget;

	[Header("Over Budget Colors")]
	public Color m_EvenColorOverBudget;

	public Color m_EvenColorHighlightOverBudget;

	public Color m_OddColorOverBudget;

	public Color m_OddColorHighlightOverBudget;

	[Header("Player Score Colors")]
	public Color m_PlayerBucketColor;

	public Color m_PlayerBucketColorHighlight;

	private static readonly int BAR_WIDTH = 10;

	private static readonly float YOU_BAR_START_X = 100f;

	private LeaderboardBucketArrays m_Buckets;

	private LeaderboardsFilter m_Filter;

	private int m_LevelBudget;

	private int m_PlayerScore;

	private void Start()
	{
	}

	private void Update()
	{
		if (m_Buckets == null)
		{
			m_BarTooltip.gameObject.SetActive(value: false);
			return;
		}
		int num = -1;
		for (int i = 0; i < LeaderboardBucketArrays.BUCKETS_PER_ARRAY; i++)
		{
			if (m_Bars[i].m_PointerEvents.m_IsHovering)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			m_BarTooltip.gameObject.SetActive(value: false);
		}
		else
		{
			m_BarTooltip.ForceEnable();
			if (m_PlayerScore != -1)
			{
				int score = ((num == 0) ? Mathf.Min(m_PlayerScore, m_Buckets.m_Start[num]) : m_Buckets.m_Start[num]);
				int score2 = ((num == LeaderboardBucketArrays.BUCKETS_PER_ARRAY - 1) ? Mathf.Max(m_PlayerScore, m_Buckets.m_End[num]) : m_Buckets.m_End[num]);
				m_BarTooltip.Set(FormatScore(score2, m_Filter) + " - " + FormatScore(score, m_Filter), null);
			}
			else
			{
				int score3 = ((num == 0) ? m_Buckets.m_Start[num] : m_Buckets.m_Start[num]);
				int score4 = ((num == LeaderboardBucketArrays.BUCKETS_PER_ARRAY - 1) ? m_Buckets.m_End[num] : m_Buckets.m_End[num]);
				m_BarTooltip.Set(FormatScore(score4, m_Filter) + " - " + FormatScore(score3, m_Filter), null);
			}
			GameUI.SetScreenPosClamped(m_BarTooltip.gameObject, GameInput.GetMousePosition(), 0f, 0f);
		}
		int num2 = ((m_PlayerScore != -1) ? GetPlayerBucketIndex(m_Buckets, m_PlayerScore) : (-1));
		for (int j = 0; j < LeaderboardBucketArrays.BUCKETS_PER_ARRAY; j++)
		{
			if (j == num2)
			{
				m_Bars[j].m_Image.color = m_PlayerBucketColor;
			}
			else
			{
				m_Bars[j].m_Image.color = GetBarColor(j, m_Buckets, m_LevelBudget, m_Filter);
			}
			if (j == num)
			{
				if (j == num2)
				{
					m_Bars[j].m_Image.color = m_PlayerBucketColorHighlight;
				}
				else
				{
					m_Bars[j].m_Image.color = GetBarHighlightColor(j, m_Buckets, m_LevelBudget, m_Filter);
				}
			}
		}
	}

	public void EnableHistogramUI(bool enable)
	{
		m_HistogramRoot.gameObject.SetActive(enable);
		if (!enable)
		{
			m_BarTooltip.gameObject.SetActive(value: false);
		}
	}

	public int GetNumScores(string levelID, LeaderboardsFilter filter)
	{
		LeaderboardBucketArrays leaderboardBuckets = GetLeaderboardBuckets(levelID, filter);
		if (leaderboardBuckets == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < LeaderboardBucketArrays.BUCKETS_PER_ARRAY; i++)
		{
			num += leaderboardBuckets.m_Count[i];
		}
		return num;
	}

	public void Populate(string levelID, LeaderboardsFilter filter, int levelBudget)
	{
		LeaderboardBucketArrays leaderboardBuckets = GetLeaderboardBuckets(levelID, filter);
		if (leaderboardBuckets == null)
		{
			return;
		}
		m_YouBar.gameObject.SetActive(value: false);
		int num = 0;
		for (int i = 0; i < LeaderboardBucketArrays.BUCKETS_PER_ARRAY; i++)
		{
			if (leaderboardBuckets.m_Count[i] > num)
			{
				num = leaderboardBuckets.m_Count[i];
			}
		}
		for (int j = 0; j < LeaderboardBucketArrays.BUCKETS_PER_ARRAY; j++)
		{
			m_Bars[j].m_RectTransform.sizeDelta = new Vector2(BAR_WIDTH, (float)m_BarHeight * (float)leaderboardBuckets.m_Count[j] / (float)num);
			m_Bars[j].m_Image.color = GetBarColor(j, leaderboardBuckets, levelBudget, filter);
		}
		m_StartBucketScore.text = FormatScore(leaderboardBuckets.m_Start[0], filter);
		int num2 = leaderboardBuckets.m_End[LeaderboardBucketArrays.BUCKETS_PER_ARRAY - 1];
		m_EndBucketScore.text = ((num2 == 0) ? string.Empty : FormatScore(num2, filter));
		m_Buckets = leaderboardBuckets;
		m_Filter = filter;
		m_LevelBudget = levelBudget;
		m_PlayerScore = -1;
	}

	public void ShowPlayerScore(string levelID, int playerScore, LeaderboardFilterState filterState)
	{
		LeaderboardBucketArrays leaderboardBuckets = GetLeaderboardBuckets(levelID, filterState.m_LeaderboardsFilter);
		if (leaderboardBuckets != null)
		{
			int playerBucketIndex = GetPlayerBucketIndex(leaderboardBuckets, playerScore);
			PositionYouBar(leaderboardBuckets, playerScore, playerBucketIndex, filterState);
			m_YouBar.gameObject.SetActive(value: true);
			m_StartBucketScore.text = FormatScore(Mathf.Min(playerScore, leaderboardBuckets.m_Start[0]), filterState.m_LeaderboardsFilter);
			int num = Mathf.Max(playerScore, leaderboardBuckets.m_End[LeaderboardBucketArrays.BUCKETS_PER_ARRAY - 1]);
			m_EndBucketScore.text = ((num == 0) ? string.Empty : FormatScore(num, filterState.m_LeaderboardsFilter));
			m_PlayerScore = playerScore;
		}
	}

	private void PositionYouBar(LeaderboardBucketArrays buckets, int playerScore, int playerBucketIndex, LeaderboardFilterState filterState)
	{
		float num = playerBucketIndex * BAR_WIDTH;
		int num2 = buckets.m_End[playerBucketIndex] - buckets.m_Start[playerBucketIndex];
		float num3 = 1f;
		if (num2 > 0)
		{
			num3 = Mathf.Clamp01((float)(playerScore - buckets.m_Start[playerBucketIndex]) / (float)(buckets.m_End[playerBucketIndex] - buckets.m_Start[playerBucketIndex]));
		}
		float x = YOU_BAR_START_X - (num + (float)(BAR_WIDTH - 2) * num3);
		m_YouBar.anchoredPosition = new Vector2(x, m_YouBar.anchoredPosition.y);
		int num4 = GameLeaderboards.TryGetBestPlayerScorePercentile(filterState);
		if (num4 >= 0)
		{
			m_YouRightPercentileText.text = Utils.FormatPercentileAsTopBottom(num4);
			m_YouLeftPercentileText.text = Utils.FormatPercentileAsTopBottom(num4);
			m_YouRight.SetActive(playerBucketIndex > 9);
			m_YouLeft.SetActive(!m_YouRight.activeSelf);
		}
		else
		{
			m_YouRight.SetActive(value: false);
			m_YouLeft.SetActive(value: false);
		}
	}

	private string FormatScore(int score, LeaderboardsFilter filter)
	{
		if (filter == LeaderboardsFilter.ALL || filter == LeaderboardsFilter.UNBREAKING)
		{
			return Utils.FormatCash(score);
		}
		return Utils.FormatStress((float)score / 100f);
	}

	private int GetPlayerBucketIndex(LeaderboardBucketArrays buckets, int playerScore)
	{
		for (int i = 0; i < LeaderboardBucketArrays.BUCKETS_PER_ARRAY; i++)
		{
			if (playerScore >= buckets.m_Start[i] && playerScore <= buckets.m_End[i])
			{
				return i;
			}
		}
		if (playerScore < buckets.m_Start[0])
		{
			return 0;
		}
		if (playerScore > buckets.m_End[LeaderboardBucketArrays.BUCKETS_PER_ARRAY - 1])
		{
			return LeaderboardBucketArrays.BUCKETS_PER_ARRAY - 1;
		}
		return 0;
	}

	private LeaderboardBucketArrays GetLeaderboardBuckets(string levelID, LeaderboardsFilter filter)
	{
		if (!LeaderboardBuckets.m_Levels.ContainsKey(levelID))
		{
			Debug.LogWarning("Leaderboard buckets don't exist for level '" + levelID + "'");
			return null;
		}
		switch (filter)
		{
		case LeaderboardsFilter.ALL:
			return LeaderboardBuckets.m_Levels[levelID].m_Score;
		case LeaderboardsFilter.UNBREAKING:
			return LeaderboardBuckets.m_Levels[levelID].m_UnbreakingScore;
		case LeaderboardsFilter.LOWEST_STRESS:
			return LeaderboardBuckets.m_Levels[levelID].m_StressScore;
		default:
			Debug.LogWarning("Unexpected filter in GetLeaderboardBuckets '" + filter.ToString() + "'");
			return null;
		}
	}

	private Color GetBarColor(int index, LeaderboardBucketArrays buckets, int levelBudget, LeaderboardsFilter filter)
	{
		if (filter == LeaderboardsFilter.LOWEST_STRESS || m_LevelBudget == 0)
		{
			if (index % 2 != 0)
			{
				return m_OddColor;
			}
			return m_EvenColor;
		}
		if (BucketIsOverBudget(buckets, levelBudget, index))
		{
			if (index % 2 != 0)
			{
				return m_OddColorOverBudget;
			}
			return m_EvenColorOverBudget;
		}
		if (index % 2 != 0)
		{
			return m_OddColorUnderBudget;
		}
		return m_EvenColorUnderBudget;
	}

	private Color GetBarHighlightColor(int index, LeaderboardBucketArrays buckets, int levelBudget, LeaderboardsFilter filter)
	{
		if (m_Filter == LeaderboardsFilter.LOWEST_STRESS || m_LevelBudget == 0)
		{
			if (index % 2 != 0)
			{
				return m_OddColorHighlight;
			}
			return m_EvenColorHighlight;
		}
		if (BucketIsOverBudget(buckets, levelBudget, index))
		{
			if (index % 2 != 0)
			{
				return m_OddColorHighlightOverBudget;
			}
			return m_EvenColorHighlightOverBudget;
		}
		if (index % 2 != 0)
		{
			return m_OddColorHighlightUnderBudget;
		}
		return m_EvenColorHighlightUnderBudget;
	}

	private bool BucketIsOverBudget(LeaderboardBucketArrays buckets, int levelBudget, int index)
	{
		return buckets.m_End[index] > levelBudget;
	}
}
