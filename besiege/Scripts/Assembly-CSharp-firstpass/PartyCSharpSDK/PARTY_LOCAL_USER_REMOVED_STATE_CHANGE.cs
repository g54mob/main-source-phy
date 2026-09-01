using System;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_LOCAL_USER_REMOVED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_NETWORK_HANDLE network { get; set; }

		public PARTY_LOCAL_USER_HANDLE localUser { get; set; }

		public PARTY_LOCAL_USER_REMOVED_REASON removedReason { get; set; }

		internal PARTY_LOCAL_USER_REMOVED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_LOCAL_USER_REMOVED_STATE_CHANGE localUserRemoved = stateChange.localUserRemoved;
			network = new PARTY_NETWORK_HANDLE(localUserRemoved.network);
			localUser = new PARTY_LOCAL_USER_HANDLE(localUserRemoved.localUser);
			removedReason = localUserRemoved.removedReason;
		}
	}
}
