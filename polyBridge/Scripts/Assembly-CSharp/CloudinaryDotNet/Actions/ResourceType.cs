using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum ResourceType
	{
		[EnumMember(Value = "image")]
		Image = 0,
		[EnumMember(Value = "raw")]
		Raw = 1,
		[EnumMember(Value = "video")]
		Video = 2,
		[EnumMember(Value = "auto")]
		Auto = 3
	}
}
