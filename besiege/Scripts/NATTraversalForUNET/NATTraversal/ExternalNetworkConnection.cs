using System.Reflection;
using UnityEngine.Networking;

namespace NATTraversal
{
	public class ExternalNetworkConnection : NetworkConnection
	{
		private int m_Offset;

		private MethodInfo handleClientDisconnectMethod;

		private MethodInfo removeObserversMethod;

		public int offset
		{
			get
			{
				return m_Offset;
			}
		}

		public override void Initialize(string networkAddress, int netHostId, int netConnId, HostTopology hostTopology)
		{
			m_Offset = NetworkServer.hostTopology.MaxDefaultConnections * (netHostId + 1);
			handleClientDisconnectMethod = typeof(ClientScene).GetMethod("HandleClientDisconnect", BindingFlags.Static | BindingFlags.NonPublic);
			removeObserversMethod = typeof(NetworkConnection).GetMethod("RemoveObservers", BindingFlags.Instance | BindingFlags.NonPublic);
			base.Initialize(networkAddress, netHostId, netConnId + m_Offset, hostTopology);
		}

		public override bool TransportSend(byte[] bytes, int numBytes, int channelId, out byte error)
		{
			return NetworkTransport.Send(hostId, connectionId - m_Offset, channelId, bytes, numBytes, out error);
		}

		public new void Disconnect()
		{
			address = "";
			isReady = false;
			handleClientDisconnectMethod.Invoke(null, new object[1] { this });
			if (hostId != -1)
			{
				byte error;
				NetworkTransport.Disconnect(hostId, connectionId - m_Offset, out error);
				removeObserversMethod.Invoke(this, null);
			}
		}
	}
}
