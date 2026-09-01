using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using Steamworks.Ugc;
using UnityEngine;

public class WorkshopItemFavorites
{
	public static HashSet<ulong> m_Favorites = new HashSet<ulong>();

	private static readonly string WORKSHOP_ITEM_FAVORITES_FILENAME = ".workshopitemfavorites";

	public static void Init()
	{
		Load();
	}

	public static void Load()
	{
		string fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), WORKSHOP_ITEM_FAVORITES_FILENAME);
		if (Utils.FileExists(fullPath))
		{
			byte[] array = Utils.ReadAllBytes(fullPath);
			if (array != null && array.Length != 0)
			{
				try
				{
					m_Favorites = SerializationUtility.DeserializeValue<HashSet<ulong>>(array, DataFormat.Binary);
				}
				catch (Exception ex)
				{
					Debug.LogWarning("Exception parsing " + WORKSHOP_ITEM_FAVORITES_FILENAME + ": " + ex.Message);
				}
			}
		}
		if (SteamManager.IsLoggedOn())
		{
			DownloadFavoritesFromSteam();
		}
	}

	public static void Save()
	{
		byte[] bytes = SerializationUtility.SerializeValue(m_Favorites, DataFormat.Binary);
		Utils.WriteBytes(Path.Combine(Profiles.GetProfileRootDirectory(), WORKSHOP_ITEM_FAVORITES_FILENAME), bytes);
	}

	private static async void DownloadFavoritesFromSteam()
	{
		m_Favorites.Clear();
		int i = 1;
		while (true)
		{
			ResultPage? resultPage = await Query.All.WithOnlyIDs(b: true).RankedByPublicationDate().WhereUserFavorited()
				.GetPageAsync(i);
			if (!resultPage.HasValue || resultPage.Value.ResultCount == 0)
			{
				break;
			}
			foreach (Item entry in resultPage.Value.Entries)
			{
				m_Favorites.Add(entry.Id);
			}
			i++;
		}
	}
}
