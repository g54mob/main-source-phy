using System;

namespace Photon.Bolt
{
	internal static class NetworkIdAllocator
	{
		private static uint EntityId;

		private static uint ConnectionId;

		public static uint LocalConnectionId => ConnectionId;

		public static void Reset(uint connectionId)
		{
			EntityId = 0u;
			ConnectionId = connectionId;
		}

		public static void Assigned(uint connectionId)
		{
			ConnectionId = connectionId;
		}

		public static NetworkId Allocate()
		{
			if (ConnectionId == 0)
			{
				throw new InvalidOperationException("Connection id not assigned");
			}
			return new NetworkId(ConnectionId, ++EntityId);
		}
	}
}
