using System;
using System.Collections.Generic;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct ItemData : IEquatable<ItemData>, IEquatable<int>, IEquatable<SteamItemDef_t>, IComparable<ItemData>, IComparable<int>, IComparable<SteamItemDef_t>
	{
		public int id;

		public readonly string Name => null;

		public readonly bool HasPrice => false;

		public static Currency.Code CurrencyCode => default(Currency.Code);

		public static string CurrencySymbol => null;

		public readonly ulong CurrentPrice => 0uL;

		public readonly ulong BasePrice => 0uL;

		public readonly List<ItemDetail> GetDetails()
		{
			return null;
		}

		public readonly long GetTotalQuantity()
		{
			return 0L;
		}

		public readonly bool AddPromoItem(Action<InventoryResult> callback)
		{
			return false;
		}

		public readonly ConsumeOrder[] GetConsumeOrders(uint quantity)
		{
			return null;
		}

		public readonly bool Consume(Action<InventoryResult> callback)
		{
			return false;
		}

		public readonly void Consume(ConsumeOrder order, Action<InventoryResult> callback)
		{
		}

		public readonly bool Consume(uint quantity, Action<InventoryResult> callback)
		{
			return false;
		}

		public readonly bool GetExchangeEntry(uint quantity, out ExchangeEntry[] entries)
		{
			entries = null;
			return false;
		}

		public readonly void Exchange(IEnumerable<ExchangeEntry> recipeEntries, Action<InventoryResult> callback)
		{
		}

		public readonly void GenerateItem(Action<InventoryResult> callback)
		{
		}

		public readonly void GenerateItem(uint quantity, Action<InventoryResult> callback)
		{
		}

		public readonly void StartPurchase(Action<SteamInventoryStartPurchaseResult_t, bool> callback)
		{
		}

		public readonly void StartPurchase(uint count, Action<SteamInventoryStartPurchaseResult_t, bool> callback)
		{
		}

		public readonly bool GetPrice(out ulong currentPrice, out ulong basePrice)
		{
			currentPrice = default(ulong);
			basePrice = default(ulong);
			return false;
		}

		public readonly void TriggerDrop(Action<InventoryResult> callback)
		{
		}

		public readonly string CurrentPriceString()
		{
			return null;
		}

		public readonly string BasePriceString()
		{
			return null;
		}

		public static void RequestPrices(Action<SteamInventoryRequestPricesResult_t, bool> callback)
		{
		}

		public static void Update(Action<InventoryResult> callback)
		{
		}

		public static ItemData Get(int id)
		{
			return default(ItemData);
		}

		public static ItemData Get(SteamItemDef_t id)
		{
			return default(ItemData);
		}

		public static ItemData Get(ItemDefinitionSettings item)
		{
			return default(ItemData);
		}

		public readonly int CompareTo(ItemData other)
		{
			return 0;
		}

		public readonly int CompareTo(int other)
		{
			return 0;
		}

		public readonly int CompareTo(SteamItemDef_t other)
		{
			return 0;
		}

		public readonly bool Equals(ItemData other)
		{
			return false;
		}

		public readonly bool Equals(int other)
		{
			return false;
		}

		public readonly bool Equals(SteamItemDef_t other)
		{
			return false;
		}

		public override readonly bool Equals(object obj)
		{
			return false;
		}

		public override readonly int GetHashCode()
		{
			return 0;
		}

		public static bool operator ==(ItemData l, ItemData r)
		{
			return false;
		}

		public static bool operator ==(ItemData l, int r)
		{
			return false;
		}

		public static bool operator ==(ItemData l, SteamItemDef_t r)
		{
			return false;
		}

		public static bool operator !=(ItemData l, ItemData r)
		{
			return false;
		}

		public static bool operator !=(ItemData l, int r)
		{
			return false;
		}

		public static bool operator !=(ItemData l, SteamItemDef_t r)
		{
			return false;
		}

		public static implicit operator int(ItemData c)
		{
			return 0;
		}

		public static implicit operator ItemData(int id)
		{
			return default(ItemData);
		}

		public static implicit operator SteamItemDef_t(ItemData c)
		{
			return default(SteamItemDef_t);
		}

		public static implicit operator ItemData(SteamItemDef_t id)
		{
			return default(ItemData);
		}
	}
}
