using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblPresenceBroadcastRecord
	{
		public string BroadcastId { get; private set; }

		public string Session { get; private set; }

		public XblPresenceBroadcastProvider Provider { get; private set; }

		public uint ViewerCount { get; private set; }

		public DateTime StartTime { get; private set; }

		internal XblPresenceBroadcastRecord(XGamingRuntime.Interop.XblPresenceBroadcastRecord interopRecord)
		{
			BroadcastId = interopRecord.broadcastId.GetString();
			Session = Converters.ByteArrayToString(interopRecord.session);
			Provider = interopRecord.provider;
			ViewerCount = interopRecord.viewerCount;
			StartTime = interopRecord.startTime.DateTime;
		}
	}
}
