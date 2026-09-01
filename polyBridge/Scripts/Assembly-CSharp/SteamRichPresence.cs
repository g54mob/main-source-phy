using System;
using Steamworks;
using UnityEngine;

public class SteamRichPresence
{
	public static void SetCampaignLevel(string text)
	{
		try
		{
			if (SteamManager.IsLoggedOn())
			{
				SteamFriends.SetRichPresence("campaign_level", text);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("SteamRichPresence.SetCampaignLevel() caused exception: " + ex.Message);
		}
	}

	public static void Set(string text)
	{
		try
		{
			if (SteamManager.IsLoggedOn())
			{
				SteamFriends.SetRichPresence("steam_display", text);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("SteamRichPresence.Set() caused exception: " + ex.Message);
		}
	}

	public static void Clear()
	{
		try
		{
			if (SteamManager.IsLoggedOn())
			{
				SteamFriends.ClearRichPresence();
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("SteamRichPresence.Clear() caused exception: " + ex.Message);
		}
	}
}
