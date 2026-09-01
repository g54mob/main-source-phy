using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_DESTROY_ENDPOINT_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_NETWORK_HANDLE network { get; set; }

		public PARTY_ENDPOINT_HANDLE localEndpoint { get; set; }

		public object asyncIdentifier { get; set; }

		internal PARTY_DESTROY_ENDPOINT_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_DESTROY_ENDPOINT_COMPLETED_STATE_CHANGE destroyEndpointCompleted = stateChange.destroyEndpointCompleted;
			result = destroyEndpointCompleted.result;
			errorDetail = destroyEndpointCompleted.errorDetail;
			network = new PARTY_NETWORK_HANDLE(destroyEndpointCompleted.network);
			localEndpoint = new PARTY_ENDPOINT_HANDLE(destroyEndpointCompleted.localEndpoint);
			asyncIdentifier = null;
			if (destroyEndpointCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(destroyEndpointCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
