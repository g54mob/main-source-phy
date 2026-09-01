using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAuthenticationData))]
	public class SteamAuthenticationGetTicket : MonoBehaviour
	{
		private AuthenticationTicket _mData;

		public AuthenticationTicket Data
		{
			get
			{
				return null;
			}
			private set
			{
			}
		}

		public void GetTicketForLobbyServer(SteamLobbyData lobby)
		{
		}

		public void GetTicketForLobbyOwner(SteamLobbyData lobby)
		{
		}

		public void GetTicketForUser(SteamUserData user)
		{
		}

		public void GetTicketForGameServer(SteamGameServerData server)
		{
		}

		public void GetTicketForWebAPI(string identity)
		{
		}

		private void HandleTicketCallback(AuthenticationTicket ticket, bool ioError)
		{
		}
	}
}
