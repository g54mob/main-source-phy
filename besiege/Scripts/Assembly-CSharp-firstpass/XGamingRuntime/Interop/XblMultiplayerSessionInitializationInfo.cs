namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerSessionInitializationInfo
	{
		internal readonly XblMultiplayerInitializationStage Stage;

		internal readonly TimeT StageStartTime;

		internal readonly uint Episode;

		internal XblMultiplayerSessionInitializationInfo(XGamingRuntime.XblMultiplayerSessionInitializationInfo publicObject)
		{
			Stage = publicObject.Stage;
			StageStartTime = new TimeT(publicObject.StageStartTime);
			Episode = publicObject.Episode;
		}
	}
}
