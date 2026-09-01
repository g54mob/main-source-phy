using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyData : IEquatable<CSteamID>, IEquatable<ulong>, IEquatable<LobbyData>
	{
		private ulong _id;

		public const string DataName = "name";

		public const string DataVersion = "z_heathenGameVersion";

		public const string DataReady = "z_heathenReady";

		public const string DataKick = "z_heathenKick";

		public const string DataMode = "z_heathenMode";

		public const string DataType = "z_heathenType";

		public const string DataSessionLobby = "z_heathenSessionLobby";

		public const string DataModeGeneral = "General";

		public const string DataModeSession = "Session";

		public const string DataModeParty = "Party";

		public readonly CSteamID SteamId => default(CSteamID);

		public readonly AccountID_t AccountId => default(AccountID_t);

		public readonly uint FriendId => 0u;

		public readonly string HexId => null;

		public readonly bool IsValid => false;

		public readonly string Name
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public readonly LobbyMemberData Owner
		{
			get
			{
				return default(LobbyMemberData);
			}
			set
			{
			}
		}

		public readonly LobbyMemberData Me => default(LobbyMemberData);

		public readonly LobbyMemberData[] Members => null;

		public readonly bool IsTypeSet => false;

		public readonly ELobbyType Type
		{
			get
			{
				return default(ELobbyType);
			}
			set
			{
			}
		}

		public readonly string GameVersion
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public readonly bool IsOwner => false;

		public readonly SteamLobbyModeType Mode
		{
			get
			{
				return default(SteamLobbyModeType);
			}
			set
			{
			}
		}

		public readonly bool IsParty => false;

		[Obsolete("Use IsParty instead")]
		public readonly bool IsGroup => false;

		public readonly bool IsSession => false;

		public readonly bool IsGeneral => false;

		public readonly bool HasServer => false;

		public readonly LobbyGameServer GameServer => default(LobbyGameServer);

		public readonly bool AllPlayersReady => false;

		public readonly bool AllPlayersNotReady => false;

		public readonly bool IsReady
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		public readonly bool Full => false;

		public readonly int MaxMembers
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public readonly int MemberCount => 0;

		public readonly string this[string metadataKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public readonly LobbyMemberData this[UserData user] => default(LobbyMemberData);

		public static List<LobbyData> MemberOfLobbies => null;

		public readonly bool GetMember(UserData user, out LobbyMemberData member)
		{
			member = default(LobbyMemberData);
			return false;
		}

		public readonly bool IsAMember(UserData id)
		{
			return false;
		}

		public readonly bool SetType(ELobbyType type)
		{
			return false;
		}

		public readonly bool SetJoinable(bool makeJoinable)
		{
			return false;
		}

		public readonly Dictionary<string, string> GetMetadata()
		{
			return null;
		}

		public static void Create(CreateArguments createArguments, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreateTask(CreateArguments createArguments)
		{
			return null;
		}

		public static void Create(ELobbyType type, SteamLobbyModeType mode, int slots, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreateTask(ELobbyType type, SteamLobbyModeType mode, int slots)
		{
			return null;
		}

		public static void CreateParty(int slots, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreatePartyTask(int slots)
		{
			return null;
		}

		public static void CreateSession(ELobbyType type, int slots, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreateSessionTask(ELobbyType type, int slots)
		{
			return null;
		}

		public static void CreatePublicSession(int slots, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreatePublicSessionTask(int slots)
		{
			return null;
		}

		public static void CreatePrivateSession(int slots, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreatePrivateSessionTask(int slots)
		{
			return null;
		}

		public static void CreateFriendOnlySession(int slots, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> CreateFriendOnlySessionTask(int slots)
		{
			return null;
		}

		public readonly void Join(Action<LobbyEnter, bool> callback)
		{
		}

		public readonly Task<(LobbyEnter, bool)> JoinTask()
		{
			return null;
		}

		public readonly void Leave()
		{
		}

		public readonly bool DeleteLobbyData(string dataKey)
		{
			return false;
		}

		public readonly bool InviteUserToLobby(UserData targetUser)
		{
			return false;
		}

		public readonly bool SendChatMessage(string message)
		{
			return false;
		}

		public readonly bool SendChatMessage(byte[] data)
		{
			return false;
		}

		public readonly bool SendChatMessage(object jsonObject)
		{
			return false;
		}

		public readonly void SetGameServer(string address, ushort port, CSteamID gameServerId)
		{
		}

		public readonly void SetGameServer(string address, ushort port)
		{
		}

		public readonly void SetGameServer(CSteamID gameServerId)
		{
		}

		public readonly void SetGameServer()
		{
		}

		public readonly bool KickMember(UserData memberId)
		{
			return false;
		}

		public readonly bool KickListContains(UserData memberId)
		{
			return false;
		}

		public readonly bool RemoveFromKickList(UserData memberId)
		{
			return false;
		}

		public readonly bool ClearKickList()
		{
			return false;
		}

		public readonly UserData[] GetKickList()
		{
			return null;
		}

		public readonly void SetMemberMetadata(string key, string value)
		{
		}

		public readonly void SetLobbyMetadata(string key, string value)
		{
		}

		public readonly string GetMemberMetadata(string key)
		{
			return null;
		}

		public readonly string GetMemberMetadata(UserData memberId, string key)
		{
			return null;
		}

		public readonly string GetMemberMetadata(LobbyMemberData member, string key)
		{
			return null;
		}

		public readonly bool RequestData()
		{
			return false;
		}

		public static LobbyData Get(string accountId)
		{
			return default(LobbyData);
		}

		public static LobbyData Get(uint accountId)
		{
			return default(LobbyData);
		}

		public static LobbyData Get(AccountID_t accountId)
		{
			return default(LobbyData);
		}

		public static LobbyData Get(ulong id)
		{
			return default(LobbyData);
		}

		public static LobbyData Get(CSteamID id)
		{
			return default(LobbyData);
		}

		public static bool PartyLobby(out LobbyData lobby)
		{
			lobby = default(LobbyData);
			return false;
		}

		[Obsolete("Use PartyLobby instead")]
		public static bool GroupLobby(out LobbyData lobby)
		{
			lobby = default(LobbyData);
			return false;
		}

		public static bool SessionLobby(out LobbyData lobby)
		{
			lobby = default(LobbyData);
			return false;
		}

		public static void Join(string accountId, Action<LobbyEnter, bool> callback)
		{
		}

		public static Task<(LobbyEnter, bool)> JoinTask(string accountId)
		{
			return null;
		}

		public static void Join(LobbyData lobby, Action<LobbyEnter, bool> callback)
		{
		}

		public static Task<(LobbyEnter, bool)> JoinTask(LobbyData lobby)
		{
			return null;
		}

		public static void Join(AccountID_t accountId, Action<LobbyEnter, bool> callback)
		{
		}

		public static Task<(LobbyEnter, bool)> JoinTask(AccountID_t accountId)
		{
			return null;
		}

		public static void Request(ELobbyDistanceFilter distanceFilter, int openSlotsRequired, int maxResultsToReturn, IEnumerable<StringFilter> stringFilters, IEnumerable<NearFilter> nearFilters, IEnumerable<NumericFilter> numericFilters, Action<LobbyData[], bool> callback)
		{
		}

		public static Task<(LobbyData[], bool)> RequestTask(ELobbyDistanceFilter distanceFilter, int openSlotsRequired, int maxResultsToReturn, IEnumerable<StringFilter> stringFilters, IEnumerable<NearFilter> nearFilters, IEnumerable<NumericFilter> numericFilters)
		{
			return null;
		}

		public static void Request(SearchArguments searchArguments, int maxResultsToReturn, Action<LobbyData[], bool> callback)
		{
		}

		public static Task<(LobbyData[], bool)> RequestTask(SearchArguments searchArguments, int maxResultsToReturn)
		{
			return null;
		}

		public static void QuickMatch(SearchArguments searchArguments, CreateArguments createArguments, Action<EResult, LobbyData, bool> callback)
		{
		}

		public static Task<(EResult, LobbyData, bool)> QuickMatchTask(SearchArguments searchArguments, CreateArguments createArguments)
		{
			return null;
		}

		public readonly void Authenticate(Action<AuthenticationTicket, bool> callback)
		{
		}

		public readonly Task<(AuthenticationTicket, bool)> AuthenticateTask()
		{
			return null;
		}

		public readonly bool Authenticate(LobbyMessagePayload data)
		{
			return false;
		}

		public int CompareTo(CSteamID other)
		{
			return 0;
		}

		public int CompareTo(ulong other)
		{
			return 0;
		}

		public override string ToString()
		{
			return null;
		}

		public bool Equals(CSteamID other)
		{
			return false;
		}

		public bool Equals(ulong other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(LobbyData other)
		{
			return false;
		}

		public static bool operator ==(LobbyData l, LobbyData r)
		{
			return false;
		}

		public static bool operator ==(CSteamID l, LobbyData r)
		{
			return false;
		}

		public static bool operator ==(LobbyData l, CSteamID r)
		{
			return false;
		}

		public static bool operator ==(LobbyData l, ulong r)
		{
			return false;
		}

		public static bool operator ==(ulong l, LobbyData r)
		{
			return false;
		}

		public static bool operator !=(LobbyData l, LobbyData r)
		{
			return false;
		}

		public static bool operator !=(CSteamID l, LobbyData r)
		{
			return false;
		}

		public static bool operator !=(LobbyData l, CSteamID r)
		{
			return false;
		}

		public static bool operator !=(LobbyData l, ulong r)
		{
			return false;
		}

		public static bool operator !=(ulong l, LobbyData r)
		{
			return false;
		}

		public static implicit operator CSteamID(LobbyData c)
		{
			return default(CSteamID);
		}

		public static implicit operator LobbyData(CSteamID id)
		{
			return default(LobbyData);
		}

		public static implicit operator ulong(LobbyData id)
		{
			return 0uL;
		}

		public static implicit operator LobbyData(ulong id)
		{
			return default(LobbyData);
		}

		public static implicit operator LobbyData(AccountID_t id)
		{
			return default(LobbyData);
		}

		public static implicit operator LobbyData(uint id)
		{
			return default(LobbyData);
		}

		public static implicit operator LobbyData(string id)
		{
			return default(LobbyData);
		}
	}
}
