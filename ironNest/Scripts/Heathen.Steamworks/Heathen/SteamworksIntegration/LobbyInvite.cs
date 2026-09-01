using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyInvite : IEquatable<LobbyInvite>, IComparable<LobbyInvite>
	{
		public LobbyInvite_t Data;

		public readonly UserData FromUser => default(UserData);

		public readonly LobbyData ToLobby => default(LobbyData);

		public readonly GameData ForGame => default(GameData);

		public static implicit operator LobbyInvite(LobbyInvite_t native)
		{
			return default(LobbyInvite);
		}

		public static implicit operator LobbyInvite_t(LobbyInvite heathen)
		{
			return default(LobbyInvite_t);
		}

		public readonly bool Equals(LobbyInvite other)
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

		public int CompareTo(LobbyInvite other)
		{
			return 0;
		}

		public static bool operator ==(LobbyInvite left, LobbyInvite right)
		{
			return false;
		}

		public static bool operator !=(LobbyInvite left, LobbyInvite right)
		{
			return false;
		}

		public static bool operator <(LobbyInvite left, LobbyInvite right)
		{
			return false;
		}

		public static bool operator >(LobbyInvite left, LobbyInvite right)
		{
			return false;
		}

		public static bool operator <=(LobbyInvite left, LobbyInvite right)
		{
			return false;
		}

		public static bool operator >=(LobbyInvite left, LobbyInvite right)
		{
			return false;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
