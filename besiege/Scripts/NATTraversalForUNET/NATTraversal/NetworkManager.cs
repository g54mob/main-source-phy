using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

namespace NATTraversal
{
	[RequireComponent(typeof(NATHelper))]
	public class NetworkManager : UnityEngine.Networking.NetworkManager
	{
		private delegate void HandleCommandFunc(NetworkIdentity netId, int cmdHash, NetworkReader reader);

		private delegate bool OnCheckObserverFunc(NetworkIdentity netId, NetworkConnection conn);

		private delegate void AddObserverFunc(NetworkIdentity netId, NetworkConnection conn);

		private const string TAG = "NATTraversal: ";

		public bool connectDirectly = true;

		public bool connectPunchthrough = true;

		public bool connectRelay = true;

		public bool autoConnectToFacilitator = true;

		public bool useUnityMatchmaking = true;

		public string externalIPSource = "http://ipv4.icanhazip.com";

		public string externalIPv6Source = "http://ipv6.icanhazip.com";

		public float externalIPTimeout = 10f;

		public int clientPort;

		public bool delayDirectConnection;

		[NonSerialized]
		public string hostExternalIP;

		[NonSerialized]
		public string hostExternalIPv6;

		[NonSerialized]
		public string hostInternalIP;

		[NonSerialized]
		public string hostInternalIPv6;

		[NonSerialized]
		public bool hasEverConnected;

		[NonSerialized]
		public NetworkID matchID;

		[NonSerialized]
		public NodeID matchmakingNodeID;

		public List<ExternalServer> natServers = new List<ExternalServer>();

		public NetworkClient relayClient;

		public NetworkClient punchthroughClient;

		public NetworkClient directClient;

		[NonSerialized]
		public string externalIP;

		[NonSerialized]
		public string externalIPv6;

		[NonSerialized]
		public NATHelper natHelper;

		protected bool isDoneFetchingExternalIPv6;

		protected bool isDoneFetchingExternalIPv4;

		[NonSerialized]
		public List<NetworkConnection> deferredConnections = new List<NetworkConnection>();

		[NonSerialized]
		public HostTopology topo;

		[NonSerialized]
		public Dictionary<NetworkConnection, ConnectionInfoMessage> connectionInfoByConnection = new Dictionary<NetworkConnection, ConnectionInfoMessage>();

		private bool wasStartClientCalled;

		protected Dictionary<short, NetworkMessageDelegate> clientMsgHandlers = new Dictionary<short, NetworkMessageDelegate>();

		[NonSerialized]
		private bool advertiseMatch;

		[NonSerialized]
		public Guid clientGUID;

		public HashSet<string> connectedClientGUIDs = new HashSet<string>();

		private Coroutine delayDirectConnectProcess;

		private Dictionary<string, bool> isClientAlreadyReadyAlready = new Dictionary<string, bool>();

		private NetworkClient clientToReplaceRelayClientWith;

		private FieldInfo clientIDField;

		private FieldInfo playerControllersField;

		private FieldInfo connectionField;

		private FieldInfo serverPortField;

		private FieldInfo serverIPField;

		private FieldInfo connectStateField;

		private FieldInfo clientOwnedObjectsField;

		private FieldInfo clientAuthorityOwnerField;

		private FieldInfo connectionToClientField;

		private FieldInfo clientSceneReadyConnectionField;

		private FieldInfo serverInstanceField;

		private FieldInfo simpleServerField;

		private FieldInfo serverIDField;

		private FieldInfo messageHandlersField;

		private MethodInfo registerClientMessagesMethod;

		private MethodInfo clientChangeSceneMethod;

		private MethodInfo handleClientDisconnectMethod;

		private MethodInfo simpleServerRegisterHandlerSafeMethod;

		private MethodInfo networkMessagesRegisterHandlerSafeMethod;

		private static HandleCommandFunc HandleCommand;

		private OnCheckObserverFunc OnCheckObserver;

		private AddObserverFunc AddObserver;

		private BindingFlags instanceFlags = BindingFlags.Instance | BindingFlags.NonPublic;

		private BindingFlags staticFlags = BindingFlags.Static | BindingFlags.NonPublic;

		private bool hasAddedIPToMatchName;

		private bool hasAddedGUIDToMatchName;

		private NetworkManagerHUD hud;

		public NetworkConnection relayCon
		{
			get
			{
				if (relayClient != null)
				{
					return relayClient.connection;
				}
				return null;
			}
		}

		public NetworkConnection punchthroughCon
		{
			get
			{
				if (punchthroughClient != null)
				{
					return punchthroughClient.connection;
				}
				return null;
			}
		}

		public NetworkConnection directCon
		{
			get
			{
				if (directClient != null)
				{
					return directClient.connection;
				}
				return null;
			}
		}

		protected bool isDoneFetchingExternalIP
		{
			get
			{
				if (isDoneFetchingExternalIPv4)
				{
					return isDoneFetchingExternalIPv6;
				}
				return false;
			}
		}

		public new MigrationManager migrationManager
		{
			get
			{
				return (MigrationManager)base.migrationManager;
			}
		}

		public int maxClients
		{
			get
			{
				return NetworkServer.hostTopology.MaxDefaultConnections / 2 + 1;
			}
		}

		public int numClients
		{
			get
			{
				return connectedClientGUIDs.Count + 1;
			}
		}

