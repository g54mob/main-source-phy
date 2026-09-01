using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum AssetType
	{
		[EnumMember(Value = "upload")]
		Upload = 0,
		[EnumMember(Value = "private")]
		Private = 1,
		[EnumMember(Value = "authenticated")]
		Authenticated = 2
	}
}
