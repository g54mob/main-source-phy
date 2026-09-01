using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_CREATE_INVITATION_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_NETWORK_HANDLE network { get; set; }

		public PARTY_LOCAL_USER_HANDLE localUser { get; set; }

		public object asyncIdentifier { get; set; }

		public PARTY_INVITATION_HANDLE invitation { get; set; }

		internal PARTY_CREATE_INVITATION_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_CREATE_INVITATION_COMPLETED_STATE_CHANGE createInvitationCompleted = stateChange.createInvitationCompleted;
			result = createInvitationCompleted.result;
			errorDetail = createInvitationCompleted.errorDetail;
			network = new PARTY_NETWORK_HANDLE(createInvitationCompleted.network);
			localUser = new PARTY_LOCAL_USER_HANDLE(createInvitationCompleted.localUser);
			asyncIdentifier = null;
			if (createInvitationCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(createInvitationCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
			invitation = new PARTY_INVITATION_HANDLE(createInvitationCompleted.invitation);
		}
	}
}
