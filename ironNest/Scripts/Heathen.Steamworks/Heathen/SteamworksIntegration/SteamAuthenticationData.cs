using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu("Steamworks/Authentication")]
	[HelpURL("https://kb.heathen.group/steam/features/authentication")]
	public class SteamAuthenticationData : MonoBehaviour
	{
		public enum ManagedEvents
		{
			Changed = 0,
			TicketRequestErred = 1,
			RPCInvoked = 2,
			InvalidTicketReceived = 3,
			InvalidSessionRequested = 4,
			SessionStarted = 5
		}

		private AuthenticationTicket _mData;

		private SteamAuthenticationEvents _mEvents;

		[FormerlySerializedAs("m_Delegates")]
		[SerializeField]
		private List<ManagedEvents> mDelegates;

		public AuthenticationTicket Data
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}
	}
}
