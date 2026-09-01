using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GameGrind
{
	[AddComponentMenu("Journal/Journal")]
	public class Journal : MonoBehaviour
	{
		public static List<Achievement> achievementMaster = new List<Achievement>();

		public AchievementUIList achievementUI;

		public static string saveDataFolder = "Achievements";

		private static string saveFileName = "achievement-save.json";

		private static string saveDataDir;

		private static string saveDataPath;

		public static void Create()
		{
			string path = "JSON/Achievements";
			string empty = string.Empty;
			empty = ((!(Resources.Load<TextAsset>(path) != null)) ? Resources.Load<TextAsset>("JSON/Demo-Achievements").text : Resources.Load<TextAsset>(path).text);
			saveDataDir = Path.Combine(Application.persistentDataPath, saveDataFolder).Replace("\\", "/");
			saveDataPath = Path.Combine(saveDataDir, saveFileName).Replace("\\", "/");
			AchievementCollection achievementCollection = JsonUtility.FromJson<AchievementCollection>(empty);
			foreach (Achievement achievement in achievementCollection.AchievementList)
			{
				achievementMaster.Add(achievement);
			}
		}

		public static void Create(Achievement stat)
		{
			achievementMaster.Add(stat);
		}

		public static void Increment(int id, int amount)
		{
			Achievement achievement = GetAchievement(id);
			if (achievement == null)
			{
				Debug.LogWarningFormat("Achievement {0} could not be found. Double check your IDs in the Journal Manager.", id);
			}
			else
			{
				Increment(achievement, amount);
			}
		}

		public static void Increment(Achievement achievement, int amount)
		{
			if (achievement != null && achievement.value < achievement.neededValue && amount != 0)
			{
				achievement.value += amount;
				AchievementController.CheckForCompletion(achievement);
				AchievementEvents.AchievementValueChanged(achievement);
			}
		}

		public static void SetValue(int id, int value, bool triggerGrant = true)
		{
			Achievement achievement = GetAchievement(id);
			SetValue(achievement, value, triggerGrant);
		}

		public static void SetValue(Achievement achievement, int value, bool triggerUpdate = true)
		{
			if (achievement.value == value)
			{
				return;
			}
			bool completed = achievement.completed;
			achievement.value = value;
			if (achievement.value >= achievement.neededValue)
			{
				achievement.completed = true;
			}
			if (triggerUpdate)
			{
				if (!completed && achievement.completed)
				{
					AchievementController.GrantWithScore(achievement);
				}
				AchievementEvents.AchievementValueChanged(achievement);
			}
		}

		public static Achievement GetAchievement(int id)
		{
			foreach (Achievement item in achievementMaster)
			{
				if (item.id == id)
				{
					return item;
				}
			}
			Debug.LogWarning("Stat " + id + " doesn't exist!");
			return null;
		}

		public static float GetAchievementValue(int id)
		{
			return GetAchievement(id).value;
		}

		public static int GetAchievementScore()
		{
			return AchievementController.CurrentAchievementScore;
		}

		public static void Save()
		{
			AchievementProgressDataCollection achievementProgressDataCollection = new AchievementProgressDataCollection();
			foreach (Achievement item in achievementMaster)
			{
				achievementProgressDataCollection.achievementProgressList.Add(new AchievementProgressData(item.id, item.value));
			}
			if (achievementProgressDataCollection.achievementProgressList.Count > 0)
			{
				string contents = JsonUtility.ToJson(achievementProgressDataCollection, true);
				string path = saveDataDir;
				Directory.CreateDirectory(path);
				File.WriteAllText(saveDataPath, contents);
			}
		}

		public static bool SaveExists()
		{
			return File.Exists(saveDataPath);
		}

		public static void DeleteSave()
		{
			if (SaveExists())
			{
				File.Delete(saveDataPath);
			}
		}

		public static void Load()
		{
			if (SaveExists())
			{
				string json = File.ReadAllText(saveDataPath);
				AchievementProgressDataCollection achievementProgressDataCollection = JsonUtility.FromJson<AchievementProgressDataCollection>(json);
				for (int i = 0; i < achievementProgressDataCollection.achievementProgressList.Count; i++)
				{
					SetValueSafe(achievementProgressDataCollection.achievementProgressList[i].id, achievementProgressDataCollection.achievementProgressList[i].value, false);
				}
			}
		}

		public static void ResetAllStats(bool saveAfterDone = true)
		{
			achievementMaster.ForEach(delegate(Achievement i)
			{
				i.value = 0;
				i.completed = false;
			});
			if (saveAfterDone)
			{
				Save();
			}
		}

		private static void SetValueSafe(int id, int value, bool triggerGrant = true)
		{
			Achievement achievement = achievementMaster.FirstOrDefault((Achievement x) => x.id == id);
			if (achievement != null)
			{
				SetValue(achievement, value, triggerGrant);
			}
		}
	}
}
