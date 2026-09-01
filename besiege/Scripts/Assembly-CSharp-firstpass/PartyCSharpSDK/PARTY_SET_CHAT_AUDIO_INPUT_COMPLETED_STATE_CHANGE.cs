using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_SET_CHAT_AUDIO_INPUT_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_CHAT_CONTROL_HANDLE localChatControl { get; set; }

		public PARTY_AUDIO_DEVICE_SELECTION_TYPE audioDeviceSelectionType { get; set; }

		public string audioDeviceSelectionContext { get; set; }

		public object asyncIdentifier { get; set; }

		internal PARTY_SET_CHAT_AUDIO_INPUT_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_SET_CHAT_AUDIO_INPUT_COMPLETED_STATE_CHANGE setChatAudioInputCompleted = stateChange.setChatAudioInputCompleted;
			result = setChatAudioInputCompleted.result;
			errorDetail = setChatAudioInputCompleted.errorDetail;
			localChatControl = new PARTY_CHAT_CONTROL_HANDLE(setChatAudioInputCompleted.localChatControl);
			audioDeviceSelectionType = setChatAudioInputCompleted.audioDeviceSelectionType;
			audioDeviceSelectionContext = Converters.PtrToStringUTF8(setChatAudioInputCompleted.audioDeviceSelectionContext);
			asyncIdentifier = null;
			if (setChatAudioInputCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(setChatAudioInputCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
