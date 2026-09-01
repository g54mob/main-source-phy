using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class WeeklyChallengesProgress
{
	private static CampaignProgress m_Progress = new CampaignProgress();

	private static readonly string WEEKLIES_PROGRESS_FILENAME = "weeklies.progress";

	public static bool Load()
	{
		Dictionary<string, CampaignLevelState> dictionary = LoadProgressFile(Profiles.GetActiveProfileName(), WEEKLIES_PROGRESS_FILENAME);
		if (dictionary == null || dictionary.Count == 0)
		{
			return false;
		}
		m_Progress.m_State = new Dictionary<string, CampaignLevelState>(dictionary);
		return true;
	}

	public static void UpdateProgress(string levelId, CampaignLevelStatus status)
	{
		m_Progress.MarkLevelAsCompleted(levelId, status);
		Save();
	}

	public static bool HasCompletedLevel(string levelId)
	{
		if (m_Progress.HasState(levelId))
		{
			return m_Progress.HasCompletedLevel(levelId);
		}
		string lowestBudgetFullPath = BridgeSaveSlots.GetLowestBudgetFullPath(levelId);
		string lowestBudgetNoBreaksFullPath = BridgeSaveSlots.GetLowestBudgetNoBreaksFullPath(levelId);
		if (!Utils.FileExists(lowestBudgetFullPath))
		{
			return Utils.FileExists(lowestBudgetNoBreaksFullPath);
		}
		return true;
	}

	public static bool HasCompletedLevelUnderBudget(string levelId, int budget)
	{
		if (m_Progress.HasState(levelId))
		{
			return m_Progress.HasCompletedLevelUnderBudget(levelId);
		}
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(BridgeSaveSlots.GetLowestBudgetFullPath(levelId));
		if (bridgeSaveSlotData != null && bridgeSaveSlotData.m_Budget <= budget)
		{
			return true;
		}
		BridgeSaveSlotData bridgeSaveSlotData2 = BridgeSaveSlots.Load(BridgeSaveSlots.GetLowestBudgetNoBreaksFullPath(levelId));
		if (bridgeSaveSlotData2 != null && bridgeSaveSlotData2.m_Budget <= budget)
		{
			return true;
		}
		return false;
	}

	public static bool HasCompletedLevelUnderBudgetNoBreaks(string levelId, int budget)
	{
		if (m_Progress.HasState(levelId))
		{
			return m_Progress.HasCompletedLevelUnderBudgetNoBreaks(levelId);
		}
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(BridgeSaveSlots.GetLowestBudgetNoBreaksFullPath(levelId));
		if (bridgeSaveSlotData != null && bridgeSaveSlotData.m_Budget <= budget)
		{
			return true;
		}
		return false;
	}

	public static void Clear()
	{
		m_Progress.m_State.Clear();
		Save();
	}

	private static void Save()
	{
		if (m_Progress.m_State == null || m_Progress.m_State.Count == 0)
		{
			return;
		}
		string profileDirectory = Profiles.GetProfileDirectory(Profiles.GetActiveProfileName());
		Utils.CreateDirectory(profileDirectory);
		if (!Directory.Exists(profileDirectory))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(m_Progress.m_State, DataFormat.JSON);
			if (array != null && array.Length != 0 && array[0] != 0)
			{
				Utils.WriteBytesWithBackup(profileDirectory, WEEKLIES_PROGRESS_FILENAME, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write progress to: '{1}'", ex.Message, Path.Combine(profileDirectory, WEEKLIES_PROGRESS_FILENAME));
		}
	}

	private static Dictionary<string, CampaignLevelState> LoadProgressFile(string profileName, string filename)
	{
		string profileDirectory = Profiles.GetProfileDirectory(profileName);
		if (!Directory.Exists(profileDirectory))
		{
			return null;
		}
		string text = Path.Combine(profileDirectory, filename);
		Dictionary<string, CampaignLevelState> dictionary = TryLoadPorgressFile(text);
		if (dictionary == null)
		{
			text = Path.ChangeExtension(text, ".restore");
			dictionary = TryLoadPorgressFile(text);
		}
		return dictionary;
	}

	private static Dictionary<string, CampaignLevelState> TryLoadPorgressFile(string filepath)
	{
		try
		{
			if (File.Exists(filepath))
			{
				byte[] array = File.ReadAllBytes(filepath);
				if (array != null && array.Length != 0 && array[0] != 0)
				{
					return SerializationUtility.DeserializeValue<Dictionary<string, CampaignLevelState>>(array, DataFormat.JSON);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception reading progress: {0}", ex.Message.ToString());
		}
		return null;
	}
}
