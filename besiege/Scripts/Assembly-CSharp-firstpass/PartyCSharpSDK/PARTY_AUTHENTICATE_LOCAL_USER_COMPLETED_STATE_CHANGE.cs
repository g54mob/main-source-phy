using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_AUTHENTICATE_LOCAL_USER_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_NETWORK_HANDLE network { get; set; }

		public PARTY_LOCAL_USER_HANDLE localUser { get; set; }

		public string invitationIdentifier { get; set; }

		public object asyncIdentifier { get; set; }

		internal PARTY_AUTHENTICATE_LOCAL_USER_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_AUTHENTICATE_LOCAL_USER_COMPLETED_STATE_CHANGE authenticateLocalUserCompleted = stateChange.authenticateLocalUserCompleted;
			result = authenticateLocalUserCompleted.result;
			errorDetail = authenticateLocalUserCompleted.errorDetail;
			network = new PARTY_NETWORK_HANDLE(authenticateLocalUserCompleted.network);
			localUser = new PARTY_LOCAL_USER_HANDLE(authenticateLocalUserCompleted.localUser);
			invitationIdentifier = Converters.PtrToStringUTF8(authenticateLocalUserCompleted.invitationIdentifier);
			asyncIdentifier = null;
			if (authenticateLocalUserCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(authenticateLocalUserCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
