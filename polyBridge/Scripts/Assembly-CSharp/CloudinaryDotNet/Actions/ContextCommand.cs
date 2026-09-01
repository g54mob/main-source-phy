using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum ContextCommand
	{
		[EnumMember(Value = "add")]
		Add = 0,
		[EnumMember(Value = "remove_all")]
		RemoveAll = 1
	}
}
