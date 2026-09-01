using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum ModerationStatus
	{
		[EnumMember(Value = "pending")]
		Pending = 0,
		[EnumMember(Value = "rejected")]
		Rejected = 1,
		[EnumMember(Value = "approved")]
		Approved = 2,
		[EnumMember(Value = "overridden")]
		Overridden = 3
	}
}
