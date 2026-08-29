using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Voice;
using Photon.Voice.Unity;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;

public class PhotonService : IConnectionCallbacks, IMatchmakingCallbacks, IOnEventCallback, IInRoomCallbacks
{
	public enum RegionSelector
	{
		Automatic = 0,
		Europe = 1,
		NorthAmerica = 2,
		Japan = 3,
		Asia = 4
	}

	private const string APP_ID_REALTIME = "d58ca953-8ea2-4e8b-82cc-bd054c9c05ce";

	private static readonly string GENERATE_ROOM_ID_URI = "https://escape-simulator-backend-yfw7w.ondigitalocean.app/" + Net.LOBBY_CREATE_PATH + "PHOTON::";

	public Action onLobbyEnterHost;

	public Action<string> onConnectedToMainServer;

	public Action<string, string> onMatchCreateFail;

	public Action<string> onLobbyEnterClient;

	public Action<string> onMatchCreated;

	public Action onLobbyFailEnter;

	public Action<string, string> onLobbyFailEnterDescription;

	public Action<byte[], int, NetPlayerId> onMessage;

	public Action<Speaker, string> onSpeakerAdd;

	public Action<string> onPlayerDataChanged;

	public Action onLobbyChanged;

	public Action<string> onPlayerLeft;

	public Action onLocalPlayerSpeaking;

	public Action onAuthenticationFailed;

	public Action onLobbyPropertyChanged;

	public Action onTimeout;

	private ConnectionHandler connectionHandler;

	public LoadBalancingClient client;

	public PhotonVoiceService voice = new PhotonVoiceService();

	private readonly Net session;

	private float timeToUpdate;

	private string roomIdToCreate;

	private string roomIdToJoin;

	private bool useVoice;

	private readonly object regionRequestLock = new object();

	private bool isRegionRequestResolved;

	private Action regionResolvedCallback;

	private string regionSummary;

	private HAuthTicket steamAuthTicket;

	public string regionToken { get; private set; }

	public PhotonService(Net session)
	{
		this.session = session;
	}

	public void startHost()
	{
		initRealtime();
		initVoice();
		regionResolvedCallback = delegate
		{
			Executor.executeCoroutine(generateRoomIdThenConnect());
		};
		client.AppId = "d58ca953-8ea2-4e8b-82cc-bd054c9c05ce";
		client.ConnectToNameServer();
	}

	private void initVoice()
	{
		if (canUseVoice())
		{
			PhotonInitData photonInitData = getPhotonInitData();
			voice.onLocalPlayerSpeaking = onLocalPlayerSpeaking;
			voice.onSpeakerAdd = onSpeakerAdd;
			voice.initVoice(photonInitData);
		}
	}

	private IEnumerator generateRoomIdThenConnect()
	{
		using UnityWebRequest generateRoomIdRequest = UnityWebRequest.Get(GENERATE_ROOM_ID_URI + regionToken);
		generateRoomIdRequest.downloadHandler = new DownloadHandlerBuffer();
		session.log("'Generate room ID' request sent.");
		yield return generateRoomIdRequest.SendWebRequest();
		session.log("'Generate room ID' response received.");
		if (generateRoomIdRequest.result == UnityWebRequest.Result.Success)
		{
			roomIdToCreate = generateRoomIdRequest.downloadHandler.text;
			session.log("Successfully generated room ID: " + roomIdToCreate);
			onMatchCreated(roomIdToCreate);
			connect();
		}
		else
		{
			onMatchCreateFail(generateRoomIdRequest.responseCode.ToString(), generateRoomIdRequest.error);
		}
	}

	public void startClient(string roomCode, string region)
	{
		initRealtime();
		initVoice();
		roomIdToJoin = roomCode;
		regionToken = region;
		connect();
	}

