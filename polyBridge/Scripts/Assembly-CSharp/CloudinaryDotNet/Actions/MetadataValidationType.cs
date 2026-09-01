using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum MetadataValidationType
	{
		[EnumMember(Value = "greater_than")]
		GreaterThan = 0,
		[EnumMember(Value = "less_than")]
		LessThan = 1,
		[EnumMember(Value = "strlen")]
		StringLength = 2,
		[EnumMember(Value = "and")]
		And = 3
	}
}
