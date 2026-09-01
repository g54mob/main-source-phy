using Heathen.SteamworksIntegration;
using Steamworks;

namespace SteamTools
{
	public delegate void SteamLobbyGameServerDelegate(LobbyData lobby, CSteamID server, string ip, ushort port);
}
