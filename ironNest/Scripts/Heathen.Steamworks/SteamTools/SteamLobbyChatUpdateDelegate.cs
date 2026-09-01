using Heathen.SteamworksIntegration;
using Steamworks;

namespace SteamTools
{
	public delegate void SteamLobbyChatUpdateDelegate(LobbyData lobby, UserData user, EChatMemberStateChange changes);
}
