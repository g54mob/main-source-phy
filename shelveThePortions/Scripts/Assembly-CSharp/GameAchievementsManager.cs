using System.Collections.Generic;
using UnityEngine;

public class GameAchievementsManager : Singleton<GameAchievementsManager>
{
	[SerializeField]
	private SteamAchievementsManager steamAchievementsManager;

	[SerializeField]
	private bool isDebugOn;

	[SerializeField]
	private List<AchievementTypes> CompletedAchievementTypes = new List<AchievementTypes>();

	private void Start()
	{
	}

	public void ActivateAchievement(AchievementTypes achievementType)
	{
		if (isDebugOn)
		{
			Debug.Log(achievementType.ToString() + " ActivateAchievement");
		}
		if (!(steamAchievementsManager == null) && !CompletedAchievementTypes.Contains(achievementType))
		{
			if (isDebugOn)
			{
				Debug.Log(achievementType.ToString() + " Completed");
			}
			steamAchievementsManager.SetAchievement(achievementType.ToString());
			if (IsAchievementCompleted(achievementType))
			{
				CompletedAchievementTypes.Add(achievementType);
			}
		}
	}

	public bool IsAchievementCompleted(AchievementTypes achievementType)
	{
		return steamAchievementsManager.IsAchievementCompleted(achievementType.ToString());
	}
}
