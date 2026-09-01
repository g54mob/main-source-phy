using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerRoleType
	{
		public string Name { get; private set; }

		public bool OwnerManaged { get; private set; }

		public XblMutableRoleSettings MutableRoleSettings { get; private set; }

		public XblMultiplayerRole[] Roles { get; private set; }

		internal XblMultiplayerRoleType(XGamingRuntime.Interop.XblMultiplayerRoleType interopStruct)
		{
			Name = interopStruct.Name.GetString();
			OwnerManaged = interopStruct.OwnerManaged.Value;
			MutableRoleSettings = interopStruct.MutableRoleSettings;
			Roles = interopStruct.GetRoles((XGamingRuntime.Interop.XblMultiplayerRole x) => new XblMultiplayerRole(x));
		}
	}
}
