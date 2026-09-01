using System;
using System.Collections;
using System.Collections.Generic;
using Localisation;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Party;
using PlayFab.ProfilesModels;
using UnityEngine;

public class PlayfabConnection : BaseConnection
{
	public class PlayfabNetworkPlayer
	{
		public PlayFabPlayer pfPlayer;

		public ushort networkId;

		public string platformUserName;

		public PlayerData playerData;

		public int Ping;
	}

	private const float KeepAliveInterval = 0.5f;

	private const byte pingPacket = 69;

	private const byte pingResultPacket = 70;

	private const byte disconnectPacket = 71;

	private string playfabNetworkId;

	private int ping;

	private float lastAckSent;

	private bool isHosting;

	private bool isPublicSession = true;

	private bool restrictComms = true;

	private ushort currentPlayerId;

	private int pfLoginRetries;

	private List<PlayfabNetworkPlayer> playerList;

	private PlayFabPlayer HostPlayer;

	private bool hasHost;

	private bool isInitialized;

	private List<DeliveryOption> channelTypes = new List<DeliveryOption>();

	private static PlayFabMultiplayerManager mpManager;

	private WorkshopManager workshopManager;

	public override string CurrentNetwork
	{
		get
		{
			return playfabNetworkId;
		}
	}

	public override string NetworkString
	{
		get
		{
			return LocalisationManager.GetTranslation(16);
		}
	}

	public override int AddChannel(BesiegeQosType qosType)
	{
		DeliveryOption item = ((qosType == BesiegeQosType.Reliable || qosType == BesiegeQosType.AllCostDelivery) ? DeliveryOption.Guaranteed : DeliveryOption.BestEffort);
		int count = channelTypes.Count;
		channelTypes.Add(item);
		return count;
	}

	public override void BroadcastMessage(int channel, byte[] data)
	{
		byte[] array = new byte[data.Length + 1];
		array[0] = (byte)channel;
		Buffer.BlockCopy(data, 0, array, 1, data.Length);
		mpManager.SendDataMessageToAllPlayers(array);
	}

	public override void ConnectToIP(string serverAddress, int serverPort)
	{
		Debug.LogError("[PlayfabConnection] ConnectIP not implemented for PlayfabConnection!");
	}

	public override void ConnectToLobby(ulong lobbyId)
	{
		Debug.LogError("[PlayfabConnection] ConnectToLobby not implemented for PlayfabConnection!");
	}

	public override void ConnectPlayfab(string pfNetworkId)
	{
		InitTransport();
		SetClientConnectionState(ClientConnectionState.PlayfabLogin);
		SingleInstance<WorkshopManager>.Instance.GetMultiplayerPermission(delegate(bool allow)
		{
			if (!allow)
			{
				Debug.LogError("[PlayfabConnection] ConnectPlayfab User doesn't have permission to play Multiplayer!");
				SetClientConnectionState(ClientConnectionState.Disconnected);
			}
			else
			{
				pfLoginRetries = 5;
				PlayfabSignin(delegate(bool success)
				{
					if (clientConnectionState == ClientConnectionState.PlayfabLogin)
					{
						if (success)
						{
							connectAddress = pfNetworkId;
							mpManager.JoinNetwork(pfNetworkId);
							SetClientConnectionState(ClientConnectionState.Connecting);
						}
						else
						{
							Debug.LogError("[PlayfabConnection] ConnectPlayfab Failed to sign into Playfab!");
							SetClientConnectionState(ClientConnectionState.Disconnected);
						}
					}
					else
					{
						Debug.Log("[PlayfabConnection] ConnectPlayfab The connection was cancelled, cancelling");
					}
				});
			}
		});
	}

	public override void Disconnect()
	{
		ShutdownClient();
	}

	public override void DisconnectPlayer(ushort playerId)
	{
		if (isHosting)
		{
			PlayfabNetworkPlayer playfabNetworkPlayer = playerList.Find((PlayfabNetworkPlayer x) => x.networkId == playerId);
			if (playfabNetworkPlayer == null || playfabNetworkPlayer.playerData == null)
			{
				Debug.LogError("[PlayfabConnection] DisconnectPlayer Couldn't disconnect player " + playerId + ", player isn't connected!");
				return;
			}
			byte[] buffer = new byte[1] { 71 };
			Debug.Log("[PlayfabConnection] DisconnectPlayer Disconnecting player " + playerId + "..");
			mpManager.SendDataMessage(buffer, new PlayFabPlayer[1] { playfabNetworkPlayer.pfPlayer }, DeliveryOption.Guaranteed);
		}
	}

