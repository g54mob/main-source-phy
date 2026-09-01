using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

namespace NATTraversal
{
	public class MigrationManager : NetworkMigrationManager
	{
		private const string TAG = "Migration Manager: ";

		protected NetworkManager networkManager;

		protected ExtraPeerInfoMessage newHost;

		private MethodInfo onPeerClientAuthorityMethod;

		private MethodInfo handleClientDisconnectMethod;

		private FieldInfo oldServerConnectionIDField;

		private FieldInfo peersField;

		private FieldInfo clientField;

		public virtual void Start()
		{
			networkManager = (NetworkManager)UnityEngine.Networking.NetworkManager.singleton;
			Reset(-1);
			onPeerClientAuthorityMethod = typeof(NetworkMigrationManager).GetMethod("OnPeerClientAuthority", BindingFlags.Instance | BindingFlags.NonPublic);
			oldServerConnectionIDField = typeof(NetworkMigrationManager).GetField("m_OldServerConnectionId", BindingFlags.Instance | BindingFlags.NonPublic);
			peersField = typeof(NetworkMigrationManager).GetField("m_Peers", BindingFlags.Instance | BindingFlags.NonPublic);
			handleClientDisconnectMethod = typeof(ClientScene).GetMethod("HandleClientDisconnect", BindingFlags.Static | BindingFlags.NonPublic);
			clientField = typeof(NetworkMigrationManager).GetField("m_Client", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public new virtual void Initialize(NetworkClient directClient, MatchInfo newMatchInfo)
		{
			base.Initialize(directClient, newMatchInfo);
			base.client.UnregisterHandler(11);
			base.client.RegisterHandlerSafe(11, EmptyMethod);
			base.client.RegisterHandler(MsgType.ExtraPeerInfo, OnExtraPeerInfo);
		}

		public virtual bool FindNewHost(out ExtraPeerInfoMessage newHostInfo, out bool youAreNewHost)
		{
			if (base.peers == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Migration Manager: NetworkMigrationManager FindLowestHost no peers");
				}
				newHostInfo = null;
				youAreNewHost = false;
				return false;
			}
			if (LogFilter.currentLogLevel == 0)
			{
				Debug.Log("Migration Manager: NetworkMigrationManager FindLowestHost");
			}
			newHostInfo = new ExtraPeerInfoMessage();
			newHostInfo.connectionId = 50000;
			newHostInfo.address = "";
			newHostInfo.port = 0;
			int num = -1;
			youAreNewHost = false;
			for (int i = 0; i < base.peers.Length; i++)
			{
				PeerInfoMessage peerInfoMessage = base.peers[i];
				if (peerInfoMessage.connectionId != 0 && !peerInfoMessage.isHost)
				{
					if (peerInfoMessage.isYou)
					{
						num = peerInfoMessage.connectionId;
					}
					if (peerInfoMessage.connectionId < newHostInfo.connectionId)
					{
						newHostInfo = (ExtraPeerInfoMessage)peerInfoMessage;
					}
				}
			}
			if (newHostInfo.connectionId == 50000)
			{
				return false;
			}
			if (newHostInfo.connectionId == num)
			{
				youAreNewHost = true;
			}
			if (LogFilter.currentLogLevel == 0)
			{
				Debug.Log("Migration Manager: FindNewHost new host is " + newHostInfo.address);
			}
			return true;
		}

		public new virtual bool BecomeNewHost(int port)
		{
			bool num = base.BecomeNewHost(port);
			if (num)
			{
				networkManager.hostExternalIP = networkManager.externalIP;
				networkManager.hostExternalIPv6 = networkManager.externalIPv6;
				networkManager.hostInternalIP = Network.player.ipAddress;
				networkManager.hostInternalIPv6 = networkManager.getLocalIPv6();
				networkManager.client.UnregisterHandler(11);
				networkManager.client.RegisterHandlerSafe(11, EmptyMethod);
				networkManager.client.RegisterHandler(MsgType.ExtraPeerInfo, OnExtraPeerInfo);
				networkManager.natHelper.StopPunchingThrough();
				networkManager.StartCoroutine(networkManager.natHelper.startListeningForPunchthrough(networkManager.OnHolePunchedServer));
				SendPeerInfo();
			}
			return num;
		}

		public new virtual void LostHostOnHost()
		{
			base.LostHostOnHost();
			clientField.SetValue(this, null);
		}

		public new virtual bool LostHostOnClient(NetworkConnection conn)
		{
			base.pendingPlayers.Clear();
			bool result = base.LostHostOnClient(conn);
			networkManager.directClient = (networkManager.punchthroughClient = (networkManager.relayClient = null));
			return result;
		}

		public new virtual void SendPeerInfo()
		{
			if (!base.hostMigration)
			{
				return;
			}
			ExtraPeerInfoListMessage extraPeerInfoListMessage = new ExtraPeerInfoListMessage();
			List<PeerInfoMessage> list = new List<PeerInfoMessage>();
			PeerInfoPlayer item = default(PeerInfoPlayer);
			PeerInfoPlayer item2 = default(PeerInfoPlayer);
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection == null)
				{
					continue;
				}
				ExtraPeerInfoMessage extraPeerInfoMessage = new ExtraPeerInfoMessage();
				extraPeerInfoMessage.connectionId = networkConnection.connectionId;
				extraPeerInfoMessage.port = NetworkServer.listenPort;
				if (i == 0)
				{
					extraPeerInfoMessage.isHost = true;
					extraPeerInfoMessage.address = "<host>";
				}
				else
				{
					ConnectionInfoMessage value;
					if (!networkManager.connectionInfoByConnection.TryGetValue(networkConnection, out value))
					{
						continue;
					}
					extraPeerInfoMessage.guid = value.raknetGUID;
					extraPeerInfoMessage.address = value.externalIP;
					extraPeerInfoMessage.internalIP = value.internalIP;
					extraPeerInfoMessage.externalIPv6 = value.externalIPv6;
					extraPeerInfoMessage.internalIPv6 = value.internalIPv6;
					extraPeerInfoMessage.isHost = false;
				}
				List<PeerInfoPlayer> list2 = new List<PeerInfoPlayer>();
				for (int j = 0; j < networkConnection.playerControllers.Count; j++)
				{
					PlayerController playerController = networkConnection.playerControllers[j];
					if (playerController != null && playerController.unetView != null)
					{
						item.netId = playerController.unetView.netId;
						item.playerControllerId = playerController.unetView.playerControllerId;
						list2.Add(item);
					}
				}
				if (networkConnection.clientOwnedObjects != null)
				{
					foreach (NetworkInstanceId clientOwnedObject in networkConnection.clientOwnedObjects)
					{
						GameObject gameObject = NetworkServer.FindLocalObject(clientOwnedObject);
						if (!(gameObject == null) && gameObject.GetComponent<NetworkIdentity>().playerControllerId == -1)
						{
							item2.netId = clientOwnedObject;
							item2.playerControllerId = -1;
							list2.Add(item2);
						}
					}
				}
				if (list2.Count > 0)
				{
					extraPeerInfoMessage.playerIds = list2.ToArray();
				}
				list.Add(extraPeerInfoMessage);
			}
			extraPeerInfoListMessage.peers = list.ToArray();
			for (int k = 0; k < NetworkServer.connections.Count; k++)
			{
				NetworkConnection networkConnection2 = NetworkServer.connections[k];
				if (networkConnection2 != null)
				{
					extraPeerInfoListMessage.oldServerConnectionId = networkConnection2.connectionId;
					networkConnection2.Send(MsgType.ExtraPeerInfo, extraPeerInfoListMessage);
				}
			}
		}

