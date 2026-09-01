using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class ActiveBeaconsUpdatedEvent : UnityEvent<ActiveBeaconsUpdated_t>
	{
	}
}
