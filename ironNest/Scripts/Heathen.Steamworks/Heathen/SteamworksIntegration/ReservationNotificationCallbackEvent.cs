using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class ReservationNotificationCallbackEvent : UnityEvent<ReservationNotificationCallback_t>
	{
	}
}
