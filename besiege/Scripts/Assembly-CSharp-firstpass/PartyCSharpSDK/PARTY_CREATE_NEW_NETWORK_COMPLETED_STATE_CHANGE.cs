using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_CREATE_NEW_NETWORK_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_LOCAL_USER_HANDLE localUser { get; set; }

		public PARTY_NETWORK_CONFIGURATION networkConfiguration { get; set; }

		public uint regionCount { get; set; }

		public PARTY_REGION[] regions { get; set; }

		public object asyncIdentifier { get; set; }

		public PARTY_NETWORK_DESCRIPTOR networkDescriptor { get; set; }

		public string appliedInitialInvitationIdentifier { get; set; }

		internal PARTY_CREATE_NEW_NETWORK_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_CREATE_NEW_NETWORK_COMPLETED_STATE_CHANGE createNewNetworkCompleted = stateChange.createNewNetworkCompleted;
			result = createNewNetworkCompleted.result;
			errorDetail = createNewNetworkCompleted.errorDetail;
			localUser = new PARTY_LOCAL_USER_HANDLE(createNewNetworkCompleted.localUser);
			networkConfiguration = new PARTY_NETWORK_CONFIGURATION(createNewNetworkCompleted.networkConfiguration);
			regionCount = createNewNetworkCompleted.regionCount;
			regions = Converters.PtrToClassArray(createNewNetworkCompleted.regions, regionCount, (PartyCSharpSDK.Interop.PARTY_REGION x) => new PARTY_REGION(x));
			asyncIdentifier = null;
			if (createNewNetworkCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(createNewNetworkCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
			networkDescriptor = new PARTY_NETWORK_DESCRIPTOR(createNewNetworkCompleted.networkDescriptor);
			appliedInitialInvitationIdentifier = Converters.PtrToStringUTF8(createNewNetworkCompleted.appliedInitialInvitationIdentifier);
		}
	}
}
