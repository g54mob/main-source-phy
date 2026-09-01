using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamGameServerData))]
	public class SteamGameServerEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent onChange;
	}
}
