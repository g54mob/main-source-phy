using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public class FavoritesListChangedEvent : UnityEvent<FavoritesListChanged>
	{
	}
}
