using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	[StructLayout((LayoutKind)2)]
	internal struct Union_IntBoolFloatString
	{
		[FieldOffset(0)]
		public int intvalue;

		[FieldOffset(0)]
		public bool boolvalue;

		[FieldOffset(0)]
		public float floatvalue;

		[FieldOffset(0)]
		public StringWrapper stringvalue;
	}
}
