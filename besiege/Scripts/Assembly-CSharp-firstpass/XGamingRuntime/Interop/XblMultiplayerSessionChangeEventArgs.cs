using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerSessionChangeEventArgs
	{
		internal XblMultiplayerSessionReference SessionReference;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
		internal byte[] Branch;

		internal readonly ulong ChangeNumber;

		internal unsafe string GetBranch()
		{
			//IL_001f->IL002b: Incompatible stack types: I vs Ref
			fixed (byte* bytePointer = &(Branch != null && Branch.Length != 0 ? ref Branch[0] : ref *(byte*)null))
			{
				return Converters.BytePointerToString(bytePointer, 40);
			}
		}
	}
}
