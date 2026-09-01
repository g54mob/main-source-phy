using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class GameLobbyJoinRequestedEvent : UnityEvent<LobbyData, UserData>
	{
	}
}
