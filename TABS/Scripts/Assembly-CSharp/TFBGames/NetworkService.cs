using System;
using System.Collections.Generic;
using System.Text;
using Landfall.TABS;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;
using TFBG;
using UdpKit;
using UdpKit.Platform;
using UdpKit.Platform.Photon;
using UnityEngine;

namespace TFBGames
{
	public class NetworkService : GlobalEventListener, INetworkService, IService
	{
		public enum State
		{
			Idle = 0,
			CreatingSession = 1,
			JoiningSession = 2,
			JoiningRandomSession = 3,
			GettingSessions = 4,
			ShuttingDown = 5,
			Cancelling = 6,
			AuthenticatingUserToken = 7
		}

		private class CreateSessionInfo
		{
			public CreateSessionCallback Callback;

			public CreateSessionProperties Properties;
		}

		private class JoinSessionInfo
		{
			public JoinSessionCallback Callback;

			public JoinSessionProperties Properties;
		}

		private class JoinRandomSessionInfo
		{
			public JoinSessionCallback Callback;
		}

		private class GetSessionsInfo
		{
			public GetSessionsCallback Callback;

			public float? Timeout;
		}

		private class ShutdownInfo
		{
			public ShutDownCallback Callback;
		}

		private class CancelInfo
		{
			public ShutDownCallback ShutdownCallback;

			public State State;

			public float NotRunningTime;
		}

		private class AuthenticateUserTokenInfo
		{
			public AuthenticateUserTokenCallback Callback;

			public string RegionCode;
		}

		private const string RegionKey = "Region";

		private GameDisruptionService disruptionService;

		private float GetSessionsTimeout = 10f;

		private float CancellingTimeout = 5f;

		private float StateTimeout = 30f;

		private State state;

		private float stateStartTime;

		private readonly CreateSessionInfo createSession = new CreateSessionInfo();

		private readonly JoinSessionInfo joinSession = new JoinSessionInfo();

		private readonly JoinRandomSessionInfo joinRandomSession = new JoinRandomSessionInfo();

		private readonly GetSessionsInfo getSessions = new GetSessionsInfo();

		private readonly ShutdownInfo shutdown = new ShutdownInfo();

		private readonly CancelInfo cancel = new CancelInfo();

		private readonly AuthenticateUserTokenInfo authenticateUser = new AuthenticateUserTokenInfo();

		private readonly List<NetworkSession> cachedSessions = new List<NetworkSession>();

		private bool boltIsShuttingDown;

		private NetworkSession currentSession;

		private PhotonRegion selectedRegion;

		private ProjectMarsGameServiceAsset projectMarsGameServiceAsset;

		private ProjectMarsGameSettingsAsset projectMarsGameSettingsAsset;

		private FixedTimeStepService fixedTimeStepService;

		protected char SessionInfoArgumentSeparator = '|';

		protected int SessionInfoArgumentsCount = 2;

		public bool IsRunning => BoltNetwork.IsRunning;

		public bool IsServer => BoltNetwork.IsServer;

		public bool IsClient => BoltNetwork.IsClient;

		public bool IsConnected => BoltNetwork.IsConnected;

		public Team PlayerTeam
		{
			get
			{
				if (!BoltNetwork.IsServer)
				{
					return Team.Blue;
				}
				return Team.Red;
			}
		}

		public Team RemotePlayerTeam
		{
			get
			{
				if (!BoltNetwork.IsServer)
				{
					return Team.Red;
				}
				return Team.Blue;
			}
		}

		public string CurrentSessionId
		{
			get
			{
				if (BoltMatchmaking.CurrentSession != null && BoltMatchmaking.CurrentSession is PhotonSession && IsRunning)
				{
					return BoltMatchmaking.CurrentSession.HostName;
				}
				return null;
			}
		}

		public string RegionCode { get; private set; }

		public event Action CreatedSession;

		public event Action JoinedSession;

		public event Action LeftSession;

		public void LoadProjectMarsSettings()
		{
			projectMarsGameServiceAsset = ServiceLocator.GetService<ProjectMarsGameServiceAsset>();
			projectMarsGameSettingsAsset = ServiceLocator.GetService<ProjectMarsGameSettingsAsset>();
			GetSessionsTimeout = projectMarsGameServiceAsset.GetSessionsTimeOut;
			CancellingTimeout = projectMarsGameServiceAsset.CancellingTimeOut;
			StateTimeout = projectMarsGameServiceAsset.StateTimeOut;
		}

