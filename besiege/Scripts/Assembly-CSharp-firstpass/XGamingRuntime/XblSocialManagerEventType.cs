namespace XGamingRuntime
{
	public enum XblSocialManagerEventType : uint
	{
		UsersAddedToSocialGraph = 0u,
		UsersRemovedFromSocialGraph = 1u,
		PresenceChanged = 2u,
		ProfilesChanged = 3u,
		SocialRelationshipsChanged = 4u,
		LocalUserAdded = 5u,
		SocialUserGroupLoaded = 6u,
		SocialUserGroupUpdated = 7u,
		UnknownEvent = 8u
	}
}
