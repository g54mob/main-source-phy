using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum AccessType
	{
		[EnumMember(Value = "anonymous")]
		Anonymous = 0,
		[EnumMember(Value = "token")]
		Token = 1
	}
}