		public virtual void Awake()
		{
			clientIDField = typeof(NetworkClient).GetField("m_ClientId", instanceFlags);
			serverIDField = typeof(NetworkServer).GetField("m_ServerId", instanceFlags);
			playerControllersField = typeof(NetworkConnection).GetField("m_PlayerControllers", instanceFlags);
			clientOwnedObjectsField = typeof(NetworkConnection).GetField("m_ClientOwnedObjects", instanceFlags);
			clientAuthorityOwnerField = typeof(NetworkIdentity).GetField("m_ClientAuthorityOwner", instanceFlags);
			connectionToClientField = typeof(NetworkIdentity).GetField("m_ConnectionToClient", instanceFlags);
			registerClientMessagesMethod = typeof(UnityEngine.Networking.NetworkManager).GetMethod("RegisterClientMessages", instanceFlags);
			clientChangeSceneMethod = typeof(UnityEngine.Networking.NetworkManager).GetMethod("ClientChangeScene", instanceFlags);
			handleClientDisconnectMethod = typeof(ClientScene).GetMethod("HandleClientDisconnect", staticFlags);
			connectionField = typeof(NetworkClient).GetField("m_Connection", instanceFlags);
			serverPortField = typeof(NetworkClient).GetField("m_ServerPort", instanceFlags);
			serverIPField = typeof(NetworkClient).GetField("m_ServerIp", instanceFlags);
			connectStateField = typeof(NetworkClient).GetField("m_AsyncConnect", instanceFlags);
			clientSceneReadyConnectionField = typeof(ClientScene).GetField("s_ReadyConnection", staticFlags);
			serverInstanceField = typeof(NetworkServer).GetField("s_Instance", staticFlags);
			simpleServerField = typeof(NetworkServer).GetField("m_SimpleServerSimple", instanceFlags);
			simpleServerRegisterHandlerSafeMethod = typeof(NetworkServerSimple).GetMethod("RegisterHandlerSafe", instanceFlags);
			messageHandlersField = typeof(NetworkServer).GetField("m_MessageHandlers", instanceFlags);
			networkMessagesRegisterHandlerSafeMethod = typeof(NetworkServer).Assembly.GetType("UnityEngine.Networking.NetworkMessageHandlers").GetMethod("RegisterHandlerSafe", instanceFlags);
			HandleCommand = Util.CreateDelegate<NetworkIdentity, HandleCommandFunc>("HandleCommand", instanceFlags);
			OnCheckObserver = Util.CreateDelegate<NetworkIdentity, OnCheckObserverFunc>("OnCheckObserver", instanceFlags);
			AddObserver = Util.CreateDelegate<NetworkIdentity, AddObserverFunc>("AddObserver", instanceFlags);
			typeof(UnityEngine.Networking.NetworkManager).GetMethod("Awake", instanceFlags).Invoke(this, null);
		}

		public virtual void Start()
		{
			clientGUID = Guid.NewGuid();
			natHelper = GetComponent<NATHelper>();
			if (natHelper == null)
			{
				Debug.LogError("NATTraversal: Required NATHelper component is missing! Add the NATHelper to the same game object as your NetworkManager.");
			}
			else
			{
				natHelper.OnDoneConnectingToFacilitator += OnDoneConnectingToFacilitator;
				natHelper.findNatDevice();
				if (autoConnectToFacilitator)
				{
					StartCoroutine(natHelper.connectToNATFacilitator());
				}
			}
			StartCoroutine(getExternalIP());
			StartCoroutine(getExternalIPv6());
			matchMaker = base.gameObject.AddComponent<NetworkMatch>();
			hud = GetComponent<NetworkManagerHUD>();
		}

		public virtual void Update()
		{
			natServers.ForEach(delegate(ExternalServer server)
			{
				server.Update();
			});
			if (!hud)
			{
				return;
			}
			if (!hasAddedIPToMatchName && isDoneFetchingExternalIP)
			{
				hasAddedIPToMatchName = true;
				matchName = matchName + "|" + Network.player.ipAddress + "|" + externalIP + "|" + getLocalIPv6() + "|" + externalIPv6;
			}
			if (hasAddedIPToMatchName && !hasAddedGUIDToMatchName && natHelper.guid != 0L && connectPunchthrough)
			{
				hasAddedGUIDToMatchName = true;
				if (connectPunchthrough)
				{
					matchName = matchName + "|" + natHelper.guid;
				}
			}
		}

		public virtual NetworkClient StartHostAll(string matchName, uint maxPlayers, bool advertise = true, string password = "", int eloScore = 0, int requestDomain = 0, NetworkMatch.DataResponseDelegate<MatchInfo> callback = null)
		{
			hostInternalIP = Network.player.ipAddress;
			hostInternalIPv6 = getLocalIPv6();
			base.maxConnections = (int)(maxPlayers - 1);
			matchSize = maxPlayers;
			initConfig();
			if (natHelper.portForwardingEnabled)
			{
				natHelper.mapPort(base.networkPort);
			}
			client = base.StartHost(base.connectionConfig, topo.MaxDefaultConnections);
			NetworkServer obj = (NetworkServer)serverInstanceField.GetValue(null);
			NetworkServerSimple networkServerSimple = (NetworkServerSimple)simpleServerField.GetValue(obj);
			networkServerSimple.UnregisterHandler(5);
			simpleServerRegisterHandlerSafeMethod.Invoke(networkServerSimple, new object[2]
			{
				(short)5,
				new NetworkMessageDelegate(OnCommandMessage)
			});
			if ((bool)migrationManager)
			{
				migrationManager.Initialize(client, null);
			}
			StartCoroutine(StartHostAsync(matchName, advertise, password, eloScore, requestDomain, callback));
			return client;
		}

		private IEnumerator StartHostAsync(string matchName, bool advertiseMatch = true, string matchPassword = "", int eloScore = 0, int requestDomain = 0, NetworkMatch.DataResponseDelegate<MatchInfo> callback = null)
		{
			if (callback == null)
			{
				callback = OnMatchCreate;
			}
			if (connectPunchthrough)
			{
				yield return StartCoroutine(natHelper.startListeningForPunchthrough(OnHolePunchedServer));
			}
			while (!isDoneFetchingExternalIP)
			{
				yield return new WaitForEndOfFrame();
			}
			if (externalIP == "" && LogFilter.logWarn)
			{
				Debug.LogWarning("NATTraversal: Unable to determine public IP. Direct connect will fail.");
			}
			this.advertiseMatch = advertiseMatch;
			hostExternalIP = externalIP;
			hostExternalIPv6 = externalIPv6;
			string text = matchName + "|" + hostInternalIP + "|" + hostExternalIP + "|" + hostInternalIPv6 + "|" + hostExternalIPv6;
			if (connectPunchthrough)
			{
				text = text + "|" + natHelper.guid;
			}
			if (connectRelay || useUnityMatchmaking)
			{
				uint num = 2u;
				if (connectRelay)
				{
					num = (uint)Mathf.Min(matchSize, (uint)(topo.MaxDefaultConnections + 1));
				}
				if (matchMaker == null)
				{
					matchMaker = gameObject.AddComponent<NetworkMatch>();
				}
				matchMaker.CreateMatch(text, num, advertiseMatch && useUnityMatchmaking, matchPassword, "", "", eloScore, requestDomain, callback);
			}
		}

