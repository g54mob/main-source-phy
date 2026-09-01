using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class GameAchievements
{
	public static Dictionary<int, DateTime> m_LocalAchievementIDsUnlockedList = new Dictionary<int, DateTime>();

	public static Dictionary<string, int> m_LocalAchievementStats = new Dictionary<string, int>();

	public static Dictionary<string, HashSet<string>> m_FailedSimChecksums = new Dictionary<string, HashSet<string>>();

	public static HashSet<string> m_LevelsFailed = new HashSet<string>();

	public static readonly int NUM_FAILURES_TO_TRIGGER_NEVER_GONNA_GIVE_YOU_UP = 20;

	private static string GAME_LOCAL_ACHIEVEMENTS_FILENAME = ".localachievements";

	private static string FAILED_SIM_CHECKSUMS_FILENAME = ".failedsimchecksums";

	private static string FAILED_LEVELS_FILENAME = ".failedlevels";

	private static readonly int SPEEDRUNNER_THRESHOLD_SECONDS = 300;

	private static float m_SpeedRunnerStartTime;

	public static void Init()
	{
		LoadLocalAchievements();
		LoadFailedSimChecksums();
		LoadFailedLevels();
		InvalidateSpeedRunnerTimer();
	}

	public static void MaybeUnlockAchievements(GameMode gameMode, string levelID, bool levelPassed, bool levelFailed)
	{
		try
		{
			if (gameMode == GameMode.CAMPAIGN && levelPassed)
			{
				CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
				if (worldWithLevelId != null)
				{
					GetProgressValues((int)worldWithLevelId.m_BeatWorldAchievement, out var progressValue, out var totalValue);
					if (totalValue > 0 && progressValue >= totalValue)
					{
						UnlockAchievement(worldWithLevelId.m_BeatWorldAchievement);
					}
				}
				if (!HasUnlocked(GameAchievement.Unlock_2Sheep) && CampaignWorlds.m_Instance.GetWorldById("003").IsUnLocked())
				{
					UnlockAchievement(GameAchievement.Unlock_2Sheep);
				}
				if (!HasUnlocked(GameAchievement.Unlock_3Sheep) && CampaignWorlds.m_Instance.GetWorldById("007").IsUnLocked())
				{
					UnlockAchievement(GameAchievement.Unlock_3Sheep);
				}
				if (!HasUnlocked(GameAchievement.Unlock_4Sheep) && CampaignWorlds.m_Instance.GetWorldById("011").IsUnLocked())
				{
					UnlockAchievement(GameAchievement.Unlock_4Sheep);
				}
				if (!HasUnlocked(GameAchievement.Unlock_5Sheep) && CampaignWorlds.m_Instance.GetWorldById("014").IsUnLocked())
				{
					UnlockAchievement(GameAchievement.Unlock_5Sheep);
				}
				if (worldWithLevelId.m_Id == "009" && Budget.m_SpringBudget > 0 && Bridge.m_BridgeRestore != null && Bridge.m_BridgeRestore.m_BridgeSprings.Count == 0)
				{
					UnlockAchievement(GameAchievement.Fun_Inflexable);
				}
			}
			if (!levelPassed)
			{
				return;
			}
			if (gameMode == GameMode.CAMPAIGN)
			{
				if (Budget.m_HydraulicBudget > 0 && Bridge.m_BridgeRestore != null && Bridge.m_BridgeRestore.m_Pistons.Count == 0)
				{
					UnlockAchievement(GameAchievement.Fun_Hydrophobic);
				}
				MaybeUnlockSpeedRunner(levelID);
			}
			foreach (Vehicle vehicle in Vehicles.m_Vehicles)
			{
				if (vehicle.gameObject.activeInHierarchy && vehicle.IsUpsideDown() && vehicle.m_Stub.m_DisplayNameLocID != "VEHICLE_BIG_WHEEL_BUGGY" && levelID != "087" && levelID != "225")
				{
					UnlockAchievement(GameAchievement.Fun_MeantToDoThat);
					break;
				}
			}
			if (GameStateSim.m_NumBridgeBreaks == 1)
			{
				UnlockAchievement(GameAchievement.Fun_TisButAScratch);
			}
			if (!m_LevelsFailed.Contains(levelID))
			{
				UnlockAchievement(GameAchievement.Fun_FirstTry);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught Excpetion in MaybeUnlockAchivements: " + ex.Message);
		}
	}

	public static void UnlockAchievement(GameAchievement achievement)
	{
		if (HasUnlocked(achievement))
		{
			return;
		}
		if (!GameManager.IsSteamOffline())
		{
			SteamAchievements.UnlockAchievement((int)achievement);
		}
		if (!m_LocalAchievementIDsUnlockedList.ContainsKey((int)achievement))
		{
			m_LocalAchievementIDsUnlockedList.Add((int)achievement, DateTime.Now);
			if (GameManager.IsSteamOffline())
			{
				GameUI.m_Instance.m_AchievementPopup.ShowAchievementPopup((int)achievement);
			}
			SaveLocalAchivements();
		}
	}

	public static void SetProgressStat(string name, int value)
	{
		if (!GameManager.IsSteamOffline())
		{
			SteamStats.SetStat(name, value);
			SteamStats.SendStatsToServer();
		}
		if (!m_LocalAchievementStats.ContainsKey(name))
		{
			m_LocalAchievementStats.Add(name, value);
		}
		else
		{
			m_LocalAchievementStats[name] = value;
		}
	}

	public static int GetProgressStat(string name)
	{
		if (!GameManager.IsSteamOffline())
		{
			try
			{
				return SteamStats.GetStat(name);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Caught exception in GetProgressStat(): " + ex.Message);
				return 0;
			}
		}
		if (m_LocalAchievementStats.ContainsKey(name))
		{
			return m_LocalAchievementStats[name];
		}
		return 0;
	}

	public static void DoEndLevelActions(GameMode gameMode, string levelID, bool levelPassed, bool levelFailed)
	{
		if (!Game.BlockScoreUploadAndAchivementStats())
		{
			if (gameMode == GameMode.CAMPAIGN && levelPassed)
			{
				UpdateLevelCompletionStats(levelID);
			}
			MaybeUnlockAchievements(gameMode, levelID, levelPassed, levelFailed);
		}
	}

	public static void GetProgressValues(int achID, out int progressValue, out int totalValue)
	{
		progressValue = 0;
		totalValue = 0;
		CampaignWorld[] worlds = CampaignWorlds.m_Instance.m_Worlds;
		foreach (CampaignWorld campaignWorld in worlds)
		{
			if (campaignWorld.m_BeatWorldAchievement == (GameAchievement)achID)
			{
				progressValue = GetProgressStat(GetStatName((GameAchievement)achID));
				totalValue = CampaignWorlds.m_Instance.GetNumLevelsInWorld(campaignWorld.m_Id);
			}
		}
	}

	public static DateTime GetUnlockTime(int achID)
	{
		if (!GameManager.IsSteamOffline())
		{
			return SteamAchievements.GetUnlockTime(achID);
		}
		if (!m_LocalAchievementIDsUnlockedList.ContainsKey(achID))
		{
			return DateTime.MinValue;
		}
		return m_LocalAchievementIDsUnlockedList[achID];
	}

	public static bool HasUnlocked(GameAchievement achievement)
	{
		if (!GameManager.IsSteamOffline())
		{
			return SteamAchievements.HasUnlocked((int)achievement);
		}
		return m_LocalAchievementIDsUnlockedList.ContainsKey((int)achievement);
	}

	public static void SaveLocalAchivements()
	{
		string profileRootDirectory = Profiles.GetProfileRootDirectory();
		if (!Directory.Exists(profileRootDirectory))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(new GameAchievementsProxy(), DataFormat.JSON);
			if (array.Length != 0)
			{
				Utils.WriteBytesWithBackup(profileRootDirectory, GAME_LOCAL_ACHIEVEMENTS_FILENAME, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write progress to: '{1}'", ex.Message, Path.Combine(profileRootDirectory, GAME_LOCAL_ACHIEVEMENTS_FILENAME));
		}
	}

	private static bool LoadLocalAchievements()
	{
		string profileRootDirectory = Profiles.GetProfileRootDirectory();
		if (!Directory.Exists(profileRootDirectory))
		{
			return false;
		}
		try
		{
			string path = Path.Combine(profileRootDirectory, GAME_LOCAL_ACHIEVEMENTS_FILENAME);
			if (File.Exists(path))
			{
				byte[] array = File.ReadAllBytes(path);
				if (array != null && array.Length != 0)
				{
					GameAchievementsProxy gameAchievementsProxy = SerializationUtility.DeserializeValue<GameAchievementsProxy>(array, DataFormat.JSON);
					if (gameAchievementsProxy.m_LocalAchievementIDsUnlockedList != null)
					{
						m_LocalAchievementIDsUnlockedList = new Dictionary<int, DateTime>(gameAchievementsProxy.m_LocalAchievementIDsUnlockedList);
					}
					if (gameAchievementsProxy.m_LocalAchievementStats != null)
					{
						m_LocalAchievementStats = new Dictionary<string, int>(gameAchievementsProxy.m_LocalAchievementStats);
					}
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception loading local achivements: {0}", ex.Message.ToString());
		}
		return false;
	}

	public static void SaveFailedSimChecksums()
	{
		string profileRootDirectory = Profiles.GetProfileRootDirectory();
		if (!Directory.Exists(profileRootDirectory))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(m_FailedSimChecksums, DataFormat.JSON);
			if (array.Length != 0)
			{
				Utils.WriteBytesWithBackup(profileRootDirectory, FAILED_SIM_CHECKSUMS_FILENAME, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write failed checksums to: '{1}'", ex.Message, Path.Combine(profileRootDirectory, FAILED_SIM_CHECKSUMS_FILENAME));
		}
	}

	private static bool LoadFailedSimChecksums()
	{
		string profileRootDirectory = Profiles.GetProfileRootDirectory();
		if (!Directory.Exists(profileRootDirectory))
		{
			return false;
		}
		try
		{
			string path = Path.Combine(profileRootDirectory, FAILED_SIM_CHECKSUMS_FILENAME);
			if (File.Exists(path))
			{
				byte[] array = File.ReadAllBytes(path);
				if (array != null && array.Length != 0)
				{
					m_FailedSimChecksums = SerializationUtility.DeserializeValue<Dictionary<string, HashSet<string>>>(array, DataFormat.JSON);
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception reading failed checksums: {0}", ex.Message.ToString());
		}
		return false;
	}

	public static void SaveFailedLevels()
	{
		string profileRootDirectory = Profiles.GetProfileRootDirectory();
		if (!Directory.Exists(profileRootDirectory))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(m_LevelsFailed, DataFormat.JSON);
			if (array.Length != 0)
			{
				Utils.WriteBytesWithBackup(profileRootDirectory, FAILED_LEVELS_FILENAME, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write failed checksums to: '{1}'", ex.Message, Path.Combine(profileRootDirectory, FAILED_LEVELS_FILENAME));
		}
	}

	private static bool LoadFailedLevels()
	{
		string profileRootDirectory = Profiles.GetProfileRootDirectory();
		if (!Directory.Exists(profileRootDirectory))
		{
			return false;
		}
		try
		{
			string path = Path.Combine(profileRootDirectory, FAILED_LEVELS_FILENAME);
			if (File.Exists(path))
			{
				byte[] array = File.ReadAllBytes(path);
				if (array != null && array.Length != 0 && array[0] != 0)
				{
					m_LevelsFailed = SerializationUtility.DeserializeValue<HashSet<string>>(array, DataFormat.JSON);
					return true;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception reading failed levels: {0}", ex.Message.ToString());
		}
		return false;
	}

	public static string GetStatName(GameAchievement achievement)
	{
		string text = Enum.GetName(typeof(GameAchievement), (int)achievement) + "_Stat";
		if (!text.StartsWith("BeatWorld"))
		{
			return string.Empty;
		}
		return text;
	}

	public static void StartSpeedRunnerTimer()
	{
		m_SpeedRunnerStartTime = Time.realtimeSinceStartup;
	}

	public static void InvalidateSpeedRunnerTimer()
	{
		m_SpeedRunnerStartTime = -1.7014117E+38f;
	}

	private static void UpdateLevelCompletionStats(string levelID)
	{
		CampaignWorld worldWithLevelId = CampaignWorlds.m_Instance.GetWorldWithLevelId(levelID);
		if (!(worldWithLevelId == null))
		{
			int num = 0;
			CampaignLevel[] levels = worldWithLevelId.m_Levels;
			foreach (CampaignLevel campaignLevel in levels)
			{
				num += (Campaign.m_CampaignProgress.HasCompletedLevel(campaignLevel.m_Id) ? 1 : 0);
			}
			string statName = GetStatName(worldWithLevelId.m_BeatWorldAchievement);
			if (num != GetProgressStat(statName))
			{
				SetProgressStat(statName, num);
			}
		}
	}

	private static void MaybeUnlockSpeedRunner(string levelID)
	{
		if (!(levelID != "011") && Time.realtimeSinceStartup - m_SpeedRunnerStartTime < (float)SPEEDRUNNER_THRESHOLD_SECONDS)
		{
			UnlockAchievement(GameAchievement.Fun_SpeedRunner);
		}
	}
}