		public virtual void ReconnectToNewHost()
		{
			Reset(base.oldServerConnectionId);
			networkManager.networkAddress = newHost.address;
			networkManager.networkPort = newHost.port;
			networkManager.hasEverConnected = false;
			handleClientDisconnectMethod.Invoke(null, new object[1] { ClientScene.readyConnection });
			ClientScene.localPlayers.Clear();
			if (networkManager.directCon != null)
			{
				networkManager.directClient.connection.Disconnect();
				networkManager.directClient = null;
			}
			if (networkManager.relayCon != null)
			{
				networkManager.relayClient.connection.Disconnect();
				networkManager.relayClient = null;
			}
			if (networkManager.punchthroughCon != null)
			{
				networkManager.punchthroughClient.connection.Disconnect();
				networkManager.punchthroughClient = null;
			}
			networkManager.StartClientAll(newHost.address, newHost.internalIP, newHost.guid, NetworkID.Invalid, newHost.externalIPv6, newHost.internalIPv6);
		}

		private void OnExtraPeerInfo(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("Migration Manager: OnPeerInfoAdvanced");
			}
			ExtraPeerInfoListMessage extraPeerInfoListMessage = new ExtraPeerInfoListMessage();
			netMsg.ReadMessage(extraPeerInfoListMessage);
			peersField.SetValue(this, extraPeerInfoListMessage.peers);
			oldServerConnectionIDField.SetValue(this, extraPeerInfoListMessage.oldServerConnectionId);
			for (int i = 0; i < base.peers.Length; i++)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("Migration Manager: peer conn " + base.peers[i].connectionId + " your conn " + extraPeerInfoListMessage.oldServerConnectionId);
				}
				if (base.peers[i].connectionId == extraPeerInfoListMessage.oldServerConnectionId)
				{
					base.peers[i].isYou = true;
					break;
				}
			}
			OnPeersUpdated(extraPeerInfoListMessage);
		}

		private void OnPeerClientAuthorityWrapper(NetworkMessage netMsg)
		{
			onPeerClientAuthorityMethod.Invoke(this, new object[1] { netMsg });
		}

		protected override void OnServerReconnectPlayer(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId)
		{
			ReconnectPlayerForConnection(newConnection, oldPlayer, oldConnectionId, playerControllerId);
		}

		protected override void OnServerReconnectPlayer(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId, NetworkReader extraMessageReader)
		{
			ReconnectPlayerForConnection(newConnection, oldPlayer, oldConnectionId, playerControllerId);
		}

		public new bool ReconnectPlayerForConnection(NetworkConnection newConnection, GameObject oldPlayer, int oldConnectionId, short playerControllerId)
		{
			if (!NetworkServer.active)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Migration Manager: ReconnectPlayerForConnection must have active server");
				}
				return false;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log(string.Concat("Migration Manager: ReconnectPlayerForConnection: oldConnId=", oldConnectionId, " player=", oldPlayer, " conn:", newConnection));
			}
			if (!base.pendingPlayers.ContainsKey(oldConnectionId))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Migration Manager: ReconnectPlayerForConnection oldConnId=" + oldConnectionId + " not found.");
				}
				return false;
			}
			oldPlayer.SetActive(true);
			NetworkServer.Spawn(oldPlayer);
			if (!NetworkServer.AddPlayerForConnection(newConnection, oldPlayer, playerControllerId))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Migration Manager: ReconnectPlayerForConnection oldConnId=" + oldConnectionId + " AddPlayerForConnection failed.");
				}
				return false;
			}
			if (NetworkServer.localClientActive)
			{
				SendPeerInfo();
			}
			return true;
		}

		private void OnGUI()
		{
			if (base.hostWasShutdown)
			{
				OnGUIHost();
			}
			else if (base.disconnectedFromHost && base.oldServerConnectionId != -1)
			{
				OnGUIClient();
			}
		}

		private void OnGUIHost()
		{
			int num = 310;
			GUI.Label(new Rect(10f, num, 200f, 40f), "Host Was Shutdown ID(" + base.oldServerConnectionId + ")");
			num += 25;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				GUI.Label(new Rect(10f, num, 200f, 40f), "Host Migration not supported for WebGL");
				return;
			}
			if (base.waitingReconnectToNewHost)
			{
				if (GUI.Button(new Rect(10f, num, 200f, 20f), "Reconnect as Client"))
				{
					Reset(0);
					networkManager.networkAddress = newHost.address;
					networkManager.StartClientAll(newHost.address, newHost.internalIP, newHost.guid, NetworkID.Invalid, newHost.externalIPv6, newHost.internalIPv6, networkManager.OnMatchJoined);
				}
				num += 25;
			}
			else
			{
				bool youAreNewHost;
				if (GUI.Button(new Rect(10f, num, 200f, 20f), "Pick New Host") && FindNewHost(out newHost, out youAreNewHost))
				{
					base.newHostAddress = newHost.address;
					if (youAreNewHost)
					{
						Debug.LogWarning("MigrationManager FindNewHost - new host is self?");
					}
					else
					{
						base.waitingReconnectToNewHost = true;
					}
				}
				num += 25;
			}
			if (GUI.Button(new Rect(10f, num, 200f, 20f), "Leave Game"))
			{
				networkManager.SetupMigrationManager(null);
				networkManager.StopHost();
				Reset(-1);
			}
			num += 25;
		}

		private void OnGUIClient()
		{
			int num = 300;
			GUI.Label(new Rect(10f, num, 200f, 40f), "Lost Connection To Host ID(" + base.oldServerConnectionId + ")");
			num += 25;
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				GUI.Label(new Rect(10f, num, 200f, 40f), "Host Migration not supported for WebGL");
				return;
			}
			if (base.waitingToBecomeNewHost)
			{
				GUI.Label(new Rect(10f, num, 200f, 40f), "You are the new host");
				num += 25;
				if (GUI.Button(new Rect(10f, num, 200f, 20f), "Start As Host"))
				{
					NetworkServer.Configure(networkManager.topo);
					BecomeNewHost(networkManager.networkPort);
				}
				num += 25;
			}
			else if (base.waitingReconnectToNewHost)
			{
				GUI.Label(new Rect(10f, num, 200f, 40f), "New host is " + newHost.address);
				num += 25;
				if (GUI.Button(new Rect(10f, num, 200f, 20f), "Reconnect To New Host"))
				{
					ReconnectToNewHost();
				}
				num += 25;
			}
			else
			{
				bool youAreNewHost;
				if (GUI.Button(new Rect(10f, num, 200f, 20f), "Pick New Host") && FindNewHost(out newHost, out youAreNewHost))
				{
					base.newHostAddress = newHost.address;
					if (youAreNewHost)
					{
						base.waitingToBecomeNewHost = true;
					}
					else
					{
						base.waitingReconnectToNewHost = true;
					}
				}
				num += 25;
			}
			if (GUI.Button(new Rect(10f, num, 200f, 20f), "Leave Game"))
			{
				networkManager.SetupMigrationManager(null);
				networkManager.StopHost();
				Reset(-1);
			}
			num += 25;
		}

		private void EmptyMethod(NetworkMessage msg)
		{
		}
	}
}
