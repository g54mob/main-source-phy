using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace NATTraversal
{
	public class ExternalServer : NetworkServerSimple
	{
		private ulong clientGUID;

		public ExternalServer(ulong clientGUID)
		{
			this.clientGUID = clientGUID;
			SetNetworkConnectionClass<ExternalNetworkConnection>();
		}

		public override void OnConnected(NetworkConnection conn)
		{
			base.OnConnected(conn);
			NetworkServer.AddExternalConnection(conn);
		}

		public override void OnDisconnected(NetworkConnection conn)
		{
			if (NetworkServer.connections.Count > conn.connectionId && NetworkServer.connections[conn.connectionId] != null)
			{
				NetworkServer.RemoveExternalConnection(conn.connectionId);
			}
			base.OnDisconnected(conn);
			((NetworkManager)UnityEngine.Networking.NetworkManager.singleton).natServers.Remove(this);
			((NetworkManager)UnityEngine.Networking.NetworkManager.singleton).StartCoroutine(StopServerNextFrame());
		}

		private IEnumerator StopServerNextFrame()
		{
			yield return new WaitForEndOfFrame();
			Stop();
		}
	}
}
