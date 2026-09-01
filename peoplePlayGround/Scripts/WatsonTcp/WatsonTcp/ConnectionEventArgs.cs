using System;

namespace WatsonTcp
{
	public class ConnectionEventArgs : EventArgs
	{
		public string IpPort { get; }

		internal ConnectionEventArgs(string ipPort)
		{
			IpPort = ipPort;
		}
	}
}
