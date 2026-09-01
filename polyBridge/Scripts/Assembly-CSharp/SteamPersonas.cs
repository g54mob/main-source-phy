using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamPersonas : MonoBehaviour
{
	public static Dictionary<SteamId, Friend> m_Personas = new Dictionary<SteamId, Friend>();

	public static void Add(SteamId steamId, Friend friend)
	{
		if (!m_Personas.ContainsKey(steamId))
		{
			m_Personas.Add(steamId, friend);
		}
	}

	public static string GetDisplayName(string steamIdAsString)
	{
		SteamId key = SteamUtils.SteamIdFromString(steamIdAsString);
		if (m_Personas.ContainsKey(key))
		{
			return m_Personas[key].Name;
		}
		return string.Empty;
	}

	public static string GetDisplayName(SteamId steamId)
	{
		if (m_Personas.ContainsKey(steamId))
		{
			return m_Personas[steamId].Name;
		}
		return string.Empty;
	}

	public static bool Exists(SteamId steamId)
	{
		return m_Personas.ContainsKey(steamId);
	}

	public static void RequestUserInfo(string steamIdAsString)
	{
		SteamId steamId = SteamUtils.SteamIdFromString(steamIdAsString);
		if (!Exists(steamId))
		{
			SteamFriends.RequestUserInformation(steamId);
			Friend friend = new Friend(steamId);
			Add(steamId, friend);
		}
	}
}
