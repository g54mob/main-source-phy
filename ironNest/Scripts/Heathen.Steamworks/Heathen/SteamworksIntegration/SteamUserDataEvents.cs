using Heathen.SteamworksIntegration.UI;
using Steamworks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamUserData))]
	[AddComponentMenu(null)]
	[RequireComponent(typeof(SteamUserData))]
	public class SteamUserDataEvents : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		[EventField]
		public UnityEvent<UserData, EPersonaChange> onChange;

		[EventField]
		public UnityUserAndPointerDataEvent onClick;

		private SteamUserData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}
	}
}
