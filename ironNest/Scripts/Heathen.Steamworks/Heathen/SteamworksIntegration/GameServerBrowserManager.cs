using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	public class GameServerBrowserManager : MonoBehaviour
	{
		[Serializable]
		public class ResultsEvent : UnityEvent<ResultData>
		{
		}

		[Serializable]
		public class ResultData
		{
			public GameServerSearchType type;

			public List<GameServerData> entries;

			public bool hasIOFailure;

			public ResultData(GameServerSearchType type, List<GameServerData> entries, bool ioFailure)
			{
			}
		}

		private class Search
		{
			public HServerListRequest HRequest;

			public Action<List<GameServerData>, bool> Callback;

			public Action Clear;

			public ISteamMatchmakingServerListResponse MServerListResponse;

			private void OnServerResponded(HServerListRequest hRequest, int iServer)
			{
			}

			private void OnServerFailedToRespond(HServerListRequest hRequest, int iServer)
			{
			}

			private void OnRefreshComplete(HServerListRequest hRequest, EMatchMakingServerResponse response)
			{
			}
		}

		private class PingQuery
		{
			public HServerQuery HQuery;

			public ISteamMatchmakingPingResponse MPingResponse;

			public GameServerData Target;

			public Action<GameServerData, bool> Callback;

			public Action Clear;

			private void OnServerFailedToRespondPing()
			{
			}

			private void OnServerRespondedPing(gameserveritem_t server)
			{
			}
		}

		private class PlayerQuery
		{
			public HServerQuery HQuery;

			public ISteamMatchmakingPlayersResponse MPlayersResponse;

			public GameServerData Target;

			public Action<GameServerData, bool> Callback;

			public Action Clear;

			private void OnPlayersRefreshComplete()
			{
			}

			private void OnPlayersFailedToRespond()
			{
			}

			private void OnAddPlayerToList(string pchName, int nScore, float flTimePlayed)
			{
			}
		}

		private class RulesQuery
		{
			public HServerQuery HQuery;

			public ISteamMatchmakingRulesResponse MRulesResponse;

			public GameServerData Target;

			public Action<GameServerData, bool> Callback;

			public Action Clear;

			private void OnAddRuleToList(string pchRule, string pchValue)
			{
			}

			private void OnRulesRefreshComplete()
			{
			}

			private void OnRulesFailedToRespond()
			{
			}
		}

		public class Filter : Dictionary<string, string>
		{
			public MatchMakingKeyValuePair_t[] Array => null;
		}

		private readonly List<Search> _searchList;

		private readonly List<PingQuery> _pingList;

		private readonly List<PlayerQuery> _playerList;

		private readonly List<RulesQuery> _ruleList;

		public ResultsEvent evtSearchCompleted;

		public void GetAllFavorites()
		{
		}

		public void GetAllFriends()
		{
		}

		public void GetAllHistory()
		{
		}

		public void GetAllInternet()
		{
		}

		public void GetAllLan()
		{
		}

		public void GetAllSpectator()
		{
		}

		public void GetFavorites(Filter filter)
		{
		}

		public void GetFriends(Filter filter)
		{
		}

		public void GetHistory(Filter filter)
		{
		}

		public void GetInternet(Filter filter)
		{
		}

		public void GetLan(Filter filter)
		{
		}

		public void GetSpectator(Filter filter)
		{
		}

		public void GetServerList(GameServerSearchType type, Action<List<GameServerData>, bool> callback = null, Filter filter = null)
		{
		}

		public void GetServerList(AppId_t appId, GameServerSearchType type, Action<List<GameServerData>, bool> callback = null, Filter filter = null)
		{
		}

		public void PingServer(string ipAddress, ushort port, Action<GameServerData, bool> callback)
		{
		}

		public void PingServer(uint ipAddress, ushort port, Action<GameServerData, bool> callback)
		{
		}

		public void PingServer(servernetadr_t address, Action<GameServerData, bool> callback)
		{
		}

		public void PingServer(GameServerData entry, Action<GameServerData, bool> callback)
		{
		}

		public void PlayerDetails(GameServerData entry, Action<GameServerData, bool> callback)
		{
		}

		public void ServerRules(GameServerData entry, Action<GameServerData, bool> callback)
		{
		}

		private void OnDestroy()
		{
		}
	}
}
