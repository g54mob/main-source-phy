using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerMatchmakingServer
	{
		public XblMatchmakingStatus Status { get; private set; }

		public string StatusDetails { get; private set; }

		public uint TypicalWaitInSeconds { get; private set; }

		public XblMultiplayerSessionReference TargetSessionRef { get; private set; }

		internal XblMultiplayerMatchmakingServer(XGamingRuntime.Interop.XblMultiplayerMatchmakingServer interopStruct)
		{
			Status = interopStruct.Status;
			StatusDetails = interopStruct.StatusDetails.GetString();
			TypicalWaitInSeconds = interopStruct.TypicalWaitInSeconds;
			TargetSessionRef = new XblMultiplayerSessionReference(interopStruct.TargetSessionRef);
		}
	}
}