	public override int GetPing(ushort playerId)
	{
		PlayfabNetworkPlayer playfabNetworkPlayer = playerList.Find((PlayfabNetworkPlayer x) => x.playerData != null && x.networkId == playerId);
		if (playfabNetworkPlayer == null)
		{
			return 0;
		}
		return playfabNetworkPlayer.Ping;
	}

	public override int Ping()
	{
		return ping;
	}

	public override bool Listen(int serverPort)
	{
		Debug.Log("[PlayfabConnection] Listen Starting host..");
		InitTransport();
		SetClientConnectionState(ClientConnectionState.PlayfabLogin);
		SingleInstance<WorkshopManager>.Instance.GetMultiplayerPermission(delegate(bool allow)
		{
			if (!allow)
			{
				Debug.LogError("[PlayfabConnection] Listen User doesn't have permission to play Multiplayer!");
				SetClientConnectionState(ClientConnectionState.Disconnected);
			}
			else
			{
				pfLoginRetries = 5;
				PlayfabSignin(delegate(bool success)
				{
					if (success)
					{
						SetClientConnectionState(ClientConnectionState.Connecting);
						SetServerConnectionState(ServerConnectionState.WaitingForConnection);
						mpManager.CreateAndJoinNetwork();
						playerList.Clear();
					}
					else
					{
						Debug.LogError("[PlayfabConnection] Listen Failed to sign into Playfab!");
						SetClientConnectionState(ClientConnectionState.Disconnected);
					}
				});
			}
		});
		return true;
	}

	private void PlayfabSignin(Action<bool> onComplete)
	{
		workshopManager.PlayfabSignin(delegate(bool success)
		{
			if (success)
			{
				onComplete(true);
			}
			else if (pfLoginRetries > 0)
			{
				pfLoginRetries--;
				PlayfabSignin(onComplete);
			}
			else
			{
				onComplete(false);
			}
		});
	}

	public override void SendNetworkMessage(ushort playerId, int channel, byte[] data)
	{
		byte[] array;
		if (data == null)
		{
			array = new byte[1] { (byte)channel };
		}
		else
		{
			array = new byte[data.Length + 1];
			array[0] = (byte)channel;
			Buffer.BlockCopy(data, 0, array, 1, data.Length);
		}
		PlayFabPlayer playFabPlayer;
		if (isHosting)
		{
			PlayfabNetworkPlayer playfabNetworkPlayer = playerList.Find((PlayfabNetworkPlayer x) => x.networkId == playerId);
			if (playfabNetworkPlayer == null)
			{
				Debug.LogError("[PlayfabConnection] SendNetworkMessage Couldn't find network player!");
				return;
			}
			playFabPlayer = playfabNetworkPlayer.pfPlayer;
		}
		else
		{
			if (!hasHost)
			{
				Debug.LogError("[PlayfabConnection] SendNetworkMessage Trying to send message on the client, but host hasn't been found yet!");
				return;
			}
			playFabPlayer = HostPlayer;
		}
		mpManager.SendDataMessage(array, new PlayFabPlayer[1] { playFabPlayer }, channelTypes[channel]);
		connectionHandler.IncrementSentMessages();
		trafficOut += (ulong)data.Length;
	}

	public override void SetPlayerID(ushort id, ulong lobbyId)
	{
		networkID = id;
		Debug.Log("[PlayfabConnection] SetPlayerID id=" + id);
		connectionHandler.OnConnected();
	}

	public override void Initialize()
	{
		workshopManager = SingleInstance<WorkshopManager>.Instance;
		if (mpManager == null)
		{
			GameObject gameObject = new GameObject("PlayFab MPManager");
			gameObject.SetActive(false);
			mpManager = gameObject.AddComponent<PlayFabMultiplayerManager>();
			gameObject.SetActive(true);
			workshopManager.InitializePlayfabManager(mpManager);
			OptionsMaster.BesiegeConfig.MaximumTransmissionUnit = 1480;
		}
		if (!isInitialized)
		{
			playerList = new List<PlayfabNetworkPlayer>();
			networkID = 0;
			mpManager.OnError += OnError;
			mpManager.OnNetworkJoined += OnNetworkJoined;
			mpManager.OnRemotePlayerJoined += OnPlayerJoin;
			mpManager.OnRemotePlayerLeft += OnPlayerLeft;
			isInitialized = true;
		}
		isHosting = StatMaster.isServer;
	}

