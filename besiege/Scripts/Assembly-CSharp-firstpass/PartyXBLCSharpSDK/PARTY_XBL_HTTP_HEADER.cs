using PartyCSharpSDK;
using PartyXBLCSharpSDK.Interop;

namespace PartyXBLCSharpSDK
{
	public class PARTY_XBL_HTTP_HEADER
	{
		public string name { get; set; }

		public string value { get; set; }

		internal PARTY_XBL_HTTP_HEADER(PartyXBLCSharpSDK.Interop.PARTY_XBL_HTTP_HEADER interopStruct)
		{
			name = Converters.PtrToStringUTF8(interopStruct.name);
			value = Converters.PtrToStringUTF8(interopStruct.value);
		}
	}
}
