using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct UserStatsReceived
	{
		public UserStatsReceived_t Data;

		public GameData Id => default(GameData);

		public EResult Result => default(EResult);

		public UserData User => default(UserData);

		public static implicit operator UserStatsReceived(UserStatsReceived_t native)
		{
			return default(UserStatsReceived);
		}

		public static implicit operator UserStatsReceived_t(UserStatsReceived heathen)
		{
			return default(UserStatsReceived_t);
		}
	}
}
