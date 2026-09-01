using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class LobbyChatUpdateEvent : UnityEvent<LobbyChatUpdate_t>
	{
	}
}
