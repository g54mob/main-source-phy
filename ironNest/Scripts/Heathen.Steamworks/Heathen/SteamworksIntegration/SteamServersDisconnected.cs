using Steamworks;

namespace Heathen.SteamworksIntegration
{
	public struct SteamServersDisconnected
	{
		public SteamServersDisconnected_t Data;

		public EResult Result => default(EResult);

		public static implicit operator SteamServersDisconnected(SteamServersDisconnected_t native)
		{
			return default(SteamServersDisconnected);
		}

		public static implicit operator SteamServersDisconnected_t(SteamServersDisconnected heathen)
		{
			return default(SteamServersDisconnected_t);
		}
	}
}
