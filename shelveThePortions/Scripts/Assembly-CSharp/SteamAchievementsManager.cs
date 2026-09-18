using Steamworks;
using UnityEngine;

public class SteamAchievementsManager : Singleton<SteamAchievementsManager>
{
	private bool isSteamInitialized;

	[SerializeField]
	private bool isDebugOn;

	private void Start()
	{
		isSteamInitialized = SteamManager.Initialized;
	}

	public bool IsAchievementCompleted(string achievementAPI_Name)
	{
		if (!isSteamInitialized)
		{
			return true;
		}
		SteamUserStats.GetAchievement(achievementAPI_Name, out var pbAchieved);
		return pbAchieved;
	}

	public void SetAchievement(string achievementAPI_Name)
	{
		if (isSteamInitialized)
		{
			SteamUserStats.GetAchievement(achievementAPI_Name, out var pbAchieved);
			if (!pbAchieved)
			{
				SteamUserStats.SetAchievement(achievementAPI_Name);
				SteamUserStats.StoreStats();
			}
		}
	}

	public void ResetAchievements()
	{
		if (isSteamInitialized)
		{
			SteamUserStats.ResetAllStats(bAchievementsToo: true);
		}
	}
}
