using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct UserLobbyLeaveData
	{
		public UserData user;

		public EChatMemberStateChange state;
	}
}
