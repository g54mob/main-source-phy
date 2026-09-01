using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerRole
	{
		public XblMultiplayerRoleType RoleType { get; private set; }

		public string Name { get; private set; }

		public ulong[] MemberXuids { get; private set; }

		public uint TargetCount { get; private set; }

		public uint MaxMemberCount { get; private set; }

		internal XblMultiplayerRole(XGamingRuntime.Interop.XblMultiplayerRole interopStruct)
		{
			RoleType = interopStruct.GetRoleType((XGamingRuntime.Interop.XblMultiplayerRoleType x) => new XblMultiplayerRoleType(x));
			Name = interopStruct.Name.GetString();
			MemberXuids = interopStruct.GetMemberXuids((ulong x) => x);
			TargetCount = interopStruct.TargetCount;
			MaxMemberCount = interopStruct.MaxMemberCount;
		}
	}
}
