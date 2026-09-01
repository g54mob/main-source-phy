using System;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public class AchievementData
	{
		[SerializeField]
		private bool isProgressAchievement;

		[SerializeField]
		private int achievementMinValue;

		[SerializeField]
		private int achievementMaxValue;

		public bool IsProgressAchievement => isProgressAchievement;

		public int MinValue => achievementMinValue;

		public int MaxValue => achievementMaxValue;

		public AchievementData(bool isProgress, int min, int max)
		{
			isProgressAchievement = isProgress;
			achievementMinValue = min;
			achievementMaxValue = max;
		}
	}
}
