using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionMemberRole
	{
		public string RoleTypeName { get; private set; }

		public string RoleName { get; private set; }

		internal XblMultiplayerSessionMemberRole(XGamingRuntime.Interop.XblMultiplayerSessionMemberRole interopStruct)
		{
			RoleTypeName = interopStruct.roleTypeName.GetString();
			RoleName = interopStruct.roleName.GetString();
		}
	}
}
