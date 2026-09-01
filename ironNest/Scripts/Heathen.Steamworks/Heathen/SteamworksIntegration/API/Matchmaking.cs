using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Matchmaking
	{
		public static class Client
		{
			public static List<LobbyData> MemberOfLobbies;

			private static CallResult<LobbyCreated_t> _lobbyCreatedT;

			private static CallResult<LobbyMatchList_t> _lobbyMatchListT;

			private static CallResult<LobbyEnter_t> _lobbyEnterT2;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static LobbyData GetCommandLineConnectLobby()
			{
				return default(LobbyData);
			}

			public static void AddHistoryGame(AppId_t appID, uint ipAddress, ushort port, ushort queryPort, DateTime lastPlayedOnServer)
			{
			}

			public static void AddFavoriteGame(AppId_t appID, uint ipAddress, ushort port, ushort queryPort, DateTime lastPlayedOnServer)
			{
			}

			public static void AddHistoryGame(AppId_t appID, string ipAddress, ushort port, ushort queryPort, DateTime lastPlayedOnServer)
			{
			}

			public static void AddFavoriteGame(AppId_t appID, string ipAddress, ushort port, ushort queryPort, DateTime lastPlayedOnServer)
			{
			}

			public static void AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter distanceFilter)
			{
			}

			public static void AddRequestLobbyListFilterSlotsAvailable(int slotsAvailable)
			{
			}

			public static void AddRequestLobbyListNearValueFilter(string key, int value)
			{
			}

			public static void AddRequestLobbyListNumericalFilter(string key, int value, ELobbyComparison comparison)
			{
			}

			public static void AddRequestLobbyListResultCountFilter(int max)
			{
			}

			public static void AddRequestLobbyListStringFilter(string key, string value, ELobbyComparison comparison)
			{
			}

			public static void CreateLobby(ELobbyType type, SteamLobbyModeType mode, int maxMembers, Action<EResult, LobbyData, bool> callback)
			{
			}

			public static Task<(EResult, LobbyData, bool)> CreateLobbyTask(ELobbyType type, SteamLobbyModeType mode, int maxMembers)
			{
				return null;
			}

			public static bool DeleteLobbyData(LobbyData lobby, string key)
			{
				return false;
			}

			public static FavoriteGame? GetFavoriteGame(int index)
			{
				return null;
			}

			public static FavoriteGame[] GetFavoriteGames()
			{
				return null;
			}

			public static int GetFavoriteGameCount()
			{
				return 0;
			}

			public static string GetLobbyData(LobbyData lobby, string key)
			{
				return null;
			}

			public static Dictionary<string, string> GetLobbyData(LobbyData lobby)
			{
				return null;
			}

			public static LobbyGameServer GetLobbyGameServer(LobbyData lobby)
			{
				return default(LobbyGameServer);
			}

			public static LobbyMemberData[] GetLobbyMembers(LobbyData lobby)
			{
				return null;
			}

			public static int GetLobbyMemberLimit(LobbyData lobby)
			{
				return 0;
			}

			public static CSteamID GetLobbyOwner(LobbyData lobby)
			{
				return default(CSteamID);
			}

			public static bool InviteUserToLobby(LobbyData lobby, UserData user)
			{
				return false;
			}

			public static void JoinLobby(LobbyData lobby, Action<LobbyEnter, bool> callback)
			{
			}

			public static Task<(LobbyEnter, bool)> JoinLobbyTask(LobbyData lobby)
			{
				return null;
			}

			public static void LeaveLobby(LobbyData lobby)
			{
			}

			public static bool RemoveFavoriteGame(AppId_t appId, uint ip, ushort connectionPort, ushort queryPort)
			{
				return false;
			}

			public static bool RemoveHistoryGame(AppId_t appId, uint ip, ushort connectionPort, ushort queryPort)
			{
				return false;
			}

			public static bool RemoveFavoriteGame(AppId_t appId, string ip, ushort connectionPort, ushort queryPort)
			{
				return false;
			}

			public static bool RemoveHistoryGame(AppId_t appId, string ip, ushort connectionPort, ushort queryPort)
			{
				return false;
			}

			public static bool RequestLobbyData(LobbyData lobby)
			{
				return false;
			}

			public static void RequestLobbyList(Action<LobbyData[], bool> callback)
			{
			}

			public static Task<(LobbyData[], bool)> RequestLobbyListTask()
			{
				return null;
			}

			public static bool SendLobbyChatMsg(LobbyData lobby, byte[] messageBody)
			{
				return false;
			}

			public static bool SetLobbyData(LobbyData lobby, string key, string value)
			{
				return false;
			}

			public static void SetLobbyGameServer(LobbyData lobby, uint ip, ushort port, CSteamID gameServerId)
			{
			}

			public static void SetLobbyGameServer(LobbyData lobby, string ip, ushort port, CSteamID gameServerId)
			{
			}

			public static bool SetLobbyJoinable(LobbyData lobby, bool joinable)
			{
				return false;
			}

			public static string GetLobbyMemberData(LobbyData lobby, CSteamID member, string key)
			{
				return null;
			}

			public static bool GetMember(LobbyData lobby, CSteamID id, out LobbyMemberData member)
			{
				member = default(LobbyMemberData);
				return false;
			}

			public static bool IsAMember(LobbyData lobby, CSteamID id)
			{
				return false;
			}

			public static void SetLobbyMemberData(LobbyData lobby, string key, string value)
			{
			}

			public static bool SetLobbyMemberLimit(LobbyData lobby, int maxMembers)
			{
				return false;
			}

			public static bool SetLobbyOwner(LobbyData lobby, CSteamID newOwner)
			{
				return false;
			}

			public static bool SetLobbyType(LobbyData lobby, ELobbyType type)
			{
				return false;
			}

			public static void CancelQuery(HServerListRequest request)
			{
			}

			public static void CancelServerQuery(HServerQuery query)
			{
			}

			public static int GetServerCount(HServerListRequest request)
			{
				return 0;
			}

			public static gameserveritem_t GetServerDetails(HServerListRequest request, int index)
			{
				return null;
			}

			public static gameserveritem_t[] GetServerDetails(HServerListRequest request)
			{
				return null;
			}

			public static bool IsRefreshing(HServerListRequest request)
			{
				return false;
			}

			public static HServerQuery PingServer(uint ip, ushort port, ISteamMatchmakingPingResponse response)
			{
				return default(HServerQuery);
			}

			public static HServerQuery PingServer(string ip, ushort port, ISteamMatchmakingPingResponse response)
			{
				return default(HServerQuery);
			}

			public static HServerQuery PlayerDetails(uint ip, ushort port, ISteamMatchmakingPlayersResponse response)
			{
				return default(HServerQuery);
			}

			public static HServerQuery PlayerDetails(string ip, ushort port, ISteamMatchmakingPlayersResponse response)
			{
				return default(HServerQuery);
			}

			public static void RefreshQuery(HServerListRequest request)
			{
			}

			public static void RefreshServer(HServerListRequest request, int index)
			{
			}

			public static void ReleaseRequest(HServerListRequest request)
			{
			}

			public static HServerListRequest RequestFavoritesServerList(AppId_t appId, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse pRequestServersResponse)
			{
				return default(HServerListRequest);
			}

			public static HServerListRequest RequestFriendsServerList(AppId_t appId, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse pRequestServersResponse)
			{
				return default(HServerListRequest);
			}

			public static HServerListRequest RequestHistoryServerList(AppId_t appId, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse pRequestServersResponse)
			{
				return default(HServerListRequest);
			}

			public static HServerListRequest RequestInternetServerList(AppId_t appId, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse pRequestServersResponse)
			{
				return default(HServerListRequest);
			}

			public static HServerListRequest RequestLanServerList(AppId_t appId, ISteamMatchmakingServerListResponse pRequestServersResponse)
			{
				return default(HServerListRequest);
			}

			public static HServerListRequest RequestSpectatorServerList(AppId_t appId, MatchMakingKeyValuePair_t[] filters, ISteamMatchmakingServerListResponse pRequestServersResponse)
			{
				return default(HServerListRequest);
			}

			public static HServerQuery ServerRules(uint ip, ushort port, ISteamMatchmakingRulesResponse response)
			{
				return default(HServerQuery);
			}

			public static HServerQuery ServerRules(string ip, ushort port, ISteamMatchmakingRulesResponse response)
			{
				return default(HServerQuery);
			}

			public static void LeaveAllLobbies()
			{
			}
		}
	}
}
