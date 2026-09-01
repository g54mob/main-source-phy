using PartyCSharpSDK;
using PartyXBLCSharpSDK.Interop;

namespace PartyXBLCSharpSDK
{
	public class PARTY_XBL_CHAT_PERMISSION_INFO
	{
		public PARTY_CHAT_PERMISSION_OPTIONS ChatPermissionMask { get; set; }

		public PARTY_XBL_CHAT_PERMISSION_MASK_REASON Reason { get; set; }

		internal PARTY_XBL_CHAT_PERMISSION_INFO(PartyXBLCSharpSDK.Interop.PARTY_XBL_CHAT_PERMISSION_INFO interopStruct)
		{
			ChatPermissionMask = interopStruct.chatPermissionMask;
			Reason = interopStruct.reason;
		}
	}
}
