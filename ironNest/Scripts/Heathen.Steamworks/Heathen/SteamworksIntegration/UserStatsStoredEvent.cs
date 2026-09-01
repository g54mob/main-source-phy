using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class UserStatsStoredEvent : UnityEvent<UserStatsStored_t>
	{
	}
}
