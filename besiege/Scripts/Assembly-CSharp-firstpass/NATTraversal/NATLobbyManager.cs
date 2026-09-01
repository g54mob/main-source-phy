using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace NATTraversal
{
	public class NATLobbyManager : NetworkManager
	{
		private struct PendingPlayer
		{
			public NetworkConnection conn;

			public GameObject lobbyPlayer;
		}

		public class LobbyReadyToBeginMessage : MessageBase
		{
			public byte slotId;

			public bool readyState;

			public override void Deserialize(NetworkReader reader)
			{
				slotId = reader.ReadByte();
				readyState = reader.ReadBoolean();
			}

			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(slotId);
				writer.Write(readyState);
			}
		}

		[SerializeField]
		private bool m_ShowLobbyGUI = true;

		[SerializeField]
		private int m_MaxPlayers = 4;

		[SerializeField]
		private int m_MaxPlayersPerConnection = 1;

		[SerializeField]
		private int m_MinPlayers;

		[SerializeField]
		private NATLobbyPlayer m_LobbyPlayerPrefab;

		[SerializeField]
		private GameObject m_GamePlayerPrefab;

		[SerializeField]
		private string m_LobbyScene = string.Empty;

		[SerializeField]
		private string m_PlayScene = string.Empty;

		private List<PendingPlayer> m_PendingPlayers = new List<PendingPlayer>();

		public NATLobbyPlayer[] lobbySlots;

		private static LobbyReadyToBeginMessage s_ReadyToBeginMessage = new LobbyReadyToBeginMessage();

		private static IntegerMessage s_SceneLoadedMessage = new IntegerMessage();

		private static LobbyReadyToBeginMessage s_LobbyReadyToBeginMessage = new LobbyReadyToBeginMessage();

		public bool showLobbyGUI
		{
			get
			{
				return m_ShowLobbyGUI;
			}
			set
			{
				m_ShowLobbyGUI = value;
			}
		}

		public int maxPlayers
		{
			get
			{
				return m_MaxPlayers;
			}
			set
			{
				m_MaxPlayers = value;
			}
		}

		public int maxPlayersPerConnection
		{
			get
			{
				return m_MaxPlayersPerConnection;
			}
			set
			{
				m_MaxPlayersPerConnection = value;
			}
		}

		public int minPlayers
		{
			get
			{
				return m_MinPlayers;
			}
			set
			{
				m_MinPlayers = value;
			}
		}

		public NATLobbyPlayer lobbyPlayerPrefab
		{
			get
			{
				return m_LobbyPlayerPrefab;
			}
			set
			{
				m_LobbyPlayerPrefab = value;
			}
		}

		public GameObject gamePlayerPrefab
		{
			get
			{
				return m_GamePlayerPrefab;
			}
			set
			{
				m_GamePlayerPrefab = value;
			}
		}

		public string lobbyScene
		{
			get
			{
				return m_LobbyScene;
			}
			set
			{
				m_LobbyScene = value;
				base.offlineScene = value;
			}
		}

		public string playScene
		{
			get
			{
				return m_PlayScene;
			}
			set
			{
				m_PlayScene = value;
			}
		}

		private void OnValidate()
		{
			if (m_MaxPlayers <= 0)
			{
				m_MaxPlayers = 1;
			}
			if (m_MaxPlayersPerConnection <= 0)
			{
				m_MaxPlayersPerConnection = 1;
			}
			if (m_MaxPlayersPerConnection > maxPlayers)
			{
				m_MaxPlayersPerConnection = maxPlayers;
			}
			if (m_MinPlayers < 0)
			{
				m_MinPlayers = 0;
			}
			if (m_MinPlayers > m_MaxPlayers)
			{
				m_MinPlayers = m_MaxPlayers;
			}
			if (m_LobbyPlayerPrefab != null)
			{
				NetworkIdentity component = m_LobbyPlayerPrefab.GetComponent<NetworkIdentity>();
				if (component == null)
				{
					m_LobbyPlayerPrefab = null;
					Debug.LogWarning("LobbyPlayer prefab must have a NetworkIdentity component.");
				}
			}
			if (m_GamePlayerPrefab != null)
			{
				NetworkIdentity component2 = m_GamePlayerPrefab.GetComponent<NetworkIdentity>();
				if (component2 == null)
				{
					m_GamePlayerPrefab = null;
					Debug.LogWarning("GamePlayer prefab must have a NetworkIdentity component.");
				}
			}
		}

		private byte FindSlot()
		{
			for (byte b = 0; b < maxPlayers; b++)
			{
				if (lobbySlots[b] == null)
				{
					return b;
				}
			}
			return byte.MaxValue;
		}

		private void SceneLoadedForPlayer(NetworkConnection conn, GameObject lobbyPlayerGameObject)
		{
			NATLobbyPlayer component = lobbyPlayerGameObject.GetComponent<NATLobbyPlayer>();
			if (component == null)
			{
				return;
			}
			string text = SceneManager.GetSceneAt(0).name;
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobby SceneLoadedForPlayer scene:" + text + " " + conn);
			}
			if (text == m_LobbyScene)
			{
				PendingPlayer item = default(PendingPlayer);
				item.conn = conn;
				item.lobbyPlayer = lobbyPlayerGameObject;
				m_PendingPlayers.Add(item);
				return;
			}
			short playerControllerId = lobbyPlayerGameObject.GetComponent<NetworkIdentity>().playerControllerId;
			GameObject gameObject = OnLobbyServerCreateGamePlayer(conn, playerControllerId);
			if (gameObject == null)
			{
				Transform startPosition = GetStartPosition();
				gameObject = ((!(startPosition != null)) ? ((GameObject)Object.Instantiate(gamePlayerPrefab, Vector3.zero, Quaternion.identity)) : ((GameObject)Object.Instantiate(gamePlayerPrefab, startPosition.position, startPosition.rotation)));
			}
			if (OnLobbyServerSceneLoadedForPlayer(lobbyPlayerGameObject, gameObject))
			{
				NetworkServer.ReplacePlayerForConnection(conn, gameObject, playerControllerId);
			}
		}

		private static int CheckConnectionIsReadyToBegin(NetworkConnection conn)
		{
			int num = 0;
			for (int i = 0; i < conn.playerControllers.Count; i++)
			{
				PlayerController playerController = conn.playerControllers[i];
				if (playerController.IsValid)
				{
					NATLobbyPlayer component = playerController.gameObject.GetComponent<NATLobbyPlayer>();
					if (component.readyToBegin)
					{
						num++;
					}
				}
			}
			return num;
		}

		public void CheckReadyToBegin()
		{
			string text = SceneManager.GetSceneAt(0).name;
			if (text != m_LobbyScene)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					num2++;
					num += CheckConnectionIsReadyToBegin(networkConnection);
				}
			}
			if ((m_MinPlayers <= 0 || num >= m_MinPlayers) && num >= num2)
			{
				m_PendingPlayers.Clear();
				OnLobbyServerPlayersReady();
			}
		}

		public void ServerReturnToLobby()
		{
			if (!NetworkServer.active)
			{
				Debug.Log("ServerReturnToLobby called on client");
			}
			else
			{
				ServerChangeScene(m_LobbyScene);
			}
		}

		private void CallOnClientEnterLobby()
		{
			OnLobbyClientEnter();
			for (int i = 0; i < lobbySlots.Length; i++)
			{
				NATLobbyPlayer nATLobbyPlayer = lobbySlots[i];
				if (!(nATLobbyPlayer == null))
				{
					nATLobbyPlayer.readyToBegin = false;
					nATLobbyPlayer.OnClientEnterLobby();
				}
			}
		}

		private void CallOnClientExitLobby()
		{
			OnLobbyClientExit();
			for (int i = 0; i < lobbySlots.Length; i++)
			{
				NATLobbyPlayer nATLobbyPlayer = lobbySlots[i];
				if (!(nATLobbyPlayer == null))
				{
					nATLobbyPlayer.OnClientExitLobby();
				}
			}
		}

		public bool SendReturnToLobby()
		{
			if (client == null || !client.isConnected)
			{
				return false;
			}
			EmptyMessage msg = new EmptyMessage();
			client.Send(46, msg);
			return true;
		}

		public override void OnServerConnect(NetworkConnection conn)
		{
			if (base.numPlayers > maxPlayers)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat("NATLobbyManager can't accept new connection [", conn, "], too many players connected."));
				}
				conn.Disconnect();
				return;
			}
			string text = SceneManager.GetSceneAt(0).name;
			if (text != m_LobbyScene)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning(string.Concat("NATLobbyManager can't accept new connection [", conn, "], not in lobby and game already in progress."));
				}
				conn.Disconnect();
			}
			else
			{
				base.OnServerConnect(conn);
				OnLobbyServerConnect(conn);
			}
		}

		public override void OnServerDisconnect(NetworkConnection conn)
		{
			base.OnServerDisconnect(conn);
			for (int i = 0; i < lobbySlots.Length; i++)
			{
				NATLobbyPlayer nATLobbyPlayer = lobbySlots[i];
				if (!(nATLobbyPlayer == null) && nATLobbyPlayer.connectionToClient == conn)
				{
					lobbySlots[i] = null;
					NetworkServer.Destroy(nATLobbyPlayer.gameObject);
				}
			}
			OnLobbyServerDisconnect(conn);
		}

		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
		{
			string text = SceneManager.GetSceneAt(0).name;
			if (text != m_LobbyScene)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < conn.playerControllers.Count; i++)
			{
				if (conn.playerControllers[i].IsValid)
				{
					num++;
				}
			}
			if (num >= maxPlayersPerConnection)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATLobbyManager no more players for this connection.");
				}
				EmptyMessage msg = new EmptyMessage();
				conn.Send(45, msg);
				return;
			}
			byte b = FindSlot();
			if (b == byte.MaxValue)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NATLobbyManager no space for more players");
				}
				EmptyMessage msg2 = new EmptyMessage();
				conn.Send(45, msg2);
				return;
			}
			GameObject gameObject = OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
			if (gameObject == null)
			{
				gameObject = (GameObject)Object.Instantiate(lobbyPlayerPrefab.gameObject, Vector3.zero, Quaternion.identity);
			}
			NATLobbyPlayer component = gameObject.GetComponent<NATLobbyPlayer>();
			component.slot = b;
			lobbySlots[b] = component;
			NetworkServer.AddPlayerForConnection(conn, gameObject, playerControllerId);
		}

		public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
		{
			short playerControllerId = player.playerControllerId;
			byte slot = player.gameObject.GetComponent<NATLobbyPlayer>().slot;
			lobbySlots[slot] = null;
			base.OnServerRemovePlayer(conn, player);
			for (int i = 0; i < lobbySlots.Length; i++)
			{
				NATLobbyPlayer nATLobbyPlayer = lobbySlots[i];
				if (nATLobbyPlayer != null)
				{
					nATLobbyPlayer.GetComponent<NATLobbyPlayer>().readyToBegin = false;
					s_LobbyReadyToBeginMessage.slotId = nATLobbyPlayer.slot;
					s_LobbyReadyToBeginMessage.readyState = false;
					NetworkServer.SendToReady(null, 43, s_LobbyReadyToBeginMessage);
				}
			}
			OnLobbyServerPlayerRemoved(conn, playerControllerId);
		}

		public override void ServerChangeScene(string sceneName)
		{
			if (sceneName == m_LobbyScene)
			{
				for (int i = 0; i < lobbySlots.Length; i++)
				{
					NATLobbyPlayer nATLobbyPlayer = lobbySlots[i];
					if (!(nATLobbyPlayer == null))
					{
						NetworkIdentity component = nATLobbyPlayer.GetComponent<NetworkIdentity>();
						PlayerController playerController;
						if (GetPlayerController(component.connectionToClient, component.playerControllerId, out playerController))
						{
							NetworkServer.Destroy(playerController.gameObject);
						}
						if (NetworkServer.active)
						{
							nATLobbyPlayer.GetComponent<NATLobbyPlayer>().readyToBegin = false;
							NetworkServer.ReplacePlayerForConnection(component.connectionToClient, nATLobbyPlayer.gameObject, component.playerControllerId);
						}
					}
				}
			}
			base.ServerChangeScene(sceneName);
		}

		private bool GetPlayerController(NetworkConnection client, short playerControllerId, out PlayerController playerController)
		{
			playerController = null;
			if (client.playerControllers.Count > 0)
			{
				for (int i = 0; i < client.playerControllers.Count; i++)
				{
					if (client.playerControllers[i].IsValid && client.playerControllers[i].playerControllerId == playerControllerId)
					{
						playerController = client.playerControllers[i];
						return true;
					}
				}
				return false;
			}
			return false;
		}

		public override void OnServerSceneChanged(string sceneName)
		{
			if (sceneName != m_LobbyScene)
			{
				for (int i = 0; i < m_PendingPlayers.Count; i++)
				{
					PendingPlayer pendingPlayer = m_PendingPlayers[i];
					SceneLoadedForPlayer(pendingPlayer.conn, pendingPlayer.lobbyPlayer);
				}
				m_PendingPlayers.Clear();
			}
			OnLobbyServerSceneChanged(sceneName);
		}

		private void OnServerReadyToBeginMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyManager OnServerReadyToBeginMessage");
			}
			netMsg.ReadMessage(s_ReadyToBeginMessage);
			PlayerController playerController;
			if (!GetPlayerController(netMsg.conn, s_ReadyToBeginMessage.slotId, out playerController))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager OnServerReadyToBeginMessage invalid playerControllerId " + s_ReadyToBeginMessage.slotId);
				}
				return;
			}
			NATLobbyPlayer component = playerController.gameObject.GetComponent<NATLobbyPlayer>();
			component.readyToBegin = s_ReadyToBeginMessage.readyState;
			LobbyReadyToBeginMessage lobbyReadyToBeginMessage = new LobbyReadyToBeginMessage();
			lobbyReadyToBeginMessage.slotId = component.slot;
			lobbyReadyToBeginMessage.readyState = s_ReadyToBeginMessage.readyState;
			NetworkServer.SendToReady(null, 43, lobbyReadyToBeginMessage);
			CheckReadyToBegin();
		}

		private void OnServerSceneLoadedMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyManager OnSceneLoadedMessage");
			}
			netMsg.ReadMessage(s_SceneLoadedMessage);
			PlayerController playerController;
			if (!GetPlayerController(netMsg.conn, (short)s_SceneLoadedMessage.value, out playerController))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager OnServerSceneLoadedMessage invalid playerControllerId " + s_SceneLoadedMessage.value);
				}
			}
			else
			{
				SceneLoadedForPlayer(netMsg.conn, playerController.gameObject);
			}
		}

		private void OnServerReturnToLobbyMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyManager OnServerReturnToLobbyMessage");
			}
			ServerReturnToLobby();
		}

		public override void OnStartServer()
		{
			if (string.IsNullOrEmpty(m_LobbyScene))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager LobbyScene is empty. Set the LobbyScene in the inspector for the NATLobbyMangaer");
				}
				return;
			}
			if (string.IsNullOrEmpty(m_PlayScene))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager PlayScene is empty. Set the PlayScene in the inspector for the NATLobbyMangaer");
				}
				return;
			}
			if (lobbySlots.Length == 0)
			{
				lobbySlots = new NATLobbyPlayer[maxPlayers];
			}
			NetworkServer.RegisterHandler(43, OnServerReadyToBeginMessage);
			NetworkServer.RegisterHandler(44, OnServerSceneLoadedMessage);
			NetworkServer.RegisterHandler(46, OnServerReturnToLobbyMessage);
			OnLobbyStartServer();
		}

		public override void OnStartHost()
		{
			OnLobbyStartHost();
		}

		public override void OnStopHost()
		{
			OnLobbyStopHost();
		}

		public override void OnStartClient(NetworkClient lobbyClient)
		{
			if (lobbySlots.Length == 0)
			{
				lobbySlots = new NATLobbyPlayer[maxPlayers];
			}
			if (m_LobbyPlayerPrefab == null || m_LobbyPlayerPrefab.gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager no LobbyPlayer prefab is registered. Please add a LobbyPlayer prefab.");
				}
			}
			else
			{
				ClientScene.RegisterPrefab(m_LobbyPlayerPrefab.gameObject);
			}
			if (m_GamePlayerPrefab == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager no GamePlayer prefab is registered. Please add a GamePlayer prefab.");
				}
			}
			else
			{
				ClientScene.RegisterPrefab(m_GamePlayerPrefab);
			}
			lobbyClient.RegisterHandler(43, OnClientReadyToBegin);
			lobbyClient.RegisterHandler(45, OnClientAddPlayerFailedMessage);
			OnLobbyStartClient(lobbyClient);
		}

		public override void OnClientConnect(NetworkConnection conn)
		{
			OnLobbyClientConnect(conn);
			CallOnClientEnterLobby();
			base.OnClientConnect(conn);
		}

		public override void OnClientDisconnect(NetworkConnection conn)
		{
			OnLobbyClientDisconnect(conn);
			base.OnClientDisconnect(conn);
		}

		public override void OnStopClient()
		{
			OnLobbyStopClient();
			CallOnClientExitLobby();
		}

		public override void OnClientSceneChanged(NetworkConnection conn)
		{
			string text = SceneManager.GetSceneAt(0).name;
			if (text == m_LobbyScene)
			{
				if (client.isConnected)
				{
					CallOnClientEnterLobby();
				}
			}
			else
			{
				CallOnClientExitLobby();
			}
			base.OnClientSceneChanged(conn);
			OnLobbyClientSceneChanged(conn);
		}

		private void OnClientReadyToBegin(NetworkMessage netMsg)
		{
			netMsg.ReadMessage(s_LobbyReadyToBeginMessage);
			if (s_LobbyReadyToBeginMessage.slotId >= lobbySlots.Count())
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager OnClientReadyToBegin invalid lobby slot " + s_LobbyReadyToBeginMessage.slotId);
				}
				return;
			}
			NATLobbyPlayer nATLobbyPlayer = lobbySlots[s_LobbyReadyToBeginMessage.slotId];
			if (nATLobbyPlayer == null || nATLobbyPlayer.gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NATLobbyManager OnClientReadyToBegin no player at lobby slot " + s_LobbyReadyToBeginMessage.slotId);
				}
			}
			else
			{
				nATLobbyPlayer.readyToBegin = s_LobbyReadyToBeginMessage.readyState;
				nATLobbyPlayer.OnClientReady(s_LobbyReadyToBeginMessage.readyState);
			}
		}

		private void OnClientAddPlayerFailedMessage(NetworkMessage netMsg)
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyManager Add Player failed.");
			}
			OnLobbyClientAddPlayerFailed();
		}

		public virtual void OnLobbyStartHost()
		{
		}

		public virtual void OnLobbyStopHost()
		{
		}

		public virtual void OnLobbyStartServer()
		{
		}

		public virtual void OnLobbyServerConnect(NetworkConnection conn)
		{
		}

		public virtual void OnLobbyServerDisconnect(NetworkConnection conn)
		{
		}

		public virtual void OnLobbyServerSceneChanged(string sceneName)
		{
		}

		public virtual GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
		{
			return null;
		}

		public virtual GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
		{
			return null;
		}

		public virtual void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
		{
		}

		public virtual bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
		{
			return true;
		}

		public virtual void OnLobbyServerPlayersReady()
		{
			ServerChangeScene(m_PlayScene);
		}

		public virtual void OnLobbyClientEnter()
		{
		}

		public virtual void OnLobbyClientExit()
		{
		}

		public virtual void OnLobbyClientConnect(NetworkConnection conn)
		{
		}

		public virtual void OnLobbyClientDisconnect(NetworkConnection conn)
		{
		}

		public virtual void OnLobbyStartClient(NetworkClient lobbyClient)
		{
		}

		public virtual void OnLobbyStopClient()
		{
		}

		public virtual void OnLobbyClientSceneChanged(NetworkConnection conn)
		{
		}

		public virtual void OnLobbyClientAddPlayerFailed()
		{
		}

		private void OnGUI()
		{
			if (!showLobbyGUI)
			{
				return;
			}
			string text = SceneManager.GetSceneAt(0).name;
			if (text != m_LobbyScene)
			{
				return;
			}
			Rect position = new Rect(90f, 180f, 500f, 150f);
			GUI.Box(position, "Players:");
			if (NetworkClient.active)
			{
				Rect position2 = new Rect(100f, 300f, 120f, 20f);
				if (GUI.Button(position2, "Add Player"))
				{
					TryToAddPlayer();
				}
			}
		}

		public void TryToAddPlayer()
		{
			if (NetworkClient.active)
			{
				short num = -1;
				List<PlayerController> playerControllers = client.connection.playerControllers;
				if (playerControllers.Count < maxPlayers)
				{
					num = (short)playerControllers.Count;
				}
				else
				{
					for (short num2 = 0; num2 < maxPlayers; num2++)
					{
						if (!playerControllers[num2].IsValid)
						{
							num = num2;
							break;
						}
					}
				}
				if (LogFilter.logDebug)
				{
					Debug.Log("NATLobbyManager TryToAddPlayer controllerId " + num + " ready:" + ClientScene.ready);
				}
				if (num == -1)
				{
					if (LogFilter.logDebug)
					{
						Debug.Log("NATLobbyManager No Space!");
					}
				}
				else if (ClientScene.ready)
				{
					ClientScene.AddPlayer(num);
				}
				else
				{
					ClientScene.AddPlayer(NetworkClient.allClients[0].connection, num);
				}
			}
			else if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyManager NetworkClient not active!");
			}
		}
	}
}
