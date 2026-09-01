using System;
using System.Threading.Tasks;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct UserData : IEquatable<CSteamID>, IEquatable<ulong>, IEquatable<UserData>
	{
		public CSteamID id;

		public static UserData Me => default(UserData);

		public static UserData[] MyFriends => null;

		public readonly bool IsMe => false;

		public readonly ulong SteamId => 0uL;

		public readonly bool IsValid => false;

		public readonly string Name => null;

		public readonly string Nickname => null;

		public readonly EPersonaState State => default(EPersonaState);

		public readonly bool InGame => false;

		public readonly bool InThisGame => false;

		public readonly FriendGameInfo GameInfo => default(FriendGameInfo);

		public readonly int Level => 0;

		public readonly AccountID_t AccountId => default(AccountID_t);

		public readonly uint FriendId => 0u;

		public readonly string HexId => null;

		public readonly string[] NameHistory => null;

		public readonly void LoadAvatar(Action<Texture2D> callback)
		{
		}

		public readonly Task<Texture2D> LoadAvatarTask()
		{
			return null;
		}

		public readonly bool GetGamePlayed(out FriendGameInfo gameInfo)
		{
			gameInfo = default(FriendGameInfo);
			return false;
		}

		public readonly void InviteToGame(string connectString)
		{
		}

		public readonly bool SendMessage(string message)
		{
			return false;
		}

		public readonly bool RequestInformation()
		{
			return false;
		}

		public readonly string GetRichPresenceValue(string key)
		{
			return null;
		}

		public readonly bool InviteToLobby(LobbyData lobby)
		{
			return false;
		}

		public readonly void AddFriend()
		{
		}

		public readonly void RemoveFriend()
		{
		}

		public readonly void SetPlayedWith()
		{
		}

		public readonly (bool, DateTime) GetAchievement(AchievementData achievement)
		{
			return default((bool, DateTime));
		}

		public readonly bool SetAchievement(AchievementData achievement)
		{
			return false;
		}

		public static void ClearRichPresence()
		{
		}

		public static UserData Get(string accountId)
		{
			return default(UserData);
		}

		public static UserData Get(ulong id)
		{
			return default(UserData);
		}

		public static UserData Get(CSteamID id)
		{
			return default(UserData);
		}

		public static UserData Get()
		{
			return default(UserData);
		}

		public static UserData Get(uint accountId)
		{
			return default(UserData);
		}

		public static UserData Get(AccountID_t accountId)
		{
			return default(UserData);
		}

		public static bool SetRichPresence(string key, string value)
		{
			return false;
		}

		public static bool AddFriend(string friendId)
		{
			return false;
		}

		public static void AddFriend(uint friendId)
		{
		}

		public static void AddFriend(UserData user)
		{
		}

		public static void AddFriend(AccountID_t user)
		{
		}

		public static void RemoveFriend(UserData user)
		{
		}

		public static void RemoveFriend(AccountID_t user)
		{
		}

		public readonly int CompareTo(UserData other)
		{
			return 0;
		}

		public readonly int CompareTo(CSteamID other)
		{
			return 0;
		}

		public readonly int CompareTo(ulong other)
		{
			return 0;
		}

		public override readonly string ToString()
		{
			return null;
		}

		public readonly bool Equals(UserData other)
		{
			return false;
		}

		public readonly bool Equals(CSteamID other)
		{
			return false;
		}

		public readonly bool Equals(ulong other)
		{
			return false;
		}

		public override readonly bool Equals(object obj)
		{
			return false;
		}

		public override readonly int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(UserData l, UserData r)
		{
			return false;
		}

		public static bool operator ==(CSteamID l, UserData r)
		{
			return false;
		}

		public static bool operator ==(UserData l, CSteamID r)
		{
			return false;
		}

		public static bool operator !=(UserData l, UserData r)
		{
			return false;
		}

		public static bool operator !=(CSteamID l, UserData r)
		{
			return false;
		}

		public static bool operator !=(UserData l, CSteamID r)
		{
			return false;
		}

		public static implicit operator ulong(UserData c)
		{
			return 0uL;
		}

		public static implicit operator UserData(ulong id)
		{
			return default(UserData);
		}

		public static implicit operator CSteamID(UserData c)
		{
			return default(CSteamID);
		}

		public static implicit operator UserData(CSteamID id)
		{
			return default(UserData);
		}
	}
}
