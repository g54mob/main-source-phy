using System.ComponentModel;

namespace Oculus.Platform
{
	public enum AppInstallResult
	{
		[Description("UNKNOWN")]
		Unknown = 0,
		[Description("LOW_STORAGE")]
		LowStorage = 1,
		[Description("NETWORK_ERROR")]
		NetworkError = 2,
		[Description("DUPLICATE_REQUEST")]
		DuplicateRequest = 3,
		[Description("INSTALLER_ERROR")]
		InstallerError = 4,
		[Description("USER_CANCELLED")]
		UserCancelled = 5,
		[Description("AUTHORIZATION_ERROR")]
		AuthorizationError = 6,
		[Description("SUCCESS")]
		Success = 7
	}
}
