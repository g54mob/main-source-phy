using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblGuid
	{
		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003Cvalue_003E__FixedBuffer20
		{
			public byte FixedElementField;
		}

		private _003Cvalue_003E__FixedBuffer20 value;

		internal unsafe XblGuid(XGamingRuntime.XblGuid publicObject)
		{
			fixed (byte* bytePointer = &value.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, bytePointer, 40);
			}
		}

		internal unsafe string GetValue()
		{
			fixed (byte* bytePointer = &value.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 40);
			}
		}
	}
}
