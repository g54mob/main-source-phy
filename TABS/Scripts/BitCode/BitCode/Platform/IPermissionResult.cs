using BitCode.Users;

namespace BitCode.Platform
{
	public interface IPermissionResult
	{
		PermissionState State { get; }

		PermissionDetail Detail { get; }

		ILocalAccount LocalUser { get; }

		IRemoteAccount TargetUser { get; }
	}
}
