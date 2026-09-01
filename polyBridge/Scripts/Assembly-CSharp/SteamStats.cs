using System;
using Steamworks;
using UnityEngine;

public class SteamStats
{
	public static void Init()
	{
		SteamUserStats.OnUserStatsReceived += OnUserStatsReceived;
		SteamUserStats.OnUserStatsStored += OnUserStatsStored;
	}

	public static void SetStat(string name, int value)
	{
		try
		{
			if (SteamUserStats.SetStat(name, value))
			{
				Debug.Log("STEAM: Set stat: " + name);
			}
			else
			{
				Debug.LogError("STEAM: Failed to set stat: " + name);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in SteamStats.SetStat(): " + ex.Message);
		}
	}

	public static void IncrementStat(string name, int value)
	{
		try
		{
			if (SteamUserStats.AddStat(name, value))
			{
				Debug.Log("STEAM: Set stat: " + name);
			}
			else
			{
				Debug.LogError("STEAM: Failed to set stat: " + name);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in SteamStats.IncrementStat(): " + ex.Message);
		}
	}

	public static int GetStat(string name)
	{
		try
		{
			return SteamUserStats.GetStatInt(name);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in SteamStats.GetStat(): " + ex.Message);
			return 0;
		}
	}

	public static void SendStatsToServer()
	{
		try
		{
			SteamUserStats.StoreStats();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in SteamStats.SendStatsToServer(): " + ex.Message);
		}
	}

	private static void OnUserStatsReceived(SteamId steamId, Result result)
	{
		if ((ulong)SteamClient.SteamId == (ulong)steamId && result != Result.OK)
		{
			Debug.Log("RequestStats - failed, " + result);
		}
	}

	private static void OnUserStatsStored(Result result)
	{
		if (result != Result.OK)
		{
			Debug.Log("StoreStats - failed, " + result);
		}
	}
}
