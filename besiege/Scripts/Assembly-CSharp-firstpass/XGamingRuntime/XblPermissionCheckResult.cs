using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblPermissionCheckResult
	{
		public bool IsAllowed { get; private set; }

		public ulong TargetXuid { get; private set; }

		public XblAnonymousUserType TargetUserType { get; private set; }

		public XblPermission PermissionRequested { get; private set; }

		public XblPermissionDenyReasonDetails[] Reasons { get; private set; }

		internal XblPermissionCheckResult(XGamingRuntime.Interop.XblPermissionCheckResult interopStruct)
		{
			IsAllowed = interopStruct.isAllowed.Value;
			TargetXuid = interopStruct.targetXuid;
			TargetUserType = interopStruct.targetUserType;
			PermissionRequested = interopStruct.permissionRequested;
			Reasons = interopStruct.GetReasons((XGamingRuntime.Interop.XblPermissionDenyReasonDetails x) => new XblPermissionDenyReasonDetails(x));
		}
	}
}
