using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PartyCSharpSDK.Interop
{
	internal struct PARTY_NETWORK_DESCRIPTOR
	{
		[StructLayout(LayoutKind.Sequential, Size = 37)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CnetworkIdentifier_003E__FixedBuffer34
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 20)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CregionName_003E__FixedBuffer35
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 301)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CopaqueConnectionInformation_003E__FixedBuffer36
		{
			public byte FixedElementField;
		}

		private _003CnetworkIdentifier_003E__FixedBuffer34 networkIdentifier;

		private _003CregionName_003E__FixedBuffer35 regionName;

		private _003CopaqueConnectionInformation_003E__FixedBuffer36 opaqueConnectionInformation;

		internal unsafe PARTY_NETWORK_DESCRIPTOR(PartyCSharpSDK.PARTY_NETWORK_DESCRIPTOR publicObject)
		{
			fixed (byte* bytePointer = &networkIdentifier.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.NetworkIdentifier, bytePointer, 37);
			}
			fixed (byte* bytePointer2 = &regionName.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.RegionName, bytePointer2, 20);
			}
			fixed (byte* ptr = &opaqueConnectionInformation.FixedElementField)
			{
				Marshal.Copy(publicObject.OpaqueConnectionInformation, 0, (IntPtr)ptr, 301);
			}
		}

		internal unsafe string GetNetworkIdentifier()
		{
			fixed (byte* bytePointer = &networkIdentifier.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 37);
			}
		}

		internal unsafe string GetRegionName()
		{
			fixed (byte* bytePointer = &regionName.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 20);
			}
		}

		internal unsafe byte[] GetOpaqueConnectionInformation()
		{
			fixed (byte* ptr = &opaqueConnectionInformation.FixedElementField)
			{
				byte[] array = new byte[301];
				byte[] destination = array;
				Marshal.Copy((IntPtr)ptr, destination, 0, 301);
				return array;
			}
		}
	}
}
