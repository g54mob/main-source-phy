using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XStorePackageUpdate
	{
		[StructLayout(LayoutKind.Sequential, Size = 33)]
		[CompilerGenerated]
		[UnsafeValueType]
		public struct _003CpackageIdentifier_003E__FixedBuffer5
		{
			public byte FixedElementField;
		}

		private _003CpackageIdentifier_003E__FixedBuffer5 packageIdentifier;

		internal readonly NativeBool isMandatory;

		internal unsafe XStorePackageUpdate(XGamingRuntime.XStorePackageUpdate publicObject)
		{
			fixed (byte* bytePointer = &packageIdentifier.FixedElementField)
			{
				Converters.StringToNullTerminatedUTF8FixedPointer(publicObject.PackageIdentifier, bytePointer, 33);
			}
			isMandatory = new NativeBool(publicObject.IsMandatory);
		}

		internal unsafe string GetPackageIdentifier()
		{
			fixed (byte* bytePointer = &packageIdentifier.FixedElementField)
			{
				return Converters.BytePointerToString(bytePointer, 33);
			}
		}
	}
}
