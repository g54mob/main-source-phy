public class LeaderboardFilterState
{
	public LeaderboardsView m_LeaderboardsView;

	public LeaderboardsFilter m_LeaderboardsFilter;

	public string m_LevelId;

	public int m_TopCount;

	public void Reset()
	{
		m_LeaderboardsView = LeaderboardsView.AROUND_YOU;
		m_LeaderboardsFilter = LeaderboardsFilter.ALL;
		m_LevelId = string.Empty;
		m_TopCount = 0;
	}

	public bool IsDifferentThan(LeaderboardFilterState other)
	{
		if (m_LeaderboardsView == other.m_LeaderboardsView && m_LeaderboardsFilter == other.m_LeaderboardsFilter && !(m_LevelId != other.m_LevelId))
		{
			return m_TopCount != other.m_TopCount;
		}
		return true;
	}

	public void CopyFrom(LeaderboardFilterState other)
	{
		m_LeaderboardsView = other.m_LeaderboardsView;
		m_LeaderboardsFilter = other.m_LeaderboardsFilter;
		m_LevelId = other.m_LevelId;
		m_TopCount = other.m_TopCount;
	}

	public void Set(LeaderboardsView view, LeaderboardsFilter filter, string levelId, int topCount)
	{
		m_LeaderboardsView = view;
		m_LeaderboardsFilter = filter;
		m_LevelId = levelId;
		m_TopCount = topCount;
	}
}
