using System.Runtime.Serialization;

namespace CloudinaryDotNet
{
	public enum FontAntialiasing
	{
		[EnumMember(Value = "none")]
		None = 0,
		[EnumMember(Value = "gray")]
		Gray = 1,
		[EnumMember(Value = "subpixel")]
		Subpixel = 2,
		[EnumMember(Value = "fast")]
		Fast = 3,
		[EnumMember(Value = "good")]
		Good = 4,
		[EnumMember(Value = "best")]
		Best = 5
	}
}
