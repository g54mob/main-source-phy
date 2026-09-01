using UnityEngine;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamInputActionData))]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamInputActionData))]
	public class SteamInputActionEvent : MonoBehaviour
	{
		[EventField]
		public ActionUpdateEvent onChanged;

		private SteamInputActionData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleEvent(InputControllerStateData controller)
		{
		}
	}
}
