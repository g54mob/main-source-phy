namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionMemberRole
	{
		internal readonly UTF8StringPtr roleTypeName;

		internal readonly UTF8StringPtr roleName;

		internal XblMultiplayerSessionMemberRole(XGamingRuntime.XblMultiplayerSessionMemberRole publicObject, DisposableCollection disposableCollection)
		{
			roleTypeName = new UTF8StringPtr(publicObject.RoleTypeName, disposableCollection);
			roleName = new UTF8StringPtr(publicObject.RoleName, disposableCollection);
		}
	}
}
