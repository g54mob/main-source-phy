using System;
using System.Runtime.InteropServices;
using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_SYNTHESIZE_TEXT_TO_SPEECH_COMPLETED_STATE_CHANGE : PARTY_STATE_CHANGE
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public PARTY_CHAT_CONTROL_HANDLE localChatControl { get; set; }

		public PARTY_SYNTHESIZE_TEXT_TO_SPEECH_TYPE type { get; set; }

		public string textToSynthesize { get; set; }

		public object asyncIdentifier { get; set; }

		internal PARTY_SYNTHESIZE_TEXT_TO_SPEECH_COMPLETED_STATE_CHANGE(PARTY_STATE_CHANGE_UNION stateChange, IntPtr StateChangeId)
			: base(stateChange.stateChange.stateChangeType, StateChangeId)
		{
			PartyCSharpSDK.Interop.PARTY_SYNTHESIZE_TEXT_TO_SPEECH_COMPLETED_STATE_CHANGE synthesizeTextToSpeechCompleted = stateChange.synthesizeTextToSpeechCompleted;
			result = synthesizeTextToSpeechCompleted.result;
			errorDetail = synthesizeTextToSpeechCompleted.errorDetail;
			localChatControl = new PARTY_CHAT_CONTROL_HANDLE(synthesizeTextToSpeechCompleted.localChatControl);
			type = synthesizeTextToSpeechCompleted.type;
			textToSynthesize = Converters.PtrToStringUTF8(synthesizeTextToSpeechCompleted.textToSynthesize);
			asyncIdentifier = null;
			if (synthesizeTextToSpeechCompleted.asyncIdentifier != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(synthesizeTextToSpeechCompleted.asyncIdentifier);
				asyncIdentifier = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
