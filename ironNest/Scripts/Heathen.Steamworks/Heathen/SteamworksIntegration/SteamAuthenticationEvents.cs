using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamAuthenticationData))]
	public class SteamAuthenticationEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent<AuthenticationTicket> onChange;

		[EventField]
		public UnityEvent<ulong, byte[]> onRpcInvoke;

		[EventField]
		public UnityEvent<EResult> onError;

		[EventField]
		public UnityEvent<EBeginAuthSessionResult> onInvalidTicket;

		[EventField]
		public UnityEvent<EAuthSessionResponse> onInvalidSession;

		[EventField]
		public UnityEvent<EAuthSessionResponse> onSessionStart;
	}
}
