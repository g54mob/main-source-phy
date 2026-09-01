using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class ItemChangedEvent : UnityEvent<ItemChangeRecord>
	{
	}
}
