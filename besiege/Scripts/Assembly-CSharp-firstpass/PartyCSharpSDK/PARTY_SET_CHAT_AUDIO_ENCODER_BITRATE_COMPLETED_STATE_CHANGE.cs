using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_SET_CHAT_AUDIO_ENCODER_BITRATE_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_CHAT_CONTROL_HANDLE localChatControl { get; set; }

		public uint bitrate { get; set; }

		public object asyncIdentifier { get; set; }

		internal PARTY_SET_CHAT_AUDIO_ENCODER_BITRATE_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_SET_CHAT_AUDIO_ENCODER_BITRATE_COMPLETED_STATE_CHANGE setChatAudioEncoderBitrateCompleted = stateChange.setChatAudioEncoderBitrateCompleted;
			result = setChatAudioEncoderBitrateCompleted.result;
			errorDetail = setChatAudioEncoderBitrateCompleted.errorDetail;
			localChatControl = new PARTY_CHAT_CONTROL_HANDLE(setChatAudioEncoderBitrateCompleted.localChatControl);
			bitrate = setChatAudioEncoderBitrateCompleted.bitrate;
			asyncIdentifier = null;
			if (setChatAudioEncoderBitrateCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(setChatAudioEncoderBitrateCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
