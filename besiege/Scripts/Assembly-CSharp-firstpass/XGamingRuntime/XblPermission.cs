namespace XGamingRuntime
{
	public enum XblPermission : uint
	{
		Unknown = 0u,
		CommunicateUsingText = 1000u,
		CommunicateUsingVideo = 1001u,
		CommunicateUsingVoice = 1002u,
		ViewTargetProfile = 1004u,
		ViewTargetGameHistory = 1005u,
		ViewTargetVideoHistory = 1006u,
		ViewTargetMusicHistory = 1007u,
		ViewTargetExerciseInfo = 1009u,
		ViewTargetPresence = 1011u,
		ViewTargetVideoStatus = 1012u,
		ViewTargetMusicStatus = 1013u,
		PlayMultiplayer = 1014u,
		ViewTargetUserCreatedContent = 1018u,
		BroadcastWithTwitch = 1019u,
		WriteComment = 1022u,
		ShareItem = 1024u,
		ShareTargetContentToExternalNetworks = 1025u
	}
}
