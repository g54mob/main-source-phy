using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblSocialManagerUser
	{
		internal readonly ulong xboxUserId;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isFavorite;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isFollowingUser;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isFollowedByCaller;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 90)]
		internal readonly byte[] displayName;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 765)]
		internal readonly byte[] realName;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 675)]
		internal readonly byte[] displayPicUrlRaw;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool useAvatar;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		internal readonly byte[] gamerscore;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
		internal readonly byte[] gamertag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 97)]
		internal readonly byte[] modernGamertag;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
		internal readonly byte[] modernGamertagSuffix;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 101)]
		internal readonly byte[] uniqueModernGamertag;

		internal readonly XblSocialManagerPresenceRecord presenceRecord;

		internal readonly XblTitleHistory titleHistory;

		internal readonly XblPreferredColor preferredColor;
	}
}
