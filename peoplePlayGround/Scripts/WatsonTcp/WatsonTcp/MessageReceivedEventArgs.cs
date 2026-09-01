using System;
using System.Collections.Generic;

namespace WatsonTcp
{
	public class MessageReceivedEventArgs : EventArgs
	{
		public string IpPort { get; }

		public Dictionary<object, object> Metadata { get; }

		public byte[] Data { get; }

		internal MessageReceivedEventArgs(string ipPort, Dictionary<object, object> metadata, byte[] data)
		{
			IpPort = ipPort;
			Metadata = metadata;
			Data = data;
		}
	}
}
