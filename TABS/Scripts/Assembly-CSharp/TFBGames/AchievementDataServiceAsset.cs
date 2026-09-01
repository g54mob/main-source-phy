using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	[CreateAssetMenu(menuName = "Achievement Data", fileName = "AchievementData")]
	public class AchievementDataServiceAsset : ServiceAsset
	{
		public List<TABSAchievement> achievements = new List<TABSAchievement>();

		public TABSAchievement GetAchievementForKey(string key)
		{
			if (achievements == null || achievements.Count <= 0)
			{
				return null;
			}
			foreach (TABSAchievement achievement in achievements)
			{
				if (achievement.Key == key)
				{
					return achievement;
				}
			}
			return null;
		}

		public List<TABSAchievement> GetAllProgressAchievements()
		{
			if (achievements == null || achievements.Count <= 0)
			{
				return null;
			}
			List<TABSAchievement> list = new List<TABSAchievement>();
			foreach (TABSAchievement achievement in achievements)
			{
				if (achievement.Data.IsProgressAchievement)
				{
					list.Add(achievement);
				}
			}
			return list;
		}

		private void SetAchievements(List<TABSAchievement> loadedAchievements)
		{
			achievements = loadedAchievements;
		}
	}
}
