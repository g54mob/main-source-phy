using PartyCSharpSDK;
using PartyXBLCSharpSDK.Interop;

namespace PartyXBLCSharpSDK
{
	public class PARTY_XBL_ACCESSIBILITY_SETTINGS
	{
		public byte SpeechToTextEnabled { get; set; }

		public byte TextToSpeechEnabled { get; set; }

		public string LanguageCode { get; set; }

		public PARTY_GENDER Gender { get; set; }

		internal PARTY_XBL_ACCESSIBILITY_SETTINGS(PartyXBLCSharpSDK.Interop.PARTY_XBL_ACCESSIBILITY_SETTINGS interopStruct)
		{
			SpeechToTextEnabled = interopStruct.speechToTextEnabled;
			TextToSpeechEnabled = interopStruct.textToSpeechEnabled;
			LanguageCode = interopStruct.GetLanguageCode();
			Gender = interopStruct.gender;
		}
	}
}
