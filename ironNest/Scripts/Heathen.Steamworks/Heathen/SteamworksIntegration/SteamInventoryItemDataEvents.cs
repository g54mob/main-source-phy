using Steamworks;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
	[ModularEvents(typeof(SteamInventoryItemData))]
	[RequireComponent(typeof(SteamInventoryItemData))]
	[AddComponentMenu(null)]
	public class SteamInventoryItemDataEvents : MonoBehaviour
	{
		[EventField]
		public UnityEvent onChange;

		[EventField]
		public UnityEvent onStateChanged;

		[EventField]
		public UnityEvent<ItemDetail[]> onConsumeRequestComplete;

		[EventField]
		public UnityEvent<SteamInventoryStartPurchaseResult_t> onPurchaseStarted;

		[EventField]
		public UnityEvent<ulong> onOrderAuthorized;

		[EventField]
		public UnityEvent<ulong> onOrderNotAuthorized;

		[EventField]
		public UnityEvent<ItemDetail[]> onAddPromoComplete;

		[EventField]
		public UnityEvent<bool> onCanExchangeChange;

		[EventField]
		public UnityEvent<ItemDetail[]> onExchangeComplete;

		[EventField]
		public UnityEvent onConsumeRequestRejected;

		[EventField]
		public UnityEvent<EResult> onConsumeRequestFailed;

		[EventField]
		public UnityEvent<EResult> onPurchaseStartFailed;

		[EventField]
		public UnityEvent onAddPromoRejected;

		[EventField]
		public UnityEvent<EResult> onAddPromoFailed;

		[EventField]
		public UnityEvent onExchangeRejected;

		[EventField]
		public UnityEvent<EResult> onExchangeFailed;

		private SteamInventoryItemData _mInspector;

		private void Awake()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleTransactionAuth(AppData arg0, ulong arg1, bool arg2)
		{
		}

		private void HandleInvResultReady(InventoryResult arg0)
		{
		}
	}
}
