using System;
using UnityEngine;

namespace TFBGames
{
	[Serializable]
	public class TABSAchievement : IEquatable<TABSAchievement>
	{
		[SerializeField]
		private string achievementId;

		[SerializeField]
		private string achievementName;

		[SerializeField]
		private string achievementKey;

		[SerializeField]
		private AchievementData achievementData;

		public string AchievementId => achievementId;

		public string Name => achievementName;

		public string Key => achievementKey;

		public AchievementData Data => achievementData;

		public TABSAchievement(string id, string name, string key, bool isProgress, int minValue, int maxValue)
		{
			achievementId = id;
			achievementName = name;
			achievementKey = key;
			achievementData = new AchievementData(isProgress, minValue, maxValue);
		}

		public bool Equals(TABSAchievement other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			return achievementId == other.achievementId;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((TABSAchievement)obj);
		}

		public override int GetHashCode()
		{
			if (achievementId == null)
			{
				return 0;
			}
			return achievementId.GetHashCode();
		}
	}
}
