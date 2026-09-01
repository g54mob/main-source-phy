using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WatsonTcp
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MessageStatus
	{
		[EnumMember(Value = "Normal")]
		Normal = 0,
		[EnumMember(Value = "Success")]
		Success = 1,
		[EnumMember(Value = "Failure")]
		Failure = 2,
		[EnumMember(Value = "AuthRequired")]
		AuthRequired = 3,
		[EnumMember(Value = "AuthRequested")]
		AuthRequested = 4,
		[EnumMember(Value = "AuthSuccess")]
		AuthSuccess = 5,
		[EnumMember(Value = "AuthFailure")]
		AuthFailure = 6,
		[EnumMember(Value = "Removed")]
		Removed = 7,
		[EnumMember(Value = "Shutdown")]
		Shutdown = 8,
		[EnumMember(Value = "Heartbeat")]
		Heartbeat = 9,
		[EnumMember(Value = "Timeout")]
		Timeout = 10
	}
}