	private void OnInvalidNetworkId()
	{
		SetClientConnectionState(ClientConnectionState.Disconnected);
		connectionHandler.OnDisconnected(false);
		NetworkAuxAddPiece.Instance.hud.ShowMessage(LocalisationManager.GetTranslation(17));
	}

	private void OnGameStateComplete()
	{
		if (clientConnectionState == ClientConnectionState.Disconnected)
		{
			return;
		}
		SetClientConnectionState(ClientConnectionState.Connected);
		List<ulong> list = new List<ulong>();
		for (int i = 0; i < Playerlist.Players.Count; i++)
		{
			PlayerData playerData = Playerlist.Players[i];
			if (!playerData.isLocalPlayer && playerData.networkType == PlayerNetworkType.Playfab)
			{
				list.Add(Playerlist.Players[i].platformUserId);
			}
		}
		workshopManager.UpdateRecentPlayers(list.ToArray());
		workshopManager.StartActivity(playfabNetworkId, true);
	}

	private bool Compare(PlayFabPlayer a, PlayFabPlayer b)
	{
		return a.EntityKey.Id.Equals(b.EntityKey.Id) && a.EntityKey.Type.Equals(b.EntityKey.Type);
	}

	private void OnDataMessage(object sender, PlayFabPlayer from, byte[] buffer)
	{
		if (clientConnectionState != ClientConnectionState.Connecting && clientConnectionState != ClientConnectionState.Connected)
		{
			return;
		}
		int num;
		if (isHosting)
		{
			num = playerList.FindIndex((PlayfabNetworkPlayer x) => Compare(x.pfPlayer, from));
			if (num == -1)
			{
				return;
			}
		}
		else
		{
			if (!hasHost)
			{
				HostPlayer = from;
				hasHost = true;
			}
			num = 0;
		}
		byte b = buffer[0];
		PlayfabNetworkPlayer playfabNetworkPlayer = playerList[num];
		switch (b)
		{
		case 69:
		{
			if (buffer[1] == 1)
			{
				byte[] array2 = new byte[6] { 69, 0, 0, 0, 0, 0 };
				Buffer.BlockCopy(buffer, 2, array2, 2, 4);
				mpManager.SendDataMessage(array2, new PlayFabPlayer[1] { from }, DeliveryOption.Guaranteed);
				break;
			}
			float num2 = BitConverter.ToSingle(buffer, 2);
			playfabNetworkPlayer.Ping = Mathf.RoundToInt((Time.time - num2) * 1000f);
			if (!isHosting && Compare(HostPlayer, from))
			{
				ping = playfabNetworkPlayer.Ping;
			}
			byte[] array3 = new byte[5] { 70, 0, 0, 0, 0 };
			byte[] bytes = BitConverter.GetBytes(playfabNetworkPlayer.Ping);
			Buffer.BlockCopy(bytes, 0, array3, 1, bytes.Length);
			mpManager.SendDataMessageToAllPlayers(array3);
			break;
		}
		case 70:
			playfabNetworkPlayer.Ping = BitConverter.ToInt32(buffer, 1);
			break;
		case 71:
			Debug.Log("[PlayfabConnection] OnDataMessage Received shutdown message from server, shutting down connection!");
			Shutdown();
			break;
		default:
		{
			byte[] array = new byte[buffer.Length - 1];
			Buffer.BlockCopy(buffer, 1, array, 0, array.Length);
			trafficIn += (ulong)array.Length;
			connectionHandler.OnDataEvent(playfabNetworkPlayer.networkId, b, array, array.Length);
			break;
		}
		}
	}

	private void InitTransport()
	{
		playerList.Clear();
	}

