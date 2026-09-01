using System;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_REMOTE_DEVICE_JOINED_NETWORK_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_DEVICE_HANDLE device { get; set; }

		public PARTY_NETWORK_HANDLE network { get; set; }

		internal PARTY_REMOTE_DEVICE_JOINED_NETWORK_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_REMOTE_DEVICE_JOINED_NETWORK_STATE_CHANGE remoteDeviceJoinedNetwork = stateChange.remoteDeviceJoinedNetwork;
			device = new PARTY_DEVICE_HANDLE(remoteDeviceJoinedNetwork.device);
			network = new PARTY_NETWORK_HANDLE(remoteDeviceJoinedNetwork.network);
		}
	}
}