		public virtual void StartClientAll(MatchInfoSnapshot match, NetworkMatch.DataResponseDelegate<MatchInfo> callback = null, string matchPassword = "", int eloScore = 0, int requestDomain = 0, bool matchAlreadyJoined = false)
		{
			string text;
			string internalIP;
			string text2;
			string internalIPv;
			ulong guid;
			ParseConnectionInfoFromMatchName(match.name, out text, out internalIP, out text2, out internalIPv, out guid);
			StartClientAll(text, internalIP, guid, match.networkId, text2, internalIPv, callback, matchPassword, eloScore, requestDomain, matchAlreadyJoined);
		}

		public virtual void StartClientAll(string hostExternalIP, string hostInternalIP, ulong hostGUID, NetworkID matchID = NetworkID.Invalid, string hostExternalIPv6 = "", string hostInternalIPv6 = "", NetworkMatch.DataResponseDelegate<MatchInfo> joinMatchCallback = null, string matchPassword = "", int eloScore = 0, int requestDomain = 0, bool matchAlreadyJoined = false)
		{
			isNetworkActive = true;
			wasStartClientCalled = true;
			initConfig();
			this.hostExternalIP = hostExternalIP;
			this.hostInternalIP = hostInternalIP;
			this.hostExternalIPv6 = hostExternalIPv6;
			this.hostInternalIPv6 = hostInternalIPv6;
			if (connectPunchthrough)
			{
				if (!NetworkTransport.IsStarted)
				{
					NetworkTransport.Init();
				}
				punchthroughClient = createClient();
				if (hostGUID != 0L)
				{
					StartCoroutine(natHelper.punchThroughToServer(hostGUID, OnHolePunchedClient));
				}
				else if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATTraversal: RakNet guid missing. Punch-through not possible.");
				}
			}
			if (connectDirectly)
			{
				if (delayDirectConnection)
				{
					if (delayDirectConnectProcess != null)
					{
						StopCoroutine(delayDirectConnectProcess);
					}
					delayDirectConnectProcess = StartCoroutine(directConnectInAWhile(hostExternalIP, hostInternalIP, hostExternalIPv6, hostInternalIPv6));
				}
				else
				{
					directConnect(hostExternalIP, hostInternalIP, hostExternalIPv6, hostInternalIPv6);
				}
			}
			if (!connectRelay || matchID == NetworkID.Invalid)
			{
				return;
			}
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: Joining match.");
			}
			relayClient = createClient();
			if (!matchAlreadyJoined)
			{
				if (joinMatchCallback == null)
				{
					joinMatchCallback = OnMatchJoined;
				}
				matchMaker.JoinMatch(matchID, matchPassword, "", "", eloScore, requestDomain, joinMatchCallback);
			}
		}

		public virtual void directConnect(string externalIP, string internalIP, string hostExternalIPv6, string hostInternalIPv6)
		{
			base.networkAddress = pickCorrectAddressToConnectTo(externalIP, internalIP, hostExternalIPv6, hostInternalIPv6);
			if (LogFilter.logInfo)
			{
				Debug.Log("NATTraversal: Attempting to connect directly: " + base.networkAddress);
			}
			directClient = createClient();
			int num = -1;
			if (clientPort != 0)
			{
				if (!NetworkTransport.IsStarted)
				{
					NetworkTransport.Init();
				}
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: Connecting from custom client port: " + clientPort);
				}
				num = NetworkTransport.AddHost(topo, clientPort);
			}
			directClient.Connect(base.networkAddress, base.networkPort);
			if (clientPort != 0)
			{
				clientIDField.SetValue(directClient, num);
			}
			if (migrationManager != null)
			{
				migrationManager.Initialize(directClient, null);
			}
			OnStartClient(directClient);
		}

		public new virtual void StopClient()
		{
			natHelper.StopPunchingThrough();
			client = null;
			if (delayDirectConnectProcess != null)
			{
				StopCoroutine(delayDirectConnectProcess);
				delayDirectConnectProcess = null;
			}
			base.StopClient();
			isNetworkActive = false;
			if (directClient != null)
			{
				if (directClient.isConnected)
				{
					directClient.Disconnect();
				}
				directClient.Shutdown();
			}
			if (punchthroughClient != null)
			{
				if (punchthroughClient.isConnected)
				{
					punchthroughClient.Disconnect();
				}
				punchthroughClient.Shutdown();
			}
			if (relayClient != null)
			{
				if (relayClient.isConnected)
				{
					relayClient.Disconnect();
				}
				relayClient.Shutdown();
			}
			directClient = (punchthroughClient = (relayClient = null));
			clientMsgHandlers.Clear();
			deferredConnections.Clear();
			clientToReplaceRelayClientWith = null;
			hasEverConnected = false;
			wasStartClientCalled = false;
			NetworkTransport.Shutdown();
			NetworkTransport.Init();
		}

		public new virtual void StopHost()
		{
			OnStopHost();
			if (matchMaker != null && matchInfo != null && matchInfo.networkId != NetworkID.Invalid)
			{
				matchMaker.SetMatchAttributes(matchInfo.networkId, false, matchInfo.domain, finishShuttingDownHostWhenMatchIsUnlisted);
			}
			else
			{
				finishShuttingDownHostWhenMatchIsUnlisted(false, "");
			}
		}

		public virtual void finishShuttingDownHostWhenMatchIsUnlisted(bool success, string extendedInfo)
		{
			bool active = NetworkServer.active;
			StopServer();
			StopClient();
			if (migrationManager != null && active)
			{
				migrationManager.LostHostOnHost();
			}
		}

		public new virtual void StopServer()
		{
			natHelper.StopListeningForPunchthrough();
			OnStopServer();
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: NetworkManager StopServer");
			}
			isNetworkActive = false;
			NetworkServer.Shutdown();
			StopMatchMaker();
			foreach (ExternalServer natServer in natServers)
			{
				for (int num = natServer.connections.Count - 1; num >= 0; num--)
				{
					if (natServer.connections[num] != null)
					{
						if (natServer.connections[num].GetType() == typeof(ExternalNetworkConnection))
						{
							((ExternalNetworkConnection)natServer.connections[num]).Disconnect();
						}
						else
						{
							natServer.connections[num].Disconnect();
						}
						natServer.connections[num].Dispose();
					}
				}
				natServer.Stop();
			}
			natServers.Clear();
			connectionInfoByConnection.Clear();
			isClientAlreadyReadyAlready.Clear();
			NetworkTransport.Shutdown();
			NetworkTransport.Init();
			if (base.offlineScene != "")
			{
				ServerChangeScene(base.offlineScene);
			}
		}

		public virtual void ReconnectDirectClientToNewHost(string externalIP, int port, string internalIP = "", string internalIPv6 = "", string externalIPv6 = "")
		{
			if (LogFilter.logInfo)
			{
				Debug.Log("NATTraversal: NetworkClient Reconnect " + externalIP + ":" + port);
			}
			if (client.isConnected)
			{
				handleClientDisconnectMethod.Invoke(null, new object[1] { client.connection });
				ClientScene.localPlayers.Clear();
			}
			client.connection.Disconnect();
			connectionField.SetValue(client, null);
			int num = NetworkTransport.AddHost(topo, clientPort);
			clientIDField.SetValue(client, num);
			serverPortField.SetValue(client, port);
			string value = pickCorrectAddressToConnectTo(externalIP, internalIP, externalIPv6, internalIPv6);
			serverIPField.SetValue(client, value);
			connectStateField.SetValue(client, 2);
		}

		public override void OnClientSceneChanged(NetworkConnection conn)
		{
			if (!NetworkServer.active)
			{
				NetworkClient networkClient = directClient;
				if (conn == punchthroughCon)
				{
					networkClient = punchthroughClient;
				}
				else if (conn == relayCon)
				{
					networkClient = relayClient;
				}
				networkClient.RegisterHandler(32, OnMultiClientConnectMsg);
				networkClient.RegisterHandler(33, OnMultiClientDisconnectMsg);
			}
			base.clientLoadedScene = true;
			if (base.onlineScene == SceneManager.GetActiveScene().name)
			{
				foreach (NetworkConnection deferredConnection in deferredConnections)
				{
					OnMultiClientConnect(deferredConnection);
				}
				deferredConnections.Clear();
			}
			base.OnClientSceneChanged(conn);
		}

		[Server]
		private static void OnCommandMessage(NetworkMessage netMsg)
		{
			int cmdHash = (int)netMsg.reader.ReadPackedUInt32();
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			GameObject gameObject = ClientScene.FindLocalObject(networkInstanceId);
			if (gameObject == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat("NATTraversal: Instance not found when handling Command message [netId=", networkInstanceId, "]"));
				}
				return;
			}
			NetworkIdentity component = gameObject.GetComponent<NetworkIdentity>();
			if (component == null)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat("NATTraversal: NetworkIdentity deleted when handling Command message [netId=", networkInstanceId, "]"));
				}
				return;
			}
			bool flag = false;
			for (int i = 0; i < netMsg.conn.playerControllers.Count; i++)
			{
				PlayerController playerController = netMsg.conn.playerControllers[i];
				if (playerController.gameObject != null && playerController.gameObject.GetComponent<NetworkIdentity>().netId == component.netId)
				{
					flag = true;
					break;
				}
			}
			if (!flag && component.clientAuthorityOwner != netMsg.conn)
			{
				if (component.clientAuthorityOwner == null)
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning(string.Concat("NATTraversal: Command for object without authority [netId=", networkInstanceId, "]"));
					}
					return;
				}
				NetworkManager obj = (NetworkManager)UnityEngine.Networking.NetworkManager.singleton;
				ConnectionInfoMessage connectionInfoMessage = obj.connectionInfoByConnection[netMsg.conn];
				ConnectionInfoMessage connectionInfoMessage2 = obj.connectionInfoByConnection[component.clientAuthorityOwner];
				if (connectionInfoMessage.clientGUID != connectionInfoMessage2.clientGUID)
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning(string.Concat("NATTraversal: Command for object without authority [netId=", networkInstanceId, "]"));
					}
					return;
				}
			}
			if (LogFilter.currentLogLevel == 0)
			{
				Debug.Log(string.Concat("NATTraversal: OnCommandMessage for netId=", networkInstanceId, " conn=", netMsg.conn));
			}
			HandleCommand(component, cmdHash, netMsg.reader);
		}

		public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
		{
			if (success)
			{
				Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);
			}
			base.matchInfo = matchInfo;
			initConfig();
			if (!NetworkServer.active)
			{
				client = base.StartHost(base.connectionConfig, topo.MaxDefaultConnections);
				if ((bool)migrationManager)
				{
					migrationManager.Initialize(client, null);
				}
				if (connectPunchthrough)
				{
					StartCoroutine(natHelper.startListeningForPunchthrough(OnHolePunchedServer));
				}
			}
			if (success)
			{
				matchID = matchInfo.networkId;
				matchmakingNodeID = matchInfo.nodeId;
				byte error;
				NetworkTransport.ConnectAsNetworkHost(NetworkServer.serverHostId, matchInfo.address, matchInfo.port, matchInfo.networkId, Utility.GetSourceID(), matchInfo.nodeId, out error);
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogError("NATTraversal: Failed to create match. We'll still try and host but it doesn't look good.");
			}
		}

		public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo info)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: UNet match joined.");
			}
			if (!success)
			{
				relayClient = null;
				if (LogFilter.logWarn)
				{
					Debug.LogError("NATTraversal: Failed to join UNET Match. We'll still try and connect but it doesn't look good: " + extendedInfo);
				}
			}
			if (success)
			{
				matchID = info.networkId;
				matchmakingNodeID = info.nodeId;
			}
			if (!wasStartClientCalled)
			{
				string text;
				string internalIP;
				string text2;
				string internalIPv;
				ulong guid;
				ParseConnectionInfoFromMatchName(matchName, out text, out internalIP, out text2, out internalIPv, out guid);
				StartClientAll(text, internalIP, guid, info.networkId, text2, internalIPv, null, "", 0, 0, true);
			}
			if (!success)
			{
				return;
			}
			bool num = directClient != null && directClient.isConnected;
			bool flag = punchthroughClient != null && punchthroughClient.isConnected;
			if (num || flag)
			{
				relayClient = null;
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: UNet match joined but we are already connected. Just ignore.");
				}
				return;
			}
			try
			{
				Utility.SetAccessTokenForNetwork(info.networkId, info.accessToken);
			}
			catch (Exception)
			{
			}
			relayClient.Connect(info);
			OnStartClient(relayClient);
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: Connecting via relay");
			}
		}

		public virtual void OnMatchDestroyed(bool success, string extendedInfo)
		{
		}

		public virtual void OnMatchDropped(bool success, string extendedInfo)
		{
			Debug.LogWarning("Match dropped");
		}

		[Server]
		private void OnServerReadyMsg(NetworkMessage msg)
		{
			NetworkServer.SetClientReady(msg.conn);
			StartCoroutine(CallOnServerReadyAfterConnectionInfoArrives(msg.conn));
		}

		[Server]
		private IEnumerator CallOnServerReadyAfterConnectionInfoArrives(NetworkConnection con)
		{
			while (!connectionInfoByConnection.ContainsKey(con))
			{
				yield return new WaitForSeconds(0.02f);
			}
			ConnectionInfoMessage connectionInfoMessage = connectionInfoByConnection[con];
			bool value = false;
			isClientAlreadyReadyAlready.TryGetValue(connectionInfoMessage.clientGUID, out value);
			if (!value)
			{
				isClientAlreadyReadyAlready[connectionInfoMessage.clientGUID] = true;
				OnServerReady(con);
			}
		}

		[Server]
		public virtual void OnHolePunchedServer(int natListenPort, ulong clientGUID)
		{
			ExternalServer externalServer = new ExternalServer(clientGUID);
			if (externalServer.Listen(natListenPort, topo))
			{
				natServers.Add(externalServer);
			}
			if ((bool)migrationManager)
			{
				migrationManager.SendPeerInfo();
			}
		}

		public virtual void OnHolePunchedClient(int natListenPort, int natConnectPort, bool success)
		{
			if (!success)
			{
				Debug.LogWarning("Punchthrough failed");
				OnMultiClientDisconnect(punchthroughClient.connection);
				return;
			}
			if (directClient != null && directClient.isConnected)
			{
				if (LogFilter.logInfo)
				{
					Debug.Log("NATTraversal: NAT hole punched but client already directly connected.");
				}
				punchthroughClient = null;
				return;
			}
			base.networkAddress = pickCorrectAddressToConnectTo(hostExternalIP, hostInternalIP, hostExternalIPv6, hostInternalIPv6);
			if (LogFilter.logInfo)
			{
				Debug.Log("NATTraversal: Attempting to connect through hole " + base.networkAddress + ":" + natConnectPort);
			}
			int num = NetworkTransport.AddHost(topo, natListenPort);
			punchthroughClient.Connect(base.networkAddress, natConnectPort);
			NetworkTransport.RemoveHost((int)clientIDField.GetValue(punchthroughClient));
			clientIDField.SetValue(punchthroughClient, num);
			if (migrationManager != null)
			{
				migrationManager.Initialize(punchthroughClient, null);
			}
			OnStartClient(punchthroughClient);
		}

		[Server]
		public override void OnServerConnect(NetworkConnection conn)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: Server got a client connection");
			}
			conn.RegisterHandler(MsgType.SetConnectionInfo, OnSetConnectionInfo);
			conn.RegisterHandler(MsgType.ReplaceConnection, OnReplaceConnectionMsg);
		}

		[Server]
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			if (checkForAnotherConnectionFromTheSameClient(conn, ConnectionType.DIRECT | ConnectionType.PUNCHTHROUGH) == null)
			{
				if (migrationManager != null)
				{
					migrationManager.SendPeerInfo();
				}
				ConnectionInfoMessage value = null;
				connectionInfoByConnection.TryGetValue(conn, out value);
				if (value != null)
				{
					isClientAlreadyReadyAlready[value.clientGUID] = false;
					connectedClientGUIDs.Remove(value.clientGUID);
				}
				if (numClients < maxClients && (bool)matchMaker && advertiseMatch && matchInfo != null)
				{
					matchMaker.SetMatchAttributes(matchInfo.networkId, true, matchInfo.domain, OnMatchAttributesUpdated);
				}
				base.OnServerDisconnect(conn);
			}
			connectionInfoByConnection.Remove(conn);
		}

		public virtual void OnMatchAttributesUpdated(bool success, string extendedInfo)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: Match attributes updated: " + success);
			}
		}

		[Server]
		private void OnReplaceConnectionMsg(NetworkMessage msg)
		{
			NetworkConnection networkConnection = checkForAnotherConnectionFromTheSameClient(msg.conn, ConnectionType.DIRECT | ConnectionType.PUNCHTHROUGH);
			if (networkConnection != null)
			{
				replaceConnection(msg.conn, networkConnection);
				msg.conn.Disconnect();
				if (LogFilter.logInfo)
				{
					Debug.Log("NATTraversal: relay connection replaced with direct connection");
				}
			}
		}

		[Server]
		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
		{
			base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
			if (migrationManager != null)
			{
				migrationManager.SendPeerInfo();
			}
		}

		[Server]
		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
		{
			base.OnServerAddPlayer(conn, playerControllerId);
			if (migrationManager != null)
			{
				migrationManager.SendPeerInfo();
			}
		}

		[Server]
		public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
		{
			base.OnServerRemovePlayer(conn, player);
			if (migrationManager != null)
			{
				migrationManager.SendPeerInfo();
			}
		}

		private void OnMultiClientConnectMsg(NetworkMessage netMsg)
		{
			OnMultiClientConnectInternal(netMsg.conn);
		}

		protected virtual void OnMultiClientConnectInternal(NetworkConnection conn)
		{
			ConnectionType connectionType = ConnectionType.DIRECT;
			if (conn == punchthroughCon)
			{
				connectionType = ConnectionType.PUNCHTHROUGH;
			}
			else if (conn == relayCon)
			{
				connectionType = ConnectionType.RELAY;
			}
			if (LogFilter.logDebug)
			{
				switch (connectionType)
				{
				case ConnectionType.DIRECT:
					Debug.Log("NATTraversal: Client connected directly");
					break;
				case ConnectionType.PUNCHTHROUGH:
					Debug.Log("NATTraversal: Client connected via NAT punch-through");
					break;
				case ConnectionType.RELAY:
					Debug.Log("NATTraversal: Client connected via relay");
					break;
				}
			}
			conn.SetMaxDelay(base.maxDelay);
			if (conn.address != "localServer" && conn.address != "localClient")
			{
				ConnectionInfoMessage msg = new ConnectionInfoMessage(clientGUID.ToString(), natHelper.guid, externalIP, Network.player.ipAddress, externalIPv6, getLocalIPv6(), connectionType);
				conn.Send(MsgType.SetConnectionInfo, msg);
			}
			string text = SceneManager.GetActiveScene().name;
			if (!string.IsNullOrEmpty(base.onlineScene) && base.onlineScene != base.offlineScene && text != base.onlineScene)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: Connection deferred until after online scene load");
				}
				switch (connectionType)
				{
				case ConnectionType.DIRECT:
					client = directClient;
					break;
				case ConnectionType.PUNCHTHROUGH:
					client = punchthroughClient;
					break;
				case ConnectionType.RELAY:
					client = relayClient;
					break;
				}
				deferredConnections.Add(conn);
			}
			else
			{
				OnMultiClientConnect(conn);
			}
		}

		public virtual void OnMultiClientConnect(NetworkConnection conn)
		{
			NetworkClient networkClient = null;
			if (conn == punchthroughCon)
			{
				networkClient = punchthroughClient;
				if (relayClient != null && !relayClient.isConnected)
				{
					relayClient.Shutdown();
					relayClient = null;
				}
			}
			else if (conn == directCon)
			{
				networkClient = directClient;
				if (punchthroughClient == null || !punchthroughClient.isConnected)
				{
					natHelper.StopPunchingThrough();
					if (punchthroughClient != null)
					{
						punchthroughClient.Shutdown();
						punchthroughClient = null;
					}
				}
				if (relayClient != null && !relayClient.isConnected)
				{
					relayClient.Shutdown();
					relayClient = null;
				}
			}
			else if (conn == relayCon)
			{
				networkClient = relayClient;
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("NATTraversal: Client connected but the connection is not recognized.");
			}
			if (!hasEverConnected)
			{
				hasEverConnected = true;
				client = networkClient;
				OnClientConnect(conn);
			}
			else if ((directClient != null && directClient != networkClient && directClient.isConnected) || (punchthroughClient != null && punchthroughClient != networkClient && punchthroughClient.isConnected))
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: A connection was established but we are already directly connected.");
				}
				conn.FlushChannels();
				conn.Disconnect();
				if (conn == punchthroughCon)
				{
					punchthroughClient = null;
				}
				else if (conn == directCon)
				{
					directClient = null;
				}
				else
				{
					relayClient = null;
				}
			}
			else
			{
				client = networkClient;
				clientToReplaceRelayClientWith = networkClient;
				replaceConnection(relayCon, networkClient.connection);
				if (relayCon.isReady)
				{
					clientSceneReadyConnectionField.SetValue(null, networkClient.connection);
					networkClient.connection.isReady = true;
				}
			}
		}

		private void OnMultiClientDisconnectMsg(NetworkMessage netMsg)
		{
			OnMultiClientDisconnect(netMsg.conn);
		}

		public virtual void OnMultiClientDisconnect(NetworkConnection conn)
		{
			bool num = relayClient != null && relayCon != conn;
			bool flag = directClient != null && directCon != conn;
			bool flag2 = punchthroughClient != null && punchthroughCon != conn;
			if (relayCon != null && conn == relayCon)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: Relay client is disconnecting.");
				}
				relayClient = null;
			}
			else if (directCon != null && conn == directCon)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: Direct connect client is disconnecting.");
				}
				directClient = null;
			}
			else if (conn == punchthroughCon)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: Punchthrough client is disconnecting.");
				}
				punchthroughClient = null;
			}
			else if (LogFilter.logWarn)
			{
				Debug.LogWarning("NATTraversal: Received disconnect message on client for unrecognized connection.");
			}
			if (!num && !flag && !flag2)
			{
				hasEverConnected = false;
				clientMsgHandlers.Clear();
				clientToReplaceRelayClientWith = null;
			}
			if (!(num || flag || flag2) && (!(migrationManager != null) || !migrationManager.LostHostOnClient(conn)))
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: All clients disconnected.");
				}
				if (base.offlineScene != "")
				{
					clientChangeSceneMethod.Invoke(this, new object[2] { base.offlineScene, false });
				}
				if (matchMaker != null && matchInfo != null && matchInfo.networkId != NetworkID.Invalid && matchInfo.nodeId != NodeID.Invalid)
				{
					matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, matchInfo.domain, OnMatchDropped);
				}
				OnClientDisconnect(conn);
				hasEverConnected = false;
				natHelper.StopPunchingThrough();
			}
		}

		public virtual void OnConnectionReplacedClient(NetworkConnection oldConnection, NetworkConnection newConnection)
		{
		}

		[Server]
		public virtual void OnConnectionReplacedServer(NetworkConnection oldConnection, NetworkConnection newConnection)
		{
		}

		[Server]
		public virtual void OnSetConnectionInfo(NetworkMessage msg)
		{
			ConnectionInfoMessage connectionInfoMessage = msg.ReadMessage<ConnectionInfoMessage>();
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: Received client connection info: " + connectionInfoMessage);
			}
			if (!connectedClientGUIDs.Contains(connectionInfoMessage.clientGUID) && numClients >= maxClients)
			{
				msg.conn.DisconnectConnection();
				return;
			}
			connectionInfoByConnection[msg.conn] = connectionInfoMessage;
			connectedClientGUIDs.Add(connectionInfoMessage.clientGUID);
			if (numClients >= maxClients && (bool)matchMaker && advertiseMatch && matchInfo != null)
			{
				matchMaker.SetMatchAttributes(matchInfo.networkId, false, matchInfo.domain, OnMatchAttributesUpdated);
			}
			if (connectionInfoMessage.connectionType != ConnectionType.RELAY)
			{
				NetworkConnection networkConnection = checkForAnotherConnectionFromTheSameClient(msg.conn, ConnectionType.RELAY);
				if (networkConnection != null)
				{
					if (networkConnection.playerControllers != null)
					{
						playerControllersField.SetValue(msg.conn, new List<PlayerController>(networkConnection.playerControllers));
						foreach (PlayerController playerController in networkConnection.playerControllers)
						{
							NetworkServer.AddPlayerForConnection(msg.conn, playerController.gameObject, playerController.playerControllerId);
						}
					}
					if (networkConnection.clientOwnedObjects != null)
					{
						clientOwnedObjectsField.SetValue(msg.conn, new HashSet<NetworkInstanceId>(networkConnection.clientOwnedObjects));
					}
					if (LogFilter.logDebug)
					{
						Debug.Log("NATTraversal: Relay connection will be dropped in favor of new direct connection.");
					}
					if (networkConnection.isReady)
					{
						msg.conn.isReady = true;
						foreach (NetworkIdentity value in NetworkServer.objects.Values)
						{
							if (!(value == null) && value.gameObject.activeSelf && OnCheckObserver(value, msg.conn))
							{
								AddObserver(value, msg.conn);
							}
						}
						msg.conn.isReady = true;
					}
					networkConnection.isReady = false;
				}
			}
			msg.conn.Send(MsgType.SetConnectionInfo, new EmptyMessage());
			if (migrationManager != null)
			{
				migrationManager.SendPeerInfo();
			}
		}

		public virtual void OnConnectionInfoConfirmationReceivedOnClient(NetworkMessage msg)
		{
			if (clientToReplaceRelayClientWith != null && clientToReplaceRelayClientWith.connection == msg.conn)
			{
				if (LogFilter.logInfo)
				{
					Debug.Log("NATTraversal: Telling server to replace relay connection with direct connection.");
				}
				clientToReplaceRelayClientWith = null;
				relayCon.Send(MsgType.ReplaceConnection, new EmptyMessage());
			}
		}

		public virtual void OnDoneConnectingToFacilitator(ulong guid)
		{
		}

		public virtual void replaceConnection(NetworkConnection oldConn, NetworkConnection newConn)
		{
			if (NetworkServer.active)
			{
				OnConnectionReplacedServer(oldConn, newConn);
			}
			else
			{
				OnConnectionReplacedClient(oldConn, newConn);
			}
			if (oldConn.playerControllers != null)
			{
				playerControllersField.SetValue(newConn, new List<PlayerController>(oldConn.playerControllers));
				foreach (PlayerController playerController in oldConn.playerControllers)
				{
					connectionToClientField.SetValue(playerController.unetView, newConn);
				}
				oldConn.playerControllers.Clear();
			}
			if (oldConn.clientOwnedObjects == null)
			{
				return;
			}
			clientOwnedObjectsField.SetValue(newConn, new HashSet<NetworkInstanceId>(oldConn.clientOwnedObjects));
			foreach (NetworkInstanceId clientOwnedObject in oldConn.clientOwnedObjects)
			{
				NetworkIdentity component = ClientScene.FindLocalObject(clientOwnedObject).GetComponent<NetworkIdentity>();
				clientAuthorityOwnerField.SetValue(component, newConn);
			}
			oldConn.clientOwnedObjects.Clear();
		}

		public virtual void initConfig()
		{
			Application.runInBackground = base.runInBackground;
			base.connectionConfig.Channels.Clear();
			if (base.customConfig)
			{
				foreach (QosType channel in base.channels)
				{
					base.connectionConfig.AddChannel(channel);
				}
			}
			else
			{
				base.connectionConfig.AddChannel(QosType.ReliableSequenced);
				base.connectionConfig.AddChannel(QosType.Unreliable);
			}
			int num = base.maxConnections;
			if (!base.customConfig)
			{
				num = (int)(matchSize - 1);
			}
			num *= 2;
			topo = new HostTopology(base.connectionConfig, num);
		}

		public virtual NetworkClient createClient()
		{
			NetworkClient networkClient = new NetworkClient();
			networkClient.Configure(topo);
			registerClientMessagesMethod.Invoke(this, new object[1] { networkClient });
			networkClient.RegisterHandler(32, OnMultiClientConnectMsg);
			networkClient.RegisterHandler(33, OnMultiClientDisconnectMsg);
			networkClient.RegisterHandler(MsgType.SetConnectionInfo, OnConnectionInfoConfirmationReceivedOnClient);
			foreach (KeyValuePair<short, NetworkMessageDelegate> clientMsgHandler in clientMsgHandlers)
			{
				networkClient.RegisterHandler(clientMsgHandler.Key, clientMsgHandler.Value);
			}
			return networkClient;
		}

		public virtual void RegisterHandlerClient(short type, NetworkMessageDelegate handler)
		{
			if (NetworkServer.active)
			{
				client.RegisterHandler(type, handler);
				return;
			}
			clientMsgHandlers[type] = handler;
			if (directClient != null)
			{
				directClient.RegisterHandler(type, handler);
			}
			if (punchthroughClient != null)
			{
				punchthroughClient.RegisterHandler(type, handler);
			}
			if (relayClient != null)
			{
				relayClient.RegisterHandler(type, handler);
			}
		}

		[Server]
		public virtual NetworkConnection checkForAnotherConnectionFromTheSameClient(NetworkConnection con, ConnectionType otherConnectionType = ConnectionType.ANY)
		{
			ConnectionInfoMessage value;
			if (!connectionInfoByConnection.TryGetValue(con, out value))
			{
				return null;
			}
			foreach (KeyValuePair<NetworkConnection, ConnectionInfoMessage> item in connectionInfoByConnection)
			{
				if (item.Key != con && item.Value.clientGUID == value.clientGUID && otherConnectionType.HasFlag(item.Value.connectionType))
				{
					return item.Key;
				}
			}
			return null;
		}

		public virtual string pickCorrectAddressToConnectTo(string hostExternalIP, string hostInternalIP, string hostExternalIPv6, string hostInternalIPv6)
		{
			if (!string.IsNullOrEmpty(externalIP) && !string.IsNullOrEmpty(hostExternalIP))
			{
				if (hostExternalIP == externalIP && !string.IsNullOrEmpty(hostInternalIP))
				{
					if (hostInternalIP == Network.player.ipAddress)
					{
						if (LogFilter.currentLogLevel == 0)
						{
							Debug.Log("NATTraversal: Using localhost address.");
						}
						return "127.0.0.1";
					}
					if (LogFilter.currentLogLevel == 0)
					{
						Debug.Log("NATTraversal: Using host's local ip address.");
					}
					return hostInternalIP;
				}
				if (LogFilter.currentLogLevel == 0)
				{
					Debug.Log("NATTraversal: Using host's external ip address.");
				}
				return hostExternalIP;
			}
			if (!string.IsNullOrEmpty(externalIPv6) && !string.IsNullOrEmpty(hostExternalIPv6))
			{
				if (hostExternalIPv6 == externalIPv6 && !string.IsNullOrEmpty(hostInternalIPv6))
				{
					if (hostInternalIPv6 == getLocalIPv6())
					{
						if (LogFilter.currentLogLevel == 0)
						{
							Debug.Log("NATTraversal: Using ipv6 localhost address.");
						}
						return "::1";
					}
					if (LogFilter.currentLogLevel == 0)
					{
						Debug.Log("NATTraversal: Using host's local ipv6 address.");
					}
					return hostInternalIPv6;
				}
				if (LogFilter.currentLogLevel == 0)
				{
					Debug.Log("NATTraversal: Using host's external ipv6 address.");
				}
				return hostExternalIPv6;
			}
			if (externalIPv6 == "" && hostExternalIPv6 != "")
			{
				Debug.LogError("NATTraversal: Host only supports ipv6 connections and client does not support ipv6.");
			}
			else if (externalIP == "" && hostExternalIP != "")
			{
				Debug.LogError("NATTraversal: Host only supports ipv4 connections and client does not support ipv4.");
			}
			return "";
		}

		private IEnumerator directConnectInAWhile(string externalIP, string internalIP, string externalIPv6, string internalIPv6)
		{
			yield return new WaitForSeconds(10f);
			directConnect(externalIP, internalIP, externalIPv6, internalIPv6);
			delayDirectConnectProcess = null;
		}

		public virtual bool ParseConnectionInfoFromMatchName(string matchName, out string externalIP, out string internalIP, out string externalIPv6, out string internalIPv6, out ulong guid)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: Match name to parse=" + matchName);
			}
			string[] array = matchName.Split('|');
			if (array.Length < 3)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATTraversal: No connection info in match name. Only relay connection is possible.");
				}
				externalIP = "";
				internalIP = "";
				externalIPv6 = "";
				internalIPv6 = "";
				guid = 0uL;
				return false;
			}
			internalIP = ((array.Length > 1) ? array[1] : "");
			externalIP = ((array.Length > 2) ? array[2] : "");
			internalIPv6 = ((array.Length > 3) ? array[3] : "");
			externalIPv6 = ((array.Length > 4) ? array[4] : "");
			guid = ((array.Length > 5) ? ulong.Parse(array[5]) : 0);
			return true;
		}

		public virtual IEnumerator getExternalIP()
		{
			WWW www = new WWW(externalIPSource);
			float startTime = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup - startTime < externalIPTimeout && !www.isDone)
			{
				yield return 0;
			}
			if (handleExternalIPResponse(www, out externalIP) && LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: External IP address fetched: " + externalIP);
			}
			isDoneFetchingExternalIPv4 = true;
		}

		public virtual IEnumerator getExternalIPv6()
		{
			WWW www = new WWW(externalIPv6Source);
			float startTime = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup - startTime < externalIPTimeout && !www.isDone)
			{
				yield return 0;
			}
			if (handleExternalIPResponse(www, out externalIPv6) && LogFilter.logDebug)
			{
				Debug.Log("NATTraversal: External IPv6 address fetched: " + externalIPv6);
			}
			isDoneFetchingExternalIPv6 = true;
		}

		private bool handleExternalIPResponse(WWW www, out string ip)
		{
			try
			{
				if (!www.isDone)
				{
					www.Dispose();
					if (LogFilter.logWarn)
					{
						Debug.LogWarning("NATTraversal: Timed out fetching ip from " + www.url);
					}
					ip = "";
					return false;
				}
				if (!string.IsNullOrEmpty(www.error))
				{
					if (LogFilter.logDebug)
					{
						Debug.Log("NATTraversal: Failed to fetch external IP: " + www.error);
					}
					ip = "";
					return false;
				}
				IPAddress address = null;
				string text = www.text.Trim();
				if (!IPAddress.TryParse(text, out address) || text != address.ToString())
				{
					if (LogFilter.logWarn)
					{
						Debug.LogWarning("NATTraversal: External ip source responded with something other than an IP: " + www.text);
					}
					ip = "";
					return false;
				}
				ip = address.ToString();
			}
			catch (Exception ex)
			{
				ip = "";
				if (LogFilter.logDebug)
				{
					Debug.Log("NATTraversal: Failed to fetch external IP: " + ex);
				}
				return false;
			}
			return true;
		}

		public virtual string getLocalIPv6()
		{
			try
			{
				NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
				foreach (NetworkInterface networkInterface in allNetworkInterfaces)
				{
					if (networkInterface.OperationalStatus != OperationalStatus.Up)
					{
						continue;
					}
					foreach (UnicastIPAddressInformation unicastAddress in networkInterface.GetIPProperties().UnicastAddresses)
					{
						if (unicastAddress.Address.AddressFamily == AddressFamily.InterNetworkV6)
						{
							return unicastAddress.Address.ToString().Trim();
						}
					}
				}
			}
			catch (Exception)
			{
				return "";
			}
			return "";
		}
	}
}
