using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerSessionTag
	{
		[StructLayout(LayoutKind.Sequential, Size = 100)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003Cvalue_003E__FixedBuffer24
		{
			public byte FixedElementField;
		}

		private _003Cvalue_003E__FixedBuffer24 value;

		internal unsafe XblMultiplayerSessionTag(XGamingRuntime.XblMultiplayerSessionTag publicObject)
		{
			fixed (byte* bytePointer = &value.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, bytePointer, 100);
			}
		}

		internal unsafe string GetValue()
		{
			fixed (byte* bytePointer = &value.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 100);
			}
		}
	}
}
