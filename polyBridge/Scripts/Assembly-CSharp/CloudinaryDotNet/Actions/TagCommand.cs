using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum TagCommand
	{
		[EnumMember(Value = "add")]
		Add = 0,
		[EnumMember(Value = "remove")]
		Remove = 1,
		[EnumMember(Value = "replace")]
		Replace = 2,
		[EnumMember(Value = "set_exclusive")]
		SetExclusive = 3,
		[EnumMember(Value = "remove_all")]
		RemoveAll = 4
	}
}
