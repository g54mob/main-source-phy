using Heathen.SteamworksIntegration;

namespace SteamTools
{
	public delegate void SteamLobbyAuthDelegate(LobbyData lobby, UserData user, byte[] ticketData, byte[] inventoryData);
}
