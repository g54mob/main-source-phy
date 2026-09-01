namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionCapabilities
	{
		internal readonly NativeBool Connectivity;

		internal readonly NativeBool Team;

		internal readonly NativeBool Arbitration;

		internal readonly NativeBool SuppressPresenceActivityCheck;

		internal readonly NativeBool Gameplay;

		internal readonly NativeBool Large;

		internal readonly NativeBool ConnectionRequiredForActiveMembers;

		internal readonly NativeBool UserAuthorizationStyle;

		internal readonly NativeBool Crossplay;

		internal readonly NativeBool Searchable;

		internal readonly NativeBool HasOwners;

		internal XblMultiplayerSessionCapabilities(XGamingRuntime.XblMultiplayerSessionCapabilities publicObject)
		{
			Connectivity = new NativeBool(publicObject.Connectivity);
			Team = new NativeBool(publicObject.Team);
			Arbitration = new NativeBool(publicObject.Arbitration);
			SuppressPresenceActivityCheck = new NativeBool(publicObject.SuppressPresenceActivityCheck);
			Gameplay = new NativeBool(publicObject.Gameplay);
			Large = new NativeBool(publicObject.Large);
			ConnectionRequiredForActiveMembers = new NativeBool(publicObject.ConnectionRequiredForActiveMembers);
			UserAuthorizationStyle = new NativeBool(publicObject.UserAuthorizationStyle);
			Crossplay = new NativeBool(publicObject.Crossplay);
			Searchable = new NativeBool(publicObject.Searchable);
			HasOwners = new NativeBool(publicObject.HasOwners);
		}
	}
}
