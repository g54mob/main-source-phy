using System;

namespace XGamingRuntime
{
	[Flags]
	public enum XblMultiplayerSessionChangeTypes : uint
	{
		None = 0u,
		Everything = 1u,
		HostDeviceTokenChange = 2u,
		InitializationStateChange = 4u,
		MatchmakingStatusChange = 8u,
		MemberListChange = 0x10u,
		MemberStatusChange = 0x20u,
		SessionJoinabilityChange = 0x40u,
		CustomPropertyChange = 0x80u,
		MemberCustomPropertyChange = 0x100u,
		TournamentPropertyChange = 0x200u,
		ArbitrationPropertyChange = 0x400u
	}
}
