using System.Runtime.InteropServices;

namespace XGamingRuntime.Interop
{
	internal struct XblSocialManagerPresenceTitleRecord
	{
		internal readonly uint titleId;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isTitleActive;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 300)]
		internal readonly byte[] presenceText;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isBroadcasting;

		internal readonly XblPresenceDeviceType deviceType;

		[MarshalAs(UnmanagedType.U1)]
		internal readonly bool isPrimary;

		internal XblSocialManagerPresenceTitleRecord(XGamingRuntime.XblSocialManagerPresenceTitleRecord titleRecord)
		{
			titleId = titleRecord.TitleId;
			isTitleActive = titleRecord.IsTitleActive;
			presenceText = Converters.StringToNullTerminatedUTF8ByteArray(titleRecord.PresenceText, 300);
			isBroadcasting = titleRecord.IsBroadcasting;
			deviceType = titleRecord.DeviceType;
			isPrimary = titleRecord.IsPrimary;
		}
	}
}
