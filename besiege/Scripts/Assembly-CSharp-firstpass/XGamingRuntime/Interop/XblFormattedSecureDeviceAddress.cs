using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblFormattedSecureDeviceAddress
	{
		[StructLayout(LayoutKind.Sequential, Size = 4096)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003Cvalue_003E__FixedBuffer11
		{
			public byte FixedElementField;
		}

		private _003Cvalue_003E__FixedBuffer11 value;

		internal unsafe string GetValue()
		{
			fixed (byte* bytePointer = &value.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 4096);
			}
		}
	}
}
