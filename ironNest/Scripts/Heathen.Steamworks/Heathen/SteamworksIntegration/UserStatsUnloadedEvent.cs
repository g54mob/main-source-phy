using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class UserStatsUnloadedEvent : UnityEvent<UserStatsUnloaded_t>
	{
	}
}
