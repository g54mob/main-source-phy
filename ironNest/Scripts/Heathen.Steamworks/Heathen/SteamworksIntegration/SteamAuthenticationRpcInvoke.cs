using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAuthenticationEvents))]
	public class SteamAuthenticationRpcInvoke : MonoBehaviour
	{
		private SteamAuthenticationEvents _mEvents;

		private void Awake()
		{
		}

		private void HandleTicketChanged(AuthenticationTicket arg0)
		{
		}
	}
}
