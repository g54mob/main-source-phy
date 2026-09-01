using System;
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class WorkshopItemInstalledEvent : UnityEvent<ItemInstalled_t>
	{
	}
}
