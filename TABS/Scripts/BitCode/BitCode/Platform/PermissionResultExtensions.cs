namespace BitCode.Platform
{
	public static class PermissionResultExtensions
	{
		public static bool HasPermission(this IPermissionResult result)
		{
			return result.State == PermissionState.Granted;
		}
	}
}
