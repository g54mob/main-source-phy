using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class SteamInventoryResultReadyEvent : UnityEvent<InventoryResult>
	{
	}
}
