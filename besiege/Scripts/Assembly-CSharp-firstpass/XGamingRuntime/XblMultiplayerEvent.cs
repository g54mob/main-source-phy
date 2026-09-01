using System;
using System.Runtime.InteropServices;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerEvent
	{
		public int Result { get; private set; }

		public string ErrorMessage { get; private set; }

		public object Context { get; private set; }

		public XblMultiplayerEventType EventType { get; private set; }

		public XblMultiplayerEventArgsHandle EventArgsHandle { get; private set; }

		public XblMultiplayerSessionType SessionType { get; private set; }

		internal XblMultiplayerEvent(XGamingRuntime.Interop.XblMultiplayerEvent interopStruct)
		{
			Result = interopStruct.Result;
			ErrorMessage = interopStruct.ErrorMessage.GetString();
			EventType = interopStruct.EventType;
			EventArgsHandle = new XblMultiplayerEventArgsHandle(interopStruct.EventArgsHandle);
			SessionType = interopStruct.SessionType;
			Context = null;
			if (interopStruct.Context != IntPtr.Zero)
			{
				GCHandle gCHandle = GCHandle.FromIntPtr(interopStruct.Context);
				Context = gCHandle.Target;
				gCHandle.Free();
			}
		}
	}
}
