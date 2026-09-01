using System;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ClanData : IEquatable<CSteamID>, IEquatable<ClanData>, IEquatable<ulong>, IComparable<CSteamID>, IComparable<ClanData>, IComparable<ulong>
	{
		[SerializeField]
		private ulong id;

		public readonly CSteamID SteamId => default(CSteamID);

		public readonly AccountID_t AccountId => default(AccountID_t);

		public readonly uint FriendId => 0u;

		public readonly bool IsValid => false;

		public readonly Texture2D Icon => null;

		public readonly string Name => null;

		public readonly string Tag => null;

		public readonly UserData Owner => default(UserData);

		public readonly UserData[] Officers => null;

		public readonly int NumberOfMembersInChat => 0;

		public readonly UserData[] MembersInChat => null;

		public readonly bool IsOfficialGameGroup => false;

		public readonly bool IsPublic => false;

		public readonly bool IsUserOwner => false;

		public readonly bool IsUserOfficer => false;

		public static ClanData[] Get()
		{
			return null;
		}

		public static ClanData Get(uint accountId)
		{
			return default(ClanData);
		}

		public static ClanData Get(AccountID_t accountId)
		{
			return default(ClanData);
		}

		public static ClanData Get(ulong id)
		{
			return default(ClanData);
		}

		public static ClanData Get(CSteamID id)
		{
			return default(ClanData);
		}

		public readonly void JoinChat(Action<ChatRoom, bool> callback)
		{
		}

		public readonly void LoadIcon(Action<Texture2D> callback)
		{
		}

		public readonly int CompareTo(CSteamID other)
		{
			return 0;
		}

		public readonly int CompareTo(ClanData other)
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

		public readonly bool Equals(CSteamID other)
		{
			return false;
		}

		public readonly bool Equals(ClanData other)
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

		public static bool operator ==(ClanData l, ClanData r)
		{
			return false;
		}

		public static bool operator !=(ClanData l, ClanData r)
		{
			return false;
		}

		public static bool operator ==(ClanData l, CSteamID r)
		{
			return false;
		}

		public static bool operator !=(ClanData l, CSteamID r)
		{
			return false;
		}

		public static bool operator ==(CSteamID l, ClanData r)
		{
			return false;
		}

		public static bool operator !=(CSteamID l, ClanData r)
		{
			return false;
		}

		public static bool operator <(ClanData l, ClanData r)
		{
			return false;
		}

		public static bool operator >(ClanData l, ClanData r)
		{
			return false;
		}

		public static bool operator <=(ClanData l, ClanData r)
		{
			return false;
		}

		public static bool operator >=(ClanData l, ClanData r)
		{
			return false;
		}

		public static implicit operator CSteamID(ClanData c)
		{
			return default(CSteamID);
		}

		public static implicit operator ClanData(CSteamID id)
		{
			return default(ClanData);
		}

		public static implicit operator ulong(ClanData c)
		{
			return 0uL;
		}

		public static implicit operator ClanData(ulong id)
		{
			return default(ClanData);
		}
	}
}
