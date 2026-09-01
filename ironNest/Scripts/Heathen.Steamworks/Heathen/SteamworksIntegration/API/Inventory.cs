using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Inventory
	{
		public static class Client
		{
			private class SerializationPointer
			{
				public UserData ExpectedUser;

				public Action<InventoryResult> Callback;
			}

			private static Dictionary<ItemData, List<ItemDetail>> _itemIndex;

			private static SteamInventoryDefinitionUpdateEvent _mOnSteamInventoryDefinitionUpdate;

			private static SteamInventoryResultReadyEvent _mOnSteamInventoryResultReady;

			private static SteamMicroTransactionAuthorizationResponce _mOnSteamMtxAuthResponse;

			private static Dictionary<SteamInventoryResult_t, Action<InventoryResult>> _mResultHandles;

			private static Dictionary<SteamInventoryResult_t, Action<byte[]>> _mSerializationResults;

			private static Dictionary<SteamInventoryResult_t, SerializationPointer> _mDeserializationResults;

			private static CallResult<SteamInventoryEligiblePromoItemDefIDs_t> _mSteamInventoryEligiblePromoItemDefIDsT;

			private static CallResult<SteamInventoryStartPurchaseResult_t> _mSteamInventoryStartPurchaseResultT;

			private static CallResult<SteamInventoryRequestPricesResult_t> _mSteamInventoryRequestPricesResultT;

			public static Currency.Code LocalCurrencyCode { get; private set; }

			public static string LocalCurrencySymbol => null;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void Init()
			{
			}

			public static List<ItemDetail> Details(ItemData item)
			{
				return null;
			}

			public static long ItemTotalQuantity(ItemData item)
			{
				return 0L;
			}

			public static bool AddPromoItem(SteamItemDef_t itemDef, Action<InventoryResult> callback)
			{
				return false;
			}

			public static bool AddPromoItems(ItemDefinitionSettings item, Action<InventoryResult> callback)
			{
				return false;
			}

			public static bool AddPromoItems(SteamItemDef_t[] itemDefs, Action<InventoryResult> callback)
			{
				return false;
			}

			public static bool AddPromoItems(ItemDefinitionSettings[] items, Action<InventoryResult> callback)
			{
				return false;
			}

			public static bool AddPromoItems(IEnumerable<SteamItemDef_t> itemDefs, Action<InventoryResult> callback)
			{
				return false;
			}

			public static bool CheckResultSteamID(SteamInventoryResult_t resultHandle, CSteamID steamIDExpected)
			{
				return false;
			}

			public static void ConsumeItem(SteamItemInstanceID_t itemConsume, uint quantity, Action<InventoryResult> callback)
			{
			}

			public static void DeserializeResult(UserData expectedUser, byte[] buffer, Action<InventoryResult> callback)
			{
			}

			public static void DestroyResult(SteamInventoryResult_t resultHandle)
			{
			}

			public static void ExchangeItems(SteamItemDef_t generate, SteamItemInstanceID_t[] destroy, uint[] destroyQuantity, Action<InventoryResult> callback)
			{
			}

			public static void GenerateItems(SteamItemDef_t[] itemDefs, uint[] quantity, Action<InventoryResult> callback)
			{
			}

			public static void GetAllItems(Action<InventoryResult> callback = null)
			{
			}

			public static void GetEligiblePromoItems(UserData user, Action<EResult, ItemData[], bool> callback)
			{
			}

			public static bool GetItemDefinitionIDs(out SteamItemDef_t[] results)
			{
				results = null;
				return false;
			}

			public static string GetItemDefinitionProperty(SteamItemDef_t item, string propertyName)
			{
				return null;
			}

			public static string[] GetItemDefinitionProperties(SteamItemDef_t item)
			{
				return null;
			}

			public static void GetItemsByID(SteamItemInstanceID_t[] instanceIds, Action<InventoryResult> callback = null)
			{
			}

			public static bool GetItemPrice(SteamItemDef_t item, out ulong currentPrice, out ulong basePrice)
			{
				currentPrice = default(ulong);
				basePrice = default(ulong);
				return false;
			}

			public static bool GetItemsWithPrices(out SteamItemDef_t[] items, out ulong[] currentPrices, out ulong[] basePrices)
			{
				items = null;
				currentPrices = null;
				basePrices = null;
				return false;
			}

			public static bool GetResultItemProperty(SteamInventoryResult_t resultHandle, uint itemIndex, string propertyName, out string valueBuffer, ref uint bufferSize)
			{
				valueBuffer = null;
				return false;
			}

			public static bool GetResultItems(SteamInventoryResult_t resultHandle, SteamItemDetails_t[] items, ref uint count)
			{
				return false;
			}

			public static DateTime GetResultTimestamp(SteamInventoryResult_t resultHandle)
			{
				return default(DateTime);
			}

			public static bool GrantPromoItems(Action<InventoryResult> callback = null)
			{
				return false;
			}

			public static bool LoadItemDefinitions()
			{
				return false;
			}

			public static void RequestPrices(Action<SteamInventoryRequestPricesResult_t, bool> callback)
			{
			}

			public static void SerializeItemResultsByID(SteamItemInstanceID_t[] instanceIds, Action<byte[]> callback)
			{
			}

			public static void SerializeAllItemResults(Action<byte[]> callback)
			{
			}

			public static void StartPurchase(SteamItemDef_t[] items, uint[] quantities, Action<SteamInventoryStartPurchaseResult_t, bool> callback)
			{
			}

			public static void TransferItemQuantity(SteamItemInstanceID_t source, uint quantity, SteamItemInstanceID_t destination, Action<InventoryResult> callback)
			{
			}

			public static void TriggerItemDrop(SteamItemDef_t item, Action<InventoryResult> callback)
			{
			}

			public static SteamInventoryUpdateHandle_t StartUpdateProperties()
			{
				return default(SteamInventoryUpdateHandle_t);
			}

			public static void SubmitUpdateProperties(SteamInventoryUpdateHandle_t handle, Action<InventoryResult> callback)
			{
			}

			public static void RemoveProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t item, string propertyName)
			{
			}

			public static void SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t item, string propertyName, string data)
			{
			}

			public static void SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t item, string propertyName, bool data)
			{
			}

			public static void SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t item, string propertyName, long data)
			{
			}

			public static void SetProperty(SteamInventoryUpdateHandle_t handle, SteamItemInstanceID_t item, string propertyName, float data)
			{
			}

			private static ItemDetail GetExtendedItemDetail(SteamInventoryResult_t result, uint index, SteamItemDetails_t detail)
			{
				return default(ItemDetail);
			}

			internal static void HandleInventoryResults(SteamInventoryResultReady_t results)
			{
			}
		}
	}
}
