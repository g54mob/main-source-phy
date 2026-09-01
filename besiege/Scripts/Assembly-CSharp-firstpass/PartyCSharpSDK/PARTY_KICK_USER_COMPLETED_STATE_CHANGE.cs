using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_KICK_USER_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_NETWORK_HANDLE network { get; set; }

		public string kickedEntityId { get; set; }

		public object asyncIdentifier { get; set; }

		internal PARTY_KICK_USER_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_KICK_USER_COMPLETED_STATE_CHANGE kickUserCompleted = stateChange.kickUserCompleted;
			result = kickUserCompleted.result;
			errorDetail = kickUserCompleted.errorDetail;
			network = new PARTY_NETWORK_HANDLE(kickUserCompleted.network);
			kickedEntityId = Converters.PtrToStringUTF8(kickUserCompleted.kickedEntityId);
			asyncIdentifier = null;
			if (kickUserCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(kickUserCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
