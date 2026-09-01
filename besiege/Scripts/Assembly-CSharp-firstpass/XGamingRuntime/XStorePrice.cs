using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStorePrice
	{
		public float BasePrice { get; private set; }

		public float Price { get; private set; }

		public float RecurrencePrice { get; private set; }

		public string CurrencyCode { get; private set; }

		public string FormattedBasePrice { get; private set; }

		public string FormattedPrice { get; private set; }

		public string FormattedRecurrencePrice { get; private set; }

		public bool IsOnSale { get; private set; }

		public DateTime SaleEndDate { get; private set; }

		internal XStorePrice(XGamingRuntime.Interop.XStorePrice interopStruct)
		{
			BasePrice = interopStruct.basePrice;
			Price = interopStruct.price;
			RecurrencePrice = interopStruct.recurrencePrice;
			CurrencyCode = interopStruct.currencyCode.GetString();
			FormattedBasePrice = interopStruct.GetFormattedBasePrice();
			FormattedPrice = interopStruct.GetFormattedPrice();
			FormattedRecurrencePrice = interopStruct.GetFormattedRecurrencePrice();
			IsOnSale = interopStruct.isOnSale.Value;
			SaleEndDate = interopStruct.saleEndDate.DateTime;
		}
	}
}
