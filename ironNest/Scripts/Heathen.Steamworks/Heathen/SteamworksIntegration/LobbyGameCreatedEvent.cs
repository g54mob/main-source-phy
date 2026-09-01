using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class LobbyGameCreatedEvent : UnityEvent<LobbyGameCreated_t>
	{
	}
}
