using System;

namespace WatsonTcp
{
	public class DisconnectionEventArgs : EventArgs
	{
		public string IpPort { get; }

		public DisconnectReason Reason { get; }

		internal DisconnectionEventArgs(string ipPort, DisconnectReason reason)
		{
			IpPort = ipPort;
			Reason = reason;
		}
	}
}
