public enum SimulationState
{
	SpectatorMode = 0,
	BuildMode = 1,
	BuildModeGlobalSim = 2,
	BuildModeRemoteLocalSim = 3,
	BuildModeGlobalSimRemoteLocalSim = 4,
	GlobalSimulation = 5,
	GlobalSimulationRemoteLocalSim = 6,
	LocalSimulation = 7,
	LocalSimulationRemoteLocalSim = 8,
	PendingReadyVote = 9,
	WaitingOnMachineReady = 10,
	SwitchingToLocalSimulation = 11,
	SwitchingToGlobalSimulation = 12,
	SwitchingToBuildMode = 13,
	SwitchingToSpectator = 14
}
