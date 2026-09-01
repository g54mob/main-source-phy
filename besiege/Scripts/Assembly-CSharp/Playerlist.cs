using System.Collections.Generic;
using UnityEngine;

public static class Playerlist
{
	public static readonly List<PlayerData> Players = new List<PlayerData>();

	public static void AddPlayer(PlayerData playerData)
	{
		if (!Contains(playerData.networkId))
		{
			Players.Add(playerData);
		}
		else
		{
			Debug.LogError("Error: Extra player has been added - should not happen");
		}
	}

	public static void ClearPlayers()
	{
		Players.Clear();
	}

	public static void DeletePlayer(PlayerData playerData)
	{
		if (Players.Contains(playerData))
		{
			Players.Remove(playerData);
		}
	}

	public static bool GetPlayer(ushort networkId, out PlayerData player)
	{
		for (int i = 0; i < Players.Count; i++)
		{
			PlayerData playerData = Players[i];
			if (playerData.networkId == networkId)
			{
				player = playerData;
				return true;
			}
		}
		player = null;
		return false;
	}

	public static PlayerData GetPlayer(ushort networkId)
	{
		PlayerData player;
		if (!GetPlayer(networkId, out player))
		{
			return null;
		}
		return player;
	}

	public static bool Contains(ushort networkId)
	{
		for (int i = 0; i < Players.Count; i++)
		{
			if (Players[i].networkId == networkId)
			{
				return true;
			}
		}
		return false;
	}

	public static bool HasRemoteLocalSimulations()
	{
		for (int i = 0; i < Players.Count; i++)
		{
			if (!Players[i].isSpectator)
			{
				if (Players[i].machine.RemoteLocal)
				{
					return true;
				}
				if (!Players[i].isLocalPlayer && Players[i].machine.isSimulating && !StatMaster.Mode.LevelEditor.clientGlobalSim)
				{
					return true;
				}
			}
		}
		return false;
	}
}
