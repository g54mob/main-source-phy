using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum MetadataFieldType
	{
		[EnumMember(Value = "string")]
		String = 0,
		[EnumMember(Value = "integer")]
		Integer = 1,
		[EnumMember(Value = "date")]
		Date = 2,
		[EnumMember(Value = "enum")]
		Enum = 3,
		[EnumMember(Value = "set")]
		Set = 4
	}
}
