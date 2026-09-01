using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public struct SteamServerConnectFailure
	{
		public SteamServerConnectFailure_t Data;

		public EResult Result => default(EResult);

		public bool Retrying => false;

		public static implicit operator SteamServerConnectFailure(SteamServerConnectFailure_t native)
		{
			return default(SteamServerConnectFailure);
		}

		public static implicit operator SteamServerConnectFailure_t(SteamServerConnectFailure heathen)
		{
			return default(SteamServerConnectFailure_t);
		}
	}
}
