using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	public static class MMAchievementManager
	{
		private static List<MMAchievement> _achievements;

		private static MMAchievement _achievement;

		public static string _defaultFileName;

		public static string _saveFolderName;

		public static string _saveFileExtension;

		public static string SaveFileName;

		public static string ListID;

		public static List<MMAchievement> AchievementsList => null;

		public static void LoadAchievementList(MMAchievementList achievementList)
		{
		}

		public static void UnlockAchievement(string achievementID)
		{
		}

		public static void LockAchievement(string achievementID)
		{
		}

		public static void AddProgress(string achievementID, int newProgress)
		{
		}

		public static void SetProgress(string achievementID, int newProgress)
		{
		}

		private static MMAchievement AchievementManagerContains(string searchedID)
		{
			return null;
		}

		public static void ResetAchievements(string listID)
		{
		}

		public static void ResetAllAchievements()
		{
		}

		public static void LoadSavedAchievements()
		{
		}

		public static void SaveAchievements()
		{
		}

		private static void DeterminePath(string specifiedFileName = "")
		{
		}

		public static void FillSerializedMMAchievementManager(SerializedMMAchievementManager serializedAchievements)
		{
		}

		public static void ExtractSerializedMMAchievementManager(SerializedMMAchievementManager serializedAchievements)
		{
		}
	}
}
