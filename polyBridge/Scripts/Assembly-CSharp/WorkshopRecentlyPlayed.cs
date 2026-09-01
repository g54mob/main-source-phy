using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class WorkshopRecentlyPlayed
{
	public static List<string> m_Levels = new List<string>();

	public static List<string> m_Campaigns = new List<string>();

	private static readonly string LEVELS_FILENAME = ".rplevels";

	private static readonly string CAMPAIGNS_FILENAME = ".rpcampaigns";

	private static readonly int MAX_RECENTLY_PLAYTED_ITEMS = 500;

	public static void SaveLevel(string id)
	{
		if (m_Levels.Contains(id))
		{
			m_Levels.Remove(id);
		}
		m_Levels.Insert(0, id);
		if (m_Levels.Count > MAX_RECENTLY_PLAYTED_ITEMS)
		{
			m_Levels.RemoveAt(m_Levels.Count - 1);
		}
		Save(m_Levels, LEVELS_FILENAME);
	}

	public static void SaveCampaign(string id)
	{
		if (m_Campaigns.Contains(id))
		{
			m_Campaigns.Remove(id);
		}
		m_Campaigns.Insert(0, id);
		if (m_Campaigns.Count > MAX_RECENTLY_PLAYTED_ITEMS)
		{
			m_Campaigns.RemoveAt(m_Campaigns.Count - 1);
		}
		Save(m_Campaigns, CAMPAIGNS_FILENAME);
	}

	public static void Load()
	{
		string profileDirectory = Profiles.GetProfileDirectory(Profiles.GetActiveProfileName());
		if (Directory.Exists(profileDirectory))
		{
			m_Levels = TryLoad(Path.Combine(profileDirectory, LEVELS_FILENAME));
			m_Campaigns = TryLoad(Path.Combine(profileDirectory, CAMPAIGNS_FILENAME));
		}
	}

	public static void ForgetItem(string id)
	{
		if (m_Levels.Contains(id))
		{
			m_Levels.Remove(id);
			Save(m_Levels, LEVELS_FILENAME);
		}
		else if (m_Campaigns.Contains(id))
		{
			m_Campaigns.Remove(id);
			Save(m_Campaigns, CAMPAIGNS_FILENAME);
		}
	}

	private static List<string> TryLoad(string filepath)
	{
		try
		{
			if (File.Exists(filepath))
			{
				byte[] array = File.ReadAllBytes(filepath);
				if (array != null && array.Length != 0 && array[0] != 0)
				{
					return SerializationUtility.DeserializeValue<List<string>>(array, DataFormat.JSON);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception reading: {0}", ex.Message.ToString());
		}
		return new List<string>();
	}

	private static void Save(List<string> ids, string filename)
	{
		string profileDirectory = Profiles.GetProfileDirectory(Profiles.GetActiveProfileName());
		Utils.CreateDirectory(profileDirectory);
		if (!Directory.Exists(profileDirectory))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(ids, DataFormat.JSON);
			if (array != null && array.Length != 0 && array[0] != 0)
			{
				Utils.WriteBytesWithBackup(profileDirectory, filename, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write to: '{1}'", ex.Message, Path.Combine(profileDirectory, filename));
		}
	}
}