	private void initRealtime()
	{
		PhotonInitData photonInitData = getPhotonInitData();
		connectionHandler = new GameObject("Connection Handler").AddComponent<ConnectionHandler>();
		client = new LoadBalancingTransport2();
		client.ClientType = ClientAppType.Realtime;
		connectionHandler.Client = client;
		connectionHandler.StartFallbackSendAckThread();
		client.LoadBalancingPeer.DisconnectTimeout = 30000;
		client.LoadBalancingPeer.SentCountAllowance = 20;
		client.AddCallbackTarget(this);
		if (Is.Steam || Is.Oculus || (Is.Switch && !Is.Editor))
		{
			client.AuthValues = photonInitData.auth;
		}
		client.LocalPlayer.NickName = photonInitData.nickname;
		Debug.Log("SetCustomProperties: " + client.LocalPlayer.SetCustomProperties(photonInitData.customProps));
		client.StateChanged += onStateChange;
	}

	private void connect()
	{
		connectRealtime();
		if (canUseVoice())
		{
			Debug.Log("[TEST] CONNECT VOICE");
			voice.connectVoice(roomIdToCreate, roomIdToJoin, regionToken);
		}
	}

	private void connectRealtime()
	{
		if (client.IsConnected)
		{
			client.Disconnect();
		}
		bool flag = client.ConnectUsingSettings(new AppSettings
		{
			AppIdRealtime = "d58ca953-8ea2-4e8b-82cc-bd054c9c05ce",
			FixedRegion = regionToken
		});
		session.log($"Connecting Realtime to '{regionToken}' server [Realtime result: {flag}]...");
	}

