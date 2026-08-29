using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace ExitGames.Demos.DemoPunVoice
{
	public class VoiceDemoUI : MonoBehaviour
	{
		public delegate void OnDebugToggle(bool debugMode);

		[SerializeField]
		private Text punState;

		[SerializeField]
		private Text voiceState;

		private PhotonVoiceNetwork punVoiceNetwork;

		private Canvas canvas;

		[SerializeField]
		private Button punSwitch;

		private Text punSwitchText;

		[SerializeField]
		private Button voiceSwitch;

		private Text voiceSwitchText;

		[SerializeField]
		private Button calibrateButton;

		private Text calibrateText;

		[SerializeField]
		private Text voiceDebugText;

		public Recorder recorder;

		[SerializeField]
		private GameObject inGameSettings;

		[SerializeField]
		private GameObject globalSettings;

		[SerializeField]
		private Text devicesInfoText;

		private GameObject debugGO;

		private bool debugMode;

		private float volumeBeforeMute;

		private DebugLevel previousDebugLevel;

		[SerializeField]
		private int calibrationMilliSeconds = 2000;

		public bool DebugMode
		{
			get
			{
				return debugMode;
			}
			set
			{
				debugMode = value;
				debugGO.SetActive(debugMode);
				voiceDebugText.text = string.Empty;
				if (debugMode)
				{
					previousDebugLevel = punVoiceNetwork.Client.LoadBalancingPeer.DebugOut;
					punVoiceNetwork.Client.LoadBalancingPeer.DebugOut = DebugLevel.ALL;
				}
				else
				{
					punVoiceNetwork.Client.LoadBalancingPeer.DebugOut = previousDebugLevel;
				}
				if (VoiceDemoUI.DebugToggled != null)
				{
					VoiceDemoUI.DebugToggled(debugMode);
				}
			}
		}

		public static event OnDebugToggle DebugToggled;

		private void Awake()
		{
			punVoiceNetwork = PhotonVoiceNetwork.Instance;
		}

		private void OnEnable()
		{
			ChangePOV.CameraChanged += OnCameraChanged;
			BetterToggle.ToggleValueChanged += BetterToggle_ToggleValueChanged;
			CharacterInstantiation.CharacterInstantiated += CharacterInstantiation_CharacterInstantiated;
			punVoiceNetwork.Client.StateChanged += VoiceClientStateChanged;
			PhotonNetwork.NetworkingClient.StateChanged += PunClientStateChanged;
		}

		private void OnDisable()
		{
			ChangePOV.CameraChanged -= OnCameraChanged;
			BetterToggle.ToggleValueChanged -= BetterToggle_ToggleValueChanged;
			CharacterInstantiation.CharacterInstantiated -= CharacterInstantiation_CharacterInstantiated;
			punVoiceNetwork.Client.StateChanged -= VoiceClientStateChanged;
			PhotonNetwork.NetworkingClient.StateChanged -= PunClientStateChanged;
		}

		private void CharacterInstantiation_CharacterInstantiated(GameObject character)
		{
			if (!recorder)
			{
				PhotonVoiceView component = character.GetComponent<PhotonVoiceView>();
				if (component.IsRecorder)
				{
					recorder = component.RecorderInUse;
				}
			}
		}

		private void InitToggles(Toggle[] toggles)
		{
			if (toggles == null)
			{
				return;
			}
			foreach (Toggle toggle in toggles)
			{
				switch (toggle.name)
				{
				case "Mute":
					toggle.isOn = AudioListener.volume <= 0.001f;
					break;
				case "VoiceDetection":
					toggle.isOn = recorder != null && recorder.VoiceDetection;
					break;
				case "DebugVoice":
					toggle.isOn = DebugMode;
					break;
				case "Transmit":
					toggle.isOn = recorder != null && recorder.TransmitEnabled;
					break;
				case "DebugEcho":
					toggle.isOn = recorder != null && recorder.DebugEchoMode;
					break;
				case "AutoConnectAndJoin":
					toggle.isOn = punVoiceNetwork.AutoConnectAndJoin;
					break;
				case "AutoLeaveAndDisconnect":
					toggle.isOn = punVoiceNetwork.AutoLeaveAndDisconnect;
					break;
				}
			}
		}

		private void BetterToggle_ToggleValueChanged(Toggle toggle)
		{
			switch (toggle.name)
			{
			case "Mute":
				if (toggle.isOn)
				{
					volumeBeforeMute = AudioListener.volume;
					AudioListener.volume = 0f;
				}
				else
				{
					AudioListener.volume = volumeBeforeMute;
					volumeBeforeMute = 0f;
				}
				break;
			case "Transmit":
				if ((bool)recorder)
				{
					recorder.TransmitEnabled = toggle.isOn;
				}
				break;
			case "VoiceDetection":
				if ((bool)recorder)
				{
					recorder.VoiceDetection = toggle.isOn;
				}
				break;
			case "DebugEcho":
				if ((bool)recorder)
				{
					recorder.DebugEchoMode = toggle.isOn;
				}
				break;
			case "DebugVoice":
				DebugMode = toggle.isOn;
				break;
			case "AutoConnectAndJoin":
				punVoiceNetwork.AutoConnectAndJoin = toggle.isOn;
				break;
			case "AutoLeaveAndDisconnect":
				punVoiceNetwork.AutoLeaveAndDisconnect = toggle.isOn;
				break;
			}
		}

		private void OnCameraChanged(Camera newCamera)
		{
			canvas.worldCamera = newCamera;
		}

		private void Start()
		{
			canvas = GetComponentInChildren<Canvas>();
			if (punSwitch != null)
			{
				punSwitchText = punSwitch.GetComponentInChildren<Text>();
				punSwitch.onClick.AddListener(PunSwitchOnClick);
			}
			if (voiceSwitch != null)
			{
				voiceSwitchText = voiceSwitch.GetComponentInChildren<Text>();
				voiceSwitch.onClick.AddListener(VoiceSwitchOnClick);
			}
			if (calibrateButton != null)
			{
				calibrateButton.onClick.AddListener(CalibrateButtonOnClick);
				calibrateText = calibrateButton.GetComponentInChildren<Text>();
			}
			if (punState != null)
			{
				debugGO = punState.transform.parent.gameObject;
			}
			volumeBeforeMute = AudioListener.volume;
			previousDebugLevel = punVoiceNetwork.Client.LoadBalancingPeer.DebugOut;
			if (globalSettings != null)
			{
				globalSettings.SetActive(value: true);
				InitToggles(globalSettings.GetComponentsInChildren<Toggle>());
			}
			if (!(devicesInfoText != null))
			{
				return;
			}
			if (UnityMicrophone.devices == null || UnityMicrophone.devices.Length == 0)
			{
				devicesInfoText.enabled = true;
				devicesInfoText.color = Color.red;
				devicesInfoText.text = "No microphone device detected!";
				return;
			}
			if (UnityMicrophone.devices.Length == 1)
			{
				devicesInfoText.text = $"Mic.: {UnityMicrophone.devices[0]}";
				return;
			}
			devicesInfoText.text = $"Multi.Mic.Devices:\n0. {UnityMicrophone.devices[0]} (active)\n";
			for (int i = 1; i < UnityMicrophone.devices.Length; i++)
			{
				devicesInfoText.text += $"{i}. {UnityMicrophone.devices[i]}\n";
			}
		}

		private void PunSwitchOnClick()
		{
			if (PhotonNetwork.NetworkClientState == ClientState.Joined)
			{
				PhotonNetwork.Disconnect();
			}
			else if (PhotonNetwork.NetworkClientState == ClientState.Disconnected || PhotonNetwork.NetworkClientState == ClientState.PeerCreated)
			{
				PhotonNetwork.ConnectUsingSettings();
			}
		}

		private void VoiceSwitchOnClick()
		{
			if (punVoiceNetwork.ClientState == ClientState.Joined)
			{
				punVoiceNetwork.Disconnect();
			}
			else if (punVoiceNetwork.ClientState == ClientState.PeerCreated || punVoiceNetwork.ClientState == ClientState.Disconnected)
			{
				punVoiceNetwork.ConnectAndJoinRoom();
			}
		}

		private void CalibrateButtonOnClick()
		{
			if ((bool)recorder && !recorder.VoiceDetectorCalibrating)
			{
				recorder.VoiceDetectorCalibrate(calibrationMilliSeconds);
			}
		}

		private void Update()
		{
			if (recorder != null && recorder.LevelMeter != null)
			{
				voiceDebugText.text = $"Amp: avg. {recorder.LevelMeter.CurrentAvgAmp:0.000000}, peak {recorder.LevelMeter.CurrentPeakAmp:0.000000}";
			}
		}

		private void PunClientStateChanged(ClientState fromState, ClientState toState)
		{
			punState.text = $"PUN: {toState}";
			switch (toState)
			{
			case ClientState.PeerCreated:
			case ClientState.Disconnected:
				punSwitch.interactable = true;
				punSwitchText.text = "PUN Connect";
				break;
			case ClientState.Joined:
				punSwitch.interactable = true;
				punSwitchText.text = "PUN Disconnect";
				break;
			default:
				punSwitch.interactable = false;
				punSwitchText.text = "PUN busy";
				break;
			}
			UpdateUiBasedOnVoiceState(punVoiceNetwork.ClientState);
		}

		private void VoiceClientStateChanged(ClientState fromState, ClientState toState)
		{
			UpdateUiBasedOnVoiceState(toState);
		}

		private void UpdateUiBasedOnVoiceState(ClientState voiceClientState)
		{
			voiceState.text = $"PhotonVoice: {voiceClientState}";
			switch (voiceClientState)
			{
			case ClientState.Joined:
				voiceSwitch.interactable = true;
				inGameSettings.SetActive(value: true);
				voiceSwitchText.text = "Voice Disconnect";
				InitToggles(inGameSettings.GetComponentsInChildren<Toggle>());
				if (recorder != null)
				{
					calibrateButton.interactable = !recorder.VoiceDetectorCalibrating;
					calibrateText.text = (recorder.VoiceDetectorCalibrating ? "Calibrating" : $"Calibrate ({calibrationMilliSeconds / 1000}s)");
				}
				else
				{
					calibrateButton.interactable = false;
					calibrateText.text = "Unavailable";
				}
				break;
			case ClientState.PeerCreated:
			case ClientState.Disconnected:
				if (PhotonNetwork.InRoom)
				{
					voiceSwitch.interactable = true;
					voiceSwitchText.text = "Voice Connect";
					voiceDebugText.text = string.Empty;
				}
				else
				{
					voiceSwitch.interactable = false;
					voiceSwitchText.text = "Voice N/A";
					voiceDebugText.text = string.Empty;
				}
				calibrateButton.interactable = false;
				voiceSwitchText.text = "Voice Connect";
				calibrateText.text = "Unavailable";
				inGameSettings.SetActive(value: false);
				break;
			default:
				voiceSwitch.interactable = false;
				voiceSwitchText.text = "Voice busy";
				break;
			}
		}
	}
}
