using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class UserAchievementStoredEvent : UnityEvent<UserAchievementStored_t>
	{
	}
}
