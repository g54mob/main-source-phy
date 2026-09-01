using System;

namespace WatsonTcp
{
	public class WatsonTcpServerCallbacks
	{
		private Func<SyncRequest, SyncResponse> _SyncRequestReceived;

		public Func<SyncRequest, SyncResponse> SyncRequestReceived
		{
			get
			{
				return _SyncRequestReceived;
			}
			set
			{
				_SyncRequestReceived = value;
			}
		}

		internal SyncResponse HandleSyncRequestReceived(SyncRequest req)
		{
			SyncResponse result = null;
			if (SyncRequestReceived != null)
			{
				try
				{
					result = SyncRequestReceived(req);
				}
				catch (Exception)
				{
				}
			}
			return result;
		}
	}
}
