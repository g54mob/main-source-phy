using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct FriendRichPresenceUpdate
	{
		public FriendRichPresenceUpdate_t Data;

		public readonly UserData Friend => default(UserData);

		public readonly AppData App => default(AppData);

		public static implicit operator FriendRichPresenceUpdate(FriendRichPresenceUpdate_t native)
		{
			return default(FriendRichPresenceUpdate);
		}

		public static implicit operator FriendRichPresenceUpdate_t(FriendRichPresenceUpdate heathen)
		{
			return default(FriendRichPresenceUpdate_t);
		}
	}
}
