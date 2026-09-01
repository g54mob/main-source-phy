using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class CampaignProgressSerialize
{
	public static Dictionary<string, CampaignLevelState> LoadCampaignProgress(string profileName, string filename)
	{
		string profileDirectory = Profiles.GetProfileDirectory(profileName);
		if (!Directory.Exists(profileDirectory))
		{
			return null;
		}
		string text = Path.Combine(profileDirectory, filename);
		Dictionary<string, CampaignLevelState> dictionary = TryLoadCampaignProgress(text);
		if (dictionary == null)
		{
			text = Path.ChangeExtension(text, ".restore");
			dictionary = TryLoadCampaignProgress(text);
		}
		return dictionary;
	}

	public static void WriteCampaignProgress(string profileName, string filename, CampaignProgress progress)
	{
		string profileDirectory = Profiles.GetProfileDirectory(profileName);
		Utils.CreateDirectory(profileDirectory);
		if (!Directory.Exists(profileDirectory))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(progress.m_State, DataFormat.JSON);
			if (array != null && array.Length != 0 && array[0] != 0)
			{
				Utils.WriteBytesWithBackup(profileDirectory, filename, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write progress to: '{1}'", ex.Message, Path.Combine(profileDirectory, filename));
		}
	}

	private static Dictionary<string, CampaignLevelState> TryLoadCampaignProgress(string filepath)
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
