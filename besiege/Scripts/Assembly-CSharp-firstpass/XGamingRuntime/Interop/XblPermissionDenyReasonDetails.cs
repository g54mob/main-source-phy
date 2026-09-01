namespace XGamingRuntime.Interop
{
	internal struct XblPermissionDenyReasonDetails
	{
		internal readonly XblPermissionDenyReason reason;

		internal readonly XblPrivilege restrictedPrivilege;

		internal readonly XblPrivacySetting restrictedPrivacySetting;

		internal XblPermissionDenyReasonDetails(XGamingRuntime.XblPermissionDenyReasonDetails publicObject)
		{
			reason = publicObject.Reason;
			restrictedPrivilege = publicObject.RestrictedPrivilege;
			restrictedPrivacySetting = publicObject.RestrictedPrivacySetting;
		}
	}
}
