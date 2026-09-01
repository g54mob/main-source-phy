using System.Runtime.Serialization;

namespace CloudinaryDotNet.Actions
{
	public enum Role
	{
		[EnumMember(Value = "master_admin")]
		MaserAdmin = 0,
		[EnumMember(Value = "admin")]
		Admin = 1,
		[EnumMember(Value = "billing")]
		Billing = 2,
		[EnumMember(Value = "technical_admin")]
		TechnicalAdmin = 3,
		[EnumMember(Value = "reports")]
		Reports = 4,
		[EnumMember(Value = "media_library_admin")]
		MediaLibraryAdmin = 5,
		[EnumMember(Value = "media_library_user")]
		MediaLibraryUser = 6
	}
}