	private void OnPlayerJoin(object sender, PlayFabPlayer player)
	{
		if (restrictComms)
		{
			player.IsMuted = true;
		}
		PlayfabNetworkPlayer playfabNetworkPlayer = new PlayfabNetworkPlayer();
		playfabNetworkPlayer.pfPlayer = player;
		PlayfabNetworkPlayer playfabNetworkPlayer2 = playfabNetworkPlayer;
		playerList.Add(playfabNetworkPlayer2);
		if (isHosting)
		{
			ushort num = (playfabNetworkPlayer2.networkId = currentPlayerId++);
			connectionHandler.OnPlayerJoin(num);
			playfabNetworkPlayer2.playerData = Playerlist.GetPlayer(num);
		}
	}

	private void GetDisplayName(PlayfabNetworkPlayer networkPlayer, Action<bool, string> onComplete)
	{
		PlayFabPlayer pfPlayer = networkPlayer.pfPlayer;
		GetEntityProfileRequest getEntityProfileRequest = new GetEntityProfileRequest();
		getEntityProfileRequest.Entity = new PlayFab.ProfilesModels.EntityKey
		{
			Id = pfPlayer.EntityKey.Id,
			Type = pfPlayer.EntityKey.Type
		};
		GetEntityProfileRequest request = getEntityProfileRequest;
		PlayFabProfilesAPI.GetProfile(request, delegate(GetEntityProfileResponse result)
		{
			string masterPlayerAccountId = result.Profile.Lineage.MasterPlayerAccountId;
			PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
			{
				PlayFabId = masterPlayerAccountId,
				ProfileConstraints = new PlayerProfileViewConstraints
				{
					ShowDisplayName = true
				}
			}, delegate(GetPlayerProfileResult profileResult)
			{
				networkPlayer.platformUserName = profileResult.PlayerProfile.DisplayName;
				onComplete(true, networkPlayer.platformUserName);
			}, delegate
			{
				onComplete(false, null);
			});
		}, delegate
		{
			onComplete(false, null);
		});
	}

	private void OnPlayerInitReady(PlayerData data, bool success)
	{
		if (!success)
		{
			return;
		}
		bool flag = false;
		PlayfabNetworkPlayer networkPlayer;
		for (int i = 0; i < playerList.Count; i++)
		{
			networkPlayer = playerList[i];
			if (networkPlayer.pfPlayer.uniquePlatformId == data.platformUserId || (!string.IsNullOrEmpty(networkPlayer.platformUserName) && networkPlayer.platformUserName.Equals(data.platformUserName)))
			{
				flag = true;
				SetNetworkPlayer(networkPlayer, data);
				break;
			}
			if (!string.IsNullOrEmpty(networkPlayer.platformUserName))
			{
				continue;
			}
			GetDisplayName(networkPlayer, delegate(bool s, string displayName)
			{
				if (s && data.platformUserName.Equals(displayName))
				{
					SetNetworkPlayer(networkPlayer, data);
				}
			});
			flag = true;
		}
		if (!flag)
		{
			Debug.LogError("[PlayfabConnection] OnPlayerInitReady Couldn't find player with ID " + data.networkId + "!");
		}
	}

	private void SetNetworkPlayer(PlayfabNetworkPlayer networkPlayer, PlayerData data)
	{
		Debug.Log("[PlayfabConnection] SetNetworkPlayer Found player " + data.platformUserName + "!");
		networkPlayer.playerData = data;
		if (data.platform == PlayerPlatform.GDK)
		{
			workshopManager.UpdateRecentPlayers(new ulong[1] { data.platformUserId });
			workshopManager.UpdateMute(networkPlayer, mpManager);
		}
		workshopManager.UpdateActivity(isPublicSession);
	}

	private void OnPlayerLeft(object sender, PlayFabPlayer player)
	{
		int num = playerList.FindIndex((PlayfabNetworkPlayer x) => x.pfPlayer == player);
		if (num != -1)
		{
			if (!isHosting && player == HostPlayer)
			{
				connectionHandler.OnDisconnected(false);
				return;
			}
			PlayfabNetworkPlayer playfabNetworkPlayer = playerList[num];
			Debug.Log("[PlayfabConnection] OnPlayerLeft Player " + playfabNetworkPlayer.networkId + " (entityKey=" + player.EntityKey.Id + ") left the server");
			connectionHandler.OnPlayerLeave(playfabNetworkPlayer.networkId);
			playerList.RemoveAt(num);
			workshopManager.UpdateActivity(isPublicSession);
		}
	}

	private void OnError(object sender, PlayFabMultiplayerManagerErrorArgs args)
	{
		int code = args.Code;
		if (code != 63 && code != 4163)
		{
			return;
		}
		switch (clientConnectionState)
		{
		case ClientConnectionState.Disconnecting:
		case ClientConnectionState.Connecting:
		case ClientConnectionState.Connected:
			if (clientConnectionState == ClientConnectionState.Connected)
			{
				LeaveNetwork();
			}
			OnNetworkLeft(null, playfabNetworkId);
			NetworkAuxAddPiece.Instance.hud.ShowMessage(LocalisationManager.GetTranslation(4153));
			break;
		case ClientConnectionState.PunchingThroughToServer:
		case ClientConnectionState.HolePunchedFailed:
		case ClientConnectionState.CRCMismatch:
			break;
		}
	}

	private void OnNetworkChanged(object sender, string newNetworkId)
	{
	}

	private void OnNetworkJoined(object sender, string pfNetworkId)
	{
		mpManager.OnNetworkJoined -= OnNetworkJoined;
		mpManager.OnDataMessageReceived += OnDataMessage;
		PlayFabMultiplayerManager playFabMultiplayerManager = mpManager;
		playFabMultiplayerManager.OnNetworkInvalid = (Action)Delegate.Combine(playFabMultiplayerManager.OnNetworkInvalid, new Action(OnInvalidNetworkId));
		mpManager.OnNetworkChanged += OnNetworkChanged;
		PlayerData.onInitReady = (Action<PlayerData, bool>)Delegate.Combine(PlayerData.onInitReady, new Action<PlayerData, bool>(OnPlayerInitReady));
		ReferenceMaster.OnGameStateReceived = (Action)Delegate.Combine(ReferenceMaster.OnGameStateReceived, new Action(OnGameStateComplete));
		networkID = currentPlayerId++;
		playfabNetworkId = pfNetworkId;
		connectionHandler.OnConnected();
		if (restrictComms)
		{
			mpManager.LocalPlayer.IsMuted = true;
		}
		PlayfabNetworkPlayer playfabNetworkPlayer = new PlayfabNetworkPlayer();
		playfabNetworkPlayer.pfPlayer = mpManager.LocalPlayer;
		playfabNetworkPlayer.networkId = networkID;
		PlayfabNetworkPlayer playfabNetworkPlayer2 = playfabNetworkPlayer;
		playerList.Add(playfabNetworkPlayer2);
		if (isHosting)
		{
			SetServerConnectionState(ServerConnectionState.Connected);
			SetPlayerID(networkID, 0uL);
			playfabNetworkPlayer2.networkId = networkID;
			connectionHandler.OnPlayerJoin(networkID);
		}
		else
		{
			StartCoroutine(IECheckEmptyServer());
		}
	}

	private IEnumerator IECheckEmptyServer()
	{
		float timeout = 2f;
		float t = 0f;
		while (mpManager.RemotePlayers.Count == 0 && t < timeout)
		{
			yield return null;
			t += Time.unscaledDeltaTime;
		}
		if (mpManager.RemotePlayers.Count == 0)
		{
			Debug.LogError("[PlayfabConnection] OnNetworkJoined No remote players, quitting!");
			NetworkAuxAddPiece.Instance.hud.ShowMessage(LocalisationManager.GetTranslation(19));
			LeaveNetwork();
		}
	}

	private void OnNetworkLeft(object sender, string networkId)
	{
		Debug.Log("[PlayfabConnection] OnNetworkLeft Successfully left network");
		mpManager.OnNetworkLeft -= OnNetworkLeft;
		playerList.Clear();
		SetClientConnectionState(ClientConnectionState.Disconnected);
		channelTypes.Clear();
		if (isDisposed)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	public override void Dispose()
	{
		if (isInitialized)
		{
			mpManager.OnError -= OnError;
			mpManager.OnNetworkJoined -= OnNetworkJoined;
			mpManager.OnRemotePlayerJoined -= OnPlayerJoin;
			mpManager.OnRemotePlayerLeft -= OnPlayerLeft;
		}
		base.Dispose();
	}

	protected override void SetClientConnectionState(ClientConnectionState newState)
	{
		base.SetClientConnectionState(newState);
		switch (clientConnectionState)
		{
		case ClientConnectionState.Disconnected:
			connectionHandler.OnDisconnected(false);
			break;
		case ClientConnectionState.HostNotFound:
			Disconnect(LocalisationManager.GetTranslation(2010));
			break;
		case ClientConnectionState.DirectConnectFailed:
			ShutdownClient();
			if (connectAddress == Network.player.ipAddress || connectAddress == "127.0.0.1")
			{
				Timeout();
			}
			else
			{
				ConnectToIP(connectAddress, connectPort);
			}
			break;
		case ClientConnectionState.CRCMismatch:
			Disconnect(LocalisationManager.GetTranslation(2028));
			break;
		}
	}

	public override void ShutdownClient()
	{
		Debug.Log("[PlayfabConnection] ShutdownClient state=" + clientConnectionState);
		LeaveNetwork();
	}

	public override void ShutdownServer()
	{
		Debug.Log("[PlayfabConnection] ShutdownServer state=" + clientConnectionState);
		LeaveNetwork();
	}

	private void LeaveNetwork()
	{
		ClientConnectionState clientConnectionState = base.clientConnectionState;
		if (clientConnectionState != ClientConnectionState.Disconnected && clientConnectionState != ClientConnectionState.Disconnecting)
		{
			workshopManager.DeleteActivity();
			StopAllCoroutines();
			Debug.Log("[PlayfabConnection] LeaveNetwork Leaving network..");
			mpManager.OnRemotePlayerJoined -= OnPlayerJoin;
			mpManager.OnDataMessageReceived -= OnDataMessage;
			mpManager.OnRemotePlayerLeft -= OnPlayerLeft;
			mpManager.OnNetworkChanged -= OnNetworkChanged;
			PlayerData.onInitReady = (Action<PlayerData, bool>)Delegate.Remove(PlayerData.onInitReady, new Action<PlayerData, bool>(OnPlayerInitReady));
			ReferenceMaster.OnGameStateReceived = (Action)Delegate.Remove(ReferenceMaster.OnGameStateReceived, new Action(OnGameStateComplete));
			if (isHosting)
			{
				StartCoroutine(IEShutdownServer());
			}
			else
			{
				mpManager.OnNetworkLeft += OnNetworkLeft;
				mpManager.LeaveNetwork();
			}
			if (base.clientConnectionState == ClientConnectionState.Connecting || base.clientConnectionState == ClientConnectionState.PlayfabLogin)
			{
				OnNetworkLeft(null, playfabNetworkId);
			}
			else
			{
				SetClientConnectionState(ClientConnectionState.Disconnecting);
			}
			networkID = (currentPlayerId = 0);
			ping = 0;
			hasHost = false;
			playerList.Clear();
		}
	}

	private IEnumerator IEShutdownServer()
	{
		for (int i = 0; i < playerList.Count; i++)
		{
			if (!playerList[i].pfPlayer.IsLocal && playerList[i].playerData != null)
			{
				DisconnectPlayer(playerList[i].networkId);
			}
		}
		yield return null;
		yield return null;
		yield return null;
		mpManager.OnNetworkLeft += OnNetworkLeft;
		mpManager.LeaveNetwork();
	}

	public override void Stop()
	{
		ShutdownServer();
	}

	private void Timeout()
	{
		if (BesiegeLogFilter.logInfo)
		{
			Debug.Log("[PlayfabConnection] Timeout Client connection timed out...");
		}
		SetClientConnectionState(ClientConnectionState.Disconnected);
		trafficIn = (trafficOut = 0uL);
		Disconnect(LocalisationManager.GetTranslation(2023));
	}

	public override void ConnectSteam(ulong gameserverId)
	{
		Debug.LogError("Connect using a gameserverId is not supported");
	}

	public void Update()
	{
		if (!isHosting && hasHost && clientConnectionState == ClientConnectionState.Connected)
		{
			float time = Time.time;
			if (time > lastAckSent + 0.5f)
			{
				byte[] array = new byte[6] { 69, 1, 0, 0, 0, 0 };
				byte[] bytes = BitConverter.GetBytes(time);
				Buffer.BlockCopy(bytes, 0, array, 2, bytes.Length);
				mpManager.SendDataMessage(array, new PlayFabPlayer[1] { HostPlayer }, DeliveryOption.Guaranteed);
				lastAckSent = time;
			}
		}
	}
}
