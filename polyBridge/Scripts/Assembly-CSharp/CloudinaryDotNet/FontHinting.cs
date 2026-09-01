using System.Runtime.Serialization;

namespace CloudinaryDotNet
{
	public enum FontHinting
	{
		[EnumMember(Value = "none")]
		None = 0,
		[EnumMember(Value = "slight")]
		Slight = 1,
		[EnumMember(Value = "medium")]
		Medium = 2,
		[EnumMember(Value = "full")]
		Full = 3
	}
}
