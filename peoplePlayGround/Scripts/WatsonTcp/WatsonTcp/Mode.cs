using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WatsonTcp
{
	[JsonConverter(typeof(StringEnumConverter))]
	internal enum Mode
	{
		[EnumMember(Value = "Tcp")]
		Tcp = 0,
		[EnumMember(Value = "Ssl")]
		Ssl = 1
	}
}
