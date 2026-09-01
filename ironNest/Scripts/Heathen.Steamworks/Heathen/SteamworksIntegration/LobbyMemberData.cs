using System;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct LobbyMemberData : IEquatable<LobbyMemberData>
	{
		public LobbyData lobby;

		public UserData user;

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

		public readonly bool Equals(LobbyMemberData other)
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

		public readonly void Kick()
		{
		}

		public static LobbyMemberData Get(LobbyData lobby, UserData user)
		{
			return default(LobbyMemberData);
		}

		public static bool operator ==(LobbyMemberData l, LobbyMemberData r)
		{
			return false;
		}

		public static bool operator !=(LobbyMemberData l, LobbyMemberData r)
		{
			return false;
		}
	}
}
