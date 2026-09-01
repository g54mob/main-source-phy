using System.Collections.Generic;
using Landfall.TABS.GameMode;
using TFBGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementService : IService
{
	private const float TimeBetweenAchievementProgression = 5f;

	private float progressAchievementTimer;

	private Dictionary<string, int> achievementBuffer = new Dictionary<string, int>();

	private ITabsAchievements platformAchievements;

	private GameModeService m_gmService;

	private AchievementDataServiceAsset achievementData;

	public static bool bypassMainMenuCheck;

	public void UnlockAchievement(string id)
	{
		if (CanUnlock() && platformAchievements != null)
		{
			platformAchievements.UnlockAchievement(id);
		}
	}

	private bool CanUnlock()
	{
		bool flag = m_gmService.IsCurrentBaseGameModeType<MainMenuGameMode>() && !bypassMainMenuCheck;
		bool flag2 = SceneManager.GetActiveScene().name == "BootScene";
		if (m_gmService.IsCurrentBaseGameModeType<OnlineMultiplayerGameMode>())
		{
			return false;
		}
		if (m_gmService.IsCurrentBaseGameModeType<LocalMultiplayerGameMode>())
		{
			return false;
		}
		return !flag || flag2;
	}

	public void AdvanceAchievementProgress(string key, int amountToAdvance)
	{
		if (CanUnlock() && achievementBuffer != null)
		{
			if (achievementBuffer.ContainsKey(key))
			{
				achievementBuffer[key] += amountToAdvance;
			}
			else
			{
				achievementBuffer.Add(key, amountToAdvance);
			}
		}
	}

	private void UpdateProgressAchievementsFromBuffer()
	{
		if (achievementBuffer == null || achievementBuffer.Count <= 0 || platformAchievements == null)
		{
			return;
		}
		foreach (KeyValuePair<string, int> item in achievementBuffer)
		{
			platformAchievements.AdvanceAchievementProgress(item.Key, item.Value);
		}
		achievementBuffer.Clear();
	}

	public void OnStart()
	{
		platformAchievements = new SteamTabsAchievements();
		m_gmService = ServiceLocator.GetService<GameModeService>();
		achievementData = ServiceLocator.GetService<AchievementDataServiceAsset>();
	}

	public void IngestAllStats()
	{
	}

	public void ResetStats()
	{
	}

	public void UnlockAchievement(string key, int value)
	{
		if (CanUnlock())
		{
			Debug.Log("ADDED VALUE TO ACHIEVEMENT: " + key + " + " + value);
		}
	}

	public void OnRegister()
	{
	}

	public void OnAwake()
	{
	}

	public void OnUpdate()
	{
		progressAchievementTimer += Time.unscaledDeltaTime;
		if (progressAchievementTimer >= 5f)
		{
			UpdateProgressAchievementsFromBuffer();
			progressAchievementTimer = 0f;
		}
	}

	public void OnFixedUpdate()
	{
	}

	public void OnLateUpdate()
	{
	}

	public void UnRegister()
	{
	}
}
