using System.Net;
using UnityEngine;

public static class BesiegeEntryPointHelper
{
	public static void JoinGameServer(ulong serverId)
	{
		Arguments args = new Arguments(new string[2]
		{
			"+connect_server",
			serverId.ToString()
		});
		TransportNetworkManager transportNetworkManager = Object.FindObjectOfType<TransportNetworkManager>();
		if (transportNetworkManager == null)
		{
			BesiegeEntryPoint.CreateEntryPoint(args);
			return;
		}
		if (serverId == transportNetworkManager.ServerID())
		{
			if (transportNetworkManager.ClientState == ClientConnectionState.Connected)
			{
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("[JoinGameServer] Already joined this server.");
				}
				return;
			}
			if (transportNetworkManager.ServerState != ServerConnectionState.Disconnected)
			{
				if (BesiegeLogFilter.logDebug)
				{
					Debug.Log("[JoinGameServer] Not joining my own server.");
				}
				return;
			}
		}
		BesiegeEntryPoint.CreateEntryPoint(args);
	}

	public static void JoinGameServer(string connectString)
	{
		Arguments args = new Arguments(connectString.Split(' '));
		TransportNetworkManager transportNetworkManager = Object.FindObjectOfType<TransportNetworkManager>();
		if (transportNetworkManager == null)
		{
			BesiegeEntryPoint.CreateEntryPoint(args);
			return;
		}
		IPEndPoint iPEndPoint = BesiegeArgumentsHelper.ParseIPPort(args);
		if (iPEndPoint == null)
		{
			Debug.LogError("[JoinGameServer] Could not parse a host to join, connectString: " + connectString);
			return;
		}
		if (iPEndPoint.Address.ToString() == transportNetworkManager.ConnectAddress && iPEndPoint.Port == transportNetworkManager.ConnectPort)
		{
			if (transportNetworkManager.ClientState == ClientConnectionState.Connected)
			{
				Debug.Log("[JoinGameServer] Already joined this server.");
				return;
			}
			if (transportNetworkManager.ServerState != ServerConnectionState.Disconnected)
			{
				Debug.Log("[JoinGameServer] Not joining my own server.");
				return;
			}
		}
		BesiegeEntryPoint.CreateEntryPoint(args);
	}

	public static void JoinGameLobby(ulong lobbyID)
	{
		Arguments args = new Arguments(new string[2]
		{
			"+connect_lobby",
			lobbyID.ToString()
		});
		TransportNetworkManager transportNetworkManager = Object.FindObjectOfType<TransportNetworkManager>();
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("[JoinGameLobby] Clicked on a friend to join lobby: " + lobbyID);
		}
		if (transportNetworkManager == null)
		{
			BesiegeEntryPoint.CreateEntryPoint(args);
		}
		else if (lobbyID != transportNetworkManager.LobbyID())
		{
			BesiegeEntryPoint.CreateEntryPoint(args);
		}
		else
		{
			Debug.Log("[JoinGameLobby] Already in this lobby, not joining...");
		}
	}

	public static void JoinPlayfabNetwork(string pfNetworkId)
	{
		Arguments args = new Arguments(new string[2] { "+pf_connect", pfNetworkId });
		TransportNetworkManager transportNetworkManager = Object.FindObjectOfType<TransportNetworkManager>();
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("[JoinPlayfabNetwork] Joining Playfab network ID: " + pfNetworkId);
		}
		if (transportNetworkManager == null)
		{
			BesiegeEntryPoint.CreateEntryPoint(args);
		}
		else if (!pfNetworkId.Equals(transportNetworkManager.CurrentNetwork))
		{
			BesiegeEntryPoint.CreateEntryPoint(args);
		}
		else
		{
			Debug.Log("[JoinPlayfabNetwork] Already in this server, not joining...");
		}
	}
}
