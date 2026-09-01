using System;
using System.Collections.Generic;

namespace WatsonTcp
{
	public class SyncRequest
	{
		public string IpPort { get; }

		public DateTime ExpirationUtc { get; }

		public Dictionary<object, object> Metadata { get; }

		public byte[] Data { get; }

		internal string ConversationGuid { get; }

		internal SyncRequest(string ipPort, string convGuid, DateTime expirationUtc, Dictionary<object, object> metadata, byte[] data)
		{
			IpPort = ipPort;
			ConversationGuid = convGuid;
			ExpirationUtc = expirationUtc;
			Metadata = metadata;
			if (data != null)
			{
				Data = new byte[data.Length];
				Buffer.BlockCopy(data, 0, Data, 0, data.Length);
			}
		}
	}
}
