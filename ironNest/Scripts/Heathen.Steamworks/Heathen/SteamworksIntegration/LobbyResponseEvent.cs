using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class LobbyResponseEvent : UnityEvent<EChatRoomEnterResponse>
	{
	}
}
