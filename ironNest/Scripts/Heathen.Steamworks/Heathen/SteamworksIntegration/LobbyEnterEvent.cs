using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class LobbyEnterEvent : UnityEvent<LobbyEnter_t>
	{
	}
}
