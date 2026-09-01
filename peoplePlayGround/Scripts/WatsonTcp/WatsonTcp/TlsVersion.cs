using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WatsonTcp
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TlsVersion
	{
		Tls12 = 0,
		Tls13 = 1
	}
}
