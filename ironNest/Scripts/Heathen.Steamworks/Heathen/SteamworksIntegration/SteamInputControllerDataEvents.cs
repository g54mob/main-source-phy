using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamInputControllerData))]
	public class SteamInputControllerDataEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent onChange;

		[EventField]
		public ControllerDataEvent onUpdate;

		private SteamInputControllerData _inspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleEvent(InputControllerStateData state)
		{
		}
	}
}
