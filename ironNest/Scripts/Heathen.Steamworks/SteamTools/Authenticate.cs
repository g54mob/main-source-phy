using System;
using System.Threading.Tasks;
using Heathen.SteamworksIntegration;
using Steamworks;

namespace SteamTools
{
	public static class Authenticate
	{
		public static void BeginSession(UserData user, byte[] ticket, BeginSessionResult callback)
		{
		}

		public static Task<(EBeginAuthSessionResult, AuthenticationSession)> BeginSessionTask(UserData user, byte[] ticket)
		{
			return null;
		}

		public static void EndSession(UserData user)
		{
		}

		public static void EndAllSessions()
		{
		}

		public static void SendToRpcWhenReady(ulong serverId, SendGameServerAuthentication serverRpcDelegate, Action<AuthenticationTicket, EResult> onResult)
		{
		}

		public static Task<(AuthenticationTicket, EResult)> SendToRpcWhenReadyTask(ulong serverId, SendGameServerAuthentication serverRpcDelegate)
		{
			return null;
		}

		public static void SendToLobbyOwnerWhenReady(LobbyData lobby, Action<AuthenticationTicket, EResult> onResult)
		{
		}

		public static Task<(AuthenticationTicket, EResult)> SendToLobbyOwnerWhenReadyTask(LobbyData lobby)
		{
			return null;
		}

		public static void DiscordConnectProvisional()
		{
		}
	}
}
