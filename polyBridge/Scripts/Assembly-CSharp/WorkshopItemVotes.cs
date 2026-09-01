using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class WorkshopItemVotes
{
	public static Dictionary<string, WorkshopItemVoteType> m_Votes = new Dictionary<string, WorkshopItemVoteType>();

	private static readonly string WORKSHOP_ITEM_VOTES_FILENAME = ".workshopitemvotes";

	public static void Init()
	{
		Load();
	}

	public static void Load()
	{
		string fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), WORKSHOP_ITEM_VOTES_FILENAME);
		if (!Utils.FileExists(fullPath))
		{
			return;
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array != null && array.Length != 0)
		{
			try
			{
				m_Votes = SerializationUtility.DeserializeValue<Dictionary<string, WorkshopItemVoteType>>(array, DataFormat.Binary);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception parsing " + WORKSHOP_ITEM_VOTES_FILENAME + ": " + ex.Message);
			}
		}
	}

	public static void Save()
	{
		byte[] bytes = SerializationUtility.SerializeValue(m_Votes, DataFormat.Binary);
		Utils.WriteBytes(Path.Combine(Profiles.GetProfileRootDirectory(), WORKSHOP_ITEM_VOTES_FILENAME), bytes);
	}
}
