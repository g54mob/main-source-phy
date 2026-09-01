namespace XGamingRuntime
{
	public enum XblMultiplayerMatchStatus : uint
	{
		None = 0u,
		SubmittingMatchTicket = 1u,
		Searching = 2u,
		Found = 3u,
		Joining = 4u,
		WaitingForRemoteClientsToJoin = 5u,
		Measuring = 6u,
		UploadingQosMeasurements = 7u,
		WaitingForRemoteClientsToUploadQos = 8u,
		Evaluating = 9u,
		Completed = 10u,
		Resubmitting = 11u,
		Expired = 12u,
		Canceling = 13u,
		Canceled = 14u,
		Failed = 15u
	}
}
