using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PartyCSharpSDK.Interop
{
	internal struct PARTY_REGION
	{
		[StructLayout(LayoutKind.Sequential, Size = 20)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CregionName_003E__FixedBuffer37
		{
			public byte FixedElementField;
		}

		private _003CregionName_003E__FixedBuffer37 regionName;

		internal readonly uint roundTripLatencyInMilliseconds;

		internal unsafe PARTY_REGION(PartyCSharpSDK.PARTY_REGION publicObject)
		{
			fixed (byte* bytePointer = &regionName.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.RegionName, bytePointer, 20);
			}
			roundTripLatencyInMilliseconds = publicObject.RoundTripLatencyInMilliseconds;
		}

		internal unsafe string GetRegionName()
		{
			fixed (byte* bytePointer = &regionName.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 20);
			}
		}
	}
}
