using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	public struct XblDeviceToken
	{
		[StructLayout(LayoutKind.Sequential, Size = 40)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003CValue_003E__FixedBuffer10
		{
			public byte FixedElementField;
		}

		private _003CValue_003E__FixedBuffer10 Value;

		internal unsafe XblDeviceToken(XGamingRuntime.XblDeviceToken publicObject)
		{
			fixed (byte* value = &Value.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, value, 40);
			}
		}

		internal unsafe string GetValue()
		{
			fixed (byte* value = &Value.FixedElementField)
			{
				return Converters.BytePointerToString(value, 40);
			}
		}
	}
}
