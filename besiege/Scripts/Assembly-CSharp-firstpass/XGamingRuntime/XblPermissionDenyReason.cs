namespace XGamingRuntime
{
	public enum XblPermissionDenyReason : uint
	{
		Unknown = 0u,
		NotAllowed = 2u,
		MissingPrivilege = 3u,
		PrivilegeRestrictsTarget = 4u,
		BlockListRestrictsTarget = 5u,
		MuteListRestrictsTarget = 7u,
		PrivacySettingsRestrictsTarget = 9u
	}
}
