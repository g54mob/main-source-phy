using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class GameServerChangeRequestedEvent : UnityEvent<string, string>
	{
	}
}
