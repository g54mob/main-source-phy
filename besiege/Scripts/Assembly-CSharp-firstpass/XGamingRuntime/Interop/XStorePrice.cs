using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XStorePrice
	{
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CformattedBasePrice_003E__FixedBuffer6
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 16)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CformattedPrice_003E__FixedBuffer7
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 16)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CformattedRecurrencePrice_003E__FixedBuffer8
		{
			public byte FixedElementField;
		}

		internal readonly float basePrice;

		internal readonly float price;

		internal readonly float recurrencePrice;

		internal readonly UTF8StringPtr currencyCode;

		private _003CformattedBasePrice_003E__FixedBuffer6 formattedBasePrice;

		private _003CformattedPrice_003E__FixedBuffer7 formattedPrice;

		private _003CformattedRecurrencePrice_003E__FixedBuffer8 formattedRecurrencePrice;

		internal readonly NativeBool isOnSale;

		internal readonly TimeT saleEndDate;

		internal unsafe XStorePrice(XGamingRuntime.XStorePrice publicObject, DisposableCollection disposableCollection)
		{
			basePrice = publicObject.BasePrice;
			price = publicObject.Price;
			recurrencePrice = publicObject.RecurrencePrice;
			currencyCode = new UTF8StringPtr(publicObject.CurrencyCode, disposableCollection);
			fixed (byte* bytePointer = &formattedBasePrice.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.FormattedBasePrice, bytePointer, 16);
			}
			fixed (byte* bytePointer2 = &formattedPrice.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.FormattedPrice, bytePointer2, 16);
			}
			fixed (byte* bytePointer3 = &formattedRecurrencePrice.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.FormattedRecurrencePrice, bytePointer3, 16);
			}
			isOnSale = new NativeBool(publicObject.IsOnSale);
			saleEndDate = new TimeT(publicObject.SaleEndDate);
		}

		internal unsafe string GetFormattedBasePrice()
		{
			fixed (byte* bytePointer = &formattedBasePrice.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 16);
			}
		}

		internal unsafe string GetFormattedPrice()
		{
			fixed (byte* bytePointer = &formattedPrice.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 16);
			}
		}

		internal unsafe string GetFormattedRecurrencePrice()
		{
			fixed (byte* bytePointer = &formattedRecurrencePrice.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 16);
			}
		}
	}
}
