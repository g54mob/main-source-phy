using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class AvailableBeaconLocationsUpdatedEvent : UnityEvent<AvailableBeaconLocationsUpdated_t>
	{
	}
}
