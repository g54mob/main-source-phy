using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionInitializationInfo
	{
		public XblMultiplayerInitializationStage Stage { get; private set; }

		public DateTime StageStartTime { get; private set; }

		public uint Episode { get; private set; }

		internal XblMultiplayerSessionInitializationInfo(XGamingRuntime.Interop.XblMultiplayerSessionInitializationInfo interopStruct)
		{
			Stage = interopStruct.Stage;
			StageStartTime = interopStruct.StageStartTime.DateTime;
			Episode = interopStruct.Episode;
		}
	}
}
