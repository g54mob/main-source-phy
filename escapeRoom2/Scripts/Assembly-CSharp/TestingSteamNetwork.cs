using Steamworks;
using UnityEngine;

public class TestingSteamNetwork : MonoBehaviour
{
	private enum SteamState
	{
		NotInited = 0,
		InitFailed = 1,
		InitSuccess = 2
	}

	private Callback<SteamRelayNetworkStatus_t> steamRelayNetworkStatusCallback;

	private Callback<SteamNetConnectionStatusChangedCallback_t> steamNetConnectionStatusChangedCallback;

	private bool useSteam;

	private SteamState steamState;

	private HSteamListenSocket listenSocket;

	private HSteamNetConnection connection;

	private void Awake()
	{
		steamState = ((!SteamAPI.Init()) ? SteamState.InitFailed : SteamState.InitSuccess);
		useSteam = steamState == SteamState.InitSuccess;
		Debug.Log($"SteamAPI.Init - {steamState}");
		if (useSteam)
		{
			steamRelayNetworkStatusCallback = Callback<SteamRelayNetworkStatus_t>.Create(onSteamRelayNetworkStatus);
			steamNetConnectionStatusChangedCallback = Callback<SteamNetConnectionStatusChangedCallback_t>.Create(onSteamNetConnectionStatusChanged);
			SteamNetworkingUtils.InitRelayNetworkAccess();
			Debug.Log("SteamNetworkingUtils.InitRelayNetworkAccess");
		}
	}

	private void onSteamRelayNetworkStatus(SteamRelayNetworkStatus_t data)
	{
		Debug.Log($"onSteamRelayNetworkStatus - m_eAvail: {data.m_eAvail}, m_bPingMeasurementInProgress: {data.m_bPingMeasurementInProgress}, m_eAvailNetworkConfig: {data.m_eAvailNetworkConfig}, m_eAvailAnyRelay: {data.m_eAvailAnyRelay}, m_debugMsg: {data.m_debugMsg}");
	}

	private void onSteamNetConnectionStatusChanged(SteamNetConnectionStatusChangedCallback_t data)
	{
		Debug.Log($"onSteamNetConnectionStatusChanged - oldState: {data.m_eOldState}, newState: {data.m_info.m_eState}");
	}

	private void Start()
	{
		if (useSteam)
		{
			listenSocket = SteamNetworkingSockets.CreateListenSocketP2P(0, 0, null);
		}
	}

	private void connect()
	{
		_ = useSteam;
	}

	private void Update()
	{
		if (useSteam)
		{
			SteamAPI.RunCallbacks();
		}
	}

	private void OnDestroy()
	{
		if (useSteam)
		{
			steamNetConnectionStatusChangedCallback.Dispose();
			steamRelayNetworkStatusCallback.Dispose();
			SteamAPI.Shutdown();
			Debug.Log("SteamAPI.Shutdown");
		}
	}
}
