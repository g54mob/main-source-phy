using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerSessionNumberAttribute
	{
		[StructLayout(LayoutKind.Sequential, Size = 100)]
		[UnsafeValueType]
		[CompilerGenerated]
		public struct _003Cname_003E__FixedBuffer21
		{
			public byte FixedElementField;
		}

		private _003Cname_003E__FixedBuffer21 name;

		internal readonly double value;

		internal unsafe XblMultiplayerSessionNumberAttribute(XGamingRuntime.XblMultiplayerSessionNumberAttribute publicObject)
		{
			fixed (byte* bytePointer = &name.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Name, bytePointer, 100);
			}
			value = publicObject.Value;
		}

		internal unsafe string GetName()
		{
			fixed (byte* bytePointer = &name.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 100);
			}
		}
	}
}
