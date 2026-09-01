namespace XGamingRuntime
{
	public enum XblMultiplayerEventType : uint
	{
		UserAdded = 0u,
		UserRemoved = 1u,
		MemberJoined = 2u,
		MemberLeft = 3u,
		MemberPropertyChanged = 4u,
		LocalMemberPropertyWriteCompleted = 5u,
		LocalMemberConnectionAddressWriteCompleted = 6u,
		SessionPropertyChanged = 7u,
		SessionPropertyWriteCompleted = 8u,
		SessionSynchronizedPropertyWriteCompleted = 9u,
		HostChanged = 10u,
		SynchronizedHostWriteCompleted = 11u,
		JoinabilityStateChanged = 12u,
		PerformQosMeasurements = 13u,
		FindMatchCompleted = 14u,
		JoinGameCompleted = 15u,
		LeaveGameCompleted = 16u,
		JoinLobbyCompleted = 17u,
		ClientDisconnectedFromMultiplayerService = 18u,
		InviteSent = 19u,
		TournamentRegistrationStateChanged = 20u,
		TournamentGameSessionReady = 21u,
		ArbitrationComplete = 22u
	}
}
