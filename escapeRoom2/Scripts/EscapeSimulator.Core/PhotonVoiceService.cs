using System;
using System.Collections.Generic;
using FMODUnity;
using Photon.Realtime;
using Photon.Voice;
using Photon.Voice.Unity;
using UnityEngine;

public class PhotonVoiceService : IMatchmakingCallbacks, IConnectionCallbacks
{
	public Action<Speaker, string> onSpeakerAdd;

	public Action onLocalPlayerSpeaking;

	private const string APP_ID_VOICE = "8e31ae4e-b26c-40b1-ab9e-d8891aec96be";

	public Recorder voiceRecorder;

	public VoiceConnection voiceConnection;

	public LoadBalancingClient client;

	private string roomIdToCreate;

	private string roomIdToJoin;

	public void OnCreatedRoom()
	{
	}

	public void OnCreateRoomFailed(short returnCode, string message)
	{
	}

	public void OnFriendListUpdate(List<FriendInfo> friendList)
	{
	}

	public void OnJoinedRoom()
	{
		voiceRecorder = new GameObject("Photon Recorder").AddComponent<Recorder>();
		voiceRecorder.MicrophoneType = Recorder.MicType.Photon;
		Debug.Log("voiceRecorder.ReactOnSystemChanges " + voiceRecorder.ReactOnSystemChanges);
		voiceRecorder.ReactOnSystemChanges = true;
		voiceConnection.PrimaryRecorder = voiceRecorder;
		UnityEngine.Object.DontDestroyOnLoad(voiceRecorder);
	}

	public void OnJoinRandomFailed(short returnCode, string message)
	{
	}

	public void OnJoinRoomFailed(short returnCode, string message)
	{
	}

	public void OnLeftRoom()
	{
	}

	public void OnConnected()
	{
	}

	public void OnConnectedToMaster()
	{
		Debug.Log("Photon Voice OnConnectedToMaster Server: " + client.LoadBalancingPeer.ServerIpAddress);
		if (roomIdToCreate != null)
		{
			EnterRoomParams enterRoomParams = new EnterRoomParams
			{
				RoomName = roomIdToCreate + "_Voice"
			};
			RoomOptions roomOptions = new RoomOptions
			{
				MaxPlayers = 8,
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
				RoomName = roomIdToJoin + "_Voice"
			});
		}
	}

	public void OnDisconnected(DisconnectCause cause)
	{
	}

	public void OnRegionListReceived(RegionHandler regionHandler)
	{
	}

	public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
	{
	}

	public void OnCustomAuthenticationFailed(string debugMessage)
	{
	}

	public void initVoice(PhotonInitData initData)
	{
		voiceConnection = new GameObject("Photon Voice").AddComponent<VoiceConnection>();
		voiceConnection.AutoCreateSpeakerIfNotFound = true;
		client = voiceConnection.Client;
		if (Is.Steam || Is.Oculus || (Is.Switch && !Is.Editor))
		{
			client.AuthValues = initData.auth;
		}
		client.LoadBalancingPeer.DisconnectTimeout = 30000;
		client.LoadBalancingPeer.SentCountAllowance = 20;
		client.AddCallbackTarget(this);
		voiceConnection.SpeakerFactory = delegate(int playerIndex, byte _, object _)
		{
			string userId = client.CurrentRoom.GetPlayer(playerIndex).UserId;
			Speaker speaker = new GameObject("Photon Voice Speaker for: " + userId).AddComponent<Speaker>();
			AudioOutDelayControl.PlayDelayConfig pdc = new AudioOutDelayControl.PlayDelayConfig
			{
				Low = 200,
				High = 400,
				Max = 1000
			};
			speaker.CustomAudioOutFactory = () => new PineFmodAudioOut<float>(RuntimeManager.CoreSystem, pdc, new Photon.Voice.Unity.Logger(), "ES_PhotonVoice", debugInfo: true, isSteamVoice: false);
			UnityEngine.Object.DontDestroyOnLoad(speaker);
			onSpeakerAdd(speaker, userId);
			return speaker;
		};
	}

	public void connectVoice(string lobbyCodeToCreate, string lobbyCodeToJoin, string region)
	{
		roomIdToCreate = lobbyCodeToCreate;
		roomIdToJoin = lobbyCodeToJoin;
		if (client.IsConnected)
		{
			client.Disconnect();
		}
		bool flag = voiceConnection.ConnectUsingSettings(new AppSettings
		{
			AppIdVoice = "8e31ae4e-b26c-40b1-ab9e-d8891aec96be",
			FixedRegion = region
		});
		Debug.Log($"Connecting Voice to '{region}' server [Voice result: {flag}]...");
	}

	public void processLocalVoiceRecorder()
	{
		if (!(voiceConnection == null) && !(voiceConnection.PrimaryRecorder == null) && voiceConnection.PrimaryRecorder.IsCurrentlyTransmitting)
		{
			onLocalPlayerSpeaking();
			client.Service();
		}
	}

	public void destroy()
	{
		if (voiceConnection != null)
		{
			UnityEngine.Object.Destroy(voiceConnection.gameObject);
		}
		if (voiceRecorder != null)
		{
			UnityEngine.Object.Destroy(voiceRecorder.gameObject);
		}
		client?.Disconnect();
		client?.RemoveCallbackTarget(this);
	}
}
