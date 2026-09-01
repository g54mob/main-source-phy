using UnityEngine.Networking;

namespace NATTraversal
{
	public static class NetworkConnectionExtension
	{
		public static void DisconnectConnection(this NetworkConnection conn)
		{
			if (conn.GetType() == typeof(ExternalNetworkConnection))
			{
				((ExternalNetworkConnection)conn).Disconnect();
			}
			else
			{
				conn.Disconnect();
			}
		}
	}
}
