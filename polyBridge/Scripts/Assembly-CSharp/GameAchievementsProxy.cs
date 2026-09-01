using System;
using System.Collections.Generic;

public class GameAchievementsProxy
{
	public Dictionary<int, DateTime> m_LocalAchievementIDsUnlockedList;

	public Dictionary<string, int> m_LocalAchievementStats;

	public GameAchievementsProxy()
	{
		m_LocalAchievementIDsUnlockedList = new Dictionary<int, DateTime>(GameAchievements.m_LocalAchievementIDsUnlockedList);
		m_LocalAchievementStats = new Dictionary<string, int>(GameAchievements.m_LocalAchievementStats);
	}
}
