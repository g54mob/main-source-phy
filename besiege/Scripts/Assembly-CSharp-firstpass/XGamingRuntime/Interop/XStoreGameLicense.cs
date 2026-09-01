using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XStoreGameLicense
	{
		[StructLayout(LayoutKind.Sequential, Size = 18)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CskuStoreId_003E__FixedBuffer3
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 64)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CtrialUniqueId_003E__FixedBuffer4
		{
			public byte FixedElementField;
		}

		private _003CskuStoreId_003E__FixedBuffer3 skuStoreId;

		internal readonly NativeBool isActive;

		internal readonly NativeBool isTrialOwnedByThisUser;

		internal readonly NativeBool isDiscLicense;

		internal readonly NativeBool isTrial;

		internal readonly uint trialTimeRemainingInSeconds;

		private _003CtrialUniqueId_003E__FixedBuffer4 trialUniqueId;

		internal readonly TimeT expirationDate;

		internal unsafe XStoreGameLicense(XGamingRuntime.XStoreGameLicense publicObject)
		{
			fixed (byte* bytePointer = &skuStoreId.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.SkuStoreId, bytePointer, 18);
			}
			isActive = new NativeBool(publicObject.IsActive);
			isTrialOwnedByThisUser = new NativeBool(publicObject.IsTrialOwnedByThisUser);
			isDiscLicense = new NativeBool(publicObject.IsDiscLicense);
			isTrial = new NativeBool(publicObject.IsTrial);
			trialTimeRemainingInSeconds = publicObject.TrialTimeRemainingInSeconds;
			fixed (byte* bytePointer2 = &trialUniqueId.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.TrialUniqueId, bytePointer2, 64);
			}
			expirationDate = new TimeT(publicObject.ExpirationDate);
		}

		internal unsafe string GetSkuStoreId()
		{
			fixed (byte* bytePointer = &skuStoreId.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 18);
			}
		}

		internal unsafe string GetTrialUniqueId()
		{
			fixed (byte* bytePointer = &trialUniqueId.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 64);
			}
		}
	}
}
