using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class LobbyAuthenticaitonSessionEvent : UnityEvent<AuthenticationSession, byte[]>
	{
	}
}
