using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[CreateAssetMenu(fileName = "AchievementList", menuName = "MoreMountains/Achievement List")]
	public class MMAchievementList : ScriptableObject
	{
		public string AchievementsListID;

		public List<MMAchievement> Achievements;

		private MMReferenceHolder<MMAchievementList> _instances;

		public static MMAchievementList Any => null;

		public virtual void ResetAchievements()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
