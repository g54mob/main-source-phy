using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WatsonTcp
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DisconnectReason
	{
		[EnumMember(Value = "Normal")]
		Normal = 0,
		[EnumMember(Value = "Removed")]
		Removed = 1,
		[EnumMember(Value = "Timeout")]
		Timeout = 2,
		[EnumMember(Value = "Shutdown")]
		Shutdown = 3,
		[EnumMember(Value = "AuthFailure")]
		AuthFailure = 4
	}
}