	private PhotonInitData getPhotonInitData()
	{
		PhotonInitData photonInitData = new PhotonInitData();
		photonInitData.auth.AuthType = CustomAuthenticationType.Steam;
		byte[] array = new byte[1024];
		SteamNetworkingIdentity pSteamNetworkingIdentity = session.getLocalPlayerSteamIdentity();
		steamAuthTicket = SteamUser.GetAuthSessionTicket(array, array.Length, out var pcbTicket, ref pSteamNetworkingIdentity);
		Array.Resize(ref array, (int)pcbTicket);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < pcbTicket; i++)
		{
			stringBuilder.AppendFormat("{0:x2}", array[i]);
		}
		photonInitData.auth.AddAuthParameter("ticket", stringBuilder.ToString());
		photonInitData.nickname = SteamFriends.GetFriendPersonaName(SteamUser.GetSteamID());
		photonInitData.customProps.Add("PlayerAvatar", session.getSteamAvatar(SteamUser.GetSteamID()).serialize());
		photonInitData.customProps.Add("PlayerPlatform", 1.ToString());
		photonInitData.customProps.Add("IsVrPlayer", session.isVR.ToString());
		photonInitData.customProps.Add("IsOculusPlayer", Is.Oculus.ToString());
		photonInitData.customProps.Add("IsNintendoSwitchPlayer", Is.Switch.ToString());
		photonInitData.customProps.Add("IsSteamPlayer", Is.Steam.ToString());
		photonInitData.customProps.Add("IsDemoPlayer", Is.Demo.ToString());
		return photonInitData;
	}

	public void updatePhoton()
	{
		timeToUpdate -= Time.deltaTime;
		if (!(timeToUpdate > 0f))
		{
			voice?.processLocalVoiceRecorder();
			processRegionRequest();
			client?.Service();
			timeToUpdate = 0.02f;
		}
	}

	private void processRegionRequest()
	{
		lock (regionRequestLock)
		{
			if (isRegionRequestResolved)
			{
				PlayerSave.getSettings().regionSummary = regionSummary;
				PlayerSave.flush();
				regionResolvedCallback?.Invoke();
				regionResolvedCallback = null;
				isRegionRequestResolved = false;
			}
		}
	}

	public bool sendData(byte[] data, NetPlayerId receiver, bool reliable)
	{
		if (!client.InRoom)
		{
			session.log("Can't call 'sendData' because you are not in a room!");
			return false;
		}
		RaiseEventOptions raiseEventOptions = new RaiseEventOptions();
		int num = 0;
		foreach (KeyValuePair<int, Player> player in client.CurrentRoom.Players)
		{
			if (!(player.Value.UserId != receiver.value))
			{
				num = player.Value.ActorNumber;
				break;
			}
		}
		raiseEventOptions.TargetActors = new int[1] { num };
		return client.OpRaiseEvent(0, data, raiseEventOptions, reliable ? SendOptions.SendReliable : SendOptions.SendUnreliable);
	}

	public void destroy()
	{
		session.log("photon destroy");
		if (voice != null)
		{
			voice.destroy();
		}
		if (connectionHandler != null)
		{
			UnityEngine.Object.Destroy(connectionHandler.gameObject);
		}
		if (client != null)
		{
			client.Disconnect();
			client.RemoveCallbackTarget(this);
		}
	}

	private void onStateChange(ClientState arg1, ClientState arg2)
	{
		session.log("Photon stateChange: " + arg1.ToString() + " -> " + arg2);
	}

	private bool canUseVoice()
	{
		if (Is.Oculus)
		{
			return VR.oculusHasMicrophonePermission;
		}
		return true;
	}

	public void OnConnectedToMaster()
	{
		session.log("Photon OnConnectedToMaster Server: " + client.LoadBalancingPeer.ServerIpAddress);
		onConnectedToMainServer(client.LocalPlayer.UserId);
		if (steamAuthTicket != HAuthTicket.Invalid)
		{
			SteamUser.CancelAuthTicket(steamAuthTicket);
		}
		if (roomIdToCreate != null)
		{
			EnterRoomParams enterRoomParams = new EnterRoomParams
			{
				RoomName = roomIdToCreate
			};
			RoomOptions roomOptions = new RoomOptions
			{
				MaxPlayers = 8,
				CustomRoomProperties = new ExitGames.Client.Photon.Hashtable
				{
					{
						"HostPlayerId",
						client.LocalPlayer.UserId
					},
					{
						"GameVersion",
						Version.baseGameVersion
					},
					{
						"ConnectionMode",
						5.ToString()
					},
					{
						"GameDifficulty",
						session.gameDifficulty.ToString()
					}
				},
				PublishUserId = true
			};
			enterRoomParams.RoomOptions = roomOptions;
			client.OpCreateRoom(enterRoomParams);
			roomIdToCreate = null;
		}
		if (roomIdToJoin != null)
		{
			client.OpJoinRoom(new EnterRoomParams
			{
				RoomName = roomIdToJoin
			});
		}
	}

	public void OnRegionListReceived(RegionHandler regionHandler)
	{
		session.log("PhotonService: OnRegionListReceived");
		string previousSummary = PlayerSave.getSettings().regionSummary;
		regionHandler.PingMinimumOfRegions(delegate(RegionHandler handler)
		{
			lock (regionRequestLock)
			{
				regionToken = handler.BestRegion.Code;
				regionSummary = handler.SummaryToCache;
				isRegionRequestResolved = true;
			}
		}, previousSummary);
	}

	public void OnCreateRoomFailed(short returnCode, string message)
	{
		session.log("Photon OnCreateRoomFailed " + returnCode + " " + message);
		if (returnCode == 32758)
		{
			onLobbyFailEnter();
		}
		else
		{
			onLobbyFailEnterDescription(returnCode.ToString(), message);
		}
	}

	public void OnJoinedRoom()
	{
		session.log("Photon OnJoinedRoom");
		if (session.netMode == NetMode.Client)
		{
			string text = client.CurrentRoom.CustomProperties["GameVersion"].ToString();
			session.gameDifficulty = UnityUtils.tryParseEnum<GameDifficulty>(client.CurrentRoom.CustomProperties["GameDifficulty"].ToString());
			if ((!string.IsNullOrEmpty(text) && Version.isBackwardsCompatibleWithVersion(text)) || Debug.isDebugBuild)
			{
				string obj = client.CurrentRoom.CustomProperties["HostPlayerId"].ToString();
				onLobbyEnterClient(obj);
			}
			else
			{
				onLobbyFailEnter();
			}
		}
		else
		{
			onLobbyEnterHost();
		}
	}

	public void OnJoinRoomFailed(short returnCode, string message)
	{
		session.log("Photon OnJoinRoomFailed " + returnCode + " " + message);
		if (returnCode == 32758)
		{
			onLobbyFailEnter();
		}
		else
		{
			onLobbyFailEnterDescription(returnCode.ToString(), message);
		}
	}

	public void OnEvent(EventData photonEvent)
	{
		if (photonEvent.Code == 0)
		{
			ByteArraySlice byteArraySlice = (ByteArraySlice)photonEvent.CustomData;
			NetPlayerId arg = new NetPlayerId(client.CurrentRoom.GetPlayer(photonEvent.Sender).UserId);
			onMessage(byteArraySlice.Buffer, byteArraySlice.Count, arg);
		}
	}

	public void OnPlayerEnteredRoom(Player newPlayer)
	{
		onLobbyChanged();
		session.log("Photon OnPlayerEnteredRoom " + newPlayer);
	}

	public void OnPlayerLeftRoom(Player otherPlayer)
	{
		onLobbyChanged();
		onPlayerLeft(otherPlayer.UserId);
		session.log("Photon OnPlayerLeftRoom " + otherPlayer);
	}

	public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
	{
		onLobbyChanged();
		onPlayerDataChanged(targetPlayer.UserId);
	}

	public static string convertSelectorToToken(RegionSelector selector)
	{
		return selector switch
		{
			RegionSelector.Europe => "eu", 
			RegionSelector.NorthAmerica => "us", 
			RegionSelector.Japan => "jp", 
			RegionSelector.Asia => "asia", 
			_ => string.Empty, 
		};
	}

	public void OnConnected()
	{
		session.log("PhotonService: OnConnected");
	}

	public void OnCustomAuthenticationFailed(string debugMessage)
	{
		onAuthenticationFailed();
		session.log("PhotonService: OnCustomAuthenticationFailed - " + debugMessage);
	}

	public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		session.log(string.Format("{0}: {1}, {2}", "PhotonService", "OnRoomPropertiesUpdate", propertiesThatChanged));
		session.gameDifficulty = UnityUtils.tryParseEnum<GameDifficulty>(client.CurrentRoom.CustomProperties["GameDifficulty"].ToString());
		if (propertiesThatChanged.Count > 0)
		{
			onLobbyPropertyChanged();
		}
	}

	public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
	{
		session.log("PhotonService: OnCustomAuthenticationResponse");
	}

	public void OnDisconnected(DisconnectCause cause)
	{
		session.log(string.Format("{0}: {1}, {2}", "PhotonService", "OnDisconnected", cause));
		if (cause == DisconnectCause.ServerTimeout || cause == DisconnectCause.ClientTimeout)
		{
			onTimeout?.Invoke();
		}
	}

	public void OnFriendListUpdate(List<FriendInfo> friendList)
	{
		session.log("PhotonService: OnFriendListUpdate");
	}

	public void OnCreatedRoom()
	{
		session.log("PhotonService: OnCreatedRoom");
	}

	public void OnJoinRandomFailed(short returnCode, string message)
	{
		session.log("PhotonService: OnJoinRandomFailed");
	}

	public void OnLeftRoom()
	{
		session.log("PhotonService: OnLeftRoom");
	}

	public void OnMasterClientSwitched(Player newMasterClient)
	{
		session.log(string.Format("{0}: {1}, {2}", "PhotonService", "OnMasterClientSwitched", newMasterClient));
	}

	public static RegionSelector convertTokenToSelector(string token)
	{
		return token switch
		{
			"eu" => RegionSelector.Europe, 
			"us" => RegionSelector.NorthAmerica, 
			"jp" => RegionSelector.Japan, 
			"asia" => RegionSelector.Asia, 
			_ => RegionSelector.Automatic, 
		};
	}
}
