using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerSessionStringAttribute
	{
		[StructLayout(LayoutKind.Sequential, Size = 100)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003Cname_003E__FixedBuffer22
		{
			public byte FixedElementField;
		}

		[StructLayout(LayoutKind.Sequential, Size = 100)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003Cvalue_003E__FixedBuffer23
		{
			public byte FixedElementField;
		}

		private _003Cname_003E__FixedBuffer22 name;

		private _003Cvalue_003E__FixedBuffer23 value;

		internal unsafe XblMultiplayerSessionStringAttribute(XGamingRuntime.XblMultiplayerSessionStringAttribute publicObject)
		{
			fixed (byte* bytePointer = &name.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Name, bytePointer, 100);
			}
			fixed (byte* bytePointer2 = &value.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.Value, bytePointer2, 100);
			}
		}

		internal unsafe string GetName()
		{
			fixed (byte* bytePointer = &name.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 100);
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
