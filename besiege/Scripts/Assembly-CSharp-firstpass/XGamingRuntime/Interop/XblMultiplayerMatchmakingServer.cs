namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerMatchmakingServer
	{
		internal readonly XblMatchmakingStatus Status;

		internal readonly UTF8StringPtr StatusDetails;

		internal readonly uint TypicalWaitInSeconds;

		internal readonly XblMultiplayerSessionReference TargetSessionRef;

		internal XblMultiplayerMatchmakingServer(XGamingRuntime.XblMultiplayerMatchmakingServer publicObject, DisposableCollection disposableCollection)
		{
			Status = publicObject.Status;
			StatusDetails = new UTF8StringPtr(publicObject.StatusDetails, disposableCollection);
			TypicalWaitInSeconds = publicObject.TypicalWaitInSeconds;
			TargetSessionRef = new XblMultiplayerSessionReference(publicObject.TargetSessionRef);
		}
	}
}
