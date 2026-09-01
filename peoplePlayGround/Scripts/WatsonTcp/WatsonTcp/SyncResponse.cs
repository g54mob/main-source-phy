using System;
using System.Collections.Generic;
using System.Text;

namespace WatsonTcp
{
	public class SyncResponse
	{
		public Dictionary<object, object> Metadata { get; }

		public byte[] Data { get; }

		internal DateTime ExpirationUtc { get; set; }

		public SyncResponse(SyncRequest req, string data)
		{
			if (req == null)
			{
				throw new ArgumentNullException("req");
			}
			ExpirationUtc = req.ExpirationUtc;
			Metadata = new Dictionary<object, object>();
			if (string.IsNullOrEmpty(data))
			{
				Data = new byte[0];
			}
			else
			{
				Data = Encoding.UTF8.GetBytes(data);
			}
		}

		public SyncResponse(SyncRequest req, byte[] data)
		{
			if (req == null)
			{
				throw new ArgumentNullException("req");
			}
			ExpirationUtc = req.ExpirationUtc;
			Metadata = new Dictionary<object, object>();
			Data = data;
		}

		public SyncResponse(SyncRequest req, Dictionary<object, object> metadata, string data)
		{
			if (req == null)
			{
				throw new ArgumentNullException("req");
			}
			ExpirationUtc = req.ExpirationUtc;
			Metadata = metadata;
			if (string.IsNullOrEmpty(data))
			{
				Data = new byte[0];
			}
			else
			{
				Data = Encoding.UTF8.GetBytes(data);
			}
		}

		public SyncResponse(SyncRequest req, Dictionary<object, object> metadata, byte[] data)
		{
			if (req == null)
			{
				throw new ArgumentNullException("req");
			}
			ExpirationUtc = req.ExpirationUtc;
			Metadata = metadata;
			Data = data;
		}

		internal SyncResponse(DateTime expirationUtc, Dictionary<object, object> metadata, byte[] data)
		{
			ExpirationUtc = expirationUtc;
			Metadata = metadata;
			Data = data;
		}
	}
}
