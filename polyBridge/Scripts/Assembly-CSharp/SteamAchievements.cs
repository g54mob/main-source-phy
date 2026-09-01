using System;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

public class SteamAchievements
{
	public static void UnlockAchievement(int achievementId)
	{
		try
		{
			GameAchievement gameAchievement = (GameAchievement)achievementId;
			string text = gameAchievement.ToString();
			foreach (Achievement achievement in SteamUserStats.Achievements)
			{
				if (achievement.Identifier == text)
				{
					achievement.Trigger();
					break;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in UnlockAchievement: " + ex.Message);
		}
	}

	public static bool HasUnlocked(int achievementId)
	{
		try
		{
			GameAchievement gameAchievement = (GameAchievement)achievementId;
			string text = gameAchievement.ToString();
			foreach (Achievement achievement in SteamUserStats.Achievements)
			{
				if (achievement.Identifier == text)
				{
					return achievement.State;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in HasUnlocked: " + ex.Message);
		}
		return false;
	}

	public static DateTime GetUnlockTime(int achievementId)
	{
		try
		{
			GameAchievement gameAchievement = (GameAchievement)achievementId;
			string text = gameAchievement.ToString();
			foreach (Achievement achievement in SteamUserStats.Achievements)
			{
				if (achievement.Identifier == text)
				{
					return achievement.UnlockTime.Value;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in GetUnlockTime: " + ex.Message);
		}
		return DateTime.MinValue;
	}

	public static void ResetAllAchievements()
	{
		try
		{
			SteamUserStats.ResetAll(includeAchievements: true);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in ResetAllAchievements: " + ex.Message);
		}
	}
}
