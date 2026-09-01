using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XStoreCanAcquireLicenseResult
	{
		[StructLayout(LayoutKind.Sequential, Size = 5)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003ClicensableSku_003E__FixedBuffer2
		{
			public byte FixedElementField;
		}

		private _003ClicensableSku_003E__FixedBuffer2 licensableSku;

		internal readonly XStoreCanLicenseStatus status;

		internal unsafe XStoreCanAcquireLicenseResult(XGamingRuntime.XStoreCanAcquireLicenseResult publicObject)
		{
			fixed (byte* bytePointer = &licensableSku.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.LicensableSku, bytePointer, 5);
			}
			status = publicObject.Status;
		}

		internal unsafe string GetLicensableSku()
		{
			fixed (byte* bytePointer = &licensableSku.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 5);
			}
		}
	}
}
