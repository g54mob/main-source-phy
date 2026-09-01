using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblUserProfile
	{
		internal readonly ulong xboxUserId;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 90)]
		internal byte[] appDisplayName;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 675)]
		internal byte[] appDisplayPictureResizeUri;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 90)]
		internal byte[] gameDisplayName;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 675)]
		internal byte[] gameDisplayPictureResizeUri;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		internal byte[] gamerscore;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		internal byte[] gamertag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 97)]
		internal byte[] modernGamertag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
		internal byte[] modernGamertagSuffix;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
		internal byte[] uniqueModernGamertag;
	}
}