		public void SetUserAuthenticationData(string data)
		{
		}

		public NetworkSession GetCurrentSession()
		{
			if (!IsRunning || !(BoltMatchmaking.CurrentSession is PhotonSession))
			{
				return null;
			}
			if (currentSession == null || currentSession.Id != BoltMatchmaking.CurrentSession.HostName)
			{
				currentSession = NetworkSessionHelper.ConvertSession(BoltMatchmaking.CurrentSession);
			}
			return currentSession;
		}

		public virtual void CreateSessionAsync(CreateSessionProperties properties, CreateSessionCallback callback)
		{
			if (state != State.Idle)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.ServiceIsBusyWithAsync));
				return;
			}
			if (IsRunning && !IsServer)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunning));
				return;
			}
			createSession.Properties = properties;
			createSession.Callback = callback;
			SetState(State.CreatingSession);
		}

		public virtual void JoinSessionAsync(bool isQuickGame, JoinSessionProperties properties, JoinSessionCallback callback)
		{
			if (state != State.Idle)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.ServiceIsBusyWithAsync));
				return;
			}
			if (IsRunning && !IsClient)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunning));
				return;
			}
			if (IsRunning && IsClient && !IsConnectedToRegion(selectedRegion, RegionCode, properties.RegionCode))
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunningInWrongRegion));
				return;
			}
			joinSession.Properties = properties;
			joinSession.Callback = callback;
			SetState(State.JoiningSession);
		}

		public virtual void JoinRandomSessionAsync(NetworkSessionFilter filter, JoinSessionCallback callback)
		{
			if (state != State.Idle)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.ServiceIsBusyWithAsync));
				return;
			}
			if (IsRunning && !IsClient)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunning));
				return;
			}
			if (IsRunning && IsClient && !IsConnectedToRegion(selectedRegion, RegionCode, null))
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunningInWrongRegion));
				return;
			}
			joinRandomSession.Callback = callback;
			SetState(State.JoiningRandomSession);
		}

		public void GetSessionsAsync(GetSessionsCallback callback)
		{
			if (state != State.Idle)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.ServiceIsBusyWithAsync));
				return;
			}
			if (IsRunning && IsClient && !IsConnectedToRegion(selectedRegion, RegionCode, null))
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunningInWrongRegion));
				return;
			}
			if (IsRunning && IsClient)
			{
				callback?.Invoke(cachedSessions.ToArray(), null);
				return;
			}
			if (IsRunning)
			{
				callback?.Invoke(null, new NetworkException(NetworkErrorCode.SystemIsRunning));
				return;
			}
			getSessions.Callback = callback;
			getSessions.Timeout = null;
			SetState(State.GettingSessions);
		}

		public virtual void ShutdownAsync(ShutDownCallback callback)
		{
			if (state != State.Idle)
			{
				if (state == State.ShuttingDown || state == State.Cancelling)
				{
					callback?.Invoke(new NetworkException(NetworkErrorCode.ServiceIsBusyWithAsync));
					return;
				}
				cancel.ShutdownCallback = callback;
				cancel.State = state;
				cancel.NotRunningTime = 0f;
				SetState(State.Cancelling);
			}
			else if (!IsRunning)
			{
				callback?.Invoke(null);
			}
			else
			{
				shutdown.Callback = callback;
				SetState(State.ShuttingDown);
			}
		}

		public JoinSessionProperties GetJoinSessionPropertiesFromDataBuffer(byte[] data)
		{
			GetSessionInfoFromDataBuffer(data, out var sessionId, out var regionCode);
			return new JoinSessionProperties(sessionId, regionCode);
		}

		public byte[] CreateJoinSessionPropertiesAsDataBuffer()
		{
			return GetDataBufferFromSessionInfo(CurrentSessionId, RegionCode);
		}

		public void GetSessionInfoFromDataBuffer(byte[] data, out string sessionId, out string regionCode)
		{
			string text = Encoding.UTF8.GetString(data);
			string[] array = text.Split(SessionInfoArgumentSeparator);
			if (array.Length != SessionInfoArgumentsCount)
			{
				throw new ArgumentException("Data buffer doesn't contain the correct number of session info arguments. String: " + text);
			}
			sessionId = array[0];
			regionCode = array[1];
		}

		public byte[] GetDataBufferFromSessionInfo(string sessionId, string regionCode)
		{
			string s = $"{sessionId}{SessionInfoArgumentSeparator}{regionCode}";
			return Encoding.UTF8.GetBytes(s);
		}

		public bool SendPleaseStayConnectedEvent()
		{
			if (!IsRunning || !IsConnected)
			{
				return false;
			}
			PleaseStayConnectedEvent pleaseStayConnectedEvent = PleaseStayConnectedEvent.Create(GlobalTargets.Others, ReliabilityModes.ReliableOrdered);
			if (pleaseStayConnectedEvent == null)
			{
				return false;
			}
			pleaseStayConnectedEvent.Send();
			return true;
		}

		public int GetConnectionsCount()
		{
			int num = 0;
			if (IsRunning && IsConnected && BoltNetwork.Connections != null)
			{
				foreach (BoltConnection connection in BoltNetwork.Connections)
				{
					_ = connection;
					num++;
				}
			}
			return num;
		}

		public void AuthenticateUserTokenAsync(string token, string regionCode, AuthenticateUserTokenCallback callback)
		{
			if (state != State.Idle)
			{
				callback?.Invoke(new NetworkException(NetworkErrorCode.ServiceIsBusyWithAsync));
				return;
			}
			if (IsRunning)
			{
				callback?.Invoke(new NetworkException(NetworkErrorCode.SystemIsRunning));
				return;
			}
			SetUserAuthenticationData(token);
			authenticateUser.Callback = callback;
			authenticateUser.RegionCode = regionCode;
			SetState(State.AuthenticatingUserToken);
		}

		public void OnUpdate()
		{
			bool boltJustFinishedShuttingDown = false;
			if (boltIsShuttingDown && !IsRunning)
			{
				boltIsShuttingDown = false;
				boltJustFinishedShuttingDown = true;
			}
			switch (state)
			{
			case State.Cancelling:
				UpdateCancelling();
				break;
			case State.ShuttingDown:
				UpdateShuttingDown(ref boltJustFinishedShuttingDown);
				break;
			case State.GettingSessions:
				UpdateGettingSessions();
				break;
			}
			if (state != State.Idle && Time.realtimeSinceStartup - stateStartTime > StateTimeout)
			{
				HandleTimeout();
			}
		}

		public void OnRegister()
		{
		}

		public void OnAwake()
		{
			fixedTimeStepService = ServiceLocator.GetService<FixedTimeStepService>();
			LoadProjectMarsSettings();
		}

		public void OnStart()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void UnRegister()
		{
		}

		public override bool PersistBetweenStartupAndShutdown()
		{
			return true;
		}

		public override void BoltStartBegin()
		{
			base.BoltStartBegin();
			BoltNetwork.RegisterTokenClass<PhotonRoomProperties>();
			BoltNetwork.RegisterTokenClass<UnitSpawnToken>();
			BoltNetwork.RegisterTokenClass<ProjectileSpawnToken>();
			BoltNetwork.RegisterTokenClass<SpookySwordsAttackToken>();
			BoltNetwork.RegisterTokenClass<DarkPHandsTargetToken>();
			BoltNetwork.RegisterTokenClass<AddUnitEffectToTargetAddEffectToken>();
			BoltNetwork.RegisterTokenClass<BalloonerMeteorAttackToken>();
			BoltNetwork.RegisterTokenClass<AddExplosionEffectToChildToken>();
			BoltNetwork.RegisterTokenClass<PikeDropAttackToken>();
			BoltNetwork.RegisterTokenClass<SyncProjectileEffectToken>();
			cachedSessions.Clear();
			State state = this.state;
			if (state == State.Cancelling)
			{
				HandleCancel();
			}
		}

		public override void BoltStartDone()
		{
			base.BoltStartDone();
			if (BoltMatchmaking.CurrentMetadata != null && BoltMatchmaking.CurrentMetadata.ContainsKey("Region"))
			{
				RegionCode = BoltMatchmaking.CurrentMetadata["Region"] as string;
			}
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				if (IsServer)
				{
					CreateSessionFromInfo();
					break;
				}
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ServerModeRequired));
				break;
			case State.JoiningSession:
				if (IsClient)
				{
					JoinSessionFromInfo();
					break;
				}
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ClientModeRequired));
				break;
			case State.JoiningRandomSession:
				if (IsClient)
				{
					BoltMatchmaking.JoinRandomSession();
					break;
				}
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ClientModeRequired));
				break;
			case State.GettingSessions:
				if (IsClient)
				{
					getSessions.Timeout = Time.realtimeSinceStartup + GetSessionsTimeout;
					break;
				}
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ClientModeRequired));
				break;
			case State.AuthenticatingUserToken:
				if (IsServer)
				{
					SetState(State.Idle);
					authenticateUser.Callback?.Invoke(null);
				}
				else
				{
					SetState(State.Idle);
					authenticateUser.Callback?.Invoke(new NetworkException(NetworkErrorCode.ServerModeRequired));
				}
				break;
			default:
				Debug.LogErrorFormat($"Unsupported state: {state}");
				break;
			case State.ShuttingDown:
				break;
			}
		}

		public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
		{
			base.BoltShutdownBegin(registerDoneCallback, disconnectReason);
			ClearRegion();
			boltIsShuttingDown = true;
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Shutdown));
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Shutdown));
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Shutdown));
				break;
			case State.GettingSessions:
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Shutdown));
				break;
			case State.ShuttingDown:
				if (!IsRunning)
				{
					SetState(State.Idle);
					shutdown.Callback?.Invoke(null);
					this.LeftSession?.Invoke();
				}
				break;
			case State.AuthenticatingUserToken:
				SetState(State.Idle);
				authenticateUser.Callback?.Invoke(new NetworkException(NetworkErrorCode.Shutdown));
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
				break;
			}
		}

		public override void Connected(BoltConnection connection)
		{
			base.Connected(connection);
			if (BoltNetwork.IsServer && BoltMatchmaking.CurrentSession is PhotonSession session && session.GetProtocolToken() is PhotonRoomProperties photonRoomProperties)
			{
				photonRoomProperties.IsOpen = false;
				BoltMatchmaking.UpdateSession(photonRoomProperties);
			}
		}

		public override void SessionCreatedOrUpdated(UdpSession session)
		{
			base.SessionCreatedOrUpdated(session);
			UdpSession source = BoltMatchmaking.CurrentSession;
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(NetworkSessionHelper.ConvertSession(source), null);
				this.CreatedSession?.Invoke();
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
				break;
			}
		}

		public override void SessionConnected(UdpSession session, IProtocolToken token)
		{
			base.SessionConnected(session, token);
			UdpSession source = BoltMatchmaking.CurrentSession;
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(NetworkSessionHelper.ConvertSession(source), null);
				this.JoinedSession?.Invoke();
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(NetworkSessionHelper.ConvertSession(source), null);
				this.JoinedSession?.Invoke();
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			}
		}

		public override async void BoltStartFailed(UdpConnectionDisconnectReason disconnectReason)
		{
			base.BoltStartFailed(disconnectReason);
			NetworkErrorCode errorCode = NetworkErrorCode.FailedToStart;
			if (disconnectReason == UdpConnectionDisconnectReason.Authentication)
			{
				errorCode = NetworkErrorCode.UserAuthenticationFailed;
			}
			NetworkException exception = new NetworkException(errorCode);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, exception);
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, exception);
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, exception);
				break;
			case State.GettingSessions:
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, exception);
				break;
			case State.AuthenticatingUserToken:
				SetState(State.Idle);
				authenticateUser.Callback?.Invoke(exception);
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
			case State.ShuttingDown:
				break;
			}
		}

		public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
		{
			base.SessionListUpdated(sessionList);
			cachedSessions.Clear();
			if (sessionList != null && sessionList.Count > 0)
			{
				foreach (KeyValuePair<Guid, UdpSession> session in sessionList)
				{
					UdpSession value = session.Value;
					if (session.Value is PhotonSession)
					{
						cachedSessions.Add(NetworkSessionHelper.ConvertSession(value));
					}
				}
			}
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.GettingSessions:
				SetState(State.Idle);
				getSessions.Callback?.Invoke(cachedSessions.ToArray(), null);
				break;
			default:
				Debug.LogError($"Unsupported state {state}");
				break;
			case State.Idle:
			case State.CreatingSession:
			case State.JoiningSession:
			case State.JoiningRandomSession:
			case State.ShuttingDown:
				break;
			}
		}

		public override void ConnectFailed(UdpEndPoint endpoint, IProtocolToken token)
		{
			base.ConnectFailed(endpoint, token);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToServer));
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToServer));
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToServer));
				break;
			case State.GettingSessions:
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToServer));
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
			case State.ShuttingDown:
				break;
			}
		}

		public override void ConnectRefused(UdpEndPoint endpoint, IProtocolToken token)
		{
			base.ConnectRefused(endpoint, token);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ConnectionRefused));
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ConnectionRefused));
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ConnectionRefused));
				break;
			case State.GettingSessions:
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.ConnectionRefused));
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
			case State.ShuttingDown:
				break;
			}
		}

		public override void Disconnected(BoltConnection connection)
		{
			if (connection.DisconnectReason == UdpConnectionDisconnectReason.Timeout && connection.ConnectionType == UdpConnectionType.Unknown)
			{
				Debug.Log("Ignoring unknown timeout disconnection message.");
				return;
			}
			if (disruptionService == null)
			{
				disruptionService = ServiceLocator.GetService<GameDisruptionService>();
				disruptionService.GameDisrupted();
			}
			base.Disconnected(connection);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Disconnected));
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Disconnected));
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Disconnected));
				break;
			case State.GettingSessions:
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Disconnected));
				break;
			case State.ShuttingDown:
				SetState(State.Idle);
				shutdown.Callback?.Invoke(new NetworkException(NetworkErrorCode.Disconnected));
				this.LeftSession?.Invoke();
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
				break;
			}
		}

		public override void SessionConnectFailed(UdpSession session, IProtocolToken token, UdpSessionError errorReason)
		{
			base.SessionConnectFailed(session, token, errorReason);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToSession));
				break;
			case State.JoiningSession:
				SetState(State.Idle);
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToSession));
				break;
			case State.JoiningRandomSession:
				SetState(State.Idle);
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToConnectToSession));
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
				break;
			}
		}

		public override void SessionCreationFailed(UdpSession session, UdpSessionError errorReason)
		{
			base.SessionCreationFailed(session, errorReason);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				SetState(State.Idle);
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.FailedToCreateSession));
				break;
			default:
				Debug.LogError($"Unsupported state: {state}");
				break;
			case State.Idle:
				break;
			}
		}

		private static bool IsConnectedToRegion(PhotonRegion mySelectedRegion, string myRegionCode, string targetRegionCode)
		{
			if ((mySelectedRegion != null && mySelectedRegion.Code == targetRegionCode) || myRegionCode == targetRegionCode)
			{
				return true;
			}
			return string.IsNullOrEmpty(targetRegionCode);
		}

		private void SetState(State newState)
		{
			state = newState;
			stateStartTime = Time.realtimeSinceStartup;
			switch (state)
			{
			case State.CreatingSession:
				if (IsRunning && IsServer)
				{
					CreateSessionFromInfo();
				}
				else
				{
					StartServer();
				}
				break;
			case State.JoiningSession:
				if (IsRunning && IsClient)
				{
					JoinSessionFromInfo();
				}
				else
				{
					StartClient(joinSession.Properties.RegionCode);
				}
				break;
			case State.JoiningRandomSession:
				if (IsRunning && IsClient)
				{
					BoltMatchmaking.JoinRandomSession();
				}
				else
				{
					StartClient(null);
				}
				break;
			case State.GettingSessions:
				StartClient(null);
				break;
			case State.ShuttingDown:
				BoltNetwork.Shutdown();
				break;
			case State.AuthenticatingUserToken:
				StartServer(authenticateUser.RegionCode);
				break;
			default:
				Debug.LogError($"Unsupported state {state}");
				break;
			case State.Idle:
			case State.Cancelling:
				break;
			}
		}

		private void StartServer(string regionCode = null)
		{
			BoltLauncher.StartServer(SetupPlatform(regionCode));
		}

		private void StartClient(string regionCode)
		{
			BoltLauncher.StartClient(SetupPlatform(regionCode));
		}

		private BoltConfig SetupPlatform(string regionCode)
		{
			BoltConfig configCopy = BoltRuntimeSettings.instance.GetConfigCopy();
			configCopy.disableAutoSceneLoading = true;
			ClearRegion();
			PhotonPlatformConfig config = null;
			int? framesPerSecond = GetFramesPerSecond();
			int? num = null;
			if (num.HasValue)
			{
				configCopy.connectionTimeout = num.Value;
			}
			if (framesPerSecond.HasValue)
			{
				configCopy.framesPerSecond = framesPerSecond.Value;
			}
			selectedRegion = GetPlatformRegion(regionCode);
			if (selectedRegion != null)
			{
				CreateConfigIfNull(ref config);
				config.Region = selectedRegion;
			}
			BoltLauncher.SetUdpPlatform(new PhotonPlatform(config));
			return configCopy;
		}

		private void CreateConfigIfNull(ref PhotonPlatformConfig config)
		{
			if (config == null)
			{
				config = new PhotonPlatformConfig();
			}
		}

		private int? GetFramesPerSecond()
		{
			switch (fixedTimeStepService.CurrentFixedTimeStep)
			{
			case FixedTimeStepService.FixedTimeStep.SixtyUpdates:
				return 60;
			case FixedTimeStepService.FixedTimeStep.ThirtyUpdates:
				return 30;
			default:
				return null;
			}
		}

		private PhotonRegion GetPlatformRegion(string regionCode)
		{
			PhotonRegion result = ((PhotonRegion.regions != null) ? PhotonRegion.GetRegion(PhotonRegion.Regions.BEST_REGION) : null);
			if (string.IsNullOrEmpty(regionCode))
			{
				return result;
			}
			try
			{
				return (PhotonRegion.regions != null && !string.IsNullOrEmpty(regionCode)) ? PhotonRegion.GetRegion(regionCode) : null;
			}
			catch (Exception arg)
			{
				Debug.LogError($"Invalid region: {regionCode}. Using best region.\n{arg}");
				return result;
			}
		}

		private void ClearRegion()
		{
			RegionCode = null;
			selectedRegion = null;
		}

		private void UpdateCancelling()
		{
			bool flag = false;
			if (cancel.State == State.GettingSessions)
			{
				flag = getSessions.Timeout.HasValue;
			}
			if (!flag && !IsRunning)
			{
				cancel.NotRunningTime += Time.unscaledDeltaTime;
				flag = cancel.NotRunningTime > CancellingTimeout;
			}
			if (flag)
			{
				HandleCancel();
			}
		}

		private void UpdateShuttingDown(ref bool boltJustFinishedShuttingDown)
		{
			if (boltJustFinishedShuttingDown)
			{
				boltJustFinishedShuttingDown = false;
				SetState(State.Idle);
				shutdown.Callback?.Invoke(null);
				this.LeftSession?.Invoke();
			}
		}

		private void UpdateGettingSessions()
		{
			if (getSessions.Timeout.HasValue && !(getSessions.Timeout.Value > Time.realtimeSinceStartup))
			{
				SetState(State.Idle);
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Timeout));
			}
		}

		private void HandleCancel()
		{
			SetState(State.Idle);
			switch (cancel.State)
			{
			case State.CreatingSession:
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.UserCancelled));
				break;
			case State.JoiningSession:
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.UserCancelled));
				break;
			case State.JoiningRandomSession:
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.UserCancelled));
				break;
			case State.GettingSessions:
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.UserCancelled));
				break;
			case State.AuthenticatingUserToken:
				authenticateUser.Callback?.Invoke(new NetworkException(NetworkErrorCode.UserCancelled));
				break;
			default:
				Debug.LogError($"Unsupported state {cancel.State}");
				break;
			}
			ShutdownAsync(cancel.ShutdownCallback);
		}

		private void HandleTimeout()
		{
			State state = this.state;
			SetState(State.Idle);
			switch (state)
			{
			case State.Cancelling:
				HandleCancel();
				break;
			case State.CreatingSession:
				createSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Timeout));
				break;
			case State.JoiningSession:
				joinSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Timeout));
				break;
			case State.JoiningRandomSession:
				joinRandomSession.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Timeout));
				break;
			case State.GettingSessions:
				getSessions.Callback?.Invoke(null, new NetworkException(NetworkErrorCode.Timeout));
				break;
			case State.ShuttingDown:
				shutdown.Callback?.Invoke(new NetworkException(NetworkErrorCode.Timeout));
				break;
			case State.AuthenticatingUserToken:
				authenticateUser.Callback?.Invoke(new NetworkException(NetworkErrorCode.Timeout));
				break;
			default:
				Debug.LogError($"Unsupported state {state}");
				break;
			}
		}

		private void CreateSessionFromInfo()
		{
			string sessionID = "room_" + Guid.NewGuid().ToString();
			PhotonRoomProperties token = NetworkSessionHelper.ConvertRoomProperties(createSession.Properties);
			BoltMatchmaking.CreateSession(sessionID, token);
			projectMarsGameSettingsAsset.ResetSettings();
		}

		private void JoinSessionFromInfo()
		{
			projectMarsGameSettingsAsset.ResetSettings();
			BoltMatchmaking.JoinSession(joinSession.Properties.SessionId);
		}
	}
}
