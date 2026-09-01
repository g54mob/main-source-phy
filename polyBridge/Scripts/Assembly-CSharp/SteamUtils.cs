using System;
using System.Collections.Generic;
using Steamworks;

public class SteamUtils
{
	public static readonly int MAX_ITEMS_PER_PAGE = 50;

	public static string GetSteamId()
	{
		if (SteamManager.IsLoggedOn())
		{
			return SteamClient.SteamId.ToString();
		}
		return string.Empty;
	}

	public static ulong GetSteamIdAsUlong()
	{
		if (SteamManager.IsLoggedOn())
		{
			return SteamClient.SteamId;
		}
		return 0uL;
	}

	public static string GetLocalSteamDisplayName()
	{
		if (SteamManager.IsLoggedOn())
		{
			return SteamClient.Name;
		}
		return string.Empty;
	}

	public static List<string> GetFriendSteamIds()
	{
		if (SteamManager.IsLoggedOn())
		{
			List<string> list = new List<string>();
			{
				foreach (Friend friend in SteamFriends.GetFriends())
				{
					SteamId id = friend.Id;
					list.Add(id.ToString());
				}
				return list;
			}
		}
		return null;
	}

	public static void OpenWorkshopAgreementOverlay()
	{
		MaybeOpenOverlayURL("http://steamcommunity.com/sharedfiles/workshoplegalagreement");
	}

	public static bool IsRunningOnSteamDeck()
	{
		try
		{
			return Steamworks.SteamUtils.IsRunningOnSteamDeck;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static bool IsSteamInBigPictureMode()
	{
		try
		{
			return Steamworks.SteamUtils.IsSteamInBigPictureMode;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static void MaybeOpenOverlayURL(string url)
	{
		SteamFriends.OpenWebOverlay(url);
	}

	public static SteamId SteamIdFromString(string id)
	{
		if (ulong.TryParse(id, out var result))
		{
			return new SteamId
			{
				Value = result
			};
		}
		return default(SteamId);
	}
}
