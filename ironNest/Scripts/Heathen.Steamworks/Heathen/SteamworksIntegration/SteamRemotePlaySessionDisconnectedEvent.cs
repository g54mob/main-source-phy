using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class SteamRemotePlaySessionDisconnectedEvent : UnityEvent<SteamRemotePlaySessionDisconnected_t>
	{
	}
}
