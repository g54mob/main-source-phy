using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XStoreAddonLicense
	{
		[StructLayout(LayoutKind.Sequential, Size = 18)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CskuStoreId_003E__FixedBuffer0
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 64)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CinAppOfferToken_003E__FixedBuffer1
		{
			public byte FixedElementField;
		}

		private _003CskuStoreId_003E__FixedBuffer0 skuStoreId;

		private _003CinAppOfferToken_003E__FixedBuffer1 inAppOfferToken;

		internal readonly NativeBool isActive;

		internal readonly TimeT expirationDate;

		internal unsafe XStoreAddonLicense(XGamingRuntime.XStoreAddonLicense publicObject)
		{
			fixed (byte* bytePointer = &skuStoreId.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.SkuStoreId, bytePointer, 18);
			}
			fixed (byte* bytePointer2 = &inAppOfferToken.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.InAppOfferToken, bytePointer2, 64);
			}
			isActive = new NativeBool(publicObject.IsActive);
			expirationDate = new TimeT(publicObject.ExpirationDate);
		}

		internal unsafe string GetSkuStoreId()
		{
			fixed (byte* bytePointer = &skuStoreId.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 18);
			}
		}

		internal unsafe string GetInAppOfferToken()
		{
			fixed (byte* bytePointer = &inAppOfferToken.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 64);
			}
		}
	}
}
