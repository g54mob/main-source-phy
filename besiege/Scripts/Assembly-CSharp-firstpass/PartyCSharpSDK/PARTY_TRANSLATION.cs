using PartyCSharpSDK.Interop;

namespace PartyCSharpSDK
{
	public class PARTY_TRANSLATION
	{
		public PARTY_STATE_CHANGE_RESULT result { get; set; }

		public uint errorDetail { get; set; }

		public string languageCode { get; set; }

		public PARTY_TRANSLATION_RECEIVED_OPTIONS options { get; set; }

		public string translation { get; set; }

		internal PARTY_TRANSLATION(PartyCSharpSDK.Interop.PARTY_TRANSLATION interopStruct)
		{
			result = interopStruct.result;
			errorDetail = interopStruct.errorDetail;
			languageCode = Converters.PtrToStringUTF8(interopStruct.languageCode);
			options = interopStruct.options;
			translation = Converters.PtrToStringUTF8(interopStruct.translation);
		}
	}
}
