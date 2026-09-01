using System;

namespace XGamingRuntime.Interop
{
	internal struct XblMultiplayerEvent
	{
		internal readonly int Result;

		internal readonly UTF8StringPtr ErrorMessage;

		internal readonly IntPtr Context;

		internal readonly XblMultiplayerEventType EventType;

		internal readonly XblMultiplayerEventArgsHandle EventArgsHandle;

		internal readonly XblMultiplayerSessionType SessionType;
	}
}
