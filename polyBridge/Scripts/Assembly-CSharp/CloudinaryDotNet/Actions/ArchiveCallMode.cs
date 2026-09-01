using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum ArchiveCallMode
	{
		[EnumMember(Value = "download")]
		Download = 0,
		[EnumMember(Value = "create")]
		Create = 1
	}
}
