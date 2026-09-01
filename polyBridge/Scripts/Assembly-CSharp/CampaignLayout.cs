using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class CampaignLayout
{
	public static int CURRENT_VERSION = 1;

	public static string SAVE_DIRECTORY = "Sandbox";

	public static string SAVE_EXTENSION = ".campaign";

	public static CampaignLayoutData m_CurrentLayout;

	public static CampaignLayoutData Save(string filename, string id, string title, string description, string winMessage, List<string> itemIds)
	{
		CampaignLayoutData campaignLayoutData = new CampaignLayoutData(CURRENT_VERSION, filename, id, title, description, winMessage, itemIds);
		byte[] bytes = SerializationUtility.SerializeValue(campaignLayoutData, DataFormat.JSON);
		try
		{
			string savePath = GetSavePath();
			if (!Directory.Exists(savePath))
			{
				Directory.CreateDirectory(savePath);
			}
			Utils.WriteBytes(savePath, AddFileExtension(filename), bytes);
			return campaignLayoutData;
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught exception in CampaignLayout::Save {0}", ex.Message);
			return null;
		}
	}

	public static CampaignLayoutData Load(string path, string name)
	{
		if (!Directory.Exists(path))
		{
			return null;
		}
		string path2 = Path.Combine(path, AddFileExtension(name));
		if (!File.Exists(path2))
		{
			return null;
		}
		try
		{
			return SerializationUtility.DeserializeValue<CampaignLayoutData>(File.ReadAllBytes(path2), DataFormat.JSON);
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught exception in CampaignLayout::Load {0}", ex.Message);
			return null;
		}
	}

	public static string GetSavePath()
	{
		return Path.Combine(Application.persistentDataPath, SAVE_DIRECTORY);
	}

	public static string AddFileExtension(string filename)
	{
		if (Path.GetExtension(filename) == SAVE_EXTENSION)
		{
			return filename;
		}
		return filename + SAVE_EXTENSION;
	}
}
