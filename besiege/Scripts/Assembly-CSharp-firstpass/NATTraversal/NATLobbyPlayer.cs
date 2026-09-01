using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace NATTraversal
{
	[DisallowMultipleComponent]
	public class NATLobbyPlayer : NetworkBehaviour
	{
		[SerializeField]
		public bool ShowLobbyGUI = true;

		private byte m_Slot;

		private bool m_ReadyToBegin;

		public byte slot
		{
			get
			{
				return m_Slot;
			}
			set
			{
				m_Slot = value;
			}
		}

		public bool readyToBegin
		{
			get
			{
				return m_ReadyToBegin;
			}
			set
			{
				m_ReadyToBegin = value;
			}
		}

		private void Start()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		public override void OnStartClient()
		{
			NATLobbyManager nATLobbyManager = UnityEngine.Networking.NetworkManager.singleton as NATLobbyManager;
			if ((bool)nATLobbyManager)
			{
				nATLobbyManager.lobbySlots[m_Slot] = this;
				m_ReadyToBegin = false;
				OnClientEnterLobby();
			}
			else
			{
				Debug.LogError("LobbyPlayer could not find a NATLobbyManager. The LobbyPlayer requires a NATLobbyManager object to function. Make sure that there is one in the scene.");
			}
		}

		public void SendReadyToBeginMessage()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyPlayer SendReadyToBeginMessage");
			}
			NATLobbyManager nATLobbyManager = UnityEngine.Networking.NetworkManager.singleton as NATLobbyManager;
			if ((bool)nATLobbyManager)
			{
				NATLobbyManager.LobbyReadyToBeginMessage lobbyReadyToBeginMessage = new NATLobbyManager.LobbyReadyToBeginMessage();
				lobbyReadyToBeginMessage.slotId = (byte)base.playerControllerId;
				lobbyReadyToBeginMessage.readyState = true;
				nATLobbyManager.client.Send(43, lobbyReadyToBeginMessage);
			}
		}

		public void SendNotReadyToBeginMessage()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyPlayer SendReadyToBeginMessage");
			}
			NATLobbyManager nATLobbyManager = UnityEngine.Networking.NetworkManager.singleton as NATLobbyManager;
			if ((bool)nATLobbyManager)
			{
				NATLobbyManager.LobbyReadyToBeginMessage lobbyReadyToBeginMessage = new NATLobbyManager.LobbyReadyToBeginMessage();
				lobbyReadyToBeginMessage.slotId = (byte)base.playerControllerId;
				lobbyReadyToBeginMessage.readyState = false;
				nATLobbyManager.client.Send(43, lobbyReadyToBeginMessage);
			}
		}

		public void SendSceneLoadedMessage()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NATLobbyPlayer SendSceneLoadedMessage");
			}
			NATLobbyManager nATLobbyManager = UnityEngine.Networking.NetworkManager.singleton as NATLobbyManager;
			if ((bool)nATLobbyManager)
			{
				IntegerMessage msg = new IntegerMessage(base.playerControllerId);
				nATLobbyManager.client.Send(44, msg);
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			NATLobbyManager nATLobbyManager = UnityEngine.Networking.NetworkManager.singleton as NATLobbyManager;
			if ((bool)nATLobbyManager)
			{
				string text = scene.name;
				if (text == nATLobbyManager.lobbyScene)
				{
					return;
				}
			}
			if (base.isLocalPlayer)
			{
				SendSceneLoadedMessage();
			}
		}

		public void RemovePlayer()
		{
			if (base.isLocalPlayer && !m_ReadyToBegin)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NATLobbyPlayer RemovePlayer");
				}
				ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
			}
		}

		public virtual void OnClientEnterLobby()
		{
		}

		public virtual void OnClientExitLobby()
		{
		}

		public virtual void OnClientReady(bool readyState)
		{
		}

		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			writer.WritePackedUInt32(1u);
			writer.Write(m_Slot);
			writer.Write(m_ReadyToBegin);
			return true;
		}

		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (reader.ReadPackedUInt32() != 0)
			{
				m_Slot = reader.ReadByte();
				m_ReadyToBegin = reader.ReadBoolean();
			}
		}

		private void OnGUI()
		{
			if (!ShowLobbyGUI)
			{
				return;
			}
			NATLobbyManager nATLobbyManager = UnityEngine.Networking.NetworkManager.singleton as NATLobbyManager;
			if ((bool)nATLobbyManager)
			{
				if (!nATLobbyManager.showLobbyGUI)
				{
					return;
				}
				string text = SceneManager.GetSceneAt(0).name;
				if (text != nATLobbyManager.lobbyScene)
				{
					return;
				}
			}
			Rect position = new Rect(100 + m_Slot * 100, 200f, 90f, 20f);
			if (base.isLocalPlayer)
			{
				GUI.Label(text: (!m_ReadyToBegin) ? "(Not Ready)" : "(Ready)", position: position);
				if (m_ReadyToBegin)
				{
					position.y += 25f;
					if (GUI.Button(position, "STOP"))
					{
						SendNotReadyToBeginMessage();
					}
					return;
				}
				position.y += 25f;
				if (GUI.Button(position, "START"))
				{
					SendReadyToBeginMessage();
				}
				position.y += 25f;
				if (GUI.Button(position, "Remove"))
				{
					ClientScene.RemovePlayer(GetComponent<NetworkIdentity>().playerControllerId);
				}
			}
			else
			{
				GUI.Label(position, string.Concat("Player [", base.netId, "]"));
				position.y += 25f;
				GUI.Label(position, "Ready [" + m_ReadyToBegin + "]");
			}
		}

		private void UNetVersion()
		{
		}
	}
}
