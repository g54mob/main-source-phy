using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	public class ItemShoppingCartManager : MonoBehaviour
	{
		[Serializable]
		public class StartPurchaseError : UnityEvent<EResult>
		{
		}

		[Serializable]
		public class StartPurchaseSuccess : UnityEvent<SteamInventoryStartPurchaseResult_t>
		{
		}

		[Serializable]
		public class OrderAuthorisation : UnityEvent<ItemEntry[], bool>
		{
		}

		[Serializable]
		public struct ItemEntry
		{
			public ItemDefinitionSettings item;

			public int quantity;
		}

		public StartPurchaseError evtStartPurchaseError;

		public StartPurchaseSuccess evtStartPurchaseSuccess;

		[FormerlySerializedAs("evtOrderAuthoriation")]
		public OrderAuthorisation evtOrderAuthorisation;

		private SteamInventoryStartPurchaseResult_t? _result;

		public List<ItemEntry> items;

		public bool OrderPending => false;

		public ulong OrderId => 0uL;

		public ulong TransactionId => 0uL;

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void HandleAuthorisationResponse(AppData appId, ulong orderId, bool authorized)
		{
		}

		public void Add(ItemDefinitionSettings item, int count)
		{
		}

		public void Set(ItemDefinitionSettings item, int count)
		{
		}

		public int Get(ItemDefinitionSettings item)
		{
			return 0;
		}

		public ulong TotalPrice()
		{
			return 0uL;
		}

		public string TotalPriceSymbolledString()
		{
			return null;
		}

		public string TotalPriceCurrencyCodeString()
		{
			return null;
		}

		public void StartPurchase()
		{
		}

		public void StartPurchase(Action<SteamInventoryStartPurchaseResult_t, bool> callback)
		{
		}

		public void ClearPending(bool clearCart = false)
		{
		}
	}
}
