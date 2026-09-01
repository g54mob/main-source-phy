using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class LobbyAuthenticationEvent : UnityEvent<LobbyData, UserData, byte[], byte[]>
	{
	}
}
